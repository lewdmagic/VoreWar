#region

using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;

#endregion

enum ModdingMode
{
    Before,
    After
}

internal class RaceData<T> : IRaceData where T : IParameters
{
    private readonly Func<T> _makeTempState;

    private readonly IMiscRaceDataReadable<T> _miscRaceDataWritableReadable;



    
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////

    private readonly ExtraRaceInfo _extraRaceInfo;
    
    public string SingularName(Unit unit)
    {
        return SingularName(unit.GetGender());
    }    
    
    public string PluralName(Unit unit)
    {
        return PluralName(unit.GetGender());
    }    
    
    public string SingularName(Gender gender)
    {
        INameInput input = new NameInput(gender);
        return _extraRaceInfo.SingularName(input);
    }    
    
    public string PluralName(Gender gender)
    {
        INameInput input = new NameInput(gender);
        return _extraRaceInfo.PluralName(input);
    }

    public WallType WallType()
    {
        return _extraRaceInfo.WallType ?? TacticalMode.DefaultType;
    }

    public List<BoneInfo> BoneInfo(Unit unit)
    {
        return _extraRaceInfo.BoneTypesGen?.Invoke(unit);
    }

    public FlavorText FlavorText()
    {
        return _extraRaceInfo.FlavorText ?? LogRaceData.DefaultFlavorText;
    }

    public void CustomizeButtons(Unit unit, EnumIndexedArray<ButtonType, CustomizerButton> buttons)
    {
        _extraRaceInfo?.CustomizeButtonsAction?.Invoke(unit, buttons);

        if (_extraRaceInfo?.CustomizeButtonsAction2 != null)
        {
            ButtonCustomizer buttonCustomizer = new ButtonCustomizer();
            _extraRaceInfo.CustomizeButtonsAction2.Invoke(unit, buttonCustomizer);
            buttonCustomizer.ApplyValues(buttons);
        }
    }

    public ExtraRaceInfo ExtraRaceInfo()
    {
        return _extraRaceInfo;
    }

    public RaceTraits RaceTraits() => _extraRaceInfo.RaceTraits ?? RaceParameters.Default;


    
    private readonly Action<IRandomCustomInput> _randomCustom;
    private readonly Action<IRunInput, IRunOutput<T>> _runBefore;
    private readonly Action<IRunInput, IRaceRenderAllOutput<T>> _renderAllAction;

    public RaceData(
        SpriteTypeIndexed<SingleRenderFunc<T>> raceSpriteSet,
        MiscRaceDataWritableReadable<T> miscRaceDataWritableReadable,
        Action<IRunInput, IRunOutput<T>> runBefore,
        Action<IRandomCustomInput> randomCustom,
        Func<T> makeTempState,
        ExtraRaceInfo extraRaceInfo,
        Action<IRunInput, IRaceRenderAllOutput<T>> renderAllAction)
    {
        RaceSpriteSet = raceSpriteSet;
        _miscRaceDataWritableReadable = miscRaceDataWritableReadable;
        _runBefore = runBefore;
        _randomCustom = randomCustom;
        _makeTempState = makeTempState;
        _extraRaceInfo = extraRaceInfo;
        _renderAllAction = renderAllAction;
    }

    private SpriteTypeIndexed<SingleRenderFunc<T>> RaceSpriteSet { get; }

    public IMiscRaceData MiscRaceData => _miscRaceDataWritableReadable;

    
    internal void ModifySingleRender(SpriteType spriteType, ModdingMode mode, Action<IRaceRenderInput<T>, IRaceRenderOutput> generator)
    {
        SingleRenderFunc<T> current = RaceSpriteSet[spriteType];
        if (current != null)
        {
            if (mode == ModdingMode.Before)
            {
                current.ModBefore(generator);
            }

            if (mode == ModdingMode.After)
            {
                current.ModAfter(generator);
            }
        }
        else
        {
            throw new Exception("Tried to modify " + spriteType + " which does not exist. Use ReplaceSingleRender instead");
        }
    }

    internal void ReplaceSingleRender(SpriteType spriteType, int layer, Action<IRaceRenderInput<T>, IRaceRenderOutput> generator)
    {
        RaceSpriteSet[spriteType] = new SingleRenderFunc<T>(layer, generator);
    }

    public FullSpriteProcessOut NewUpdate(Actor_Unit actor)
    {
        T state = _makeTempState();

        SpriteChangeDict changeDict = new SpriteChangeDict();
        RunOutput<T> runOutput = new RunOutput<T>(changeDict, state);

        IRunInput runInput = new RunInput(actor);
        _runBefore?.Invoke(runInput, runOutput);

        IRaceRenderAllOutput<T> renderAllOutput = new RaceRenderAllOutput<T>(changeDict, state);
        
        try
        {
            _renderAllAction?.Invoke(runInput, renderAllOutput);
        }
        catch (ScriptRuntimeException ex)
        {
            Debug.Log("Doh! An error occured! " + ex.DecoratedMessage);
        }

        foreach (KeyValuePair<SpriteType,SingleRenderFunc<T>> raceSprite in RaceSpriteSet.KeyValues)
        {
            SpriteType spriteType = raceSprite.Key;
            SingleRenderFunc<T> renderFunc = raceSprite.Value;
            IRaceRenderInput<T> input = new RaceRenderInput<T>(actor, _miscRaceDataWritableReadable, MiscRaceData.BaseBody, state);
            IRaceRenderOutput raceRenderOutput = changeDict.ChangeSprite(spriteType);
            raceRenderOutput.Layer(renderFunc.Layer); // Set the default layer
            try
            {
                renderFunc.Invoke(input, raceRenderOutput);
            }
            catch (Exception e)
            {
                Debug.LogError($"Render Error in SpriteType: {spriteType}");
                throw;
            }
        }

        if (runOutput.ActorFurry.HasValue)
        {
            actor.Unit.Furry = runOutput.ActorFurry.Value; // This should probably be changed to be somewhere else. 
            // This isnt an appropriate place for it, but it's used by clothing generators. TODO
        }

        actor.SquishedBreasts = false;

        // Advanced, slightly different behavior; 
            
        List<ClothingRenderOutput> results = ClothingResults(actor, state, changeDict);
        AccumulatedClothes accumulatedClothes = Accumulate(results, actor);
            
        return new FullSpriteProcessOut(runOutput, changeDict.ReusedChangesDict, accumulatedClothes);
    }


    public void RandomCustomCall(Unit unit)
    {
        _randomCustom(new RandomCustomInput(unit, _miscRaceDataWritableReadable));
    }

    private List<ClothingRenderOutput> ClothingResults(Actor_Unit actorUnit, T state, SpriteChangeDict changeDict)
    {
        List<ClothingRenderOutput> clothingResults = new List<ClothingRenderOutput>();

        if (actorUnit.Unit.ClothingType > 0)
        {
            if (actorUnit.Unit.ClothingType <= _miscRaceDataWritableReadable.AllowedMainClothingTypesRead.Count)
            {
                clothingResults.Add(_miscRaceDataWritableReadable.AllowedMainClothingTypesRead[actorUnit.Unit.ClothingType - 1]
                    .Configure(actorUnit, state, changeDict));

                if (actorUnit.Unit.ClothingType2 > 0 &&
                    actorUnit.Unit.ClothingType2 <= _miscRaceDataWritableReadable.AllowedWaistTypesRead.Count && _miscRaceDataWritableReadable
                        .AllowedMainClothingTypesRead[actorUnit.Unit.ClothingType - 1].FixedData.OccupiesAllSlots == false)
                {
                    clothingResults.Add(_miscRaceDataWritableReadable.AllowedWaistTypesRead[actorUnit.Unit.ClothingType2 - 1]
                        .Configure(actorUnit, state, changeDict));
                }
            }
            else
            {
                Debug.Log("Invalid Clothing Type Detected and Nullified");
                actorUnit.Unit.ClothingType = 0;
            }
        }
        else
        {
            if (actorUnit.Unit.ClothingType2 > 0 && actorUnit.Unit.ClothingType2 <= _miscRaceDataWritableReadable.AllowedWaistTypesRead.Count)
            {
                clothingResults.Add(_miscRaceDataWritableReadable.AllowedWaistTypesRead[actorUnit.Unit.ClothingType2 - 1]
                    .Configure(actorUnit, state, changeDict));
            }
        }

        if (actorUnit.Unit.ClothingExtraType1 > 0 &&
            actorUnit.Unit.ClothingExtraType1 <= _miscRaceDataWritableReadable.ExtraMainClothing1TypesRead.Count)
        {
            clothingResults.Add(_miscRaceDataWritableReadable.ExtraMainClothing1TypesRead[actorUnit.Unit.ClothingExtraType1 - 1]
                .Configure(actorUnit, state, changeDict));
        }

        if (actorUnit.Unit.ClothingExtraType2 > 0 &&
            actorUnit.Unit.ClothingExtraType2 <= _miscRaceDataWritableReadable.ExtraMainClothing2TypesRead.Count)
        {
            clothingResults.Add(_miscRaceDataWritableReadable.ExtraMainClothing2TypesRead[actorUnit.Unit.ClothingExtraType2 - 1]
                .Configure(actorUnit, state, changeDict));
        }

        if (actorUnit.Unit.ClothingExtraType3 > 0 &&
            actorUnit.Unit.ClothingExtraType3 <= _miscRaceDataWritableReadable.ExtraMainClothing3TypesRead.Count)
        {
            clothingResults.Add(_miscRaceDataWritableReadable.ExtraMainClothing3TypesRead[actorUnit.Unit.ClothingExtraType3 - 1]
                .Configure(actorUnit, state, changeDict));
        }

        if (actorUnit.Unit.ClothingExtraType4 > 0 &&
            actorUnit.Unit.ClothingExtraType4 <= _miscRaceDataWritableReadable.ExtraMainClothing4TypesRead.Count)
        {
            clothingResults.Add(_miscRaceDataWritableReadable.ExtraMainClothing4TypesRead[actorUnit.Unit.ClothingExtraType4 - 1]
                .Configure(actorUnit, state, changeDict));
        }

        if (actorUnit.Unit.ClothingExtraType5 > 0 &&
            actorUnit.Unit.ClothingExtraType5 <= _miscRaceDataWritableReadable.ExtraMainClothing5TypesRead.Count)
        {
            clothingResults.Add(_miscRaceDataWritableReadable.ExtraMainClothing5TypesRead[actorUnit.Unit.ClothingExtraType5 - 1]
                .Configure(actorUnit, state, changeDict));
        }

        if (actorUnit.Unit.ClothingHatType > 0 &&
            actorUnit.Unit.ClothingHatType <= _miscRaceDataWritableReadable.AllowedClothingHatTypesRead.Count)
        {
            clothingResults.Add(_miscRaceDataWritableReadable.AllowedClothingHatTypesRead[actorUnit.Unit.ClothingHatType - 1]
                .Configure(actorUnit, state, changeDict));
        }

        if (actorUnit.Unit.ClothingAccessoryType > 0 &&
            actorUnit.Unit.ClothingAccessoryType <= _miscRaceDataWritableReadable.AllowedClothingAccessoryTypesRead.Count)
        {
            clothingResults.Add(_miscRaceDataWritableReadable
                .AllowedClothingAccessoryTypesRead[actorUnit.Unit.ClothingAccessoryType - 1].Configure(actorUnit, state, changeDict));
        }
        else if (actorUnit.Unit.EarnedMask && actorUnit.Unit.ClothingAccessoryType > 0 && actorUnit.Unit.ClothingAccessoryType - 1 ==
                 _miscRaceDataWritableReadable.AllowedClothingAccessoryTypesRead.Count)
        {
            IClothing asuraMask;
            // switch (RaceFuncs.RaceToSwitch(actorUnit.Unit.Race))
            // {
            //     case RaceNumbers.Imps:
            //     case RaceNumbers.Goblins:
            //         asuraMask = AsuraMask.AsuraMaskInstanceImpsGoblins;
            //         break;
            //     case RaceNumbers.Taurus:
            //         asuraMask = AsuraMask.AsuraMaskInstanceTaurus;
            //         break;
            //     default:
            //         asuraMask = AsuraMask.AsuraMaskInstanceNormal;
            //         break;
            // }

            if (Equals(actorUnit.Unit.Race, Race.Imps) || Equals(actorUnit.Unit.Race, Race.Goblins))
            {
                asuraMask = AsuraMask.AsuraMaskInstanceImpsGoblins;
            }
            else if (Equals(actorUnit.Unit.Race, Race.Taurus))
            {
                asuraMask = AsuraMask.AsuraMaskInstanceTaurus;
            }
            else
            {
                asuraMask = AsuraMask.AsuraMaskInstanceNormal;
            }
            
            clothingResults.Add(asuraMask.Configure(actorUnit, state, changeDict));
        }

        return clothingResults;
    }

    private AccumulatedClothes Accumulate(List<ClothingRenderOutput> clothingResults, Actor_Unit actor)
    {
        AccumulatedClothes accumulatedClothes = new AccumulatedClothes();

        bool revealsBreasts = true;
        bool revealsDick = true;
        foreach (ClothingRenderOutput clothingOut in clothingResults)
        {
            if (!clothingOut.SkipCheck)
            {
                if ((!clothingOut.RevealsDick || clothingOut.InFrontOfDick) && Config.CockVoreHidesClothes &&
                    actor.PredatorComponent?.BallsFullness > 0)
                {
                    // Skip this clothing item. 
                    continue;
                }
            }

            if (!clothingOut.RevealsBreasts)
            {
                revealsBreasts = false;
            }

            if (!clothingOut.RevealsDick)
            {
                revealsDick = false;
            }

            if (clothingOut.BlocksBreasts)
            {
                accumulatedClothes.BlocksBreasts = true;
            }

            if (clothingOut.InFrontOfDick)
            {
                accumulatedClothes.InFrontOfDick = true;
            }

            accumulatedClothes.SpritesInfos.AddRange(clothingOut.ClothingSpriteChanges.Values);
        }

        accumulatedClothes.RevealsBreasts = revealsBreasts;
        accumulatedClothes.RevealsDick = revealsDick;

        return accumulatedClothes;
    }
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    ///////////////////////// API IMPLEMENTATIONS

    private class RunInput : RenderInput, IRunInput
    {
        internal RunInput(Actor_Unit actor) : base(actor)
        {
            
        }
    }

    private abstract class RaceRenderInput : RenderInput, IRaceRenderInput
    {
        protected RaceRenderInput(Actor_Unit actor, IMiscRaceData miscRaceData, bool baseBody) : base(actor)
        {
            RaceData = miscRaceData;
            BaseBody = baseBody;
        }

        public IMiscRaceData RaceData { get; private set; }
        public bool BaseBody { get; private set; }
    }


    private class RaceRenderInput<TU> : RaceRenderInput, IRaceRenderInput<TU> where TU : IParameters
    {
        internal RaceRenderInput(Actor_Unit actor, IMiscRaceData miscRaceData, bool baseBody, TU state)
            : base(actor, miscRaceData, baseBody)
        {
            Params = state;
        }

        public TU Params { get; private set; }
    }



    private class RandomCustomInput : IRandomCustomInput
    {
        internal RandomCustomInput(Unit unit, IMiscRaceData miscRaceData)
        {
            Unit = unit;
            MiscRaceData = miscRaceData;
        }

        public Unit Unit { get; }
        public IMiscRaceData MiscRaceData { get; }
    }


    private class RunOutput : RunOutputShared
    {
        protected RunOutput(SpriteChangeDict spriteChangeDict) : base(spriteChangeDict)
        {
        }
    }

    private class RunOutput<TU> : RunOutput, IRunOutput<TU> where TU : IParameters
    {
        public RunOutput(SpriteChangeDict spriteChangeDict, TU state) : base(spriteChangeDict)
        {
            Params = state;
        }

        public TU Params { get; }
    }
}
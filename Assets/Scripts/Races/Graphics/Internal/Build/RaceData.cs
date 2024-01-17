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

    private readonly MiscRaceData _miscRaceData;



    
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
    private readonly Action<IRunInput, IRunOutput> _runBefore;
    private readonly Action<IRunInput, IRaceRenderAllOutput> _renderAllAction;

    public RaceData(
        SpriteTypeIndexed<SingleRenderFunc> raceSpriteSet,
        MiscRaceData miscRaceData,
        Action<IRunInput, IRunOutput> runBefore,
        Action<IRandomCustomInput> randomCustom,
        Func<T> makeTempState,
        ExtraRaceInfo extraRaceInfo,
        Action<IRunInput, IRaceRenderAllOutput> renderAllAction)
    {
        RaceSpriteSet = raceSpriteSet;
        _miscRaceData = miscRaceData;
        _runBefore = runBefore;
        _randomCustom = randomCustom;
        _makeTempState = makeTempState;
        _extraRaceInfo = extraRaceInfo;
        _renderAllAction = renderAllAction;
    }

    private SpriteTypeIndexed<SingleRenderFunc> RaceSpriteSet { get; }

    public IMiscRaceData MiscRaceData => _miscRaceData;
    
    internal void ModifySingleRender(SpriteType spriteType, ModdingMode mode, Action<IRaceRenderInput, IRaceRenderOutput> generator)
    {
        SingleRenderFunc current = RaceSpriteSet[spriteType];
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

    internal void ReplaceSingleRender(SpriteType spriteType, int layer, Action<IRaceRenderInput, IRaceRenderOutput> generator)
    {
        RaceSpriteSet[spriteType] = new SingleRenderFunc(layer, generator);
    }

    public FullSpriteProcessOut NewUpdate(Actor_Unit actor)
    {
        T state = _makeTempState();

        SpriteChangeDict changeDict = new SpriteChangeDict(State.SpriteManager);
        RunOutput runOutput = new RunOutput(changeDict);

        IRunInput runInput = new RunInput(actor);
        _runBefore?.Invoke(runInput, runOutput);

        IRaceRenderAllOutput renderAllOutput = new RaceRenderAllOutput(changeDict);
        
        try
        {
            _renderAllAction?.Invoke(runInput, renderAllOutput);
        }
        catch (ScriptRuntimeException ex)
        {
            Debug.Log("Doh! An error occured! " + ex.DecoratedMessage);
        }

        foreach (KeyValuePair<SpriteType, SingleRenderFunc> raceSprite in RaceSpriteSet.KeyValues)
        {
            SpriteType spriteType = raceSprite.Key;
            SingleRenderFunc renderFunc = raceSprite.Value;
            IRaceRenderInput input = new RaceRenderInput(actor, _miscRaceData, MiscRaceData.BaseBody);
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
        _randomCustom(new RandomCustomInput(unit, _miscRaceData));
    }

    private List<ClothingRenderOutput> ClothingResults(Actor_Unit actorUnit, T state, SpriteChangeDict changeDict)
    {
        List<ClothingRenderOutput> clothingResults = new List<ClothingRenderOutput>();

        if (actorUnit.Unit.ClothingType > 0)
        {
            if (actorUnit.Unit.ClothingType <= _miscRaceData.AllowedMainClothingTypes.Count)
            {
                clothingResults.Add(_miscRaceData.AllowedMainClothingTypes[actorUnit.Unit.ClothingType - 1]
                    .Configure(actorUnit, changeDict));

                if (actorUnit.Unit.ClothingType2 > 0 &&
                    actorUnit.Unit.ClothingType2 <= _miscRaceData.AllowedWaistTypes.Count && _miscRaceData
                        .AllowedMainClothingTypes[actorUnit.Unit.ClothingType - 1].FixedData.OccupiesAllSlots == false)
                {
                    clothingResults.Add(_miscRaceData.AllowedWaistTypes[actorUnit.Unit.ClothingType2 - 1]
                        .Configure(actorUnit, changeDict));
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
            if (actorUnit.Unit.ClothingType2 > 0 && actorUnit.Unit.ClothingType2 <= _miscRaceData.AllowedWaistTypes.Count)
            {
                clothingResults.Add(_miscRaceData.AllowedWaistTypes[actorUnit.Unit.ClothingType2 - 1]
                    .Configure(actorUnit, changeDict));
            }
        }

        if (actorUnit.Unit.ClothingExtraType1 > 0 &&
            actorUnit.Unit.ClothingExtraType1 <= _miscRaceData.ExtraMainClothing1Types.Count)
        {
            clothingResults.Add(_miscRaceData.ExtraMainClothing1Types[actorUnit.Unit.ClothingExtraType1 - 1]
                .Configure(actorUnit, changeDict));
        }

        if (actorUnit.Unit.ClothingExtraType2 > 0 &&
            actorUnit.Unit.ClothingExtraType2 <= _miscRaceData.ExtraMainClothing2Types.Count)
        {
            clothingResults.Add(_miscRaceData.ExtraMainClothing2Types[actorUnit.Unit.ClothingExtraType2 - 1]
                .Configure(actorUnit, changeDict));
        }

        if (actorUnit.Unit.ClothingExtraType3 > 0 &&
            actorUnit.Unit.ClothingExtraType3 <= _miscRaceData.ExtraMainClothing3Types.Count)
        {
            clothingResults.Add(_miscRaceData.ExtraMainClothing3Types[actorUnit.Unit.ClothingExtraType3 - 1]
                .Configure(actorUnit, changeDict));
        }

        if (actorUnit.Unit.ClothingExtraType4 > 0 &&
            actorUnit.Unit.ClothingExtraType4 <= _miscRaceData.ExtraMainClothing4Types.Count)
        {
            clothingResults.Add(_miscRaceData.ExtraMainClothing4Types[actorUnit.Unit.ClothingExtraType4 - 1]
                .Configure(actorUnit, changeDict));
        }

        if (actorUnit.Unit.ClothingExtraType5 > 0 &&
            actorUnit.Unit.ClothingExtraType5 <= _miscRaceData.ExtraMainClothing5Types.Count)
        {
            clothingResults.Add(_miscRaceData.ExtraMainClothing5Types[actorUnit.Unit.ClothingExtraType5 - 1]
                .Configure(actorUnit, changeDict));
        }

        if (actorUnit.Unit.ClothingHatType > 0 &&
            actorUnit.Unit.ClothingHatType <= _miscRaceData.AllowedClothingHatTypes.Count)
        {
            clothingResults.Add(_miscRaceData.AllowedClothingHatTypes[actorUnit.Unit.ClothingHatType - 1]
                .Configure(actorUnit, changeDict));
        }

        if (actorUnit.Unit.ClothingAccessoryType > 0 &&
            actorUnit.Unit.ClothingAccessoryType <= _miscRaceData.AllowedClothingAccessoryTypes.Count)
        {
            clothingResults.Add(_miscRaceData
                .AllowedClothingAccessoryTypes[actorUnit.Unit.ClothingAccessoryType - 1].Configure(actorUnit, changeDict));
        }
        else if (actorUnit.Unit.EarnedMask && actorUnit.Unit.ClothingAccessoryType > 0 && actorUnit.Unit.ClothingAccessoryType - 1 ==
                 _miscRaceData.AllowedClothingAccessoryTypes.Count)
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
            
            clothingResults.Add(asuraMask.Configure(actorUnit, changeDict));
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

    internal class RaceRenderInput : RenderInput, IRaceRenderInput
    {
        internal RaceRenderInput(Actor_Unit actor, IMiscRaceData miscRaceData, bool baseBody) : base(actor)
        {
            RaceData = miscRaceData;
            BaseBody = baseBody;
        }

        public IMiscRaceData RaceData { get; private set; }
        public bool BaseBody { get; private set; }
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
}
#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

internal abstract class RaceBuilderShared
{
    private protected class RaceData<T> : IRaceData where T : IParameters
    {
        private readonly Func<T> _makeTempState;

        private readonly IMiscRaceDataReadable<T> _miscRaceDataReadable;

        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////

        private readonly Action<IRandomCustomInput> _randomCustom;
        private readonly Action<IRunInput, IRunOutput<T>> _runBefore;

        public RaceData(SpriteTypeIndexed<SingleRenderFunc<T>> raceSpriteSet,
            MiscRaceDataReadable<T> miscRaceDataReadable,
            Action<IRunInput, IRunOutput<T>> runBefore,
            Action<IRandomCustomInput> randomCustom,
            Func<T> makeTempState)
        {
            RaceSpriteSet = raceSpriteSet;
            _miscRaceDataReadable = miscRaceDataReadable;
            _runBefore = runBefore;
            _randomCustom = randomCustom;
            _makeTempState = makeTempState;
        }

        private SpriteTypeIndexed<SingleRenderFunc<T>> RaceSpriteSet { get; }

        public IMiscRaceData MiscRaceData => _miscRaceDataReadable;


        public FullSpriteProcessOut NewUpdate(Actor_Unit actor)
        {
            T state = _makeTempState();

            SpriteChangeDict changeDict = new SpriteChangeDict();
            RunOutput<T> runOutput = new RunOutput<T>(changeDict, state);

            IRunInput runInput = new RunInput(actor);

            _runBefore?.Invoke(runInput, runOutput);

            foreach (KeyValuePair<SpriteType,SingleRenderFunc<T>> raceSprite in RaceSpriteSet.KeyValues)
            {
                SpriteType spriteType = raceSprite.Key;
                SingleRenderFunc<T> renderFunc = raceSprite.Value;
                IRaceRenderInput<T> input = new RaceRenderInput<T>(actor, _miscRaceDataReadable, MiscRaceData.BaseBody, state);
                IRaceRenderOutput raceRenderOutput = changeDict.ChangeSprite(spriteType);
                raceRenderOutput.Layer(renderFunc.Layer); // Set the default layer
                renderFunc.Generator.Invoke(input, raceRenderOutput);
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
            _randomCustom(new RandomCustomInput(unit, _miscRaceDataReadable));
        }

        private List<ClothingRenderOutput> ClothingResults(Actor_Unit actorUnit, T state, SpriteChangeDict changeDict)
        {
            List<ClothingRenderOutput> clothingResults = new List<ClothingRenderOutput>();

            if (actorUnit.Unit.ClothingType > 0)
            {
                if (actorUnit.Unit.ClothingType <= _miscRaceDataReadable.AllowedMainClothingTypes.Count)
                {
                    clothingResults.Add(_miscRaceDataReadable.AllowedMainClothingTypes[actorUnit.Unit.ClothingType - 1]
                        .Configure(actorUnit, state, changeDict));

                    if (actorUnit.Unit.ClothingType2 > 0 &&
                        actorUnit.Unit.ClothingType2 <= _miscRaceDataReadable.AllowedWaistTypes.Count && _miscRaceDataReadable
                            .AllowedMainClothingTypes[actorUnit.Unit.ClothingType - 1].FixedData.OccupiesAllSlots == false)
                    {
                        clothingResults.Add(_miscRaceDataReadable.AllowedWaistTypes[actorUnit.Unit.ClothingType2 - 1]
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
                if (actorUnit.Unit.ClothingType2 > 0 && actorUnit.Unit.ClothingType2 <= _miscRaceDataReadable.AllowedWaistTypes.Count)
                {
                    clothingResults.Add(_miscRaceDataReadable.AllowedWaistTypes[actorUnit.Unit.ClothingType2 - 1]
                        .Configure(actorUnit, state, changeDict));
                }
            }

            if (actorUnit.Unit.ClothingExtraType1 > 0 &&
                actorUnit.Unit.ClothingExtraType1 <= _miscRaceDataReadable.ExtraMainClothing1Types.Count)
            {
                clothingResults.Add(_miscRaceDataReadable.ExtraMainClothing1Types[actorUnit.Unit.ClothingExtraType1 - 1]
                    .Configure(actorUnit, state, changeDict));
            }

            if (actorUnit.Unit.ClothingExtraType2 > 0 &&
                actorUnit.Unit.ClothingExtraType2 <= _miscRaceDataReadable.ExtraMainClothing2Types.Count)
            {
                clothingResults.Add(_miscRaceDataReadable.ExtraMainClothing2Types[actorUnit.Unit.ClothingExtraType2 - 1]
                    .Configure(actorUnit, state, changeDict));
            }

            if (actorUnit.Unit.ClothingExtraType3 > 0 &&
                actorUnit.Unit.ClothingExtraType3 <= _miscRaceDataReadable.ExtraMainClothing3Types.Count)
            {
                clothingResults.Add(_miscRaceDataReadable.ExtraMainClothing3Types[actorUnit.Unit.ClothingExtraType3 - 1]
                    .Configure(actorUnit, state, changeDict));
            }

            if (actorUnit.Unit.ClothingExtraType4 > 0 &&
                actorUnit.Unit.ClothingExtraType4 <= _miscRaceDataReadable.ExtraMainClothing4Types.Count)
            {
                clothingResults.Add(_miscRaceDataReadable.ExtraMainClothing4Types[actorUnit.Unit.ClothingExtraType4 - 1]
                    .Configure(actorUnit, state, changeDict));
            }

            if (actorUnit.Unit.ClothingExtraType5 > 0 &&
                actorUnit.Unit.ClothingExtraType5 <= _miscRaceDataReadable.ExtraMainClothing5Types.Count)
            {
                clothingResults.Add(_miscRaceDataReadable.ExtraMainClothing5Types[actorUnit.Unit.ClothingExtraType5 - 1]
                    .Configure(actorUnit, state, changeDict));
            }

            if (actorUnit.Unit.ClothingHatType > 0 &&
                actorUnit.Unit.ClothingHatType <= _miscRaceDataReadable.AllowedClothingHatTypes.Count)
            {
                clothingResults.Add(_miscRaceDataReadable.AllowedClothingHatTypes[actorUnit.Unit.ClothingHatType - 1]
                    .Configure(actorUnit, state, changeDict));
            }

            if (actorUnit.Unit.ClothingAccessoryType > 0 &&
                actorUnit.Unit.ClothingAccessoryType <= _miscRaceDataReadable.AllowedClothingAccessoryTypes.Count)
            {
                clothingResults.Add(_miscRaceDataReadable
                    .AllowedClothingAccessoryTypes[actorUnit.Unit.ClothingAccessoryType - 1].Configure(actorUnit, state, changeDict));
            }
            else if (actorUnit.Unit.EarnedMask && actorUnit.Unit.ClothingAccessoryType > 0 && actorUnit.Unit.ClothingAccessoryType - 1 ==
                     _miscRaceDataReadable.AllowedClothingAccessoryTypes.Count)
            {
                clothingResults.Add(MainAccessories.AsuraMaskInstance.Configure(actorUnit, state, changeDict));
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

        private class RunInput : IRunInput
        {
            internal RunInput(Actor_Unit actor)
            {
                Actor = actor;
            }

            public Actor_Unit Actor { get; private set; }
        }

        private abstract class RaceRenderInput : IRaceRenderInput
        {
            protected RaceRenderInput(Actor_Unit actor, IMiscRaceData miscRaceData, bool baseBody)
            {
                Actor = actor;
                RaceData = miscRaceData;
                BaseBody = baseBody;
                Sprites = State.GameManager.SpriteDictionary;
            }

            public Actor_Unit Actor { get; private set; }
            public SpriteDictionary Sprites { get; private set; }
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
}
#region

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion


internal partial class RaceRenderer
{
    public static readonly SpriteType AssumedFluffType = SpriteType.BodyAccent3;

    private readonly List<ISpriteContainer> _clothingSprites = new List<ISpriteContainer>();
    private readonly SpriteTypeIndexed<ISpriteContainer> _sprites = new SpriteTypeIndexed<ISpriteContainer>();
    private IEnumerable<ISpriteContainer> AllContainers => _sprites.Concat(_clothingSprites);

    private readonly ActorUnit _actor;
    private readonly Transform _folder;
    private readonly GameObject _type;


    internal RaceRenderer(GameObject type, GameObject animatedType, Transform folder, ActorUnit actor)
    {
        var animatedType1 = animatedType;
        _type = type;
        _folder = folder;
        _actor = actor;

        foreach (SpriteType spriteType in EnumUtil.GetValues<SpriteType>())
        {
            ISpriteContainer newContainer;
            if (animatedType1 != null && (
                    spriteType == SpriteType.Belly ||
                    spriteType == SpriteType.Balls ||
                    spriteType == SpriteType.Breasts ||
                    spriteType == SpriteType.SecondaryBreasts))
            {
                newContainer = SpriteContainer.MakeContainer(animatedType1, _folder);
            }
            else
            {
                newContainer = SpriteContainer.MakeContainer(_type, _folder);
            }

            newContainer.Name = spriteType.ToString();
            _sprites[spriteType] = newContainer;
        }
    }

    private ISpriteContainer GetClothingContainer(int index)
    {
        return _clothingSprites.GetOrAdd(index, () => SpriteContainer.MakeContainer(_type, _folder));
    }

    internal void Destroy()
    {
        foreach (ISpriteContainer container in AllContainers)
        {
            if (container != null)
            {
                container.Destroy();
            }
        }
    }

    internal ISpriteContainer GetSpriteOfType(SpriteType spriteType)
    {
        return _sprites[spriteType];
    }

    private void HideSprite(SpriteType spriteType)
    {
        if (_sprites[spriteType] != null)
        {
            _sprites[spriteType].GameObject.SetActive(false);
        }
    }


    private void ChangeLayer(SpriteType spriteType, int layer)
    {
        if (_sprites[spriteType] != null)
        {
            _sprites[spriteType].SortOrder = layer + _actor.SpriteLayerOffset;
        }
    }

    internal void ResetBellyScale()
    {
        if (_sprites[SpriteType.Belly]?.GameObject != null)
        {
            _sprites[SpriteType.Belly].GameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////


    private static RunOutput CalculateRaceRender(ActorUnit actor, IRaceData raceData, RaceSpriteChangeDict changeDict)
    {
        IRunInput runInput = new RunInput(actor);
        RunOutput runOutput = new RunOutput(changeDict);

        raceData.RunBefore?.Invoke(runInput, runOutput);

        IRaceRenderAllOutput renderAllOutput = new RaceRenderAllOutput(changeDict);

        raceData.RenderAllAction?.Invoke(runInput, renderAllOutput);

        foreach (KeyValuePair<SpriteType, SingleRenderFunc> raceSprite in raceData.RaceSpriteSet.KeyValues)
        {
            SpriteType spriteType = raceSprite.Key;
            SingleRenderFunc renderFunc = raceSprite.Value;
            IRaceRenderInput input = new RaceRenderInput(actor, raceData.SetupOutputRaw, raceData.SetupOutput.BaseBody);
            IRaceRenderOutput raceRenderOutput = changeDict.ChangeSprite(spriteType);
            raceRenderOutput.Layer(renderFunc.Layer); // Set the default layer
            try
            {
                renderFunc.Invoke(input, raceRenderOutput);
            }
            catch (Exception e)
            {
                // TODO improve formatting? 
                Debug.LogError($"Render Error in SpriteType: {spriteType}\n{e.Message}\n{e.StackTrace}");
                throw;
            }
        }

        if (runOutput.ActorFurry.HasValue)
        {
            actor.Unit.Furry = runOutput.ActorFurry.Value; // This should probably be changed to be somewhere else. 
            // This isnt an appropriate place for it, but it's used by clothing generators. TODO
        }

        actor.SquishedBreasts = false;

        return runOutput;
    }


    private static AccumulatedClothes CalculateClothingRender(ActorUnit actorUnit, RaceSpriteChangeDict changeDict, SetupOutput setupOutput)
    {
        List<ClothingRenderOutput> clothingResults = new List<ClothingRenderOutput>();

        if (actorUnit.Unit.ClothingType > 0)
        {
            if (actorUnit.Unit.ClothingType <= setupOutput.AllowedMainClothingTypes.Count)
            {
                clothingResults.Add(setupOutput.AllowedMainClothingTypes[actorUnit.Unit.ClothingType - 1]
                    .Configure(actorUnit, changeDict));

                if (actorUnit.Unit.ClothingType2 > 0 &&
                    actorUnit.Unit.ClothingType2 <= setupOutput.AllowedWaistTypes.Count && setupOutput
                        .AllowedMainClothingTypes[actorUnit.Unit.ClothingType - 1].FixedData.OccupiesAllSlots == false)
                {
                    clothingResults.Add(setupOutput.AllowedWaistTypes[actorUnit.Unit.ClothingType2 - 1]
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
            if (actorUnit.Unit.ClothingType2 > 0 && actorUnit.Unit.ClothingType2 <= setupOutput.AllowedWaistTypes.Count)
            {
                clothingResults.Add(setupOutput.AllowedWaistTypes[actorUnit.Unit.ClothingType2 - 1]
                    .Configure(actorUnit, changeDict));
            }
        }

        if (actorUnit.Unit.ClothingExtraType1 > 0 &&
            actorUnit.Unit.ClothingExtraType1 <= setupOutput.ExtraMainClothing1Types.Count)
        {
            clothingResults.Add(setupOutput.ExtraMainClothing1Types[actorUnit.Unit.ClothingExtraType1 - 1]
                .Configure(actorUnit, changeDict));
        }

        if (actorUnit.Unit.ClothingExtraType2 > 0 &&
            actorUnit.Unit.ClothingExtraType2 <= setupOutput.ExtraMainClothing2Types.Count)
        {
            clothingResults.Add(setupOutput.ExtraMainClothing2Types[actorUnit.Unit.ClothingExtraType2 - 1]
                .Configure(actorUnit, changeDict));
        }

        if (actorUnit.Unit.ClothingExtraType3 > 0 &&
            actorUnit.Unit.ClothingExtraType3 <= setupOutput.ExtraMainClothing3Types.Count)
        {
            clothingResults.Add(setupOutput.ExtraMainClothing3Types[actorUnit.Unit.ClothingExtraType3 - 1]
                .Configure(actorUnit, changeDict));
        }

        if (actorUnit.Unit.ClothingExtraType4 > 0 &&
            actorUnit.Unit.ClothingExtraType4 <= setupOutput.ExtraMainClothing4Types.Count)
        {
            clothingResults.Add(setupOutput.ExtraMainClothing4Types[actorUnit.Unit.ClothingExtraType4 - 1]
                .Configure(actorUnit, changeDict));
        }

        if (actorUnit.Unit.ClothingExtraType5 > 0 &&
            actorUnit.Unit.ClothingExtraType5 <= setupOutput.ExtraMainClothing5Types.Count)
        {
            clothingResults.Add(setupOutput.ExtraMainClothing5Types[actorUnit.Unit.ClothingExtraType5 - 1]
                .Configure(actorUnit, changeDict));
        }

        if (actorUnit.Unit.ClothingHatType > 0 &&
            actorUnit.Unit.ClothingHatType <= setupOutput.AllowedClothingHatTypes.Count)
        {
            clothingResults.Add(setupOutput.AllowedClothingHatTypes[actorUnit.Unit.ClothingHatType - 1]
                .Configure(actorUnit, changeDict));
        }

        if (actorUnit.Unit.ClothingAccessoryType > 0 &&
            actorUnit.Unit.ClothingAccessoryType <= setupOutput.AllowedClothingAccessoryTypes.Count)
        {
            clothingResults.Add(setupOutput
                .AllowedClothingAccessoryTypes[actorUnit.Unit.ClothingAccessoryType - 1].Configure(actorUnit, changeDict));
        }
        else if (actorUnit.Unit.EarnedMask && actorUnit.Unit.ClothingAccessoryType > 0 && actorUnit.Unit.ClothingAccessoryType - 1 ==
                 setupOutput.AllowedClothingAccessoryTypes.Count)
        {
            IClothing asuraMask;

            if (Equals(actorUnit.Unit.Race, Race.Imp) || Equals(actorUnit.Unit.Race, Race.Goblin))
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


        AccumulatedClothes accumulatedClothes = new AccumulatedClothes();

        bool revealsBreasts = true;
        bool revealsDick = true;
        foreach (ClothingRenderOutput clothingOut in clothingResults)
        {
            if (!clothingOut.SkipCheck)
            {
                if ((!clothingOut.RevealsDick || clothingOut.InFrontOfDick) && Config.CockVoreHidesClothes &&
                    actorUnit.PredatorComponent?.BallsFullness > 0)
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

            accumulatedClothes.SpritesInfos.AddRange(clothingOut.ClothingSpriteChanges);
        }

        accumulatedClothes.RevealsBreasts = revealsBreasts;
        accumulatedClothes.RevealsDick = revealsDick;

        return accumulatedClothes;
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////


    private void RenderRace(RaceSpriteChangeDict changeDict, Vector2 wholeBodyOffset)
    {
        foreach (SpriteType spriteType in EnumUtil.GetValues<SpriteType>())
        {
            ISpriteContainer container = _sprites[spriteType];
            if (changeDict.ReusedChangesDict.TryGetValue(spriteType, out var raceRenderOutput))
            {
                container.NewSetSprite(raceRenderOutput, wholeBodyOffset, _actor.SpriteLayerOffset);
            }
            else
            {
                container.GameObject.SetActive(false);
            }
        }

        foreach (KeyValuePair<SpriteType, RaceRenderOutput> entry in changeDict.ReusedChangesDict)
        {
            SpriteType type = entry.Key;
            RaceRenderOutput changes = entry.Value;

            ISpriteContainer container = _sprites[type];

            if (container != null)
            {
                if (changes.GameObjectActive != null)
                {
                    container.GameObject.SetActive(changes.GameObjectActive.Value);
                }

                if (changes.GameObjectLocalScale != null)
                {
                    container.GameObject.transform.localScale = changes.GameObjectLocalScale.Value;
                }

                if (changes.SetParentData != null)
                {
                    Transform transform = _sprites[changes.SetParentData.Item1]?.GameObject?.transform.parent;
                    container.GameObject.transform.SetParent(transform, changes.SetParentData.Item2);
                }
            }
            //if (changes._offset != null) sprites[type].GameObject.transform.SetParent(changes._SetParentData.Item1, changes._SetParentData.Item2);
        }

        if (_actor.Unit.IsDead == false && _actor.UnitSprite != null && _actor.UnitSprite.transform.rotation != Quaternion.Euler(0, 0, 0))
        {
            _actor.UnitSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
            _actor.UnitSprite.LevelText.gameObject.SetActive(true);
            _actor.UnitSprite.FlexibleSquare.gameObject.SetActive(true);
            _actor.UnitSprite.HealthBar.gameObject.SetActive(true);
        }

        if (_sprites[SpriteType.BackWeapon] != null)
        {
            ISpriteContainer container = _sprites[SpriteType.BackWeapon];
            if (container.IsImage)
            {
                container.GameObject.transform.localPosition = new Vector3(-6, 50, 0);
                container.GameObject.transform.rotation = Quaternion.Euler(0, 0, -45);
            }
            else
            {
                container.GameObject.transform.localPosition = new Vector3(-.08f, .22f, 0);
                container.GameObject.transform.rotation = Quaternion.Euler(0, 0, -45);
            }
        }
    }


    private void SortSprites()
    {
        if (_sprites[0] != null && _sprites[0].IsImage) //Manual sort for Images
        {
            ISpriteContainer[] containers = AllContainers.Where(s => s != null).OrderBy(s => s.SortOrder).ToArray();
            for (int i = 0; i < containers.Length; i++)
            {
                containers[i].GameObject.transform.SetSiblingIndex(i + 1);
            }
        }
    }


    public void UpdateSprite()
    {
        SpriteCollection raceSpriteCollection = GameManager.CustomManager.GetRaceSpriteCollection(_actor.Unit.Race.Id);
        RaceSpriteChangeDict changeDict = new RaceSpriteChangeDict(raceSpriteCollection);

        IRaceData raceData = RaceFuncs.GetRace(_actor.Unit);
        RunOutput runOutput = CalculateRaceRender(_actor, raceData, changeDict);

        // MUST BE DONE BEFORE RenderRace
        // changeDict gets modified by ClothingResults
        AccumulatedClothes accumulatedClothes = CalculateClothingRender(_actor, changeDict, raceData.SetupOutputRaw);

        Vector3 clothingShift = runOutput.ClothingShift ?? raceData.SetupOutput.ClothingShift;
        Vector2 wholeBodyOffset = runOutput.WholeBodyOffset ?? raceData.SetupOutput.WholeBodyOffset;

        RenderRace(changeDict, wholeBodyOffset);

        RenderClothing(accumulatedClothes, wholeBodyOffset, clothingShift);

        SortSprites();
    }

    private void RenderClothing(AccumulatedClothes accumulatedClothes, Vector2 wholeBodyOffset, Vector3 clothingShift)
    {
        int clothesContainers = Math.Max(accumulatedClothes.SpritesInfos.Count, _clothingSprites.Count);
        for (int i = 0; i < clothesContainers; i++)
        {
            ISpriteContainer container = GetClothingContainer(i);
            RaceRenderOutput one = accumulatedClothes.SpritesInfos.GetOrNull(i);

            if (one != null)
            {
                Vector2 extraOffset = new Vector2(wholeBodyOffset.x + clothingShift.x, wholeBodyOffset.y + clothingShift.y);
                container.NewSetSprite(one, extraOffset, _actor.SpriteLayerOffset);
                container.Name = "Clothing_Sprite_" + i;
            }
            else
            {
                container.GameObject.SetActive(false);
            }
        }

        if (accumulatedClothes.BlocksBreasts)
        {
            HideSprite(SpriteType.Breasts);
            HideSprite(SpriteType.SecondaryBreasts);
            if (Equals(_actor.Unit.Race, Race.Succubus))
            {
                HideSprite(SpriteType.BreastShadow); //Used for other things in newgraphics
            }
        }
        else if (!accumulatedClothes.RevealsBreasts)
        {
            ChangeLayer(SpriteType.Breasts, 8);
            ChangeLayer(SpriteType.SecondaryBreasts, 8);
            if (Equals(_actor.Unit.Race, Race.Succubus))
            {
                ChangeLayer(SpriteType.Breasts, 7);
                ChangeLayer(SpriteType.BreastShadow, 8);
            }
        }

        if (!accumulatedClothes.RevealsDick)
        {
            HideSprite(SpriteType.Dick);
            HideSprite(SpriteType.Balls);
        }
    }


    internal void DarkenSprites()
    {
        float tint = 0.6f;
        foreach (ISpriteContainer container in AllContainers)
        {
            if (container != null)
            {
                container.Color = ColorMap.Darken(container.Color, tint);
            }
        }
    }

    internal void ApplyDeadEffect()
    {
        float tint = .4f;

        foreach (ISpriteContainer container in AllContainers)
        {
            if (container != null)
            {
                container.Color = container.Color = ColorMap.Bluify(container.Color, tint);
            }
        }
    }

    internal void RedifySprite(float tint)
    {
        foreach (ISpriteContainer container in AllContainers)
        {
            if (container != null)
            {
                container.Color = ColorMap.Redify(container.Color, tint);
            }
        }
    }
}
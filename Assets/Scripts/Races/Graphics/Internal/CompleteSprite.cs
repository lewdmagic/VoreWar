﻿#region

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion


internal class CompleteSprite
{
    
    public static readonly SpriteType AssumedFluffType = SpriteType.BodyAccent3;
    
    private readonly List<ISpriteContainer> _clothingSprites = new List<ISpriteContainer>();
    private readonly SpriteTypeIndexed<ISpriteContainer> _sprites = new SpriteTypeIndexed<ISpriteContainer>();
    private IEnumerable<ISpriteContainer> _allContainers => _sprites.Concat(_clothingSprites);
    
    private readonly Actor_Unit _actor;
    private readonly Transform _folder;
    private readonly GameObject _type;
    

    internal CompleteSprite(GameObject type, GameObject animatedType, Transform folder, Actor_Unit actor)
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
    
    private ISpriteContainer getClothingContainer(int index)
    {
        return _clothingSprites.GetOrAdd(index, () =>
        {
            return SpriteContainer.MakeContainer(_type, _folder);
        });
    }
    
    private IRaceData GetRace => Races.GetRace(_actor.Unit);

    internal void Destroy()
    {
        foreach (ISpriteContainer container in _allContainers)
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

    internal void HideSprite(SpriteType spriteType)
    {
        if (_sprites[spriteType] != null)
        {
            _sprites[spriteType].GameObject.SetActive(false);
        }
    }


    internal void ChangeLayer(SpriteType spriteType, int layer)
    {
        if (_sprites[spriteType] != null)
        {
            _sprites[spriteType].SortOrder = layer + _actor.spriteLayerOffset;
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

    public void UpdateSprite()
    {
        FullSpriteProcessOut fullOut = GetRace.NewUpdate(_actor);

        Vector3 clothingShift = fullOut.RunOutput.ClothingShift.HasValue ? fullOut.RunOutput.ClothingShift.Value : GetRace.MiscRaceData.ClothingShift;
        Vector2 wholebodyOffset = fullOut.RunOutput.WholeBodyOffset.HasValue ? fullOut.RunOutput.WholeBodyOffset.Value : GetRace.MiscRaceData.WholeBodyOffset;

        foreach (SpriteType spriteType in EnumUtil.GetValues<SpriteType>())
        {
            ISpriteContainer container = _sprites[spriteType];
            RaceRenderOutput raceRenderOutput;
            if (fullOut.spriteOutputs.TryGetValue(spriteType, out raceRenderOutput))
            {
                container.NewSetSprite(raceRenderOutput, wholebodyOffset, _actor.spriteLayerOffset);
            }
            else
            {
                container.GameObject.SetActive(false);
            }
        }
        
        foreach (KeyValuePair<SpriteType, RaceRenderOutput> entry in fullOut.spriteOutputs)
        {
            SpriteType type = entry.Key;
            RaceRenderOutput changes = entry.Value;

            ISpriteContainer container = _sprites[type];

            if (container != null)
            {
                if (changes._gameObjectActive != null)
                {
                    container.GameObject.SetActive(changes._gameObjectActive.Value);
                }

                if (changes._gameObjectLocalScale != null)
                {
                    container.GameObject.transform.localScale = changes._gameObjectLocalScale.Value;
                }

                if (changes._SetParentData != null)
                {
                    Transform transform = _sprites[changes._SetParentData.Item1]?.GameObject?.transform.parent;
                    container.GameObject.transform.SetParent(transform, changes._SetParentData.Item2);
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

        processAccumulated(fullOut.AccumulatedClothes, wholebodyOffset, clothingShift);

        if (_sprites[0] != null && _sprites[0].IsImage) //Manual sort for Images
        {
            ISpriteContainer[] containers = _sprites.Where(s => s != null).OrderBy(s => s.SortOrder).ToArray();
            for (int i = 0; i < containers.Length; i++)
            {
                containers[i].GameObject.transform.SetSiblingIndex(i + 1);
            }
        }
    }

    private void processAccumulated(AccumulatedClothes accumulatedClothes, Vector2 wholebodyOffset, Vector3 clothingShift)
    {
        int clothesContainers = Math.Max(accumulatedClothes.spritesInfos.Count, _clothingSprites.Count);
        for (int i = 0; i < clothesContainers; i++)
        {
            ISpriteContainer container = getClothingContainer(i);
            ISpriteChangeReadable one = accumulatedClothes.spritesInfos.GetOrNull(i);

            if (one != null)
            {
                Vector2 extraOffset = new Vector2(wholebodyOffset.x + clothingShift.x, wholebodyOffset.y + clothingShift.y);
                container.NewSetSprite(one, extraOffset, _actor.spriteLayerOffset);
                container.Name = "Clothing_Sprite_" + i;
            }
            else
            {
                container.GameObject.SetActive(false);
            }
        }

        if (accumulatedClothes.blocksBreasts)
        {
            HideSprite(SpriteType.Breasts);
            HideSprite(SpriteType.SecondaryBreasts);
            if (_actor.Unit.Race == Race.Succubi)
            {
                HideSprite(SpriteType.BreastShadow); //Used for other things in newgraphics
            }
        }
        else if (!accumulatedClothes.revealsBreasts)
        {
            ChangeLayer(SpriteType.Breasts, 8);
            ChangeLayer(SpriteType.SecondaryBreasts, 8);
            if (_actor.Unit.Race == Race.Succubi)
            {
                ChangeLayer(SpriteType.Breasts, 7);
                ChangeLayer(SpriteType.BreastShadow, 8);
            }
        }

        if (!accumulatedClothes.revealsDick)
        {
            HideSprite(SpriteType.Dick);
            HideSprite(SpriteType.Balls);
        }
    }


    internal void DarkenSprites()
    {
        float tint = 0.6f;
        foreach (ISpriteContainer container in _allContainers)
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

        foreach (ISpriteContainer container in _allContainers)
        {
            if (container != null)
            {
                container.Color = container.Color = ColorMap.Bluify(container.Color, tint);
            }
        }
    }

    internal void RedifySprite(float tint)
    {
        foreach (ISpriteContainer container in _allContainers)
        {
            if (container != null)
            {
                container.Color = ColorMap.Redify(container.Color, tint);
            }
        }
    }
}
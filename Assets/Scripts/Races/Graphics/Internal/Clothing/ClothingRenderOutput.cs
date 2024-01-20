using System;
using System.Collections.Generic;
using UnityEngine;

public class ClothingRenderOutput : IClothingRenderOutput
{
    private readonly SpriteCollection _spriteCollection;

    private readonly SpriteChangeDict _changeDict;
    public IRaceRenderOutput ChangeRaceSprite(SpriteType spriteType) => _changeDict.ChangeSprite(spriteType);
    
    internal readonly Dictionary<string, RaceRenderOutput> ClothingSpriteChanges = new Dictionary<string, RaceRenderOutput>();
    public ClothingRenderOutput(SpriteChangeDict changeDict, ClothingMiscData miscData, SpriteCollection spriteCollection)
    {
        _spriteCollection = spriteCollection; 
        _changeDict = changeDict;
        RevealsBreasts = miscData.RevealsBreasts;
        BlocksBreasts = miscData.BlocksBreasts;
        RevealsDick = miscData.RevealsDick;
        InFrontOfDick = miscData.InFrontOfDick;
    }
        
    public IRaceRenderOutput this[string key]
    {
        get
        {
            if (!ClothingSpriteChanges.TryGetValue(key, out var clothing))
            {
                clothing = new RaceRenderOutput(_spriteCollection);
                ClothingSpriteChanges.Add(key, clothing);
            }

            return clothing;
        }
    }
        
    public IRaceRenderOutput NewSprite(string name, int layer)
    {
        if (ClothingSpriteChanges.TryGetValue(name, out var clothing))
        {
            throw new Exception($"Sprite with {name} already exists");
        }
        else
        {
            clothing = new RaceRenderOutput(_spriteCollection);
            clothing.Layer(layer);
            ClothingSpriteChanges.Add(name, clothing);
            return clothing;
        }
    }

    public bool RevealsBreasts { get; set; }
    public bool BlocksBreasts { get; set; }
    public bool RevealsDick { get; set; }
    public bool InFrontOfDick { get; set; }
    public bool SkipCheck { get; set; }
}
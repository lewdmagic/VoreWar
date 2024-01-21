using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClothingRenderOutput : IClothingRenderOutput
{
    private readonly SpriteCollection _spriteCollection;

    private readonly SpriteChangeDict _changeDict;
    public IRaceRenderOutput ChangeRaceSprite(SpriteType spriteType) => _changeDict.ChangeSprite(spriteType);
    
    private readonly Dictionary<string, RaceRenderOutput> _namedClothingSpriteChanges = new Dictionary<string, RaceRenderOutput>();
    private readonly List<RaceRenderOutput> _clothingSpriteChanges = new List<RaceRenderOutput>();

    internal IEnumerable<RaceRenderOutput> ClothingSpriteChanges => _clothingSpriteChanges.Concat(_namedClothingSpriteChanges.Values);
    
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
            if (!_namedClothingSpriteChanges.TryGetValue(key, out var clothing))
            {
                clothing = new RaceRenderOutput(_spriteCollection);
                _namedClothingSpriteChanges.Add(key, clothing);
            }

            return clothing;
        }
    }
        
    public IRaceRenderOutput NewSprite(string name, int layer)
    {
        if (_namedClothingSpriteChanges.TryGetValue(name, out var clothing))
        {
            throw new Exception($"Sprite with {name} already exists");
        }
        else
        {
            clothing = new RaceRenderOutput(_spriteCollection);
            clothing.Layer(layer);
            _namedClothingSpriteChanges.Add(name, clothing);
            return clothing;
        }
    }
        
    public IRaceRenderOutput NewSprite(int layer)
    {
        var clothing = new RaceRenderOutput(_spriteCollection);
        clothing.Layer(layer);
        _clothingSpriteChanges.Add(clothing);
        return clothing;
    }

    public bool RevealsBreasts { get; set; }
    public bool BlocksBreasts { get; set; }
    public bool RevealsDick { get; set; }
    public bool InFrontOfDick { get; set; }
    public bool SkipCheck { get; set; }

    public void DisableBreasts()
    {
        BlocksBreasts = true;
    }

    public void DisableDick()
    {
        RevealsDick = false;
    }
}
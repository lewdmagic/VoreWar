using System.Collections.Generic;
using UnityEngine;

public class ClothingRenderOutput : IClothingRenderOutput
{

    
    private readonly SpriteChangeDict _changeDict;
    public IRaceRenderOutput ChangeSprite(SpriteType spriteType) => _changeDict.ChangeSprite(spriteType);
    
    internal readonly Dictionary<string, RaceRenderOutput> ClothingSpriteChanges = new Dictionary<string, RaceRenderOutput>();
    public ClothingRenderOutput(SpriteChangeDict changeDict, ClothingMiscData miscData)
    {
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
                clothing = new RaceRenderOutput();
                ClothingSpriteChanges.Add(key, clothing);
            }

            return clothing;
        }
    }

    public bool RevealsBreasts { get; set; }
    public bool BlocksBreasts { get; set; }
    public bool RevealsDick { get; set; }
    public bool InFrontOfDick { get; set; }
    public bool SkipCheck { get; set; }
}
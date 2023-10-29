using System.Collections.Generic;
using UnityEngine;

internal class ClothingRenderOutput : IClothingRenderOutput
{

    
    private SpriteChangeDict _changeDict;
    public IRaceRenderOutput changeSprite(SpriteType spriteType) => _changeDict.changeSprite(spriteType);
    
    internal Dictionary<string, RaceRenderOutput> ClothingSpriteChanges = new Dictionary<string, RaceRenderOutput>();
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
            RaceRenderOutput clothing;
            if (!ClothingSpriteChanges.TryGetValue(key, out clothing))
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
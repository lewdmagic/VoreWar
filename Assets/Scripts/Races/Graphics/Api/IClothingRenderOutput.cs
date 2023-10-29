#region

using UnityEngine;

#endregion

internal interface IClothingRenderOutput
{
    IRaceRenderOutput this[string key] { get; }
    IRaceRenderOutput changeSprite(SpriteType spriteType);

    bool RevealsBreasts { set; }
    bool BlocksBreasts { set; }
    bool RevealsDick { set; }
    bool SkipCheck { set; }
    bool InFrontOfDick { set; }
}
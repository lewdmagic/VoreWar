#region

using UnityEngine;

#endregion

public interface IClothingRenderOutput
{
    IRaceRenderOutput this[string key] { get; }
    IRaceRenderOutput NewSprite(string name, int layer);
    IRaceRenderOutput NewSprite(int layer);
    
    IRaceRenderOutput ChangeRaceSprite(SpriteType spriteType);

    bool RevealsBreasts { set; }
    bool BlocksBreasts { set; }
    bool RevealsDick { set; }
    bool SkipCheck { set; }
    bool InFrontOfDick { set; }
}
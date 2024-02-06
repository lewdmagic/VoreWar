using UnityEngine;

public interface IClothingSetupOutput
{
    
    /// <summary>Only wearable by units with the leader type</summary>
    bool LeaderOnly { get; set; }

    /// <summary>Only wearable by females / herms</summary>
    bool FemaleOnly { get; set; }

    /// <summary>Only wearable by males</summary>
    bool MaleOnly { get; set; }

    bool ReqWinterHoliday { get; set; }

    Sprite DiscardSprite { get; set; }

    /// <summary>
    /// A unique type number, only used in relation to discarded sprites
    /// </summary>
    ClothingId ClothingId { get; set; }

    /// <summary>Discarded sprite uses palettes instead of the solid color</summary>
    bool DiscardUsesPalettes { get; set; }
    
    /// <summary>Prevents Clothing2 (Waist Clothing) from being used at the same time</summary>
    bool OccupiesAllSlots { get; set; }

    /// <summary>Whether discards will use clothing 1 color instead of clothing 2 color</summary>
    bool DiscardUsesColor2 { get; set; }

    /// <summary>Whether the clothing is considered to be always the default color, also affects the discard</summary>
    bool FixedColor { get; set; }
    
    /// <summary>If false lowers breast layer to 8 so that it will be under clothing</summary>
    bool RevealsBreasts { set; }

    bool RevealsDick { set; }

    /// <summary>Turns off the breast sprites entirely</summary>
    bool BlocksBreasts { set; }
    
    /// <summary>Doesn't turn off the dick, but is in front of it</summary>
    bool InFrontOfDick { set; }
}
#region

using UnityEngine;

#endregion


internal interface IClothingDataFixed
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
    int Type { get; set; }

    /// <summary>Discarded sprite uses palettes instead of the solid color</summary>
    bool DiscardUsesPalettes { get; set; }
    
    /// <summary>Prevents Clothing2 (Waist Clothing) from being used at the same time</summary>
    bool OccupiesAllSlots { get; set; }

    /// <summary>Whether discards will use clothing 1 color instead of clothing 2 color</summary>
    bool DiscardUsesColor2 { get; set; }

    /// <summary>Whether the clothing is considered to be always the default color, also affects the discard</summary>
    bool FixedColor { get; set; }
}


internal interface IClothingSetupOutput : IClothingDataFixed
{
    /// <summary>If false lowers breast layer to 8 so that it will be under clothing</summary>
    bool RevealsBreasts { set; }

    bool RevealsDick { set; }

    /// <summary>Turns off the breast sprites entirely</summary>
    bool BlocksBreasts { set; }
    
    /// <summary>Doesn't turn off the dick, but is in front of it</summary>
    bool InFrontOfDick { set; }
}


internal class ClothingMiscData : IClothingSetupOutput
{
    /// <summary>Only wearable by units with the leader type</summary>
    public bool LeaderOnly { get; set; } = false;

    /// <summary>Only wearable by females / herms</summary>
    public bool FemaleOnly { get; set; } = false;

    /// <summary>Only wearable by males</summary>
    public bool MaleOnly { get; set; } = false;

    public bool ReqWinterHoliday { get; set; } = false;

    /// <summary>Prevents Clothing2 (Waist Clothing) from being used at the same time</summary>
    public bool OccupiesAllSlots { get; set; } = false;

    /// <summary>Discarded sprite uses palettes instead of the solid color</summary>
    public bool DiscardUsesPalettes { get; set; } = false;

    /// <summary>If true lowers breast layer to 8 so that it will be under clothing</summary>
    public bool RevealsBreasts { get; set; } = false;

    public bool RevealsDick { get; set; } = false;

    /// <summary>Turns off the breast sprites entirely</summary>
    public bool BlocksBreasts { get; set; } = false;


    /// <summary>Doesn't turn off the dick, but is in front of it</summary>
    public bool InFrontOfDick { get; set; } = false;

    /// <summary>Whether the clothing is considered to be always the default color, also affects the discard</summary>
    public bool FixedColor { get; set; } = false;

    public Sprite DiscardSprite { get; set; } = null;

    /// <summary>
    /// A unique type number, only used in relation to discarded sprites
    /// </summary>
    public int Type { get; set; } = 0;
    
    public bool DiscardUsesColor2 { get; set; }

    internal ClothingMiscData ShallowCopy()
    {
        return (ClothingMiscData)MemberwiseClone();
    }
}
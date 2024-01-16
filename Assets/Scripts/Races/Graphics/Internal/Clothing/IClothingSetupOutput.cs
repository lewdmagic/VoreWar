public interface IClothingSetupOutput : IClothingDataFixed
{
    /// <summary>If false lowers breast layer to 8 so that it will be under clothing</summary>
    bool RevealsBreasts { set; }

    bool RevealsDick { set; }

    /// <summary>Turns off the breast sprites entirely</summary>
    bool BlocksBreasts { set; }
    
    /// <summary>Doesn't turn off the dick, but is in front of it</summary>
    bool InFrontOfDick { set; }
}
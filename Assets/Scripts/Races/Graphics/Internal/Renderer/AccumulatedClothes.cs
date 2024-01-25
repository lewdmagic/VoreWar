using System.Collections.Generic;

internal partial class RaceRenderer
{
    private class AccumulatedClothes
    {

        /// <summary>Turns off the breast sprites entirely</summary>
        internal bool BlocksBreasts;

        // TODO possibly missing implementation? 
        /// <summary>Doesn't turn off the dick, but is in front of it</summary>
        internal bool InFrontOfDick;

        /// <summary>if true lowers breast layer to 8 so that it will be under clothing</summary>
        internal bool RevealsBreasts;

        /// <summary>Turns off the dick sprites entirely</summary>
        internal bool RevealsDick;

        internal readonly List<RaceRenderOutput> SpritesInfos = new List<RaceRenderOutput>();
    }
}
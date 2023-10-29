#region

using System.Collections.Generic;
using UnityEngine;

#endregion

internal class AccumulatedClothes
{

    /// <summary>Turns off the breast sprites entirely</summary>
    internal bool blocksBreasts;

    /// <summary>Doesn't turn off the dick, but is in front of it</summary>
    internal bool inFrontOfDick;

    /// <summary>if true lowers breast layer to 8 so that it will be under clothing</summary>
    internal bool revealsBreasts;

    /// <summary>Turns off the dick sprites entirely</summary>
    internal bool revealsDick;

    internal List<ISpriteChangeReadable> spritesInfos = new List<ISpriteChangeReadable>();
}
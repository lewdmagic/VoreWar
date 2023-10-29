#region

using System.Collections.Generic;
using UnityEngine;

#endregion


internal abstract class RunOutputShared : IRunOutput, IRunOutputReadable
{
    // Interal fields
    public Vector3? ClothingShift { get; set; }
    public bool? ActorFurry { get; set; }

    public Vector2? WholeBodyOffset { get; set; }

    private SpriteChangeDict _changeDict;
    
    //
    protected RunOutputShared(SpriteChangeDict schangeDict)
    {
        _changeDict = schangeDict;
    }

    public IRaceRenderOutput changeSprite(SpriteType spriteType) => _changeDict.changeSprite(spriteType);
}
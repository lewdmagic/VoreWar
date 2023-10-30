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

    private readonly SpriteChangeDict _changeDict;
    
    //
    protected RunOutputShared(SpriteChangeDict changeDict)
    {
        _changeDict = changeDict;
    }

    public IRaceRenderOutput ChangeSprite(SpriteType spriteType) => _changeDict.ChangeSprite(spriteType);
}
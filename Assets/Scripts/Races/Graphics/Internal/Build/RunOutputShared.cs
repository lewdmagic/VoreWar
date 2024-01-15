#region

using System.Collections.Generic;
using UnityEngine;

#endregion


internal class RunOutput : IRunOutput, IRunOutputReadable
{
    // Interal fields
    public Vector3? ClothingShift { get; set; }
    public bool? ActorFurry { get; set; }

    public Vector2? WholeBodyOffset { get; set; }

    private readonly SpriteChangeDict _changeDict;
    
    //
    internal RunOutput(SpriteChangeDict changeDict)
    {
        _changeDict = changeDict;
    }

    public IRaceRenderOutput ChangeSprite(SpriteType spriteType) => _changeDict.ChangeSprite(spriteType);
}
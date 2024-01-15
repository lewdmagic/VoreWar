#region

using UnityEngine;

#endregion

public interface IRunOutput : ISpriteChanger
{
    Vector3? ClothingShift { set; }
    bool? ActorFurry { set; }
    Vector2? WholeBodyOffset { set; }
}
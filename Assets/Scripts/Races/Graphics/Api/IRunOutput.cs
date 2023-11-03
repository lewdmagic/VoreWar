#region

using UnityEngine;

#endregion

public interface IRunOutput : ISpriteChanger
{
    Vector3? ClothingShift { set; }
    bool? ActorFurry { set; }
    Vector2? WholeBodyOffset { set; }
}

public interface IRunOutput<out T> : IRunOutput where T : IParameters
{
    T Params { get; }
}
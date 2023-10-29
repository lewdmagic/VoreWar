#region

using UnityEngine;

#endregion

internal interface IRunOutput : ISpriteChanger
{
    Vector3? ClothingShift { set; }
    bool? ActorFurry { set; }
    Vector2? WholeBodyOffset { set; }
}

internal interface IRunOutput<out T> : IRunOutput where T : IParameters
{
    T Params { get; }
}
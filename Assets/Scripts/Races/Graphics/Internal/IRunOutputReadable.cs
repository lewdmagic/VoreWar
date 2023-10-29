#region

using System.Collections.Generic;
using UnityEngine;

#endregion

internal interface IRunOutputReadable
{
    Vector3? ClothingShift { get; }
    bool? ActorFurry { get; }
    Vector2? WholeBodyOffset { get; }
}
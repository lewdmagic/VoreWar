#region

using UnityEngine;

#endregion

internal interface IClothingDataSimple
{
    IClothingDataFixed FixedData { get; }
    bool CanWear(Unit unit);
}

internal interface IClothing<in T> : IClothingDataSimple where T : IParameters
{
    ClothingRenderOutput Configure(Actor_Unit actor, T state, SpriteChangeDict changeDict);
}


internal interface IClothing : IClothing<IParameters>
{
    new ClothingRenderOutput Configure(Actor_Unit actor, IParameters state, SpriteChangeDict changeDict);
}
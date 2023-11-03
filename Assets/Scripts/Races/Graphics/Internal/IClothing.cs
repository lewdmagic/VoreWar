#region

using UnityEngine;

#endregion

public interface IClothingDataSimple
{
    IClothingDataFixed FixedData { get; }
    bool CanWear(Unit unit);
}

public interface IClothing<in T> : IClothingDataSimple where T : IParameters
{
    ClothingRenderOutput Configure(Actor_Unit actor, T state, SpriteChangeDict changeDict);
}


public interface IClothing : IClothing<IParameters>
{
    new ClothingRenderOutput Configure(Actor_Unit actor, IParameters state, SpriteChangeDict changeDict);
}
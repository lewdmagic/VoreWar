#region

using UnityEngine;

#endregion

public interface IClothingDataSimple
{
    IClothingDataFixed FixedData { get; }
    bool CanWear(Unit unit);
}

public interface IClothing : IClothingDataSimple
{
    ClothingRenderOutput Configure(Actor_Unit actor, RaceSpriteChangeDict changeDict);
}
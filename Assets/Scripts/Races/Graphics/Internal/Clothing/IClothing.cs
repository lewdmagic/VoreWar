#region

using UnityEngine;

#endregion
 
public interface IClothing
{
    IClothingDataFixed FixedData { get; }
    bool CanWear(Unit unit);
    ClothingRenderOutput Configure(Actor_Unit actor, ISpriteChanger changeDict);
}
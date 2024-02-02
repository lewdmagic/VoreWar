#region

using UnityEngine;

#endregion
 
public interface IClothing
{
    IClothingDataFixed FixedData { get; }
    bool CanWear(IUnitRead unit);
    ClothingRenderOutput Configure(Actor_Unit actor, ISpriteChanger changeDict);
}
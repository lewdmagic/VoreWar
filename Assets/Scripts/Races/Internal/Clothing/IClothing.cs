#region

using UnityEngine;

#endregion

public interface IClothing
{
    IClothingSetupOutput FixedData { get; }
    bool CanWear(IUnitRead unit);
    ClothingRenderOutput Configure(ActorUnit actor, ISpriteChanger changeDict);
}
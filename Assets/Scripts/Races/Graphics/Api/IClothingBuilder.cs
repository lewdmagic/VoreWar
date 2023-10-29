using System;

internal interface IClothingBuilder
{
    void Setup(ClothingMiscData template, Action<IClothingSetupInput, IClothingSetupOutput> setMisc);
    void RenderAll(Action<IClothingRenderInput, IClothingRenderOutput> completeGen);
}

internal interface IClothingBuilder<T> where T : IParameters
{
    void Setup(ClothingMiscData template, Action<IClothingSetupInput, IClothingSetupOutput> setMisc);
    void RenderAll(Action<IClothingRenderInput<T>, IClothingRenderOutput> completeGen);
}
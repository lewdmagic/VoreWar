using System;

public interface IClothingBuilder
{
    void Setup(ClothingMiscData template);
    void Setup(ClothingMiscData template, Action<IClothingSetupInput, IClothingSetupOutput> setMisc);
    void RenderAll(Action<IClothingRenderInput, IClothingRenderOutput> completeGen);
}

public interface IClothingBuilder<out T> where T : IParameters
{
    void Setup(ClothingMiscData template);
    void Setup(ClothingMiscData template, Action<IClothingSetupInput, IClothingSetupOutput> setMisc);
    void RenderAll(Action<IClothingRenderInput, IClothingRenderOutput, T> completeGen);
}
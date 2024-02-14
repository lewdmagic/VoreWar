using System;

internal class BindableClothing<T> where T : IParameters
{
    private readonly Action<IClothingBuilder<T>> _builderUser;

    internal BindableClothing(Action<IClothingBuilder<T>> builderUser)
    {
        _builderUser = builderUser;
    }

    internal IClothing Create(Func<IClothingRenderInput, T> paramsCalc)
    {
        ClothingBuilderV2<T> builder = new ClothingBuilderV2<T>(paramsCalc);
        _builderUser.Invoke(builder);
        return builder.BuildClothing();
    }
}
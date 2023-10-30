internal interface IClothingRenderInput
{
    Actor_Unit Actor { get; }
    SpriteDictionary Sprites { get; }
}

internal interface IClothingRenderInput<out T> : IClothingRenderInput where T : IParameters
{
    T Params { get; }
}
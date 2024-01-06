public interface IClothingRenderInput : IRunInput
{

}

public interface IClothingRenderInput<out T> : IClothingRenderInput where T : IParameters
{
    T Params { get; }
}
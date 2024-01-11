public interface IClothingRenderInput : IRunInput
{

}

public interface IClothingRenderInput<out T> : IClothingRenderInput where T : IParameters
{
    T Params { get; }
}

public class ClothingRenderInputImpl<TU> : ClothingRenderInputImpl, IClothingRenderInput<TU> where TU : IParameters
{
    public ClothingRenderInputImpl(Actor_Unit actor, TU state) : base(actor)
    {
        Params = state;
    }

    public TU Params { get; private set; }
}
public interface IClothingRenderInput : IRunInput
{
    
}

public class ClothingRenderInput : RenderInput, IClothingRenderInput
{
    public ClothingRenderInput(Actor_Unit actor) : base(actor)
    {
        
    }
}
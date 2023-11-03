using System;

internal class Clothing : ClothingDataShared, IClothing
{
    private readonly Action<IClothingRenderInput, IClothingRenderOutput> _completeGen;

    public Clothing(ClothingMiscData fixedData, Action<IClothingRenderInput, IClothingRenderOutput> completeGen) : base(fixedData)
    {
        _completeGen = completeGen;
    }

    public ClothingRenderOutput Configure(Actor_Unit actor, IParameters parameters, SpriteChangeDict changeDict)
    {
        IClothingRenderInput input = new ClothingRenderInputImpl(actor);
        ClothingRenderOutput renderOutput = new ClothingRenderOutput(changeDict, Misc);
        _completeGen.Invoke(input, renderOutput);
        return renderOutput;
    }
}

public class Clothing<TS> : ClothingDataShared, IClothing<TS> where TS : IParameters
{
    private readonly Action<IClothingRenderInput<TS>, IClothingRenderOutput> _completeGen;

    public Clothing(ClothingMiscData misc, Action<IClothingRenderInput<TS>, IClothingRenderOutput> completeGen) : base(misc)
    {
        _completeGen = completeGen;
    }

    public ClothingRenderOutput Configure(Actor_Unit actor, TS state, SpriteChangeDict changeDict)
    {
        IClothingRenderInput<TS> input = new ClothingRenderInputImpl<TS>(actor, state);
        ClothingRenderOutput renderOutput = new ClothingRenderOutput(changeDict, Misc);

        _completeGen.Invoke(input, renderOutput);

        return renderOutput;
    }

    private class ClothingRenderInputImpl<TU> : ClothingRenderInputImpl, IClothingRenderInput<TU> where TU : IParameters
    {
        public ClothingRenderInputImpl(Actor_Unit actor, TU state) : base(actor)
        {
            Params = state;
        }

        public TU Params { get; private set; }
    }
}
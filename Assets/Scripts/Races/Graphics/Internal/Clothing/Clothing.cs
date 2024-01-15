using System;

internal class Clothing : ClothingDataShared, IClothing
{
    private readonly Action<IClothingRenderInput, IClothingRenderOutput> _completeGen;

    public Clothing(ClothingMiscData fixedData, Action<IClothingRenderInput, IClothingRenderOutput> completeGen) : base(fixedData)
    {
        _completeGen = completeGen;
    }

    public ClothingRenderOutput Configure(Actor_Unit actor, SpriteChangeDict changeDict)
    {
        IClothingRenderInput input = new ClothingRenderInputImpl(actor);
        ClothingRenderOutput renderOutput = new ClothingRenderOutput(changeDict, Misc);
        _completeGen.Invoke(input, renderOutput);
        return renderOutput;
    }
}

internal class Clothing<T> : ClothingDataShared, IClothing where T : IParameters
{
    private readonly Action<IClothingRenderInput<T>, IClothingRenderOutput> _completeGen;
    private readonly Func<IClothingRenderInput, T> _calcParams;

    public Clothing(ClothingMiscData fixedData, Action<IClothingRenderInput<T>, IClothingRenderOutput> completeGen, Func<IClothingRenderInput, T> calcParams) : base(fixedData)
    {
        _completeGen = completeGen;
        _calcParams = calcParams;
    }

    public ClothingRenderOutput Configure(Actor_Unit actor, SpriteChangeDict changeDict)
    {
        IClothingRenderInput inputBasic = new ClothingRenderInputImpl(actor);
        T calcdParameters = _calcParams.Invoke(inputBasic);
        ClothingRenderInputImpl<T> input = new ClothingRenderInputImpl<T>(actor, calcdParameters);
        ClothingRenderOutput renderOutput = new ClothingRenderOutput(changeDict, Misc);
        _completeGen.Invoke(input, renderOutput);
        return renderOutput;
    }
}
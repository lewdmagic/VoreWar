using System;

internal class ClothingV2<T> : ClothingDataShared, IClothing where T : IParameters
{
    private readonly Action<IClothingRenderInput<T>, IClothingRenderOutput> _completeGen;
    private readonly Func<IClothingRenderInput, T> _calcParams;

    public ClothingV2(ClothingMiscData fixedData, Action<IClothingRenderInput<T>, IClothingRenderOutput> completeGen, Func<IClothingRenderInput, T> calcParams) : base(fixedData)
    {
        _completeGen = completeGen;
        _calcParams = calcParams;
    }

    public ClothingRenderOutput Configure(Actor_Unit actor, IParameters parameters, SpriteChangeDict changeDict)
    {
        IClothingRenderInput inputBasic = new ClothingRenderInputImpl(actor);
        T calcdParameters = _calcParams.Invoke(inputBasic);
        ClothingRenderInputImpl<T> input = new ClothingRenderInputImpl<T>(actor, calcdParameters);
        ClothingRenderOutput renderOutput = new ClothingRenderOutput(changeDict, Misc);
        _completeGen.Invoke(input, renderOutput);
        return renderOutput;
    }
}


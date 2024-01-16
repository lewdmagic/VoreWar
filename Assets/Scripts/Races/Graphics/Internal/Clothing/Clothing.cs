using System;
using MoonSharp.Interpreter;

public abstract class ClothingDataShared : IClothingDataSimple
{
    private protected readonly ClothingMiscData Misc;
    public IClothingDataFixed FixedData { get; set; }

    protected ClothingDataShared(ClothingMiscData fixedData)
    {
        Misc = fixedData;
        FixedData = fixedData;
    }

    public bool CanWear(Unit unit)
    {
        if (FixedData.MaleOnly && (unit.HasBreasts || unit.HasDick == false))
        {
            return false;
        }

        if (FixedData.FemaleOnly && unit.HasDick && unit.HasBreasts == false)
        {
            return false;
        }

        if (FixedData.LeaderOnly && unit.Type != UnitType.Leader)
        {
            return false;
        }

        if (FixedData.ReqWinterHoliday && Config.WinterActive() == false)
        {
            return false;
        }

        return true;
    }
}

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
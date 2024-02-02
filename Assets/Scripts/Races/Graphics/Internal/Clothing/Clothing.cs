using System;
using MoonSharp.Interpreter;

public abstract class ClothingDataShared : IClothing
{
    private protected readonly ClothingMiscData Misc;
    public IClothingDataFixed FixedData { get; set; }

    protected ClothingDataShared(ClothingMiscData fixedData)
    {
        Misc = fixedData;
        FixedData = fixedData;
    }

    public bool CanWear(IUnitRead unit)
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

    public abstract ClothingRenderOutput Configure(Actor_Unit actor, ISpriteChanger changeDict);
}

internal class Clothing : ClothingDataShared, IClothing
{
    private readonly Action<IClothingRenderInput, IClothingRenderOutput> _completeGen;

    public Clothing(ClothingMiscData fixedData, Action<IClothingRenderInput, IClothingRenderOutput> completeGen) : base(fixedData)
    {
        _completeGen = completeGen;
    }

    public override ClothingRenderOutput Configure(Actor_Unit actor, ISpriteChanger changeDict)
    {
        IClothingRenderInput input = new ClothingRenderInput(actor);
        ClothingRenderOutput renderOutput = new ClothingRenderOutput(changeDict, Misc, null);
        _completeGen.Invoke(input, renderOutput);
        return renderOutput;
    }
}

internal class Clothing<T> : ClothingDataShared, IClothing where T : IParameters
{
    private readonly Action<IClothingRenderInput, IClothingRenderOutput, T> _completeGen;
    private readonly Func<IClothingRenderInput, T> _calcParams;

    public Clothing(ClothingMiscData fixedData, Action<IClothingRenderInput, IClothingRenderOutput, T> completeGen, Func<IClothingRenderInput, T> calcParams) : base(fixedData)
    {
        _completeGen = completeGen;
        _calcParams = calcParams;
    }

    public override ClothingRenderOutput Configure(Actor_Unit actor, ISpriteChanger changeDict)
    {
        ClothingRenderInput input = new ClothingRenderInput(actor);
        ClothingRenderOutput renderOutput = new ClothingRenderOutput(changeDict, Misc, null);
        T calcdParameters = _calcParams.Invoke(input);
        _completeGen.Invoke(input, renderOutput, calcdParameters);
        return renderOutput;
    }
}
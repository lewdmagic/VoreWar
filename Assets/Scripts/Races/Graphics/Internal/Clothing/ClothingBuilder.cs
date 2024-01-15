#region

using System;

#endregion

public abstract class ClothingBuilderShared
{
    private protected ClothingMiscData Misc;

    public void Setup(ClothingMiscData template, Action<IClothingSetupInput, IClothingSetupOutput> setMisc)
    {
        IClothingSetupInput input = new ClothingSetupInput();
        ClothingMiscData copy = template.ShallowCopy();
        setMisc?.Invoke(input, copy);
        Misc = copy;
    }
    
    public void Setup(ClothingMiscData template)
    {
        Setup(template, null);
    }
}

public class ClothingRenderInputImpl : RenderInput, IClothingRenderInput
{
    public ClothingRenderInputImpl(Actor_Unit actor) : base(actor)
    {
            
    }
}

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

internal class ClothingSetupInput : IClothingSetupInput
{
    public SpriteDictionary Sprites => State.GameManager.SpriteDictionary;
}


public class ClothingBuilder : ClothingBuilderShared, IClothingBuilder
{
    internal static readonly ClothingMiscData DefaultMisc = new ClothingMiscData();
    private Action<IClothingRenderInput, IClothingRenderOutput> _completeGen;
    
    internal static ClothingBuilder New()
    {
        return new ClothingBuilder();
    }
    
    public void RenderAll(Action<IClothingRenderInput, IClothingRenderOutput> completeGen)
    {
        _completeGen = completeGen;
    }

    [Obsolete("Old way of building.")]
    internal IClothing BuildClothing()
    {
        return new Clothing(Misc, _completeGen);
    }

    internal static IClothing Create(Action<IClothingBuilder> builderUser)
    {
        ClothingBuilder builder = new ClothingBuilder();
        builderUser.Invoke(builder);
        return builder.BuildClothing();
    }

    internal static BindableClothing<T> CreateV2<T>(Action<IClothingBuilder<T>> builderUser) where T : IParameters
    {
        return new BindableClothing<T>(builderUser);
    }
    
    
}

internal class ClothingBuilderV2<T> : ClothingBuilderShared, IClothingBuilder<T> where T : IParameters
{
    private Action<IClothingRenderInput<T>, IClothingRenderOutput> _completeGen;
    private readonly Func<IClothingRenderInput, T> _paramsCalc;
    
    public ClothingBuilderV2(Func<IClothingRenderInput, T> paramsCalc)
    {
        _paramsCalc = paramsCalc;
    }

    public void RenderAll(Action<IClothingRenderInput<T>, IClothingRenderOutput> completeGen)
    {
        _completeGen = completeGen;
    }
    
    internal IClothing BuildClothing()
    {
        return new Clothing<T>(Misc, _completeGen, _paramsCalc);
    }
}
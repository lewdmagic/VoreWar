#region

using System;

#endregion

internal abstract class ClothingBuilderShared
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

internal class ClothingRenderInputImpl : RenderInput, IClothingRenderInput
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


internal class ClothingBuilder : ClothingBuilderShared, IClothingBuilder
{
    internal static readonly ClothingMiscData DefaultMisc = new ClothingMiscData();
    private Action<IClothingRenderInput, IClothingRenderOutput> _completeGen;

    
    internal static ClothingBuilder New()
    {
        return new ClothingBuilder();
    }

    internal static ClothingBuilder<T> New<T>() where T : IParameters
    {
        return new ClothingBuilder<T>();
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

    internal static IClothing<T> Create<T>(Action<IClothingBuilder<T>> builderUser) where T : IParameters
    {
        ClothingBuilder<T> builder = new ClothingBuilder<T>();
        builderUser.Invoke(builder);
        return builder.BuildClothing();
    }
}

internal class ClothingBuilder<T> : ClothingBuilderShared, IClothingBuilder<T> where T : IParameters
{
    private Action<IClothingRenderInput<T>, IClothingRenderOutput> _completeGen;

    public void RenderAll(Action<IClothingRenderInput<T>, IClothingRenderOutput> completeGen)
    {
        _completeGen = completeGen;
    }

    [Obsolete("Old way of building.")]
    internal IClothing<T> BuildClothing()
    {
        return new Clothing<T>(Misc, _completeGen);
    }
}
#region

using System;

#endregion

public abstract class ClothingBuilderShared
{
    private protected ClothingMiscData Template;
    private protected Action<IClothingSetupInput, IClothingSetupOutput> SetMisc;

    public void Setup(ClothingMiscData template, Action<IClothingSetupInput, IClothingSetupOutput> setMisc)
    {
        Template = template;
        SetMisc = setMisc;
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
        IClothingSetupInput input = new ClothingSetupInput();
        ClothingMiscData copy = Template.ShallowCopy();
        SetMisc?.Invoke(input, copy);
        return new Clothing(copy, _completeGen);
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
        IClothingSetupInput input = new ClothingSetupInput();
        ClothingMiscData copy = Template.ShallowCopy();
        SetMisc?.Invoke(input, copy);
        return new Clothing<T>(copy, _completeGen, _paramsCalc);
    }
}

/*
internal class ClothingBuilderV3<T>
{
    private protected ClothingMiscData Misc;

    public void Setup(ClothingMiscData template, Action<IClothingSetupInput, IClothingSetupOutput> setMisc)
    {
        
        
        
        IClothingSetupInput input = new ClothingSetupInput();
        ClothingMiscData copy = ClothingBuilder.DefaultMisc.ShallowCopy();
        setMisc?.Invoke(input, copy);
        Misc = copy;
    }
    
    public void Setup(ClothingMiscData template)
    {
        Setup(template, null);
    }
    
    private Action<IClothingRenderInput<T>, IClothingRenderOutput> _completeGen;
    private readonly Func<IClothingRenderInput, T> _paramsCalc;
    
    public ClothingBuilderV3(Func<IClothingRenderInput, T> paramsCalc)
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
*/
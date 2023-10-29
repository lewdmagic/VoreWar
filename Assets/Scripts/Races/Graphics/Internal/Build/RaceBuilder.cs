#region

using System;
using UnityEngine;

#endregion

internal class RaceBuilder : RaceBuilderShared, IRaceBuilder
{
    private protected readonly SpriteTypeIndexed<SingleRenderFunc<IParameters>> _raceSpriteSet = new SpriteTypeIndexed<SingleRenderFunc<IParameters>>();


    internal Action<IRandomCustomInput> _randomCustom;

    private Action<IRunInput, IRunOutput> _runBefore;
    private Action<MiscRaceDataReadable<IParameters>> _setupFunc;

    private Func<MiscRaceDataReadable<IParameters>> _template;

    internal RaceBuilder(Func<MiscRaceDataReadable<IParameters>> template)
    {
        _template = template;
    }

    public void Setup(Action<IMiscRaceData<IParameters>> setupFunc)
    {
        _setupFunc = setupFunc;
    }

    public void RandomCustom(Action<IRandomCustomInput> value)
    {
        _randomCustom = value;
    }

    public void RunBefore(Action<IRunInput, IRunOutput> value)
    {
        _runBefore = value;
    }

    public void RenderSingle(SpriteType spriteType, int layer, Action<IRaceRenderInput, IRaceRenderOutput> generator)
    {
        _raceSpriteSet[spriteType] = new SingleRenderFunc<IParameters>(layer, generator);
    }
    
    public void RenderSingle(SpriteType spriteType, SingleRenderFunc<IParameters> render)
    {
        _raceSpriteSet[spriteType] = render;
    }

    public MiscRaceDataReadable<IParameters> toMiscData()
    {
        MiscRaceDataReadable<IParameters> dataReadable = _template();
        _setupFunc?.Invoke(dataReadable);
        return dataReadable;
    }


    internal static RaceBuilder New(Func<MiscRaceDataReadable<IParameters>> template)
    {
        return new RaceBuilder(template);
    }

    internal static RaceBuilder<T> New<T>(Func<MiscRaceDataReadable<T>> template) where T : IParameters, new()
    {
        return new RaceBuilder<T>(template);
    }

    internal static IRaceData Create(Func<MiscRaceDataReadable<IParameters>> template, Action<IRaceBuilder> builderUser)
    {
        RaceBuilder builder = New(template);
        builderUser.Invoke(builder);
        return builder.Build();
    }

    internal static IRaceData Create<T>(Func<MiscRaceDataReadable<T>> template, Action<IRaceBuilder<T>> builderUser) where T : IParameters, new()
    {
        RaceBuilder<T> builder = New(template);
        builderUser.Invoke(builder);
        return builder.Build();
    }

    public IRaceData Build()
    {
        Func<IParameters> basic = () => new EmptyParameters();
        return new RaceData<IParameters>(_raceSpriteSet, toMiscData(), _runBefore, _randomCustom, basic);
    }

    private class EmptyParameters : IParameters
    {
    }
}


internal class RaceBuilder<T> : RaceBuilderShared, IRaceBuilder<T> where T : IParameters, new()
{
    private protected readonly SpriteTypeIndexed<SingleRenderFunc<T>> RaceSpriteSet = new SpriteTypeIndexed<SingleRenderFunc<T>>();

    private Action<IRandomCustomInput> _randomCustom;

    private Action<IRunInput, IRunOutput<T>> _runBefore;
    private Action<MiscRaceDataReadable<T>> _setupFunc;

    private Func<MiscRaceDataReadable<T>> _template;


    internal RaceBuilder(Func<MiscRaceDataReadable<T>> template)
    {
        _template = template;
    }

    public void Setup(Action<IMiscRaceData<T>> setupFunc)
    {
        _setupFunc = setupFunc;
    }

    public void RandomCustom(Action<IRandomCustomInput> value)
    {
        _randomCustom = value;
    }

    public void RunBefore(Action<IRunInput, IRunOutput<T>> value)
    {
        _runBefore = value;
    }

    public void RenderSingle(SpriteType spriteType, int layer, Action<IRaceRenderInput<T>, IRaceRenderOutput> generator)
    {
        RaceSpriteSet[spriteType] = new SingleRenderFunc<T>(layer, generator);
    }

    public void RenderSingle(SpriteType spriteType, SingleRenderFunc<T> render)
    {
        RaceSpriteSet[spriteType] = render;
    }

    private MiscRaceDataReadable<T> toMiscData()
    {
        MiscRaceDataReadable<T> dataReadable = _template();
        _setupFunc?.Invoke(dataReadable);
        return dataReadable;
    }

    public IRaceData Build()
    {
        Func<T> makeTempState = () => new T();
        return new RaceData<T>(RaceSpriteSet, toMiscData(), _runBefore, _randomCustom, makeTempState);
    }
}
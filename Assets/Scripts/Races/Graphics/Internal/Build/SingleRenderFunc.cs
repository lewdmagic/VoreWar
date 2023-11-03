#region

using System;
using System.Collections.Generic;

#endregion

internal interface ISingleRenderFunc
{
    int Layer { get; }
}

internal class SingleRenderFunc<T> : ISingleRenderFunc where T : IParameters
{
    private readonly List<Action<IRaceRenderInput<T>, IRaceRenderOutput>> _generators = new List<Action<IRaceRenderInput<T>, IRaceRenderOutput>>();

    internal SingleRenderFunc(int layer, Action<IRaceRenderInput<T>, IRaceRenderOutput> generator)
    {
        Layer = layer;
        _generators.Add(generator);
    }

    internal void ModAfter(Action<IRaceRenderInput<T>, IRaceRenderOutput> generator)
    {
        _generators.Add(generator);
    }
    
    internal void ModBefore(Action<IRaceRenderInput<T>, IRaceRenderOutput> generator)
    {
        _generators.Insert(0, generator);
    }
    
    public void Invoke(IRaceRenderInput<T> input, IRaceRenderOutput output)
    {
        foreach (Action<IRaceRenderInput<T>,IRaceRenderOutput> generator in _generators)
        {
            generator.Invoke(input, output);
        }
    }

    public int Layer { get; }
}
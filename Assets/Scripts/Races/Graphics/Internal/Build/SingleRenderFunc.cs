#region

using System;
using System.Collections.Generic;

#endregion

internal interface ISingleRenderFunc
{
    int Layer { get; }
}

internal class SingleRenderFunc : ISingleRenderFunc
{
    private readonly List<Action<IRaceRenderInput, IRaceRenderOutput>> _generators = new List<Action<IRaceRenderInput, IRaceRenderOutput>>();

    internal SingleRenderFunc(int layer, Action<IRaceRenderInput, IRaceRenderOutput> generator)
    {
        Layer = layer;
        _generators.Add(generator);
    }

    internal void ModAfter(Action<IRaceRenderInput, IRaceRenderOutput> generator)
    {
        _generators.Add(generator);
    }
    
    internal void ModBefore(Action<IRaceRenderInput, IRaceRenderOutput> generator)
    {
        _generators.Insert(0, generator);
    }
    
    public void Invoke(IRaceRenderInput input, IRaceRenderOutput output)
    {
        foreach (Action<IRaceRenderInput,IRaceRenderOutput> generator in _generators)
        {
            generator.Invoke(input, output);
        }
    }

    public int Layer { get; }
}
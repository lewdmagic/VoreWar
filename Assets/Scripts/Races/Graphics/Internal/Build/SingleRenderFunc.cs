#region

using System;
using UnityEngine;

#endregion

internal interface ISingleRenderFunc
{
    int Layer { get; }
}

internal class SingleRenderFunc<T> : ISingleRenderFunc where T : IParameters
{
    internal readonly Action<IRaceRenderInput<T>, IRaceRenderOutput> Generator;
    internal Func<IRaceRenderInput<T>, IRaceRenderOutput> GeneratorMod; // TODO temp testing

    internal SingleRenderFunc(int layer, Action<IRaceRenderInput<T>, IRaceRenderOutput> generator)
    {
        Layer = layer;
        Generator = generator;
    }

    public int Layer { get; private set; }
}
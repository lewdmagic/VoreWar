#region

using System;
using UnityEngine;

#endregion

internal interface IRaceBuilder
{
    void Setup(Action<IMiscRaceData<IParameters>> setupFunc);
    void RandomCustom(Action<IRandomCustomInput> value);
    void RunBefore(Action<IRunInput, IRunOutput> value);
    
    void RenderSingle(SpriteType spriteType, int layer, Action<IRaceRenderInput, IRaceRenderOutput> generator);

    void RenderSingle(SpriteType spriteType, SingleRenderFunc<IParameters> render);
}


internal interface IRaceBuilder<T> where T : IParameters, new()
{
    void Setup(Action<IMiscRaceData<T>> setupFunc);
    void RandomCustom(Action<IRandomCustomInput> value);
    void RunBefore(Action<IRunInput, IRunOutput<T>> value);
    
    void RenderSingle(SpriteType spriteType, int layer, Action<IRaceRenderInput<T>, IRaceRenderOutput> generator);

    void RenderSingle(SpriteType spriteType, SingleRenderFunc<T> render);
}
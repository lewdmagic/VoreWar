#region

using System;

#endregion


internal interface IRaceBuilder<T> where T : IParameters
{
    void Setup(Action<MiscRaceDataWritableReadable<T>> setupFunc);
    void RandomCustom(Action<IRandomCustomInput> value);
    void RunBefore(Action<IRunInput, IRunOutput<T>> value);
    
    void RenderSingle(SpriteType spriteType, int layer, Action<IRaceRenderInput<T>, IRaceRenderOutput> generator);

    void RenderSingle(SpriteType spriteType, SingleRenderFunc<T> render);
    
    void RenderAll(Action<IRunInput, IRaceRenderAllOutput<T>> generator);
}
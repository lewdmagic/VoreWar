#region

using System;

#endregion


internal interface IRaceBuilder
{
    void Setup(Action<ISetupInput, ISetupOutput> setupFunc);
    void RandomCustom(Action<IRandomCustomInput, IRandomCustomOutput> value);
    void RunBefore(Action<IRunInput, IRunOutput> value);

    void RenderSingle(SpriteType spriteType, int layer, Action<IRaceRenderInput, IRaceRenderOutput> generator);

    void RenderSingle(SpriteType spriteType, SingleRenderFunc render);

    void RenderAll(Action<IRunInput, IRaceRenderAllOutput> generator);
}
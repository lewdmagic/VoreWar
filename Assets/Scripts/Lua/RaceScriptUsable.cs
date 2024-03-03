using System;

internal class RaceScriptUsable
{
    internal readonly Action<ISetupInput, ISetupOutput> SetupFunc;
    internal readonly Action<IRunInput, IRaceRenderAllOutput> Generator;
    internal readonly Action<IRandomCustomInput, IRandomCustomOutput> Value;

    internal RaceScriptUsable(Action<ISetupInput, ISetupOutput> setupFunc, Action<IRunInput, IRaceRenderAllOutput> generator, Action<IRandomCustomInput, IRandomCustomOutput> value)
    {
        SetupFunc = setupFunc;
        Generator = generator;
        Value = value;
    }
}
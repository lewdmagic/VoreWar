using System;

internal class RaceScriptUsable
{
    internal Action<MiscRaceData> SetupFunc;
    internal Action<IRunInput, IRaceRenderAllOutput> Generator;
    internal Action<IRandomCustomInput> Value;

    internal RaceScriptUsable(Action<MiscRaceData> setupFunc, Action<IRunInput, IRaceRenderAllOutput> generator, Action<IRandomCustomInput> value)
    {
        SetupFunc = setupFunc;
        Generator = generator;
        Value = value;
    }
}
using System;
using MoonSharp.Interpreter;

internal class ClothingScriptUsable
{
    internal Action<IClothingSetupInput, IClothingSetupOutput> SetMisc;
    internal Action<IClothingRenderInput, IClothingRenderOutput, Table> CompleteGen;

    public ClothingScriptUsable(Action<IClothingSetupInput, IClothingSetupOutput> setMisc, Action<IClothingRenderInput, IClothingRenderOutput, Table> completeGen)
    {
        SetMisc = setMisc;
        CompleteGen = completeGen;
    }
}
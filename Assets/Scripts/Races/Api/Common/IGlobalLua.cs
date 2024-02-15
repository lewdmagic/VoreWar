using System;
using MoonSharp.Interpreter;
using UnityEngine;

public interface IGlobalLua
{
    void Log(object message);

    Vector3 NewVector3(float x, float y, float z);

    Vector2 NewVector2(float x, float y);

    ColorSwapPalette GetPalette(SwapType swap, int index);

    int GetPaletteCount(SwapType swap);

    IClothing MakeClothing(string stringId);

    IClothing MakeClothingWithParams(string stringId, Func<IClothingRenderInput,Table> paramCalc);

    int RandomInt(int maxValue);

    FlavorEntry NewFlavorEntry(string text);

    FlavorEntry NewFlavorEntryGendered(string text, Gender gender);
}
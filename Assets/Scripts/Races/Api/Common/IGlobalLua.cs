using System;
using MoonSharp.Interpreter;
using UnityEngine;

public interface IGlobalLua
{
    void Log(object message);

    Vector3 NewVector3(float x, float y, float z);

    Vector2 NewVector2(float x, float y);
    
    int GetPaletteCount(string paletteName);

    IClothing MakeClothing(string stringId);

    IClothing MakeClothingWithParams(string stringId, Func<IClothingRenderInput,Table> paramCalc);

    int RandomInt(int maxValue);

    FlavorEntry NewFlavorEntry(string text);

    StatRange NewStatRange(int min, int max);

    FlavorEntry NewFlavorEntryGendered(string text, Gender gender);
}
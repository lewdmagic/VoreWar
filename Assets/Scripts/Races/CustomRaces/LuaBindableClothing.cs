using System;
using MoonSharp.Interpreter;




internal class ClothingLua : ClothingDataShared, IClothing
{
    private readonly Action<IClothingRenderInput, IClothingRenderOutput, DynValue> _renderAll;
    private readonly Func<IClothingRenderInput, DynValue> _calcParams;
    private readonly SpriteCollection _spriteCollection;

    public ClothingLua(
        ClothingMiscData fixedData,
        Action<IClothingRenderInput, IClothingRenderOutput, DynValue> renderAll,
        Func<IClothingRenderInput, DynValue> calcParams,
        SpriteCollection spriteCollection
        ) : base(fixedData)
    {
        _renderAll = renderAll;
        _calcParams = calcParams;
        _spriteCollection = spriteCollection;
    }

    public ClothingRenderOutput Configure(Actor_Unit actor, SpriteChangeDict changeDict)
    {
        ClothingRenderInput input = new ClothingRenderInput(actor);
        ClothingRenderOutput renderOutput = new ClothingRenderOutput(changeDict, Misc, _spriteCollection);
        
        DynValue calcdParameters = _calcParams.Invoke(input);
        _renderAll.Invoke(input, renderOutput, calcdParameters);
        
        return renderOutput;
    }
}


internal class LuaBindableClothing
{
    private Action<IClothingSetupInput, IClothingSetupOutput> _setMisc;
    private Action<IClothingRenderInput, IClothingRenderOutput, DynValue> _renderAll;

    public LuaBindableClothing(Action<IClothingSetupInput, IClothingSetupOutput> setMisc, Action<IClothingRenderInput, IClothingRenderOutput, DynValue> renderAll)
    {
        _setMisc = setMisc;
        _renderAll = renderAll;
    }

    internal IClothing Create(
        Func<IClothingRenderInput, DynValue> paramsCalc,
        SpriteCollection spriteCollection
        )
    {
        IClothingSetupInput input = new ClothingSetupInput();
        ClothingMiscData copy = ClothingBuilder.DefaultMisc.ShallowCopy();
        _setMisc?.Invoke(input, copy);

        ClothingLua clothingLua = new ClothingLua(copy, _renderAll, paramsCalc, spriteCollection);
        return clothingLua;
    }
}
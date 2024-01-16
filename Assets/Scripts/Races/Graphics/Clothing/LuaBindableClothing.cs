using System;
using MoonSharp.Interpreter;




internal class ClothingLua : ClothingDataShared, IClothing
{
    private readonly Action<IClothingRenderInput, IClothingRenderOutput, DynValue> _renderAll;
    private readonly Func<IClothingRenderInput, DynValue> _calcParams;

    public ClothingLua(ClothingMiscData fixedData, Action<IClothingRenderInput, IClothingRenderOutput, DynValue> renderAll, Func<IClothingRenderInput, DynValue> calcParams) : base(fixedData)
    {
        _renderAll = renderAll;
        _calcParams = calcParams;
    }

    public ClothingRenderOutput Configure(Actor_Unit actor, SpriteChangeDict changeDict)
    {
        IClothingRenderInput inputBasic = new ClothingRenderInputImpl(actor);
        ClothingRenderInputImpl input = new ClothingRenderInputImpl(actor);
        ClothingRenderOutput renderOutput = new ClothingRenderOutput(changeDict, Misc);
        
        DynValue calcdParameters = _calcParams.Invoke(inputBasic);
        _renderAll.Invoke(input, renderOutput, calcdParameters);
        
        return renderOutput;
    }
}


internal class LuaBindableClothing
{
    internal IClothing Create(
        Action<IClothingSetupInput, IClothingSetupOutput> setMisc,
        Action<IClothingRenderInput, IClothingRenderOutput, DynValue> renderAll,
        Func<IClothingRenderInput, DynValue> paramsCalc
        )
    {
        IClothingSetupInput input = new ClothingSetupInput();
        ClothingMiscData copy = ClothingBuilder.DefaultMisc.ShallowCopy();
        setMisc?.Invoke(input, copy);

        ClothingLua clothingLua = new ClothingLua(copy, renderAll, paramsCalc);
        return clothingLua;
    }
}
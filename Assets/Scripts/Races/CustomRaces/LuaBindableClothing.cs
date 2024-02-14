using System;
using MoonSharp.Interpreter;


internal class ClothingLua : ClothingDataShared, IClothing
{
    private readonly Action<IClothingRenderInput, IClothingRenderOutput, Table> _renderAll;
    private readonly Func<IClothingRenderInput, Table> _calcParams;
    private readonly SpriteCollection _spriteCollection;

    public ClothingLua(
        ClothingMiscData fixedData,
        Action<IClothingRenderInput, IClothingRenderOutput, Table> renderAll,
        Func<IClothingRenderInput, Table> calcParams,
        SpriteCollection spriteCollection
    ) : base(fixedData)
    {
        _renderAll = renderAll;
        _calcParams = calcParams;
        _spriteCollection = spriteCollection;
    }

    public override ClothingRenderOutput Configure(ActorUnit actor, ISpriteChanger changeDict)
    {
        ClothingRenderInput input = new ClothingRenderInput(actor);
        ClothingRenderOutput renderOutput = new ClothingRenderOutput(changeDict, Misc, _spriteCollection);

        // TODO These are modified defaults for now only set for lua
        // This will need to be implemented into the rest of the clothing
        // but it will probably require scripted regex refactor
        renderOutput.BlocksBreasts = false;
        renderOutput.RevealsDick = true;
        renderOutput.RevealsBreasts = true;

        Table calcdParameters = _calcParams.Invoke(input);
        _renderAll.Invoke(input, renderOutput, calcdParameters);

        return renderOutput;
    }
}


internal class LuaBindableClothing
{
    private Action<IClothingSetupInput, IClothingSetupOutput> _setMisc;
    private Action<IClothingRenderInput, IClothingRenderOutput, Table> _renderAll;

    public LuaBindableClothing(Action<IClothingSetupInput, IClothingSetupOutput> setMisc, Action<IClothingRenderInput, IClothingRenderOutput, Table> renderAll)
    {
        _setMisc = setMisc;
        _renderAll = renderAll;
    }

    internal IClothing Create(
        Func<IClothingRenderInput, Table> paramsCalc,
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
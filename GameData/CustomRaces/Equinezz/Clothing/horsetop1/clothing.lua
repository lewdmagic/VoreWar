function setup(input, output)
    output.DiscardSprite = null;
    output.Type = 76147;
    output.FemaleOnly = true;
    output.RevealsBreasts = true;
    output.RevealsDick = true;
    output.DiscardUsesPalettes = true;
end

function render(input, output)
    output["Clothing1"].Layer(20);
    --if (input.Params.Oversize) then
    --    output["Clothing1"].Sprite(input.Sprites.HorseClothing[47]);
    --elseif (input.U.HasBreasts) then
    --    output["Clothing1"].Sprite(input.Sprites.HorseClothing[40 + input.U.BreastSize]);
    --end
    if (input.U.HasBreasts) then
        output["Clothing1"].Sprite(input.Sprites.HorseClothing[40 + input.U.BreastSize]);
    end

    output["Clothing1"].Coloring(GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
end
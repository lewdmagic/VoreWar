API_VERSION = "0.0.1"

---@param input IClothingSetupInput
---@param output IClothingSetupOutput
function Setup(input, output)
    output.FemaleOnly = true;
    output.DiscardUsesPalettes = true;
end

function Render(input, output, params)
    local sprite = output.NewSprite(20);
    sprite.Coloring(GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));

    if (params.oversize) then
        sprite.Sprite("oversize");
    elseif (input.U.HasBreasts) then
        sprite.Sprite0("main", input.U.BreastSize);
    end
end
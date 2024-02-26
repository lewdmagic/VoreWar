API_VERSION = "0.0.1"

---@param input IClothingSetupInput
---@param output IClothingSetupOutput
function Setup(input, output)
    output.MaleOnly = true;
    output.DiscardUsesPalettes = true;
end

---@param input IClothingRenderInput
---@param output IClothingRenderOutput
function Render(input, output)
    local size = input.A.GetStomachSize(32, 1.2);

    output.NewSprite(20)
            .Coloring(GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor))
            .Sprite(ternary(size >= 6, "big_stomach", "normal"));
end
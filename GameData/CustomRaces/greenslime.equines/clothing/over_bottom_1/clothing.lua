API_VERSION = "0.0.1"

---@param input IClothingSetupInput
---@param output IClothingSetupOutput
function Setup(input, output)
    output.DiscardUsesPalettes = true;
end

---@param input IClothingRenderInput
---@param output IClothingRenderOutput
function Render(input, output)
    output.DisableDick();

    output.NewSprite("main", 15)
            .Sprite(input.Sex, ternary(input.A.HasBelly, "hasbelly", "nobelly"))
            .Coloring(GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
end
API_VERSION = "0.0.1"

---@param input IClothingSetupInput
---@param output IClothingSetupOutput
function Setup(input, output)
    output.DiscardUsesPalettes = true;
end

---@param input IClothingRenderInput
---@param output IClothingRenderOutput
function Render(input, output)
    output.NewSprite(3)
            .Palette("clothing", input.U.ClothingColor)
            .Sprite("main_back");

    output.NewSprite(21)
            .Palette("clothing", input.U.ClothingColor)
            .Sprite("main_front");
end
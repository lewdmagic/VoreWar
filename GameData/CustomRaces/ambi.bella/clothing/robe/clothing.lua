API_VERSION = "0.1.0"

---@param input IClothingSetupInput
---@param output IClothingSetupOutput
function Setup(input, output)

end

function Render(input, output)
    local stomachSize = input.A.GetStomachSize(4, 1)
    local bodySize = tostring(input.U.BodySize + 1);

    local sprite = output.NewSprite(3);
    sprite.Sprite0("robe", bodySize, stomachSize);
end
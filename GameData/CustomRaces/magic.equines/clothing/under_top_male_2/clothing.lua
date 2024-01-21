API_VERSION = "0.0.1"

function setup(input, output)
    output.MaleOnly = true;
    output.DiscardUsesPalettes = true;
end

function render(input, output)
    local size = input.A.GetStomachSize(32, 1.2);

    output.NewSprite(20)
            .Coloring(GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor))
            .Sprite(ternary(size >= 6, "big_stomach", "normal"));
end
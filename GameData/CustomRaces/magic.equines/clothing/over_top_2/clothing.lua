API_VERSION = "0.0.1"

function setup(input, output)
    output.DiscardUsesPalettes = true;
end

function render(input, output)
    output.NewSprite(21)
            .Coloring(GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor))
            .Sprite("main");
end
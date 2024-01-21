API_VERSION = "0.0.1"

function setup(input, output)
    output.DiscardUsesPalettes = true;
end

function render(input, output)
    output.DisableDick();
    
    output.NewSprite("main", 15)
            .Sprite(input.Sex, ternary(input.A.HasBelly, "hasbelly", "nobelly"))
            .Coloring(GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
end
function setup(input, output)
    output.MaleOnly = true;
    output.RevealsBreasts = true;
    output.RevealsDick = true;
    output.DiscardUsesPalettes = true;
end

function render(input, output)
    output.NewSprite(20)
            .Coloring(GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor))
            .Sprite("main");
end



function setup(input, output)
    output.DiscardSprite = null;
    output.Type = 76109;
    output.RevealsBreasts = true;
    output.DiscardUsesPalettes = true;
end

function render(input, output)
    
    output.NewSprite("main", 9)
            .Sprite(input.Sex, ternary(input.A.HasBelly, "hasbelly", "nobelly"))
            .Coloring(GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));

    if (input.U.HasDick) then
        output.NewSprite("bulge", 10)
                .Coloring(GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor))
                .Sprite0("bulge", input.U.DickSize);
    end
end



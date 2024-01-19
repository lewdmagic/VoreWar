function setup(input, output)
    output.DiscardSprite = null;
    output.Type = 76105;
    output.RevealsBreasts = true;
    output.DiscardUsesPalettes = true;
end

function render(input, output)
    local belly = ternary(input.A.HasBelly, "hasbelly", "nobelly");
    output["Clothing1"].Layer(9);
    output["Clothing1"].Coloring(Color.white);
    output["Clothing1"].Sprite("male", belly);

    if (input.U.HasDick) then
        output["Clothing2"].Layer(10).Coloring(Color.white).Sprite0("bulge", input.U.DickSize);
    end
end



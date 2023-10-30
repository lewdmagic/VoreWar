internal static class Goodra
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.GoodraSkin);
            output.EyeTypes = 4;
            output.GentleAnimation = true;
            output.ClothingColors = 0;
        });


        /*
        Body = new SpriteExtraInfo(1, RaceSpriteGeneratorSet.BodySprite, WhiteColored); // Body
        Belly = new SpriteExtraInfo(2, null, WhiteColored); // Belly
        BodyAccent = new SpriteExtraInfo(3, RaceSpriteGeneratorSet.BodyAccentSprite, WhiteColored); // Leg
        Eyes = new SpriteExtraInfo(4, RaceSpriteGeneratorSet.EyesSprite, WhiteColored); // Face
        Hair = new SpriteExtraInfo(5, RaceSpriteGeneratorSet.HeadSprite, WhiteColored); // Attack Frame
        */

        builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
        {
            output.Coloring(Defaults.FurryColor(input.Actor));
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Goodra[3]);
            }
        });

        builder.RenderSingle(SpriteType.Eyes, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GoodraSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsOralVoring)
            {
                return;
            }

            output.Sprite(input.Sprites.Goodra[4 + input.Actor.Unit.EyeType]);
        }); // Face

        builder.RenderSingle(SpriteType.Hair, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GoodraSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Goodra[3]);
            }
        }); // Attack Frame (This is headSprite set to Hair for some reason). Might be a mistake

        builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GoodraSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Goodra[2]);
                return;
            }

            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Goodra[1]);
                return;
            }

            output.Sprite(input.Sprites.Goodra[0]);
        }); // Body

        builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GoodraSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Goodra[9]);
        }); // Leg
        builder.RenderSingle(SpriteType.Belly, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.GoodraSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            output.Sprite(input.Sprites.Goodra[10 + input.Actor.GetStomachSize(6)]);
        }); // Belly

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(Defaults.RandomCustom);
    });
}
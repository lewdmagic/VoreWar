internal static class Terrorbird
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.SpecialAccessoryCount = 8; // head plumage type
            output.ClothingColors = 0;
            output.GentleAnimation = true;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.TerrorbirdSkin);
            output.EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.TerrorbirdSkin);
        });


        builder.RenderSingle(SpriteType.Head, 9, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Terrorbird[10]);
                return;
            }

            output.Sprite(input.Sprites.Terrorbird[9]);
        });

        builder.RenderSingle(SpriteType.Eyes, 10, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerrorbirdSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Terrorbird[12]);
                return;
            }

            output.Sprite(input.Sprites.Terrorbird[11]);
        });

        builder.RenderSingle(SpriteType.Body, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerrorbirdSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Terrorbird[1]);
                return;
            }

            output.Sprite(input.Sprites.Terrorbird[0]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 9, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerrorbirdSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.GetStomachSize(29) >= 27)
            {
                output.Sprite(input.Sprites.Terrorbird[14]);
                return;
            }

            output.Sprite(input.Sprites.Terrorbird[6]);
        }); // front legs feathers

        builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerrorbirdSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Terrorbird[7]);
        }); // back leg feathers
        builder.RenderSingle(SpriteType.BodyAccent3, 10, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.GetStomachSize(29) >= 27)
            {
                output.Sprite(input.Sprites.Terrorbird[15]);
                return;
            }

            output.Sprite(input.Sprites.Terrorbird[4]);
        }); // front leg claws

        builder.RenderSingle(SpriteType.BodyAccent4, 2, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Terrorbird[5]);
        }); // back leg claws
        builder.RenderSingle(SpriteType.BodyAccent5, 11, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerrorbirdSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Terrorbird[3]);
                return;
            }

            output.Sprite(input.Sprites.Terrorbird[2]);
        }); // wings

        builder.RenderSingle(SpriteType.BodyAccent6, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerrorbirdSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Terrorbird[13]);
        }); // back tail feather
        builder.RenderSingle(SpriteType.BodyAccent7, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerrorbirdSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Terrorbird[8]);
        }); // belly cover
        builder.RenderSingle(SpriteType.BodyAccent8, 12, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerrorbirdSkin, input.Actor.Unit.SkinColor));
            int sizet = input.Actor.GetTailSize(4);
            if (input.Actor.Unit.Predator == false || input.Actor.PredatorComponent?.TailFullness == 0)
            {
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Terrorbird[67 + 2 * sizet]);
                return;
            }

            output.Sprite(input.Sprites.Terrorbird[66 + 2 * sizet]);
        }); // crop

        builder.RenderSingle(SpriteType.BodyAccessory, 13, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerrorbirdSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Terrorbird[24 + input.Actor.Unit.SpecialAccessoryType]);
                return;
            }

            output.Sprite(input.Sprites.Terrorbird[16 + input.Actor.Unit.SpecialAccessoryType]);
        }); // head plumage

        builder.RenderSingle(SpriteType.Belly, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.TerrorbirdSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
            {
                output.Sprite(input.Sprites.Terrorbird[65]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
            {
                if (input.Actor.GetStomachSize(29, .75f) == 29)
                {
                    output.Sprite(input.Sprites.Terrorbird[64]);
                    return;
                }

                if (input.Actor.GetStomachSize(29, .8275f) == 29)
                {
                    output.Sprite(input.Sprites.Terrorbird[63]);
                    return;
                }

                if (input.Actor.GetStomachSize(29, .9f) == 29)
                {
                    output.Sprite(input.Sprites.Terrorbird[62]);
                    return;
                }
            }

            output.Sprite(input.Sprites.Terrorbird[32 + input.Actor.GetStomachSize(29)]);
        });

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(Defaults.RandomCustom);
    });
}
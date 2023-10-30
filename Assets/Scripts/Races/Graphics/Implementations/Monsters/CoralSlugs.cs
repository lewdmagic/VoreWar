#region

using System.Collections.Generic;

#endregion

internal static class CoralSlugs
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.None };
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SlugSkin);
            output.GentleAnimation = true;
            output.ClothingColors = 0;
        });


        builder.RenderSingle(SpriteType.Head, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.CoralSlug[1]);
                return;
            }

            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.CoralSlug[2]);
                return;
            }

            output.Sprite(input.Sprites.CoralSlug[0]);
        });

        builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly == false)
            {
                output.Sprite(input.Sprites.CoralSlug[5]);
                return;
            }

            if (input.Actor.GetStomachSize(9) < 3)
            {
                output.Sprite(input.Sprites.CoralSlug[5]);
                return;
            }

            if (input.Actor.GetStomachSize(9) >= 3 && input.Actor.GetStomachSize(9) < 5)
            {
                output.Sprite(input.Sprites.CoralSlug[6]);
                return;
            }

            if (input.Actor.GetStomachSize(9) >= 5 && input.Actor.GetStomachSize(9) < 7)
            {
                output.Sprite(input.Sprites.CoralSlug[7]);
                return;
            }

            if (input.Actor.GetStomachSize(9) >= 7 && input.Actor.GetStomachSize(9) < 9)
            {
                output.Sprite(input.Sprites.CoralSlug[8]);
                return;
            }

            output.Sprite(input.Sprites.CoralSlug[9]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 10, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.CoralSlug[3]);
            }
        }); // acid

        builder.RenderSingle(SpriteType.BodySize, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly == false)
            {
                output.Sprite(input.Sprites.CoralSlug[4]);
            }
        }); // belly cover up

        builder.RenderSingle(SpriteType.Belly, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.CoralSlug[20]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.CoralSlug[10 + input.Actor.GetStomachSize(9)]);
                return;
            }

            output.Sprite(input.Sprites.CoralSlug[10 + input.Actor.GetStomachSize(9)]);
        });
        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(Defaults.RandomCustom);
    });
}
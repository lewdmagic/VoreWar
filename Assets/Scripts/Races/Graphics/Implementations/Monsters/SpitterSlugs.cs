﻿#region

using System.Collections.Generic;

#endregion

internal static class SpitterSlugs
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
                output.Sprite(input.Sprites.SpitterSlug[3]);
                return;
            }

            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.SpitterSlug[4]);
                return;
            }

            output.Sprite(input.Sprites.SpitterSlug[2]);
        });

        builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsOralVoring || input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.SpitterSlug[1]);
                return;
            }

            output.Sprite(input.Sprites.SpitterSlug[0]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 10, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.SpitterSlug[7]);
                return;
            }

            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.SpitterSlug[8]);
                return;
            }

            output.Sprite(input.Sprites.SpitterSlug[6]);
        }); // slime

        builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.SpitterSlug[23]);
        }); // tail end
        builder.RenderSingle(SpriteType.BodySize, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly == false)
            {
                output.Sprite(input.Sprites.SpitterSlug[22]);
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
                output.Sprite(input.Sprites.SpitterSlug[21]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.SpitterSlug[11 + input.Actor.GetStomachSize(9)]);
                return;
            }

            output.Sprite(input.Sprites.SpitterSlug[11 + input.Actor.GetStomachSize(9)]);
        });

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(Defaults.RandomCustom);
    });
}
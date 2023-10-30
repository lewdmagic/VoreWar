#region

using System.Collections.Generic;

#endregion

internal static class RockSlugs
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
                output.Sprite(input.Sprites.RockSlug[1]);
                return;
            }

            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.RockSlug[2]);
                return;
            }

            output.Sprite(input.Sprites.RockSlug[0]);
        });

        builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.RockSlug[3]);
        });
        builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.RockSlug[4]);
        }); // tail end
        builder.RenderSingle(SpriteType.BodySize, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlugSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly == false)
            {
                output.Sprite(input.Sprites.RockSlug[5]);
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
                output.Sprite(input.Sprites.RockSlug[18]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.RockSlug[6 + input.Actor.GetStomachSize(11)]);
                return;
            }

            output.Sprite(input.Sprites.RockSlug[6 + input.Actor.GetStomachSize(11)]);
        });


        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(Defaults.RandomCustom);
    });
}
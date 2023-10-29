#region

using System.Collections.Generic;

#endregion

internal static class FeralAnts
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.None };

            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Ant);
            output.GentleAnimation = true;
        });

        builder.RenderSingle(SpriteType.Head, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Ant, input.Actor.Unit.SkinColor));
            if (input.Actor.IsOralVoring || input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Ant[1]);
                return;
            }

            output.Sprite(input.Sprites.Ant[0]);
        }); // Head

        builder.RenderSingle(SpriteType.Belly, 0, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Ant, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.Predator == false)
            {
                output.Sprite(input.Sprites.Ant[2]);
                return;
            }

            int size = input.Actor.GetStomachSize(16, 0.75f);

            if (size == 16 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.Ant[19]);
                return;
            }

            if (size == 16 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.Ant[18]);
                return;
            }

            output.Sprite(input.Sprites.Ant[2 + input.Actor.GetStomachSize()]);
        }); // Belly

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(Defaults.RandomCustom);
    });
}
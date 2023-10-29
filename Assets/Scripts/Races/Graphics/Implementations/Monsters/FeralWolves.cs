#region

using System.Collections.Generic;

#endregion

internal static class FeralWolves
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.None };
            output.SkinColors = 6;
            output.HairColors = 6;
            output.GentleAnimation = true;
        });

        builder.RenderSingle(SpriteType.Head, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralWolfFur, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.FeralWolf[input.Actor.IsAttacking || input.Actor.IsEating ? 1 : 0]);
        });
        builder.RenderSingle(SpriteType.Eyes, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralWolfMane, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.FeralWolf[input.Actor.IsAttacking || input.Actor.IsEating ? 3 : 2]);
        });
        builder.RenderSingle(SpriteType.Hair, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralWolfMane, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.FeralWolf[6]);
        });
        builder.RenderSingle(SpriteType.Hair2, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralWolfMane, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.FeralWolf[7]);
        });
        builder.RenderSingle(SpriteType.Body, 0, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralWolfFur, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.FeralWolf[4]);
        });
        builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralWolfFur, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.FeralWolf[5]);
        });
        builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.FeralWolf[8]);
        });
        builder.RenderSingle(SpriteType.BodyAccent3, 2, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.FeralWolf[9]);
        });
        builder.RenderSingle(SpriteType.BodyAccessory, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralWolfFur, input.Actor.Unit.SkinColor));
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false) && input.Actor.GetStomachSize() == 15)
            {
                return;
            }

            if (input.Actor.GetStomachSize(4) == 4)
            {
                output.Sprite(input.Sprites.FeralWolf[15]);
            }
        });

        builder.RenderSingle(SpriteType.Belly, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FeralWolfFur, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false) && input.Actor.GetStomachSize() == 15)
            {
                output.Sprite(input.Sprites.FeralWolf[16]);
                return;
            }

            output.Sprite(input.Sprites.FeralWolf[10 + input.Actor.GetStomachSize(4)]);
        });
        builder.RandomCustom(Defaults.RandomCustom);
    });
}
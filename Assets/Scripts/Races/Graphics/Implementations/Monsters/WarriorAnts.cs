#region

using System.Collections.Generic;

#endregion

internal static class WarriorAnts
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.None };
            output.SpecialAccessoryCount = 9; // antennae
            output.ClothingColors = 0;
            output.GentleAnimation = true;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemiantSkin);
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemiantSkin);
        });


        builder.RenderSingle(SpriteType.Head, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.WarriorAnt[1]);
                return;
            }

            output.Sprite(input.Sprites.WarriorAnt[0]);
        });

        builder.RenderSingle(SpriteType.Eyes, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.WarriorAnt[15]);
        });
        builder.RenderSingle(SpriteType.Mouth, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.WarriorAnt[2]);
            }
        });

        builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly == false)
            {
                output.Sprite(input.Sprites.WarriorAnt[16]);
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.WarriorAnt[4]);
                return;
            }

            output.Sprite(input.Sprites.WarriorAnt[3]);
        }); // mandibles

        builder.RenderSingle(SpriteType.BodyAccent2, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.WarriorAnt[5]);
        }); // legs
        builder.RenderSingle(SpriteType.BodyAccessory, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.WarriorAnt[6 + input.Actor.Unit.SpecialAccessoryType]);
        }); // antennae
        builder.RenderSingle(SpriteType.Belly, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.WarriorAnt[36]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                if (input.Actor.GetStomachSize(16, .8f) == 20)
                {
                    output.Sprite(input.Sprites.WarriorAnt[35]);
                    return;
                }

                if (input.Actor.GetStomachSize(16, .9f) == 20)
                {
                    output.Sprite(input.Sprites.WarriorAnt[34]);
                    return;
                }
            }

            output.Sprite(input.Sprites.WarriorAnt[17 + input.Actor.GetStomachSize(16)]);
        });


        builder.RunBefore((input, output) =>
        {
            output.changeSprite(SpriteType.Body).AddOffset(20 * .625f, 0);
            output.changeSprite(SpriteType.Belly).AddOffset(20 * .625f, 0);
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.AccessoryColor = unit.SkinColor;
        });
    });
}
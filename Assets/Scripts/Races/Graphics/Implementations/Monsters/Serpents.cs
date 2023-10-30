#region

using System;
using System.Collections.Generic;

#endregion

internal static class Serpents
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.None };
            output.EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);
            output.EyeTypes = 4;
            output.AvoidedEyeTypes = 1;
            output.GentleAnimation = true;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.LizardMain);
        });

        builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            output.Sprite(input.Sprites.Serpents[Math.Min(16 + input.Actor.Unit.EyeType, 19)]);
        });
        builder.RenderSingle(SpriteType.Mouth, 10, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.GetSimpleBodySprite() == 0 && input.Actor.Targetable)
            {
                if (State.Rand.Next(400) == 0)
                {
                    input.Actor.SetAnimationMode(2, .25f);
                }
            }

            int animationFrame = input.Actor.CheckAnimationFrame();
            if (animationFrame == 2)
            {
                output.Sprite(input.Sprites.Serpents[6]);
                return;
            }

            if (animationFrame == 1)
            {
                output.Sprite(input.Sprites.Serpents[7]);
                return;
            }

            output.Sprite(input.Actor.IsEating ? input.Sprites.Serpents[5] : null);
        });

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Serpents[input.Actor.IsAttacking || input.Actor.IsEating ? 2 : 0]);
        });
        builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Serpents[input.Actor.IsAttacking || input.Actor.IsEating ? 3 : 1]);
        });
        builder.RenderSingle(SpriteType.BodyAccessory, 9, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, input.Actor.Unit.SkinColor));
            output.Sprite(input.Actor.IsEating ? input.Sprites.Serpents[4] : null);
        });


        builder.RenderSingle(SpriteType.BodySize, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardMain, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            output.Sprite(input.Sprites.Serpents[8 + input.Actor.GetStomachSize(3, 3)]);
        });

        builder.RenderSingle(SpriteType.Belly, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.LizardLight, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            output.Sprite(input.Sprites.Serpents[12 + input.Actor.GetStomachSize(3, 3)]);
        });

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(Defaults.RandomCustom);
    });
}
#region

using System.Collections.Generic;

#endregion

internal static class Salamanders
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        RaceFrameList frameListSalamanderFlame = new RaceFrameList(new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, new float[10] { .1f, .1f, .1f, .1f, .1f, .1f, .1f, .1f, .1f, .1f });


        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.None };
            output.EyeTypes = 6;
            output.SpecialAccessoryCount = 12; // Backside spikes/patterns
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SalamanderSkin); // Backside spikes/pattern color
            output.EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SalamanderSkin);
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SalamanderSkin);
            output.GentleAnimation = true;
            output.WeightGainDisabled = true;
            output.ClothingColors = 0;
        });


        builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SalamanderSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsEating || input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Salamanders[1]);
                return;
            }

            output.Sprite(input.Sprites.Salamanders[0]);
        });

        builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SalamanderSkin, input.Actor.Unit.EyeColor));
            output.Sprite(input.Sprites.Salamanders[4 + input.Actor.Unit.EyeType]);
        });
        builder.RenderSingle(SpriteType.Mouth, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Salamanders[2]);
                return;
            }

            if (input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Salamanders[3]);
            }
        });

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SalamanderSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            output.Sprite(input.Sprites.Salamanders[32]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.AnimationController.frameLists[0].currentTime >= frameListSalamanderFlame.times[input.Actor.AnimationController.frameLists[0].currentFrame] && input.Actor.Unit.IsDead == false)
            {
                input.Actor.AnimationController.frameLists[0].currentFrame++;
                input.Actor.AnimationController.frameLists[0].currentTime = 0f;

                if (input.Actor.AnimationController.frameLists[0].currentFrame >= frameListSalamanderFlame.frames.Length)
                {
                    input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                    input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                }
            }

            output.Sprite(input.Sprites.Salamanders[22 + frameListSalamanderFlame.frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
        }); // flame

        builder.RenderSingle(SpriteType.BodyAccent2, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SalamanderSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly == false)
            {
                output.Sprite(input.Sprites.Salamanders[33]);
            }
        }); // Belly cover up

        builder.RenderSingle(SpriteType.BodyAccent3, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SalamanderSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.GetStomachSize(16) < 5)
            {
                output.Sprite(input.Sprites.Salamanders[34]);
                return;
            }

            if (input.Actor.GetStomachSize(16) >= 5 && input.Actor.GetStomachSize(16) < 9)
            {
                output.Sprite(input.Sprites.Salamanders[35]);
                return;
            }

            if (input.Actor.GetStomachSize(16) >= 9 && input.Actor.GetStomachSize(16) < 13)
            {
                output.Sprite(input.Sprites.Salamanders[36]);
                return;
            }

            if (input.Actor.GetStomachSize(16) >= 13 && input.Actor.GetStomachSize(16) < 16)
            {
                output.Sprite(input.Sprites.Salamanders[37]);
                return;
            }

            if (input.Actor.GetStomachSize(16) == 16)
            {
                output.Sprite(input.Sprites.Salamanders[38]);
                return;
            }

            output.Sprite(input.Sprites.Salamanders[34]);
        }); // right back leg

        builder.RenderSingle(SpriteType.BodyAccent4, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SalamanderSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.GetStomachSize(16) < 5)
            {
                output.Sprite(input.Sprites.Salamanders[39]);
                return;
            }

            if (input.Actor.GetStomachSize(16) >= 5 && input.Actor.GetStomachSize(16) < 8)
            {
                output.Sprite(input.Sprites.Salamanders[40]);
                return;
            }

            if (input.Actor.GetStomachSize(16) >= 8 && input.Actor.GetStomachSize(16) < 10)
            {
                output.Sprite(input.Sprites.Salamanders[41]);
                return;
            }

            if (input.Actor.GetStomachSize(16) >= 10 && input.Actor.GetStomachSize(16) < 13)
            {
                output.Sprite(input.Sprites.Salamanders[42]);
                return;
            }

            if (input.Actor.GetStomachSize(16) >= 13 && input.Actor.GetStomachSize(16) < 15)
            {
                output.Sprite(input.Sprites.Salamanders[43]);
                return;
            }

            if (input.Actor.GetStomachSize(16) >= 15)
            {
                output.Sprite(input.Sprites.Salamanders[44]);
                return;
            }

            output.Sprite(input.Sprites.Salamanders[39]);
        }); // left front leg

        builder.RenderSingle(SpriteType.BodyAccent5, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SalamanderSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.GetStomachSize(16) < 2)
            {
                output.Sprite(input.Sprites.Salamanders[45]);
                return;
            }

            if (input.Actor.GetStomachSize(16) >= 2 && input.Actor.GetStomachSize(16) < 5)
            {
                output.Sprite(input.Sprites.Salamanders[46]);
                return;
            }

            if (input.Actor.GetStomachSize(16) >= 5 && input.Actor.GetStomachSize(16) < 7)
            {
                output.Sprite(input.Sprites.Salamanders[47]);
                return;
            }

            if (input.Actor.GetStomachSize(16) >= 7 && input.Actor.GetStomachSize(16) < 9)
            {
                output.Sprite(input.Sprites.Salamanders[48]);
                return;
            }

            if (input.Actor.GetStomachSize(16) >= 9 && input.Actor.GetStomachSize(16) < 11)
            {
                output.Sprite(input.Sprites.Salamanders[49]);
                return;
            }

            if (input.Actor.GetStomachSize(16) >= 11 && input.Actor.GetStomachSize(16) < 13)
            {
                output.Sprite(input.Sprites.Salamanders[50]);
                return;
            }

            if (input.Actor.GetStomachSize(16) >= 13 && input.Actor.GetStomachSize(16) < 15)
            {
                output.Sprite(input.Sprites.Salamanders[51]);
                return;
            }

            if (input.Actor.GetStomachSize(16) >= 15)
            {
                output.Sprite(input.Sprites.Salamanders[52]);
                return;
            }

            output.Sprite(input.Sprites.Salamanders[45]);
        }); // right front leg

        builder.RenderSingle(SpriteType.BodyAccessory, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SalamanderSkin, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Salamanders[10 + input.Actor.Unit.SpecialAccessoryType]);
        }); // Backside spikes/patterns
        builder.RenderSingle(SpriteType.Belly, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SalamanderSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach))
            {
                output.Sprite(input.Sprites.Salamanders[72]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach))
            {
                if (input.Actor.GetStomachSize(16, .8f) == 16)
                {
                    output.Sprite(input.Sprites.Salamanders[71]);
                    return;
                }

                if (input.Actor.GetStomachSize(16, .9f) == 16)
                {
                    output.Sprite(input.Sprites.Salamanders[70]);
                    return;
                }
            }

            output.Sprite(input.Sprites.Salamanders[53 + input.Actor.GetStomachSize(16)]);
        });

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(Defaults.RandomCustom);
    });

    internal static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
        {
            new AnimationController.FrameList(State.Rand.Next(0, 2), 0, true)
        };
    }
}
#region

using System;

#endregion

internal static class Monitors
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        RaceFrameList frameListTongue = new RaceFrameList(new int[7] { 0, 1, 2, 1, 2, 1, 0 }, new float[7] { 0.1f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.3f });


        builder.Setup(output =>
        {
            output.DickSizes = () => 6;
            output.BreastSizes = () => 1; // (no breasts)

            output.BodySizes = 3;
            output.SpecialAccessoryCount = 7; // body pattern
            output.ClothingColors = 0;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.KomodosSkin);
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.KomodosSkin);
            output.EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);
        });


        builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Monitors[7]);
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Monitors[6]);
                return;
            }

            output.Sprite(input.Sprites.Monitors[5]);
        });

        builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Monitors[11]);
                return;
            }

            output.Sprite(input.Sprites.Monitors[10]);
        });

        builder.RenderSingle(SpriteType.Mouth, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Monitors[9]);
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Monitors[8]);
            }
        });

        builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            output.Sprite(input.Sprites.Monitors[0 + input.Actor.Unit.BodySize]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Monitors[4]);
                return;
            }

            output.Sprite(input.Sprites.Monitors[3]);
        }); // right arm

        builder.RenderSingle(SpriteType.BodyAccent2, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.SpecialAccessoryType == 6)
            {
                return;
            }

            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Monitors[19 + 7 * input.Actor.Unit.SpecialAccessoryType]);
                return;
            }

            output.Sprite(input.Sprites.Monitors[18 + 7 * input.Actor.Unit.SpecialAccessoryType]);
        }); // right arm pattern

        builder.RenderSingle(SpriteType.BodyAccent3, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.SpecialAccessoryType == 6)
            {
                return;
            }

            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Monitors[21 + 7 * input.Actor.Unit.SpecialAccessoryType]);
                return;
            }

            output.Sprite(input.Sprites.Monitors[20 + 7 * input.Actor.Unit.SpecialAccessoryType]);
        }); // head pattern

        builder.RenderSingle(SpriteType.BodyAccent4, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Monitors[12]);
        }); // claws
        builder.RenderSingle(SpriteType.BodyAccent5, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Monitors[14]);
                return;
            }

            output.Sprite(input.Sprites.Monitors[13]);
        }); // right arm claws

        builder.RenderSingle(SpriteType.BodyAccent6, 9, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (!input.Actor.Targetable)
            {
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                return;
            }

            if (input.Actor.AnimationController.frameLists[0].currentlyActive)
            {
                if (input.Actor.AnimationController.frameLists[0].currentTime >= frameListTongue.Times[input.Actor.AnimationController.frameLists[0].currentFrame])
                {
                    input.Actor.AnimationController.frameLists[0].currentFrame++;
                    input.Actor.AnimationController.frameLists[0].currentTime = 0f;

                    if (input.Actor.AnimationController.frameLists[0].currentFrame >= frameListTongue.Frames.Length)
                    {
                        input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                        input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                        input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Monitors[57 + frameListTongue.Frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                return;
            }

            if (State.Rand.Next(900) == 0)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = true;
            }
        }); // tongue

        builder.RenderSingle(SpriteType.BodyAccent7, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (Config.HideCocks)
            {
                return;
            }

            if (input.Actor.Unit.HasDick == false)
            {
                if (input.Actor.IsUnbirthing)
                {
                    output.Sprite(input.Sprites.Monitors[62]);
                    return;
                }

                return;
            }

            if (input.Actor.IsErect() || input.Actor.IsCockVoring)
            {
                output.Sprite(input.Sprites.Monitors[65]);
            }
        }); // slit inside

        builder.RenderSingle(SpriteType.BodyAccent8, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
            if (Config.HideCocks)
            {
                return;
            }

            if (input.Actor.Unit.HasDick == false)
            {
                if (input.Actor.IsUnbirthing)
                {
                    output.Sprite(input.Sprites.Monitors[61]);
                    return;
                }

                output.Sprite(input.Sprites.Monitors[60]);
                return;
            }

            if (input.Actor.IsErect() || input.Actor.IsCockVoring)
            {
                output.Sprite(input.Sprites.Monitors[64]);
                return;
            }

            output.Sprite(input.Sprites.Monitors[63]);
        }); // slit outside

        builder.RenderSingle(SpriteType.BodyAccessory, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.SpecialAccessoryType == 6)
            {
                return;
            }

            output.Sprite(input.Sprites.Monitors[15 + input.Actor.Unit.BodySize + 7 * input.Actor.Unit.SpecialAccessoryType]);
        }); // body pattern

        builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(29, 0.7f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.Monitors[153]).AddOffset(0, -13 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.Monitors[152]).AddOffset(0, -13 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 28)
                {
                    output.Sprite(input.Sprites.Monitors[151]).AddOffset(0, -13 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 27)
                {
                    output.Sprite(input.Sprites.Monitors[150]).AddOffset(0, -13 * .625f);
                    return;
                }

                switch (size)
                {
                    case 26:
                        output.AddOffset(0, -2 * .625f);
                        break;
                    case 27:
                        output.AddOffset(0, -4 * .625f);
                        break;
                    case 28:
                        output.AddOffset(0, -7 * .625f);
                        break;
                    case 29:
                        output.AddOffset(0, -11 * .625f);
                        break;
                }

                output.Sprite(input.Sprites.Monitors[120 + size]);
            }
        });

        builder.RenderSingle(SpriteType.Dick, 11, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect())
            {
                if (input.Actor.PredatorComponent?.VisibleFullness < 1.35f)
                {
                    output.Layer(20);
                    if (input.Actor.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.Monitors[72 + input.Actor.Unit.DickSize]);
                        return;
                    }

                    output.Sprite(input.Sprites.Monitors[66 + input.Actor.Unit.DickSize]);
                    return;
                }

                output.Layer(13);
                if (input.Actor.IsCockVoring)
                {
                    output.Sprite(input.Sprites.Monitors[84 + input.Actor.Unit.DickSize]);
                    return;
                }

                output.Sprite(input.Sprites.Monitors[78 + input.Actor.Unit.DickSize]);
            }

            // output.Layer(11);
        });

        builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect() && input.Actor.PredatorComponent?.VisibleFullness < 1.35f)
            {
                output.Layer(19);
            }
            else
            {
                output.Layer(10);
            }

            int offset = input.Actor.GetBallSize(28, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Monitors[119]).AddOffset(0, -27 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Monitors[118]).AddOffset(0, -27 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.Monitors[117]).AddOffset(0, -27 * .625f);
                return;
            }

            if (offset >= 26)
            {
                output.AddOffset(0, -27 * .625f);
            }
            else if (offset == 25)
            {
                output.AddOffset(0, -18 * .625f);
            }
            else if (offset == 24)
            {
                output.AddOffset(0, -16 * .625f);
            }
            else if (offset == 23)
            {
                output.AddOffset(0, -15 * .625f);
            }
            else if (offset == 22)
            {
                output.AddOffset(0, -12 * .625f);
            }
            else if (offset == 21)
            {
                output.AddOffset(0, -11 * .625f);
            }
            else if (offset == 20)
            {
                output.AddOffset(0, -9 * .625f);
            }
            else if (offset == 19)
            {
                output.AddOffset(0, -7 * .625f);
            }
            else if (offset == 18)
            {
                output.AddOffset(0, -5 * .625f);
            }
            else if (offset == 17)
            {
                output.AddOffset(0, -3 * .625f);
            }

            if (offset > 0)
            {
                output.Sprite(input.Sprites.Monitors[Math.Min(90 + offset, 116)]);
            }
        });


        builder.RunBefore(Defaults.BasicBellyRunAfter);

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.AccessoryColor = unit.SkinColor;
        });
    });

    private static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
        {
            new AnimationController.FrameList(0, 0, false)
        }; // Tongue controller. Index 0.
    }
}
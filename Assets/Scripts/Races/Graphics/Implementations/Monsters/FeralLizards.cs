internal static class FeralLizards
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        RaceFrameList frameListTongue = new RaceFrameList(new int[3] { 0, 1, 2 }, new float[3] { 0.5f, 0.2f, 0.3f });


        builder.Setup(output =>
        {
            output.SpecialAccessoryCount = 10; // body pattern
            output.BodyAccentTypes1 = 2; // teeths on/off
            output.ClothingColors = 0;
            output.GentleAnimation = true;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.KomodosSkin);
            output.EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);
        });


        builder.RenderSingle(SpriteType.Head, 11, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.FeralLizards[8]);
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.FeralLizards[7]);
                return;
            }

            output.Sprite(input.Sprites.FeralLizards[6]);
        });

        builder.RenderSingle(SpriteType.Eyes, 13, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.FeralLizards[11]);
                return;
            }

            output.Sprite(input.Sprites.FeralLizards[10]);
        });

        builder.RenderSingle(SpriteType.Mouth, 13, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.FeralLizards[13]);
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.FeralLizards[12]);
            }
        });

        builder.RenderSingle(SpriteType.Body, 10, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            output.Sprite(input.Sprites.FeralLizards[0]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.FeralLizards[1]);
        }); // legs1
        builder.RenderSingle(SpriteType.BodyAccent2, 0, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.FeralLizards[2]);
        }); // legs2
        builder.RenderSingle(SpriteType.BodyAccent3, 10, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.FeralLizards[3]);
        }); // claws1
        builder.RenderSingle(SpriteType.BodyAccent4, 0, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.FeralLizards[5]);
        }); // claws2
        builder.RenderSingle(SpriteType.BodyAccent5, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.FeralLizards[4]);
        }); // claws3
        builder.RenderSingle(SpriteType.BodyAccent6, 14, (input, output) =>
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
                if (input.Actor.AnimationController.frameLists[0].currentTime >= frameListTongue.times[input.Actor.AnimationController.frameLists[0].currentFrame])
                {
                    input.Actor.AnimationController.frameLists[0].currentFrame++;
                    input.Actor.AnimationController.frameLists[0].currentTime = 0f;

                    if (input.Actor.AnimationController.frameLists[0].currentFrame >= frameListTongue.frames.Length)
                    {
                        input.Actor.AnimationController.frameLists[0].currentlyActive = false;
                        input.Actor.AnimationController.frameLists[0].currentFrame = 0;
                        input.Actor.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.FeralLizards[87 + frameListTongue.frames[input.Actor.AnimationController.frameLists[0].currentFrame]]);
                return;
            }

            if (input.Actor.PredatorComponent?.VisibleFullness > 0 && State.Rand.Next(900) == 0)
            {
                input.Actor.AnimationController.frameLists[0].currentlyActive = true;
            }
        }); // tongue animation

        builder.RenderSingle(SpriteType.BodyAccent7, 14, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.BodyAccentType1 == 1)
            {
                return;
            }

            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.FeralLizards[86]);
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.FeralLizards[85]);
            }
        }); // teeth

        builder.RenderSingle(SpriteType.BodyAccent8, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
            if (Config.HideCocks)
            {
                return;
            }

            if (input.Actor.Unit.HasDick)
            {
                output.Sprite(input.Sprites.FeralLizards[23]);
            }
        }); // sheath

        builder.RenderSingle(SpriteType.BodyAccent9, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.FeralLizards[9]);
        }); // belly cover
        builder.RenderSingle(SpriteType.BodyAccessory, 12, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.SpecialAccessoryType == 9)
            {
                return;
            }

            output.Sprite(input.Sprites.FeralLizards[14 + input.Actor.Unit.SpecialAccessoryType]);
        }); // body pattern

        builder.RenderSingle(SpriteType.Belly, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.GetStomachSize(26) > 9)
            {
                output.Layer(9);
            }
            else
            {
                output.Layer(6);
            }

            if (input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
            {
                output.Sprite(input.Sprites.FeralLizards[54]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
            {
                if (input.Actor.GetStomachSize(26, .8f) == 26)
                {
                    output.Sprite(input.Sprites.FeralLizards[53]);
                    return;
                }

                if (input.Actor.GetStomachSize(26, .9f) == 26)
                {
                    output.Sprite(input.Sprites.FeralLizards[52]);
                    return;
                }
            }

            output.Sprite(input.Sprites.FeralLizards[25 + input.Actor.GetStomachSize(26)]);
        });

        builder.RenderSingle(SpriteType.Dick, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect())
            {
                output.Sprite(input.Sprites.FeralLizards[24]);
            }
        });

        builder.RenderSingle(SpriteType.Balls, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                {
                    output.Sprite(input.Sprites.FeralLizards[84]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    if (input.Actor.GetBallSize(26, .8f) == 26)
                    {
                        output.Sprite(input.Sprites.FeralLizards[83]);
                        return;
                    }

                    if (input.Actor.GetBallSize(26, .9f) == 26)
                    {
                        output.Sprite(input.Sprites.FeralLizards[82]);
                        return;
                    }
                }

                output.Sprite(input.Sprites.FeralLizards[55 + input.Actor.GetBallSize(26)]);
                return;
            }

            output.Sprite(input.Sprites.FeralLizards[55]);
        });


        builder.RunBefore((input, output) =>
        {
            output.changeSprite(SpriteType.Balls).AddOffset(-30 * .625f, 0);
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.BodyAccentType1 = 0;
        });
    });

    internal static void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new[]
        {
            new AnimationController.FrameList(0, 0, false)
        }; // Tongue controller. Index 0.
    }
}
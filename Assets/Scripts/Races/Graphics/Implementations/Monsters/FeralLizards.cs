using System.Collections.Generic;

internal static class FeralLizards
{
    internal static readonly IRaceData Instance = RaceBuilder.CreateV2(Defaults.Blank, builder =>
    {
        RaceFrameList frameListTongue = new RaceFrameList(new int[3] { 0, 1, 2 }, new float[3] { 0.5f, 0.2f, 0.3f });


        builder.Setup(output =>
        {
            output.Names("Feral Lizard", "Feral Lizards");
            output.RaceTraits(new RaceTraits()
            {
                BodySize = 17,
                StomachSize = 16,
                HasTail = true,
                FavoredStat = Stat.Strength,
                AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth, VoreType.CockVore },
                ExpMultiplier = 1.5f,
                PowerAdjustment = 1.75f,
                RaceStats = new RaceStats()
                {
                    Strength = new RaceStats.StatRange(12, 20),
                    Dexterity = new RaceStats.StatRange(8, 16),
                    Endurance = new RaceStats.StatRange(10, 18),
                    Mind = new RaceStats.StatRange(8, 16),
                    Will = new RaceStats.StatRange(8, 16),
                    Agility = new RaceStats.StatRange(8, 16),
                    Voracity = new RaceStats.StatRange(10, 18),
                    Stomach = new RaceStats.StatRange(8, 16),
                },
                RacialTraits = new List<Traits>()
                {
                    Traits.Intimidating,
                    Traits.Biter,
                    Traits.Resilient
                },
                RaceDescription = ""
            });
            output.CustomizeButtons((unit, buttons) =>
            {
                buttons.SetText(ButtonType.BodyAccessoryType, "Body Pattern Type");
                buttons.SetText(ButtonType.BodyAccentTypes1, "Visible Teeth (during attacks)");
            });
            output.SpecialAccessoryCount = 10; // body pattern
            output.BodyAccentTypes1 = 2; // teeths on/off
            output.ClothingColors = 0;
            output.GentleAnimation = true;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.KomodosSkin);
            output.EyeColors = ColorPaletteMap.GetPaletteCount(SwapType.EyeColor);
        });


        builder.RenderSingle(SpriteType.Head, 11, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
            if (input.A.IsOralVoring)
            {
                output.Sprite(input.Sprites.FeralLizards[8]);
                return;
            }

            if (input.A.IsAttacking || input.A.IsEating)
            {
                output.Sprite(input.Sprites.FeralLizards[7]);
                return;
            }

            output.Sprite(input.Sprites.FeralLizards[6]);
        });

        builder.RenderSingle(SpriteType.Eyes, 13, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
            if (input.A.IsAttacking || input.A.IsEating)
            {
                output.Sprite(input.Sprites.FeralLizards[11]);
                return;
            }

            output.Sprite(input.Sprites.FeralLizards[10]);
        });

        builder.RenderSingle(SpriteType.Mouth, 13, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.A.IsOralVoring)
            {
                output.Sprite(input.Sprites.FeralLizards[13]);
                return;
            }

            if (input.A.IsAttacking || input.A.IsEating)
            {
                output.Sprite(input.Sprites.FeralLizards[12]);
            }
        });

        builder.RenderSingle(SpriteType.Body, 10, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
            if (input.A.AnimationController.frameLists == null)
            {
                SetUpAnimations(input.Actor);
            }

            output.Sprite(input.Sprites.FeralLizards[0]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
            output.Sprite(input.Sprites.FeralLizards[1]);
        }); // legs1
        builder.RenderSingle(SpriteType.BodyAccent2, 0, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
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
            if (!input.A.Targetable)
            {
                return;
            }

            if (input.A.IsAttacking || input.A.IsEating)
            {
                input.A.AnimationController.frameLists[0].currentlyActive = false;
                input.A.AnimationController.frameLists[0].currentFrame = 0;
                input.A.AnimationController.frameLists[0].currentTime = 0f;
                return;
            }

            if (input.A.AnimationController.frameLists[0].currentlyActive)
            {
                if (input.A.AnimationController.frameLists[0].currentTime >= frameListTongue.Times[input.A.AnimationController.frameLists[0].currentFrame])
                {
                    input.A.AnimationController.frameLists[0].currentFrame++;
                    input.A.AnimationController.frameLists[0].currentTime = 0f;

                    if (input.A.AnimationController.frameLists[0].currentFrame >= frameListTongue.Frames.Length)
                    {
                        input.A.AnimationController.frameLists[0].currentlyActive = false;
                        input.A.AnimationController.frameLists[0].currentFrame = 0;
                        input.A.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.FeralLizards[87 + frameListTongue.Frames[input.A.AnimationController.frameLists[0].currentFrame]]);
                return;
            }

            if (input.A.PredatorComponent?.VisibleFullness > 0 && State.Rand.Next(900) == 0)
            {
                input.A.AnimationController.frameLists[0].currentlyActive = true;
            }
        }); // tongue animation

        builder.RenderSingle(SpriteType.BodyAccent7, 14, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.U.BodyAccentType1 == 1)
            {
                return;
            }

            if (input.A.IsOralVoring)
            {
                output.Sprite(input.Sprites.FeralLizards[86]);
                return;
            }

            if (input.A.IsAttacking || input.A.IsEating)
            {
                output.Sprite(input.Sprites.FeralLizards[85]);
            }
        }); // teeth

        builder.RenderSingle(SpriteType.BodyAccent8, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
            if (Config.HideCocks)
            {
                return;
            }

            if (input.U.HasDick)
            {
                output.Sprite(input.Sprites.FeralLizards[23]);
            }
        }); // sheath

        builder.RenderSingle(SpriteType.BodyAccent9, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
            output.Sprite(input.Sprites.FeralLizards[9]);
        }); // belly cover
        builder.RenderSingle(SpriteType.BodyAccessory, 12, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
            if (input.U.SpecialAccessoryType == 9)
            {
                return;
            }

            output.Sprite(input.Sprites.FeralLizards[14 + input.U.SpecialAccessoryType]);
        }); // body pattern

        builder.RenderSingle(SpriteType.Belly, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
            output.Layer(input.A.GetStomachSize(26) > 9 ? 9 : 6);

            if (input.A.HasBelly == false)
            {
                return;
            }

            output.Sprite(input.Sprites.FeralLizards[25 + input.A.GetStomachSize(26)]);
        });

        builder.RenderSingle(SpriteType.Dick, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.U.HasDick == false)
            {
                return;
            }

            if (input.A.IsErect())
            {
                output.Sprite(input.Sprites.FeralLizards[24]);
            }
        });

        builder.RenderSingle(SpriteType.Balls, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
            if (input.U.HasDick == false)
            {
                return;
            }

            if (input.A.PredatorComponent?.BallsFullness > 0)
            {
                output.Sprite(input.Sprites.FeralLizards[55 + input.A.GetBallSize(26)]);
                return;
            }

            output.Sprite(input.Sprites.FeralLizards[55]);
        });


        builder.RunBefore((input, output) =>
        {
            output.ChangeSprite(SpriteType.Balls).AddOffset(-30 * .625f, 0);
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.BodyAccentType1 = 0;
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
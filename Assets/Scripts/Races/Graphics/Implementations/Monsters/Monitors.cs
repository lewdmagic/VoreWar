#region

using System;
using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class Monitors
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            RaceFrameList frameListTongue = new RaceFrameList(new int[7] { 0, 1, 2, 1, 2, 1, 0 }, new float[7] { 0.1f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.3f });


            builder.Setup(output =>
            {
                output.Names("Monitor", "Monitors");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 17,
                    StomachSize = 17,
                    HasTail = true,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth, VoreType.CockVore },
                    PowerAdjustment = 1.3f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(12, 20),
                        Dexterity = new RaceStats.StatRange(6, 10),
                        Endurance = new RaceStats.StatRange(12, 20),
                        Mind = new RaceStats.StatRange(6, 12),
                        Will = new RaceStats.StatRange(8, 16),
                        Agility = new RaceStats.StatRange(10, 16),
                        Voracity = new RaceStats.StatRange(16, 24),
                        Stomach = new RaceStats.StatRange(12, 18),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.HardSkin,
                        TraitType.Resilient,
                        TraitType.VenomShock,
                    },
                    RaceDescription = "",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.BodyAccessoryType, "Body Pattern Type");
                    buttons.SetText(ButtonType.BodyAccessoryColor, "Body Pattern Colors");
                });
                output.DickSizes = () => 6;
                output.BreastSizes = () => 1; // (no breasts)

                output.BodySizes = 3;
                output.SpecialAccessoryCount = 7; // body pattern
                output.ClothingColors = 0;
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.KomodosSkin);
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.KomodosSkin);
                output.EyeColors = ColorPaletteMap.GetPaletteCount(SwapType.EyeColor);
            });


            builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Monitors[7]);
                    return;
                }

                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Monitors[6]);
                    return;
                }

                output.Sprite(input.Sprites.Monitors[5]);
            });

            builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Monitors[11]);
                    return;
                }

                output.Sprite(input.Sprites.Monitors[10]);
            });

            builder.RenderSingle(SpriteType.Mouth, 8, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Monitors[9]);
                    return;
                }

                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Monitors[8]);
                }
            });

            builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
                if (input.A.AnimationController.FrameLists == null)
                {
                    SetUpAnimations(input.Actor);
                }

                output.Sprite(input.Sprites.Monitors[0 + input.U.BodySize]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Monitors[4]);
                    return;
                }

                output.Sprite(input.Sprites.Monitors[3]);
            }); // right arm

            builder.RenderSingle(SpriteType.BodyAccent2, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.AccessoryColor));
                if (input.U.SpecialAccessoryType == 6)
                {
                    return;
                }

                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Monitors[19 + 7 * input.U.SpecialAccessoryType]);
                    return;
                }

                output.Sprite(input.Sprites.Monitors[18 + 7 * input.U.SpecialAccessoryType]);
            }); // right arm pattern

            builder.RenderSingle(SpriteType.BodyAccent3, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.AccessoryColor));
                if (input.U.SpecialAccessoryType == 6)
                {
                    return;
                }

                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Monitors[21 + 7 * input.U.SpecialAccessoryType]);
                    return;
                }

                output.Sprite(input.Sprites.Monitors[20 + 7 * input.U.SpecialAccessoryType]);
            }); // head pattern

            builder.RenderSingle(SpriteType.BodyAccent4, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Monitors[12]);
            }); // claws
            builder.RenderSingle(SpriteType.BodyAccent5, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Monitors[14]);
                    return;
                }

                output.Sprite(input.Sprites.Monitors[13]);
            }); // right arm claws

            builder.RenderSingle(SpriteType.BodyAccent6, 9, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (!input.A.Targetable)
                {
                    return;
                }

                if (input.A.IsAttacking || input.A.IsEating)
                {
                    input.A.AnimationController.FrameLists[0].CurrentlyActive = false;
                    input.A.AnimationController.FrameLists[0].CurrentFrame = 0;
                    input.A.AnimationController.FrameLists[0].CurrentTime = 0f;
                    return;
                }

                if (input.A.AnimationController.FrameLists[0].CurrentlyActive)
                {
                    if (input.A.AnimationController.FrameLists[0].CurrentTime >= frameListTongue.Times[input.A.AnimationController.FrameLists[0].CurrentFrame])
                    {
                        input.A.AnimationController.FrameLists[0].CurrentFrame++;
                        input.A.AnimationController.FrameLists[0].CurrentTime = 0f;

                        if (input.A.AnimationController.FrameLists[0].CurrentFrame >= frameListTongue.Frames.Length)
                        {
                            input.A.AnimationController.FrameLists[0].CurrentlyActive = false;
                            input.A.AnimationController.FrameLists[0].CurrentFrame = 0;
                            input.A.AnimationController.FrameLists[0].CurrentTime = 0f;
                        }
                    }

                    output.Sprite(input.Sprites.Monitors[57 + frameListTongue.Frames[input.A.AnimationController.FrameLists[0].CurrentFrame]]);
                    return;
                }

                if (State.Rand.Next(900) == 0)
                {
                    input.A.AnimationController.FrameLists[0].CurrentlyActive = true;
                }
            }); // tongue

            builder.RenderSingle(SpriteType.BodyAccent7, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (Config.HideCocks)
                {
                    return;
                }

                if (input.U.HasDick == false)
                {
                    if (input.A.IsUnbirthing)
                    {
                        output.Sprite(input.Sprites.Monitors[62]);
                        return;
                    }

                    return;
                }

                if (input.A.IsErect() || input.A.IsCockVoring)
                {
                    output.Sprite(input.Sprites.Monitors[65]);
                }
            }); // slit inside

            builder.RenderSingle(SpriteType.BodyAccent8, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
                if (Config.HideCocks)
                {
                    return;
                }

                if (input.U.HasDick == false)
                {
                    if (input.A.IsUnbirthing)
                    {
                        output.Sprite(input.Sprites.Monitors[61]);
                        return;
                    }

                    output.Sprite(input.Sprites.Monitors[60]);
                    return;
                }

                if (input.A.IsErect() || input.A.IsCockVoring)
                {
                    output.Sprite(input.Sprites.Monitors[64]);
                    return;
                }

                output.Sprite(input.Sprites.Monitors[63]);
            }); // slit outside

            builder.RenderSingle(SpriteType.BodyAccessory, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.AccessoryColor));
                if (input.U.SpecialAccessoryType == 6)
                {
                    return;
                }

                output.Sprite(input.Sprites.Monitors[15 + input.U.BodySize + 7 * input.U.SpecialAccessoryType]);
            }); // body pattern

            builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
                if (input.A.HasBelly)
                {
                    int size = input.A.GetStomachSize(29, 0.7f);

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
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect())
                {
                    if (input.A.PredatorComponent?.VisibleFullness < 1.35f)
                    {
                        output.Layer(20);
                        if (input.A.IsCockVoring)
                        {
                            output.Sprite(input.Sprites.Monitors[72 + input.U.DickSize]);
                            return;
                        }

                        output.Sprite(input.Sprites.Monitors[66 + input.U.DickSize]);
                        return;
                    }

                    output.Layer(13);
                    if (input.A.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.Monitors[84 + input.U.DickSize]);
                        return;
                    }

                    output.Sprite(input.Sprites.Monitors[78 + input.U.DickSize]);
                }

                // output.Layer(11);
            });

            builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect() && input.A.PredatorComponent?.VisibleFullness < 1.35f)
                {
                    output.Layer(19);
                }
                else
                {
                    output.Layer(10);
                }

                int offset = input.A.GetBallSize(28, .8f);

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
                IUnitRead unit = data.Unit;

                unit.AccessoryColor = unit.SkinColor;
            });
        });

        private static void SetUpAnimations(IActorUnit actor)
        {
            actor.AnimationController.FrameLists = new[]
            {
                new AnimationController.FrameList(0, 0, false)
            }; // Tongue controller. Index 0.
        }
    }
}
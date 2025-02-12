﻿#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class FeralBats
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            RaceFrameList frameListWings = new RaceFrameList(new int[2] { 0, 1 }, new float[2] { .2f, .2f });

            builder.Setup((input, output) =>
            {
                output.Names("Feral Bat", "Feral Bats");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 12,
                    StomachSize = 12,
                    HasTail = false,
                    FavoredStat = Stat.Agility,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth, VoreType.CockVore },
                    ExpMultiplier = 1.2f,
                    PowerAdjustment = 1.5f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new StatRange(8, 12),
                        Dexterity = new StatRange(12, 16),
                        Endurance = new StatRange(8, 12),
                        Mind = new StatRange(6, 8),
                        Will = new StatRange(8, 12),
                        Agility = new StatRange(12, 16),
                        Voracity = new StatRange(10, 14),
                        Stomach = new StatRange(8, 12),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Flight,
                        TraitType.EvasiveBattler
                    },
                    RaceDescription = "A species with large difference in size between genders, the male bats being barely half the female's size. This has led many to believe that the tendency of the females to hunt both for sustenance and pleasure is due to the males being unable to satisfy some of the female's needs."
                });
                output.DickSizes = () => 1;
                output.BreastSizes = () => 1;

                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.Bat);
                output.CanBeGender = new List<Gender> { Gender.Female, Gender.Male };
            });

            builder.RenderSingle(SpriteType.Head, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Bat, input.U.SkinColor));
                if (input.A.IsOralVoring || input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Bat[4]);
                    return;
                }

                if (input.A.HasBelly)
                {
                    output.Sprite(input.Sprites.Bat[3]);
                    return;
                }

                output.Sprite(input.Sprites.Bat[2]);
            }); // Head

            builder.RenderSingle(SpriteType.Body, 0, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Bat, input.U.SkinColor));
                if (input.A.AnimationController.FrameLists == null)
                {
                    SetUpAnimations(input.Actor);
                }

                if (input.A.IsUnbirthing || input.A.IsAnalVoring)
                {
                    output.Sprite(input.Sprites.Bat[1]);
                    return;
                }

                output.Sprite(input.Sprites.Bat[0]);
            }); // Body

            builder.RenderSingle(SpriteType.BodyAccent, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Bat, input.U.SkinColor));
                if (input.A.AnimationController.FrameLists[0].CurrentTime >= frameListWings.Times[input.A.AnimationController.FrameLists[0].CurrentFrame] && input.U.IsDead == false)
                {
                    input.A.AnimationController.FrameLists[0].CurrentFrame++;
                    input.A.AnimationController.FrameLists[0].CurrentTime = 0f;

                    if (input.A.AnimationController.FrameLists[0].CurrentFrame >= frameListWings.Frames.Length)
                    {
                        input.A.AnimationController.FrameLists[0].CurrentFrame = 0;
                        input.A.AnimationController.FrameLists[0].CurrentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Bat[5 + frameListWings.Frames[input.A.AnimationController.FrameLists[0].CurrentFrame]]);
            }); // Wings

            builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Bat, input.U.SkinColor));
                if (!input.U.HasDick)
                {
                    if (input.A.IsAnalVoring)
                    {
                        output.Sprite(input.Sprites.Bat[7]);
                        return;
                    }

                    if (input.A.IsUnbirthing)
                    {
                        output.Sprite(input.Sprites.Bat[9]);
                        return;
                    }

                    output.Sprite(input.Sprites.Bat[8]);
                }
            }); // Privates

            builder.RenderSingle(SpriteType.Belly, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Bat, input.U.SkinColor));
                if (input.U.Predator == false)
                {
                    return;
                }

                if (input.A.HasBelly)
                {
                    int sprite = input.A.GetStomachSize(22);

                    if (sprite >= 15)
                    {
                        output.Sprite(input.Sprites.Bat[23]);
                        return;
                    }

                    output.Sprite(input.Sprites.Bat[9 + sprite]);
                }
            }); // Belly

            builder.RenderSingle(SpriteType.Dick, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Bat, input.U.SkinColor));
                if (input.U.HasDick)
                {
                    if (input.A.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.Bat[29]);
                        return;
                    }

                    if (input.A.IsErect())
                    {
                        output.Sprite(input.Sprites.Bat[30]);
                        return;
                    }

                    output.Sprite(input.Sprites.Bat[28]);
                }
            }); // Dick        

            builder.RenderSingle(SpriteType.Balls, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Bat, input.U.SkinColor));
                if (input.U.HasDick)
                {
                    if (input.U.Predator == false)
                    {
                        output.Sprite(input.Sprites.Bat[31]);
                        return;
                    }

                    if (input.A.PredatorComponent.BallsFullness <= 0)
                    {
                        output.Sprite(input.Sprites.Bat[28]);
                        return;
                    }

                    int sprite = input.A.GetBallSize(21);

                    if (sprite >= 15)
                    {
                        output.Sprite(input.Sprites.Bat[46]);
                        return;
                    }

                    output.Sprite(input.Sprites.Bat[31 + sprite]);
                }
            }); // Balls

            builder.RunBefore(Defaults.BasicBellyRunAfter);

            builder.RandomCustom((data, output) =>   
            {
                IUnitRead unit = data.Unit;
                unit.SkinColor = State.Rand.Next(0, data.SetupOutput.SkinColors);
            });
        });

        private static void SetUpAnimations(IActorUnit actor)
        {
            actor.AnimationController.FrameLists = new[]
            {
                new AnimationController.FrameList(State.Rand.Next(0, 2), 0, true)
            }; // Wing controller. Index 0.
        }
    }
}
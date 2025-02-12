﻿using System.Collections.Generic;

namespace Races.Graphics.Implementations.Monsters
{
    internal static class Dragon
    {
        internal enum Position
        {
            Down,
            Standing,
            StandingCrouch
        }

        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup((input, output) =>
            {
                output.Names("Dragon", "Dragons");
                output.BonesInfo((unit) => new List<BoneInfo>()
                {
                    new BoneInfo(BoneType.WyvernBonesWithoutHead, unit.Name)
                });
                output.FlavorText(new FlavorText(
                    new Texts { "formerly apex predator", "delicious dragon", "ex-predator" },
                    new Texts { "apex predator", "hungry dragon", "voracious dragon" },
                    new Texts { "dragon", "draconian", { "dragoness", Gender.Female }, { "drakon", Gender.Male } }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 100,
                    StomachSize = 80,
                    HasTail = true,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth, VoreType.CockVore },
                    ExpMultiplier = 6f,
                    PowerAdjustment = 12f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new StatRange(24, 32),
                        Dexterity = new StatRange(10, 18),
                        Endurance = new StatRange(30, 42),
                        Mind = new StatRange(24, 32),
                        Will = new StatRange(12, 24),
                        Agility = new StatRange(16, 22),
                        Voracity = new StatRange(20, 28),
                        Stomach = new StatRange(12, 24),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Flight,
                        TraitType.Maul,
                        TraitType.Greedy,
                        TraitType.Cruel,
                        TraitType.AdeptLearner,
                    },
                    RaceDescription = ""
                });
                output.GentleAnimation = true;
                output.SpecialAccessoryCount = 3;

                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.Dragon);
                output.ClothingColors = 0;
            });

            builder.RenderSingle(SpriteType.Head, 8, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Dragon, input.U.AccessoryColor));
                switch (CalcPosition(input.A))
                {
                    case Position.Down:
                        output.Sprite(input.Sprites.Dragon[3]);
                        return;
                    case Position.Standing:
                        if (input.A.IsOralVoring)
                        {
                            output.Sprite(input.Sprites.Dragon[5]);
                            return;
                        }

                        output.Sprite(input.Sprites.Dragon[4]);
                        return;
                    case Position.StandingCrouch:
                        if (input.A.IsOralVoring)
                        {
                            output.Sprite(input.Sprites.Dragon[7]);
                            return;
                        }

                        output.Sprite(input.Sprites.Dragon[6]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Dragon, input.U.AccessoryColor));
                switch (CalcPosition(input.A))
                {
                    case Position.Down:
                        output.Sprite(input.Sprites.Dragon[0]);
                        return;
                    case Position.Standing:
                        output.Sprite(input.Sprites.Dragon[1]);
                        return;
                    case Position.StandingCrouch:
                        output.Sprite(input.Sprites.Dragon[2]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent, 5, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                switch (CalcPosition(input.A))
                {
                    case Position.Down:
                        output.Sprite(input.Sprites.Dragon[11]);
                        return;
                    case Position.Standing:
                        output.Sprite(input.Sprites.Dragon[8]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Dragon[9]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent2, 17, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                switch (CalcPosition(input.A))
                {
                    case Position.Standing:
                        if (input.A.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Dragon[13]);
                            return;
                        }

                        output.Sprite(input.Sprites.Dragon[12]);
                        return;
                    case Position.StandingCrouch:
                        if (input.A.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Dragon[15]);
                            return;
                        }

                        output.Sprite(input.Sprites.Dragon[14]);
                        return;
                    default:
                        return;
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent3, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (CalcPosition(input.A) == Position.Down)
                {
                    output.Sprite(input.Sprites.Dragon[10]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent4, 17, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Dragon, input.U.AccessoryColor));
                if (CalcPosition(input.A) == Position.Down)
                {
                    return;
                }

                if (CalcPosition(input.A) == Position.Standing)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Dragon[17]);
                        return;
                    }

                    output.Sprite(input.Sprites.Dragon[16]);
                    return;
                }

                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Dragon[19]);
                    return;
                }

                output.Sprite(input.Sprites.Dragon[18]);
            });

            builder.RenderSingle(SpriteType.BodyAccent5, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Dragon, input.U.AccessoryColor));
                switch (CalcPosition(input.A))
                {
                    case Position.Standing:
                        if (input.A.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Dragon[37]);
                            return;
                        }

                        output.Sprite(input.Sprites.Dragon[36]);
                        return;
                    case Position.StandingCrouch:
                        if (input.A.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Dragon[38]);
                            return;
                        }

                        output.Sprite(input.Sprites.Dragon[39]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Dragon[35]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent6, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Dragon, input.U.AccessoryColor));
                switch (CalcPosition(input.A))
                {
                    case Position.Standing:
                        output.Sprite(input.Sprites.Dragon[41]);
                        return;
                    case Position.StandingCrouch:
                        output.Sprite(input.Sprites.Dragon[42]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Dragon[40]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent7, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Dragon, input.U.AccessoryColor));
                switch (CalcPosition(input.A))
                {
                    case Position.Standing:
                        output.Sprite(input.Sprites.Dragon[44]);
                        return;
                    case Position.StandingCrouch:
                        output.Sprite(input.Sprites.Dragon[45]);
                        return;
                    default:
                        return;
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent8, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Dragon, input.U.AccessoryColor));
                if (CalcPosition(input.A) == Position.Standing)
                {
                    output.Sprite(input.Sprites.Dragon[49]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccessory, 9, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Dragon, input.U.AccessoryColor));
                int sprite = 20 + 5 * input.U.SpecialAccessoryType;
                switch (CalcPosition(input.A))
                {
                    case Position.Standing:
                        if (input.A.IsOralVoring)
                        {
                            output.Sprite(input.Sprites.Dragon[sprite + 1]);
                            return;
                        }

                        output.Sprite(input.Sprites.Dragon[sprite + 2]);
                        return;
                    case Position.StandingCrouch:
                        if (input.A.IsOralVoring)
                        {
                            output.Sprite(input.Sprites.Dragon[sprite + 3]);
                            return;
                        }

                        output.Sprite(input.Sprites.Dragon[sprite + 4]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Dragon[sprite]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.BodySize, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Dragon, input.U.AccessoryColor));
                switch (CalcPosition(input.A))
                {
                    case Position.Standing:
                        output.Sprite(input.Sprites.Dragon[47]);
                        return;
                    case Position.StandingCrouch:
                        output.Sprite(input.Sprites.Dragon[48]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Dragon[46]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Dragon, input.U.AccessoryColor));
                if (input.U.Predator == false || input.A.HasBelly == false)
                {
                    return;
                }

                if (CalcPosition(input.A) == Position.Standing || CalcPosition(input.A) == Position.StandingCrouch)
                {
                    output.Layer(16);

                    output.Sprite(input.Sprites.Dragon[50 + input.A.GetStomachSize(16, 1.75f)]);
                }
            });

            builder.RenderSingle(SpriteType.Dick, 15, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Dragon, input.U.AccessoryColor));
                if (CalcPosition(input.A) == Position.Down)
                {
                    return;
                }

                if (input.A.GetStomachSize(16) > 1)
                {
                    return;
                }

                if (input.U.DickSize >= 0)
                {
                    output.Sprite(input.Sprites.Dragon[73 + input.U.DickSize]);
                    return;
                }

                if (input.A.IsUnbirthing)
                {
                    output.Sprite(input.Sprites.Dragon[72]);
                    return;
                }

                output.Sprite(input.Sprites.Dragon[71]);
            });

            builder.RenderSingle(SpriteType.Balls, 11, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Dragon, input.U.AccessoryColor));
                if (input.U.HasDick == false || CalcPosition(input.A) == Position.Down)
                {
                    return;
                }

                if (input.A.PredatorComponent?.BallsFullness > 0)
                {
                    output.AddOffset(0, 1 * .625f);

                    output.Sprite(input.Sprites.Dragon[75 + input.A.GetBallSize(13, 1.75f)]);
                    return;
                }

                output.Sprite(input.Sprites.Dragon[75]);
            });


            builder.RunBefore((input, output) =>
            {
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.ChangeSprite(SpriteType.Belly).AddOffset(0, 14 * .625f);
                }
            });

            builder.RandomCustom(Defaults.Randomize);
        });

        private static Position CalcPosition(IActorUnit actor)
        {
            if (actor.IsAttacking || actor.IsEating)
            {
                return Position.StandingCrouch;
            }
            else if (actor.HasBelly || actor.PredatorComponent?.BallsFullness > 0)
            {
                return Position.Standing;
            }
            else
            {
                return Position.Down;
            }
        }
    }
}
using System.Collections.Generic;

namespace Races.Graphics.Implementations.Monsters
{
    internal static class Gryphons
    {
        internal class PositionParameters : IParameters
        {
            internal Position Position;
        }

        internal enum Position
        {
            Standing,
            Sitting
        }

        private static Position CalcPosition(IActorUnit actor)
        {
            if (actor.HasBelly || actor.PredatorComponent?.BallsFullness > 0)
            {
                return Position.Sitting;
            }
            else
            {
                return Position.Standing;
            }
        }

        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Gryphon", "Gryphons");
                output.FlavorText(new FlavorText(
                    new Texts { },
                    new Texts { },
                    new Texts { "gryphon", "griffin", "griffon" }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 20,
                    StomachSize = 22,
                    HasTail = true,
                    FavoredStat = Stat.Agility,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth, VoreType.CockVore },
                    ExpMultiplier = 1.75f,
                    PowerAdjustment = 2.5f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(8, 18),
                        Dexterity = new RaceStats.StatRange(8, 16),
                        Endurance = new RaceStats.StatRange(12, 16),
                        Mind = new RaceStats.StatRange(8, 16),
                        Will = new RaceStats.StatRange(8, 16),
                        Agility = new RaceStats.StatRange(12, 20),
                        Voracity = new RaceStats.StatRange(8, 16),
                        Stomach = new RaceStats.StatRange(8, 14),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Flight,
                        TraitType.Intimidating,
                        TraitType.Charge,
                        TraitType.Greedy,
                        TraitType.Pathfinder,
                    },
                    RaceDescription = ""
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.Skintone, "Body Color");
                    buttons.SetText(ButtonType.BodyAccessoryType, "Body Style");
                });
                output.IndividualNames(new List<string>
                {
                    "Aquila",
                    "Harpia",
                    "Accipiter",
                    "Kirkos",
                    "Cathartes",
                    "Necrosyrtes",
                    "Neophron",
                    "Sarcogyps",
                    "Elanus",
                    "Milvus",
                    "Haliastur",
                    "Pandion",
                    "Buteo",
                    "Falco",
                    "Harpagus",
                    "Milvago",
                    "Caracara",
                    "Ibycter",
                    "Daptrius",
                    "Ictinia",
                    "Minerva",
                    "Aegolius",
                    "Sagittarius",
                    "Lanius",
                    "Vultur",
                    "Surnia",
                    "Strix",
                    "Pulsatrix",
                    "Ninox",
                    "Ealonides",
                    "Dryotriorchis",
                    "Casuarius",
                });
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.GryphonSkin);
                output.GentleAnimation = true;

                output.SpecialAccessoryCount = 2;
                output.ClothingColors = 0;
            });

            builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.SpecialAccessoryType == 0)
                {
                    switch (CalcPosition(input.A))
                    {
                        case Position.Standing:
                            if (input.A.IsOralVoring || input.A.IsAttacking)
                            {
                                output.Sprite(input.Sprites.Gryphon[11]);
                                return;
                            }

                            output.Sprite(input.Sprites.Gryphon[10]);
                            return;
                        case Position.Sitting:
                            if (input.A.IsOralVoring || input.A.IsAttacking)
                            {
                                output.Sprite(input.Sprites.Gryphon[13]);
                                return;
                            }

                            output.Sprite(input.Sprites.Gryphon[12]);
                            return;
                    }

                    return;
                }

                switch (CalcPosition(input.A))
                {
                    case Position.Standing:
                        if (input.A.IsOralVoring || input.A.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Griffin[11]);
                            return;
                        }

                        output.Sprite(input.Sprites.Griffin[10]);
                        return;
                    case Position.Sitting:
                        if (input.A.IsOralVoring || input.A.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Griffin[13]);
                            return;
                        }

                        output.Sprite(input.Sprites.Griffin[12]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.GryphonSkin, input.U.SkinColor));
                if (input.U.SpecialAccessoryType == 0)
                {
                    switch (CalcPosition(input.A))
                    {
                        case Position.Standing:
                            if (input.A.IsOralVoring || input.A.IsAttacking)
                            {
                                output.Sprite(input.Sprites.Gryphon[1]);
                                return;
                            }

                            output.Sprite(input.Sprites.Gryphon[0]);
                            return;
                        case Position.Sitting:
                            if (input.A.IsOralVoring || input.A.IsAttacking)
                            {
                                output.Sprite(input.Sprites.Gryphon[3]);
                                return;
                            }

                            output.Sprite(input.Sprites.Gryphon[2]);
                            return;
                    }

                    return;
                }

                switch (CalcPosition(input.A))
                {
                    case Position.Standing:
                        if (input.A.IsOralVoring || input.A.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Griffin[1]);
                            return;
                        }

                        output.Sprite(input.Sprites.Griffin[0]);
                        return;
                    case Position.Sitting:
                        if (input.A.IsOralVoring || input.A.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Griffin[3]);
                            return;
                        }

                        output.Sprite(input.Sprites.Griffin[2]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.GryphonSkin, input.U.SkinColor));
                if (input.U.SpecialAccessoryType == 0)
                {
                    switch (CalcPosition(input.A))
                    {
                        case Position.Standing:
                            if (input.A.IsOralVoring || input.A.IsAttacking)
                            {
                                output.Sprite(input.Sprites.Gryphon[7]);
                                return;
                            }

                            output.Sprite(input.Sprites.Gryphon[5]);
                            return;
                        case Position.Sitting:
                            output.Sprite(input.Sprites.Gryphon[9]);
                            return;
                        default:
                            output.Sprite(input.Sprites.Gryphon[5]);
                            return;
                    }
                }

                switch (CalcPosition(input.A))
                {
                    case Position.Standing:
                        if (input.A.IsOralVoring || input.A.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Griffin[7]);
                            return;
                        }

                        output.Sprite(input.Sprites.Griffin[5]);
                        return;
                    case Position.Sitting:
                        output.Sprite(input.Sprites.Griffin[9]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Griffin[5]);
                        return;
                }
            }); // right wing

            builder.RenderSingle(SpriteType.BodyAccent2, 17, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.GryphonSkin, input.U.SkinColor));
                if (input.U.SpecialAccessoryType == 0)
                {
                    switch (CalcPosition(input.A))
                    {
                        case Position.Standing:
                            if (input.A.IsOralVoring || input.A.IsAttacking)
                            {
                                output.Sprite(input.Sprites.Gryphon[6]);
                                return;
                            }

                            output.Sprite(input.Sprites.Gryphon[4]);
                            return;
                        case Position.Sitting:
                            output.Sprite(input.Sprites.Gryphon[8]);
                            return;
                        default:
                            output.Sprite(input.Sprites.Gryphon[4]);
                            return;
                    }
                }

                switch (CalcPosition(input.A))
                {
                    case Position.Standing:
                        if (input.A.IsOralVoring || input.A.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Griffin[6]);
                            return;
                        }

                        output.Sprite(input.Sprites.Griffin[4]);
                        return;
                    case Position.Sitting:
                        output.Sprite(input.Sprites.Griffin[8]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Griffin[4]);
                        return;
                }
            }); // left wing

            builder.RenderSingle(SpriteType.BodyAccent3, 14, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.GryphonSkin, input.U.SkinColor));
                if (input.U.SpecialAccessoryType == 0)
                {
                    if (CalcPosition(input.A) == Position.Sitting)
                    {
                        output.Sprite(input.Sprites.Gryphon[14]);
                        return;
                    }

                    return;
                }

                if (CalcPosition(input.A) == Position.Sitting)
                {
                    output.Sprite(input.Sprites.Griffin[14]);
                }
            }); // left side legs (only sitting)

            builder.RenderSingle(SpriteType.BodyAccent4, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.SpecialAccessoryType == 0)
                {
                    switch (CalcPosition(input.A))
                    {
                        case Position.Standing:
                            output.Sprite(input.Sprites.Gryphon[15]);
                            return;
                        case Position.Sitting:
                            output.Sprite(input.Sprites.Gryphon[17]);
                            return;
                        default:
                            output.Sprite(input.Sprites.Gryphon[15]);
                            return;
                    }
                }

                switch (CalcPosition(input.A))
                {
                    case Position.Standing:
                        output.Sprite(input.Sprites.Griffin[15]);
                        return;
                    case Position.Sitting:
                        output.Sprite(input.Sprites.Griffin[17]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Griffin[15]);
                        return;
                }
            }); // right claw (or both in standing)

            builder.RenderSingle(SpriteType.BodyAccent5, 15, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.SpecialAccessoryType == 0)
                {
                    if (CalcPosition(input.A) == Position.Sitting)
                    {
                        output.Sprite(input.Sprites.Gryphon[16]);
                        return;
                    }

                    return;
                }

                if (CalcPosition(input.A) == Position.Sitting)
                {
                    output.Sprite(input.Sprites.Griffin[16]);
                }
            }); // left claw (only sitting)

            builder.RenderSingle(SpriteType.BodyAccent6, 10, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.GryphonSkin, input.U.SkinColor));
                if (input.U.HasDick == false || CalcPosition(input.A) == Position.Standing)
                {
                    return;
                }

                if (input.A.GetBallSize(10, 1.5f) > 5)
                {
                    output.Layer(1);
                    if (input.A.PredatorComponent?.BallsFullness > 0)
                    {
                        output.Sprite(input.Sprites.Gryphon[36 + input.A.GetBallSize(10, 1.5f)]);
                        return;
                    }

                    output.Sprite(input.Sprites.Gryphon[36]);
                    return;
                }

                if (input.A.GetStomachSize(16) < 3)
                {
                    output.Layer(10);
                    if (input.A.PredatorComponent?.BallsFullness > 0)
                    {
                        output.Sprite(input.Sprites.Gryphon[36 + input.A.GetBallSize(10, 1.5f)]);
                        return;
                    }

                    output.Sprite(input.Sprites.Gryphon[36]);
                    return;
                }

                output.Layer(5);
                if (input.A.PredatorComponent?.BallsFullness > 0)
                {
                    output.Sprite(input.Sprites.Gryphon[36 + input.A.GetBallSize(10, 1.5f)]);
                    return;
                }

                output.Sprite(input.Sprites.Gryphon[36]);
            }); // right ball (only sitting)

            builder.RenderSingle(SpriteType.BodyAccent7, 11, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.GryphonSkin, input.U.SkinColor));
                if (input.U.HasDick)
                {
                    output.Layer(11);
                    if (CalcPosition(input.A) == Position.Standing)
                    {
                        output.Sprite(input.Sprites.Gryphon[48]);
                        return;
                    }

                    if (input.A.HasBelly)
                    {
                        if (input.A.GetStomachSize(16) == 0)
                        {
                            output.Sprite(input.Sprites.Gryphon[49]);
                            return;
                        }

                        if (input.A.GetStomachSize(16) == 1)
                        {
                            output.Sprite(input.Sprites.Gryphon[50]);
                            return;
                        }

                        if (input.A.GetStomachSize(16) > 1 && input.A.GetStomachSize(16) < 4)
                        {
                            output.Sprite(input.Sprites.Gryphon[51]);
                            return;
                        }

                        if (input.A.GetStomachSize(16) >= 4 && input.A.GetStomachSize(16) < 7)
                        {
                            output.Sprite(input.Sprites.Gryphon[52]);
                            return;
                        }

                        if (input.A.GetStomachSize(16) >= 7 && input.A.GetStomachSize(16) < 11)
                        {
                            output.Sprite(input.Sprites.Gryphon[53]);
                            return;
                        }

                        if (input.A.GetStomachSize(16) >= 11)
                        {
                            output.Sprite(input.Sprites.Gryphon[53]).Layer(6);
                            return;
                        }

                        return;
                    }

                    output.Sprite(input.Sprites.Gryphon[49]);
                }
            }); // dick base

            builder.RenderSingle(SpriteType.BodyAccent8, 16, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.GryphonSkin, input.U.SkinColor));
                if (input.U.SpecialAccessoryType == 0)
                {
                    return;
                }

                switch (CalcPosition(input.A))
                {
                    case Position.Standing:
                        output.Sprite(input.Sprites.Griffin[18]);
                        return;
                    case Position.Sitting:
                        output.Sprite(input.Sprites.Griffin[19]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Griffin[18]);
                        return;
                }
            }); // exra feather patch (only Griffin)

            builder.RenderSingle(SpriteType.BodySize, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.GryphonSkin, input.U.SkinColor));
                if (CalcPosition(input.A) == Position.Sitting && input.A.HasBelly == false)
                {
                    output.Sprite(input.Sprites.Gryphon[59]);
                }
            }); // belly cover up

            builder.RenderSingle(SpriteType.Belly, 9, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.GryphonSkin, input.U.SkinColor));
                if (input.U.Predator == false || input.A.HasBelly == false)
                {
                    return;
                }

                if (CalcPosition(input.A) == Position.Sitting)
                {
                    output.Sprite(input.Sprites.Gryphon[18 + input.A.GetStomachSize(16)]);
                }
            });

            builder.RenderSingle(SpriteType.Dick, 12, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasDick == false || CalcPosition(input.A) == Position.Standing)
                {
                    return;
                }

                if (input.A.IsErect())
                {
                    output.Layer(12);
                    if (input.A.GetStomachSize(16) == 0)
                    {
                        output.Sprite(input.Sprites.Gryphon[54]);
                        return;
                    }

                    if (input.A.GetStomachSize(16) == 1)
                    {
                        output.Sprite(input.Sprites.Gryphon[55]);
                        return;
                    }

                    if (input.A.GetStomachSize(16) > 1 && input.A.GetStomachSize(16) < 4)
                    {
                        output.Sprite(input.Sprites.Gryphon[56]);
                        return;
                    }

                    if (input.A.GetStomachSize(16) >= 4 && input.A.GetStomachSize(16) < 7)
                    {
                        output.Sprite(input.Sprites.Gryphon[57]);
                        return;
                    }

                    if (input.A.GetStomachSize(16) >= 7 && input.A.GetStomachSize(16) < 11)
                    {
                        output.Sprite(input.Sprites.Gryphon[58]);
                        return;
                    }

                    if (input.A.GetStomachSize(16) >= 11)
                    {
                        output.Sprite(input.Sprites.Gryphon[58]).Layer(7);
                    }
                }
            });

            builder.RenderSingle(SpriteType.Balls, 13, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.GryphonSkin, input.U.SkinColor));
                int sz = input.A.GetStomachSize(16);
                int bz = input.A.GetBallSize(10, 1.5f);
                if (input.U.HasDick == false || CalcPosition(input.A) == Position.Standing)
                {
                    return;
                }

                if (input.A.GetStomachSize(16) < 12 || sz < bz * 2)
                {
                    output.Layer(13);
                    if (input.A.PredatorComponent?.BallsFullness > 0)
                    {
                        output.Sprite(input.Sprites.Gryphon[36 + input.A.GetBallSize(10, 1.5f)]);
                        return;
                    }

                    output.Sprite(input.Sprites.Gryphon[36]);
                    return;
                }

                output.Layer(8);
                if (input.A.PredatorComponent?.BallsFullness > 0)
                {
                    output.Sprite(input.Sprites.Gryphon[36 + input.A.GetBallSize(10, 1.5f)]);
                    return;
                }

                output.Sprite(input.Sprites.Gryphon[36]);
            });


            builder.RunBefore((input, output) =>
            {
                if (input.U.Predator && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.A.GetStomachSize(16) == 16)
                {
                    output.ChangeSprite(SpriteType.Body).AddOffset(0, 30 * .625f);
                    output.ChangeSprite(SpriteType.Head).AddOffset(0, 30 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 30 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 30 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 30 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 30 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 30 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent6).AddOffset(15 * .625f, 12 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent7).AddOffset(0, 30 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent8).AddOffset(0, 30 * .625f);
                    output.ChangeSprite(SpriteType.Dick).AddOffset(0, 30 * .625f);
                    output.ChangeSprite(SpriteType.Balls).AddOffset(20 * .625f, 10 * .625f);
                }
                else if (input.U.Predator && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && input.A.GetStomachSize(16, .8f) == 16)
                {
                    output.ChangeSprite(SpriteType.Body).AddOffset(0, 20 * .625f);
                    output.ChangeSprite(SpriteType.Head).AddOffset(0, 20 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 20 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 20 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 20 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 20 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 20 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent6).AddOffset(15 * .625f, 2 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent7).AddOffset(0, 20 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent8).AddOffset(0, 20 * .625f);
                    output.ChangeSprite(SpriteType.Dick).AddOffset(0, 20 * .625f);
                    output.ChangeSprite(SpriteType.Balls).AddOffset(20 * .625f, 0);
                }
                else if (input.U.Predator && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && input.A.GetStomachSize(16, .9f) == 16)
                {
                    output.ChangeSprite(SpriteType.Body).AddOffset(0, 10 * .625f);
                    output.ChangeSprite(SpriteType.Head).AddOffset(0, 10 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 10 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 10 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 10 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 10 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 10 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent6).AddOffset(15 * .625f, -8 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent7).AddOffset(0, 10 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent8).AddOffset(0, 10 * .625f);
                    output.ChangeSprite(SpriteType.Dick).AddOffset(0, 10 * .625f);
                    output.ChangeSprite(SpriteType.Balls).AddOffset(20 * .625f, -10 * .625f);
                }
                else
                {
                    output.ChangeSprite(SpriteType.Balls).AddOffset(20 * .625f, -20 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent6).AddOffset(15 * .625f, -18 * .625f);
                }
            });


            builder.RandomCustom(Defaults.RandomCustom);
        });
    }
}
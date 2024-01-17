using System.Collections.Generic;

namespace Races.Graphics.Implementations.Monsters
{
    internal static class Gazelle
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Gazelle", "Gazelles");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 18,
                    StomachSize = 16,
                    HasTail = true,
                    FavoredStat = Stat.Agility,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth, VoreType.CockVore },
                    ExpMultiplier = 1.1f,
                    PowerAdjustment = 1.3f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(8, 12),
                        Dexterity = new RaceStats.StatRange(10, 16),
                        Endurance = new RaceStats.StatRange(10, 16),
                        Mind = new RaceStats.StatRange(6, 10),
                        Will = new RaceStats.StatRange(6, 10),
                        Agility = new RaceStats.StatRange(16, 24),
                        Voracity = new RaceStats.StatRange(10, 16),
                        Stomach = new RaceStats.StatRange(10, 16),
                    },
                    RacialTraits = new List<Traits>()
                    {
                        Traits.Charge,
                        Traits.ForcefulBlow,
                    },
                    RaceDescription = ""
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.Skintone, "Fur Color");
                    buttons.SetText(ButtonType.BodyAccessoryType, "Ear Type");
                    buttons.SetText(ButtonType.BodyAccentTypes1, "Fur Pattern");
                    buttons.SetText(ButtonType.BodyAccentTypes2, "Horn Type (for males)");
                });
                output.SpecialAccessoryCount = 8; // ears
                output.BodyAccentTypes1 = 8; // fur patterns
                output.BodyAccentTypes2 = 10; // horns (for males)
                output.TailTypes = 6;
                output.ClothingColors = 0;
                output.GentleAnimation = true;
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.GazelleSkin);
                output.EyeColors = ColorPaletteMap.GetPaletteCount(SwapType.EyeColor);
            });


            builder.RenderSingle(SpriteType.Head, 11, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.GazelleSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Gazelle1[24 + (input.A.IsAttacking || input.A.IsEating ? 1 : 0) + 2 * input.U.BodyAccentType1]);
            });
            builder.RenderSingle(SpriteType.Eyes, 13, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
                output.Sprite(input.Sprites.Gazelle1[70]);
            });
            builder.RenderSingle(SpriteType.Mouth, 13, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Gazelle1[71 + (input.A.IsAttacking || input.A.IsEating ? 1 : 0)]);
            });
            builder.RenderSingle(SpriteType.Body, 10, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.GazelleSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Gazelle1[0 + input.U.BodyAccentType1]);
            });
            builder.RenderSingle(SpriteType.BodyAccent, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.GazelleSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Gazelle1[9 + input.U.BodyAccentType1 * 2]);
            }); // legs1
            builder.RenderSingle(SpriteType.BodyAccent2, 0, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.GazelleSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Gazelle1[8 + input.U.BodyAccentType1 * 2]);
            }); // legs2
            builder.RenderSingle(SpriteType.BodyAccent3, 10, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Gazelle1[40]);
            }); // hoof1
            builder.RenderSingle(SpriteType.BodyAccent4, 0, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Gazelle1[41]);
            }); // hoof2
            builder.RenderSingle(SpriteType.BodyAccent5, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Gazelle1[42]);
            }); // hoof3
            builder.RenderSingle(SpriteType.BodyAccent6, 8, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.GazelleSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Gazelle1[43 + input.U.TailType]);
            }); // tail
            builder.RenderSingle(SpriteType.BodyAccent7, 15, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasDick)
                {
                    output.Sprite(input.Sprites.Gazelle1[60 + input.U.BodyAccentType2]);
                }
            }); // horns

            builder.RenderSingle(SpriteType.BodyAccent8, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.GazelleSkin, input.U.SkinColor));
                if (Config.HideCocks)
                {
                    return;
                }

                if (input.U.HasDick)
                {
                    output.Sprite(input.Sprites.Gazelle2[31]);
                }
            }); // sheath

            builder.RenderSingle(SpriteType.BodyAccent9, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.GazelleSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Gazelle1[49]);
            }); // belly cover
            builder.RenderSingle(SpriteType.BodyAccessory, 13, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.GazelleSkin, input.U.SkinColor));
                if ((input.U.BodyAccentType1 == 1 || input.U.BodyAccentType1 == 5) && input.U.SpecialAccessoryType == 3)
                {
                    output.Sprite(input.Sprites.Gazelle1[58]);
                    return;
                }

                if ((input.U.BodyAccentType1 == 1 || input.U.BodyAccentType1 == 5) && input.U.SpecialAccessoryType == 5)
                {
                    output.Sprite(input.Sprites.Gazelle1[59]);
                    return;
                }

                output.Sprite(input.Sprites.Gazelle1[50 + input.U.SpecialAccessoryType]);
            }); // ears

            builder.RenderSingle(SpriteType.Belly, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.GazelleSkin, input.U.SkinColor));
                output.Layer(input.A.GetStomachSize(27) > 9 ? 9 : 6);

                if (input.A.HasBelly == false)
                {
                    return;
                }

                output.Sprite(input.Sprites.Gazelle2[0 + input.A.GetStomachSize(27)]);
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
                    output.Sprite(input.Sprites.Gazelle2[32]);
                }
            });

            builder.RenderSingle(SpriteType.Balls, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.GazelleSkin, input.U.SkinColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.BallsFullness > 0)
                {
                    output.Sprite(input.Sprites.Gazelle2[33 + input.A.GetBallSize(30)]);
                    return;
                }

                output.Sprite(input.Sprites.Gazelle2[33]);
            });


            builder.RunBefore((input, output) =>
            {
                output.ChangeSprite(SpriteType.Balls).AddOffset(-30 * .625f, -45 * .625f);
                output.ChangeSprite(SpriteType.Belly).AddOffset(-10 * .625f, -40 * .625f);
            });

            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                Unit unit = data.Unit;


                unit.BodyAccentType1 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes1);
                unit.BodyAccentType2 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes2);
                unit.TailType = State.Rand.Next(data.MiscRaceData.TailTypes);
            });
        });
    }
}
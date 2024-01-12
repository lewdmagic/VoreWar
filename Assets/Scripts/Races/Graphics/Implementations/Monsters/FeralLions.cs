#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class FeralLions
    {
        internal static readonly IRaceData Instance = RaceBuilder.CreateV2(Defaults.Blank<HindViewParameters>, builder =>
        {
            RaceFrameList frameListRumpVore = new RaceFrameList(new int[2] { 0, 1 }, new float[2] { .75f, .5f });


            builder.Setup(output =>
            {
                output.Names("Feral Lion", "Feral Lions");
                output.FlavorText(new FlavorText(
                    new Texts { "roaring", "once-vicious", "formerly-fearsome" },
                    new Texts { "indulgent", "greedily snarling", "voracious", "capacious", "insatiable", "dominant", "pleased" },
                    new Texts { "feline", "leonine", "kitty", {"lioness", Gender.Female}, {"lion", Gender.Male} }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 20,
                    StomachSize = 20,
                    HasTail = true,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth, VoreType.CockVore },
                    ExpMultiplier = 1.75f,
                    PowerAdjustment = 3f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(14, 20),
                        Dexterity = new RaceStats.StatRange(8, 16),
                        Endurance = new RaceStats.StatRange(16, 24),
                        Mind = new RaceStats.StatRange(6, 12),
                        Will = new RaceStats.StatRange(12, 18),
                        Agility = new RaceStats.StatRange(14, 20),
                        Voracity = new RaceStats.StatRange(18, 24),
                        Stomach = new RaceStats.StatRange(18, 24),
                    },
                    RacialTraits = new List<Traits>()
                    {
                        Traits.Biter,
                        Traits.Pounce,
                        Traits.Ravenous,
                        Traits.TasteForBlood,
                        Traits.PleasurableTouch,
                    },
                    RaceDescription = $"Big hedonistic felines. They were probably following a migration of gazelle before they came upon this land.\nMuch older texts claim they are the children of Raha, another world's godess of pleasure. She spread her blessing to this realm, and in exchange, these kitties are feeling right at home digesting the natives.",
                    RaceAI = RaceAI.Hedonist,
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.Skintone, "Fur Color");
                    buttons.SetText(ButtonType.HairStyle, "Mane Style");
                    buttons.SetText(ButtonType.HairColor, "Mane Color");
                });
                output.IndividualNames(new List<string>
                {
                    "Kalahari",
                    "Okavangu",
                    "Zenobia"
                });
                output.CanBeGender = new List<Gender> { Gender.Male, Gender.Female, Gender.Hermaphrodite, Gender.Maleherm };
                output.HairStyles = 10; // Manes
                output.GentleAnimation = true;
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.FeralLionsFur);
                output.EyeColors = ColorPaletteMap.GetPaletteCount(SwapType.FeralLionsEyes);
                output.HairColors = ColorPaletteMap.GetPaletteCount(SwapType.FeralLionsMane);
            });


            builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralLionsFur, input.U.SkinColor));
                if (input.Params.HindView)
                {
                    output.Sprite(input.Sprites.FeralLions[48]);
                    return;
                }

                if (input.A.IsAttacking || input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.FeralLions[26]);
                    return;
                }

                output.Sprite(input.Sprites.FeralLions[27]);
            });

            builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralLionsEyes, input.U.EyeColor));
                if (input.A.IsOralVoring || input.A.IsAttacking || input.Params.HindView || input.A.IsAbsorbing || input.A.IsDigesting || input.A.HasJustVored || input.A.IsSuckling || input.A.IsBeingSuckled || input.A.IsBeingRubbed)
                {
                    return;
                }

                output.Sprite(input.Sprites.FeralLions[37]);
            });

            builder.RenderSingle(SpriteType.Mouth, 13, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(!input.A.IsAttacking && !input.A.IsOralVoring ? null : input.Sprites.FeralLions[91]);
            }); // Maw for vore and attack
            builder.RenderSingle(SpriteType.Body, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralLionsFur, input.U.SkinColor));
                if (input.Params.HindView)
                {
                    output.Sprite(input.Sprites.FeralLions[59]).Layer(15);
                    return;
                }

                output.Sprite(input.Sprites.FeralLions[0]).Layer(3);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 8, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralLionsFur, input.U.SkinColor));
                if (input.Params.HindView)
                {
                    output.Sprite(input.Sprites.FeralLions[49]).Layer(1);
                }
                else
                {
                    output.Sprite(input.Sprites.FeralLions[11]).Layer(1);
                }
            }); // Foreground legs / Background legs during hind view

            builder.RenderSingle(SpriteType.BodyAccent2, 10, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralLionsFur, input.U.SkinColor));
                if (input.A.IsOralVoring || input.A.IsAttacking || input.Params.HindView)
                {
                    return;
                }

                if (input.A.Targetable == false && input.A.Visible && input.A.Surrendered)
                {
                    output.Sprite(input.Sprites.FeralLions[41]);
                    return;
                }

                if (input.A.IsDigesting || input.A.IsAbsorbing || input.A.IsBeingSuckled)
                {
                    output.Sprite(input.Sprites.FeralLions[40]);
                    return;
                }

                if (input.A.HasJustVored || input.A.IsSuckling || input.A.IsBeingRubbed)
                {
                    output.Sprite(input.Sprites.FeralLions[39]);
                    return;
                }

                output.Sprite(input.Sprites.FeralLions[38]);
            }); // facial expression

            builder.RenderSingle(SpriteType.BodyAccent3, 9, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralLionsMane, input.U.HairColor));
                if (input.U.HairStyle == 0)
                {
                    return;
                }

                if (input.Params.HindView)
                {
                    output.Sprite(input.Sprites.FeralLions[50 + input.U.HairStyle - 1]);
                    return;
                }

                output.Sprite(input.Sprites.FeralLions[28 + input.U.HairStyle - 1]);
            }); // Mane

            builder.RenderSingle(SpriteType.BodyAccent4, 11, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralLionsMane, input.U.HairColor));
                if (input.Params.HindView)
                {
                    return;
                }

                output.Sprite(input.U.HairStyle == 0 ? null : input.A.IsAttacking || input.A.IsOralVoring || input.A.IsAbsorbing || input.A.DamagedColors ? input.Sprites.FeralLions[45] : input.Sprites.FeralLions[44]);
            }); // Mane over ears

            builder.RenderSingle(SpriteType.BodyAccent5, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralLionsMane, input.U.HairColor));
                output.Sprite(input.Params.HindView ? input.Sprites.FeralLions[90] : input.Sprites.FeralLions[12]);
            }); // Tail tip
            builder.RenderSingle(SpriteType.BodyAccent6, 13, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralLionsFur, input.U.SkinColor));
                output.Sprite(!input.A.IsAttacking && !input.A.IsOralVoring ? null : input.Sprites.FeralLions[46]);
            }); // The Maw parts that are fur-colored
            builder.RenderSingle(SpriteType.BodyAccent7, 17, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralLionsFur, input.U.SkinColor));
                if (!input.U.HasVagina)
                {
                    return;
                }

                output.Sprite(input.Params.HindView ? input.Sprites.FeralLions[88] : null);
            }); // Pussy

            builder.RenderSingle(SpriteType.BodyAccessory, 10, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralLionsFur, input.U.SkinColor));
                if (!input.Params.HindView)
                {
                    if (input.A.IsAttacking || input.A.IsOralVoring || input.A.IsAbsorbing ||
                        input.A.IsBeingSuckled || input.A.DamagedColors)
                    {
                        output.Sprite(input.Sprites.FeralLions[42]).Layer(10);
                        return;
                    }

                    output.Sprite(input.Sprites.FeralLions[43]).Layer(10);
                    return;
                }

                output.Sprite(input.Sprites.FeralLions[47]).Layer(1);
            }); // Ears

            builder.RenderSingle(SpriteType.SecondaryAccessory, 18, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralLionsFur, input.U.SkinColor));
                if (!input.A.IsAnalVoring && !input.A.IsUnbirthing)
                {
                    return;
                }

                output.Sprite(input.A.IsAnalVoring ? input.Sprites.FeralLions[87] : input.Sprites.FeralLions[89]);
            }); // AV and UB

            builder.RenderSingle(SpriteType.Belly, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralLionsFur, input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    return;
                }

                output.Layer(input.Params.HindView ? 14 : 4);

                output.Sprite(input.Params.HindView ? input.Sprites.FeralLions[60 + input.A.GetStomachSize(13)] : input.Sprites.FeralLions[1 + input.A.GetStomachSize(8)]);
            });

            builder.RenderSingle(SpriteType.Dick, 8, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralLionsFur, input.U.SkinColor));
                //if ((input.State.HindView ? input.A.GetBallSize(8) : input.A.GetBallSize(9)) > 2)
                output.Layer(input.Params.HindView ? 15 : 6);
                //else 
                //Dick.layer = input.State.HindView ? 19 : 6;
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect())
                {
                    output.Sprite(input.Params.HindView ? input.Sprites.FeralLions[76] : input.Sprites.FeralLions[13]);
                }
            });

            builder.RenderSingle(SpriteType.Balls, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralLionsFur, input.U.SkinColor));
                output.Layer(input.Params.HindView ? 18 : 7);
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.BallsFullness > 0)
                {
                    output.Sprite(input.Params.HindView ? input.Sprites.FeralLions[78 + input.A.GetBallSize(7)] : input.Sprites.FeralLions[15 + input.A.GetBallSize(9)]);
                    return;
                }

                output.Sprite(input.Params.HindView ? input.Sprites.FeralLions[77] : input.Sprites.FeralLions[14]);
            });

            builder.RunBefore((input, output) =>
            {
                if (input.A.IsAnalVoring || input.A.IsUnbirthing || input.A.IsCockVoring)
                {
                    output.Params.HindView = true;
                }
                else
                {
                    output.Params.HindView = false;
                }

                Defaults.Finalize.Invoke(input, output);
            });

            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                Unit unit = data.Unit;


                if (unit.GetGender() == Gender.Female || (unit.GetGender() == Gender.Hermaphrodite && Config.HermsOnlyUseFemaleHair))
                {
                    unit.HairStyle = 0;
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
                }
            });
        });

        internal class HindViewParameters : IParameters
        {
            internal bool HindView;
        }
    }
}
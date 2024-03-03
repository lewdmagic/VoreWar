#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.MainRaces
{
    internal static class Aabayx
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            builder.Setup((input, output) =>
            {
                output.Names("Aabayx", "Aabayx");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 6,
                    StomachSize = 15,
                    HasTail = true,
                    FavoredStat = Stat.Strength,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.CockVore, VoreType.Anal },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.ViralDigestion,
                        TraitType.AwkwardShape,
                        TraitType.SlowAbsorption,
                        TraitType.SlowBreeder,
                    },
                    RaceDescription = "The Aabayx are a species of virosapiens who recently revealed themselves to the world and were quick to commit to the stage of war.  Strangely enough, they are not new arrivals to the realm, but rather have been in extreme isolation in an unknown location and were waiting for the exact right time to resurface and conquer the masses.  That time is now.",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.BodyAccessoryColor, "Head Color");
                    buttons.SetText(ButtonType.ClothingExtraType1, "Face Paint");
                });
                output.TownNames(new List<string>
                {
                    "Akaryocyte", ///(this one is the capital, by the way)
                    "Infection Site Zero",
                    "Arenai",
                    "Temple of Twenty",
                    "Adnaviria",
                    "Ysynsr aaz Lextrnl's Domain",
                    "Duplodnaviria",
                    "Bacteriophage",
                    "Monodnaviria",
                    "Aychkaynienteeseven",
                    "Riboviria",
                    "Capsid",
                    "Ribozyviria",
                    "Lipid Envelope",
                    "Varidnaviria",
                    "Mimi",
                    "Tevenvirinae",
                    "Myoviridae",
                    "Podoviridae",
                    "Autographiviridae",
                });

                builder.RandomCustom((data, output) =>
                {
                    Defaults.Randomize(data, output);
                    IUnitRead unit = data.Unit;

                    unit.TailType = State.Rand.Next(data.SetupOutput.TailTypes);
                });

                output.DickSizes = () => 6;
                output.BreastSizes = () => 1;

                output.BodySizes = 5;
                output.HairStyles = 0;
                output.HairColors = 0;
                output.BodyAccentTypes1 = 0; // eyebrows
                output.EyeColors = 0;
                output.EyeTypes = 21;
                output.MouthTypes = 0;
                output.TailTypes = 16;
                output.SpecialAccessoryCount = 0;
                output.ClothingColors = 0;
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.AabayxSkin); // Head color
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.AabayxSkin);

                output.AllowedMainClothingTypes.Set(
                    AabayxTop1.AabayxTop1Instance,
                    AabayxTop2.AabayxTop2Instance,
                    AabayxTop3.AabayxTop3Instance,
                    AabayxTop4.AabayxTop4Instance,
                    AabayxTop5.AabayxTop5Instance,
                    AabayxTop6.AabayxTop6Instance,
                    AabayxTop7.AabayxTop7Instance
                );

                output.AllowedClothingHatTypes.Clear();
                output.AvoidedMainClothingTypes = 0;
                output.AllowedWaistTypes.Set(
                    AabayxPants1.AabayxPants1Instance,
                    AabayxPants2.AabayxPants2Instance,
                    AabayxPants3.AabayxPants3Instance,
                    AabayxPants4.AabayxPants4Instance,
                    AabayxPants5.AabayxPants5Instance
                );
                output.ExtraMainClothing1Types.Set(
                    AabayxFacePaint1.AabayxFacePaint1Instance,
                    AabayxFacePaint2.AabayxFacePaint2Instance,
                    AabayxFacePaint3.AabayxFacePaint3Instance,
                    AabayxFacePaint4.AabayxFacePaint4Instance,
                    AabayxFacePaint5.AabayxFacePaint5Instance,
                    AabayxFacePaint6.AabayxFacePaint6Instance,
                    AabayxFacePaint7.AabayxFacePaint7Instance
                );
            });


            builder.RenderSingle(SpriteType.Head, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.AabayxSkin, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Aabayx[2]);
            });
            builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsOralVoring)
                {
                }
                else
                {
                    output.Sprite(input.Sprites.Aabayx[12 + input.U.EyeType]);
                }
            });

            builder.RenderSingle(SpriteType.Mouth, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Aabayx[3]);
                }
            }); // Mouth

            builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.AabayxSkin, input.U.SkinColor));
                if (input.U.BodySize == 0)
                {
                    output.Sprite(input.Sprites.Aabayx[0 + (input.A.IsAttacking ? 1 : 0)]);
                    return;
                }

                if (input.U.BodySize == 1)
                {
                    output.Sprite(input.Sprites.Aabayx[4 + (input.A.IsAttacking ? 1 : 0)]);
                    return;
                }

                if (input.U.BodySize == 2)
                {
                    output.Sprite(input.Sprites.Aabayx[6 + (input.A.IsAttacking ? 1 : 0)]);
                    return;
                }

                if (input.U.BodySize == 3)
                {
                    output.Sprite(input.Sprites.Aabayx[8 + (input.A.IsAttacking ? 1 : 0)]);
                    return;
                }

                if (input.U.BodySize == 4)
                {
                    output.Sprite(input.Sprites.Aabayx[10 + (input.A.IsAttacking ? 1 : 0)]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent3, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Aabayx[60 + input.U.TailType]);
            }); // Tail

            builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.AabayxSkin, input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    return;
                }

                output.Sprite(input.A.HasBelly ? input.Sprites.Aabayx[33 + input.A.GetStomachSize(21)] : null);
            });

            builder.RenderSingle(SpriteType.Dick, 11, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.AabayxSkin, input.U.SkinColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                output.AddOffset(0, 3 * .625f);

                if (input.A.IsErect())
                {
                    if (input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(32 * 32)) < 16)
                    {
                        output.Sprite(input.Sprites.HumansBodySprites4[1 + input.U.DickSize * 2 + (input.U.BodySize > 1 ? 12 : 0) + (!input.U.HasBreasts ? 24 : 0)]).Layer(20);
                        return;
                    }

                    output.Sprite(input.Sprites.HumansBodySprites4[0 + input.U.DickSize * 2 + (input.U.BodySize > 1 ? 12 : 0) + (!input.U.HasBreasts ? 24 : 0)]).Layer(13);
                    return;
                }

                output.Sprite(input.Sprites.HumansBodySprites4[0 + input.U.DickSize * 2 + (input.U.BodySize > 1 ? 12 : 0) + (!input.U.HasBreasts ? 24 : 0)]).Layer(11);
            });

            builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.AabayxSkin, input.U.SkinColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                output.AddOffset(0, 2 * .625f);

                if (input.A.IsErect() && input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(32 * 32)) < 16)
                {
                    output.Layer(19);
                }
                else
                {
                    output.Layer(10);
                }

                int size = input.U.DickSize;
                int offsetI = input.A.GetBallSize(28, .8f);

                if (offsetI >= 26)
                {
                    output.AddOffset(0, -22 * .625f);
                }
                else if (offsetI == 25)
                {
                    output.AddOffset(0, -16 * .625f);
                }
                else if (offsetI == 24)
                {
                    output.AddOffset(0, -13 * .625f);
                }
                else if (offsetI == 23)
                {
                    output.AddOffset(0, -11 * .625f);
                }
                else if (offsetI == 22)
                {
                    output.AddOffset(0, -10 * .625f);
                }
                else if (offsetI == 21)
                {
                    output.AddOffset(0, -7 * .625f);
                }
                else if (offsetI == 20)
                {
                    output.AddOffset(0, -6 * .625f);
                }
                else if (offsetI == 19)
                {
                    output.AddOffset(0, -4 * .625f);
                }
                else if (offsetI == 18)
                {
                    output.AddOffset(0, -1 * .625f);
                }

                if (offsetI > 0)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[Math.Min(112 + offsetI, 138)]);
                }
                else
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[106 + size]);
                }
            });

            builder.RenderSingle(SpriteType.Weapon, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    output.Sprite(input.Sprites.Aabayx[88 + input.A.GetWeaponSprite()]);
                }
            });
        });


        //##################
        //#### CLOTHING ####
        //##################

        private static class AabayxTop1
        {
            internal static readonly IClothing AabayxTop1Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = null;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.aabayx/60018");
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(15);
                    output["Clothing1"].Sprite(input.Sprites.Aabayx[96 + (input.A.IsAttacking ? 1 : 0)]);
                });
            });
        }

        private static class AabayxTop2
        {
            internal static readonly IClothing AabayxTop2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = null;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.aabayx/60018");
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(15);
                    output["Clothing1"].Sprite(input.Sprites.Aabayx[98 + (input.A.IsAttacking ? 1 : 0)]);
                });
            });
        }

        private static class AabayxTop3
        {
            internal static readonly IClothing AabayxTop3Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = null;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.aabayx/60018");
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(15);
                    output["Clothing1"].Sprite(input.Sprites.Aabayx[100 + (input.A.IsAttacking ? 1 : 0)]);
                });
            });
        }

        private static class AabayxTop4
        {
            internal static readonly IClothing AabayxTop4Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = null;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.aabayx/60018");
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(15);
                    output["Clothing1"].Sprite(input.Sprites.Aabayx[102 + (input.A.IsAttacking ? 1 : 0)]);
                });
            });
        }

        private static class AabayxTop5
        {
            internal static readonly IClothing AabayxTop5Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = null;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.aabayx/60018");
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(15);
                    output["Clothing1"].Sprite(input.Sprites.Aabayx[104 + (input.A.IsAttacking ? 1 : 0)]);
                });
            });
        }

        private static class AabayxTop6
        {
            internal static readonly IClothing AabayxTop6Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = null;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.aabayx/60018");
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(15);
                    output["Clothing1"].Sprite(input.Sprites.Aabayx[106 + (input.A.IsAttacking ? 1 : 0)]);
                });
            });
        }

        private static class AabayxTop7
        {
            internal static readonly IClothing AabayxTop7Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = null;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.aabayx/60018");
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(15);
                    output["Clothing1"].Sprite(input.Sprites.Aabayx[108 + (input.A.IsAttacking ? 1 : 0)]);
                });
            });
        }

        private static class AabayxPants1
        {
            internal static readonly IClothing AabayxPants1Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = null;
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.aabayx/60018");
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(13);
                    output["Clothing1"].Sprite(input.Sprites.Aabayx[55]);
                });
            });
        }

        private static class AabayxPants2
        {
            internal static readonly IClothing AabayxPants2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = null;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.aabayx/60018");
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(13);
                    output["Clothing1"].Sprite(input.Sprites.Aabayx[56]);
                });
            });
        }

        private static class AabayxPants3
        {
            internal static readonly IClothing AabayxPants3Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = null;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.aabayx/60018");
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(13);
                    output["Clothing1"].Sprite(input.Sprites.Aabayx[57]);
                });
            });
        }

        private static class AabayxPants4
        {
            internal static readonly IClothing AabayxPants4Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = null;
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.aabayx/60018");
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(13);
                    output["Clothing1"].Sprite(input.Sprites.Aabayx[58]);
                });
            });
        }

        private static class AabayxPants5
        {
            internal static readonly IClothing AabayxPants5Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = null;
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.aabayx/60018");
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(13);
                    output["Clothing1"].Sprite(input.Sprites.Aabayx[59]);
                });
            });
        }

        private static class AabayxFacePaint1
        {
            internal static readonly IClothing AabayxFacePaint1Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.LeaderOnly = false;
                    output.RevealsDick = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AabayxSkin, input.U.SkinColor));
                    output["Clothing1"].Sprite(input.A.IsOralVoring ? input.Sprites.AabayxFacePaint[7] : input.Sprites.AabayxFacePaint[0]);
                });
            });
        }

        private static class AabayxFacePaint2
        {
            internal static readonly IClothing AabayxFacePaint2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.LeaderOnly = false;
                    output.RevealsDick = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AabayxSkin, input.U.SkinColor));
                    if (input.A.IsOralVoring)
                    {
                        output["Clothing1"].Sprite(input.Sprites.AabayxFacePaint[8]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.AabayxFacePaint[1]);
                    }
                });
            });
        }

        private static class AabayxFacePaint3
        {
            internal static readonly IClothing AabayxFacePaint3Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.LeaderOnly = false;
                    output.RevealsDick = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AabayxSkin, input.U.SkinColor));
                    output["Clothing1"].Sprite(input.A.IsOralVoring ? input.Sprites.AabayxFacePaint[9] : input.Sprites.AabayxFacePaint[2]);
                });
            });
        }

        private static class AabayxFacePaint4
        {
            internal static readonly IClothing AabayxFacePaint4Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.LeaderOnly = false;
                    output.RevealsDick = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AabayxSkin, input.U.SkinColor));
                    output["Clothing1"].Sprite(input.A.IsOralVoring ? input.Sprites.AabayxFacePaint[10] : input.Sprites.AabayxFacePaint[3]);
                });
            });
        }

        private static class AabayxFacePaint5
        {
            internal static readonly IClothing AabayxFacePaint5Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.LeaderOnly = false;
                    output.RevealsDick = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AabayxSkin, input.U.SkinColor));
                    output["Clothing1"].Sprite(input.A.IsOralVoring ? input.Sprites.AabayxFacePaint[11] : input.Sprites.AabayxFacePaint[4]);
                });
            });
        }

        private static class AabayxFacePaint6
        {
            internal static readonly IClothing AabayxFacePaint6Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.LeaderOnly = false;
                    output.RevealsDick = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AabayxSkin, input.U.SkinColor));
                    output["Clothing1"].Sprite(input.A.IsOralVoring ? input.Sprites.AabayxFacePaint[12] : input.Sprites.AabayxFacePaint[5]);
                });
            });
        }

        private static class AabayxFacePaint7
        {
            internal static readonly IClothing AabayxFacePaint7Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.LeaderOnly = false;
                    output.RevealsDick = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AabayxSkin, input.U.SkinColor));
                    if (!input.A.IsOralVoring)
                    {
                        output["Clothing1"].Sprite(input.Sprites.AabayxFacePaint[6]);
                    }
                });
            });
        }
    }
}
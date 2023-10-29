#region

using System;
using UnityEngine;

#endregion

internal static class Aabayx
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Default, builder =>
    {
        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.TailType = State.Rand.Next(data.MiscRaceData.TailTypes);
        });

        builder.Setup(output =>
        {
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
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AabayxSkin); // Head color
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AabayxSkin);

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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Aabayx[2]);
        });
        builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsOralVoring)
            {
            }
            else
            {
                output.Sprite(input.Sprites.Aabayx[12 + input.Actor.Unit.EyeType]);
            }
        });

        builder.RenderSingle(SpriteType.Mouth, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Aabayx[3]);
            }
        }); // Mouth

        builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.BodySize == 0)
            {
                output.Sprite(input.Sprites.Aabayx[0 + (input.Actor.IsAttacking ? 1 : 0)]);
                return;
            }

            if (input.Actor.Unit.BodySize == 1)
            {
                output.Sprite(input.Sprites.Aabayx[4 + (input.Actor.IsAttacking ? 1 : 0)]);
                return;
            }

            if (input.Actor.Unit.BodySize == 2)
            {
                output.Sprite(input.Sprites.Aabayx[6 + (input.Actor.IsAttacking ? 1 : 0)]);
                return;
            }

            if (input.Actor.Unit.BodySize == 3)
            {
                output.Sprite(input.Sprites.Aabayx[8 + (input.Actor.IsAttacking ? 1 : 0)]);
                return;
            }

            if (input.Actor.Unit.BodySize == 4)
            {
                output.Sprite(input.Sprites.Aabayx[10 + (input.Actor.IsAttacking ? 1 : 0)]);
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent3, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Aabayx[60 + input.Actor.Unit.TailType]);
        }); // Tail

        builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            output.Sprite(input.Actor.HasBelly ? input.Sprites.Aabayx[33 + input.Actor.GetStomachSize(21)] : null);
        });

        builder.RenderSingle(SpriteType.Dick, 11, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            output.AddOffset(0, 3 * .625f);

            if (input.Actor.IsErect())
            {
                if (input.Actor.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32)) < 16)
                {
                    output.Sprite(input.Sprites.HumansBodySprites4[1 + input.Actor.Unit.DickSize * 2 + (input.Actor.Unit.BodySize > 1 ? 12 : 0) + (!input.Actor.Unit.HasBreasts ? 24 : 0)]).Layer(20);
                    return;
                }

                output.Sprite(input.Sprites.HumansBodySprites4[0 + input.Actor.Unit.DickSize * 2 + (input.Actor.Unit.BodySize > 1 ? 12 : 0) + (!input.Actor.Unit.HasBreasts ? 24 : 0)]).Layer(13);
                return;
            }

            output.Sprite(input.Sprites.HumansBodySprites4[0 + input.Actor.Unit.DickSize * 2 + (input.Actor.Unit.BodySize > 1 ? 12 : 0) + (!input.Actor.Unit.HasBreasts ? 24 : 0)]).Layer(11);
        });

        builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            output.AddOffset(0, 2 * .625f);

            if (input.Actor.IsErect() && input.Actor.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32)) < 16)
            {
                output.Layer(19);
            }
            else
            {
                output.Layer(10);
            }

            int size = input.Actor.Unit.DickSize;
            int offsetI = input.Actor.GetBallSize(28, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offsetI == 28)
            {
                output.Sprite(input.Sprites.HumansVoreSprites[141]).AddOffset(0, -22 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offsetI == 28)
            {
                output.Sprite(input.Sprites.HumansVoreSprites[140]).AddOffset(0, -22 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offsetI == 27)
            {
                output.Sprite(input.Sprites.HumansVoreSprites[139]).AddOffset(0, -22 * .625f);
                return;
            }

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
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                output.Sprite(input.Sprites.Aabayx[88 + input.Actor.GetWeaponSprite()]);
            }
        });
    });


    //##################
    //#### CLOTHING ####
    //##################

    private static class AabayxTop1
    {
        internal static IClothing AabayxTop1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = null;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 60018;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(15);
                output["Clothing1"].Sprite(input.Sprites.Aabayx[96 + (input.Actor.IsAttacking ? 1 : 0)]);
            });
        });
    }

    private static class AabayxTop2
    {
        internal static IClothing AabayxTop2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = null;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 60018;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(15);
                output["Clothing1"].Sprite(input.Sprites.Aabayx[98 + (input.Actor.IsAttacking ? 1 : 0)]);
            });
        });
    }

    private static class AabayxTop3
    {
        internal static IClothing AabayxTop3Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = null;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 60018;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(15);
                output["Clothing1"].Sprite(input.Sprites.Aabayx[100 + (input.Actor.IsAttacking ? 1 : 0)]);
            });
        });
    }

    private static class AabayxTop4
    {
        internal static IClothing AabayxTop4Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = null;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 60018;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(15);
                output["Clothing1"].Sprite(input.Sprites.Aabayx[102 + (input.Actor.IsAttacking ? 1 : 0)]);
            });
        });
    }

    private static class AabayxTop5
    {
        internal static IClothing AabayxTop5Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = null;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 60018;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(15);
                output["Clothing1"].Sprite(input.Sprites.Aabayx[104 + (input.Actor.IsAttacking ? 1 : 0)]);
            });
        });
    }

    private static class AabayxTop6
    {
        internal static IClothing AabayxTop6Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = null;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 60018;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(15);
                output["Clothing1"].Sprite(input.Sprites.Aabayx[106 + (input.Actor.IsAttacking ? 1 : 0)]);
            });
        });
    }

    private static class AabayxTop7
    {
        internal static IClothing AabayxTop7Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = null;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 60018;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(15);
                output["Clothing1"].Sprite(input.Sprites.Aabayx[108 + (input.Actor.IsAttacking ? 1 : 0)]);
            });
        });
    }

    private static class AabayxPants1
    {
        internal static IClothing AabayxPants1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = null;
                output.RevealsBreasts = true;
                output.Type = 60018;
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
        internal static IClothing AabayxPants2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = null;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 60018;
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
        internal static IClothing AabayxPants3Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = null;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 60018;
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
        internal static IClothing AabayxPants4Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = null;
                output.RevealsBreasts = true;
                output.Type = 60018;
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
        internal static IClothing AabayxPants5Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = null;
                output.RevealsBreasts = true;
                output.Type = 60018;
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
        internal static IClothing AabayxFacePaint1Instance = ClothingBuilder.Create(builder =>
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
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, input.Actor.Unit.SkinColor));
                if (input.Actor.IsOralVoring)
                {
                    output["Clothing1"].Sprite(input.Sprites.AabayxFacePaint[7]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.AabayxFacePaint[0]);
                }
            });
        });
    }

    private static class AabayxFacePaint2
    {
        internal static IClothing AabayxFacePaint2Instance = ClothingBuilder.Create(builder =>
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
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, input.Actor.Unit.SkinColor));
                if (input.Actor.IsOralVoring)
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
        internal static IClothing AabayxFacePaint3Instance = ClothingBuilder.Create(builder =>
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
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, input.Actor.Unit.SkinColor));
                if (input.Actor.IsOralVoring)
                {
                    output["Clothing1"].Sprite(input.Sprites.AabayxFacePaint[9]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.AabayxFacePaint[2]);
                }
            });
        });
    }

    private static class AabayxFacePaint4
    {
        internal static IClothing AabayxFacePaint4Instance = ClothingBuilder.Create(builder =>
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
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, input.Actor.Unit.SkinColor));
                if (input.Actor.IsOralVoring)
                {
                    output["Clothing1"].Sprite(input.Sprites.AabayxFacePaint[10]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.AabayxFacePaint[3]);
                }
            });
        });
    }

    private static class AabayxFacePaint5
    {
        internal static IClothing AabayxFacePaint5Instance = ClothingBuilder.Create(builder =>
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
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, input.Actor.Unit.SkinColor));
                if (input.Actor.IsOralVoring)
                {
                    output["Clothing1"].Sprite(input.Sprites.AabayxFacePaint[11]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.AabayxFacePaint[4]);
                }
            });
        });
    }

    private static class AabayxFacePaint6
    {
        internal static IClothing AabayxFacePaint6Instance = ClothingBuilder.Create(builder =>
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
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, input.Actor.Unit.SkinColor));
                if (input.Actor.IsOralVoring)
                {
                    output["Clothing1"].Sprite(input.Sprites.AabayxFacePaint[12]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.AabayxFacePaint[5]);
                }
            });
        });
    }

    private static class AabayxFacePaint7
    {
        internal static IClothing AabayxFacePaint7Instance = ClothingBuilder.Create(builder =>
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
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AabayxSkin, input.Actor.Unit.SkinColor));
                if (input.Actor.IsOralVoring)
                {
                    output["Clothing1"].Sprite(null);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.AabayxFacePaint[6]);
                }
            });
        });
    }
}
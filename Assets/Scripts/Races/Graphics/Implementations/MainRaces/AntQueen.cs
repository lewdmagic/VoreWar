#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.MainRaces
{
    internal static class AntQueen
    {
        private static Func<IClothingRenderInput, IOverSizeParameters> paramsCalc = CommonRaceCode.MakeOversizeFunc(31 * 31);
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            IClothing LeaderClothes = AntLeaderClothes.AntLeaderClothesInstance.Create(paramsCalc);


            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                Unit unit = data.Unit;
                unit.AccessoryColor = unit.SkinColor;

                unit.HairStyle = State.Rand.Next(14);

                if (unit.Type == UnitType.Leader)
                {
                    unit.ClothingType = 1 + data.MiscRaceData.AllowedMainClothingTypesBasic.IndexOf(LeaderClothes);
                }
            });

            builder.Setup(output =>
            {
                output.Names("AntQueen", "AntQueens");
                output.CanBeGender = new List<Gender> { Gender.Female, Gender.Hermaphrodite };
                output.BodySizes = 3;
                output.EyeTypes = 8;
                output.SpecialAccessoryCount = 12; // antennae        
                output.HairStyles = 14;
                output.MouthTypes = 3;
                output.EyeColors = 0;
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.DemiantSkin);
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.DemiantSkin);

                output.ExtendedBreastSprites = true;

                output.AllowedMainClothingTypes.Set(
                    LeaderClothes
                );
                output.AvoidedMainClothingTypes = 1;
                output.AvoidedEyeTypes = 0;
                output.AllowedWaistTypes.Set(
                );

                output.ClothingColors = 0;
            });


            builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.AntQueen1[0 + input.U.BodySize]);
            }); // Upper Body (White)
            builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.AntQueen1[14 + input.U.EyeType]);
            });
            builder.RenderSingle(SpriteType.Mouth, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsEating)
                {
                    output.Sprite(input.Sprites.AntQueen1[48]);
                    return;
                }

                output.Sprite(input.Sprites.AntQueen1[49 + input.U.MouthType]);
            });

            builder.RenderSingle(SpriteType.Hair, 18, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.NormalHair, input.U.HairColor));
                output.Sprite(input.Sprites.AntQueen1[34 + input.U.HairStyle]);
            });
            builder.RenderSingle(SpriteType.Body, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.AccessoryColor));
                output.Sprite(input.Sprites.AntQueen1[3 + input.U.BodySize]);
            }); // Lower Body (black)
            builder.RenderSingle(SpriteType.BodyAccent, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.AntQueen1[12]);
            }); // Abdomen 2 (White)
            builder.RenderSingle(SpriteType.BodyAccent2, 20, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.AccessoryColor));
                output.Sprite(input.Sprites.AntQueen1[22 + input.U.SpecialAccessoryType]);
            }); // Antennae (black)
            builder.RenderSingle(SpriteType.BodyAccent3, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.AccessoryColor));
                if (input.U.HasWeapon == false)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.AntQueen1[11]);
                        return;
                    }

                    output.Sprite(input.Sprites.AntQueen1[7]);
                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.AntQueen1[8]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.AntQueen1[10]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.AntQueen1[8]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.AntQueen1[10]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.AntQueen1[9]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.AntQueen1[11]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.AntQueen1[9]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.AntQueen1[11]);
                        return;
                    default:
                        output.Sprite(input.Sprites.AntQueen1[7]);
                        return;
                }
            }); // Upper Front Arms (black)

            builder.RenderSingle(SpriteType.BodyAccent4, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.AccessoryColor));
                output.Sprite(input.Sprites.AntQueen1[6]);
            }); // Lower Back Arms (black)
            builder.RenderSingle(SpriteType.BodyAccessory, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.AccessoryColor));
                output.Sprite(input.Sprites.AntQueen1[13]);
            }); // Abdomen (black)
            builder.RenderSingle(SpriteType.Breasts, 17, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.SkinColor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.LeftBreastFullness > 0)
                {
                    int leftSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(31 * 31));

                    if (leftSize > 27)
                    {
                        leftSize = 27;
                    }

                    output.Sprite(input.Sprites.AntQueen2[0 + leftSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.AntQueen2[0 + input.U.BreastSize]);
                }
            }); // Breasts (white)

            builder.RenderSingle(SpriteType.SecondaryBreasts, 17, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.SkinColor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.RightBreastFullness > 0)
                {
                    int rightSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(31 * 31));

                    if (rightSize > 27)
                    {
                        rightSize = 27;
                    }

                    output.Sprite(input.Sprites.AntQueen2[31 + rightSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.AntQueen2[31 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.SkinColor));
                if (input.A.HasBelly)
                {
                    int size = input.A.GetStomachSize(29, 0.8f);

                    switch (size)
                    {
                        case 24:
                            output.AddOffset(0, -7 * .625f);
                            break;
                        case 25:
                            output.AddOffset(0, -11 * .625f);
                            break;
                        case 26:
                            output.AddOffset(0, -14 * .625f);
                            break;
                        case 27:
                            output.AddOffset(0, -18 * .625f);
                            break;
                        case 28:
                            output.AddOffset(0, -21 * .625f);
                            break;
                        case 29:
                            output.AddOffset(0, -26 * .625f);
                            break;
                    }

                    output.Sprite(input.Sprites.AntQueen2[62 + size]);
                }
            }); // Belly (white)

            builder.RenderSingle(SpriteType.Dick, 11, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect())
                {
                    if (input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(31 * 31)) < 15 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(31 * 31)) < 15)
                    {
                        if (input.A.IsCockVoring)
                        {
                            output.Sprite(input.Sprites.AntQueen1[68 + input.U.DickSize]).Layer(20);
                        }
                        else
                        {
                            output.Sprite(input.Sprites.AntQueen1[52 + input.U.DickSize]).Layer(20);
                        }
                    }
                    else
                    {
                        if (input.A.IsCockVoring)
                        {
                            output.Sprite(input.Sprites.AntQueen1[76 + input.U.DickSize]).Layer(13);
                        }
                        else
                        {
                            output.Sprite(input.Sprites.AntQueen1[60 + input.U.DickSize]).Layer(13);
                        }
                    }
                }

                // pretty sure this is redundant. 
                //output.Layer(11); 
            });

            builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.SkinColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect() && input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(31 * 31)) < 15 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(31 * 31)) < 15)
                {
                    output.Layer(19);
                }
                else
                {
                    output.Layer(10);
                }

                int size = input.U.DickSize;
                int offsetI = input.A.GetBallSize(27, .8f);

                if (offsetI >= 25)
                {
                    output.AddOffset(0, -11 * .625f);
                }
                else if (offsetI == 24)
                {
                    output.AddOffset(0, -7 * .625f);
                }
                else if (offsetI == 23)
                {
                    output.AddOffset(0, -5 * .625f);
                }
                else if (offsetI == 22)
                {
                    output.AddOffset(0, -4 * .625f);
                }
                else if (offsetI == 21)
                {
                    output.AddOffset(0, -1 * .625f);
                }

                if (offsetI > 0)
                {
                    output.Sprite(input.Sprites.AntQueen2[Math.Min(104 + offsetI, 129)]);
                    return;
                }

                output.Sprite(input.Sprites.AntQueen2[96 + size]);
            }); // Balls (white)

            builder.RenderSingle(SpriteType.Weapon, 5, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    switch (input.A.GetWeaponSprite())
                    {
                        case 0:
                            output.Sprite(input.Sprites.AntQueen1[84]);
                            return;
                        case 1:
                            output.Sprite(input.Sprites.AntQueen1[85]);
                            return;
                        case 2:
                            output.Sprite(input.Sprites.AntQueen1[84]);
                            return;
                        case 3:
                            output.Sprite(input.Sprites.AntQueen1[85]);
                            return;
                        case 4:
                            output.Sprite(input.Sprites.AntQueen1[86]);
                            return;
                        case 5:
                            output.Sprite(input.Sprites.AntQueen1[87]);
                            return;
                        case 6:
                            output.Sprite(input.Sprites.AntQueen1[86]);
                            return;
                        case 7:
                            output.Sprite(input.Sprites.AntQueen1[87]);
                            return;
                        default:
                            return;
                    }
                }
            });


            builder.RunBefore((input, output) =>
            {
                Defaults.BasicBellyRunAfter.Invoke(input, output);
            });
        });


        private static class AntLeaderClothes
        {
            internal static readonly BindableClothing<IOverSizeParameters> AntLeaderClothesInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.AntQueen1[104];
                    output.RevealsBreasts = true;
                    output.OccupiesAllSlots = true;
                    output.FixedColor = true;
                    output.ClothingId = new ClothingId("base.antqueen/199");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing4"].Layer(6);
                    output["Clothing4"].Coloring(Color.white);
                    output["Clothing3"].Layer(19);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing2"].Layer(12);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Sprite(extra.Oversize ? input.Sprites.AntQueen1[96] : input.Sprites.AntQueen1[Mathf.Min(88 + input.U.BreastSize, 96)]);

                    output["Clothing2"].Sprite(input.Sprites.AntQueen1[97 + input.U.BodySize]);

                    output["Clothing3"].Sprite(input.Sprites.AntQueen1[100]);

                    if (input.A.GetWeaponSprite() == 1 || input.A.GetWeaponSprite() == 3)
                    {
                        output["Clothing4"].Sprite(input.Sprites.AntQueen1[102]);
                    }
                    else if (input.A.GetWeaponSprite() == 5 || input.A.GetWeaponSprite() == 7)
                    {
                        output["Clothing4"].Sprite(input.Sprites.AntQueen1[103]);
                    }
                    else
                    {
                        output["Clothing4"].Sprite(input.Sprites.AntQueen1[101]);
                    }
                });
            });
        }
    }
}
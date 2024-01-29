#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.Mercs
{
    internal static class Vargul
    {
        private static Func<IClothingRenderInput, IOverSizeParameters> paramsCalc = CommonRaceCode.MakeOversizeFunc(30 * 30);
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Vargul", "Vargul");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 17,
                    StomachSize = 17,
                    HasTail = true,
                    FavoredStat = Stat.Strength,
                    ExpMultiplier = 1.25f,
                    PowerAdjustment = 1.5f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(14, 20),
                        Dexterity = new RaceStats.StatRange(14, 20),
                        Endurance = new RaceStats.StatRange(14, 20),
                        Mind = new RaceStats.StatRange(8, 16),
                        Will = new RaceStats.StatRange(8, 16),
                        Agility = new RaceStats.StatRange(14, 20),
                        Voracity = new RaceStats.StatRange(14, 20),
                        Stomach = new RaceStats.StatRange(12, 18),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Intimidating,
                        TraitType.SenseWeakness,
                        TraitType.StrongGullet,
                        TraitType.Berserk,
                    },
                    RaceDescription = "",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.BodyAccessoryType, "Body Pattern Type");
                    buttons.SetText(ButtonType.BodyAccentTypes1, "Ear Type");
                    buttons.SetText(ButtonType.BodyAccentTypes2, "Head Pattern Type");
                    buttons.SetText(ButtonType.BodyAccentTypes3, "Mask On/Off (for armors)");
                    buttons.SetText(ButtonType.BodyAccessoryColor, "Body Pattern Colors");
                    buttons.SetText(ButtonType.ExtraColor1, "Armor Details Color");
                });
                output.DickSizes = () => 8;
                output.BreastSizes = () => 8;

                output.BodySizes = 4;
                output.EyeTypes = 5;
                output.SpecialAccessoryCount = 6; // body patterns    
                output.HairStyles = 0;
                output.MouthTypes = 0;
                output.HairColors = 0;
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.VargulSkin);
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.VargulSkin);
                output.EyeColors = ColorPaletteMap.GetPaletteCount(SwapType.ViperSkin);
                output.BodyAccentTypes1 = 5; // ears
                output.BodyAccentTypes2 = 6; // head pattern
                output.BodyAccentTypes3 = 2; // mask on/off

                output.ExtendedBreastSprites = true;

                output.AllowedClothingHatTypes.Clear();

                output.AllowedMainClothingTypes.Set(
                    GenericTop1.GenericTop1Instance.Create(paramsCalc),
                    GenericTop2.GenericTop2Instance.Create(paramsCalc),
                    GenericTop3.GenericTop3Instance.Create(paramsCalc),
                    GenericTop4.GenericTop4Instance.Create(paramsCalc),
                    GenericTop5.GenericTop5Instance.Create(paramsCalc),
                    Natural.NaturalInstance.Create(paramsCalc),
                    Tribal.TribalInstance.Create(paramsCalc),
                    LightArmour.LightArmourInstance.Create(paramsCalc),
                    MediumArmour.MediumArmourInstance.Create(paramsCalc),
                    HeavyArmour.HeavyArmourInstance.Create(paramsCalc)
                );
                output.AvoidedMainClothingTypes = 0;
                output.AvoidedEyeTypes = 0;
                output.AllowedWaistTypes.Set(
                    GenericBot1.GenericBot1Instance,
                    GenericBot2.GenericBot2Instance,
                    GenericBot3.GenericBot3Instance,
                    ArmourBot1.ArmourBot1Instance,
                    ArmourBot2.ArmourBot2Instance
                );

                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.AviansSkin);
                output.ExtraColors1 = ColorPaletteMap.GetPaletteCount(SwapType.Clothing50Spaced);
            });


            builder.RunBefore((input, output) =>
            {
                Defaults.BasicBellyRunAfter.Invoke(input, output);
            });

            builder.RenderSingle(SpriteType.Head, 19, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.VargulSkin, input.U.SkinColor));
                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Vargul1[22]);
                    return;
                }

                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Vargul1[21]);
                    return;
                }

                output.Sprite(input.Sprites.Vargul1[20]);
            });

            builder.RenderSingle(SpriteType.Eyes, 21, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Vargul1[40 + input.U.EyeType]);
            });

            builder.RenderSingle(SpriteType.SecondaryEyes, 21, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.EyeColor));
                output.Sprite(input.Sprites.Vargul1[45 + input.U.EyeType]);
            });


            builder.RenderSingle(SpriteType.Mouth, 21, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Vargul1[24]);
                    return;
                }

                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Vargul1[23]);
                }
            });

            builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.VargulSkin, input.U.SkinColor));
                output.Sprite(input.U.HasBreasts ? input.Sprites.Vargul1[0 + input.U.BodySize] : input.Sprites.Vargul1[4 + input.U.BodySize]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.VargulSkin, input.U.SkinColor));
                if (input.U.HasWeapon == false)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Vargul1[16 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0)]);
                        return;
                    }

                    output.Sprite(input.Sprites.Vargul1[8 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0)]);
                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Vargul1[12 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0)]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Vargul1[16 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0)]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Vargul1[12 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0)]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Vargul1[16 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0)]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.Vargul1[12 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0)]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.Vargul1[16 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0)]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Vargul1[12 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0)]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.Vargul1[16 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0)]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Vargul1[8 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0)]);
                        return;
                }
            }); // Right Arm

            builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.VargulSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Vargul1[30]);
            }); // Tail
            builder.RenderSingle(SpriteType.BodyAccent3, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.VargulSkin, input.U.AccessoryColor));
                if (input.U.SpecialAccessoryType == 5)
                {
                }
                else
                {
                    output.Sprite(input.Sprites.Vargul1[31]);
                }
            }); // Tail Pattern

            builder.RenderSingle(SpriteType.BodyAccent4, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.VargulSkin, input.U.AccessoryColor));
                if (input.U.SpecialAccessoryType == 5)
                {
                }
                else
                {
                    if (input.U.HasBreasts)
                    {
                        output.Sprite(input.Sprites.Vargul2[0 + input.U.BodySize + 20 * input.U.SpecialAccessoryType]);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.Vargul2[4 + input.U.BodySize + 20 * input.U.SpecialAccessoryType]);
                    }
                }
            }); // Body Secondary Pattern

            builder.RenderSingle(SpriteType.BodyAccent5, 20, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.VargulSkin, input.U.AccessoryColor));
                if (input.U.BodyAccentType2 == 5)
                {
                    return;
                }

                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Vargul1[62 + 3 * input.U.BodyAccentType2]);
                    return;
                }

                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Vargul1[61 + 3 * input.U.BodyAccentType2]);
                    return;
                }

                output.Sprite(input.Sprites.Vargul1[60 + 3 * input.U.BodyAccentType2]);
            }); // Head Secondary Pattern

            builder.RenderSingle(SpriteType.BodyAccent6, 21, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.VargulSkin, input.U.AccessoryColor));
                if (input.U.BodyAccentType2 == 5 || input.U.BodyAccentType2 == 3)
                {
                }
                else
                {
                    output.Sprite(input.Sprites.Vargul1[55 + input.U.BodyAccentType1]);
                }
            }); // Ears Pattern

            builder.RenderSingle(SpriteType.BodyAccent7, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.VargulSkin, input.U.AccessoryColor));
                if (input.U.SpecialAccessoryType == 5)
                {
                    return;
                }

                if (input.U.HasWeapon == false)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Vargul2[16 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0) + 20 * input.U.SpecialAccessoryType]);
                        return;
                    }

                    output.Sprite(input.Sprites.Vargul2[8 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0) + 20 * input.U.SpecialAccessoryType]);
                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Vargul2[12 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0) + 20 * input.U.SpecialAccessoryType]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Vargul2[16 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0) + 20 * input.U.SpecialAccessoryType]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Vargul2[12 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0) + 20 * input.U.SpecialAccessoryType]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Vargul2[16 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0) + 20 * input.U.SpecialAccessoryType]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.Vargul2[12 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0) + 20 * input.U.SpecialAccessoryType]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.Vargul2[16 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0) + 20 * input.U.SpecialAccessoryType]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Vargul2[12 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0) + 20 * input.U.SpecialAccessoryType]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.Vargul2[16 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0) + 20 * input.U.SpecialAccessoryType]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Vargul2[8 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0) + 20 * input.U.SpecialAccessoryType]);
                        return;
                }
            }); // Right Arm Secondary Pattern

            builder.RenderSingle(SpriteType.BodyAccent8, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Vargul1[26]);
            }); // Claws
            builder.RenderSingle(SpriteType.BodyAccent9, 2, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon == false)
                {
                    output.Sprite(input.A.IsAttacking ? input.Sprites.Vargul1[29] : input.Sprites.Vargul1[27]);
                }
                else
                {
                    output.Sprite(input.A.IsAttacking ? input.Sprites.Vargul1[29] : input.Sprites.Vargul1[28]);
                }
            }); // Right Arm Claws

            builder.RenderSingle(SpriteType.BodyAccent10, 18, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.VargulSkin, input.U.SkinColor));
                if (input.U.ClothingType == 9)
                {
                    return;
                }

                output.Sprite(input.Sprites.Vargul1[25]);
            }); // Fur Collar

            builder.RenderSingle(SpriteType.BodyAccessory, 20, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.VargulSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Vargul1[50 + input.U.BodyAccentType1]);
            }); // Ears
            builder.RenderSingle(SpriteType.Breasts, 17, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.VargulSkin, input.U.SkinColor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.LeftBreastFullness > 0)
                {
                    int leftSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(30 * 30));

                    if (leftSize > 26)
                    {
                        leftSize = 26;
                    }

                    output.Sprite(input.Sprites.Vargul3[0 + leftSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Vargul3[0 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.SecondaryBreasts, 17, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.VargulSkin, input.U.SkinColor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.RightBreastFullness > 0)
                {
                    int rightSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(30 * 30));

                    if (rightSize > 26)
                    {
                        rightSize = 26;
                    }

                    output.Sprite(input.Sprites.Vargul3[30 + rightSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Vargul3[30 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.VargulSkin, input.U.SkinColor));
                if (input.A.HasBelly)
                {
                    int size = input.A.GetStomachSize(26, 0.7f);

                    switch (size)
                    {
                        case 22:
                            output.AddOffset(0, -4 * .625f);
                            break;
                        case 23:
                            output.AddOffset(0, -9 * .625f);
                            break;
                        case 24:
                            output.AddOffset(0, -14 * .625f);
                            break;
                        case 25:
                            output.AddOffset(0, -19 * .625f);
                            break;
                        case 26:
                            output.AddOffset(0, -22 * .625f);
                            break;
                    }

                    output.Sprite(input.Sprites.Vargul3[60 + size]);
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
                    if (input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(31 * 31)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(31 * 31)) < 16)
                    {
                        output.Layer(20);
                        output.Sprite(input.A.IsCockVoring ? input.Sprites.Vargul1[83 + input.U.DickSize] : input.Sprites.Vargul1[75 + input.U.DickSize]);
                    }
                    else
                    {
                        output.Layer(13);
                        output.Sprite(input.A.IsCockVoring ? input.Sprites.Vargul1[99 + input.U.DickSize] : input.Sprites.Vargul1[91 + input.U.DickSize]);
                    }
                }

                //output.Layer(11);
            });

            builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.VargulSkin, input.U.SkinColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect() && input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(30 * 30)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(30 * 30)) < 16)
                {
                    output.Layer(19);
                }
                else
                {
                    output.Layer(10);
                }

                int size = input.U.DickSize;
                int offset = input.A.GetBallSize(27, .8f);

                if (offset >= 25)
                {
                    output.AddOffset(0, -10 * .625f);
                }
                else if (offset == 24)
                {
                    output.AddOffset(0, -5 * .625f);
                }

                if (offset > 0)
                {
                    output.Sprite(input.Sprites.Vargul3[Math.Min(99 + offset, 124)]);
                    return;
                }

                output.Sprite(input.Sprites.Vargul3[91 + size]);
            });

            builder.RenderSingle(SpriteType.Weapon, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    output.Sprite(input.Sprites.Vargul1[32 + input.A.GetWeaponSprite()]);
                }
            });

            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                Unit unit = data.Unit;


                unit.AccessoryColor = unit.SkinColor;

                unit.SpecialAccessoryType = State.Rand.Next(2) > 0 ? State.Rand.Next(data.SetupOutput.SpecialAccessoryCount) : 0;

                unit.BodyAccentType2 = unit.SpecialAccessoryType == 5 ? 5 : State.Rand.Next(data.SetupOutput.BodyAccentTypes2 - 1);

                unit.BodyAccentType1 = State.Rand.Next(data.SetupOutput.BodyAccentTypes1);
                unit.BodyAccentType3 = 1;
            });
        });


        private static class GenericTop1
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop1Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Komodos4[48];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vargul/61401");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vargul4[61]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vargul4[53 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop2
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop2Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Komodos4[58];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vargul/61402");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vargul4[70]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vargul4[62 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop3
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop3Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Komodos4[68];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vargul/61403");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing2"].Layer(18);
                    output["Clothing2"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vargul4[79]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vargul4[71 + input.U.BreastSize]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vargul4[80]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop4
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop4Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Komodos4[79];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vargul/61404");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing2"].Layer(18);
                    output["Clothing2"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                        output["Clothing2"].Sprite(input.Sprites.Vargul4[89]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vargul4[90 + input.U.BreastSize]);
                        output["Clothing2"].Sprite(input.Sprites.Vargul4[81 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop5
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop5Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Komodos4[97];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vargul/61405");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vargul4[106]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vargul4[98 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class Natural
        {
            internal static readonly BindableClothing<IOverSizeParameters> NaturalInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.OccupiesAllSlots = true;
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(7);
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vargul4[2 + input.U.BreastSize]);
                    }

                    output["Clothing2"].Sprite(input.U.BodySize < 3 ? input.Sprites.Vargul4[0] : input.Sprites.Vargul4[1]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.VargulSkin, input.U.SkinColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.VargulSkin, input.U.SkinColor));
                });
            });
        }

        private static class Tribal
        {
            internal static readonly BindableClothing<IOverSizeParameters> TribalInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Komodos4[38];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.vargul/61406");
                    output.OccupiesAllSlots = true;
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(12);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vargul4[52]);
                        output["Clothing2"].Sprite(input.Sprites.Vargul4[36 + input.U.BodySize]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vargul4[44 + input.U.BreastSize]);
                        output["Clothing2"].Sprite(input.Sprites.Vargul4[36 + input.U.BodySize]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.Vargul4[40 + input.U.BodySize]);
                    }
                });
            });
        }

        private static class LightArmour
        {
            internal static readonly BindableClothing<IOverSizeParameters> LightArmourInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vargul5[38];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.vargul/61802");
                    output.FixedColor = true;
                    output.OccupiesAllSlots = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing8"].Layer(18);
                    output["Clothing6"].Layer(12);
                    output["Clothing5"].Layer(7);
                    output["Clothing4"].Layer(7);
                    output["Clothing7"].Layer(19);
                    output["Clothing7"].Coloring(Color.white);
                    output["Clothing3"].Layer(22);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing2"].Layer(3);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(0);
                    output["Clothing1"].Coloring(Color.white);
                    if (input.U.BodyAccentType3 == 0)
                    {
                        output["Clothing3"].Sprite(input.Sprites.Vargul5[2]);
                    }
                    else
                    {
                        if (input.A.IsOralVoring)
                        {
                            output["Clothing3"].Sprite(input.Sprites.Vargul5[5]);
                        }
                        else if (input.A.IsAttacking || input.A.IsEating)
                        {
                            output["Clothing3"].Sprite(input.Sprites.Vargul5[4]);
                        }
                        else
                        {
                            output["Clothing3"].Sprite(input.Sprites.Vargul5[3]);
                        }
                    }

                    if (extra.Oversize)
                    {
                        if (input.U.BodySize < 2)
                        {
                            output["Clothing4"].Sprite(input.Sprites.Vargul5[6]);
                            output["Clothing5"].Sprite(input.Sprites.Vargul5[10]);
                        }
                        else if (input.U.BodySize > 2)
                        {
                            output["Clothing4"].Sprite(input.Sprites.Vargul5[7]);
                            output["Clothing5"].Sprite(input.Sprites.Vargul5[11]);
                        }
                        else
                        {
                            output["Clothing4"].Sprite(input.Sprites.Vargul5[7]);
                            output["Clothing5"].Sprite(input.Sprites.Vargul5[10]);
                        }

                        output["Clothing2"].Sprite(input.Sprites.Vargul5[1]);
                        output["Clothing6"].Sprite(input.Sprites.Vargul5[14 + input.U.BodySize]);
                        output["Clothing7"].Sprite(null);
                        output["Clothing8"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        if (input.U.BodySize < 2)
                        {
                            output["Clothing4"].Sprite(input.Sprites.Vargul5[6]);
                            output["Clothing5"].Sprite(input.Sprites.Vargul5[10]);
                        }
                        else if (input.U.BodySize > 2)
                        {
                            output["Clothing4"].Sprite(input.Sprites.Vargul5[7]);
                            output["Clothing5"].Sprite(input.Sprites.Vargul5[11]);
                        }
                        else
                        {
                            output["Clothing4"].Sprite(input.Sprites.Vargul5[7]);
                            output["Clothing5"].Sprite(input.Sprites.Vargul5[10]);
                        }

                        output["Clothing2"].Sprite(input.Sprites.Vargul5[1]);
                        output["Clothing6"].Sprite(input.Sprites.Vargul5[14 + input.U.BodySize]);
                        output["Clothing7"].Sprite(input.Sprites.Vargul5[22 + input.U.BreastSize]);
                        output["Clothing8"].Sprite(input.Sprites.Vargul5[30 + input.U.BodySize]);
                    }
                    else
                    {
                        if (input.U.BodySize < 2)
                        {
                            output["Clothing4"].Sprite(input.Sprites.Vargul5[8]);
                            output["Clothing5"].Sprite(input.Sprites.Vargul5[12]);
                        }
                        else
                        {
                            output["Clothing4"].Sprite(input.Sprites.Vargul5[9]);
                            output["Clothing5"].Sprite(input.Sprites.Vargul5[13]);
                        }

                        output["Clothing2"].Sprite(null);
                        output["Clothing6"].Sprite(input.Sprites.Vargul5[18 + input.U.BodySize]);
                        output["Clothing7"].Sprite(null);
                        output["Clothing8"].Sprite(input.Sprites.Vargul5[34 + input.U.BodySize]);
                    }

                    output["Clothing1"].Sprite(input.Sprites.Vargul5[0]);

                    output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ExtraColor1));
                    output["Clothing5"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ExtraColor1));
                    output["Clothing6"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ExtraColor1));
                    output["Clothing8"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ExtraColor1));
                });
            });
        }

        private static class MediumArmour
        {
            internal static readonly BindableClothing<IOverSizeParameters> MediumArmourInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vargul5[39];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.vargul/61803");
                    output.FixedColor = true;
                    output.OccupiesAllSlots = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing8"].Layer(12);
                    output["Clothing6"].Layer(15);
                    output["Clothing5"].Layer(11);
                    output["Clothing4"].Layer(7);
                    output["Clothing3"].Layer(7);
                    output["Clothing2"].Layer(22);
                    output["Clothing1"].Layer(0);
                    output["Clothing7"].Layer(19);
                    output["Clothing7"].Coloring(Color.white);
                    if (input.A.GetStomachSize(26, 0.7f) > 6)
                    {
                        output["Clothing6"].Sprite(input.U.HasBreasts ? input.Sprites.Vargul5[82] : input.Sprites.Vargul5[83]);

                        output["Clothing6"].Layer(13);
                    }
                    else if (input.A.GetStomachSize(26, 0.7f) > 0)
                    {
                        output["Clothing6"].Sprite(input.U.HasBreasts ? input.Sprites.Vargul5[74 + input.U.BodySize] : input.Sprites.Vargul5[78 + input.U.BodySize]);

                        output["Clothing6"].Layer(15);
                    }
                    else
                    {
                        output["Clothing6"].Sprite(input.U.HasBreasts ? input.Sprites.Vargul5[66 + input.U.BodySize] : input.Sprites.Vargul5[70 + input.U.BodySize]);

                        output["Clothing6"].Layer(15);
                    }

                    if (extra.Oversize)
                    {
                        if (input.U.BodySize < 2)
                        {
                            output["Clothing4"].Sprite(input.Sprites.Vargul5[54]);
                        }
                        else if (input.U.BodySize > 2)
                        {
                            output["Clothing4"].Sprite(input.Sprites.Vargul5[55]);
                        }
                        else
                        {
                            output["Clothing4"].Sprite(input.Sprites.Vargul5[54]);
                        }

                        if (input.A.HasBelly)
                        {
                            output["Clothing7"].Sprite(input.Sprites.Vargul5[107]);
                            output["Clothing8"].Sprite(input.Sprites.Vargul5[88 + input.U.BodySize]);
                        }
                        else
                        {
                            output["Clothing7"].Sprite(input.Sprites.Vargul5[107]);
                            output["Clothing8"].Sprite(input.Sprites.Vargul5[84 + input.U.BodySize]);
                        }

                        if (input.U.BodyAccentType3 == 0)
                        {
                            output["Clothing2"].Sprite(input.Sprites.Vargul5[42]);
                        }
                        else
                        {
                            if (input.A.IsOralVoring)
                            {
                                output["Clothing2"].Sprite(input.Sprites.Vargul5[45]);
                            }
                            else if (input.A.IsAttacking || input.A.IsEating)
                            {
                                output["Clothing2"].Sprite(input.Sprites.Vargul5[44]);
                            }
                            else
                            {
                                output["Clothing2"].Sprite(input.Sprites.Vargul5[43]);
                            }
                        }

                        output["Clothing5"].Sprite(input.Sprites.Vargul5[56 + input.U.BodySize]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        if (input.U.BodySize < 2)
                        {
                            output["Clothing4"].Sprite(input.Sprites.Vargul5[54]);
                        }
                        else if (input.U.BodySize > 2)
                        {
                            output["Clothing4"].Sprite(input.Sprites.Vargul5[55]);
                        }
                        else
                        {
                            output["Clothing4"].Sprite(input.Sprites.Vargul5[54]);
                        }

                        if (input.A.HasBelly)
                        {
                            output["Clothing7"].Sprite(input.Sprites.Vargul5[100 + input.U.BreastSize]);
                            output["Clothing8"].Sprite(input.Sprites.Vargul5[88 + input.U.BodySize]);
                        }
                        else
                        {
                            output["Clothing7"].Sprite(input.Sprites.Vargul5[92 + input.U.BreastSize]);
                            output["Clothing8"].Sprite(input.Sprites.Vargul5[84 + input.U.BodySize]);
                        }

                        if (input.U.BodyAccentType3 == 0)
                        {
                            output["Clothing2"].Sprite(input.Sprites.Vargul5[42]);
                        }
                        else
                        {
                            if (input.A.IsOralVoring)
                            {
                                output["Clothing2"].Sprite(input.Sprites.Vargul5[45]);
                            }
                            else if (input.A.IsAttacking || input.A.IsEating)
                            {
                                output["Clothing2"].Sprite(input.Sprites.Vargul5[44]);
                            }
                            else
                            {
                                output["Clothing2"].Sprite(input.Sprites.Vargul5[43]);
                            }
                        }

                        output["Clothing5"].Sprite(input.Sprites.Vargul5[56 + input.U.BodySize]);
                    }
                    else
                    {
                        output["Clothing4"].Sprite(input.U.BodySize < 2 ? input.Sprites.Vargul5[54] : input.Sprites.Vargul5[55]);

                        if (input.A.HasBelly)
                        {
                            output["Clothing7"].Sprite(null);
                            output["Clothing8"].Sprite(input.Sprites.Vargul5[112 + input.U.BodySize]);
                        }
                        else
                        {
                            output["Clothing7"].Sprite(null);
                            output["Clothing8"].Sprite(input.Sprites.Vargul5[108 + input.U.BodySize]);
                        }

                        if (input.U.BodyAccentType3 == 0)
                        {
                            output["Clothing2"].Sprite(input.Sprites.Vargul5[151]);
                        }
                        else
                        {
                            if (input.A.IsOralVoring)
                            {
                                output["Clothing2"].Sprite(input.Sprites.Vargul5[154]);
                            }
                            else if (input.A.IsAttacking || input.A.IsEating)
                            {
                                output["Clothing2"].Sprite(input.Sprites.Vargul5[153]);
                            }
                            else
                            {
                                output["Clothing2"].Sprite(input.Sprites.Vargul5[152]);
                            }
                        }

                        output["Clothing5"].Sprite(input.Sprites.Vargul5[60 + input.U.BodySize]);
                    }

                    output["Clothing1"].Sprite(input.Sprites.Vargul5[40]);
                    output["Clothing3"].Sprite(input.Sprites.Vargul5[46 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0) + (input.A.IsAttacking ? 4 : 0)]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ExtraColor1));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ExtraColor1));
                    output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ExtraColor1));
                    output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ExtraColor1));
                    output["Clothing5"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ExtraColor1));
                    output["Clothing6"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ExtraColor1));
                    output["Clothing8"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ExtraColor1));
                });
            });
        }

        private static class HeavyArmour
        {
            internal static readonly BindableClothing<IOverSizeParameters> HeavyArmourInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vargul5[159];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.vargul/61804");
                    output.FixedColor = true;
                    output.OccupiesAllSlots = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing8"].Layer(20);

                    output["Clothing7"].Layer(19);

                    output["Clothing6"].Layer(12);

                    output["Clothing5"].Layer(13);

                    output["Clothing4"].Layer(7);

                    output["Clothing3"].Layer(22);

                    output["Clothing1"].Layer(0);

                    output["Clothing2"].Layer(22);

                    output["Clothing2"].Coloring(Color.white);

                    if (input.U.BodyAccentType3 == 0)
                    {
                        output["Clothing3"].Sprite(null);
                    }
                    else
                    {
                        if (input.A.IsOralVoring)
                        {
                            output["Clothing3"].Sprite(input.Sprites.Vargul5[122]);
                        }
                        else if (input.A.IsAttacking || input.A.IsEating)
                        {
                            output["Clothing3"].Sprite(input.Sprites.Vargul5[121]);
                        }
                        else
                        {
                            output["Clothing3"].Sprite(input.Sprites.Vargul5[120]);
                        }
                    }

                    if (extra.Oversize)
                    {
                        if (input.U.BodySize < 2)
                        {
                            output["Clothing5"].Sprite(input.Sprites.Vargul5[123]);
                        }
                        else if (input.U.BodySize > 2)
                        {
                            output["Clothing5"].Sprite(input.Sprites.Vargul5[124]);
                        }
                        else
                        {
                            output["Clothing5"].Sprite(input.Sprites.Vargul5[123]);
                        }

                        if (input.A.HasBelly || input.A.GetBallSize(27, .8f) > 0 || input.A.HasPreyInBreasts)
                        {
                            output["Clothing8"].Sprite(null);
                        }
                        else
                        {
                            output["Clothing8"].Sprite(input.Sprites.Vargul5[127 + input.U.BodySize]);
                        }

                        output["Clothing7"].Layer(21);
                        output["Clothing1"].Sprite(input.Sprites.Vargul5[117]);
                        output["Clothing2"].Sprite(input.Sprites.Vargul5[116]);
                        output["Clothing6"].Sprite(input.Sprites.Vargul5[135 + input.U.BodySize]);
                        output["Clothing7"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        if (input.U.BodySize < 2)
                        {
                            output["Clothing5"].Sprite(input.Sprites.Vargul5[123]);
                        }
                        else if (input.U.BodySize > 2)
                        {
                            output["Clothing5"].Sprite(input.Sprites.Vargul5[124]);
                        }
                        else
                        {
                            output["Clothing5"].Sprite(input.Sprites.Vargul5[123]);
                        }

                        if (input.A.HasBelly || input.A.GetBallSize(27, .8f) > 0 || input.A.HasPreyInBreasts)
                        {
                            output["Clothing8"].Sprite(null);
                        }
                        else
                        {
                            output["Clothing8"].Sprite(input.Sprites.Vargul5[127 + input.U.BodySize]);
                        }

                        output["Clothing7"].Layer(21);
                        output["Clothing1"].Sprite(input.Sprites.Vargul5[117]);
                        output["Clothing2"].Sprite(input.Sprites.Vargul5[116]);
                        output["Clothing6"].Sprite(input.Sprites.Vargul5[135 + input.U.BodySize]);
                        output["Clothing7"].Sprite(input.Sprites.Vargul5[143 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing5"].Sprite(input.U.BodySize < 2 ? input.Sprites.Vargul5[125] : input.Sprites.Vargul5[126]);

                        if (input.A.HasBelly || input.A.GetBallSize(27, .8f) > 0 || input.A.HasPreyInBreasts)
                        {
                            output["Clothing8"].Sprite(null);
                        }
                        else
                        {
                            output["Clothing8"].Sprite(input.Sprites.Vargul5[131 + input.U.BodySize]);
                        }

                        output["Clothing7"].Layer(19);
                        output["Clothing1"].Sprite(input.Sprites.Vargul5[119]);
                        output["Clothing2"].Sprite(input.Sprites.Vargul5[118]);
                        output["Clothing6"].Sprite(input.Sprites.Vargul5[139 + input.U.BodySize]);
                        output["Clothing7"].Sprite(input.Sprites.Vargul5[155 + input.U.BodySize]);
                    }

                    output["Clothing4"].Sprite(input.Sprites.Vargul5[46 + (input.U.BodySize > 1 ? 1 : 0) + (!input.U.HasBreasts ? 2 : 0) + (input.A.IsAttacking ? 4 : 0)]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ExtraColor1));
                    output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ExtraColor1));
                    output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ExtraColor1));
                    output["Clothing5"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ExtraColor1));
                    output["Clothing6"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ExtraColor1));
                    output["Clothing7"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ExtraColor1));
                    output["Clothing8"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ExtraColor1));
                });
            });
        }

        private static class GenericBot1
        {
            internal static readonly IClothing GenericBot1Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Komodos4[9];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.vargul/61407");
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(13);

                    output["Clothing2"].Layer(12);

                    output["Clothing2"].Coloring(Color.white);

                    if (input.U.BodySize < 3)
                    {
                        if (input.U.DickSize > 0)
                        {
                            if (input.U.DickSize < 3)
                            {
                                output["Clothing1"].Sprite(input.Sprites.Vargul4[19]);
                            }
                            else if (input.U.DickSize > 5)
                            {
                                output["Clothing1"].Sprite(input.Sprites.Vargul4[21]);
                            }
                            else
                            {
                                output["Clothing1"].Sprite(input.Sprites.Vargul4[20]);
                            }
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Vargul4[18]);
                        }
                    }
                    else
                    {
                        if (input.U.DickSize > 0)
                        {
                            if (input.U.DickSize < 3)
                            {
                                output["Clothing1"].Sprite(input.Sprites.Vargul4[23]);
                            }
                            else if (input.U.DickSize > 5)
                            {
                                output["Clothing1"].Sprite(input.Sprites.Vargul4[25]);
                            }
                            else
                            {
                                output["Clothing1"].Sprite(input.Sprites.Vargul4[24]);
                            }
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Vargul4[22]);
                        }
                    }

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Vargul4[10 + input.U.BodySize] : input.Sprites.Vargul4[14 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericBot2
        {
            internal static readonly IClothing GenericBot2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Komodos4[19];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.vargul/61408");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(13);

                    output["Clothing2"].Layer(12);

                    output["Clothing2"].Coloring(Color.white);

                    output["Clothing1"].Sprite(input.U.BodySize < 3 ? input.Sprites.Vargul4[26] : input.Sprites.Vargul4[27]);

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Vargul4[10 + input.U.BodySize] : input.Sprites.Vargul4[14 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericBot3
        {
            internal static readonly IClothing GenericBot3Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Komodos4[24];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.vargul/61409");
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(12);

                    output["Clothing1"].Coloring(Color.white);

                    output["Clothing1"].Sprite(input.U.HasBreasts ? input.Sprites.Vargul4[28 + input.U.BodySize] : input.Sprites.Vargul4[32 + input.U.BodySize]);
                });
            });
        }

        private static class ArmourBot1
        {
            internal static readonly IClothing ArmourBot1Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vargul5[41];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.vargul/61801");
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(12);

                    output["Clothing1"].Layer(13);

                    output["Clothing1"].Coloring(Color.white);

                    if (input.U.DickSize > 0)
                    {
                        output["Clothing1"].Sprite(input.U.BodySize < 3 ? input.Sprites.Vargul5[64] : input.Sprites.Vargul5[65]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(null);
                    }

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Vargul5[56 + input.U.BodySize] : input.Sprites.Vargul5[60 + input.U.BodySize]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ExtraColor1));
                });
            });
        }

        private static class ArmourBot2
        {
            internal static readonly IClothing ArmourBot2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vargul5[41];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.vargul/61801");
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(12);

                    output["Clothing1"].Layer(13);

                    output["Clothing1"].Coloring(Color.white);

                    if (input.U.DickSize > 0)
                    {
                        output["Clothing1"].Sprite(input.U.BodySize < 3 ? input.Sprites.Vargul5[64] : input.Sprites.Vargul5[65]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(null);
                    }

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Vargul5[135 + input.U.BodySize] : input.Sprites.Vargul5[139 + input.U.BodySize]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ExtraColor1));
                });
            });
        }
    }
}
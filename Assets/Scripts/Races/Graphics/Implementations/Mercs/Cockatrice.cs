#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.Mercs
{
    internal static class Cockatrice
    {
        private static Func<IClothingRenderInput, IOverSizeParameters> _paramsCalc = CommonRaceCode.MakeOversizeFunc(32 * 32);

        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Cockatrice", "Cockatrice");
                output.FlavorText(new FlavorText(
                    new Texts { },
                    new Texts { },
                    new Texts { "cockatrice", "terror chicken", "danger chicken", { "scary hen", Gender.Female }, { "scary hen", Gender.Male } } ////new, blame Flame_Valxsarion for encouraging me. Actually don't, I came up with "monster cock" 
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 14,
                    StomachSize = 15,
                    HasTail = true,
                    FavoredStat = Stat.Mind,
                    ExpMultiplier = 1.25f,
                    PowerAdjustment = 1.5f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(10, 16),
                        Dexterity = new RaceStats.StatRange(8, 14),
                        Endurance = new RaceStats.StatRange(8, 14),
                        Mind = new RaceStats.StatRange(12, 20),
                        Will = new RaceStats.StatRange(8, 14),
                        Agility = new RaceStats.StatRange(8, 14),
                        Voracity = new RaceStats.StatRange(8, 14),
                        Stomach = new RaceStats.StatRange(12, 15),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Intimidating,
                        TraitType.Petrifier,
                    },
                    RaceDescription = "",
                });
                output.CustomizeButtons((unit, buttons) => { buttons.SetText(ButtonType.BodyAccessoryColor, "Feather Color"); });
                output.DickSizes = () => 8;
                output.BreastSizes = () => 8;
                output.BodySizes = 4;
                output.EyeTypes = 12;
                output.SpecialAccessoryCount = 0;
                output.HairStyles = 24;
                output.MouthTypes = 6;
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.CockatriceSkin); // Feather Colors
                output.HairColors = ColorPaletteMap.GetPaletteCount(SwapType.CockatriceSkin);
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.CockatriceSkin);

                output.ExtendedBreastSprites = true;

                output.AllowedMainClothingTypes.Set(
                    GenericTop1.GenericTop1Instance.Create(_paramsCalc),
                    GenericTop2.GenericTop2Instance.Create(_paramsCalc),
                    GenericTop3.GenericTop3Instance.Create(_paramsCalc),
                    GenericTop4.GenericTop4Instance.Create(_paramsCalc),
                    GenericTop5.GenericTop5Instance.Create(_paramsCalc),
                    GenericTop6.GenericTop6Instance.Create(_paramsCalc),
                    GenericTop7.GenericTop7Instance.Create(_paramsCalc),
                    MaleTop.MaleTopInstance,
                    MaleTop2.MaleTop2Instance,
                    Natural.NaturalInstance.Create(_paramsCalc),
                    Cuirass.CuirassInstance.Create(_paramsCalc)
                );
                output.AvoidedMainClothingTypes = 0;
                output.AvoidedEyeTypes = 0;
                output.AllowedWaistTypes.Set(
                    GenericBot1.GenericBot1Instance,
                    GenericBot2.GenericBot2Instance,
                    GenericBot3.GenericBot3Instance,
                    GenericBot4.GenericBot4Instance
                );

                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.AviansSkin);
            });


            builder.RunBefore((input, output) =>
            {
                if (input.A.HasBelly)
                {
                    output.ChangeSprite(SpriteType.Belly).SetActive(true).SetLocalScale(new Vector3(1, 1, 1));
                }
            });

            builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.CockatriceSkin, input.U.SkinColor));
                output.Sprite(input.U.HasBreasts ? input.Sprites.Cockatrice1[54] : input.Sprites.Cockatrice1[55]);
            }); // Head - skin

            builder.RenderSingle(SpriteType.Eyes, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
                output.Sprite(input.Sprites.Cockatrice1[72 + input.U.EyeType]);
            });
            builder.RenderSingle(SpriteType.Mouth, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.CockatriceSkin, input.U.SkinColor));
                if (input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Cockatrice1[62 + 2 * (!input.U.HasBreasts ? 1 : 0)]);
                    return;
                }

                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Cockatrice1[63 + 2 * (!input.U.HasBreasts ? 1 : 0)]);
                    return;
                }

                output.Sprite(input.Sprites.Cockatrice1[66 + input.U.MouthType]);
            });

            builder.RenderSingle(SpriteType.Hair, 20, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.CockatriceSkin, input.U.HairColor));
                output.Sprite(input.Sprites.Cockatrice1[96 + input.U.HairStyle]);
            });
            builder.RenderSingle(SpriteType.Hair2, 0, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.CockatriceSkin, input.U.HairColor));
                if (input.U.HairStyle > 13)
                {
                }
                else
                {
                    output.Sprite(input.Sprites.Cockatrice1[120 + input.U.HairStyle]);
                }
            });

            builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.CockatriceSkin, input.U.SkinColor));
                output.Sprite(input.U.HasBreasts ? input.Sprites.Cockatrice1[0 + input.U.BodySize] : input.Sprites.Cockatrice1[4 + input.U.BodySize]);
            }); // Body - skin

            builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.U.HasBreasts ? input.Sprites.Cockatrice1[8 + input.U.BodySize] : input.Sprites.Cockatrice1[12 + input.U.BodySize]);
            }); // Body - scales

            builder.RenderSingle(SpriteType.BodyAccent2, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.CockatriceSkin, input.U.AccessoryColor));
                output.Sprite(input.U.HasBreasts ? input.Sprites.Cockatrice1[16 + input.U.BodySize] : input.Sprites.Cockatrice1[20 + input.U.BodySize]);
            }); // Legs - feathers

            builder.RenderSingle(SpriteType.BodyAccent3, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.CockatriceSkin, input.U.AccessoryColor));
                if (input.U.HasWeapon == false)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Cockatrice1[25 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    }

                    output.Sprite(input.Sprites.Cockatrice1[24 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Cockatrice1[25 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Cockatrice1[26 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Cockatrice1[24 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Cockatrice1[25 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.Cockatrice1[24 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.Cockatrice1[25 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Cockatrice1[24 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.Cockatrice1[25 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Cockatrice1[24 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                }
            }); // Arms - feathers

            builder.RenderSingle(SpriteType.BodyAccent4, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon == false)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Cockatrice1[37 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    }

                    output.Sprite(input.Sprites.Cockatrice1[36 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Cockatrice1[37 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Cockatrice1[38 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Cockatrice1[36 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Cockatrice1[37 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.Cockatrice1[36 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.Cockatrice1[37 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Cockatrice1[36 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.Cockatrice1[37 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Cockatrice1[36 + 3 * (input.U.BodySize > 1 ? 1 : 0) + 6 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                }
            }); // Arms - scales

            builder.RenderSingle(SpriteType.BodyAccent5, 2, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Cockatrice1[48 + (input.U.BodySize > 1 ? 1 : 0) + 2 * (!input.U.HasBreasts ? 1 : 0)]);
            }); // Legs - scales
            builder.RenderSingle(SpriteType.BodyAccent6, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.CockatriceSkin, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Cockatrice1[52]);
            }); // Tail - feathers
            builder.RenderSingle(SpriteType.BodyAccent7, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Cockatrice1[53]);
            }); // Tail - scales
            builder.RenderSingle(SpriteType.BodyAccent8, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Cockatrice1[57 + 3 * (!input.U.HasBreasts ? 1 : 0)]);
                    return;
                }

                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Cockatrice1[58 + 3 * (!input.U.HasBreasts ? 1 : 0)]);
                    return;
                }

                output.Sprite(input.Sprites.Cockatrice1[56 + 3 * (!input.U.HasBreasts ? 1 : 0)]);
            }); // Head - scales

            builder.RenderSingle(SpriteType.BodyAccent9, 2, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon == false)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Cockatrice1[145 + 4 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    }

                    output.Sprite(input.Sprites.Cockatrice1[143 + 4 * (!input.U.HasBreasts ? 1 : 0)]);
                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Cockatrice1[145 + 4 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Cockatrice1[146 + 4 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Cockatrice1[144 + 4 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Cockatrice1[145 + 4 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.Cockatrice1[144 + 4 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.Cockatrice1[145 + 4 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Cockatrice1[144 + 4 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.Cockatrice1[145 + 4 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Cockatrice1[144 + 4 * (!input.U.HasBreasts ? 1 : 0)]);
                        return;
                }
            }); // Hands - scales

            builder.RenderSingle(SpriteType.BodyAccent10, 8, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.CockatriceSkin, input.U.HairColor));
                output.Sprite(input.Sprites.Cockatrice1[84 + input.U.EyeType]);
            }); // Eyebrows
            builder.RenderSingle(SpriteType.BodyAccessory, 21, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Cockatrice1[134]);
            });
            builder.RenderSingle(SpriteType.Breasts, 17, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.CockatriceSkin, input.U.SkinColor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.LeftBreastFullness > 0)
                {
                    int leftSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(32 * 32));

                    if (leftSize > 28)
                    {
                        leftSize = 28;
                    }

                    output.Sprite(input.Sprites.Cockatrice2[0 + leftSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Cockatrice2[0 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.SecondaryBreasts, 17, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.CockatriceSkin, input.U.SkinColor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.RightBreastFullness > 0)
                {
                    int rightSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(32 * 32));

                    if (rightSize > 28)
                    {
                        rightSize = 28;
                    }

                    output.Sprite(input.Sprites.Cockatrice2[32 + rightSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Cockatrice2[32 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.CockatriceSkin, input.U.SkinColor));
                if (input.A.HasBelly)
                {
                    int size = input.A.GetStomachSize(31, 0.7f);

                    switch (size)
                    {
                        case 26:
                            output.AddOffset(0, -3 * .625f);
                            break;
                        case 27:
                            output.AddOffset(0, -8 * .625f);
                            break;
                        case 28:
                            output.AddOffset(0, -13 * .625f);
                            break;
                        case 29:
                            output.AddOffset(0, -16 * .625f);
                            break;
                        case 30:
                            output.AddOffset(0, -22 * .625f);
                            break;
                        case 31:
                            output.AddOffset(0, -28 * .625f);
                            break;
                    }

                    output.Sprite(input.Sprites.Cockatrice2[64 + size]);
                }
            });

            builder.RenderSingle(SpriteType.Dick, 11, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.CockatriceSkin, input.U.SkinColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                output.AddOffset(0, 1 * .625f);

                if (input.A.IsErect())
                {
                    if (input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(32 * 32)) < 16)
                    {
                        output.Layer(20);
                        if (input.A.IsCockVoring)
                        {
                            output.Sprite(input.Sprites.Sharks3[132 + input.U.DickSize]);
                            return;
                        }

                        output.Sprite(input.Sprites.Sharks3[124 + input.U.DickSize]);
                        return;
                    }

                    output.Layer(13);
                    if (input.A.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.Sharks3[116 + input.U.DickSize]);
                        return;
                    }

                    output.Sprite(input.Sprites.Sharks3[108 + input.U.DickSize]);
                    return;
                }

                output.Sprite(input.Sprites.Sharks3[108 + input.U.DickSize]).Layer(11);
            });

            builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.CockatriceSkin, input.U.SkinColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect() && input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(32 * 32)) < 16)
                {
                    output.Layer(19);
                }
                else
                {
                    output.Layer(10);
                }

                int size = input.U.DickSize;
                int offset = input.A.GetBallSize(28, .8f);

                if (offset >= 26)
                {
                    output.AddOffset(0, -19 * .625f);
                }
                else if (offset == 25)
                {
                    output.AddOffset(0, -10 * .625f);
                }
                else if (offset == 24)
                {
                    output.AddOffset(0, -8 * .625f);
                }
                else if (offset == 23)
                {
                    output.AddOffset(0, -7 * .625f);
                }
                else if (offset == 22)
                {
                    output.AddOffset(0, -4 * .625f);
                }
                else if (offset == 21)
                {
                    output.AddOffset(0, -3 * .625f);
                }
                else if (offset == 20)
                {
                    output.AddOffset(0, -1 * .625f);
                }

                if (offset > 0)
                {
                    output.Sprite(input.Sprites.Cockatrice2[Math.Min(108 + offset, 134)]);
                    return;
                }

                output.Sprite(input.Sprites.Cockatrice2[100 + size]);
            });

            builder.RenderSingle(SpriteType.Weapon, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    output.Sprite(input.Sprites.Cockatrice1[135 + input.A.GetWeaponSprite()]);
                    if (!input.U.HasBreasts)
                    {
                        output.AddOffset(2 * .625f, 0);
                    }
                }
            });

            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                IUnitRead unit = data.Unit;


                unit.AccessoryColor = unit.SkinColor;
                unit.HairColor = unit.AccessoryColor;

                if (unit.HasDick && unit.HasBreasts)
                {
                    unit.HairStyle = State.Rand.Next(Config.HermsOnlyUseFemaleHair ? 18 : data.SetupOutput.HairStyles);
                }
                else if (unit.HasDick && Config.FemaleHairForMales)
                {
                    unit.HairStyle = State.Rand.Next(data.SetupOutput.HairStyles);
                }
                else if (unit.HasDick == false && Config.MaleHairForFemales)
                {
                    unit.HairStyle = State.Rand.Next(data.SetupOutput.HairStyles);
                }
                else
                {
                    if (unit.HasDick)
                    {
                        unit.HairStyle = 12 + State.Rand.Next(12);
                    }
                    else
                    {
                        unit.HairStyle = State.Rand.Next(18);
                    }
                }
            });
        });

        private static class GenericTop1
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop1Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[24];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.cockatrice/1524");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[56]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[48 + input.U.BreastSize]);
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
                    output.DiscardSprite = input.Sprites.Avians4[34];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.cockatrice/1534");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[65]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[57 + input.U.BreastSize]);
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
                    output.DiscardSprite = input.Sprites.Avians4[44];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.cockatrice/1544");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[74]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[66 + input.U.BreastSize]);
                    }

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
                    output.DiscardSprite = input.Sprites.Avians4[55];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.cockatrice/1555");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing2"].Layer(18);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].SetOffset(0, -2 * .625f);
                    output["Clothing2"].SetOffset(0, -2 * .625f);

                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[80]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[72 + input.U.BreastSize]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Sharks5[81]);
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
                    output.DiscardSprite = input.Sprites.Avians4[74];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.cockatrice/1574");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing2"].Layer(18);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].SetOffset(0, -2 * .625f);
                    output["Clothing2"].SetOffset(0, -2 * .625f);

                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[90]);
                        output["Clothing2"].Sprite(input.Sprites.Sharks5[99]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[82 + input.U.BreastSize]);
                        output["Clothing2"].Sprite(input.Sprites.Sharks5[91 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop6
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop6Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[88];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.cockatrice/1588");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].SetOffset(0, -2 * .625f);

                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[104 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop7
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop7Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[44];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.cockatrice/1544");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[95]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[87 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class MaleTop
        {
            internal static readonly IClothing MaleTopInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[79];
                    output.MaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.cockatrice/1579");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);

                    output["Clothing1"].Sprite(input.A.HasBelly ? input.Sprites.Cockatrice3[83 + input.U.BodySize] : input.Sprites.Cockatrice3[79 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class MaleTop2
        {
            internal static readonly IClothing MaleTop2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[79];
                    output.MaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.cockatrice/1579");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Sprite(input.Sprites.Cockatrice3[75 + input.U.BodySize]);
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
                    output["Clothing1"].Layer(18);
                    output["Clothing2"].Layer(7);
                    output["Clothing2"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                        output["Clothing2"].SetOffset(0, 0);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[4 + input.U.BreastSize]);
                        output["Clothing2"].SetOffset(0, 0);
                    }
                    else
                    {
                        output["Clothing2"].SetOffset(0, -1 * .625f);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.CockatriceSkin, input.U.SkinColor));
                    output["Clothing2"].Sprite(input.Sprites.Cockatrice3[0 + input.U.BodySize]);
                });
            });
        }

        private static class Cuirass
        {
            internal static readonly BindableClothing<IOverSizeParameters> CuirassInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Cockatrice3[120];
                    output.RevealsBreasts = true;
                    output.OccupiesAllSlots = true;
                    output.FixedColor = true;
                    output.ClothingId = new ClothingId("base.cockatrice/61601");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing4"].Layer(7);
                    output["Clothing4"].Coloring(Color.white);
                    output["Clothing3"].Layer(12);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing2"].Layer(7);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[100]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        if (input.U.BreastSize < 2)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[96]);
                        }
                        else if (input.U.BreastSize < 4)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[97]);
                        }
                        else if (input.U.BreastSize < 6)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[98]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[99]);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[119]);
                    }

                    if (input.A.HasBelly)
                    {
                        output["Clothing2"].Sprite(null);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Cockatrice3[109 + input.U.BodySize] : input.Sprites.Cockatrice3[113 + input.U.BodySize]);
                    }

                    output["Clothing3"].Sprite(input.U.HasBreasts ? input.Sprites.Cockatrice3[101 + input.U.BodySize] : input.Sprites.Cockatrice3[105 + input.U.BodySize]);

                    output["Clothing4"].Sprite(input.A.GetWeaponSprite() == 1 ? input.Sprites.Cockatrice3[118] : input.Sprites.Cockatrice3[117]);
                });
            });
        }

        private static class GenericBot1
        {
            internal static readonly IClothing GenericBot1Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians3[121];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.cockatrice/1521");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(12);
                    output["Clothing1"].Layer(13);
                    if (input.U.DickSize > 0)
                    {
                        if (input.U.DickSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[20]);
                        }
                        else if (input.U.DickSize > 5)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[22]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[21]);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(null);
                    }

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Cockatrice3[12 + input.U.BodySize] : input.Sprites.Cockatrice3[16 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericBot2
        {
            internal static readonly IClothing GenericBot2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians3[137];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.cockatrice/1537");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(13);

                    output["Clothing2"].Layer(12);

                    output["Clothing2"].Coloring(Color.white);

                    if (input.U.DickSize > 0)
                    {
                        if (input.U.DickSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[32]);
                        }
                        else if (input.U.DickSize > 5)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[34]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[33]);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[31]);
                    }

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Cockatrice3[23 + input.U.BodySize] : input.Sprites.Cockatrice3[27 + input.U.BodySize]);

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
                    output.DiscardSprite = input.Sprites.Avians3[140];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.cockatrice/1540");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(13);
                    output["Clothing2"].Layer(12);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Sprite(input.Sprites.Cockatrice3[35]);

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Cockatrice3[23 + input.U.BodySize] : input.Sprites.Cockatrice3[27 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericBot4
        {
            internal static readonly IClothing GenericBot4Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Cockatrice3[47];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.cockatrice/61602");
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(12);

                    output["Clothing2"].Coloring(Color.white);

                    output["Clothing1"].Layer(13);

                    output["Clothing1"].Coloring(Color.white);

                    if (input.U.DickSize > 0)
                    {
                        if (input.U.DickSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[44]);
                        }
                        else if (input.U.DickSize > 5)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[46]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[45]);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(null);
                    }

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Cockatrice3[36 + input.U.BodySize] : input.Sprites.Cockatrice3[40 + input.U.BodySize]);
                });
            });
        }
    }
}
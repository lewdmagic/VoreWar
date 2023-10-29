﻿#region

using System;
using UnityEngine;

#endregion

internal static class Vipers
{
    private static readonly float xOffset = -7.5f; //12 pixels * 5/8

    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Default<OverSizeParameters>, builder =>
    {
        builder.Setup(output =>
        {
            output.DickSizes = () => 8;
            output.BreastSizes = () => 8;

            output.BodySizes = 0;
            output.HairStyles = 0;
            output.EyeTypes = 4;
            output.SpecialAccessoryCount = 16; // hood
            output.AccessoryColors = 0;
            output.HairColors = 0;
            output.MouthTypes = 0;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.ViperSkin);
            output.EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.ViperSkin);
            output.ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.ViperSkin);
            output.BodyAccentTypes1 = 7; // extra pattern
            output.TailTypes = 2; // scale pattern on/off

            output.WeightGainDisabled = true;
            output.GentleAnimation = true;

            output.ExtendedBreastSprites = true;
            output.AllowedMainClothingTypes.Set(
                ViperArmour1TypeFull.ViperArmour1TypeFullInstance,
                ViperArmour1TypeNoGloves.ViperArmour1TypeNoGlovesInstance,
                ViperArmour1TypeNoCap.ViperArmour1TypeNoCapInstance,
                ViperArmour1TypeBare.ViperArmour1TypeBareInstance,
                ViperArmour2TypeFull.ViperArmour2TypeFullInstance,
                ViperArmour2TypeNoGloves.ViperArmour2TypeNoGlovesInstance,
                ViperArmour2TypeNoCap.ViperArmour2TypeNoCapInstance,
                ViperArmour2TypeBare.ViperArmour2TypeBareInstance,
                ViperArmour3TypeFull.ViperArmour3TypeFullInstance,
                ViperArmour3TypeNoGloves.ViperArmour3TypeNoGlovesInstance,
                ViperArmour3TypeNoCap.ViperArmour3TypeNoCapInstance,
                ViperArmour3TypeBare.ViperArmour3TypeBareInstance,
                ViperArmour4TypeFull.ViperArmour4TypeFullInstance,
                ViperArmour4TypeNoGloves.ViperArmour4TypeNoGlovesInstance,
                ViperArmour4TypeNoCap.ViperArmour4TypeNoCapInstance,
                ViperArmour4TypeBare.ViperArmour4TypeBareInstance,
                ViperRuler1TypeFull.ViperRuler1TypeFullInstance,
                ViperRuler1TypeNoGloves.ViperRuler1TypeNoGlovesInstance,
                ViperRuler1TypeNoCap.ViperRuler1TypeNoCapInstance,
                ViperRuler1TypeBare.ViperRuler1TypeBareInstance,
                ViperRuler2TypeFull.ViperRuler2TypeFullInstance,
                ViperRuler2TypeNoGloves.ViperRuler2TypeNoGlovesInstance,
                ViperRuler2TypeNoCap.ViperRuler2TypeNoCapInstance,
                ViperRuler2TypeBare.ViperRuler2TypeBareInstance
            );

            output.AllowedWaistTypes.Set(
            );

            output.AllowedClothingHatTypes.Clear();

            output.AvoidedMainClothingTypes = 0;
            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.ViperSkin);
        });


        builder.RunBefore(CommonRaceCode.MakeBreastOversize(28 * 28));

        builder.RenderSingle(SpriteType.Head, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.TailType == 0)
            {
                if (input.Actor.IsEating || input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Vipers1[3]);
                    return;
                }

                output.Sprite(input.Sprites.Vipers1[2]);
            }
            else
            {
                if (input.Actor.IsEating || input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Vipers4[3]);
                    return;
                }

                output.Sprite(input.Sprites.Vipers4[2]);
            }
        });

        builder.RenderSingle(SpriteType.Eyes, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.EyeColor));
            output.Sprite(input.Sprites.Vipers1[44 + input.Actor.Unit.EyeType]);
        });
        builder.RenderSingle(SpriteType.SecondaryEyes, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Vipers1[40 + input.Actor.Unit.EyeType]);
        });
        builder.RenderSingle(SpriteType.Mouth, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Vipers1[5]);
            }
            else if (input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Vipers1[4]);
            }
        });

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.TailType == 0)
            {
                if (input.Actor.Unit.HasBreasts)
                {
                    output.Sprite(input.Sprites.Vipers1[0]);
                    return;
                }

                output.Sprite(input.Sprites.Vipers1[1]);
            }
            else
            {
                if (input.Actor.Unit.HasBreasts)
                {
                    output.Sprite(input.Sprites.Vipers4[0]);
                    return;
                }

                output.Sprite(input.Sprites.Vipers4[1]);
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ExtraColor1));
            output.Sprite(input.Sprites.Vipers1[6 + (input.Actor.IsAttacking ? 1 : 0) + 2 * input.Actor.Unit.BodyAccentType1]);
        }); // extra pattern
        builder.RenderSingle(SpriteType.BodyAccent2, 12, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.Predator == false)
            {
                return;
            }

            int size2;
            if (Config.LamiaUseTailAsSecondBelly && (input.Actor.PredatorComponent.Stomach2ndFullness > 0 || input.Actor.PredatorComponent.TailFullness > 0))
            {
                size2 = Math.Min(input.Actor.GetStomach2Size(19, 0.9f) + input.Actor.GetTailSize(19, 0.9f), 19);
            }
            else if (input.Actor.PredatorComponent.TailFullness > 0)
            {
                size2 = input.Actor.GetTailSize(19, 0.9f);
            }
            else
            {
                return;
            }

            if (input.Actor.Unit.TailType == 0)
            {
                if (size2 == 19 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach2, PreyLocation.tail))
                {
                    output.Sprite(input.Sprites.Vipers3[1]);
                }
                else if (size2 == 19 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach2, PreyLocation.tail))
                {
                    output.Sprite(input.Sprites.Vipers3[0]);
                }
                else
                {
                    output.Sprite(input.Sprites.Vipers2[80 + size2]);
                }
            }
            else
            {
                if (size2 == 19 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach2, PreyLocation.tail))
                {
                    output.Sprite(input.Sprites.Vipers4[47]);
                }
                else if (size2 == 19 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach2, PreyLocation.tail))
                {
                    output.Sprite(input.Sprites.Vipers4[46]);
                }
                else
                {
                    output.Sprite(input.Sprites.Vipers4[48 + size2]);
                }
            }
        }); // second stomach

        builder.RenderSingle(SpriteType.BodyAccent3, 11, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.TailType == 0)
            {
                output.Sprite(input.Sprites.Vipers1[37]);
            }
            else
            {
                output.Sprite(input.Sprites.Vipers4[21]);
            }
        }); // default tail

        builder.RenderSingle(SpriteType.BodyAccent4, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Vipers1[38 + (input.Actor.IsAttacking ? 1 : 0)]);
        }); // arms
        builder.RenderSingle(SpriteType.BodyAccent5, 10, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.SkinColor));
            if (Config.HideCocks)
            {
                return;
            }

            if (Config.HideViperSlits)
            {
                return;
            }


            if (input.Actor.Unit.HasDick == false)
            {
                if (input.Actor.IsUnbirthing)
                {
                    output.Sprite(input.Sprites.Vipers1[49]);
                    return;
                }

                output.Sprite(input.Sprites.Vipers1[48]);
            }
            else
            {
                if (input.Actor.IsErect() || input.Actor.IsCockVoring)
                {
                    output.Sprite(input.Sprites.Vipers1[52]);
                    return;
                }

                output.Sprite(input.Sprites.Vipers1[51]);
            }
        }); // slit outside

        builder.RenderSingle(SpriteType.BodyAccent6, 10, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (Config.HideCocks)
            {
                return;
            }

            if (Config.HideViperSlits)
            {
                return;
            }

            if (input.Actor.Unit.HasDick == false)
            {
                if (input.Actor.IsUnbirthing)
                {
                    output.Sprite(input.Sprites.Vipers1[50]);
                }
            }
            else
            {
                if (input.Actor.IsErect() || input.Actor.IsCockVoring)
                {
                    output.Sprite(input.Sprites.Vipers1[53]);
                }
            }
        }); // slit inside

        builder.RenderSingle(SpriteType.BodyAccent7, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.TailType == 0)
            {
                if (input.Actor.GetStomachSize() == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                {
                    output.Sprite(input.Sprites.Vipers1[99]);
                }
                else if (input.Actor.GetStomachSize() == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    output.Sprite(input.Sprites.Vipers1[98]);
                }
                else if (input.Actor.GetStomachSize() >= 14)
                {
                    output.Sprite(input.Sprites.Vipers1[97]);
                }
                else if (input.Actor.GetStomachSize() >= 12)
                {
                    output.Sprite(input.Sprites.Vipers1[96]);
                }
            }
            else
            {
                if (input.Actor.GetStomachSize() == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                {
                    output.Sprite(input.Sprites.Vipers4[27]);
                }
                else if (input.Actor.GetStomachSize() == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    output.Sprite(input.Sprites.Vipers4[26]);
                }
                else if (input.Actor.GetStomachSize() >= 14)
                {
                    output.Sprite(input.Sprites.Vipers4[25]);
                }
                else if (input.Actor.GetStomachSize() >= 12)
                {
                    output.Sprite(input.Sprites.Vipers4[24]);
                }
            }
        }); // middle tail

        builder.RenderSingle(SpriteType.BodyAccent8, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.Predator == false)
            {
                return;
            }

            int size2;
            if (Config.LamiaUseTailAsSecondBelly && (input.Actor.PredatorComponent.Stomach2ndFullness > 0 || input.Actor.PredatorComponent.TailFullness > 0))
            {
                size2 = Math.Min(input.Actor.GetStomach2Size(19, 0.9f) + input.Actor.GetTailSize(19, 0.9f), 19);
            }
            else if (input.Actor.PredatorComponent.TailFullness > 0)
            {
                size2 = input.Actor.GetTailSize(19, 0.9f);
            }
            else
            {
                return;
            }

            if (input.Actor.Unit.TailType == 0)
            {
                if (size2 >= 19)
                {
                    output.Sprite(input.Sprites.Vipers4[70]);
                }
                else if (size2 >= 18)
                {
                    output.Sprite(input.Sprites.Vipers4[69]);
                }
                else if (size2 >= 15)
                {
                    output.Sprite(input.Sprites.Vipers4[68]);
                }
            }
            else
            {
                if (size2 >= 19)
                {
                    output.Sprite(input.Sprites.Vipers4[73]);
                }
                else if (size2 >= 18)
                {
                    output.Sprite(input.Sprites.Vipers4[72]);
                }
                else if (size2 >= 15)
                {
                    output.Sprite(input.Sprites.Vipers4[71]);
                }
            }
        }); // middle tail B

        builder.RenderSingle(SpriteType.BodyAccessory, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.TailType == 0)
            {
                output.Sprite(input.Sprites.Vipers1[21 + input.Actor.Unit.SpecialAccessoryType]);
            }
            else
            {
                output.Sprite(input.Sprites.Vipers4[5 + input.Actor.Unit.SpecialAccessoryType]);
            }
        }); // hood

        builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(28 * 28));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.Vipers2[27]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 26)
                {
                    output.Sprite(input.Sprites.Vipers2[26]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 24)
                {
                    output.Sprite(input.Sprites.Vipers2[25]);
                    return;
                }

                if (leftSize > 24)
                {
                    leftSize = 24;
                }

                output.Sprite(input.Sprites.Vipers2[0 + leftSize]);
            }
            else
            {
                output.Sprite(input.Sprites.Vipers2[0 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.SecondaryBreasts, 16, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(28 * 28));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.Vipers2[55]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 26)
                {
                    output.Sprite(input.Sprites.Vipers2[54]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 24)
                {
                    output.Sprite(input.Sprites.Vipers2[53]);
                    return;
                }

                if (rightSize > 24)
                {
                    rightSize = 24;
                }

                output.Sprite(input.Sprites.Vipers2[28 + rightSize]);
            }
            else
            {
                output.Sprite(input.Sprites.Vipers2[28 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.Belly, 9, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.SkinColor));
            if (!Config.LamiaUseTailAsSecondBelly)
            {
                if (input.Actor.HasBelly)
                {
                    int size0 = input.Actor.GetCombinedStomachSize();

                    if (input.Actor.Unit.TailType == 0)
                    {
                        if (size0 == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true,
                                PreyLocation.stomach, PreyLocation.stomach2, PreyLocation.womb))
                        {
                            output.Sprite(input.Sprites.Vipers1[95]);
                            return;
                        }

                        if (size0 == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false,
                                PreyLocation.stomach, PreyLocation.stomach2, PreyLocation.womb))
                        {
                            output.Sprite(input.Sprites.Vipers1[94]);
                            return;
                        }

                        output.Sprite(input.Sprites.Vipers1[78 + size0]);
                        return;
                    }

                    if (size0 == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true,
                            PreyLocation.stomach, PreyLocation.stomach2, PreyLocation.womb))
                    {
                        output.Sprite(input.Sprites.Vipers4[45]);
                        return;
                    }

                    if (size0 == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false,
                            PreyLocation.stomach, PreyLocation.stomach2, PreyLocation.womb))
                    {
                        output.Sprite(input.Sprites.Vipers4[44]);
                        return;
                    }

                    output.Sprite(input.Sprites.Vipers4[28 + size0]);
                    return;
                }

                return;
            }

            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize();

                if (input.Actor.Unit.TailType == 0)
                {
                    if (size == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true,
                            PreyLocation.stomach, PreyLocation.womb))
                    {
                        output.Sprite(input.Sprites.Vipers1[95]);
                    }
                    else if (size == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false,
                                 PreyLocation.stomach, PreyLocation.womb))
                    {
                        output.Sprite(input.Sprites.Vipers1[94]);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.Vipers1[78 + size]);
                    }
                }
                else
                {
                    if (size == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true,
                            PreyLocation.stomach, PreyLocation.womb))
                    {
                        output.Sprite(input.Sprites.Vipers4[45]);
                    }
                    else if (size == 15 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false,
                                 PreyLocation.stomach, PreyLocation.womb))
                    {
                        output.Sprite(input.Sprites.Vipers4[44]);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.Vipers4[28 + size]);
                    }
                }
            }
        });

        builder.RenderSingle(SpriteType.Dick, 15, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsCockVoring)
            {
                output.Sprite(input.Sprites.Vipers1[62 + input.Actor.Unit.DickSize]);
            }
            else if (input.Actor.IsErect())
            {
                output.Sprite(input.Sprites.Vipers1[54 + input.Actor.Unit.DickSize]);
            }
        });

        builder.RenderSingle(SpriteType.Balls, 14, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            int ballsize = input.Actor.GetBallSize(20, 1.5f);

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(20, 1.5f) == 20)
            {
                output.Sprite(input.Sprites.Vipers2[79]);
            }
            else if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(20, 1.2f) == 20)
            {
                output.Sprite(input.Sprites.Vipers2[78]);
            }
            else if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(20, 1.35f) == 20)
            {
                output.Sprite(input.Sprites.Vipers2[77]);
            }
            else if (input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                output.Sprite(input.Sprites.Vipers2[56 + ballsize]);
            }
        });

        builder.RenderSingle(SpriteType.Weapon, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                output.Sprite(input.Sprites.Vipers1[70 + input.Actor.GetWeaponSprite()]);
            }
        });


        builder.RunBefore((input, output) =>
        {
            if (!Config.LamiaUseTailAsSecondBelly)
            {
                if (input.Actor.GetCombinedStomachSize() > 14)
                {
                    output.changeSprite(SpriteType.Belly).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Breasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.SecondaryBreasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Balls).AddOffset(xOffset, -8 * .625f);
                    output.changeSprite(SpriteType.Dick).AddOffset(0, -8 * .625f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, -8 * .625f);
                    output.changeSprite(SpriteType.BodyAccent6).AddOffset(0, -8 * .625f);
                }
                else if (input.Actor.GetCombinedStomachSize() > 12)
                {
                    output.changeSprite(SpriteType.Belly).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Breasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.SecondaryBreasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Balls).AddOffset(xOffset, -6 * .625f);
                    output.changeSprite(SpriteType.Dick).AddOffset(0, -6 * .625f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, -6 * .625f);
                    output.changeSprite(SpriteType.BodyAccent6).AddOffset(0, -6 * .625f);
                }
                else if (input.Actor.GetCombinedStomachSize() > 10)
                {
                    output.changeSprite(SpriteType.Belly).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Breasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.SecondaryBreasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Balls).AddOffset(xOffset, -4 * .625f);
                    output.changeSprite(SpriteType.Dick).AddOffset(0, -4 * .625f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, -4 * .625f);
                    output.changeSprite(SpriteType.BodyAccent6).AddOffset(0, -4 * .625f);
                }
                else if (input.Actor.GetCombinedStomachSize() > 8)
                {
                    output.changeSprite(SpriteType.Belly).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Breasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.SecondaryBreasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Balls).AddOffset(xOffset, -2 * .625f);
                    output.changeSprite(SpriteType.Dick).AddOffset(0, -2 * .625f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, -2 * .625f);
                    output.changeSprite(SpriteType.BodyAccent6).AddOffset(0, -2 * .625f);
                }
                else if (input.Actor.GetCombinedStomachSize() > 6)
                {
                    output.changeSprite(SpriteType.Belly).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Breasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.SecondaryBreasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Balls).AddOffset(xOffset, -1 * .625f);
                    output.changeSprite(SpriteType.Dick).AddOffset(0, -1 * .625f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, -1 * .625f);
                    output.changeSprite(SpriteType.BodyAccent6).AddOffset(0, -1 * .625f);
                }
                else
                {
                    output.changeSprite(SpriteType.Belly).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Breasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.SecondaryBreasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Balls).AddOffset(xOffset, 0);
                }
            }
            else
            {
                if (input.Actor.GetStomachSize() > 14)
                {
                    output.changeSprite(SpriteType.Belly).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Breasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.SecondaryBreasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Balls).AddOffset(xOffset, -8 * .625f);
                    output.changeSprite(SpriteType.Dick).AddOffset(0, -8 * .625f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, -8 * .625f);
                    output.changeSprite(SpriteType.BodyAccent6).AddOffset(0, -8 * .625f);
                }
                else if (input.Actor.GetStomachSize() > 12)
                {
                    output.changeSprite(SpriteType.Belly).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Breasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.SecondaryBreasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Balls).AddOffset(xOffset, -6 * .625f);
                    output.changeSprite(SpriteType.Dick).AddOffset(0, -6 * .625f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, -6 * .625f);
                    output.changeSprite(SpriteType.BodyAccent6).AddOffset(0, -6 * .625f);
                }
                else if (input.Actor.GetStomachSize() > 10)
                {
                    output.changeSprite(SpriteType.Belly).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Breasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.SecondaryBreasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Balls).AddOffset(xOffset, -4 * .625f);
                    output.changeSprite(SpriteType.Dick).AddOffset(0, -4 * .625f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, -4 * .625f);
                    output.changeSprite(SpriteType.BodyAccent6).AddOffset(0, -4 * .625f);
                }
                else if (input.Actor.GetStomachSize() > 8)
                {
                    output.changeSprite(SpriteType.Belly).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Breasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.SecondaryBreasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Balls).AddOffset(xOffset, -2 * .625f);
                    output.changeSprite(SpriteType.Dick).AddOffset(0, -2 * .625f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, -2 * .625f);
                    output.changeSprite(SpriteType.BodyAccent6).AddOffset(0, -2 * .625f);
                }
                else if (input.Actor.GetStomachSize() > 6)
                {
                    output.changeSprite(SpriteType.Belly).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Breasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.SecondaryBreasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Balls).AddOffset(xOffset, -1 * .625f);
                    output.changeSprite(SpriteType.Dick).AddOffset(0, -1 * .625f);
                    output.changeSprite(SpriteType.BodyAccent5).AddOffset(0, -1 * .625f);
                    output.changeSprite(SpriteType.BodyAccent6).AddOffset(0, -1 * .625f);
                }
                else
                {
                    output.changeSprite(SpriteType.Belly).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Breasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.SecondaryBreasts).AddOffset(xOffset, 0);
                    output.changeSprite(SpriteType.Balls).AddOffset(xOffset, 0);
                }
            }
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;


            unit.BodyAccentType1 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes1);

            unit.ExtraColor1 = unit.SkinColor;

            unit.TailType = 0;
        });
    });


    private static class ViperArmour1TypeFull
    {
        internal static IClothing<IOverSizeParameters> ViperArmour1TypeFullInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers4[22];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1422;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing4"].Layer(7);
                output["Clothing2"].Layer(17);
                output["Clothing5"].Layer(7);
                output["Clothing5"].Coloring(Color.white);
                output["Clothing3"].Layer(7);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[10]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[2 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[11]);
                }

                output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);
                output["Clothing5"].Sprite(input.Sprites.Vipers3[17]);

                bool attacking = input.Actor.IsAttacking;
                output["Clothing3"].Sprite(input.Sprites.Vipers3[13 + (attacking ? 1 : 0)]);
                output["Clothing4"].Sprite(input.Sprites.Vipers3[15 + (attacking ? 1 : 0)]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
                output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperArmour1TypeNoGloves
    {
        internal static IClothing<IOverSizeParameters> ViperArmour1TypeNoGlovesInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers4[22];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1422;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(17);
                output["Clothing3"].Layer(7);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[10]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[2 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[11]);
                }

                output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);
                output["Clothing3"].Sprite(input.Sprites.Vipers3[17]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperArmour1TypeNoCap
    {
        internal static IClothing<IOverSizeParameters> ViperArmour1TypeNoCapInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers4[22];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1422;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing4"].Layer(7);
                output["Clothing2"].Layer(17);
                output["Clothing3"].Layer(7);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[10]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[2 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[11]);
                }

                output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);

                bool attacking = input.Actor.IsAttacking;
                output["Clothing3"].Sprite(input.Sprites.Vipers3[13 + (attacking ? 1 : 0)]);
                output["Clothing4"].Sprite(input.Sprites.Vipers3[15 + (attacking ? 1 : 0)]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
                output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperArmour1TypeBare
    {
        internal static IClothing<IOverSizeParameters> ViperArmour1TypeBareInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers4[22];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1422;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(17);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[10]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[2 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[11]);
                }

                output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperArmour2TypeFull
    {
        internal static IClothing<IOverSizeParameters> ViperArmour2TypeFullInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers3[97];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1497;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing4"].Layer(7);
                output["Clothing2"].Layer(17);
                output["Clothing5"].Layer(7);
                output["Clothing5"].Coloring(Color.white);
                output["Clothing3"].Layer(7);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[18]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[20 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[19]);
                }

                output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);
                output["Clothing5"].Sprite(input.Sprites.Vipers3[30]);

                bool attacking = input.Actor.IsAttacking;
                output["Clothing3"].Sprite(input.Sprites.Vipers3[28 + (attacking ? 1 : 0)]);
                output["Clothing4"].Sprite(input.Sprites.Vipers3[15 + (attacking ? 1 : 0)]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
                output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperArmour2TypeNoGloves
    {
        internal static IClothing<IOverSizeParameters> ViperArmour2TypeNoGlovesInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers3[97];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1497;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(17);
                output["Clothing3"].Layer(7);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[18]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[20 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[19]);
                }

                output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);
                output["Clothing3"].Sprite(input.Sprites.Vipers3[30]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperArmour2TypeNoCap
    {
        internal static IClothing<IOverSizeParameters> ViperArmour2TypeNoCapInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers3[97];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1497;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing4"].Layer(7);
                output["Clothing2"].Layer(17);
                output["Clothing3"].Layer(7);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[18]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[20 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[19]);
                }

                output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);

                bool attacking = input.Actor.IsAttacking;
                output["Clothing3"].Sprite(input.Sprites.Vipers3[28 + (attacking ? 1 : 0)]);
                output["Clothing4"].Sprite(input.Sprites.Vipers3[15 + (attacking ? 1 : 0)]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
                output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperArmour2TypeBare
    {
        internal static IClothing<IOverSizeParameters> ViperArmour2TypeBareInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers3[97];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1497;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(17);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[18]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[20 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[19]);
                }

                output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperArmour3TypeFull
    {
        internal static IClothing<IOverSizeParameters> ViperArmour3TypeFullInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers4[23];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1423;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing4"].Layer(7);
                output["Clothing2"].Layer(17);
                output["Clothing5"].Layer(7);
                output["Clothing5"].Coloring(Color.white);
                output["Clothing3"].Layer(7);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[40]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[31 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[41]);
                }

                output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);
                output["Clothing5"].Sprite(input.Sprites.Vipers3[39]);

                bool attacking = input.Actor.IsAttacking;
                output["Clothing3"].Sprite(input.Sprites.Vipers3[42 + (attacking ? 1 : 0)]);
                output["Clothing4"].Sprite(input.Sprites.Vipers3[15 + (attacking ? 1 : 0)]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
                output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperArmour3TypeNoGloves
    {
        internal static IClothing<IOverSizeParameters> ViperArmour3TypeNoGlovesInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers4[23];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1423;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(17);
                output["Clothing3"].Layer(7);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[40]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[31 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[41]);
                }

                output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);
                output["Clothing3"].Sprite(input.Sprites.Vipers3[39]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperArmour3TypeNoCap
    {
        internal static IClothing<IOverSizeParameters> ViperArmour3TypeNoCapInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers4[23];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1423;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing4"].Layer(7);
                output["Clothing2"].Layer(17);
                output["Clothing3"].Layer(7);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[40]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[31 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[41]);
                }

                output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);

                bool attacking = input.Actor.IsAttacking;
                output["Clothing3"].Sprite(input.Sprites.Vipers3[42 + (attacking ? 1 : 0)]);
                output["Clothing4"].Sprite(input.Sprites.Vipers3[15 + (attacking ? 1 : 0)]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
                output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperArmour3TypeBare
    {
        internal static IClothing<IOverSizeParameters> ViperArmour3TypeBareInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers4[23];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1423;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(17);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[40]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[31 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[41]);
                }

                output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperRuler1TypeFull
    {
        internal static IClothing<IOverSizeParameters> ViperRuler1TypeFullInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers3[98];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1498;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(17);
                output["Clothing4"].Layer(7);
                output["Clothing4"].Coloring(Color.white);
                output["Clothing3"].Layer(7);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[52]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[44 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[53]);
                }

                output["Clothing2"].Sprite(input.Sprites.Vipers3[54]);
                output["Clothing4"].Sprite(input.Sprites.Vipers3[57]);

                bool attacking = input.Actor.IsAttacking;
                output["Clothing3"].Sprite(input.Sprites.Vipers3[55 + (attacking ? 1 : 0)]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperRuler1TypeNoGloves
    {
        internal static IClothing<IOverSizeParameters> ViperRuler1TypeNoGlovesInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers3[98];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1498;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(17);
                output["Clothing3"].Layer(7);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[52]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[44 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[53]);
                }

                output["Clothing2"].Sprite(input.Sprites.Vipers3[54]);
                output["Clothing3"].Sprite(input.Sprites.Vipers3[57]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperRuler1TypeNoCap
    {
        internal static IClothing<IOverSizeParameters> ViperRuler1TypeNoCapInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers3[98];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1498;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(17);
                output["Clothing3"].Layer(7);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[52]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[44 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[53]);
                }

                output["Clothing2"].Sprite(input.Sprites.Vipers3[54]);

                bool attacking = input.Actor.IsAttacking;
                output["Clothing3"].Sprite(input.Sprites.Vipers3[55 + (attacking ? 1 : 0)]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperRuler1TypeBare
    {
        internal static IClothing<IOverSizeParameters> ViperRuler1TypeBareInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers3[98];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1498;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(17);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[52]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[44 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[53]);
                }

                output["Clothing2"].Sprite(input.Sprites.Vipers3[54]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperArmour4TypeFull
    {
        internal static IClothing<IOverSizeParameters> ViperArmour4TypeFullInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers3[97];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1497;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing6"].Layer(7);
                output["Clothing4"].Layer(7);
                output["Clothing2"].Layer(17);
                output["Clothing5"].Layer(7);
                output["Clothing5"].Coloring(Color.white);
                output["Clothing3"].Layer(7);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[68]);
                    output["Clothing2"].Sprite(input.Sprites.Vipers3[78]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[60 + input.Actor.Unit.BreastSize]);
                    output["Clothing2"].Sprite(input.Sprites.Vipers3[70 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[69]);
                    output["Clothing2"].Sprite(input.Sprites.Vipers3[79]);
                }

                output["Clothing5"].Sprite(input.Sprites.Vipers3[58]);
                output["Clothing6"].Sprite(input.Sprites.Vipers3[59]);

                bool attacking = input.Actor.IsAttacking;
                output["Clothing3"].Sprite(input.Sprites.Vipers3[80 + (attacking ? 1 : 0)]);
                output["Clothing4"].Sprite(input.Sprites.Vipers3[82 + (attacking ? 1 : 0)]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
                output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
                output["Clothing6"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperArmour4TypeNoGloves
    {
        internal static IClothing<IOverSizeParameters> ViperArmour4TypeNoGlovesInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers3[97];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1497;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing4"].Layer(7);
                output["Clothing2"].Layer(17);
                output["Clothing3"].Layer(7);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[68]);
                    output["Clothing2"].Sprite(input.Sprites.Vipers3[78]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[60 + input.Actor.Unit.BreastSize]);
                    output["Clothing2"].Sprite(input.Sprites.Vipers3[70 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[69]);
                    output["Clothing2"].Sprite(input.Sprites.Vipers3[79]);
                }

                output["Clothing3"].Sprite(input.Sprites.Vipers3[58]);
                output["Clothing4"].Sprite(input.Sprites.Vipers3[59]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
                output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperArmour4TypeNoCap
    {
        internal static IClothing<IOverSizeParameters> ViperArmour4TypeNoCapInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers3[97];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1497;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing4"].Layer(7);
                output["Clothing2"].Layer(17);
                output["Clothing3"].Layer(7);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[68]);
                    output["Clothing2"].Sprite(input.Sprites.Vipers3[78]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[60 + input.Actor.Unit.BreastSize]);
                    output["Clothing2"].Sprite(input.Sprites.Vipers3[70 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[69]);
                    output["Clothing2"].Sprite(input.Sprites.Vipers3[79]);
                }

                bool attacking = input.Actor.IsAttacking;
                output["Clothing3"].Sprite(input.Sprites.Vipers3[80 + (attacking ? 1 : 0)]);
                output["Clothing4"].Sprite(input.Sprites.Vipers3[82 + (attacking ? 1 : 0)]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
                output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperArmour4TypeBare
    {
        internal static IClothing<IOverSizeParameters> ViperArmour4TypeBareInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers3[97];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1497;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(17);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[68]);
                    output["Clothing2"].Sprite(input.Sprites.Vipers3[78]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[60 + input.Actor.Unit.BreastSize]);
                    output["Clothing2"].Sprite(input.Sprites.Vipers3[70 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[69]);
                    output["Clothing2"].Sprite(input.Sprites.Vipers3[79]);
                }

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperRuler2TypeFull
    {
        internal static IClothing<IOverSizeParameters> ViperRuler2TypeFullInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers3[99];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1499;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(17);
                output["Clothing4"].Layer(7);
                output["Clothing4"].Coloring(Color.white);
                output["Clothing3"].Layer(7);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[92]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[84 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[93]);
                }

                output["Clothing2"].Sprite(input.Sprites.Vipers3[54]);
                output["Clothing4"].Sprite(input.Sprites.Vipers3[96]);

                bool attacking = input.Actor.IsAttacking;
                output["Clothing3"].Sprite(input.Sprites.Vipers3[94 + (attacking ? 1 : 0)]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperRuler2TypeNoGloves
    {
        internal static IClothing<IOverSizeParameters> ViperRuler2TypeNoGlovesInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers3[99];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1499;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(17);
                output["Clothing3"].Layer(7);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[92]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[84 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[93]);
                }

                output["Clothing2"].Sprite(input.Sprites.Vipers3[54]);
                output["Clothing3"].Sprite(input.Sprites.Vipers3[96]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperRuler2TypeNoCap
    {
        internal static IClothing<IOverSizeParameters> ViperRuler2TypeNoCapInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers3[99];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1499;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(17);
                output["Clothing3"].Layer(7);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[92]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[84 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[93]);
                }

                output["Clothing2"].Sprite(input.Sprites.Vipers3[54]);

                bool attacking = input.Actor.IsAttacking;
                output["Clothing3"].Sprite(input.Sprites.Vipers3[94 + (attacking ? 1 : 0)]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class ViperRuler2TypeBare
    {
        internal static IClothing<IOverSizeParameters> ViperRuler2TypeBareInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Vipers3[99];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1499;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(17);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[92]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[84 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Vipers3[93]);
                }

                output["Clothing2"].Sprite(input.Sprites.Vipers3[54]);

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ViperSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }
}
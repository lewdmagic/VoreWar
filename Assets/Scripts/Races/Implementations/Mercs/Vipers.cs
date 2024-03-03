#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.Mercs
{
    internal static class Vipers
    {
        private static Func<IClothingRenderInput, IOverSizeParameters> _paramsCalc = CommonRaceCode.MakeOversizeFunc(28 * 28);
        private static readonly float XOffset = -7.5f; //12 pixels * 5/8

        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            builder.Setup((input, output) =>
            {
                output.Names("Viper", "Vipers");
                output.FlavorText(new FlavorText(
                    new Texts { },
                    new Texts { },
                    new Texts { },
                    new Dictionary<string, string>
                    {
                        [WeaponNames.Mace] = "Arc Blade",
                        [WeaponNames.Axe] = "Fusion Blade",
                        [WeaponNames.SimpleBow] = "Plasma Pistol",
                        [WeaponNames.CompoundBow] = "Plasma Rifle"
                    }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 18,
                    StomachSize = 25,
                    HasTail = true,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.CockVore, VoreType.BreastVore, VoreType.Anal, VoreType.TailVore },
                    PowerAdjustment = 1.4f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new StatRange(10, 16),
                        Dexterity = new StatRange(14, 20),
                        Endurance = new StatRange(8, 14),
                        Mind = new StatRange(10, 16),
                        Will = new StatRange(10, 16),
                        Agility = new StatRange(14, 20),
                        Voracity = new StatRange(18, 28),
                        Stomach = new StatRange(14, 20),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.ArtfulDodge,
                        TraitType.DualStomach,
                        TraitType.RangedVore,
                        TraitType.KeenShot,
                        TraitType.SlowMetabolism,
                        TraitType.PoisonSpit
                    },
                    RaceDescription = "",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.Skintone, "Body Color");
                    buttons.SetText(ButtonType.BodyAccessoryType, "Hood Type");
                    buttons.SetText(ButtonType.ExtraColor1, "Accent Color");
                    buttons.SetText(ButtonType.TailTypes, "Tail Pattern");
                    buttons.SetText(ButtonType.BodyAccentTypes1, "Accent Pattern");
                });
                output.DickSizes = () => 8;
                output.BreastSizes = () => 8;

                output.BodySizes = 0;
                output.HairStyles = 0;
                output.EyeTypes = 4;
                output.SpecialAccessoryCount = 16; // hood
                output.AccessoryColors = 0;
                output.HairColors = 0;
                output.MouthTypes = 0;
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.ViperSkin);
                output.EyeColors = ColorPaletteMap.GetPaletteCount(SwapType.ViperSkin);
                output.ExtraColors1 = ColorPaletteMap.GetPaletteCount(SwapType.ViperSkin);
                output.BodyAccentTypes1 = 7; // extra pattern
                output.TailTypes = 2; // scale pattern on/off

                output.WeightGainDisabled = true;
                output.GentleAnimation = true;

                output.ExtendedBreastSprites = true;
                output.AllowedMainClothingTypes.Set(
                    ViperArmour1TypeFull.ViperArmour1TypeFullInstance.Create(_paramsCalc),
                    ViperArmour1TypeNoGloves.ViperArmour1TypeNoGlovesInstance.Create(_paramsCalc),
                    ViperArmour1TypeNoCap.ViperArmour1TypeNoCapInstance.Create(_paramsCalc),
                    ViperArmour1TypeBare.ViperArmour1TypeBareInstance.Create(_paramsCalc),
                    ViperArmour2TypeFull.ViperArmour2TypeFullInstance.Create(_paramsCalc),
                    ViperArmour2TypeNoGloves.ViperArmour2TypeNoGlovesInstance.Create(_paramsCalc),
                    ViperArmour2TypeNoCap.ViperArmour2TypeNoCapInstance.Create(_paramsCalc),
                    ViperArmour2TypeBare.ViperArmour2TypeBareInstance.Create(_paramsCalc),
                    ViperArmour3TypeFull.ViperArmour3TypeFullInstance.Create(_paramsCalc),
                    ViperArmour3TypeNoGloves.ViperArmour3TypeNoGlovesInstance.Create(_paramsCalc),
                    ViperArmour3TypeNoCap.ViperArmour3TypeNoCapInstance.Create(_paramsCalc),
                    ViperArmour3TypeBare.ViperArmour3TypeBareInstance.Create(_paramsCalc),
                    ViperArmour4TypeFull.ViperArmour4TypeFullInstance.Create(_paramsCalc),
                    ViperArmour4TypeNoGloves.ViperArmour4TypeNoGlovesInstance.Create(_paramsCalc),
                    ViperArmour4TypeNoCap.ViperArmour4TypeNoCapInstance.Create(_paramsCalc),
                    ViperArmour4TypeBare.ViperArmour4TypeBareInstance.Create(_paramsCalc),
                    ViperRuler1TypeFull.ViperRuler1TypeFullInstance.Create(_paramsCalc),
                    ViperRuler1TypeNoGloves.ViperRuler1TypeNoGlovesInstance.Create(_paramsCalc),
                    ViperRuler1TypeNoCap.ViperRuler1TypeNoCapInstance.Create(_paramsCalc),
                    ViperRuler1TypeBare.ViperRuler1TypeBareInstance.Create(_paramsCalc),
                    ViperRuler2TypeFull.ViperRuler2TypeFullInstance.Create(_paramsCalc),
                    ViperRuler2TypeNoGloves.ViperRuler2TypeNoGlovesInstance.Create(_paramsCalc),
                    ViperRuler2TypeNoCap.ViperRuler2TypeNoCapInstance.Create(_paramsCalc),
                    ViperRuler2TypeBare.ViperRuler2TypeBareInstance.Create(_paramsCalc)
                );

                output.AllowedWaistTypes.Set(
                );

                output.AllowedClothingHatTypes.Clear();

                output.AvoidedMainClothingTypes = 0;
                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.ViperSkin);
            });


            builder.RunBefore((input, output) => { });

            builder.RenderSingle(SpriteType.Head, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.SkinColor));
                if (input.U.TailType == 0)
                {
                    if (input.A.IsEating || input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Vipers1[3]);
                        return;
                    }

                    output.Sprite(input.Sprites.Vipers1[2]);
                }
                else
                {
                    if (input.A.IsEating || input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Vipers4[3]);
                        return;
                    }

                    output.Sprite(input.Sprites.Vipers4[2]);
                }
            });

            builder.RenderSingle(SpriteType.Eyes, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.EyeColor));
                output.Sprite(input.Sprites.Vipers1[44 + input.U.EyeType]);
            });
            builder.RenderSingle(SpriteType.SecondaryEyes, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Vipers1[40 + input.U.EyeType]);
            });
            builder.RenderSingle(SpriteType.Mouth, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Vipers1[5]);
                }
                else if (input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Vipers1[4]);
                }
            });

            builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.SkinColor));
                if (input.U.TailType == 0)
                {
                    if (input.U.HasBreasts)
                    {
                        output.Sprite(input.Sprites.Vipers1[0]);
                        return;
                    }

                    output.Sprite(input.Sprites.Vipers1[1]);
                }
                else
                {
                    if (input.U.HasBreasts)
                    {
                        output.Sprite(input.Sprites.Vipers4[0]);
                        return;
                    }

                    output.Sprite(input.Sprites.Vipers4[1]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ExtraColor1));
                output.Sprite(input.Sprites.Vipers1[6 + (input.A.IsAttacking ? 1 : 0) + 2 * input.U.BodyAccentType1]);
            }); // extra pattern
            builder.RenderSingle(SpriteType.BodyAccent2, 12, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.SkinColor));
                if (input.U.Predator == false)
                {
                    return;
                }

                int size2;
                if (Config.LamiaUseTailAsSecondBelly && (input.A.PredatorComponent.Stomach2NdFullness > 0 || input.A.PredatorComponent.TailFullness > 0))
                {
                    size2 = Math.Min(input.A.GetStomach2Size(19, 0.9f) + input.A.GetTailSize(19, 0.9f), 19);
                }
                else if (input.A.PredatorComponent.TailFullness > 0)
                {
                    size2 = input.A.GetTailSize(19, 0.9f);
                }
                else
                {
                    return;
                }

                if (input.U.TailType == 0)
                {
                    output.Sprite(input.Sprites.Vipers2[80 + size2]);
                }
                else
                {
                    output.Sprite(input.Sprites.Vipers4[48 + size2]);
                }
            }); // second stomach

            builder.RenderSingle(SpriteType.BodyAccent3, 11, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.SkinColor));
                output.Sprite(input.U.TailType == 0 ? input.Sprites.Vipers1[37] : input.Sprites.Vipers4[21]);
            }); // default tail

            builder.RenderSingle(SpriteType.BodyAccent4, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Vipers1[38 + (input.A.IsAttacking ? 1 : 0)]);
            }); // arms
            builder.RenderSingle(SpriteType.BodyAccent5, 10, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.SkinColor));
                if (Config.HideCocks)
                {
                    return;
                }

                if (Config.HideViperSlits)
                {
                    return;
                }


                if (input.U.HasDick == false)
                {
                    if (input.A.IsUnbirthing)
                    {
                        output.Sprite(input.Sprites.Vipers1[49]);
                        return;
                    }

                    output.Sprite(input.Sprites.Vipers1[48]);
                }
                else
                {
                    if (input.A.IsErect() || input.A.IsCockVoring)
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

                if (input.U.HasDick == false)
                {
                    if (input.A.IsUnbirthing)
                    {
                        output.Sprite(input.Sprites.Vipers1[50]);
                    }
                }
                else
                {
                    if (input.A.IsErect() || input.A.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.Vipers1[53]);
                    }
                }
            }); // slit inside

            builder.RenderSingle(SpriteType.BodyAccent7, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.SkinColor));
                if (input.U.TailType == 0)
                {
                    if (input.A.GetStomachSize() >= 14)
                    {
                        output.Sprite(input.Sprites.Vipers1[97]);
                    }
                    else if (input.A.GetStomachSize() >= 12)
                    {
                        output.Sprite(input.Sprites.Vipers1[96]);
                    }
                }
                else
                {
                    if (input.A.GetStomachSize() >= 14)
                    {
                        output.Sprite(input.Sprites.Vipers4[25]);
                    }
                    else if (input.A.GetStomachSize() >= 12)
                    {
                        output.Sprite(input.Sprites.Vipers4[24]);
                    }
                }
            }); // middle tail

            builder.RenderSingle(SpriteType.BodyAccent8, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.SkinColor));
                if (input.U.Predator == false)
                {
                    return;
                }

                int size2;
                if (Config.LamiaUseTailAsSecondBelly && (input.A.PredatorComponent.Stomach2NdFullness > 0 || input.A.PredatorComponent.TailFullness > 0))
                {
                    size2 = Math.Min(input.A.GetStomach2Size(19, 0.9f) + input.A.GetTailSize(19, 0.9f), 19);
                }
                else if (input.A.PredatorComponent.TailFullness > 0)
                {
                    size2 = input.A.GetTailSize(19, 0.9f);
                }
                else
                {
                    return;
                }

                if (input.U.TailType == 0)
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
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.SkinColor));
                output.Sprite(input.U.TailType == 0 ? input.Sprites.Vipers1[21 + input.U.SpecialAccessoryType] : input.Sprites.Vipers4[5 + input.U.SpecialAccessoryType]);
            }); // hood

            builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.SkinColor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.LeftBreastFullness > 0)
                {
                    int leftSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(28 * 28));

                    if (leftSize > 24)
                    {
                        leftSize = 24;
                    }

                    output.Sprite(input.Sprites.Vipers2[0 + leftSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Vipers2[0 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.SecondaryBreasts, 16, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.SkinColor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.RightBreastFullness > 0)
                {
                    int rightSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(28 * 28));

                    if (rightSize > 24)
                    {
                        rightSize = 24;
                    }

                    output.Sprite(input.Sprites.Vipers2[28 + rightSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Vipers2[28 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 9, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.SkinColor));
                if (!Config.LamiaUseTailAsSecondBelly)
                {
                    if (input.A.HasBelly)
                    {
                        int size0 = input.A.GetCombinedStomachSize();

                        if (input.U.TailType == 0)
                        {
                            output.Sprite(input.Sprites.Vipers1[78 + size0]);
                            return;
                        }

                        output.Sprite(input.Sprites.Vipers4[28 + size0]);
                        return;
                    }

                    return;
                }

                if (input.A.HasBelly)
                {
                    int size = input.A.GetStomachSize();

                    if (input.U.TailType == 0)
                    {
                        output.Sprite(input.Sprites.Vipers1[78 + size]);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.Vipers4[28 + size]);
                    }
                }
            });

            builder.RenderSingle(SpriteType.Dick, 15, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsCockVoring)
                {
                    output.Sprite(input.Sprites.Vipers1[62 + input.U.DickSize]);
                }
                else if (input.A.IsErect())
                {
                    output.Sprite(input.Sprites.Vipers1[54 + input.U.DickSize]);
                }
            });

            builder.RenderSingle(SpriteType.Balls, 14, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasDick == false)
                {
                    return;
                }

                int ballsize = input.A.GetBallSize(20, 1.5f);

                if (input.A.PredatorComponent?.BallsFullness > 0)
                {
                    output.Sprite(input.Sprites.Vipers2[56 + ballsize]);
                }
            });

            builder.RenderSingle(SpriteType.Weapon, 8, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    output.Sprite(input.Sprites.Vipers1[70 + input.A.GetWeaponSprite()]);
                }
            });


            builder.RunBefore((input, output) =>
            {
                if (!Config.LamiaUseTailAsSecondBelly)
                {
                    if (input.A.GetCombinedStomachSize() > 14)
                    {
                        output.ChangeSprite(SpriteType.Belly).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Breasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.SecondaryBreasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Balls).AddOffset(XOffset, -8 * .625f);
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, -8 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, -8 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent6).AddOffset(0, -8 * .625f);
                    }
                    else if (input.A.GetCombinedStomachSize() > 12)
                    {
                        output.ChangeSprite(SpriteType.Belly).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Breasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.SecondaryBreasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Balls).AddOffset(XOffset, -6 * .625f);
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, -6 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, -6 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent6).AddOffset(0, -6 * .625f);
                    }
                    else if (input.A.GetCombinedStomachSize() > 10)
                    {
                        output.ChangeSprite(SpriteType.Belly).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Breasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.SecondaryBreasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Balls).AddOffset(XOffset, -4 * .625f);
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, -4 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, -4 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent6).AddOffset(0, -4 * .625f);
                    }
                    else if (input.A.GetCombinedStomachSize() > 8)
                    {
                        output.ChangeSprite(SpriteType.Belly).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Breasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.SecondaryBreasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Balls).AddOffset(XOffset, -2 * .625f);
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, -2 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, -2 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent6).AddOffset(0, -2 * .625f);
                    }
                    else if (input.A.GetCombinedStomachSize() > 6)
                    {
                        output.ChangeSprite(SpriteType.Belly).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Breasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.SecondaryBreasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Balls).AddOffset(XOffset, -1 * .625f);
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, -1 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, -1 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent6).AddOffset(0, -1 * .625f);
                    }
                    else
                    {
                        output.ChangeSprite(SpriteType.Belly).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Breasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.SecondaryBreasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Balls).AddOffset(XOffset, 0);
                    }
                }
                else
                {
                    if (input.A.GetStomachSize() > 14)
                    {
                        output.ChangeSprite(SpriteType.Belly).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Breasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.SecondaryBreasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Balls).AddOffset(XOffset, -8 * .625f);
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, -8 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, -8 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent6).AddOffset(0, -8 * .625f);
                    }
                    else if (input.A.GetStomachSize() > 12)
                    {
                        output.ChangeSprite(SpriteType.Belly).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Breasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.SecondaryBreasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Balls).AddOffset(XOffset, -6 * .625f);
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, -6 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, -6 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent6).AddOffset(0, -6 * .625f);
                    }
                    else if (input.A.GetStomachSize() > 10)
                    {
                        output.ChangeSprite(SpriteType.Belly).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Breasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.SecondaryBreasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Balls).AddOffset(XOffset, -4 * .625f);
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, -4 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, -4 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent6).AddOffset(0, -4 * .625f);
                    }
                    else if (input.A.GetStomachSize() > 8)
                    {
                        output.ChangeSprite(SpriteType.Belly).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Breasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.SecondaryBreasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Balls).AddOffset(XOffset, -2 * .625f);
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, -2 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, -2 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent6).AddOffset(0, -2 * .625f);
                    }
                    else if (input.A.GetStomachSize() > 6)
                    {
                        output.ChangeSprite(SpriteType.Belly).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Breasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.SecondaryBreasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Balls).AddOffset(XOffset, -1 * .625f);
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, -1 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, -1 * .625f);
                        output.ChangeSprite(SpriteType.BodyAccent6).AddOffset(0, -1 * .625f);
                    }
                    else
                    {
                        output.ChangeSprite(SpriteType.Belly).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Breasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.SecondaryBreasts).AddOffset(XOffset, 0);
                        output.ChangeSprite(SpriteType.Balls).AddOffset(XOffset, 0);
                    }
                }
            });

            builder.RandomCustom((data, output) =>   
            {
                Defaults.Randomize(data, output);
                IUnitRead unit = data.Unit;


                unit.BodyAccentType1 = State.Rand.Next(data.SetupOutput.BodyAccentTypes1);

                unit.ExtraColor1 = unit.SkinColor;

                unit.TailType = 0;
            });
        });


        private static class ViperArmour1TypeFull
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperArmour1TypeFullInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers4[22];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1422");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing4"].Layer(7);
                    output["Clothing2"].Layer(17);
                    output["Clothing5"].Layer(7);
                    output["Clothing5"].Coloring(Color.white);
                    output["Clothing3"].Layer(7);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[10]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[2 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[11]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);
                    output["Clothing5"].Sprite(input.Sprites.Vipers3[17]);

                    bool attacking = input.A.IsAttacking;
                    output["Clothing3"].Sprite(input.Sprites.Vipers3[13 + (attacking ? 1 : 0)]);
                    output["Clothing4"].Sprite(input.Sprites.Vipers3[15 + (attacking ? 1 : 0)]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                    output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperArmour1TypeNoGloves
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperArmour1TypeNoGlovesInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers4[22];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1422");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(17);
                    output["Clothing3"].Layer(7);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[10]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[2 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[11]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);
                    output["Clothing3"].Sprite(input.Sprites.Vipers3[17]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperArmour1TypeNoCap
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperArmour1TypeNoCapInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers4[22];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1422");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing4"].Layer(7);
                    output["Clothing2"].Layer(17);
                    output["Clothing3"].Layer(7);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[10]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[2 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[11]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);

                    bool attacking = input.A.IsAttacking;
                    output["Clothing3"].Sprite(input.Sprites.Vipers3[13 + (attacking ? 1 : 0)]);
                    output["Clothing4"].Sprite(input.Sprites.Vipers3[15 + (attacking ? 1 : 0)]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                    output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperArmour1TypeBare
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperArmour1TypeBareInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers4[22];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1422");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(17);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[10]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[2 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[11]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperArmour2TypeFull
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperArmour2TypeFullInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers3[97];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1497");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing4"].Layer(7);
                    output["Clothing2"].Layer(17);
                    output["Clothing5"].Layer(7);
                    output["Clothing5"].Coloring(Color.white);
                    output["Clothing3"].Layer(7);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[18]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[20 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[19]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);
                    output["Clothing5"].Sprite(input.Sprites.Vipers3[30]);

                    bool attacking = input.A.IsAttacking;
                    output["Clothing3"].Sprite(input.Sprites.Vipers3[28 + (attacking ? 1 : 0)]);
                    output["Clothing4"].Sprite(input.Sprites.Vipers3[15 + (attacking ? 1 : 0)]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                    output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperArmour2TypeNoGloves
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperArmour2TypeNoGlovesInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers3[97];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1497");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(17);
                    output["Clothing3"].Layer(7);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[18]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[20 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[19]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);
                    output["Clothing3"].Sprite(input.Sprites.Vipers3[30]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperArmour2TypeNoCap
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperArmour2TypeNoCapInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers3[97];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1497");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing4"].Layer(7);
                    output["Clothing2"].Layer(17);
                    output["Clothing3"].Layer(7);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[18]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[20 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[19]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);

                    bool attacking = input.A.IsAttacking;
                    output["Clothing3"].Sprite(input.Sprites.Vipers3[28 + (attacking ? 1 : 0)]);
                    output["Clothing4"].Sprite(input.Sprites.Vipers3[15 + (attacking ? 1 : 0)]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                    output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperArmour2TypeBare
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperArmour2TypeBareInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers3[97];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1497");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(17);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[18]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[20 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[19]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperArmour3TypeFull
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperArmour3TypeFullInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers4[23];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1423");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing4"].Layer(7);
                    output["Clothing2"].Layer(17);
                    output["Clothing5"].Layer(7);
                    output["Clothing5"].Coloring(Color.white);
                    output["Clothing3"].Layer(7);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[40]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[31 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[41]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);
                    output["Clothing5"].Sprite(input.Sprites.Vipers3[39]);

                    bool attacking = input.A.IsAttacking;
                    output["Clothing3"].Sprite(input.Sprites.Vipers3[42 + (attacking ? 1 : 0)]);
                    output["Clothing4"].Sprite(input.Sprites.Vipers3[15 + (attacking ? 1 : 0)]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                    output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperArmour3TypeNoGloves
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperArmour3TypeNoGlovesInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers4[23];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1423");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(17);
                    output["Clothing3"].Layer(7);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[40]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[31 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[41]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);
                    output["Clothing3"].Sprite(input.Sprites.Vipers3[39]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperArmour3TypeNoCap
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperArmour3TypeNoCapInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers4[23];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1423");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing4"].Layer(7);
                    output["Clothing2"].Layer(17);
                    output["Clothing3"].Layer(7);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[40]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[31 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[41]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);

                    bool attacking = input.A.IsAttacking;
                    output["Clothing3"].Sprite(input.Sprites.Vipers3[42 + (attacking ? 1 : 0)]);
                    output["Clothing4"].Sprite(input.Sprites.Vipers3[15 + (attacking ? 1 : 0)]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                    output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperArmour3TypeBare
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperArmour3TypeBareInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers4[23];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1423");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(17);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[40]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[31 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[41]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vipers3[12]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperRuler1TypeFull
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperRuler1TypeFullInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers3[98];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1498");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(17);
                    output["Clothing4"].Layer(7);
                    output["Clothing4"].Coloring(Color.white);
                    output["Clothing3"].Layer(7);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[52]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[44 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[53]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vipers3[54]);
                    output["Clothing4"].Sprite(input.Sprites.Vipers3[57]);

                    bool attacking = input.A.IsAttacking;
                    output["Clothing3"].Sprite(input.Sprites.Vipers3[55 + (attacking ? 1 : 0)]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperRuler1TypeNoGloves
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperRuler1TypeNoGlovesInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers3[98];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1498");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(17);
                    output["Clothing3"].Layer(7);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[52]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[44 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[53]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vipers3[54]);
                    output["Clothing3"].Sprite(input.Sprites.Vipers3[57]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperRuler1TypeNoCap
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperRuler1TypeNoCapInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers3[98];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1498");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(17);
                    output["Clothing3"].Layer(7);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[52]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[44 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[53]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vipers3[54]);

                    bool attacking = input.A.IsAttacking;
                    output["Clothing3"].Sprite(input.Sprites.Vipers3[55 + (attacking ? 1 : 0)]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperRuler1TypeBare
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperRuler1TypeBareInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers3[98];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1498");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(17);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[52]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[44 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[53]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vipers3[54]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperArmour4TypeFull
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperArmour4TypeFullInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers3[97];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1497");
                });

                builder.RenderAll((input, output, extra) =>
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
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[68]);
                        output["Clothing2"].Sprite(input.Sprites.Vipers3[78]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[60 + input.U.BreastSize]);
                        output["Clothing2"].Sprite(input.Sprites.Vipers3[70 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[69]);
                        output["Clothing2"].Sprite(input.Sprites.Vipers3[79]);
                    }

                    output["Clothing5"].Sprite(input.Sprites.Vipers3[58]);
                    output["Clothing6"].Sprite(input.Sprites.Vipers3[59]);

                    bool attacking = input.A.IsAttacking;
                    output["Clothing3"].Sprite(input.Sprites.Vipers3[80 + (attacking ? 1 : 0)]);
                    output["Clothing4"].Sprite(input.Sprites.Vipers3[82 + (attacking ? 1 : 0)]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                    output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                    output["Clothing6"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperArmour4TypeNoGloves
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperArmour4TypeNoGlovesInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers3[97];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1497");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing4"].Layer(7);
                    output["Clothing2"].Layer(17);
                    output["Clothing3"].Layer(7);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[68]);
                        output["Clothing2"].Sprite(input.Sprites.Vipers3[78]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[60 + input.U.BreastSize]);
                        output["Clothing2"].Sprite(input.Sprites.Vipers3[70 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[69]);
                        output["Clothing2"].Sprite(input.Sprites.Vipers3[79]);
                    }

                    output["Clothing3"].Sprite(input.Sprites.Vipers3[58]);
                    output["Clothing4"].Sprite(input.Sprites.Vipers3[59]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                    output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperArmour4TypeNoCap
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperArmour4TypeNoCapInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers3[97];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1497");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing4"].Layer(7);
                    output["Clothing2"].Layer(17);
                    output["Clothing3"].Layer(7);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[68]);
                        output["Clothing2"].Sprite(input.Sprites.Vipers3[78]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[60 + input.U.BreastSize]);
                        output["Clothing2"].Sprite(input.Sprites.Vipers3[70 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[69]);
                        output["Clothing2"].Sprite(input.Sprites.Vipers3[79]);
                    }

                    bool attacking = input.A.IsAttacking;
                    output["Clothing3"].Sprite(input.Sprites.Vipers3[80 + (attacking ? 1 : 0)]);
                    output["Clothing4"].Sprite(input.Sprites.Vipers3[82 + (attacking ? 1 : 0)]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                    output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperArmour4TypeBare
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperArmour4TypeBareInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers3[97];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1497");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(17);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[68]);
                        output["Clothing2"].Sprite(input.Sprites.Vipers3[78]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[60 + input.U.BreastSize]);
                        output["Clothing2"].Sprite(input.Sprites.Vipers3[70 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[69]);
                        output["Clothing2"].Sprite(input.Sprites.Vipers3[79]);
                    }

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperRuler2TypeFull
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperRuler2TypeFullInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers3[99];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1499");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(17);
                    output["Clothing4"].Layer(7);
                    output["Clothing4"].Coloring(Color.white);
                    output["Clothing3"].Layer(7);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[92]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[84 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[93]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vipers3[54]);
                    output["Clothing4"].Sprite(input.Sprites.Vipers3[96]);

                    bool attacking = input.A.IsAttacking;
                    output["Clothing3"].Sprite(input.Sprites.Vipers3[94 + (attacking ? 1 : 0)]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperRuler2TypeNoGloves
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperRuler2TypeNoGlovesInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers3[99];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1499");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(17);
                    output["Clothing3"].Layer(7);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[92]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[84 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[93]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vipers3[54]);
                    output["Clothing3"].Sprite(input.Sprites.Vipers3[96]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperRuler2TypeNoCap
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperRuler2TypeNoCapInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers3[99];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1499");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(17);
                    output["Clothing3"].Layer(7);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[92]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[84 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[93]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vipers3[54]);

                    bool attacking = input.A.IsAttacking;
                    output["Clothing3"].Sprite(input.Sprites.Vipers3[94 + (attacking ? 1 : 0)]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }


        private static class ViperRuler2TypeBare
        {
            internal static readonly BindableClothing<IOverSizeParameters> ViperRuler2TypeBareInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Vipers3[99];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.vipers/1499");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(17);
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[92]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[84 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Vipers3[93]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Vipers3[54]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ViperSkin, input.U.ClothingColor));
                });
            });
        }
    }
}
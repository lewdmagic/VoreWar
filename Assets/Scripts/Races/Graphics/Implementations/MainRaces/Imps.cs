#region

using System;
using System.Collections.Generic;
using UnityEngine;
#endregion

namespace Races.Graphics.Implementations.MainRaces
{


//TODO:
// recolor bulges on clothes
// add color selection
// add wobble to imp belly accent

    internal static class Imps
    {
        private static Func<IClothingRenderInput, IOverSizeParameters> paramsCalc = CommonRaceCode.MakeOversizeFunc(22 * 22);
        private static List<IClothingDataSimple> _allClothing;

        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Imp", "Imps");
                output.WallType(WallType.Imp);
                output.BonesInfo((unit) => 
                {
                    if (unit.Furry)
                    {
                        return new List<BoneInfo>
                        {
                            new BoneInfo(BoneTypes.FurryBones, unit.Name)
                        };
                    }
                    else
                    {
                        if (unit.EyeType == 0)
                        {
                            return new List<BoneInfo>
                            {
                                new BoneInfo(BoneTypes.GenericBonePile, unit.Name),
                                new BoneInfo(BoneTypes.Imp3EyeSkull, "")
                            };
                        }
                        else if (unit.EyeType == 1)
                        {
                            return new List<BoneInfo>
                            {
                                new BoneInfo(BoneTypes.GenericBonePile, unit.Name),
                                new BoneInfo(BoneTypes.Imp1EyeSkull, "")
                            };
                        }
                        else
                        {
                            return new List<BoneInfo>
                            {
                                new BoneInfo(BoneTypes.GenericBonePile, unit.Name),
                                new BoneInfo(BoneTypes.Imp2EyeSkull, "")
                            };
                        }
                    }
                });
                output.FlavorText(new FlavorText(
                    new Texts { "infernal", "diminutive", "sized" },
                    new Texts { "infernal", "deceptive", "devious" },
                    new Texts { "imp", "infernal being", "small demon" },
                    new Dictionary<string, string>
                    {
                        [WeaponNames.Mace]        = "Morningstar",
                        [WeaponNames.Axe]         = "Cleaver",
                        [WeaponNames.SimpleBow]   = "Bow",
                        [WeaponNames.CompoundBow] = "Infernal Bow",
                        [WeaponNames.Claw]        = "Fist"
                    }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 6,
                    StomachSize = 12,
                    HasTail = true,
                    FavoredStat = Stat.Will,
                    RacialTraits = new List<Traits>()
                    {
                        Traits.PackStomach,
                        Traits.AstralCall,
                    },
                    RaceDescription = "Following the scent of new lands to torment, these beings erupted forth from the underworld. So eager are they that at the promise of battle some of the Imps still in the infernal realm may manifest just for a chance at carnage.",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.BodyAccessoryColor, "Body Accent Color");
                    buttons.SetText(ButtonType.ClothingType, "Under Tops");
                    buttons.SetText(ButtonType.Clothing2Type, "Under Bottoms");
                    buttons.SetText(ButtonType.ClothingExtraType1, "Over Bottoms");
                    buttons.SetText(ButtonType.ClothingExtraType2, "Over Tops");
                    buttons.SetText(ButtonType.ClothingExtraType3, "Legs");
                    buttons.SetText(ButtonType.ClothingExtraType4, "Gloves");
                    buttons.SetText(ButtonType.ClothingExtraType5, "Hats");
                    buttons.SetText(ButtonType.BodyAccentTypes1, "Center Pattern");
                    buttons.SetText(ButtonType.BodyAccentTypes2, "Outer Pattern");
                    buttons.SetText(ButtonType.BodyAccentTypes3, "Horn Type");
                    buttons.SetText(ButtonType.BodyAccentTypes4, "Special Type");
                });
                output.TownNames(new List<string>
                {
                    "Demongate",
                    "Cinderpool",
                    "Ashburn",
                    "Hellbreath",
                    "Stygia",
                    "Blackden",
                    "Gehenna",
                    "Funtown",
                });
                output.DickSizes = () => 4;
                output.BreastSizes = () => 7;

                output.SpecialAccessoryCount = 0;
                output.ClothingShift = new Vector3(0, 0, 0);
                output.AvoidedEyeTypes = 0;
                output.AvoidedMouthTypes = 0;

                output.HairColors = ColorPaletteMap.GetPaletteCount(SwapType.UniversalHair);
                output.HairStyles = 16;
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.Imp);
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.ImpDark);
                output.EyeTypes = 8;
                output.EyeColors = ColorPaletteMap.GetPaletteCount(SwapType.EyeColor);
                output.SecondaryEyeColors = 1;
                output.BodySizes = 2;
                output.AllowedWaistTypes.Clear();
                output.MouthTypes = 0;
                output.AvoidedMainClothingTypes = 0;
                output.BodyAccentTypes1 = 4;
                output.BodyAccentTypes2 = 4;
                output.BodyAccentTypes3 = 4;
                output.BodyAccentTypes4 = 3;

                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.Clothing50Spaced);

                output.ExtendedBreastSprites = true;

                List<IClothing> allowedMainClothingTypes = new List<IClothing>() //undertops
                {
                    NewImpLeotard.NewImpLeotardInstance,
                    NewImpCasinoBunny.NewImpCasinoBunnyInstance,
                    NewImpUndertop1.NewImpUndertop1Instance.Create(paramsCalc),
                    NewImpUndertop2.NewImpUndertop2Instance.Create(paramsCalc),
                    NewImpUndertop3.NewImpUndertop3Instance.Create(paramsCalc),
                    NewImpUndertop4.NewImpUndertop4Instance.Create(paramsCalc),
                    NewImpUndertop5.NewImpUndertop5Instance.Create(paramsCalc)
                };
                output.AllowedMainClothingTypes.Clear();

                output.AllowedClothingHatTypes.Clear();


                output.AllowedWaistTypes.Set( //underbottoms
                    ImpUBottom.ImpUBottom1,
                    ImpUBottom.ImpUBottom2,
                    ImpUBottom.ImpUBottom3,
                    ImpUBottom.ImpUBottom4,
                    ImpUBottom.ImpUBottom5
                );

                output.ExtraMainClothing1Types.Set( //Overbottoms
                    ImpOBottom.GenericBottom1,
                    ImpOBottom.GenericBottom2,
                    ImpOBottom.GenericBottom3,
                    ImpOBottom.GenericBottom4,
                    ImpOBottomAlt.ImpOBottomAlt1,
                    ImpOBottom.GenericBottom5
                );

                output.ExtraMainClothing2Types.Set( //Special clothing
                    NewImpOverOPFem.NewImpOverOPFemInstance.Create(paramsCalc),
                    NewImpOverOpm.NewImpOverOpmInstance,
                    NewImpOverTop1.NewImpOverTop1Instance,
                    NewImpOverTop2.NewImpOverTop2Instance,
                    NewImpOverTop3.NewImpOverTop3Instance,
                    NewImpOverTop4.NewImpOverTop4Instance
                );

                List<IClothing> extraMainClothing3Types = new List<IClothing>() //Legs
                {
                    GenericLegs.GenericLegs1,
                    GenericLegs.GenericLegs2,
                    GenericLegsAlt.GenericLegsAlts1,
                    GenericLegs.GenericLegs3,
                    GenericLegsAlt.GenericLegsAlts2,
                    GenericLegs.GenericLegs4,
                    GenericLegs.GenericLegs5,
                    GenericLegs.GenericLegs6,
                    GenericLegs.GenericLegs7,
                    GenericLegs.GenericLegs8,
                    GenericLegsAlt.GenericLegsAlts3,
                    GenericLegs.GenericLegs9,
                    GenericLegsAlt.GenericLegsAlts4,
                    GenericLegs.GenericLegs10
                };
                output.ExtraMainClothing3Types.Set(extraMainClothing3Types);

                List<IClothing> extraMainClothing4Types = new List<IClothing>() //Gloves
                {
                    GenericGloves.GenericGloves1,
                    GenericGlovesPlusSecond.GenericGlovesPlusSecond1,
                    GenericGlovesPlusSecondAlt.GenericGlovesPlusSecondAlt1,
                    GenericGlovesPlusSecond.GenericGlovesPlusSecond2,
                    GenericGlovesPlusSecondAlt.GenericGlovesPlusSecondAlt2,
                    GenericGloves.GenericGloves2
                };
                output.ExtraMainClothing4Types.Set(extraMainClothing4Types);

                List<IClothing> extraMainClothing5Types = new List<IClothing>() //Hats
                {
                    Hat.Hat1,
                    Hat.Hat2,
                    HolidayHat.HolidayHatInstance
                };
                output.ExtraMainClothing5Types.Set(extraMainClothing5Types);


                _allClothing = new List<IClothingDataSimple>();
                _allClothing.AddRange(allowedMainClothingTypes);
                _allClothing.AddRange(extraMainClothing3Types);
                _allClothing.AddRange(extraMainClothing4Types);
                _allClothing.AddRange(extraMainClothing5Types);
            });


            builder.RunBefore((input, output) =>
            {
                if (input.A.HasBelly)
                {
                    output.ChangeSprite(SpriteType.Belly).SetActive(true).SetLocalScale(new Vector3(1, 1, 1));
                }

                output.ChangeSprite(SpriteType.BodyAccent6).SetTransformParent(SpriteType.BodyAccent6, false);
            });


            ColorSwapPalette ImpColor(Actor_Unit actor)
            {
                if (actor.Unit.BodyAccentType1 > 1)
                {
                    return ColorPaletteMap.GetPalette(SwapType.ImpDark, actor.Unit.AccessoryColor);
                }

                return ColorPaletteMap.GetPalette(SwapType.Imp, actor.Unit.SkinColor);
            }

            ColorSwapPalette ImpHorn(Actor_Unit actor)
            {
                if (actor.Unit.BodyAccentType2 != 0)
                {
                    return ColorPaletteMap.GetPalette(SwapType.ImpDark, actor.Unit.AccessoryColor);
                }

                return ColorPaletteMap.GetPalette(SwapType.Imp, actor.Unit.SkinColor);
            }

            ColorSwapPalette ImpBack(Actor_Unit actor)
            {
                if (actor.Unit.BodyAccentType2 != 0)
                {
                    return ColorPaletteMap.GetPalette(SwapType.ImpDark, actor.Unit.AccessoryColor);
                }

                return ColorPaletteMap.GetPalette(SwapType.Imp, actor.Unit.SkinColor);
            }

            ColorSwapPalette ImpWing(Actor_Unit actor)
            {
                if (actor.Unit.BodyAccentType2 != 0)
                {
                    return ColorPaletteMap.GetPalette(SwapType.Imp, actor.Unit.SkinColor);
                }

                return ColorPaletteMap.GetPalette(SwapType.ImpDark, actor.Unit.AccessoryColor);
            }

            ColorSwapPalette ImpBelly(Actor_Unit actor)
            {
                if (actor.Unit.BodyAccentType1 != 3)
                {
                    return ColorPaletteMap.GetPalette(SwapType.Imp, actor.Unit.SkinColor);
                }

                return ColorPaletteMap.GetPalette(SwapType.ImpDark, actor.Unit.AccessoryColor);
            }

            ColorSwapPalette ImpLeftTit(Actor_Unit actor)
            {
                if (actor.Unit.BodyAccentType1 <= 1)
                {
                    return ColorPaletteMap.GetPalette(SwapType.Imp, actor.Unit.SkinColor);
                }

                return ColorPaletteMap.GetPalette(SwapType.ImpDark, actor.Unit.AccessoryColor);
            }

            ColorSwapPalette ImpRightTit(Actor_Unit actor)
            {
                if (actor.Unit.BodyAccentType1 == 2)
                {
                    return ColorPaletteMap.GetPalette(SwapType.ImpDark, actor.Unit.AccessoryColor);
                }

                return ColorPaletteMap.GetPalette(SwapType.Imp, actor.Unit.SkinColor);
            }

            builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Imp, input.U.SkinColor));
                int attackingOffset = input.A.IsAttacking ? 1 : 0;
                int eatingOffset = input.A.IsEating ? 2 : 0;
                int hurtOffset = input.U.IsDead && input.U.Items != null ? 3 : 0;
                output.Sprite(input.Sprites.NewimpBase[32 + attackingOffset + eatingOffset + hurtOffset]);
            });

            builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.IsDead && input.U.Items != null)
                {
                    int sprite = 80;
                    sprite += input.U.EyeType;
                    output.Sprite(input.Sprites.NewimpBase[sprite]);
                }
                else
                {
                    int sprite = 40;
                    int attackingOffset = input.A.IsAttacking ? 1 : 0;
                    if (input.U.EyeType > 8)
                    {
                        sprite += 2 * input.U.EyeType;
                        output.Sprite(input.Sprites.NewimpBase[sprite - 16 + attackingOffset]);
                    }
                    else
                    {
                        sprite += 2 * input.U.EyeType;
                        output.Sprite(input.Sprites.NewimpBase[sprite + attackingOffset]);
                    }
                }
            });

            builder.RenderSingle(SpriteType.SecondaryEyes, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
                if (input.U.IsDead && input.U.Items != null)
                {
                }
                else
                {
                    int sprite = 72;
                    sprite += input.U.EyeType;
                    output.Sprite(input.Sprites.NewimpBase[sprite]);
                }
            });

            builder.RenderSingle(SpriteType.Hair, 9, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.UniversalHair, input.U.HairColor));
                if (input.U.ClothingExtraType5 == 1)
                {
                    output.Sprite(input.Sprites.NewimpHats[2 + 2 * input.U.HairStyle]);
                }
                else if (input.U.ClothingExtraType5 == 2)
                {
                    output.Sprite(input.Sprites.NewimpHats[36 + 2 * input.U.HairStyle]);
                }
                else
                {
                    output.Sprite(input.Sprites.NewimpBase[96 + 2 * input.U.HairStyle]);
                }
            });

            builder.RenderSingle(SpriteType.Hair2, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.UniversalHair, input.U.HairColor));
                if (input.U.ClothingExtraType5 == 1)
                {
                    output.Sprite(input.Sprites.NewimpHats[3 + 2 * input.U.HairStyle]);
                }
                else if (input.U.ClothingExtraType5 == 2)
                {
                    output.Sprite(input.Sprites.NewimpHats[37 + 2 * input.U.HairStyle]);
                }
                else
                {
                    output.Sprite(input.Sprites.NewimpBase[97 + 2 * input.U.HairStyle]);
                }
            });

            builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Imp, input.U.SkinColor));
                int weightMod = input.U.BodySize * 2;
                if (weightMod < 0) //I can't believe I had to add this, but had an exception here.  
                {
                    weightMod = 0;
                }

                if (input.U.HasBreasts)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.NewimpBase[1 + weightMod]);
                        return;
                    }

                    output.Sprite(input.Sprites.NewimpBase[0 + weightMod]);
                }
                else
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.NewimpBase[5 + weightMod]);
                        return;
                    }

                    output.Sprite(input.Sprites.NewimpBase[4 + weightMod]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ImpDark, input.U.AccessoryColor));
                if (input.U.BodyAccentType1 == 0)
                {
                    return;
                }

                if (input.U.BodyAccentType1 >= input.RaceData.BodyAccentTypes1)
                {
                    input.U.BodyAccentType1 = input.RaceData.BodyAccentTypes1 - 1;
                }

                int genderOffset = input.U.HasBreasts ? 0 : 4;
                int attackingOffset = input.A.IsAttacking ? 1 : 0;
                int weightMod = input.U.BodySize * 2;
                output.Sprite(input.Sprites.NewimpBase[8 + genderOffset + attackingOffset + weightMod + 8 * (input.U.BodyAccentType1 - 1)]);
            });

            builder.RenderSingle(SpriteType.BodyAccent2, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ImpDark, input.U.AccessoryColor));
                if (input.U.BodyAccentType2 == 0)
                {
                    return;
                }

                if (input.U.BodyAccentType2 >= input.RaceData.BodyAccentTypes2)
                {
                    input.U.BodyAccentType2 = input.RaceData.BodyAccentTypes2 - 1;
                }

                output.Sprite(input.Sprites.NewimpBase[36 + (input.U.BodyAccentType2 - 1)]);
            });

            builder.RenderSingle(SpriteType.BodyAccent3, 10, (input, output) =>
            {
                output.Coloring(ImpHorn(input.Actor));
                int sprite = 136;
                sprite += input.U.BodyAccentType3;
                output.Sprite(input.Sprites.NewimpBase[sprite]).Layer(input.U.BodyAccentType3 == 0 ? 6 : 9);
            });

            builder.RenderSingle(SpriteType.BodyAccent4, 1, (input, output) =>
            {
                output.Coloring(ImpBack(input.Actor));
                int sprite = 140;
                sprite += input.U.BodyAccentType4;
                output.Sprite(input.Sprites.NewimpBase[sprite]);
            });

            builder.RenderSingle(SpriteType.BodyAccent5, 1, (input, output) =>
            {
                output.Coloring(ImpWing(input.Actor));
                if (input.U.BodyAccentType4 == 2)
                {
                    output.Sprite(input.Sprites.NewimpBase[143]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent6, 18, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ImpDark, input.U.AccessoryColor));
                if (input.U.BodyAccentType1 == 2)
                {
                    if (input.A.HasBelly)
                    {
                        int size = input.A.GetStomachSize(32, 1.2f);

                        switch (size)
                        {
                            case 24:
                                output.AddOffset(0, -3 * .625f);
                                break;
                            case 25:
                                output.AddOffset(0, -8 * .625f);
                                break;
                            case 26:
                                output.AddOffset(0, -9 * .625f);
                                break;
                            case 27:
                                output.AddOffset(0, -13 * .625f);
                                break;
                            case 28:
                                output.AddOffset(0, -16 * .625f);
                                break;
                            case 29:
                                output.AddOffset(0, -23 * .625f);
                                break;
                            case 30:
                                output.AddOffset(0, -25 * .625f);
                                break;
                            case 31:
                                output.AddOffset(0, -27 * .625f);
                                break;
                            case 32:
                                output.AddOffset(0, -30 * .625f);
                                break;
                        }

                        //if (input.A.PredatorComponent.OnlyOnePreyAndLiving() && size >= 8 && size <= 13)
                        //    return Out.Update(State.GameManager.SpriteDictionary.NewimpVore[71]);

                        output.Sprite(input.Sprites.NewimpVore[106 + size]);
                    }
                }
            });

            builder.RenderSingle(SpriteType.Breasts, 19, (input, output) =>
            {
                output.Coloring(ImpLeftTit(input.Actor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.LeftBreastFullness > 0)
                {
                    int leftSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(22 * 22));

                    if (leftSize > 17)
                    {
                        leftSize = 17;
                    }


                    output.Sprite(input.Sprites.NewimpVore[leftSize]);
                }
                else
                {
                    if (input.U.DefaultBreastSize == 0)
                    {
                        output.Sprite(input.Sprites.NewimpVore[0]);
                        return;
                    }

                    output.Sprite(input.Sprites.NewimpVore[0 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.SecondaryBreasts, 19, (input, output) =>
            {
                output.Coloring(ImpRightTit(input.Actor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.RightBreastFullness > 0)
                {
                    int rightSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(22 * 22));
                
                    if (rightSize > 17)
                    {
                        rightSize = 17;
                    }

                    output.Sprite(input.Sprites.NewimpVore[22 + rightSize]);
                }
                else
                {
                    if (input.U.DefaultBreastSize == 0)
                    {
                        output.Sprite(input.Sprites.NewimpVore[22]);
                        return;
                    }

                    output.Sprite(input.Sprites.NewimpVore[22 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 17, (input, output) =>
            {
                output.Coloring(ImpBelly(input.Actor));
                if (input.A.HasBelly)
                {
                    int size = input.A.GetStomachSize(32, 1.2f);

                    switch (size)
                    {
                        case 24:
                            output.AddOffset(0, -3 * .625f);
                            break;
                        case 25:
                            output.AddOffset(0, -8 * .625f);
                            break;
                        case 26:
                            output.AddOffset(0, -9 * .625f);
                            break;
                        case 27:
                            output.AddOffset(0, -13 * .625f);
                            break;
                        case 28:
                            output.AddOffset(0, -16 * .625f);
                            break;
                        case 29:
                            output.AddOffset(0, -23 * .625f);
                            break;
                        case 30:
                            output.AddOffset(0, -25 * .625f);
                            break;
                        case 31:
                            output.AddOffset(0, -27 * .625f);
                            break;
                        case 32:
                            output.AddOffset(0, -30 * .625f);
                            break;
                    }

                    //if (input.A.PredatorComponent.OnlyOnePreyAndLiving() && size >= 8 && size <= 13)
                    //    return Out.Update(State.GameManager.SpriteDictionary.NewimpVore[71]);

                    output.Sprite(input.Sprites.NewimpVore[70 + size]);
                }
            });

            builder.RenderSingle(SpriteType.Dick, 14, (input, output) =>
            {
                output.Coloring(ImpColor(input.Actor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect())
                {
                    if (input.A.PredatorComponent?.VisibleFullness < .75f)
                    {
                        output.Sprite(input.Sprites.NewimpBase[129 + 2 * input.U.DickSize]).Layer(21);
                        return;
                    }

                    output.Sprite(input.Sprites.NewimpBase[129 + 2 * input.U.DickSize]).Layer(14);
                    return;
                }

                output.Sprite(input.Sprites.NewimpBase[128 + 2 * input.U.DickSize]).Layer(14);
            });

            builder.RenderSingle(SpriteType.Balls, 13, (input, output) =>
            {
                output.Coloring(ImpColor(input.Actor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                int size = input.A.GetBallSize(22, .8f);
                int baseSize = (input.U.DickSize + 1) / 3;

                int combined = Math.Min(baseSize + size + 2, 22);
                if (combined == 22)
                {
                    output.AddOffset(0, -10 * .625f);
                }
                else if (combined >= 21)
                {
                    output.AddOffset(0, -8 * .625f);
                }
                else if (combined >= 20)
                {
                    output.AddOffset(0, -6 * .625f);
                }
                else if (combined >= 19)
                {
                    output.AddOffset(0, -4 * .625f);
                }
                else if (combined >= 18)
                {
                    output.AddOffset(0, -1 * .625f);
                }

                if (size > 0)
                {
                    output.Sprite(input.Sprites.NewimpVore[44 + combined]);
                    return;
                }

                output.Sprite(input.Sprites.NewimpVore[44 + baseSize]);
            });

            builder.RenderSingle(SpriteType.Weapon, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    if (input.A.GetWeaponSprite() == 5)
                    {
                        return;
                    }

                    output.Sprite(input.Sprites.NewimpBase[88 + input.A.GetWeaponSprite()]);
                }
            });

            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                Unit unit = data.Unit;

                unit.SkinColor = State.Rand.Next(data.MiscRaceData.SkinColors);
                unit.EyeType = State.Rand.Next(data.MiscRaceData.EyeTypes);
                unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
                unit.HairColor = State.Rand.Next(data.MiscRaceData.HairColors);
                unit.BodyAccentType1 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes1);
                unit.BodyAccentType2 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes2);
                unit.BodyAccentType3 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes3);
                unit.BodyAccentType4 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes4);
                unit.ClothingColor = State.Rand.Next(data.MiscRaceData.ClothingColors);
                unit.ClothingColor2 = State.Rand.Next(data.MiscRaceData.ClothingColors);
            });
        });

        // protected override Color HairColor(Actor_Unit actor) => Color.white;    


        private static class GenericGloves
        {
            internal static readonly IClothing GenericGloves1 = MakeGenericGloves(0, 8, new ClothingId("base.imps/9108"));
            internal static readonly IClothing GenericGloves2 = MakeGenericGloves(27, 35, new ClothingId("base.imps/9135"));

            private static IClothing MakeGenericGloves(int start, int discard, ClothingId clothingId)
            {
                ClothingBuilder builder = ClothingBuilder.New();

                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.DiscardSprite = input.Sprites.NewimpGloves[discard];
                    output.ClothingId = clothingId;
                    output.FixedColor = false;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);
                    if (input.U.HasBreasts)
                    {
                        int weightMod = input.U.BodySize * 2;
                        output["Clothing1"].Sprite(input.A.IsAttacking ? input.Sprites.NewimpGloves[start + 1 + weightMod] : input.Sprites.NewimpGloves[start + weightMod]);
                    }
                    else
                    {
                        int weightMod = input.U.BodySize * 2;
                        output["Clothing1"].Sprite(input.A.IsAttacking ? input.Sprites.NewimpGloves[start + 5 + weightMod] : input.Sprites.NewimpGloves[start + 4 + weightMod]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
                return builder.BuildClothing();
            }
        }

        private static class GenericGlovesPlusSecond
        {
            internal static readonly IClothing GenericGlovesPlusSecond1 = MakeGenericGlovesPlusSecond(9, 17, new ClothingId("base.imps/9117"));
            internal static readonly IClothing GenericGlovesPlusSecond2 = MakeGenericGlovesPlusSecond(18, 26, new ClothingId("base.imps/9126"));

            private static IClothing MakeGenericGlovesPlusSecond(int start, int discard, ClothingId clothingId)
            {
                ClothingBuilder builder = ClothingBuilder.New();

                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.DiscardSprite = input.Sprites.NewimpGloves[discard];
                    output.ClothingId = clothingId;
                    output.FixedColor = false;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);
                    if (input.U.HasBreasts)
                    {
                        int weightMod = input.U.BodySize * 2;
                        output["Clothing1"].Sprite(input.A.IsAttacking ? input.Sprites.NewimpGloves[start + 1 + weightMod] : input.Sprites.NewimpGloves[start + weightMod]);
                    }
                    else
                    {
                        int weightMod = input.U.BodySize * 2;
                        output["Clothing1"].Sprite(input.A.IsAttacking ? input.Sprites.NewimpGloves[start + 5 + weightMod] : input.Sprites.NewimpGloves[start + 4 + weightMod]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
                return builder.BuildClothing();
            }
        }

        private static class GenericGlovesPlusSecondAlt
        {
            internal static readonly IClothing GenericGlovesPlusSecondAlt1 = MakeGenericGlovesPlusSecondAlt(9, 17, new ClothingId("base.imps/9117"));
            internal static readonly IClothing GenericGlovesPlusSecondAlt2 = MakeGenericGlovesPlusSecondAlt(18, 26, new ClothingId("base.imps/9126"));

            private static IClothing MakeGenericGlovesPlusSecondAlt(int start, int discard, ClothingId clothingId)
            {
                ClothingBuilder builder = ClothingBuilder.New();

                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.DiscardSprite = input.Sprites.NewimpGloves[discard];
                    output.ClothingId = clothingId;
                    output.FixedColor = false;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);
                    if (input.U.HasBreasts)
                    {
                        int weightMod = input.U.BodySize * 2;
                        output["Clothing1"].Sprite(input.A.IsAttacking ? input.Sprites.NewimpGloves[start + 1 + weightMod] : input.Sprites.NewimpGloves[start + weightMod]);
                    }
                    else
                    {
                        int weightMod = input.U.BodySize * 2;
                        output["Clothing1"].Sprite(input.A.IsAttacking ? input.Sprites.NewimpGloves[start + 5 + weightMod] : input.Sprites.NewimpGloves[start + 4 + weightMod]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                });
                return builder.BuildClothing();
            }
        }

        private static class GenericLegs
        {
            internal static readonly IClothing GenericLegs1 = MakeGenericLegs(0, 4, 45, new ClothingId("base.imps/9004"), femaleOnly: true, blocksDick: false);
            internal static readonly IClothing GenericLegs2 = MakeGenericLegs(5, 9, 45, new ClothingId("base.imps/9009"), femaleOnly: true, blocksDick: false);
            internal static readonly IClothing GenericLegs3 = MakeGenericLegs(10, 14, 45, new ClothingId("base.imps/9019"), femaleOnly: true, blocksDick: false);
            internal static readonly IClothing GenericLegs4 = MakeGenericLegs(15, 19, 45, new ClothingId("base.imps/9015"), femaleOnly: true, blocksDick: false);
            internal static readonly IClothing GenericLegs5 = MakeGenericLegs(20, 24, 45, new ClothingId("base.imps/9020"), femaleOnly: true, blocksDick: false);
            internal static readonly IClothing GenericLegs6 = MakeGenericLegs(2, 4, 45, new ClothingId("base.imps/9002"), true, blocksDick: true, black: true);
            internal static readonly IClothing GenericLegs7 = MakeGenericLegs(7, 9, 45, new ClothingId("base.imps/9007"), true, blocksDick: true, black: true);
            internal static readonly IClothing GenericLegs8 = MakeGenericLegs(12, 14, 45, new ClothingId("base.imps/9012"), true, blocksDick: true);
            internal static readonly IClothing GenericLegs9 = MakeGenericLegs(17, 19, 45, new ClothingId("base.imps/9017"), true, blocksDick: true);
            internal static readonly IClothing GenericLegs10 = MakeGenericLegs(22, 24, 45, new ClothingId("base.imps/9022"), true, blocksDick: false);

            private static IClothing MakeGenericLegs(int start, int discard, int bulge, ClothingId clothingId, bool maleOnly = false,
                bool femaleOnly = false, bool blocksDick = false, bool black = false)
            {
                ClothingBuilder builder = ClothingBuilder.New();

                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.ClothingId = clothingId;
                    output.DiscardSprite = input.Sprites.NewimpLegs[discard];
                    if (maleOnly)
                    {
                        output.MaleOnly = true;
                    }

                    output.RevealsDick = !blocksDick;

                    if (femaleOnly)
                    {
                        output.FemaleOnly = true;
                    }

                    output.FixedColor = false;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(11);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Coloring(Color.white);
                    int weightMod = input.U.BodySize;
                    output["Clothing1"].Sprite(input.Sprites.NewimpLegs[start + weightMod]);
                    if (input.U.HasDick)
                    {
                        if (blocksDick)
                        {
                            if (black)
                            {
                                output["Clothing2"].Sprite(input.Sprites.NewimpUBottoms
                                    [bulge + 4 + input.U.DickSize]);
                            }
                            else
                            {
                                output["Clothing2"].Sprite(input.Sprites.NewimpUBottoms[bulge + input.U.DickSize]);
                            }
                        }
                        else
                        {
                            output["Clothing2"].Sprite(null);
                        }
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced,
                        input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced,
                        input.U.ClothingColor));
                });
                return builder.BuildClothing();
            }
        }

        private static class GenericLegsAlt
        {
            internal static readonly IClothing GenericLegsAlts1 = MakeGenericLegsAlt(10, 14, 45, new ClothingId("base.imps/9019"), femaleOnly: true, blocksDick: false);
            internal static readonly IClothing GenericLegsAlts2 = MakeGenericLegsAlt(15, 19, 45, new ClothingId("base.imps/9015"), femaleOnly: true, blocksDick: false);
            internal static readonly IClothing GenericLegsAlts3 = MakeGenericLegsAlt(12, 14, 45, new ClothingId("base.imps/9012"), true, blocksDick: true);
            internal static readonly IClothing GenericLegsAlts4 = MakeGenericLegsAlt(17, 19, 45, new ClothingId("base.imps/9017"), true, blocksDick: true);

            private static IClothing MakeGenericLegsAlt(int start, int discard, int bulge, ClothingId clothingId, bool maleOnly = false, bool femaleOnly = false, bool blocksDick = false, bool black = false)
            {
                ClothingBuilder builder = ClothingBuilder.New();

                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.ClothingId = clothingId;
                    output.DiscardSprite = input.Sprites.NewimpLegs[discard];
                    if (maleOnly)
                    {
                        output.MaleOnly = true;
                    }

                    output.RevealsDick = !blocksDick;

                    if (femaleOnly)
                    {
                        output.FemaleOnly = true;
                    }

                    output.FixedColor = false;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(11);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Coloring(Color.white);
                    int weightMod = input.U.BodySize;
                    output["Clothing1"].Sprite(input.Sprites.NewimpLegs[start + weightMod]);
                    if (input.U.HasDick)
                    {
                        if (blocksDick)
                        {
                            output["Clothing2"].Sprite(black ? input.Sprites.NewimpUBottoms[bulge + 4 + input.U.DickSize] : input.Sprites.NewimpUBottoms[bulge + input.U.DickSize]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(null);
                        }
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                });
                return builder.BuildClothing();
            }
            //dongs
        }

        private static class ImpUBottom
        {
            internal static readonly IClothing ImpUBottom1 = MakeImpUBottom(0, 2, 45, 8, 9, State.GameManager.SpriteDictionary.NewimpUBottoms, new ClothingId("base.imps/8808"));
            internal static readonly IClothing ImpUBottom2 = MakeImpUBottom(9, 11, 45, 17, 9, State.GameManager.SpriteDictionary.NewimpUBottoms, new ClothingId("base.imps/8817"));
            internal static readonly IClothing ImpUBottom3 = MakeImpUBottom(18, 20, 45, 26, 9, State.GameManager.SpriteDictionary.NewimpUBottoms, new ClothingId("base.imps/8826"), true);
            internal static readonly IClothing ImpUBottom4 = MakeImpUBottom(27, 29, 45, 35, 9, State.GameManager.SpriteDictionary.NewimpUBottoms, new ClothingId("base.imps/8835"));
            internal static readonly IClothing ImpUBottom5 = MakeImpUBottom(36, 38, 45, 44, 9, State.GameManager.SpriteDictionary.NewimpUBottoms, new ClothingId("base.imps/8844"));

            private static IClothing MakeImpUBottom(int sprF, int sprM, int bulge, int discard, int layer,
                Sprite[] sheet, ClothingId clothingId, bool black = false)
            {
                ClothingBuilder builder = ClothingBuilder.New();

                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.DiscardSprite = sheet[discard];
                    output.ClothingId = clothingId;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(layer);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing2"].Layer(layer + 1);
                    output["Clothing2"].Coloring(Color.white);
                    int weightMod = input.U.BodySize;
                    if (input.A.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.U.HasBreasts ? sheet[sprF + 4 + weightMod] : sheet[sprM + 4 + weightMod]);

                        if (input.U.HasDick)
                        {
                            //if (output.BlocksDick == true)
                            if (true)
                            {
                                output["Clothing2"].Sprite(black ? input.Sprites.NewimpUBottoms[bulge + 4 + input.U.DickSize] : input.Sprites.NewimpUBottoms[bulge + input.U.DickSize]);
                            }
                        }
                        else
                        {
                            output["Clothing2"].Sprite(null);
                        }
                    }
                    else
                    {
                        if (input.U.HasBreasts)
                        {
                            output["Clothing1"].Sprite(sheet[sprF + weightMod]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(sheet[sprM + weightMod]);
                        }

                        if (input.U.HasDick)
                        {
                            //if (output.BlocksDick == true)
                            if (true)
                            {
                                output["Clothing2"].Sprite(black ? input.Sprites.NewimpUBottoms[bulge + 4 + input.U.DickSize] : input.Sprites.NewimpUBottoms[bulge + input.U.DickSize]);
                            }
                        }
                        else
                        {
                            output["Clothing2"].Sprite(null);
                        }
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                });
                return builder.BuildClothing();
            }
        }

        private static class NewImpLeotard
        {
            internal static readonly IClothing NewImpLeotardInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.NewimpOnePieces[74];
                    output.ClothingId = new ClothingId("base.imps/11001");
                    output.RevealsBreasts = true;
                    output.OccupiesAllSlots = true;
                    output.DiscardUsesPalettes = true;
                });


                builder.RenderAll((input, output) =>
                {
                    output["Clothing3"].Layer(13);
                    output["Clothing2"].Layer(20);
                    output["Clothing1"].Layer(16);
                    int weightMod = input.U.BodySize;
                    int bobs = input.U.BreastSize;
                    if (bobs > 7)
                    {
                        bobs = 7;
                    }

                    int size = input.A.GetStomachSize(32, 1.2f);
                    if (size > 7)
                    {
                        size = 7;
                    }

                    if (input.U.HasBreasts)
                    {
                        if (input.U.HasDick)
                        {
                            output["Clothing3"].Sprite(input.Sprites.NewimpUBottoms[45 + input.U.DickSize]);
                        }

                        output["Clothing2"].Sprite(input.Sprites.NewimpOnePieces[33 + bobs]);
                        output["Clothing1"].Sprite(input.Sprites.NewimpOnePieces[41 + size + 8 * weightMod]);

                        output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                        output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                        output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                        //bellyPalette = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SkinToClothing, input.U.ClothingColor);
                    }
                    else
                    {
                        if (input.U.HasDick)
                        {
                            output["Clothing3"].Sprite(input.Sprites.NewimpUBottoms[45 + input.U.DickSize]);
                        }

                        output["Clothing2"].Sprite(null);
                        output["Clothing1"].Sprite(input.Sprites.NewimpOnePieces[58 + size + 8 * weightMod]);
                        output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced,
                            input.U.ClothingColor2));
                        output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced,
                            input.U.ClothingColor2));
                        output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced,
                            input.U.ClothingColor2));
                    }
                });
            });
        }
    }

    internal static class NewImpCasinoBunny
    {
        internal static readonly IClothing NewImpCasinoBunnyInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.NewimpOnePieces[74];
                output.ClothingId = new ClothingId("base.imps/11011");
                output.OccupiesAllSlots = true;
                output.DiscardUsesPalettes = true;
                output.RevealsBreasts = true;
            });


            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(13);
                output["Clothing2"].Layer(20);
                output["Clothing1"].Layer(16);
                int weightMod = input.U.BodySize;
                int bobs = input.U.BreastSize;
                if (bobs > 7)
                {
                    bobs = 7;
                }

                int size = input.A.GetStomachSize(32, 1.2f);
                if (size > 5)
                {
                    size = 5;
                }

                if (input.U.HasBreasts)
                {
                    if (input.U.HasDick)
                    {
                        output["Clothing3"].Sprite(input.Sprites.NewimpUBottoms[45 + input.U.DickSize]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.NewimpOnePieces[0 + bobs]);
                    output["Clothing1"].Sprite(input.Sprites.NewimpOnePieces[8 + size + 6 * weightMod]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                    output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                    //bellyPalette = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SkinToClothing, input.U.ClothingColor);
                }
                else
                {
                    output["Clothing3"].Sprite(input.Sprites.NewimpUBottoms[45 + input.U.DickSize]);
                    output["Clothing2"].Sprite(null);
                    output["Clothing1"].Sprite(input.Sprites.NewimpOnePieces[20 + size + 6 * weightMod]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced,
                        input.U.ClothingColor2));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced,
                        input.U.ClothingColor2));
                    output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced,
                        input.U.ClothingColor2));
                }
            });
        });
    }

    internal static class ImpOBottom
    {
        internal static readonly IClothing GenericBottom1 = MakeImpOBottom(0, 2, false, 45, 8, 15, State.GameManager.SpriteDictionary.NewimpOBottoms, new ClothingId("base.imps/8908"));
        internal static readonly IClothing GenericBottom2 = MakeImpOBottom(9, 11, false, 45, 17, 15, State.GameManager.SpriteDictionary.NewimpOBottoms, new ClothingId("base.imps/8917"));
        internal static readonly IClothing GenericBottom3 = MakeImpOBottom(18, 20, true, 45, 26, 15, State.GameManager.SpriteDictionary.NewimpOBottoms, new ClothingId("base.imps/8926"));
        internal static readonly IClothing GenericBottom4 = MakeImpOBottom(27, 29, true, 49, 35, 15, State.GameManager.SpriteDictionary.NewimpOBottoms, new ClothingId("base.imps/8935"));
        internal static readonly IClothing GenericBottom5 = MakeImpOBottom(36, 38, false, 45, 44, 15, State.GameManager.SpriteDictionary.NewimpOBottoms, new ClothingId("base.imps/8944"));

        private static IClothing MakeImpOBottom(int sprF, int sprM, bool showbulge, int bulge, int discard, int layer,
            Sprite[] sheet, ClothingId clothingId)
        {
            ClothingBuilder builder = ClothingBuilder.New();

            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.DiscardSprite = sheet[discard];
                output.ClothingId = clothingId;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(layer);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing2"].Layer(layer + 1);
                output["Clothing2"].Coloring(Color.white);
                int weightMod = input.U.BodySize;
                if (input.A.HasBelly)
                {
                    output["Clothing1"].Sprite(input.U.HasBreasts ? sheet[sprF + 4 + weightMod] : sheet[sprM + 4 + weightMod]);

                    if (input.U.HasDick && showbulge)
                    {
                        //if (output.BlocksDick == true)
                        if (true)
                        {
                            output["Clothing2"].Sprite(input.Sprites.NewimpUBottoms[Math.Min(bulge + input.U.DickSize, 52)]);
                        }
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(input.U.HasBreasts ? sheet[sprF + weightMod] : sheet[sprM + weightMod]);

                    if (input.U.HasDick)
                    {
                        //if (output.BlocksDick == true && showbulge == true)
                        output["Clothing2"].Sprite(showbulge ? input.Sprites.NewimpUBottoms[Math.Min(bulge + input.U.DickSize, 52)] : null);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
            return builder.BuildClothing();
        }
    }

    internal static class ImpOBottomAlt
    {
        internal static readonly IClothing ImpOBottomAlt1 = MakeImpOBottomAlt(27, 29, true, 49, 35, 15, State.GameManager.SpriteDictionary.NewimpOBottoms, new ClothingId("base.imps/8935"));

        private static IClothing MakeImpOBottomAlt(int sprF, int sprM, bool showbulge, int bulge, int discard,
            int layer, Sprite[] sheet, ClothingId clothingId)
        {
            ClothingBuilder builder = ClothingBuilder.New();


            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.DiscardSprite = sheet[discard];
                output.ClothingId = clothingId;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(layer);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing2"].Layer(layer + 1);
                output["Clothing2"].Coloring(Color.white);
                int weightMod = input.U.BodySize;
                if (input.A.HasBelly)
                {
                    output["Clothing1"].Sprite(input.U.HasBreasts ? sheet[sprF + 4 + weightMod] : sheet[sprM + 4 + weightMod]);

                    if (input.U.HasDick && showbulge)
                    {
                        // if (output.BlocksDick == true)
                        if (true)
                        {
                            output["Clothing2"].Sprite(input.Sprites.NewimpUBottoms[Math.Min(bulge + input.U.DickSize, 52)]);
                        }
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(input.U.HasBreasts ? sheet[sprF + weightMod] : sheet[sprM + weightMod]);

                    if (input.U.HasDick)
                    {
                        //if (output.BlocksDick == true && showbulge == true)
                        output["Clothing2"].Sprite(showbulge ? input.Sprites.NewimpUBottoms[Math.Min(bulge + input.U.DickSize, 52)] : null);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
            });
            return builder.BuildClothing();
        }
    }

    internal static class Hat
    {
        internal static readonly IClothing Hat1 = MakeHat(0, 0, State.GameManager.SpriteDictionary.NewimpHats, new ClothingId("base.imps/666"));
        internal static readonly IClothing Hat2 = MakeHat(34, 0, State.GameManager.SpriteDictionary.NewimpHats, new ClothingId("base.imps/666"));

        private static IClothing MakeHat(int start, int discard, Sprite[] sheet, ClothingId clothingId)
        {
            ClothingBuilder builder = ClothingBuilder.New();

            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardSprite = sheet[discard];
                output.ClothingId = clothingId;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(2);
                output["Clothing1"].Layer(22);
                output["Clothing1"].Sprite(sheet[start]);
                output["Clothing2"].Sprite(sheet[start + 1]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
            return builder.BuildClothing();
        }
    }

    internal static class NewImpUndertop1
    {
        internal static readonly BindableClothing<IOverSizeParameters> NewImpUndertop1Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.NewimpUTops[8];
                output.ClothingId = new ClothingId("base.imps/11012");
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output, extra) =>
            {
                output["Clothing1"].Layer(20);
                if (extra.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.NewimpUTops[7]);
                }
                else if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.NewimpUTops[0 + input.U.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
            });
        });
    }

    internal static class NewImpUndertop2
    {
        internal static readonly BindableClothing<IOverSizeParameters> NewImpUndertop2Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.NewimpUTops[17];
                output.ClothingId = new ClothingId("base.imps/11013");
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output, extra) =>
            {
                output["Clothing1"].Layer(20);
                if (extra.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.NewimpUTops[16]);
                }
                else if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.NewimpUTops[9 + input.U.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
            });
        });
    }

    internal static class NewImpUndertop3
    {
        internal static readonly BindableClothing<IOverSizeParameters> NewImpUndertop3Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.NewimpUTops[26];
                output.ClothingId = new ClothingId("base.imps/11014");
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output, extra) =>
            {
                output["Clothing1"].Layer(20);
                if (extra.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.NewimpUTops[25]);
                }
                else if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.NewimpUTops[18 + input.U.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
            });
        });
    }

    internal static class NewImpUndertop4
    {
        internal static readonly BindableClothing<IOverSizeParameters> NewImpUndertop4Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.NewimpUTops[35];
                output.ClothingId = new ClothingId("base.imps/11015");
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output, extra) =>
            {
                output["Clothing1"].Layer(20);
                if (extra.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.NewimpUTops[34]);
                }
                else if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.NewimpUTops[27 + input.U.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
            });
        });
    }

    internal static class NewImpUndertop5
    {
        internal static readonly BindableClothing<IOverSizeParameters> NewImpUndertop5Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.NewimpUTops[75];
                output.ClothingId = new ClothingId("base.imps/11016");
                output.FemaleOnly = false;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output, extra) =>
            {
                output["Clothing2"].Layer(16);
                output["Clothing1"].Layer(20);
                int weightMod = input.U.BodySize;
                int size = input.A.GetStomachSize(32, 1.2f);
                if (size > 5)
                {
                    size = 5;
                }

                if (extra.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.NewimpUTops[48 + 13 * weightMod]);
                }
                else if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.NewimpUTops[42 + input.U.BreastSize + 13 * weightMod]);
                }

                output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.NewimpUTops[36 + size + 13 * weightMod] : input.Sprites.NewimpUTops[62 + size + 6 * weightMod]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
            });
        });
    }

    internal static class NewImpOverOPFem
    {
        internal static readonly BindableClothing<IOverSizeParameters> NewImpOverOPFemInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.NewImpOverOnePieces[23];
                output.ClothingId = new ClothingId("base.imps/11017");
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output, extra) =>
            {
                output["Clothing2"].Layer(16);
                output["Clothing1"].Layer(20);
                int weightMod = input.U.BodySize;
                int size = input.A.GetStomachSize(32, 1.2f);
                if (size > 8)
                {
                    size = 8;
                }

                int bobs = input.U.BreastSize;
                if (bobs > 7)
                {
                    bobs = 7;
                }

                {
                    if (extra.Oversize || input.U.HasBreasts == false)
                    {
                        output["Clothing1"].Sprite(null);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.NewImpOverOnePieces[bobs]);
                    }
                }
                output["Clothing2"].Sprite(input.Sprites.NewImpOverOnePieces[7 + size + 8 * weightMod]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor2));
            });
        });
    }

    internal static class NewImpOverOpm
    {
        internal static readonly IClothing NewImpOverOpmInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.NewImpOverOnePieces[40];
                output.ClothingId = new ClothingId("base.imps/11018");
                output.MaleOnly = true;
                output.RevealsBreasts = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(20);
                output["Clothing1"].Layer(20);
                int weightMod = input.U.BodySize;
                int size = input.A.GetStomachSize(32, 1.2f);
                if (size > 6)
                {
                    size = 6;
                }

                output["Clothing2"].Sprite(input.A.IsAttacking ? input.Sprites.NewImpOverOnePieces[25] : input.Sprites.NewImpOverOnePieces[24]);


                output["Clothing1"].Sprite(input.Sprites.NewImpOverOnePieces[26 + size + 7 * weightMod]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
        });
    }

    internal static class NewImpOverTop1
    {
        internal static readonly IClothing NewImpOverTop1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.NewImpOTops[3];
                output.ClothingId = new ClothingId("base.imps/11019");
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(3);
                output["Clothing1"].Layer(21);
                int weightMod = input.U.BodySize;


                output["Clothing1"].Sprite(input.Sprites.NewImpOTops[0 + weightMod]);
                output["Clothing2"].Sprite(input.Sprites.NewImpOTops[2]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
        });
    }

    internal static class NewImpOverTop2
    {
        internal static readonly IClothing NewImpOverTop2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.NewImpOTops[7];
                output.ClothingId = new ClothingId("base.imps/11020");
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(3);
                output["Clothing1"].Layer(21);
                int weightMod = input.U.BodySize;


                output["Clothing1"].Sprite(input.Sprites.NewImpOTops[4 + weightMod]);
                output["Clothing2"].Sprite(input.Sprites.NewImpOTops[6]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
        });
    }

    internal static class NewImpOverTop3
    {
        internal static readonly IClothing NewImpOverTop3Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.NewImpOTops[11];
                output.ClothingId = new ClothingId("base.imps/11021");
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(3);
                output["Clothing1"].Layer(21);
                int weightMod = input.U.BodySize;


                output["Clothing1"].Sprite(input.Sprites.NewImpOTops[8 + weightMod]);
                output["Clothing2"].Sprite(input.Sprites.NewImpOTops[10]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
        });
    }

    internal static class NewImpOverTop4
    {
        internal static readonly IClothing NewImpOverTop4Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.NewImpOTops[15];
                output.ClothingId = new ClothingId("base.imps/11022");
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(3);
                output["Clothing1"].Layer(21);
                int weightMod = input.U.BodySize;


                output["Clothing1"].Sprite(input.Sprites.NewImpOTops[12 + weightMod]);
                output["Clothing2"].Sprite(input.Sprites.NewImpOTops[14]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
            });
        });
    }


    internal static class HolidayHat
    {
        internal static readonly IClothing HolidayHatInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.ReqWinterHoliday = true;
                output.DiscardSprite = null;
                output.ClothingId = new ClothingId("base.imps/0");
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(29);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.ImpGobHat[0]);
            });
        });
    }
}
#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

//TODO:
// recolor bulges on clothes
// add color selection
// add wobble to imp belly accent

internal static class Imps
{
    private static List<IClothingDataSimple> _allClothing;

    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Default<OverSizeParameters>, builder =>
    {
        builder.Setup(output =>
        {
            output.DickSizes = () => 4;
            output.BreastSizes = () => 7;

            output.SpecialAccessoryCount = 0;
            output.ClothingShift = new Vector3(0, 0, 0);
            output.AvoidedEyeTypes = 0;
            output.AvoidedMouthTypes = 0;

            output.HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UniversalHair);
            output.HairStyles = 16;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Imp);
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.ImpDark);
            output.EyeTypes = 8;
            output.EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor);
            output.SecondaryEyeColors = 1;
            output.BodySizes = 2;
            output.AllowedWaistTypes.Clear();
            output.MouthTypes = 0;
            output.AvoidedMainClothingTypes = 0;
            output.BodyAccentTypes1 = 4;
            output.BodyAccentTypes2 = 4;
            output.BodyAccentTypes3 = 4;
            output.BodyAccentTypes4 = 3;

            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing50Spaced);

            output.ExtendedBreastSprites = true;

            List<IClothing<IOverSizeParameters>> allowedMainClothingTypes = new List<IClothing<IOverSizeParameters>>() //undertops
            {
                NewImpLeotard.NewImpLeotardInstance,
                NewImpCasinoBunny.NewImpCasinoBunnyInstance,
                NewImpUndertop1.NewImpUndertop1Instance,
                NewImpUndertop2.NewImpUndertop2Instance,
                NewImpUndertop3.NewImpUndertop3Instance,
                NewImpUndertop4.NewImpUndertop4Instance,
                NewImpUndertop5.NewImpUndertop5Instance
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
                NewImpOverOPFem.NewImpOverOPFemInstance,
                NewImpOverOpm.NewImpOverOpmInstance,
                NewImpOverTop1.NewImpOverTop1Instance,
                NewImpOverTop2.NewImpOverTop2Instance,
                NewImpOverTop3.NewImpOverTop3Instance,
                NewImpOverTop4.NewImpOverTop4Instance
            );

            List<IClothing<IOverSizeParameters>> extraMainClothing3Types = new List<IClothing<IOverSizeParameters>>() //Legs
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

            List<IClothing<IOverSizeParameters>> extraMainClothing4Types = new List<IClothing<IOverSizeParameters>>() //Gloves
            {
                GenericGloves.GenericGloves1,
                GenericGlovesPlusSecond.GenericGlovesPlusSecond1,
                GenericGlovesPlusSecondAlt.GenericGlovesPlusSecondAlt1,
                GenericGlovesPlusSecond.GenericGlovesPlusSecond2,
                GenericGlovesPlusSecondAlt.GenericGlovesPlusSecondAlt2,
                GenericGloves.GenericGloves2
            };
            output.ExtraMainClothing4Types.Set(extraMainClothing4Types);

            List<IClothing<IOverSizeParameters>> extraMainClothing5Types = new List<IClothing<IOverSizeParameters>>() //Hats
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
            CommonRaceCode.MakeBreastOversize(22 * 22).Invoke(input, output);

            if (input.Actor.HasBelly)
            {
                output.changeSprite(SpriteType.Belly).SetActive(true).SetLocalScale(new Vector3(1, 1, 1));
            }

            output.changeSprite(SpriteType.BodyAccent6).SetTransformParent(SpriteType.BodyAccent6, false);
        });


        ColorSwapPalette ImpColor(Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType1 > 1)
            {
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpDark, actor.Unit.AccessoryColor);
            }

            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Imp, actor.Unit.SkinColor);
        }

        ColorSwapPalette ImpHorn(Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType2 != 0)
            {
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpDark, actor.Unit.AccessoryColor);
            }

            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Imp, actor.Unit.SkinColor);
        }

        ColorSwapPalette ImpBack(Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType2 != 0)
            {
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpDark, actor.Unit.AccessoryColor);
            }

            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Imp, actor.Unit.SkinColor);
        }

        ColorSwapPalette ImpWing(Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType2 != 0)
            {
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Imp, actor.Unit.SkinColor);
            }

            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpDark, actor.Unit.AccessoryColor);
        }

        ColorSwapPalette ImpBelly(Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType1 != 3)
            {
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Imp, actor.Unit.SkinColor);
            }

            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpDark, actor.Unit.AccessoryColor);
        }

        ColorSwapPalette ImpLeftTit(Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType1 <= 1)
            {
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Imp, actor.Unit.SkinColor);
            }

            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpDark, actor.Unit.AccessoryColor);
        }

        ColorSwapPalette ImpRightTit(Actor_Unit actor)
        {
            if (actor.Unit.BodyAccentType1 == 2)
            {
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpDark, actor.Unit.AccessoryColor);
            }

            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Imp, actor.Unit.SkinColor);
        }

        builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Imp, input.Actor.Unit.SkinColor));
            int attackingOffset = input.Actor.IsAttacking ? 1 : 0;
            int eatingOffset = input.Actor.IsEating ? 2 : 0;
            int hurtOffset = input.Actor.Unit.IsDead && input.Actor.Unit.Items != null ? 3 : 0;
            output.Sprite(input.Sprites.NewimpBase[32 + attackingOffset + eatingOffset + hurtOffset]);
        });

        builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.IsDead && input.Actor.Unit.Items != null)
            {
                int sprite = 80;
                sprite += input.Actor.Unit.EyeType;
                output.Sprite(input.Sprites.NewimpBase[sprite]);
            }
            else
            {
                int sprite = 40;
                int attackingOffset = input.Actor.IsAttacking ? 1 : 0;
                if (input.Actor.Unit.EyeType > 8)
                {
                    sprite += 2 * input.Actor.Unit.EyeType;
                    output.Sprite(input.Sprites.NewimpBase[sprite - 16 + attackingOffset]);
                }
                else
                {
                    sprite += 2 * input.Actor.Unit.EyeType;
                    output.Sprite(input.Sprites.NewimpBase[sprite + attackingOffset]);
                }
            }
        });

        builder.RenderSingle(SpriteType.SecondaryEyes, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
                if (input.Actor.Unit.IsDead && input.Actor.Unit.Items != null)
                {
                }
                else
                {
                    int sprite = 72;
                    sprite += input.Actor.Unit.EyeType;
                    output.Sprite(input.Sprites.NewimpBase[sprite]);
                }
            });

        builder.RenderSingle(SpriteType.Hair, 9, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, input.Actor.Unit.HairColor));
            if (input.Actor.Unit.ClothingExtraType5 == 1)
            {
                output.Sprite(input.Sprites.NewimpHats[2 + 2 * input.Actor.Unit.HairStyle]);
            }
            else if (input.Actor.Unit.ClothingExtraType5 == 2)
            {
                output.Sprite(input.Sprites.NewimpHats[36 + 2 * input.Actor.Unit.HairStyle]);
            }
            else
            {
                output.Sprite(input.Sprites.NewimpBase[96 + 2 * input.Actor.Unit.HairStyle]);
            }
        });

        builder.RenderSingle(SpriteType.Hair2, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, input.Actor.Unit.HairColor));
            if (input.Actor.Unit.ClothingExtraType5 == 1)
            {
                output.Sprite(input.Sprites.NewimpHats[3 + 2 * input.Actor.Unit.HairStyle]);
            }
            else if (input.Actor.Unit.ClothingExtraType5 == 2)
            {
                output.Sprite(input.Sprites.NewimpHats[37 + 2 * input.Actor.Unit.HairStyle]);
            }
            else
            {
                output.Sprite(input.Sprites.NewimpBase[97 + 2 * input.Actor.Unit.HairStyle]);
            }
        });

        builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Imp, input.Actor.Unit.SkinColor));
            int weightMod = input.Actor.Unit.BodySize * 2;
            if (weightMod < 0) //I can't believe I had to add this, but had an exception here.  
            {
                weightMod = 0;
            }

            if (input.Actor.Unit.HasBreasts)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.NewimpBase[1 + weightMod]);
                    return;
                }

                output.Sprite(input.Sprites.NewimpBase[0 + weightMod]);
            }
            else
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.NewimpBase[5 + weightMod]);
                    return;
                }

                output.Sprite(input.Sprites.NewimpBase[4 + weightMod]);
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpDark, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType1 == 0)
            {
                return;
            }

            if (input.Actor.Unit.BodyAccentType1 >= input.RaceData.BodyAccentTypes1)
            {
                input.Actor.Unit.BodyAccentType1 = input.RaceData.BodyAccentTypes1 - 1;
            }

            int genderOffset = input.Actor.Unit.HasBreasts ? 0 : 4;
            int attackingOffset = input.Actor.IsAttacking ? 1 : 0;
            int weightMod = input.Actor.Unit.BodySize * 2;
            output.Sprite(input.Sprites.NewimpBase[8 + genderOffset + attackingOffset + weightMod + 8 * (input.Actor.Unit.BodyAccentType1 - 1)]);
        });

        builder.RenderSingle(SpriteType.BodyAccent2, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpDark, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType2 == 0)
            {
                return;
            }

            if (input.Actor.Unit.BodyAccentType2 >= input.RaceData.BodyAccentTypes2)
            {
                input.Actor.Unit.BodyAccentType2 = input.RaceData.BodyAccentTypes2 - 1;
            }

            output.Sprite(input.Sprites.NewimpBase[36 + (input.Actor.Unit.BodyAccentType2 - 1)]);
        });

        builder.RenderSingle(SpriteType.BodyAccent3, 10, (input, output) =>
        {
            output.Coloring(ImpHorn(input.Actor));
            int sprite = 136;
            sprite += input.Actor.Unit.BodyAccentType3;
            output.Sprite(input.Sprites.NewimpBase[sprite]).Layer(input.Actor.Unit.BodyAccentType3 == 0 ? 6 : 9);
        });

        builder.RenderSingle(SpriteType.BodyAccent4, 1, (input, output) =>
        {
            output.Coloring(ImpBack(input.Actor));
            int sprite = 140;
            sprite += input.Actor.Unit.BodyAccentType4;
            output.Sprite(input.Sprites.NewimpBase[sprite]);
        });

        builder.RenderSingle(SpriteType.BodyAccent5, 1, (input, output) =>
        {
            output.Coloring(ImpWing(input.Actor));
            if (input.Actor.Unit.BodyAccentType4 == 2)
            {
                output.Sprite(input.Sprites.NewimpBase[143]);
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent6, 18, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpDark, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType1 == 2)
            {
                if (input.Actor.HasBelly)
                {
                    int size = input.Actor.GetStomachSize(32, 1.2f);
                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                    {
                        output.Sprite(input.Sprites.NewimpVore[141]).AddOffset(0, -31 * .625f);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                    {
                        output.Sprite(input.Sprites.NewimpVore[140]).AddOffset(0, -30 * .625f);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size >= 30)
                    {
                        output.Sprite(input.Sprites.NewimpVore[139]).AddOffset(0, -30 * .625f);
                        return;
                    }

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

                    //if (input.Actor.PredatorComponent.OnlyOnePreyAndLiving() && size >= 8 && size <= 13)
                    //    return Out.Update(State.GameManager.SpriteDictionary.NewimpVore[71]);

                    output.Sprite(input.Sprites.NewimpVore[106 + size]);
                }
            }
        });

        builder.RenderSingle(SpriteType.Breasts, 19, (input, output) =>
        {
            output.Coloring(ImpLeftTit(input.Actor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(22 * 22));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 22)
                {
                    output.Sprite(input.Sprites.NewimpVore[21]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 22)
                {
                    output.Sprite(input.Sprites.NewimpVore[20]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 20)
                {
                    output.Sprite(input.Sprites.NewimpVore[19]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 18)
                {
                    output.Sprite(input.Sprites.NewimpVore[18]);
                    return;
                }

                if (leftSize > 17)
                {
                    leftSize = 17;
                }


                output.Sprite(input.Sprites.NewimpVore[leftSize]);
            }
            else
            {
                if (input.Actor.Unit.DefaultBreastSize == 0)
                {
                    output.Sprite(input.Sprites.NewimpVore[0]);
                    return;
                }

                output.Sprite(input.Sprites.NewimpVore[0 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.SecondaryBreasts, 19, (input, output) =>
        {
            output.Coloring(ImpRightTit(input.Actor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(22 * 22));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 22)
                {
                    output.Sprite(input.Sprites.NewimpVore[43]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 22)
                {
                    output.Sprite(input.Sprites.NewimpVore[42]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 20)
                {
                    output.Sprite(input.Sprites.NewimpVore[41]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 18)
                {
                    output.Sprite(input.Sprites.NewimpVore[40]);
                    return;
                }

                if (rightSize > 17)
                {
                    rightSize = 17;
                }

                output.Sprite(input.Sprites.NewimpVore[22 + rightSize]);
            }
            else
            {
                if (input.Actor.Unit.DefaultBreastSize == 0)
                {
                    output.Sprite(input.Sprites.NewimpVore[22]);
                    return;
                }

                output.Sprite(input.Sprites.NewimpVore[22 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.Belly, 17, (input, output) =>
        {
            output.Coloring(ImpBelly(input.Actor));
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(32, 1.2f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    output.Sprite(input.Sprites.NewimpVore[105]).AddOffset(0, -31 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    output.Sprite(input.Sprites.NewimpVore[104]).AddOffset(0, -30 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size >= 30)
                {
                    output.Sprite(input.Sprites.NewimpVore[103]).AddOffset(0, -30 * .625f);
                    return;
                }

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

                //if (input.Actor.PredatorComponent.OnlyOnePreyAndLiving() && size >= 8 && size <= 13)
                //    return Out.Update(State.GameManager.SpriteDictionary.NewimpVore[71]);

                output.Sprite(input.Sprites.NewimpVore[70 + size]);
            }
        });

        builder.RenderSingle(SpriteType.Dick, 14, (input, output) =>
        {
            output.Coloring(ImpColor(input.Actor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect())
            {
                if (input.Actor.PredatorComponent?.VisibleFullness < .75f)
                {
                    output.Sprite(input.Sprites.NewimpBase[129 + 2 * input.Actor.Unit.DickSize]).Layer(21);
                    return;
                }

                output.Sprite(input.Sprites.NewimpBase[129 + 2 * input.Actor.Unit.DickSize]).Layer(14);
                return;
            }

            output.Sprite(input.Sprites.NewimpBase[128 + 2 * input.Actor.Unit.DickSize]).Layer(14);
        });

        builder.RenderSingle(SpriteType.Balls, 13, (input, output) =>
        {
            output.Coloring(ImpColor(input.Actor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            int size = input.Actor.GetBallSize(22, .8f);
            int baseSize = (input.Actor.Unit.DickSize + 1) / 3;

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && size == 22)
            {
                output.Sprite(input.Sprites.NewimpVore[69]).AddOffset(0, -24 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 22)
            {
                output.Sprite(input.Sprites.NewimpVore[68]).AddOffset(0, -22 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && size >= 20)
            {
                output.Sprite(input.Sprites.NewimpVore[67]).AddOffset(0, -20 * .625f);
                return;
            }

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
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                if (input.Actor.GetWeaponSprite() == 5)
                {
                    return;
                }

                output.Sprite(input.Sprites.NewimpBase[88 + input.Actor.GetWeaponSprite()]);
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
        internal static readonly IClothing GenericGloves1 = MakeGenericGloves(0, 8, 9108);
        internal static readonly IClothing GenericGloves2 = MakeGenericGloves(27, 35, 9135);

        private static IClothing MakeGenericGloves(int start, int discard, int type)
        {
            ClothingBuilder builder = ClothingBuilder.New();

            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardSprite = input.Sprites.NewimpGloves[discard];
                output.Type = type;
                output.FixedColor = false;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                if (input.Actor.Unit.HasBreasts)
                {
                    int weightMod = input.Actor.Unit.BodySize * 2;
                    output["Clothing1"].Sprite(input.Actor.IsAttacking ? input.Sprites.NewimpGloves[start + 1 + weightMod] : input.Sprites.NewimpGloves[start + weightMod]);
                }
                else
                {
                    int weightMod = input.Actor.Unit.BodySize * 2;
                    output["Clothing1"].Sprite(input.Actor.IsAttacking ? input.Sprites.NewimpGloves[start + 5 + weightMod] : input.Sprites.NewimpGloves[start + 4 + weightMod]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
            return builder.BuildClothing();
        }
    }

    private static class GenericGlovesPlusSecond
    {
        internal static readonly IClothing GenericGlovesPlusSecond1 = MakeGenericGlovesPlusSecond(9, 17, 9117);
        internal static readonly IClothing GenericGlovesPlusSecond2 = MakeGenericGlovesPlusSecond(18, 26, 9126);

        private static IClothing MakeGenericGlovesPlusSecond(int start, int discard, int type)
        {
            ClothingBuilder builder = ClothingBuilder.New();

            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardSprite = input.Sprites.NewimpGloves[discard];
                output.Type = type;
                output.FixedColor = false;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                if (input.Actor.Unit.HasBreasts)
                {
                    int weightMod = input.Actor.Unit.BodySize * 2;
                    output["Clothing1"].Sprite(input.Actor.IsAttacking ? input.Sprites.NewimpGloves[start + 1 + weightMod] : input.Sprites.NewimpGloves[start + weightMod]);
                }
                else
                {
                    int weightMod = input.Actor.Unit.BodySize * 2;
                    output["Clothing1"].Sprite(input.Actor.IsAttacking ? input.Sprites.NewimpGloves[start + 5 + weightMod] : input.Sprites.NewimpGloves[start + 4 + weightMod]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            });
            return builder.BuildClothing();
        }
    }

    private static class GenericGlovesPlusSecondAlt
    {
        internal static readonly IClothing GenericGlovesPlusSecondAlt1 = MakeGenericGlovesPlusSecondAlt(9, 17, 9117);
        internal static readonly IClothing GenericGlovesPlusSecondAlt2 = MakeGenericGlovesPlusSecondAlt(18, 26, 9126);

        private static IClothing MakeGenericGlovesPlusSecondAlt(int start, int discard, int type)
        {
            ClothingBuilder builder = ClothingBuilder.New();

            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardSprite = input.Sprites.NewimpGloves[discard];
                output.Type = type;
                output.FixedColor = false;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                if (input.Actor.Unit.HasBreasts)
                {
                    int weightMod = input.Actor.Unit.BodySize * 2;
                    output["Clothing1"].Sprite(input.Actor.IsAttacking ? input.Sprites.NewimpGloves[start + 1 + weightMod] : input.Sprites.NewimpGloves[start + weightMod]);
                }
                else
                {
                    int weightMod = input.Actor.Unit.BodySize * 2;
                    output["Clothing1"].Sprite(input.Actor.IsAttacking ? input.Sprites.NewimpGloves[start + 5 + weightMod] : input.Sprites.NewimpGloves[start + 4 + weightMod]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
            });
            return builder.BuildClothing();
        }
    }

    private static class GenericLegs
    {
        internal static readonly IClothing GenericLegs1 = MakeGenericLegs(0, 4, 45, 9004, femaleOnly: true, blocksDick: false);
        internal static readonly IClothing GenericLegs2 = MakeGenericLegs(5, 9, 45, 9009, femaleOnly: true, blocksDick: false);
        internal static readonly IClothing GenericLegs3 = MakeGenericLegs(10, 14, 45, 9019, femaleOnly: true, blocksDick: false);
        internal static readonly IClothing GenericLegs4 = MakeGenericLegs(15, 19, 45, 9015, femaleOnly: true, blocksDick: false);
        internal static readonly IClothing GenericLegs5 = MakeGenericLegs(20, 24, 45, 9020, femaleOnly: true, blocksDick: false);
        internal static readonly IClothing GenericLegs6 = MakeGenericLegs(2, 4, 45, 9002, true, blocksDick: true, black: true);
        internal static readonly IClothing GenericLegs7 = MakeGenericLegs(7, 9, 45, 9007, true, blocksDick: true, black: true);
        internal static readonly IClothing GenericLegs8 = MakeGenericLegs(12, 14, 45, 9012, true, blocksDick: true);
        internal static readonly IClothing GenericLegs9 = MakeGenericLegs(17, 19, 45, 9017, true, blocksDick: true);
        internal static readonly IClothing GenericLegs10 = MakeGenericLegs(22, 24, 45, 9022, true, blocksDick: false);

        private static IClothing MakeGenericLegs(int start, int discard, int bulge, int type, bool maleOnly = false,
            bool femaleOnly = false, bool blocksDick = false, bool black = false)
        {
            ClothingBuilder builder = ClothingBuilder.New();

            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.Type = type;
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
                int weightMod = input.Actor.Unit.BodySize;
                output["Clothing1"].Sprite(input.Sprites.NewimpLegs[start + weightMod]);
                if (input.Actor.Unit.HasDick)
                {
                    if (blocksDick)
                    {
                        if (black)
                        {
                            output["Clothing2"].Sprite(input.Sprites.NewimpUBottoms
                                [bulge + 4 + input.Actor.Unit.DickSize]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(input.Sprites.NewimpUBottoms[bulge + input.Actor.Unit.DickSize]);
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

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced,
                    input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced,
                    input.Actor.Unit.ClothingColor));
            });
            return builder.BuildClothing();
        }
    }

    private static class GenericLegsAlt
    {
        internal static readonly IClothing GenericLegsAlts1 = MakeGenericLegsAlt(10, 14, 45, 9019, femaleOnly: true, blocksDick: false);
        internal static readonly IClothing GenericLegsAlts2 = MakeGenericLegsAlt(15, 19, 45, 9015, femaleOnly: true, blocksDick: false);
        internal static readonly IClothing GenericLegsAlts3 = MakeGenericLegsAlt(12, 14, 45, 9012, true, blocksDick: true);
        internal static readonly IClothing GenericLegsAlts4 = MakeGenericLegsAlt(17, 19, 45, 9017, true, blocksDick: true);

        private static IClothing MakeGenericLegsAlt(int start, int discard, int bulge, int type, bool maleOnly = false, bool femaleOnly = false, bool blocksDick = false, bool black = false)
        {
            ClothingBuilder builder = ClothingBuilder.New();

            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.Type = type;
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
                int weightMod = input.Actor.Unit.BodySize;
                output["Clothing1"].Sprite(input.Sprites.NewimpLegs[start + weightMod]);
                if (input.Actor.Unit.HasDick)
                {
                    if (blocksDick)
                    {
                        output["Clothing2"].Sprite(black ? input.Sprites.NewimpUBottoms[bulge + 4 + input.Actor.Unit.DickSize] : input.Sprites.NewimpUBottoms[bulge + input.Actor.Unit.DickSize]);
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

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
            });
            return builder.BuildClothing();
        }
        //dongs
    }

    private static class ImpUBottom
    {
        internal static readonly IClothing ImpUBottom1 = MakeImpUBottom(0, 2, 45, 8, 9, State.GameManager.SpriteDictionary.NewimpUBottoms, 8808);
        internal static readonly IClothing ImpUBottom2 = MakeImpUBottom(9, 11, 45, 17, 9, State.GameManager.SpriteDictionary.NewimpUBottoms, 8817);
        internal static readonly IClothing ImpUBottom3 = MakeImpUBottom(18, 20, 45, 26, 9, State.GameManager.SpriteDictionary.NewimpUBottoms, 8826, true);
        internal static readonly IClothing ImpUBottom4 = MakeImpUBottom(27, 29, 45, 35, 9, State.GameManager.SpriteDictionary.NewimpUBottoms, 8835);
        internal static readonly IClothing ImpUBottom5 = MakeImpUBottom(36, 38, 45, 44, 9, State.GameManager.SpriteDictionary.NewimpUBottoms, 8844);

        private static IClothing MakeImpUBottom(int sprF, int sprM, int bulge, int discard, int layer,
            Sprite[] sheet, int type, bool black = false)
        {
            ClothingBuilder builder = ClothingBuilder.New();

            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.DiscardSprite = sheet[discard];
                output.Type = type;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(layer);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing2"].Layer(layer + 1);
                output["Clothing2"].Coloring(Color.white);
                int weightMod = input.Actor.Unit.BodySize;
                if (input.Actor.HasBelly)
                {
                    output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? sheet[sprF + 4 + weightMod] : sheet[sprM + 4 + weightMod]);

                    if (input.Actor.Unit.HasDick)
                    {
                        //if (output.BlocksDick == true)
                        if (true)
                        {
                            output["Clothing2"].Sprite(black ? input.Sprites.NewimpUBottoms[bulge + 4 + input.Actor.Unit.DickSize] : input.Sprites.NewimpUBottoms[bulge + input.Actor.Unit.DickSize]);
                        }
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }
                }
                else
                {
                    if (input.Actor.Unit.HasBreasts)
                    {
                        output["Clothing1"].Sprite(sheet[sprF + weightMod]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(sheet[sprM + weightMod]);
                    }

                    if (input.Actor.Unit.HasDick)
                    {
                        //if (output.BlocksDick == true)
                        if (true)
                        {
                            output["Clothing2"].Sprite(black ? input.Sprites.NewimpUBottoms[bulge + 4 + input.Actor.Unit.DickSize] : input.Sprites.NewimpUBottoms[bulge + input.Actor.Unit.DickSize]);
                        }
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
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
                output.Type = 11001;
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
                output.DiscardUsesPalettes = true;
            });


            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(13);
                output["Clothing2"].Layer(20);
                output["Clothing1"].Layer(16);
                int weightMod = input.Actor.Unit.BodySize;
                int bobs = input.Actor.Unit.BreastSize;
                if (bobs > 7)
                {
                    bobs = 7;
                }

                int size = input.Actor.GetStomachSize(32, 1.2f);
                if (size > 7)
                {
                    size = 7;
                }

                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.Unit.HasDick)
                    {
                        output["Clothing3"].Sprite(input.Sprites.NewimpUBottoms[45 + input.Actor.Unit.DickSize]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.NewimpOnePieces[33 + bobs]);
                    output["Clothing1"].Sprite(input.Sprites.NewimpOnePieces[41 + size + 8 * weightMod]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
                    output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
                    //bellyPalette = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SkinToClothing, input.Actor.Unit.ClothingColor);
                }
                else
                {
                    if (input.Actor.Unit.HasDick)
                    {
                        output["Clothing3"].Sprite(input.Sprites.NewimpUBottoms[45 + input.Actor.Unit.DickSize]);
                    }

                    output["Clothing2"].Sprite(null);
                    output["Clothing1"].Sprite(input.Sprites.NewimpOnePieces[58 + size + 8 * weightMod]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced,
                        input.Actor.Unit.ClothingColor2));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced,
                        input.Actor.Unit.ClothingColor2));
                    output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced,
                        input.Actor.Unit.ClothingColor2));
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
            output.Type = 11011;
            output.OccupiesAllSlots = true;
            output.DiscardUsesPalettes = true;
            output.RevealsBreasts = true;
        });


        builder.RenderAll((input, output) =>
        {
            output["Clothing3"].Layer(13);
            output["Clothing2"].Layer(20);
            output["Clothing1"].Layer(16);
            int weightMod = input.Actor.Unit.BodySize;
            int bobs = input.Actor.Unit.BreastSize;
            if (bobs > 7)
            {
                bobs = 7;
            }

            int size = input.Actor.GetStomachSize(32, 1.2f);
            if (size > 5)
            {
                size = 5;
            }

            if (input.Actor.Unit.HasBreasts)
            {
                if (input.Actor.Unit.HasDick)
                {
                    output["Clothing3"].Sprite(input.Sprites.NewimpUBottoms[45 + input.Actor.Unit.DickSize]);
                }

                output["Clothing2"].Sprite(input.Sprites.NewimpOnePieces[0 + bobs]);
                output["Clothing1"].Sprite(input.Sprites.NewimpOnePieces[8 + size + 6 * weightMod]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
                output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
                //bellyPalette = ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SkinToClothing, input.Actor.Unit.ClothingColor);
            }
            else
            {
                output["Clothing3"].Sprite(input.Sprites.NewimpUBottoms[45 + input.Actor.Unit.DickSize]);
                output["Clothing2"].Sprite(null);
                output["Clothing1"].Sprite(input.Sprites.NewimpOnePieces[20 + size + 6 * weightMod]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced,
                    input.Actor.Unit.ClothingColor2));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced,
                    input.Actor.Unit.ClothingColor2));
                output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced,
                    input.Actor.Unit.ClothingColor2));
            }
        });
    });
}

internal static class ImpOBottom
{
    internal static readonly IClothing GenericBottom1 = MakeImpOBottom(0, 2, false, 45, 8, 15, State.GameManager.SpriteDictionary.NewimpOBottoms, 8908);
    internal static readonly IClothing GenericBottom2 = MakeImpOBottom(9, 11, false, 45, 17, 15, State.GameManager.SpriteDictionary.NewimpOBottoms, 8917);
    internal static readonly IClothing GenericBottom3 = MakeImpOBottom(18, 20, true, 45, 26, 15, State.GameManager.SpriteDictionary.NewimpOBottoms, 8926);
    internal static readonly IClothing GenericBottom4 = MakeImpOBottom(27, 29, true, 49, 35, 15, State.GameManager.SpriteDictionary.NewimpOBottoms, 8935);
    internal static readonly IClothing GenericBottom5 = MakeImpOBottom(36, 38, false, 45, 44, 15, State.GameManager.SpriteDictionary.NewimpOBottoms, 8944);

    private static IClothing MakeImpOBottom(int sprF, int sprM, bool showbulge, int bulge, int discard, int layer,
        Sprite[] sheet, int type)
    {
        ClothingBuilder builder = ClothingBuilder.New();

        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.RevealsBreasts = true;
            output.DiscardSprite = sheet[discard];
            output.Type = type;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(layer);
            output["Clothing1"].Coloring(Color.white);
            output["Clothing2"].Layer(layer + 1);
            output["Clothing2"].Coloring(Color.white);
            int weightMod = input.Actor.Unit.BodySize;
            if (input.Actor.HasBelly)
            {
                output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? sheet[sprF + 4 + weightMod] : sheet[sprM + 4 + weightMod]);

                if (input.Actor.Unit.HasDick && showbulge)
                {
                    //if (output.BlocksDick == true)
                    if (true)
                    {
                        output["Clothing2"].Sprite(input.Sprites.NewimpUBottoms[Math.Min(bulge + input.Actor.Unit.DickSize, 52)]);
                    }
                }
                else
                {
                    output["Clothing2"].Sprite(null);
                }
            }
            else
            {
                output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? sheet[sprF + weightMod] : sheet[sprM + weightMod]);

                if (input.Actor.Unit.HasDick)
                {
                    //if (output.BlocksDick == true && showbulge == true)
                    output["Clothing2"].Sprite(showbulge ? input.Sprites.NewimpUBottoms[Math.Min(bulge + input.Actor.Unit.DickSize, 52)] : null);
                }
                else
                {
                    output["Clothing2"].Sprite(null);
                }
            }

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
        });
        return builder.BuildClothing();
    }
}

internal static class ImpOBottomAlt
{
    internal static readonly IClothing ImpOBottomAlt1 = MakeImpOBottomAlt(27, 29, true, 49, 35, 15, State.GameManager.SpriteDictionary.NewimpOBottoms, 8935);

    private static IClothing MakeImpOBottomAlt(int sprF, int sprM, bool showbulge, int bulge, int discard,
        int layer, Sprite[] sheet, int type)
    {
        ClothingBuilder builder = ClothingBuilder.New();


        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.RevealsBreasts = true;
            output.DiscardSprite = sheet[discard];
            output.Type = type;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(layer);
            output["Clothing1"].Coloring(Color.white);
            output["Clothing2"].Layer(layer + 1);
            output["Clothing2"].Coloring(Color.white);
            int weightMod = input.Actor.Unit.BodySize;
            if (input.Actor.HasBelly)
            {
                output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? sheet[sprF + 4 + weightMod] : sheet[sprM + 4 + weightMod]);

                if (input.Actor.Unit.HasDick && showbulge)
                {
                    // if (output.BlocksDick == true)
                    if (true)
                    {
                        output["Clothing2"].Sprite(input.Sprites.NewimpUBottoms[Math.Min(bulge + input.Actor.Unit.DickSize, 52)]);
                    }
                }
                else
                {
                    output["Clothing2"].Sprite(null);
                }
            }
            else
            {
                output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? sheet[sprF + weightMod] : sheet[sprM + weightMod]);

                if (input.Actor.Unit.HasDick)
                {
                    //if (output.BlocksDick == true && showbulge == true)
                    output["Clothing2"].Sprite(showbulge ? input.Sprites.NewimpUBottoms[Math.Min(bulge + input.Actor.Unit.DickSize, 52)] : null);
                }
                else
                {
                    output["Clothing2"].Sprite(null);
                }
            }

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
        });
        return builder.BuildClothing();
    }
}

internal static class Hat
{
    internal static readonly IClothing Hat1 = MakeHat(0, 0, State.GameManager.SpriteDictionary.NewimpHats, 666);
    internal static readonly IClothing Hat2 = MakeHat(34, 0, State.GameManager.SpriteDictionary.NewimpHats, 666);

    private static IClothing MakeHat(int start, int discard, Sprite[] sheet, int type)
    {
        ClothingBuilder builder = ClothingBuilder.New();

        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.DiscardSprite = sheet[discard];
            output.Type = type;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(2);
            output["Clothing1"].Layer(22);
            output["Clothing1"].Sprite(sheet[start]);
            output["Clothing2"].Sprite(sheet[start + 1]);

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
        });
        return builder.BuildClothing();
    }
}

internal static class NewImpUndertop1
{
    internal static readonly IClothing<IOverSizeParameters> NewImpUndertop1Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.NewimpUTops[8];
            output.Type = 11012;
            output.FemaleOnly = true;
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(20);
            if (input.Params.Oversize)
            {
                output["Clothing1"].Sprite(input.Sprites.NewimpUTops[7]);
            }
            else if (input.Actor.Unit.HasBreasts)
            {
                output["Clothing1"].Sprite(input.Sprites.NewimpUTops[0 + input.Actor.Unit.BreastSize]);
            }

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
        });
    });
}

internal static class NewImpUndertop2
{
    internal static readonly IClothing<IOverSizeParameters> NewImpUndertop2Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.NewimpUTops[17];
            output.Type = 11013;
            output.FemaleOnly = true;
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(20);
            if (input.Params.Oversize)
            {
                output["Clothing1"].Sprite(input.Sprites.NewimpUTops[16]);
            }
            else if (input.Actor.Unit.HasBreasts)
            {
                output["Clothing1"].Sprite(input.Sprites.NewimpUTops[9 + input.Actor.Unit.BreastSize]);
            }

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
        });
    });
}

internal static class NewImpUndertop3
{
    internal static readonly IClothing<IOverSizeParameters> NewImpUndertop3Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.NewimpUTops[26];
            output.Type = 11014;
            output.FemaleOnly = true;
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(20);
            if (input.Params.Oversize)
            {
                output["Clothing1"].Sprite(input.Sprites.NewimpUTops[25]);
            }
            else if (input.Actor.Unit.HasBreasts)
            {
                output["Clothing1"].Sprite(input.Sprites.NewimpUTops[18 + input.Actor.Unit.BreastSize]);
            }

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
        });
    });
}

internal static class NewImpUndertop4
{
    internal static readonly IClothing<IOverSizeParameters> NewImpUndertop4Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.NewimpUTops[35];
            output.Type = 11015;
            output.FemaleOnly = true;
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(20);
            if (input.Params.Oversize)
            {
                output["Clothing1"].Sprite(input.Sprites.NewimpUTops[34]);
            }
            else if (input.Actor.Unit.HasBreasts)
            {
                output["Clothing1"].Sprite(input.Sprites.NewimpUTops[27 + input.Actor.Unit.BreastSize]);
            }

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
        });
    });
}

internal static class NewImpUndertop5
{
    internal static readonly IClothing<IOverSizeParameters> NewImpUndertop5Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.NewimpUTops[75];
            output.Type = 11016;
            output.FemaleOnly = false;
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(16);
            output["Clothing1"].Layer(20);
            int weightMod = input.Actor.Unit.BodySize;
            int size = input.Actor.GetStomachSize(32, 1.2f);
            if (size > 5)
            {
                size = 5;
            }

            if (input.Params.Oversize)
            {
                output["Clothing1"].Sprite(input.Sprites.NewimpUTops[48 + 13 * weightMod]);
            }
            else if (input.Actor.Unit.HasBreasts)
            {
                output["Clothing1"].Sprite(input.Sprites.NewimpUTops[42 + input.Actor.Unit.BreastSize + 13 * weightMod]);
            }

            output["Clothing2"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.NewimpUTops[36 + size + 13 * weightMod] : input.Sprites.NewimpUTops[62 + size + 6 * weightMod]);

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
        });
    });
}

internal static class NewImpOverOPFem
{
    internal static readonly IClothing<IOverSizeParameters> NewImpOverOPFemInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.NewImpOverOnePieces[23];
            output.Type = 11017;
            output.FemaleOnly = true;
            output.RevealsBreasts = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(16);
            output["Clothing1"].Layer(20);
            int weightMod = input.Actor.Unit.BodySize;
            int size = input.Actor.GetStomachSize(32, 1.2f);
            if (size > 8)
            {
                size = 8;
            }

            int bobs = input.Actor.Unit.BreastSize;
            if (bobs > 7)
            {
                bobs = 7;
            }

            {
                if (input.Params.Oversize || input.Actor.Unit.HasBreasts == false)
                {
                    output["Clothing1"].Sprite(null);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.NewImpOverOnePieces[bobs]);
                }
            }
            output["Clothing2"].Sprite(input.Sprites.NewImpOverOnePieces[7 + size + 8 * weightMod]);

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor2));
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
            output.Type = 11018;
            output.MaleOnly = true;
            output.RevealsBreasts = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(20);
            output["Clothing1"].Layer(20);
            int weightMod = input.Actor.Unit.BodySize;
            int size = input.Actor.GetStomachSize(32, 1.2f);
            if (size > 6)
            {
                size = 6;
            }

            output["Clothing2"].Sprite(input.Actor.IsAttacking ? input.Sprites.NewImpOverOnePieces[25] : input.Sprites.NewImpOverOnePieces[24]);


            output["Clothing1"].Sprite(input.Sprites.NewImpOverOnePieces[26 + size + 7 * weightMod]);

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
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
            output.Type = 11019;
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(3);
            output["Clothing1"].Layer(21);
            int weightMod = input.Actor.Unit.BodySize;


            output["Clothing1"].Sprite(input.Sprites.NewImpOTops[0 + weightMod]);
            output["Clothing2"].Sprite(input.Sprites.NewImpOTops[2]);

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
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
            output.Type = 11020;
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(3);
            output["Clothing1"].Layer(21);
            int weightMod = input.Actor.Unit.BodySize;


            output["Clothing1"].Sprite(input.Sprites.NewImpOTops[4 + weightMod]);
            output["Clothing2"].Sprite(input.Sprites.NewImpOTops[6]);

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
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
            output.Type = 11021;
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(3);
            output["Clothing1"].Layer(21);
            int weightMod = input.Actor.Unit.BodySize;


            output["Clothing1"].Sprite(input.Sprites.NewImpOTops[8 + weightMod]);
            output["Clothing2"].Sprite(input.Sprites.NewImpOTops[10]);

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
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
            output.Type = 11022;
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(3);
            output["Clothing1"].Layer(21);
            int weightMod = input.Actor.Unit.BodySize;


            output["Clothing1"].Sprite(input.Sprites.NewImpOTops[12 + weightMod]);
            output["Clothing2"].Sprite(input.Sprites.NewImpOTops[14]);

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing50Spaced, input.Actor.Unit.ClothingColor));
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
            output.Type = 0;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(29);
            output["Clothing1"].Coloring(Color.white);
            output["Clothing1"].Sprite(input.Sprites.ImpGobHat[0]);
        });
    });
}
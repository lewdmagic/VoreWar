#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.MainRaces
{
    internal static class Panthers
    {
        private static Func<IClothingRenderInput, IOverSizeParameters> paramsCalc = CommonRaceCode.MakeOversizeFunc(32 * 32);
        internal static List<IClothing> AllClothing;

        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Panther", "Panthers");
                output.FlavorText(new FlavorText(
                    new Texts {  },
                    new Texts {  },
                    new Texts {  },
                    new Dictionary<string, string>
                    {
                        [WeaponNames.Mace]        = "Karambit",
                        [WeaponNames.Axe]         = "Kukri",
                        [WeaponNames.SimpleBow]   = "Chakram",
                        [WeaponNames.CompoundBow] = "Onzil"
                    }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 10,
                    StomachSize = 15,
                    HasTail = true,
                    FavoredStat = Stat.Strength,
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.TasteForBlood,
                        TraitType.Pounce
                    },
                    RaceDescription = "Long before \"elder races\" walked among the stars, the Panthers thrived. Long before first words of power were uttered, they have carved their homes into the lightning-struck bark, feeding off its power. And long after the last bastion of so-called civilization will fall to onslaught of wings, claws and fangs, they will thrive in the darkness, pouncing on unsuspecting prey.",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.EyeType, "Face Type");
                    buttons.SetText(ButtonType.ClothingColor, "Innerwear Color");
                    buttons.SetText(ButtonType.ClothingColor2, "Outerwear Color");
                    buttons.SetText(ButtonType.ClothingColor3, "Clothing Accent Color");
                    buttons.SetText(ButtonType.BodyAccentTypes1, "Arm Bodypaint");
                    buttons.SetText(ButtonType.BodyAccentTypes2, "Shoulder Bodypaint");
                    buttons.SetText(ButtonType.BodyAccentTypes3, "Feet Bodypaint");
                    buttons.SetText(ButtonType.BodyAccentTypes4, "Thigh Bodypaint");
                    buttons.SetText(ButtonType.BodyAccentTypes5, "Face Bodypaint");
                    buttons.SetText(ButtonType.BodyAccessoryColor, "Bodypaint Color");
                    buttons.SetText(ButtonType.ClothingExtraType1, "Over Tops");
                    buttons.SetText(ButtonType.ClothingExtraType2, "Over Bottoms");
                    buttons.SetText(ButtonType.ClothingExtraType3, "Hats");
                    buttons.SetText(ButtonType.ClothingExtraType4, "Gloves");
                    buttons.SetText(ButtonType.ClothingExtraType5, "Legs");
                });
                output.TownNames(new List<string>
                {
                    "Panthera",
                    "Wakana",
                    "Therishi",
                    "Vogoma",
                    "Kwa-Duka",
                    "Hlobava",
                    "Plundi",
                    "Pumatra",
                    "Khangela",
                    "Qweni-Sho",
                    "Sitting Belly",
                    "Endless Feast",
                    "Gurgling Tents",
                });
                output.BreastSizes = () => 8;
                output.DickSizes = () => 4;
                output.EyeTypes = 2;
                output.HairStyles = 14;

                output.BodyAccentTypes1 = 4;
                output.BodyAccentTypes2 = 4;
                output.BodyAccentTypes3 = 3;
                output.BodyAccentTypes4 = 5;
                output.BodyAccentTypes5 = 4;

                output.ExtendedBreastSprites = true;

                output.HairColors = ColorPaletteMap.GetPaletteCount(SwapType.PantherHair);
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.PantherSkin);
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.PantherBodyPaint);

                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.PantherClothes);

                List<IClothing> allowedMainClothingTypes = new List<IClothing>
                {
                    All.GenericFemaleTop1.Create(paramsCalc),
                    All.BeltTop1.Create(paramsCalc),
                    All.GenericFemaleTop2.Create(paramsCalc),
                    All.GenericFemaleTop3.Create(paramsCalc),
                    All.GenericFemaleTop4.Create(paramsCalc),
                    All.GenericFemaleTop5.Create(paramsCalc),
                    All.GenericFemaleTop6.Create(paramsCalc),
                    All.Simple1,
                    All.Simple2,
                    All.Simple3,
                    All.Simple4,
                    All.Simple5,
                    All.GenericOnepiece1.Create(paramsCalc),
                    All.GenericOnepiece2.Create(paramsCalc),
                    All.GenericOnepiece3.Create(paramsCalc),
                    All.GenericOnepiece4.Create(paramsCalc)
                };

                output.AllowedMainClothingTypes.Set(allowedMainClothingTypes);

                List<IClothing> allowedWaistTypes = new List<IClothing>() //Bottoms
                {
                    All.GenericBottom1,
                    All.GenericBottom2,
                    All.GenericBottom3,
                    All.GenericBottom4,
                    All.GenericBottom5,
                    All.GenericBottom6
                };
                output.AllowedWaistTypes.Set(allowedWaistTypes);

                List<IClothing> extraMainClothing1Types = new List<IClothing>() //Overtops
                {
                    All.GenericFemaleTop7.Create(paramsCalc),
                    All.SimpleAttack1,
                    All.GenericFemaleTop8.Create(paramsCalc),
                    All.BoneTop1.Create(paramsCalc),
                    All.Simple6,
                    All.Simple7,
                    All.SimpleAttack2,
                    All.SimpleAttack3
                };
                output.ExtraMainClothing1Types.Set(extraMainClothing1Types); //Overtops

                List<IClothing> extraMainClothing2Types = new List<IClothing>() //Overbottoms
                {
                    All.Simple8,
                    All.OverbottomTwoTone1,
                    All.Simple9,
                    All.OverbottomTwoTone2,
                    All.Simple10,
                    All.Simple11,
                    All.Simple12
                };
                output.ExtraMainClothing2Types.Set(extraMainClothing2Types); //Overbottoms

                List<IClothing> extraMainClothing3Types = new List<IClothing>() //Hats
                {
                    All.GenericItem1,
                    All.GenericItem2,
                    SantaHat.SantaHatInstance
                };
                output.ExtraMainClothing3Types.Set(extraMainClothing3Types); //Hats

                List<IClothing> extraMainClothing4Types = new List<IClothing>() //Gloves
                {
                    All.GenericGlovesPlusSecond1,
                    All.GenericGloves1,
                    All.GenericGloves2,
                    All.GenericGloves3,
                    All.GenericGloves4,
                    All.GenericGloves5,
                    All.GenericGloves6
                };
                output.ExtraMainClothing4Types.Set(extraMainClothing4Types); //Gloves

                List<IClothing> extraMainClothing5Types = new List<IClothing>() //Legs
                {
                    All.GenericItem3,
                    All.GenericItem4,
                    All.GenericItem5,
                    All.GenericItem6,
                    All.GenericItem7
                };
                output.ExtraMainClothing5Types.Set(extraMainClothing5Types); //Legs

                AllClothing = new List<IClothing>();
                AllClothing.AddRange(allowedMainClothingTypes);
                AllClothing.AddRange(allowedWaistTypes);
                AllClothing.AddRange(extraMainClothing1Types);
                AllClothing.AddRange(extraMainClothing2Types);
                AllClothing.AddRange(extraMainClothing3Types);
                AllClothing.AddRange(extraMainClothing4Types);
                AllClothing.AddRange(extraMainClothing5Types);
            });


            builder.RunBefore((input, output) =>
            {
                Defaults.BasicBellyRunAfter.Invoke(input, output);
            });


            builder.RenderSingle(SpriteType.Head, 25, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.PantherSkin, input.U.SkinColor));
                int attackingOffset = input.A.IsAttacking || input.A.IsEating ? 2 : 0;
                int genderOffset = input.U.HasBreasts ? 0 : 1;
                output.Sprite(input.Sprites.PantherBase[4 + genderOffset + attackingOffset + 4 * input.U.EyeType]);
            });

            builder.RenderSingle(SpriteType.Hair, 27, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.PantherHair, input.U.HairColor));
                switch (input.U.HairStyle)
                {
                    case 0:
                        output.Sprite(input.Sprites.PantherBase[46]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.PantherBase[49]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.PantherBase[51]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.PantherBase[54]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.PantherBase[57]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.PantherBase[60]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.PantherBase[62]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.PantherBase[64]);
                        return;
                    case 8:
                        output.Sprite(input.Sprites.PantherBase[65]);
                        return;
                    case 9:
                        output.Sprite(input.Sprites.PantherBase[66]);
                        return;
                    case 10:
                        output.Sprite(input.Sprites.PantherBase[68]);
                        return;
                    case 11:
                        output.Sprite(input.Sprites.PantherBase[70]);
                        return;
                    case 12:
                        output.Sprite(input.Sprites.PantherBase[72]);
                        return;
                    case 13:
                        output.Sprite(input.Sprites.PantherBase[73]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.Hair2, 0, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.PantherHair, input.U.HairColor));
                switch (input.U.HairStyle)
                {
                    case 0:
                        output.Sprite(input.Sprites.PantherBase[48]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.PantherBase[53]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.PantherBase[56]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.PantherBase[59]);
                        return;
                    case 10:
                        output.Sprite(input.Sprites.PantherBase[69]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.Hair3, 27, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.PantherClothes, input.U.ClothingColor3));
                switch (input.U.HairStyle)
                {
                    case 0:
                        output.Sprite(input.Sprites.PantherBase[47]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.PantherBase[50]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.PantherBase[52]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.PantherBase[55]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.PantherBase[58]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.PantherBase[61]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.PantherBase[63]);
                        return;
                    case 9:
                        output.Sprite(input.Sprites.PantherBase[67]);
                        return;
                    case 11:
                        output.Sprite(input.Sprites.PantherBase[71]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.PantherSkin, input.U.SkinColor));
                int attackingOffset = input.A.IsAttacking ? 2 : 0;
                int genderOffset = input.U.HasBreasts ? 0 : 1;
                output.Sprite(input.Sprites.PantherBase[genderOffset + attackingOffset]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.PantherBodyPaint, input.U.AccessoryColor));
                if (input.U.BodyAccentType1 == 0)
                {
                    return;
                }

                if (input.U.BodyAccentType1 >= input.RaceData.BodyAccentTypes1)
                {
                    input.U.BodyAccentType1 = input.RaceData.BodyAccentTypes1 - 1;
                }

                int genderOffset = input.U.HasBreasts ? 0 : 1;
                int attackingOffset = input.A.IsAttacking ? 2 : 0;
                output.Sprite(input.Sprites.PantherBase[74 + genderOffset + attackingOffset + 8 * (input.U.BodyAccentType1 - 1)]);
            });

            builder.RenderSingle(SpriteType.BodyAccent2, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.PantherBodyPaint, input.U.AccessoryColor));
                if (input.U.BodyAccentType2 == 0)
                {
                    return;
                }

                if (input.U.BodyAccentType2 >= input.RaceData.BodyAccentTypes2)
                {
                    input.U.BodyAccentType2 = input.RaceData.BodyAccentTypes2 - 1;
                }

                int genderOffset = input.U.HasBreasts ? 0 : 1;
                int attackingOffset = input.A.IsAttacking ? 2 : 0;
                output.Sprite(input.Sprites.PantherBase[78 + genderOffset + attackingOffset + 8 * (input.U.BodyAccentType2 - 1)]);
            });

            builder.RenderSingle(SpriteType.BodyAccent3, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.PantherBodyPaint, input.U.AccessoryColor));
                if (input.U.BodyAccentType3 == 0)
                {
                    return;
                }

                if (input.U.BodyAccentType3 >= input.RaceData.BodyAccentTypes3)
                {
                    input.U.BodyAccentType3 = input.RaceData.BodyAccentTypes3 - 1;
                }

                int genderOffset = input.U.HasBreasts ? 0 : 1;
                output.Sprite(input.Sprites.PantherBase[98 + genderOffset + 2 * (input.U.BodyAccentType3 - 1)]);
            });

            builder.RenderSingle(SpriteType.BodyAccent4, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.PantherBodyPaint, input.U.AccessoryColor));
                if (input.U.BodyAccentType4 == 0)
                {
                    return;
                }

                if (input.U.BodyAccentType4 >= input.RaceData.BodyAccentTypes4)
                {
                    input.U.BodyAccentType4 = input.RaceData.BodyAccentTypes4 - 1;
                }

                int genderOffset = input.U.HasBreasts ? 0 : 1;
                output.Sprite(input.Sprites.PantherBase[102 + genderOffset + 2 * (input.U.BodyAccentType4 - 1)]);
            });

            builder.RenderSingle(SpriteType.BodyAccent5, 26, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.PantherBodyPaint, input.U.AccessoryColor));
                if (input.U.BodyAccentType5 == 0)
                {
                    return;
                }

                if (input.U.BodyAccentType5 >= input.RaceData.BodyAccentTypes5)
                {
                    input.U.BodyAccentType5 = input.RaceData.BodyAccentTypes5 - 1;
                }

                int genderOffset = input.U.HasBreasts ? 0 : 1;
                int attackingOffset = input.A.IsAttacking ? 2 : 0;
                output.Sprite(input.Sprites.PantherBase[110 + genderOffset + attackingOffset + 4 * (input.U.BodyAccentType5 - 1)]);
            });

            builder.RenderSingle(SpriteType.BodyAccent6, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.PantherSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.PantherBase[20 + (input.U.HasBreasts ? 0 : 1)]);
            });

            builder.RenderSingle(SpriteType.BodySize, 23, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    int weaponSprite = input.A.GetWeaponSprite();
                    int genderMod = input.U.HasBreasts ? 0 : 1;
                    switch (weaponSprite)
                    {
                        case 1:
                            output.Sprite(input.Sprites.PantherBase[26 + genderMod]);
                            return;
                        case 3:
                            output.Sprite(input.Sprites.PantherBase[32 + genderMod]);
                            return;
                    }
                }
            }); // Weapon Flash

            builder.RenderSingle(SpriteType.Breasts, 19, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.PantherSkin, input.U.SkinColor));
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


                    output.Sprite(input.Sprites.PantherVoreParts[4 + leftSize]);
                }
                else
                {
                    if (input.U.DefaultBreastSize == 0)
                    {
                        output.Sprite(input.Sprites.PantherVoreParts[0]);
                        return;
                    }

                    if (input.A.SquishedBreasts && input.U.BreastSize < 6 && input.U.BreastSize >= 4)
                    {
                        output.Sprite(input.Sprites.PantherVoreParts[-2 + input.U.BreastSize]);
                        return;
                    }

                    output.Sprite(input.Sprites.PantherVoreParts[3 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.SecondaryBreasts, 19, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.PantherSkin, input.U.SkinColor));
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

                    output.Sprite(input.Sprites.PantherVoreParts[39 + rightSize]);
                }
                else
                {
                    if (input.U.DefaultBreastSize == 0)
                    {
                        output.Sprite(input.Sprites.PantherVoreParts[140]);
                        return;
                    }

                    if (input.A.SquishedBreasts && input.U.BreastSize < 6 && input.U.BreastSize >= 4)
                    {
                        output.Sprite(input.Sprites.PantherVoreParts[33 + input.U.BreastSize]);
                        return;
                    }

                    output.Sprite(input.Sprites.PantherVoreParts[38 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 16, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.PantherSkin, input.U.SkinColor));
                if (input.A.HasBelly)
                {
                    int size = input.A.GetStomachSize(32, 1.2f);

                    size = input.A.GetStomachSize(29, 0.8f);
                    switch (size)
                    {
                        case 24:
                            output.AddOffset(0, -10 * .625f);
                            break;
                        case 25:
                            output.AddOffset(0, -13 * .625f);
                            break;
                        case 26:
                            output.AddOffset(0, -16 * .625f);
                            break;
                        case 27:
                            output.AddOffset(0, -19 * .625f);
                            break;
                        case 28:
                            output.AddOffset(0, -22 * .625f);
                            break;
                        case 29:
                            output.AddOffset(0, -27 * .625f);
                            break;
                    }

                    if (input.A.PredatorComponent.OnlyOnePreyAndLiving() && size >= 8 && size <= 13)
                    {
                        output.Sprite(input.Sprites.PantherVoreParts[71]);
                        return;
                    }

                    output.Sprite(input.Sprites.PantherVoreParts[74 + size]);
                }
            });

            builder.RenderSingle(SpriteType.Dick, 14, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.PantherSkin, input.U.SkinColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect())
                {
                    if (input.A.PredatorComponent?.VisibleFullness < .75f)
                    {
                        output.Sprite(input.Sprites.PantherBase[16 + input.U.DickSize]).Layer(18);
                        return;
                    }

                    output.Sprite(input.Sprites.PantherBase[16 + input.U.DickSize]).Layer(14);
                    return;
                }

                output.Sprite(input.Sprites.PantherBase[12 + input.U.DickSize]).Layer(14);
            });

            builder.RenderSingle(SpriteType.Balls, 13, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.PantherSkin, input.U.SkinColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.U.Predator == false)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[107 + input.U.BallsSize]);
                    return;
                }

                int size = input.A.GetBallSize(31, .8f);

                if (size > 29)
                {
                    size = 29;
                }

                switch (size)
                {
                    case 26:
                        output.AddOffset(0, -2 * .625f);
                        break;
                    case 27:
                        output.AddOffset(0, -4 * .625f);
                        break;
                    case 28:
                        output.AddOffset(0, -7 * .625f);
                        break;
                    case 29:
                        output.AddOffset(0, -9 * .625f);
                        break;
                    // Cases are unreachable
                    // case 30: 
                    //     output.AddOffset(0, -10 * .625f);
                    //     break;
                    // case 31:
                    //     output.AddOffset(0, -13 * .625f);
                    //     break;
                }

                output.Sprite(input.Sprites.PantherVoreParts[107 + size]);
            });

            builder.RenderSingle(SpriteType.Weapon, 12, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    int weaponSprite = input.A.GetWeaponSprite();
                    int genderMod = input.U.HasBreasts ? 0 : 1;
                    switch (weaponSprite)
                    {
                        case 0:
                            output.Sprite(input.Sprites.PantherBase[22 + genderMod]);
                            return;
                        case 1:
                            output.Sprite(input.Sprites.PantherBase[24 + genderMod]);
                            return;
                        case 2:
                            output.Sprite(input.Sprites.PantherBase[28 + genderMod]);
                            return;
                        case 3:
                            if (genderMod == 0)
                            {
                                output.AddOffset(4 * .625f, 0);
                            }
                            else
                            {
                                output.AddOffset(4 * .625f, 0);
                            }

                            output.Sprite(input.Sprites.PantherBase[30 + genderMod]).AddOffset(5 * .625f, 0);
                            return;
                        case 4:
                            output.Sprite(input.Sprites.PantherBase[34 + genderMod]);
                            return;
                        case 5:
                            return;
                        case 6:
                            output.Sprite(input.Sprites.PantherBase[40 + genderMod]);
                            return;
                        case 7:
                            return;
                    }
                }
            });

            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                Unit unit = data.Unit;

                unit.EyeType = State.Rand.Next(10) == 0 ? 1 : 0;

                unit.ClothingColor = State.Rand.Next(data.SetupOutput.ClothingColors);
                unit.ClothingColor2 = State.Rand.Next(data.SetupOutput.ClothingColors);
                unit.ClothingColor3 = State.Rand.Next(data.SetupOutput.ClothingColors);
            });
        });

        private static Color? CalcColor(ColorStyle style)
        {
            switch (style)
            {
                case ColorStyle.InnerWear:
                    return null;
                case ColorStyle.OuterWear:
                    return null;
                case ColorStyle.Other:
                    return null;
                case ColorStyle.None:
                    return Color.white;
            }

            return null;
        }

        private static ColorSwapPalette CalcPalette(ColorStyle style, Actor_Unit actor)
        {
            switch (style)
            {
                case ColorStyle.InnerWear:
                    return ColorPaletteMap.GetPalette(SwapType.PantherClothes, actor.Unit.ClothingColor);
                case ColorStyle.OuterWear:
                    return ColorPaletteMap.GetPalette(SwapType.PantherClothes, actor.Unit.ClothingColor2);
                case ColorStyle.Other:
                    return ColorPaletteMap.GetPalette(SwapType.PantherClothes, actor.Unit.ClothingColor3);
                case ColorStyle.None:
                    return null;
            }

            return null;
        }


        private enum ColorStyle
        {
            InnerWear,
            OuterWear,
            Other,
            None
        }


        private static class All
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericFemaleTop1 = ClothingBuilder.CreateV2<IOverSizeParameters>( b =>
            {
                GenericFemaleTop.MakeGenericFemaleTop(b, 0, 10, 20, State.GameManager.SpriteDictionary.PantherFTops, new ClothingId("base.panthers/801"), ColorStyle.InnerWear);
            });
            internal static readonly BindableClothing<IOverSizeParameters> BeltTop1 = ClothingBuilder.CreateV2<IOverSizeParameters>( b =>
            {
                BeltTop.MakeBeltTop(b, 20);
            });
            internal static readonly BindableClothing<IOverSizeParameters> GenericFemaleTop2 = ClothingBuilder.CreateV2<IOverSizeParameters>( b =>
            {
                GenericFemaleTop.MakeGenericFemaleTop(b, 19, 29, 20, State.GameManager.SpriteDictionary.PantherFTops, new ClothingId("base.panthers/803"), ColorStyle.InnerWear);
            });
            internal static readonly BindableClothing<IOverSizeParameters> GenericFemaleTop3 = ClothingBuilder.CreateV2<IOverSizeParameters>( b =>
            {
                GenericFemaleTop.MakeGenericFemaleTop(b, 24, 34, 20, State.GameManager.SpriteDictionary.PantherFTops, new ClothingId("base.panthers/804"), ColorStyle.None);
            });
            internal static readonly BindableClothing<IOverSizeParameters> GenericFemaleTop4 = ClothingBuilder.CreateV2<IOverSizeParameters>( b =>
            {
                GenericFemaleTop.MakeGenericFemaleTop(b, 39, 49, 20, State.GameManager.SpriteDictionary.PantherFTops, new ClothingId("base.panthers/805"), ColorStyle.InnerWear);
            });
            internal static readonly BindableClothing<IOverSizeParameters> GenericFemaleTop5 = ClothingBuilder.CreateV2<IOverSizeParameters>( b =>
            {
                GenericFemaleTop.MakeGenericFemaleTop(b, 44, 54, 20, State.GameManager.SpriteDictionary.PantherFTops, new ClothingId("base.panthers/806"), ColorStyle.None);
            });
            internal static readonly BindableClothing<IOverSizeParameters> GenericFemaleTop6 = ClothingBuilder.CreateV2<IOverSizeParameters>( b =>
            {
                GenericFemaleTop.MakeGenericFemaleTop(b, 59, 64, 20, State.GameManager.SpriteDictionary.PantherFTops, new ClothingId("base.panthers/807"), ColorStyle.InnerWear);
            });
            internal static readonly IClothing Simple1 = ClothingBuilder.Create( b =>
            {
                Simple.MakeSimple(b, 0, 5, 20, State.GameManager.SpriteDictionary.PantherMTops, new ClothingId("base.panthers/808"), ColorStyle.None, true);
            });
            internal static readonly IClothing Simple2 = ClothingBuilder.Create( b =>
            {
                Simple.MakeSimple(b, 1, 6, 20, State.GameManager.SpriteDictionary.PantherMTops, new ClothingId("base.panthers/809"), ColorStyle.InnerWear, true);
            });
            internal static readonly IClothing Simple3 = ClothingBuilder.Create( b =>
            {
                Simple.MakeSimple(b, 2, 7, 20, State.GameManager.SpriteDictionary.PantherMTops, new ClothingId("base.panthers/810"), ColorStyle.InnerWear, true);
            });
            internal static readonly IClothing Simple4 = ClothingBuilder.Create( b =>
            {
                Simple.MakeSimple(b, 3, 8, 20, State.GameManager.SpriteDictionary.PantherMTops, new ClothingId("base.panthers/811"), ColorStyle.None, true);
            });
            internal static readonly IClothing Simple5 = ClothingBuilder.Create( b =>
            {
                Simple.MakeSimple(b, 4, 9, 20, State.GameManager.SpriteDictionary.PantherMTops, new ClothingId("base.panthers/812"), ColorStyle.None, true);
            });
            internal static readonly BindableClothing<IOverSizeParameters> GenericOnepiece1 = ClothingBuilder.CreateV2<IOverSizeParameters>( b =>
            {
                GenericOnepiece.MakeGenericOnepiece(b, 0, 5, 18, false);
            });
            internal static readonly BindableClothing<IOverSizeParameters> GenericOnepiece2 = ClothingBuilder.CreateV2<IOverSizeParameters>( b =>
            {
                GenericOnepiece.MakeGenericOnepiece(b, 9, 14, 18, false);
            });
            internal static readonly BindableClothing<IOverSizeParameters> GenericOnepiece3 = ClothingBuilder.CreateV2<IOverSizeParameters>( b =>
            {
                GenericOnepiece.MakeGenericOnepiece(b, 52, 57, 69, true);
            });
            internal static readonly BindableClothing<IOverSizeParameters> GenericOnepiece4 = ClothingBuilder.CreateV2<IOverSizeParameters>( b =>
            {
                GenericOnepiece.MakeGenericOnepiece(b, 60, 65, 69, false);
            });


            internal static readonly IClothing GenericBottom1 = ClothingBuilder.Create( b =>
            {
                GenericBottom.MakeGenericBottom(b, 0, 0, 12, 6, 8, State.GameManager.SpriteDictionary.PantherBottoms, new ClothingId("base.panthers/840"), true);
            });
            internal static readonly IClothing GenericBottom2 = ClothingBuilder.Create( b =>
            {
                GenericBottom.MakeGenericBottom(b, 1, 1, 12, 7, 8, State.GameManager.SpriteDictionary.PantherBottoms, new ClothingId("base.panthers/841"), true);
            });
            internal static readonly IClothing GenericBottom3 = ClothingBuilder.Create( b =>
            {
                GenericBottom.MakeGenericBottom(b, 2, 2, -2, 8, 8, State.GameManager.SpriteDictionary.PantherBottoms, new ClothingId("base.panthers/842"), true);
            });
            internal static readonly IClothing GenericBottom4 = ClothingBuilder.Create( b =>
            {
                GenericBottom.MakeGenericBottom(b, 3, 17, 14, 9, 8, State.GameManager.SpriteDictionary.PantherBottoms, new ClothingId("base.panthers/843"), false);
            });
            internal static readonly IClothing GenericBottom5 = ClothingBuilder.Create( b =>
            {
                GenericBottom.MakeGenericBottom(b, 16, 4, 12, 10, 8, State.GameManager.SpriteDictionary.PantherBottoms, new ClothingId("base.panthers/844"), true);
            });
            internal static readonly IClothing GenericBottom6 = ClothingBuilder.Create( b =>
            {
                GenericBottom.MakeGenericBottom(b, 5, 5, 12, 11, 8, State.GameManager.SpriteDictionary.PantherBottoms, new ClothingId("base.panthers/845"), true);
            });


            internal static readonly BindableClothing<IOverSizeParameters> GenericFemaleTop7 = ClothingBuilder.CreateV2<IOverSizeParameters>( b =>
            {
                GenericFemaleTop.MakeGenericFemaleTop(b, 0, 5, 21, State.GameManager.SpriteDictionary.PantherFOvertops, new ClothingId("base.panthers/830"), ColorStyle.OuterWear);
            });
            internal static readonly IClothing SimpleAttack1 = ClothingBuilder.Create( b =>
            {
                SimpleAttack.MakeSimpleAttack(b, 20, 21, 22, 21, State.GameManager.SpriteDictionary.PantherFOvertops, new ClothingId("base.panthers/831"), ColorStyle.OuterWear, femaleOnly: true);
            });
            internal static readonly BindableClothing<IOverSizeParameters> GenericFemaleTop8 = ClothingBuilder.CreateV2<IOverSizeParameters>( b =>
            {
                GenericFemaleTop.MakeGenericFemaleTop(b, 10, 15, 21, State.GameManager.SpriteDictionary.PantherFOvertops, new ClothingId("base.panthers/832"), ColorStyle.OuterWear);
            });
            internal static readonly BindableClothing<IOverSizeParameters> BoneTop1 = ClothingBuilder.CreateV2<IOverSizeParameters>( b =>
            {
                BoneTop.MakeBoneTop(b, 21);
            });
            internal static readonly IClothing Simple6 = ClothingBuilder.Create( b =>
            {
                Simple.MakeSimple(b, 0, 6, 21, State.GameManager.SpriteDictionary.PantherMOvertops, new ClothingId("base.panthers/834"), ColorStyle.None, true);
            });
            internal static readonly IClothing Simple7 = ClothingBuilder.Create( b =>
            {
                Simple.MakeSimple(b, 1, 7, 21, State.GameManager.SpriteDictionary.PantherMOvertops, new ClothingId("base.panthers/835"), ColorStyle.OuterWear, true);
            });
            internal static readonly IClothing SimpleAttack2 = ClothingBuilder.Create( b =>
            {
                SimpleAttack.MakeSimpleAttack(b, 2, 4, 8, 21, State.GameManager.SpriteDictionary.PantherMOvertops, new ClothingId("base.panthers/836"), ColorStyle.OuterWear, true);
            });
            internal static readonly IClothing SimpleAttack3 = ClothingBuilder.Create( b =>
            {
                SimpleAttack.MakeSimpleAttack(b, 3, 5, 9, 21, State.GameManager.SpriteDictionary.PantherMOvertops, new ClothingId("base.panthers/837"), ColorStyle.OuterWear, true);
            });


            internal static readonly IClothing Simple8 = ClothingBuilder.Create( b =>
            {
                Simple.MakeSimple(b, 0, 10, 11, State.GameManager.SpriteDictionary.PantherOverBottoms, new ClothingId("base.panthers/860"), ColorStyle.OuterWear, blocksDick: true);
            });
            internal static readonly IClothing OverbottomTwoTone1 = ClothingBuilder.Create( b =>
            {
                OverbottomTwoTone.MakeOverbottomTwoTone(b, 1, 2, 3, 11, 11, new ClothingId("base.panthers/861"));
            });
            internal static readonly IClothing Simple9 = ClothingBuilder.Create( b =>
            {
                Simple.MakeSimple(b, 4, 12, 11, State.GameManager.SpriteDictionary.PantherOverBottoms, new ClothingId("base.panthers/862"), ColorStyle.OuterWear, blocksDick: true);
            });
            internal static readonly IClothing OverbottomTwoTone2 = ClothingBuilder.Create( b =>
            {
                OverbottomTwoTone.MakeOverbottomTwoTone(b, 5, 5, 6, 13, 11, new ClothingId("base.panthers/863"), true);
            });
            internal static readonly IClothing Simple10 = ClothingBuilder.Create( b =>
            {
                Simple.MakeSimple(b, 7, 14, 15, State.GameManager.SpriteDictionary.PantherOverBottoms, new ClothingId("base.panthers/864"), ColorStyle.None, blocksDick: false);
            });
            internal static readonly IClothing Simple11 = ClothingBuilder.Create( b =>
            {
                Simple.MakeSimple(b, 8, 15, 11, State.GameManager.SpriteDictionary.PantherOverBottoms, new ClothingId("base.panthers/865"), ColorStyle.None, femaleOnly: true, blocksDick: true);
            });
            internal static readonly IClothing Simple12 = ClothingBuilder.Create( b =>
            {
                Simple.MakeSimple(b, 9, 16, 11, State.GameManager.SpriteDictionary.PantherOverBottoms, new ClothingId("base.panthers/866"), ColorStyle.None, true, blocksDick: true);
            });


            internal static readonly IClothing GenericItem1 = ClothingBuilder.Create( b =>
            {
                GenericItem.MakeGenericItem(b, 0, 2, 4, 28, State.GameManager.SpriteDictionary.PantherHats, new ClothingId("base.panthers/888"), ColorStyle.None);
            });
            internal static readonly IClothing GenericItem2 = ClothingBuilder.Create( b =>
            {
                GenericItem.MakeGenericItem(b, 1, 3, 5, 28, State.GameManager.SpriteDictionary.PantherHats, new ClothingId("base.panthers/889"), ColorStyle.Other);
            });


            internal static readonly IClothing GenericGlovesPlusSecond1 = ClothingBuilder.Create( b =>
            {
                GenericGlovesPlusSecond.MakeGenericGlovesPlusSecond(b, 0, new ClothingId("base.panthers/890"));
            });
            internal static readonly IClothing GenericGloves1 = ClothingBuilder.Create( b =>
            {
                GenericGloves.MakeGenericGloves(b, 9, new ClothingId("base.panthers/891"));
            });
            internal static readonly IClothing GenericGloves2 = ClothingBuilder.Create( b =>
            {
                GenericGloves.MakeGenericGloves(b, 14, new ClothingId("base.panthers/892"));
            });
            internal static readonly IClothing GenericGloves3 = ClothingBuilder.Create( b =>
            {
                GenericGloves.MakeGenericGloves(b, 19, new ClothingId("base.panthers/893"));
            });
            internal static readonly IClothing GenericGloves4 = ClothingBuilder.Create( b =>
            {
                GenericGloves.MakeGenericGloves(b, 24, new ClothingId("base.panthers/894"));
            });
            internal static readonly IClothing GenericGloves5 = ClothingBuilder.Create( b =>
            {
                GenericGloves.MakeGenericGloves(b, 29, new ClothingId("base.panthers/895"));
            });
            internal static readonly IClothing GenericGloves6 = ClothingBuilder.Create( b =>
            {
                GenericGloves.MakeGenericGloves(b, 34, new ClothingId("base.panthers/896"));
            });


            internal static readonly IClothing GenericItem3 = ClothingBuilder.Create( b =>
            {
                GenericItem.MakeGenericItem(b, 0, 1, 2, 9, State.GameManager.SpriteDictionary.PantherLegs, new ClothingId("base.panthers/901"), ColorStyle.None);
            });
            internal static readonly IClothing GenericItem4 = ClothingBuilder.Create( b =>
            {
                GenericItem.MakeGenericItem(b, 3, 4, 5, 9, State.GameManager.SpriteDictionary.PantherLegs, new ClothingId("base.panthers/902"), ColorStyle.None);
            });
            internal static readonly IClothing GenericItem5 = ClothingBuilder.Create( b =>
            {
                GenericItem.MakeGenericItem(b, 6, 7, 8, 9, State.GameManager.SpriteDictionary.PantherLegs, new ClothingId("base.panthers/903"), ColorStyle.None);
            });
            internal static readonly IClothing GenericItem6 = ClothingBuilder.Create( b =>
            {
                GenericItem.MakeGenericItem(b, 9, 10, 11, 9, State.GameManager.SpriteDictionary.PantherLegs, new ClothingId("base.panthers/904"), ColorStyle.None);
            });
            internal static readonly IClothing GenericItem7 = ClothingBuilder.Create( b =>
            {
                GenericItem.MakeGenericItem(b, 12, 13, 14, 9, State.GameManager.SpriteDictionary.PantherLegs, new ClothingId("base.panthers/905"), ColorStyle.None);
            });
        }

        private static class Simple
        {
            internal static void MakeSimple(IClothingBuilder builder, int sprite, int discard, int layer, Sprite[] sheet,ClothingId clothingId, ColorStyle color, bool maleOnly = false, bool femaleOnly = false, bool blocksDick = false)
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.DiscardSprite = sheet[discard];
                    output.ClothingId = clothingId;
                    output.RevealsDick = !blocksDick;

                    if (maleOnly)
                    {
                        output.MaleOnly = true;
                    }

                    if (femaleOnly)
                    {
                        output.FemaleOnly = true;
                    }

                    output.FixedColor = CalcColor(color) != null;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(layer);
                    output["Clothing1"].Coloring(CalcColor(color));
                    output["Clothing1"].Coloring(CalcPalette(color, input.Actor));
                    output["Clothing1"].Sprite(sheet[sprite]);
                });
            }
        }

        private static class SimpleAttack
        {
            internal static void MakeSimpleAttack(IClothingBuilder builder, int spr, int attacksprite, int discard, int layer, Sprite[] sheet,ClothingId clothingId, ColorStyle color, bool maleOnly = false, bool femaleOnly = false)
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.DiscardSprite = sheet[discard];
                    output.ClothingId = clothingId;
                    if (maleOnly)
                    {
                        output.MaleOnly = true;
                    }

                    if (femaleOnly)
                    {
                        output.FemaleOnly = true;
                    }

                    output.FixedColor = CalcColor(color) != null;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(layer);
                    output["Clothing1"].Coloring(CalcColor(color));
                    output["Clothing1"].Coloring(CalcPalette(color, input.Actor));
                    output["Clothing1"].Sprite(input.A.IsAttacking ? sheet[attacksprite] : sheet[spr]);
                });
            }
        }

        private static class GenericFemaleTop
        {
            internal static void MakeGenericFemaleTop(IClothingBuilder<IOverSizeParameters> builder, int firstRowStart, int secondRowStart, int layer, Sprite[] sheet,ClothingId clothingId, ColorStyle color)
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.DiscardSprite = sheet[firstRowStart + 4];
                    output.ClothingId = clothingId;
                    output.FemaleOnly = true;
                    output.FixedColor = CalcColor(color) != null;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(layer);
                    output["Clothing1"].Coloring(CalcColor(color));

                    output["Clothing1"].Coloring(CalcPalette(color, input.Actor));
                    if (input.U.HasBreasts)
                    {
                        if (extra.Oversize)
                        {
                            output["Clothing1"].Sprite(sheet[secondRowStart + 3]);
                        }
                        else if (input.U.BreastSize < 3)
                        {
                            output["Clothing1"].Sprite(sheet[firstRowStart + input.U.BreastSize]);
                        }
                        else if (input.U.BreastSize < 6)
                        {
                            input.A.SquishedBreasts = true;
                            output["Clothing1"].Sprite(sheet[secondRowStart + input.U.BreastSize - 3]);
                        }
                        else // if (input.U.BreastSize < 8)
                        {
                            output["Clothing1"].Sprite(sheet[firstRowStart + input.U.BreastSize - 3]);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(sheet[firstRowStart]);
                    }
                });
            }
        }

        private static class GenericBottom
        {
            internal static void MakeGenericBottom(IClothingBuilder builder, int sprF, int sprM, int bulge, int discard, int layer, Sprite[] sheet,ClothingId clothingId, bool colored)
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.DiscardSprite = sheet[discard];
                    output.ClothingId = clothingId;
                    output.FixedColor = !colored;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(layer);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing2"].Layer(layer + 1);
                    output["Clothing2"].Coloring(Color.white);

                    if (colored)
                    {
                        output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.PantherClothes, input.U.ClothingColor));
                        output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.PantherClothes, input.U.ClothingColor));
                    }

                    output["Clothing1"].Sprite(input.U.HasDick ? sheet[sprM] : sheet[sprF]);

                    if (input.U.HasDick && bulge > 0)
                    {
                        if (input.U.DickSize > 2)
                        {
                            output["Clothing2"].Sprite(sheet[bulge + 1]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(sheet[bulge + 1]);
                        }
                    }
                    else
                    {
                        output["Clothing2"].Sprite(null);
                    }
                });
            }
        }

        private static class BoneTop
        {
            internal static void MakeBoneTop(IClothingBuilder<IOverSizeParameters> builder, int layer)
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.DiscardSprite = input.Sprites.PantherFOvertops[22];
                    output.FemaleOnly = true;
                    output.ClothingId = new ClothingId("base.panthers/870");
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(layer);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing2"].Layer(layer + 1);
                    if (input.U.HasBreasts)
                    {
                        if (extra.Oversize)
                        {
                            output["Clothing1"].Sprite(input.Sprites.PantherFOvertops[31]);
                        }
                        else if (input.U.BreastSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.PantherFOvertops[23 + input.U.BreastSize]);
                        }
                        else if (input.U.BreastSize < 6)
                        {
                            input.A.SquishedBreasts = true;
                            output["Clothing1"].Sprite(input.Sprites.PantherFOvertops[28 + input.U.BreastSize - 3]);
                        }
                        else //if (input.U.BreastSize < 8)
                        {
                            output["Clothing1"].Sprite(input.Sprites.PantherFOvertops[23 + input.U.BreastSize - 3]);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.PantherFOvertops[18]);
                    }

                    output["Clothing2"].Sprite(input.A.IsAttacking ? input.Sprites.PantherFOvertops[21] : input.Sprites.PantherFOvertops[20]);

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.PantherClothes, input.U.ClothingColor2));
                });
            }
        }

        private static class BeltTop
        {
            internal static void MakeBeltTop(IClothingBuilder<IOverSizeParameters> builder, int layer)
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.DiscardSprite = input.Sprites.PantherFTops[15 + 3];
                    output.ClothingId = new ClothingId("base.panthers/802");
                    output.FemaleOnly = true;
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(layer);
                    output["Clothing1"].Coloring(Color.white);

                    if (input.U.HasBreasts)
                    {
                        if (extra.Oversize)
                        {
                            output["Clothing1"].Sprite(null);
                        }
                        else if (input.U.BreastSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.PantherFTops[5 + input.U.BreastSize]);
                        }
                        else if (input.U.BreastSize < 6)
                        {
                            input.A.SquishedBreasts = true;
                            output["Clothing1"].Sprite(input.Sprites.PantherFTops[15 + input.U.BreastSize - 3]);
                        }
                        else //if (input.U.BreastSize < 8)
                        {
                            output["Clothing1"].Sprite(input.Sprites.PantherFTops[5 + input.U.BreastSize - 3]);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.PantherFTops[5]);
                    }
                });
            }
        }

        private static class GenericItem
        {
            internal static void MakeGenericItem(IClothingBuilder builder, int sprF, int sprM, int discard, int layer, Sprite[] sheet,ClothingId clothingId, ColorStyle color)
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.DiscardSprite = sheet[discard];
                    output.ClothingId = clothingId;
                    output.FixedColor = CalcColor(color) != null;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(layer);
                    output["Clothing1"].Coloring(CalcColor(color));

                    output["Clothing1"].Coloring(CalcPalette(color, input.Actor));
                    output["Clothing1"].Sprite(input.U.HasBreasts ? sheet[sprF] : sheet[sprM]);
                });
            }
        }

        private static class SantaHat
        {
            internal static readonly IClothing SantaHatInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.DiscardSprite = null;
                    output.ClothingId = new ClothingId("base.panthers/484841");
                    output.FixedColor = true;
                    output.ReqWinterHoliday = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(28);

                    output["Clothing1"].Coloring(Color.white);

                    output["Clothing1"].Sprite(input.Sprites.PantherHats[6]);
                });
            });
        }

        private static class OverbottomTwoTone
        {
            internal static void MakeOverbottomTwoTone(IClothingBuilder builder, int spr, int sprB, int secondSprite, int discard, int layer,ClothingId clothingId, bool blocksDick = false)
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.DiscardSprite = input.Sprites.PantherOverBottoms[discard];
                    output.ClothingId = clothingId;
                    output.RevealsDick = !blocksDick;

                    output.FixedColor = CalcColor(ColorStyle.OuterWear) != null;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(layer);
                    output["Clothing1"].Coloring(CalcColor(ColorStyle.OuterWear));
                    output["Clothing2"].Layer(layer);
                    output["Clothing2"].Coloring(CalcColor(ColorStyle.Other));

                    output["Clothing1"].Coloring(CalcPalette(ColorStyle.OuterWear, input.Actor));
                    output["Clothing2"].Coloring(CalcPalette(ColorStyle.Other, input.Actor));
                    output["Clothing2"].Sprite(input.Sprites.PantherOverBottoms[secondSprite]);

                    output["Clothing1"].Sprite(input.A.HasBelly ? input.Sprites.PantherOverBottoms[sprB] : input.Sprites.PantherOverBottoms[spr]);
                });
            }
        }

        private static class GenericGloves
        {
            internal static void MakeGenericGloves(IClothingBuilder builder, int start,ClothingId clothingId)
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.DiscardSprite = input.Sprites.PantherGloves[start + 4];
                    output.ClothingId = clothingId;
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(14);
                    output["Clothing1"].Coloring(Color.white);
                    if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.A.IsAttacking ? input.Sprites.PantherGloves[start + 1] : input.Sprites.PantherGloves[start]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.A.IsAttacking ? input.Sprites.PantherGloves[start + 3] : input.Sprites.PantherGloves[start + 2]);
                    }
                });
            }
        }

        private static class GenericGlovesPlusSecond
        {
            internal static void MakeGenericGlovesPlusSecond(IClothingBuilder builder, int start,ClothingId clothingId)
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.DiscardSprite = input.Sprites.PantherGloves[start + 4];
                    output.ClothingId = clothingId;
                    output.FixedColor = false;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(14);
                    output["Clothing1"].Layer(14);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.PantherClothes, input.U.ClothingColor2)); // output.Clothing1 was set to ClothingColor2 in original code
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.PantherClothes, input.U.ClothingColor3)); // output.Clothing2 was set to ClothingColor3 in original code - these arent typos, at least not by LewdMagic


                    if (input.U.HasBreasts)
                    {
                        if (input.A.IsAttacking)
                        {
                            output["Clothing1"].Sprite(input.Sprites.PantherGloves[start + 1]);
                            output["Clothing2"].Sprite(input.Sprites.PantherGloves[start + 5]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.PantherGloves[start]);
                            output["Clothing2"].Sprite(input.Sprites.PantherGloves[start + 4]);
                        }
                    }
                    else
                    {
                        if (input.A.IsAttacking)
                        {
                            output["Clothing1"].Sprite(input.Sprites.PantherGloves[start + 3]);
                            output["Clothing2"].Sprite(input.Sprites.PantherGloves[start + 7]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.PantherGloves[start + 2]);
                            output["Clothing2"].Sprite(input.Sprites.PantherGloves[start + 6]);
                        }
                    }
                });
            }
        }

        private static class GenericOnepiece
        {
            internal static void MakeGenericOnepiece(IClothingBuilder<IOverSizeParameters> builder, int firstRowStart, int secondRowStart, int finalStart, bool noPlusBreast)
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.FemaleOnly = true;
                    output.DiscardSprite = input.Sprites.PantherOnePiece[finalStart];
                    output.OccupiesAllSlots = true;
                    output.FixedColor = false;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(8);
                    output["Clothing1"].Layer(20);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.PantherClothes, input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.PantherClothes, input.U.ClothingColor));

                    if (input.U.HasBreasts)
                    {
                        if (extra.Oversize)
                        {
                            if (!noPlusBreast)
                            {
                                output["Clothing1"].Sprite(input.Sprites.PantherOnePiece[secondRowStart + 3]);
                            }
                        }
                        else if (input.U.BreastSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.PantherOnePiece[firstRowStart + input.U.BreastSize]);
                        }
                        else if (input.U.BreastSize < 6)
                        {
                            input.A.SquishedBreasts = true;
                            output["Clothing1"].Sprite(input.Sprites.PantherOnePiece[secondRowStart + input.U.BreastSize - 3]);
                        }
                        else if (input.U.BreastSize < 8)
                        {
                            output["Clothing1"].Sprite(input.Sprites.PantherOnePiece[firstRowStart + input.U.BreastSize - 3]);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.PantherOnePiece[firstRowStart]);
                    }

                    if (input.A.HasBelly)
                    {
                        output["Clothing2"].Sprite(input.Sprites.PantherOnePiece[finalStart + 1 + input.A.GetStomachSize(32)]);
                        output["Clothing2"].Layer(17);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.PantherOnePiece[finalStart + 1]);
                        output["Clothing2"].Layer(8);
                    }
                });
            }
        }
    }
}
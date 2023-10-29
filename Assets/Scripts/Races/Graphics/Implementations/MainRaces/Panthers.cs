#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

internal static class Panthers
{
    internal static List<IClothingDataSimple> AllClothing;

    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank<OverSizeParameters>, builder =>
    {
        builder.Setup(output =>
        {
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

            output.HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.PantherHair);
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.PantherSkin);
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.PantherBodyPaint);

            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.PantherClothes);

            List<IClothing<IOverSizeParameters>> AllowedMainClothingTypes = new List<IClothing<IOverSizeParameters>>
            {
                All.GenericFemaleTop1,
                All.BeltTop1,
                All.GenericFemaleTop2,
                All.GenericFemaleTop3,
                All.GenericFemaleTop4,
                All.GenericFemaleTop5,
                All.GenericFemaleTop6,
                All.Simple1,
                All.Simple2,
                All.Simple3,
                All.Simple4,
                All.Simple5,
                All.GenericOnepiece1,
                All.GenericOnepiece2,
                All.GenericOnepiece3,
                All.GenericOnepiece4
            };

            output.AllowedMainClothingTypes.Set(AllowedMainClothingTypes);

            List<IClothing<IOverSizeParameters>> AllowedWaistTypes = new List<IClothing<IOverSizeParameters>>() //Bottoms
            {
                All.GenericBottom1,
                All.GenericBottom2,
                All.GenericBottom3,
                All.GenericBottom4,
                All.GenericBottom5,
                All.GenericBottom6
            };
            output.AllowedWaistTypes.Set(AllowedWaistTypes);

            List<IClothing<IOverSizeParameters>> ExtraMainClothing1Types = new List<IClothing<IOverSizeParameters>>() //Overtops
            {
                All.GenericFemaleTop7,
                All.SimpleAttack1,
                All.GenericFemaleTop8,
                All.BoneTop1,
                All.Simple6,
                All.Simple7,
                All.SimpleAttack2,
                All.SimpleAttack3
            };
            output.ExtraMainClothing1Types.Set(ExtraMainClothing1Types); //Overtops

            List<IClothing<IOverSizeParameters>> ExtraMainClothing2Types = new List<IClothing<IOverSizeParameters>>() //Overbottoms
            {
                All.Simple8,
                All.OverbottomTwoTone1,
                All.Simple9,
                All.OverbottomTwoTone2,
                All.Simple10,
                All.Simple11,
                All.Simple12
            };
            output.ExtraMainClothing2Types.Set(ExtraMainClothing2Types); //Overbottoms

            List<IClothing<IOverSizeParameters>> ExtraMainClothing3Types = new List<IClothing<IOverSizeParameters>>() //Hats
            {
                All.GenericItem1,
                All.GenericItem2,
                SantaHat.SantaHatInstance
            };
            output.ExtraMainClothing3Types.Set(ExtraMainClothing3Types); //Hats

            List<IClothing<IOverSizeParameters>> ExtraMainClothing4Types = new List<IClothing<IOverSizeParameters>>() //Gloves
            {
                All.GenericGlovesPlusSecond1,
                All.GenericGloves1,
                All.GenericGloves2,
                All.GenericGloves3,
                All.GenericGloves4,
                All.GenericGloves5,
                All.GenericGloves6
            };
            output.ExtraMainClothing4Types.Set(ExtraMainClothing4Types); //Gloves

            List<IClothing<IOverSizeParameters>> ExtraMainClothing5Types = new List<IClothing<IOverSizeParameters>>() //Legs
            {
                All.GenericItem3,
                All.GenericItem4,
                All.GenericItem5,
                All.GenericItem6,
                All.GenericItem7
            };
            output.ExtraMainClothing5Types.Set(ExtraMainClothing5Types); //Legs

            AllClothing = new List<IClothingDataSimple>();
            AllClothing.AddRange(AllowedMainClothingTypes);
            AllClothing.AddRange(AllowedWaistTypes);
            AllClothing.AddRange(ExtraMainClothing1Types);
            AllClothing.AddRange(ExtraMainClothing2Types);
            AllClothing.AddRange(ExtraMainClothing3Types);
            AllClothing.AddRange(ExtraMainClothing4Types);
            AllClothing.AddRange(ExtraMainClothing5Types);
        });


        builder.RunBefore((input, output) =>
        {
            CommonRaceCode.MakeBreastOversize(32 * 32).Invoke(input, output);
            Defaults.BasicBellyRunAfter.Invoke(input, output);
        });


        builder.RenderSingle(SpriteType.Head, 25, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherSkin, input.Actor.Unit.SkinColor));
            int attackingOffset = input.Actor.IsAttacking || input.Actor.IsEating ? 2 : 0;
            int GenderOffset = input.Actor.Unit.HasBreasts ? 0 : 1;
            output.Sprite(input.Sprites.PantherBase[4 + GenderOffset + attackingOffset + 4 * input.Actor.Unit.EyeType]);
        });

        builder.RenderSingle(SpriteType.Hair, 27, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherHair, input.Actor.Unit.HairColor));
            switch (input.Actor.Unit.HairStyle)
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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherHair, input.Actor.Unit.HairColor));
            switch (input.Actor.Unit.HairStyle)
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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, input.Actor.Unit.ClothingColor3));
            switch (input.Actor.Unit.HairStyle)
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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherSkin, input.Actor.Unit.SkinColor));
            int attackingOffset = input.Actor.IsAttacking ? 2 : 0;
            int GenderOffset = input.Actor.Unit.HasBreasts ? 0 : 1;
            output.Sprite(input.Sprites.PantherBase[GenderOffset + attackingOffset]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherBodyPaint, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType1 == 0)
            {
                return;
            }

            if (input.Actor.Unit.BodyAccentType1 >= input.RaceData.BodyAccentTypes1)
            {
                input.Actor.Unit.BodyAccentType1 = input.RaceData.BodyAccentTypes1 - 1;
            }

            int genderOffset = input.Actor.Unit.HasBreasts ? 0 : 1;
            int attackingOffset = input.Actor.IsAttacking ? 2 : 0;
            output.Sprite(input.Sprites.PantherBase[74 + genderOffset + attackingOffset + 8 * (input.Actor.Unit.BodyAccentType1 - 1)]);
        });

        builder.RenderSingle(SpriteType.BodyAccent2, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherBodyPaint, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType2 == 0)
            {
                return;
            }

            if (input.Actor.Unit.BodyAccentType2 >= input.RaceData.BodyAccentTypes2)
            {
                input.Actor.Unit.BodyAccentType2 = input.RaceData.BodyAccentTypes2 - 1;
            }

            int genderOffset = input.Actor.Unit.HasBreasts ? 0 : 1;
            int attackingOffset = input.Actor.IsAttacking ? 2 : 0;
            output.Sprite(input.Sprites.PantherBase[78 + genderOffset + attackingOffset + 8 * (input.Actor.Unit.BodyAccentType2 - 1)]);
        });

        builder.RenderSingle(SpriteType.BodyAccent3, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherBodyPaint, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType3 == 0)
            {
                return;
            }

            if (input.Actor.Unit.BodyAccentType3 >= input.RaceData.BodyAccentTypes3)
            {
                input.Actor.Unit.BodyAccentType3 = input.RaceData.BodyAccentTypes3 - 1;
            }

            int genderOffset = input.Actor.Unit.HasBreasts ? 0 : 1;
            output.Sprite(input.Sprites.PantherBase[98 + genderOffset + 2 * (input.Actor.Unit.BodyAccentType3 - 1)]);
        });

        builder.RenderSingle(SpriteType.BodyAccent4, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherBodyPaint, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType4 == 0)
            {
                return;
            }

            if (input.Actor.Unit.BodyAccentType4 >= input.RaceData.BodyAccentTypes4)
            {
                input.Actor.Unit.BodyAccentType4 = input.RaceData.BodyAccentTypes4 - 1;
            }

            int genderOffset = input.Actor.Unit.HasBreasts ? 0 : 1;
            output.Sprite(input.Sprites.PantherBase[102 + genderOffset + 2 * (input.Actor.Unit.BodyAccentType4 - 1)]);
        });

        builder.RenderSingle(SpriteType.BodyAccent5, 26, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherBodyPaint, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType5 == 0)
            {
                return;
            }

            if (input.Actor.Unit.BodyAccentType5 >= input.RaceData.BodyAccentTypes5)
            {
                input.Actor.Unit.BodyAccentType5 = input.RaceData.BodyAccentTypes5 - 1;
            }

            int genderOffset = input.Actor.Unit.HasBreasts ? 0 : 1;
            int attackingOffset = input.Actor.IsAttacking ? 2 : 0;
            output.Sprite(input.Sprites.PantherBase[110 + genderOffset + attackingOffset + 4 * (input.Actor.Unit.BodyAccentType5 - 1)]);
        });

        builder.RenderSingle(SpriteType.BodyAccent6, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.PantherBase[20 + (input.Actor.Unit.HasBreasts ? 0 : 1)]);
        });

        builder.RenderSingle(SpriteType.BodySize, 23, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                int weaponSprite = input.Actor.GetWeaponSprite();
                int genderMod = input.Actor.Unit.HasBreasts ? 0 : 1;
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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[35]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[34]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[33]);
                    return;
                }

                if (leftSize > 28)
                {
                    leftSize = 28;
                }


                output.Sprite(input.Sprites.PantherVoreParts[4 + leftSize]);
            }
            else
            {
                if (input.Actor.Unit.DefaultBreastSize == 0)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[0]);
                    return;
                }

                if (input.Actor.SquishedBreasts && input.Actor.Unit.BreastSize < 6 && input.Actor.Unit.BreastSize >= 4)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[-2 + input.Actor.Unit.BreastSize]);
                    return;
                }

                output.Sprite(input.Sprites.PantherVoreParts[3 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.SecondaryBreasts, 19, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[70]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[69]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[68]);
                    return;
                }

                if (rightSize > 28)
                {
                    rightSize = 28;
                }

                output.Sprite(input.Sprites.PantherVoreParts[39 + rightSize]);
            }
            else
            {
                if (input.Actor.Unit.DefaultBreastSize == 0)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[140]);
                    return;
                }

                if (input.Actor.SquishedBreasts && input.Actor.Unit.BreastSize < 6 && input.Actor.Unit.BreastSize >= 4)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[33 + input.Actor.Unit.BreastSize]);
                    return;
                }

                output.Sprite(input.Sprites.PantherVoreParts[38 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.Belly, 16, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(32, 1.2f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[145]).AddOffset(0, -22 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 32)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[144]).AddOffset(0, -22 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[143]).AddOffset(0, -22 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[142]).AddOffset(0, -22 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[141]).AddOffset(0, -22 * .625f);
                    return;
                }

                size = input.Actor.GetStomachSize(29, 0.8f);
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

                if (input.Actor.PredatorComponent.OnlyOnePreyAndLiving() && size >= 8 && size <= 13)
                {
                    output.Sprite(input.Sprites.PantherVoreParts[71]);
                    return;
                }

                output.Sprite(input.Sprites.PantherVoreParts[74 + size]);
            }
        });

        builder.RenderSingle(SpriteType.Dick, 14, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect())
            {
                if (input.Actor.PredatorComponent?.VisibleFullness < .75f)
                {
                    output.Sprite(input.Sprites.PantherBase[16 + input.Actor.Unit.DickSize]).Layer(18);
                    return;
                }

                output.Sprite(input.Sprites.PantherBase[16 + input.Actor.Unit.DickSize]).Layer(14);
                return;
            }

            output.Sprite(input.Sprites.PantherBase[12 + input.Actor.Unit.DickSize]).Layer(14);
        });

        builder.RenderSingle(SpriteType.Balls, 13, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.Unit.Predator == false)
            {
                output.Sprite(input.Sprites.PantherVoreParts[107 + input.Actor.Unit.BallsSize]);
                return;
            }

            int size = input.Actor.GetBallSize(31, .8f);
            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) && size == 31)
            {
                output.Sprite(input.Sprites.PantherVoreParts[139]).AddOffset(0, -19 * .625f);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) &&
                size == 31)
            {
                output.Sprite(input.Sprites.PantherVoreParts[138]).AddOffset(0, -16 * .625f);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) &&
                size == 30)
            {
                output.Sprite(input.Sprites.PantherVoreParts[137]).AddOffset(0, -15 * .625f);
                return;
            }

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
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                int weaponSprite = input.Actor.GetWeaponSprite();
                int genderMod = input.Actor.Unit.HasBreasts ? 0 : 1;
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

            if (State.Rand.Next(10) == 0)
            {
                unit.EyeType = 1;
            }
            else
            {
                unit.EyeType = 0;
            }

            unit.ClothingColor = State.Rand.Next(data.MiscRaceData.ClothingColors);
            unit.ClothingColor2 = State.Rand.Next(data.MiscRaceData.ClothingColors);
            unit.ClothingColor3 = State.Rand.Next(data.MiscRaceData.ClothingColors);
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
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, actor.Unit.ClothingColor);
            case ColorStyle.OuterWear:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, actor.Unit.ClothingColor2);
            case ColorStyle.Other:
                return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, actor.Unit.ClothingColor3);
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
        internal static IClothing<IOverSizeParameters> GenericFemaleTop1 = ClothingBuilder.Create<IOverSizeParameters>( b =>
        {
            GenericFemaleTop.MakeGenericFemaleTop(b, 0, 10, 20, State.GameManager.SpriteDictionary.PantherFTops, 801, ColorStyle.InnerWear);
        });
        internal static IClothing<IOverSizeParameters> BeltTop1 = ClothingBuilder.Create<IOverSizeParameters>( b =>
        {
            BeltTop.MakeBeltTop(b, 20);
        });
        internal static IClothing<IOverSizeParameters> GenericFemaleTop2 = ClothingBuilder.Create<IOverSizeParameters>( b =>
        {
            GenericFemaleTop.MakeGenericFemaleTop(b, 19, 29, 20, State.GameManager.SpriteDictionary.PantherFTops, 803, ColorStyle.InnerWear);
        });
        internal static IClothing<IOverSizeParameters> GenericFemaleTop3 = ClothingBuilder.Create<IOverSizeParameters>( b =>
        {
            GenericFemaleTop.MakeGenericFemaleTop(b, 24, 34, 20, State.GameManager.SpriteDictionary.PantherFTops, 804, ColorStyle.None);
        });
        internal static IClothing<IOverSizeParameters> GenericFemaleTop4 = ClothingBuilder.Create<IOverSizeParameters>( b =>
        {
            GenericFemaleTop.MakeGenericFemaleTop(b, 39, 49, 20, State.GameManager.SpriteDictionary.PantherFTops, 805, ColorStyle.InnerWear);
        });
        internal static IClothing<IOverSizeParameters> GenericFemaleTop5 = ClothingBuilder.Create<IOverSizeParameters>( b =>
        {
            GenericFemaleTop.MakeGenericFemaleTop(b, 44, 54, 20, State.GameManager.SpriteDictionary.PantherFTops, 806, ColorStyle.None);
        });
        internal static IClothing<IOverSizeParameters> GenericFemaleTop6 = ClothingBuilder.Create<IOverSizeParameters>( b =>
        {
            GenericFemaleTop.MakeGenericFemaleTop(b, 59, 64, 20, State.GameManager.SpriteDictionary.PantherFTops, 807, ColorStyle.InnerWear);
        });
        internal static IClothing Simple1 = ClothingBuilder.Create( b =>
        {
            Simple.MakeSimple(b, 0, 5, 20, State.GameManager.SpriteDictionary.PantherMTops, 808, ColorStyle.None, true);
        });
        internal static IClothing Simple2 = ClothingBuilder.Create( b =>
        {
            Simple.MakeSimple(b, 1, 6, 20, State.GameManager.SpriteDictionary.PantherMTops, 809, ColorStyle.InnerWear, true);
        });
        internal static IClothing Simple3 = ClothingBuilder.Create( b =>
        {
            Simple.MakeSimple(b, 2, 7, 20, State.GameManager.SpriteDictionary.PantherMTops, 810, ColorStyle.InnerWear, true);
        });
        internal static IClothing Simple4 = ClothingBuilder.Create( b =>
        {
            Simple.MakeSimple(b, 3, 8, 20, State.GameManager.SpriteDictionary.PantherMTops, 811, ColorStyle.None, true);
        });
        internal static IClothing Simple5 = ClothingBuilder.Create( b =>
        {
            Simple.MakeSimple(b, 4, 9, 20, State.GameManager.SpriteDictionary.PantherMTops, 812, ColorStyle.None, true);
        });
        internal static IClothing<IOverSizeParameters> GenericOnepiece1 = ClothingBuilder.Create<IOverSizeParameters>( b =>
        {
            GenericOnepiece.MakeGenericOnepiece(b, 0, 5, 18, false);
        });
        internal static IClothing<IOverSizeParameters> GenericOnepiece2 = ClothingBuilder.Create<IOverSizeParameters>( b =>
        {
            GenericOnepiece.MakeGenericOnepiece(b, 9, 14, 18, false);
        });
        internal static IClothing<IOverSizeParameters> GenericOnepiece3 = ClothingBuilder.Create<IOverSizeParameters>( b =>
        {
            GenericOnepiece.MakeGenericOnepiece(b, 52, 57, 69, true);
        });
        internal static IClothing<IOverSizeParameters> GenericOnepiece4 = ClothingBuilder.Create<IOverSizeParameters>( b =>
        {
            GenericOnepiece.MakeGenericOnepiece(b, 60, 65, 69, false);
        });


        internal static IClothing GenericBottom1 = ClothingBuilder.Create( b =>
        {
            GenericBottom.MakeGenericBottom(b, 0, 0, 12, 6, 8, State.GameManager.SpriteDictionary.PantherBottoms, 840, true);
        });
        internal static IClothing GenericBottom2 = ClothingBuilder.Create( b =>
        {
            GenericBottom.MakeGenericBottom(b, 1, 1, 12, 7, 8, State.GameManager.SpriteDictionary.PantherBottoms, 841, true);
        });
        internal static IClothing GenericBottom3 = ClothingBuilder.Create( b =>
        {
            GenericBottom.MakeGenericBottom(b, 2, 2, -2, 8, 8, State.GameManager.SpriteDictionary.PantherBottoms, 842, true);
        });
        internal static IClothing GenericBottom4 = ClothingBuilder.Create( b =>
        {
            GenericBottom.MakeGenericBottom(b, 3, 17, 14, 9, 8, State.GameManager.SpriteDictionary.PantherBottoms, 843, false);
        });
        internal static IClothing GenericBottom5 = ClothingBuilder.Create( b =>
        {
            GenericBottom.MakeGenericBottom(b, 16, 4, 12, 10, 8, State.GameManager.SpriteDictionary.PantherBottoms, 844, true);
        });
        internal static IClothing GenericBottom6 = ClothingBuilder.Create( b =>
        {
            GenericBottom.MakeGenericBottom(b, 5, 5, 12, 11, 8, State.GameManager.SpriteDictionary.PantherBottoms, 845, true);
        });


        internal static IClothing<IOverSizeParameters> GenericFemaleTop7 = ClothingBuilder.Create<IOverSizeParameters>( b =>
        {
            GenericFemaleTop.MakeGenericFemaleTop(b, 0, 5, 21, State.GameManager.SpriteDictionary.PantherFOvertops, 830, ColorStyle.OuterWear);
        });
        internal static IClothing SimpleAttack1 = ClothingBuilder.Create( b =>
        {
            SimpleAttack.MakeSimpleAttack(b, 20, 21, 22, 21, State.GameManager.SpriteDictionary.PantherFOvertops, 831, ColorStyle.OuterWear, femaleOnly: true);
        });
        internal static IClothing<IOverSizeParameters> GenericFemaleTop8 = ClothingBuilder.Create<IOverSizeParameters>( b =>
        {
            GenericFemaleTop.MakeGenericFemaleTop(b, 10, 15, 21, State.GameManager.SpriteDictionary.PantherFOvertops, 832, ColorStyle.OuterWear);
        });
        internal static IClothing<IOverSizeParameters> BoneTop1 = ClothingBuilder.Create<IOverSizeParameters>( b =>
        {
            BoneTop.MakeBoneTop(b, 21);
        });
        internal static IClothing Simple6 = ClothingBuilder.Create( b =>
        {
            Simple.MakeSimple(b, 0, 6, 21, State.GameManager.SpriteDictionary.PantherMOvertops, 834, ColorStyle.None, true);
        });
        internal static IClothing Simple7 = ClothingBuilder.Create( b =>
        {
            Simple.MakeSimple(b, 1, 7, 21, State.GameManager.SpriteDictionary.PantherMOvertops, 835, ColorStyle.OuterWear, true);
        });
        internal static IClothing SimpleAttack2 = ClothingBuilder.Create( b =>
        {
            SimpleAttack.MakeSimpleAttack(b, 2, 4, 8, 21, State.GameManager.SpriteDictionary.PantherMOvertops, 836, ColorStyle.OuterWear, true);
        });
        internal static IClothing SimpleAttack3 = ClothingBuilder.Create( b =>
        {
            SimpleAttack.MakeSimpleAttack(b, 3, 5, 9, 21, State.GameManager.SpriteDictionary.PantherMOvertops, 837, ColorStyle.OuterWear, true);
        });


        internal static IClothing Simple8 = ClothingBuilder.Create( b =>
        {
            Simple.MakeSimple(b, 0, 10, 11, State.GameManager.SpriteDictionary.PantherOverBottoms, 860, ColorStyle.OuterWear, blocksDick: true);
        });
        internal static IClothing OverbottomTwoTone1 = ClothingBuilder.Create( b =>
        {
            OverbottomTwoTone.MakeOverbottomTwoTone(b, 1, 2, 3, 11, 11, 861);
        });
        internal static IClothing Simple9 = ClothingBuilder.Create( b =>
        {
            Simple.MakeSimple(b, 4, 12, 11, State.GameManager.SpriteDictionary.PantherOverBottoms, 862, ColorStyle.OuterWear, blocksDick: true);
        });
        internal static IClothing OverbottomTwoTone2 = ClothingBuilder.Create( b =>
        {
            OverbottomTwoTone.MakeOverbottomTwoTone(b, 5, 5, 6, 13, 11, 863, true);
        });
        internal static IClothing Simple10 = ClothingBuilder.Create( b =>
        {
            Simple.MakeSimple(b, 7, 14, 15, State.GameManager.SpriteDictionary.PantherOverBottoms, 864, ColorStyle.None, blocksDick: false);
        });
        internal static IClothing Simple11 = ClothingBuilder.Create( b =>
        {
            Simple.MakeSimple(b, 8, 15, 11, State.GameManager.SpriteDictionary.PantherOverBottoms, 865, ColorStyle.None, femaleOnly: true, blocksDick: true);
        });
        internal static IClothing Simple12 = ClothingBuilder.Create( b =>
        {
            Simple.MakeSimple(b, 9, 16, 11, State.GameManager.SpriteDictionary.PantherOverBottoms, 866, ColorStyle.None, true, blocksDick: true);
        });


        internal static IClothing GenericItem1 = ClothingBuilder.Create( b =>
        {
            GenericItem.MakeGenericItem(b, 0, 2, 4, 28, State.GameManager.SpriteDictionary.PantherHats, 888, ColorStyle.None);
        });
        internal static IClothing GenericItem2 = ClothingBuilder.Create( b =>
        {
            GenericItem.MakeGenericItem(b, 1, 3, 5, 28, State.GameManager.SpriteDictionary.PantherHats, 889, ColorStyle.Other);
        });


        internal static IClothing GenericGlovesPlusSecond1 = ClothingBuilder.Create( b =>
        {
            GenericGlovesPlusSecond.MakeGenericGlovesPlusSecond(b, 0, 890);
        });
        internal static IClothing GenericGloves1 = ClothingBuilder.Create( b =>
        {
            GenericGloves.MakeGenericGloves(b, 9, 891);
        });
        internal static IClothing GenericGloves2 = ClothingBuilder.Create( b =>
        {
            GenericGloves.MakeGenericGloves(b, 14, 892);
        });
        internal static IClothing GenericGloves3 = ClothingBuilder.Create( b =>
        {
            GenericGloves.MakeGenericGloves(b, 19, 893);
        });
        internal static IClothing GenericGloves4 = ClothingBuilder.Create( b =>
        {
            GenericGloves.MakeGenericGloves(b, 24, 894);
        });
        internal static IClothing GenericGloves5 = ClothingBuilder.Create( b =>
        {
            GenericGloves.MakeGenericGloves(b, 29, 895);
        });
        internal static IClothing GenericGloves6 = ClothingBuilder.Create( b =>
        {
            GenericGloves.MakeGenericGloves(b, 34, 896);
        });


        internal static IClothing GenericItem3 = ClothingBuilder.Create( b =>
        {
            GenericItem.MakeGenericItem(b, 0, 1, 2, 9, State.GameManager.SpriteDictionary.PantherLegs, 901, ColorStyle.None);
        });
        internal static IClothing GenericItem4 = ClothingBuilder.Create( b =>
        {
            GenericItem.MakeGenericItem(b, 3, 4, 5, 9, State.GameManager.SpriteDictionary.PantherLegs, 902, ColorStyle.None);
        });
        internal static IClothing GenericItem5 = ClothingBuilder.Create( b =>
        {
            GenericItem.MakeGenericItem(b, 6, 7, 8, 9, State.GameManager.SpriteDictionary.PantherLegs, 903, ColorStyle.None);
        });
        internal static IClothing GenericItem6 = ClothingBuilder.Create( b =>
        {
            GenericItem.MakeGenericItem(b, 9, 10, 11, 9, State.GameManager.SpriteDictionary.PantherLegs, 904, ColorStyle.None);
        });
        internal static IClothing GenericItem7 = ClothingBuilder.Create( b =>
        {
            GenericItem.MakeGenericItem(b, 12, 13, 14, 9, State.GameManager.SpriteDictionary.PantherLegs, 905, ColorStyle.None);
        });
    }

    private static class Simple
    {
        internal static void MakeSimple(IClothingBuilder builder, int sprite, int discard, int layer, Sprite[] sheet, int type, ColorStyle color, bool maleOnly = false, bool femaleOnly = false, bool blocksDick = false)
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.DiscardSprite = sheet[discard];
                output.Type = type;
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
        internal static void MakeSimpleAttack(IClothingBuilder builder, int spr, int attacksprite, int discard, int layer, Sprite[] sheet, int type, ColorStyle color, bool maleOnly = false, bool femaleOnly = false)
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardSprite = sheet[discard];
                output.Type = type;
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
                if (input.Actor.IsAttacking)
                {
                    output["Clothing1"].Sprite(sheet[attacksprite]);
                }
                else
                {
                    output["Clothing1"].Sprite(sheet[spr]);
                }
            });
        }
    }

    private static class GenericFemaleTop
    {
        internal static void MakeGenericFemaleTop(IClothingBuilder<IOverSizeParameters> builder, int firstRowStart, int secondRowStart, int layer, Sprite[] sheet, int type, ColorStyle color)
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardSprite = sheet[firstRowStart + 4];
                output.Type = type;
                output.FemaleOnly = true;
                output.FixedColor = CalcColor(color) != null;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(layer);
                output["Clothing1"].Coloring(CalcColor(color));

                output["Clothing1"].Coloring(CalcPalette(color, input.Actor));
                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Params.Oversize)
                    {
                        output["Clothing1"].Sprite(sheet[secondRowStart + 3]);
                    }
                    else if (input.Actor.Unit.BreastSize < 3)
                    {
                        output["Clothing1"].Sprite(sheet[firstRowStart + input.Actor.Unit.BreastSize]);
                    }
                    else if (input.Actor.Unit.BreastSize < 6)
                    {
                        input.Actor.SquishedBreasts = true;
                        output["Clothing1"].Sprite(sheet[secondRowStart + input.Actor.Unit.BreastSize - 3]);
                    }
                    else // if (input.Actor.Unit.BreastSize < 8)
                    {
                        output["Clothing1"].Sprite(sheet[firstRowStart + input.Actor.Unit.BreastSize - 3]);
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
        internal static void MakeGenericBottom(IClothingBuilder builder, int sprF, int sprM, int bulge, int discard, int layer, Sprite[] sheet, int type, bool colored)
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.DiscardSprite = sheet[discard];
                output.Type = type;
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
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, input.Actor.Unit.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, input.Actor.Unit.ClothingColor));
                }

                if (input.Actor.Unit.HasDick)
                {
                    output["Clothing1"].Sprite(sheet[sprM]);
                }
                else
                {
                    output["Clothing1"].Sprite(sheet[sprF]);
                }

                if (input.Actor.Unit.HasDick && bulge > 0)
                {
                    if (input.Actor.Unit.DickSize > 2)
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
                output.Type = 870;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(layer);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing2"].Layer(layer + 1);
                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Params.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.PantherFOvertops[31]);
                    }
                    else if (input.Actor.Unit.BreastSize < 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.PantherFOvertops[23 + input.Actor.Unit.BreastSize]);
                    }
                    else if (input.Actor.Unit.BreastSize < 6)
                    {
                        input.Actor.SquishedBreasts = true;
                        output["Clothing1"].Sprite(input.Sprites.PantherFOvertops[28 + input.Actor.Unit.BreastSize - 3]);
                    }
                    else //if (input.Actor.Unit.BreastSize < 8)
                    {
                        output["Clothing1"].Sprite(input.Sprites.PantherFOvertops[23 + input.Actor.Unit.BreastSize - 3]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.PantherFOvertops[18]);
                }

                if (input.Actor.IsAttacking)
                {
                    output["Clothing2"].Sprite(input.Sprites.PantherFOvertops[21]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.PantherFOvertops[20]);
                }

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, input.Actor.Unit.ClothingColor2));
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
                output.Type = 802;
                output.FemaleOnly = true;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(layer);
                output["Clothing1"].Coloring(Color.white);

                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Params.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                    }
                    else if (input.Actor.Unit.BreastSize < 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.PantherFTops[5 + input.Actor.Unit.BreastSize]);
                    }
                    else if (input.Actor.Unit.BreastSize < 6)
                    {
                        input.Actor.SquishedBreasts = true;
                        output["Clothing1"].Sprite(input.Sprites.PantherFTops[15 + input.Actor.Unit.BreastSize - 3]);
                    }
                    else //if (input.Actor.Unit.BreastSize < 8)
                    {
                        output["Clothing1"].Sprite(input.Sprites.PantherFTops[5 + input.Actor.Unit.BreastSize - 3]);
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
        internal static void MakeGenericItem(IClothingBuilder builder, int sprF, int sprM, int discard, int layer, Sprite[] sheet, int type, ColorStyle color)
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardSprite = sheet[discard];
                output.Type = type;
                output.FixedColor = CalcColor(color) != null;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(layer);
                output["Clothing1"].Coloring(CalcColor(color));

                output["Clothing1"].Coloring(CalcPalette(color, input.Actor));
                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(sheet[sprF]);
                }
                else
                {
                    output["Clothing1"].Sprite(sheet[sprM]);
                }
            });
        }
    }

    private static class SantaHat
    {
        internal static IClothing SantaHatInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardSprite = null;
                output.Type = 484841;
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
        internal static void MakeOverbottomTwoTone(IClothingBuilder builder, int spr, int sprB, int secondSprite, int discard, int layer, int type, bool blocksDick = false)
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.DiscardSprite = input.Sprites.PantherOverBottoms[discard];
                output.Type = type;
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

                if (input.Actor.HasBelly)
                {
                    output["Clothing1"].Sprite(input.Sprites.PantherOverBottoms[sprB]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.PantherOverBottoms[spr]);
                }
            });
        }
    }

    private static class GenericGloves
    {
        internal static void MakeGenericGloves(IClothingBuilder builder, int start, int type)
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardSprite = input.Sprites.PantherGloves[start + 4];
                output.Type = type;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(14);
                output["Clothing1"].Coloring(Color.white);
                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.IsAttacking)
                    {
                        output["Clothing1"].Sprite(input.Sprites.PantherGloves[start + 1]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.PantherGloves[start]);
                    }
                }
                else
                {
                    if (input.Actor.IsAttacking)
                    {
                        output["Clothing1"].Sprite(input.Sprites.PantherGloves[start + 3]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.PantherGloves[start + 2]);
                    }
                }
            });
        }
    }

    private static class GenericGlovesPlusSecond
    {
        internal static void MakeGenericGlovesPlusSecond(IClothingBuilder builder, int start, int type)
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardSprite = input.Sprites.PantherGloves[start + 4];
                output.Type = type;
                output.FixedColor = false;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(14);
                output["Clothing1"].Layer(14);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, input.Actor.Unit.ClothingColor2)); // output.Clothing1 was set to ClothingColor2 in original code
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, input.Actor.Unit.ClothingColor3)); // output.Clothing2 was set to ClothingColor3 in original code - these arent typos, at least not by LewdMagic


                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.IsAttacking)
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
                    if (input.Actor.IsAttacking)
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

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(8);
                output["Clothing1"].Layer(20);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PantherClothes, input.Actor.Unit.ClothingColor));

                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Params.Oversize)
                    {
                        if (noPlusBreast)
                        {
                            output["Clothing1"].Sprite(null);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.PantherOnePiece[secondRowStart + 3]);
                        }
                    }
                    else if (input.Actor.Unit.BreastSize < 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.PantherOnePiece[firstRowStart + input.Actor.Unit.BreastSize]);
                    }
                    else if (input.Actor.Unit.BreastSize < 6)
                    {
                        input.Actor.SquishedBreasts = true;
                        output["Clothing1"].Sprite(input.Sprites.PantherOnePiece[secondRowStart + input.Actor.Unit.BreastSize - 3]);
                    }
                    else if (input.Actor.Unit.BreastSize < 8)
                    {
                        output["Clothing1"].Sprite(input.Sprites.PantherOnePiece[firstRowStart + input.Actor.Unit.BreastSize - 3]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.PantherOnePiece[firstRowStart]);
                }

                if (input.Actor.HasBelly)
                {
                    output["Clothing2"].Sprite(input.Sprites.PantherOnePiece[finalStart + 1 + input.Actor.GetStomachSize(32)]);
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
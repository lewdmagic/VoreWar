#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

internal static class AntQueen
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Default<OverSizeParameters>, builder =>
    {
        IClothing<IOverSizeParameters> LeaderClothes = AntLeaderClothes.AntLeaderClothesInstance;


        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;
            unit.AccessoryColor = unit.SkinColor;

            unit.HairStyle = State.Rand.Next(14);

            if (unit.Type == UnitType.Leader)
            {
                unit.ClothingType = 1 + data.MiscRaceData.AllowedMainClothingTypes.IndexOf(LeaderClothes);
            }
        });

        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.Female, Gender.Hermaphrodite };
            output.BodySizes = 3;
            output.EyeTypes = 8;
            output.SpecialAccessoryCount = 12; // antennae        
            output.HairStyles = 14;
            output.MouthTypes = 3;
            output.EyeColors = 0;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemiantSkin);
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemiantSkin);

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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.AntQueen1[0 + input.Actor.Unit.BodySize]);
        }); // Upper Body (White)
        builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.AntQueen1[14 + input.Actor.Unit.EyeType]);
        });
        builder.RenderSingle(SpriteType.Mouth, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.AntQueen1[48]);
                return;
            }

            output.Sprite(input.Sprites.AntQueen1[49 + input.Actor.Unit.MouthType]);
        });

        builder.RenderSingle(SpriteType.Hair, 18, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.AntQueen1[34 + input.Actor.Unit.HairStyle]);
        });
        builder.RenderSingle(SpriteType.Body, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.AntQueen1[3 + input.Actor.Unit.BodySize]);
        }); // Lower Body (black)
        builder.RenderSingle(SpriteType.BodyAccent, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.AntQueen1[12]);
        }); // Abdomen 2 (White)
        builder.RenderSingle(SpriteType.BodyAccent2, 20, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.AntQueen1[22 + input.Actor.Unit.SpecialAccessoryType]);
        }); // Antennae (black)
        builder.RenderSingle(SpriteType.BodyAccent3, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.HasWeapon == false)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.AntQueen1[11]);
                    return;
                }

                output.Sprite(input.Sprites.AntQueen1[7]);
                return;
            }

            switch (input.Actor.GetWeaponSprite())
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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.AntQueen1[6]);
        }); // Lower Back Arms (black)
        builder.RenderSingle(SpriteType.BodyAccessory, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.AntQueen1[13]);
        }); // Abdomen (black)
        builder.RenderSingle(SpriteType.Breasts, 17, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(31 * 31));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 31)
                {
                    output.Sprite(input.Sprites.AntQueen2[30]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 29)
                {
                    output.Sprite(input.Sprites.AntQueen2[29]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 27)
                {
                    output.Sprite(input.Sprites.AntQueen2[28]);
                    return;
                }

                if (leftSize > 27)
                {
                    leftSize = 27;
                }

                output.Sprite(input.Sprites.AntQueen2[0 + leftSize]);
            }
            else
            {
                output.Sprite(input.Sprites.AntQueen2[0 + input.Actor.Unit.BreastSize]);
            }
        }); // Breasts (white)

        builder.RenderSingle(SpriteType.SecondaryBreasts, 17, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(31 * 31));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 31)
                {
                    output.Sprite(input.Sprites.AntQueen2[61]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 29)
                {
                    output.Sprite(input.Sprites.AntQueen2[60]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 27)
                {
                    output.Sprite(input.Sprites.AntQueen2[59]);
                    return;
                }

                if (rightSize > 27)
                {
                    rightSize = 27;
                }

                output.Sprite(input.Sprites.AntQueen2[31 + rightSize]);
            }
            else
            {
                output.Sprite(input.Sprites.AntQueen2[31 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(29, 0.8f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.AntQueen2[95]).AddOffset(0, -26 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.AntQueen2[94]).AddOffset(0, -26 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 28)
                {
                    output.Sprite(input.Sprites.AntQueen2[93]).AddOffset(0, -26 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 27)
                {
                    output.Sprite(input.Sprites.AntQueen2[92]).AddOffset(0, -26 * .625f);
                    return;
                }

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
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect())
            {
                if (input.Actor.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(31 * 31)) < 15 && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(31 * 31)) < 15)
                {
                    if (input.Actor.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.AntQueen1[68 + input.Actor.Unit.DickSize]).Layer(20);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.AntQueen1[52 + input.Actor.Unit.DickSize]).Layer(20);
                    }
                }
                else
                {
                    if (input.Actor.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.AntQueen1[76 + input.Actor.Unit.DickSize]).Layer(13);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.AntQueen1[60 + input.Actor.Unit.DickSize]).Layer(13);
                    }
                }
            }

            // pretty sure this is redundant. 
            //output.Layer(11); 
        });

        builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect() && input.Actor.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(31 * 31)) < 15 && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(31 * 31)) < 15)
            {
                output.Layer(19);
            }
            else
            {
                output.Layer(10);
            }

            int size = input.Actor.Unit.DickSize;
            int offsetI = input.Actor.GetBallSize(27, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offsetI == 27)
            {
                output.Sprite(input.Sprites.AntQueen2[132]).AddOffset(0, -17 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offsetI == 27)
            {
                output.Sprite(input.Sprites.AntQueen2[131]).AddOffset(0, -15 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offsetI == 26)
            {
                output.Sprite(input.Sprites.AntQueen2[130]).AddOffset(0, -13 * .625f);
                return;
            }

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
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                switch (input.Actor.GetWeaponSprite())
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
            CommonRaceCode.MakeBreastOversize(31 * 31).Invoke(input, output);
            Defaults.BasicBellyRunAfter.Invoke(input, output);
        });
    });


    private static class AntLeaderClothes
    {
        internal static readonly IClothing<IOverSizeParameters> AntLeaderClothesInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.AntQueen1[104];
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
                output.FixedColor = true;
                output.Type = 199;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing4"].Layer(6);
                output["Clothing4"].Coloring(Color.white);
                output["Clothing3"].Layer(19);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing2"].Layer(12);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Params.Oversize ? input.Sprites.AntQueen1[96] : input.Sprites.AntQueen1[Mathf.Min(88 + input.Actor.Unit.BreastSize, 96)]);

                output["Clothing2"].Sprite(input.Sprites.AntQueen1[97 + input.Actor.Unit.BodySize]);

                output["Clothing3"].Sprite(input.Sprites.AntQueen1[100]);

                if (input.Actor.GetWeaponSprite() == 1 || input.Actor.GetWeaponSprite() == 3)
                {
                    output["Clothing4"].Sprite(input.Sprites.AntQueen1[102]);
                }
                else if (input.Actor.GetWeaponSprite() == 5 || input.Actor.GetWeaponSprite() == 7)
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
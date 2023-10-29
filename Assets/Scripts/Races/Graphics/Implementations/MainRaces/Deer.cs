#region

using System;
using UnityEngine;

#endregion

internal static class Deer
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Default<OverSizeParameters>, builder =>
    {
        IClothing<IOverSizeParameters> LeaderClothes1 = DeerLeader1.DeerLeader1Instance;
        IClothing LeaderClothes2 = DeerLeader2.DeerLeader2Instance;
        IClothing LeaderClothes3 = DeerLeader3.DeerLeader3Instance;
        IClothing Rags = DeerRags.DeerRagsInstance;


        builder.Setup(output =>
        {
            output.DickSizes = () => 8;
            output.BreastSizes = () => 8;

            output.BodySizes = 4;
            output.EyeTypes = 5;
            output.SpecialAccessoryCount = 12; // ears     
            output.HairStyles = 25;
            output.MouthTypes = 6;
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DeerLeaf);
            output.HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.UniversalHair);
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DeerSkin);
            output.BodyAccentTypes1 = 12; // antlers
            output.BodyAccentTypes2 = 7; // pattern types
            output.BodyAccentTypes3 = 2; // leg types

            output.ExtendedBreastSprites = true;
            output.FurCapable = true;

            output.AllowedMainClothingTypes.Set(
                GenericTop1.GenericTop1Instance,
                GenericTop2.GenericTop2Instance,
                GenericTop3.GenericTop3Instance,
                GenericTop4.GenericTop4Instance,
                GenericTop5.GenericTop5Instance,
                GenericTop6.GenericTop6Instance,
                GenericTop7.GenericTop7Instance,
                MaleTop.MaleTopInstance,
                MaleTop2.MaleTop2Instance,
                Natural.NaturalInstance,
                Cuirass.CuirassInstance,
                Special1.Special1Instance,
                Special2.Special2Instance,
                Rags,
                LeaderClothes1,
                LeaderClothes2
            );
            output.AvoidedMainClothingTypes = 3;
            output.AvoidedEyeTypes = 0;
            output.AllowedWaistTypes.Set(
                GenericBot1.GenericBot1Instance,
                GenericBot2.GenericBot2Instance,
                GenericBot3.GenericBot3Instance,
                GenericBot4.GenericBot4Instance,
                GenericBot5.GenericBot5Instance,
                Loincloth.LoinclothInstance,
                LeaderClothes3
            );
            output.ExtraMainClothing1Types.Set(
                Scarf.ScarfInstance,
                Necklace.NecklaceInstance
            );

            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
        });


        builder.RunBefore((input, output) =>
        {
            CommonRaceCode.MakeBreastOversize(32 * 32).Invoke(input, output);
            Defaults.BasicBellyRunAfter.Invoke(input, output);
        });


        builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.Furry)
            {
                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.IsEating)
                    {
                        output.Sprite(input.Sprites.Deer1[53]);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.Deer1[52]);
                    }
                }
                else
                {
                    if (input.Actor.IsEating)
                    {
                        output.Sprite(input.Sprites.Deer1[55]);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.Deer1[54]);
                    }
                }
            }
            else
            {
                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.IsEating)
                    {
                        output.Sprite(input.Sprites.Deer1[49]);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.Deer1[48]);
                    }
                }
                else
                {
                    if (input.Actor.IsEating)
                    {
                        output.Sprite(input.Sprites.Deer1[51]);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.Deer1[50]);
                    }
                }
            }
        });

        builder.RenderSingle(SpriteType.Eyes, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            if (input.Actor.Unit.Furry)
            {
                output.AddOffset(0, 1 * .625f);
            }

            output.Sprite(input.Sprites.Deer1[71 + input.Actor.Unit.EyeType]);
        });
        builder.RenderSingle(SpriteType.Mouth, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.Furry)
            {
                if (input.Actor.IsEating)
                {
                    output.Sprite(input.Sprites.Deer1[60]);
                }
                else if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Deer1[62]);
                }
            }
            else
            {
                if (input.Actor.IsEating)
                {
                    output.Sprite(input.Sprites.Deer1[56]);
                }
                else if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Deer1[58]);
                }
            }
        });

        builder.RenderSingle(SpriteType.Hair, 21, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, input.Actor.Unit.HairColor));
            if (input.Actor.Unit.HairStyle == 24 || input.Actor.Unit.Furry)
            {
            }
            else
            {
                output.Sprite(input.Sprites.Deer1[84 + input.Actor.Unit.HairStyle]);
            }
        });

        builder.RenderSingle(SpriteType.Hair2, 0, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, input.Actor.Unit.HairColor));
            if (input.Actor.Unit.HairStyle == 24 || input.Actor.Unit.Furry)
            {
            }
            else
            {
                output.Sprite(input.Sprites.Deer1[108 + input.Actor.Unit.HairStyle]);
            }
        });

        builder.RenderSingle(SpriteType.Hair3, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.UniversalHair, input.Actor.Unit.HairColor));
            if (input.Actor.Unit.Furry)
            {
            }
            else
            {
                if (input.Actor.Unit.HasBreasts)
                {
                    output.Sprite(input.Sprites.Deer1[132]);
                }
                else
                {
                    output.Sprite(input.Sprites.Deer1[133]);
                }
            }
        }); // Eyebrows

        builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Sprites.Deer1[0 + input.Actor.Unit.BodySize]);
            }
            else
            {
                output.Sprite(input.Sprites.Deer1[12 + input.Actor.Unit.BodySize]);
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasWeapon == false)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Deer1[6 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 12 : 0)]);
                    return;
                }

                output.Sprite(input.Sprites.Deer1[4 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 12 : 0)]);
                return;
            }

            switch (input.Actor.GetWeaponSprite())
            {
                case 0:
                    output.Sprite(input.Sprites.Deer1[5 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 12 : 0)]);
                    return;
                case 1:
                    output.Sprite(input.Sprites.Deer1[6 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 12 : 0)]);
                    return;
                case 2:
                    output.Sprite(input.Sprites.Deer1[5 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 12 : 0)]);
                    return;
                case 3:
                    output.Sprite(input.Sprites.Deer1[6 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 12 : 0)]);
                    return;
                case 4:
                    output.Sprite(input.Sprites.Deer1[4 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 12 : 0)]);
                    return;
                case 5:
                    output.Sprite(input.Sprites.Deer1[5 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 12 : 0)]);
                    return;
                case 6:
                    output.Sprite(input.Sprites.Deer1[4 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 12 : 0)]);
                    return;
                case 7:
                    output.Sprite(input.Sprites.Deer1[5 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 12 : 0)]);
                    return;
                default:
                    output.Sprite(input.Sprites.Deer1[4 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 12 : 0)]);
                    return;
            }
        }); // Right Arm

        builder.RenderSingle(SpriteType.BodyAccent2, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasWeapon == false)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Deer1[23]);
                    return;
                }

                output.Sprite(input.Sprites.Deer1[10]);
                return;
            }

            if (!input.Actor.Unit.HasBreasts)
            {
                output.AddOffset(2 * .625f, 0);
            }

            switch (input.Actor.GetWeaponSprite())
            {
                case 0:
                    output.Sprite(input.Sprites.Deer1[22]);
                    return;
                case 1:
                    output.Sprite(input.Sprites.Deer1[23]);
                    return;
                case 2:
                    output.Sprite(input.Sprites.Deer1[22]);
                    return;
                case 3:
                    output.Sprite(input.Sprites.Deer1[23]);
                    return;
                case 4:
                    output.Sprite(input.Sprites.Deer1[11]);
                    return;
                case 5:
                    output.Sprite(input.Sprites.Deer1[22]);
                    return;
                case 6:
                    output.Sprite(input.Sprites.Deer1[11]);
                    return;
                case 7:
                    output.Sprite(input.Sprites.Deer1[22]);
                    return;
                default:
                    output.Sprite(input.Sprites.Deer1[10]);
                    return;
            }
        }); // Right Hand

        builder.RenderSingle(SpriteType.BodyAccent3, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.BodyAccentType2 >= 6) //Changed to >= to hopefully prevent a rare exception
            {
            }
            else if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Sprites.Deer2[0 + input.Actor.Unit.BodySize + 20 * input.Actor.Unit.BodyAccentType2]);
            }
            else
            {
                output.Sprite(input.Sprites.Deer2[10 + input.Actor.Unit.BodySize + 20 * input.Actor.Unit.BodyAccentType2]);
            }
        }); // Body Pattern

        builder.RenderSingle(SpriteType.BodyAccent4, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.BodyAccentType2 == 6)
            {
                return;
            }

            if (input.Actor.Unit.HasWeapon == false)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Deer2[6 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 10 : 0) + 20 * input.Actor.Unit.BodyAccentType2]);
                    return;
                }

                output.Sprite(input.Sprites.Deer2[4 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 10 : 0) + 20 * input.Actor.Unit.BodyAccentType2]);
                return;
            }

            switch (input.Actor.GetWeaponSprite())
            {
                case 0:
                    output.Sprite(input.Sprites.Deer2[5 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 10 : 0) + 20 * input.Actor.Unit.BodyAccentType2]);
                    return;
                case 1:
                    output.Sprite(input.Sprites.Deer2[6 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 10 : 0) + 20 * input.Actor.Unit.BodyAccentType2]);
                    return;
                case 2:
                    output.Sprite(input.Sprites.Deer2[5 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 10 : 0) + 20 * input.Actor.Unit.BodyAccentType2]);
                    return;
                case 3:
                    output.Sprite(input.Sprites.Deer2[6 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 10 : 0) + 20 * input.Actor.Unit.BodyAccentType2]);
                    return;
                case 4:
                    output.Sprite(input.Sprites.Deer2[4 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 10 : 0) + 20 * input.Actor.Unit.BodyAccentType2]);
                    return;
                case 5:
                    output.Sprite(input.Sprites.Deer2[5 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 10 : 0) + 20 * input.Actor.Unit.BodyAccentType2]);
                    return;
                case 6:
                    output.Sprite(input.Sprites.Deer2[4 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 10 : 0) + 20 * input.Actor.Unit.BodyAccentType2]);
                    return;
                case 7:
                    output.Sprite(input.Sprites.Deer2[5 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 10 : 0) + 20 * input.Actor.Unit.BodyAccentType2]);
                    return;
                default:
                    output.Sprite(input.Sprites.Deer2[4 + (input.Actor.Unit.BodySize > 1 ? 3 : 0) + (!input.Actor.Unit.HasBreasts ? 10 : 0) + 20 * input.Actor.Unit.BodyAccentType2]);
                    return;
            }
        }); // Arm Pattern

        builder.RenderSingle(SpriteType.BodyAccent5, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.Furry)
            {
                if (input.Actor.Unit.HasBreasts)
                {
                    output.Sprite(input.Sprites.Deer1[136]);
                }
                else
                {
                    output.Sprite(input.Sprites.Deer1[137]);
                }
            }
            else
            {
                output.Sprite(input.Sprites.Deer1[135]);
            }
        }); // Nose

        builder.RenderSingle(SpriteType.BodyAccent6, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Deer1[134]);
        }); // Hoofs
        builder.RenderSingle(SpriteType.BodyAccent7, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.Furry)
            {
                if (input.Actor.IsEating)
                {
                    output.Sprite(input.Sprites.Deer1[61]);
                    return;
                }

                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Deer1[63]);
                    return;
                }

                output.Sprite(input.Sprites.Deer1[70]);
            }
            else
            {
                if (input.Actor.IsEating)
                {
                    output.Sprite(input.Sprites.Deer1[57]);
                    return;
                }

                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Deer1[59]);
                    return;
                }

                output.Sprite(input.Sprites.Deer1[64 + input.Actor.Unit.MouthType]);
            }
        }); // Mouth external

        builder.RenderSingle(SpriteType.BodyAccent8, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.BodyAccentType3 == 1)
            {
            }
            else if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Sprites.Deer4[88 + input.Actor.Unit.BodySize]);
            }
            else
            {
                output.Sprite(input.Sprites.Deer4[92 + input.Actor.Unit.BodySize]);
            }
        }); // alternative legs

        builder.RenderSingle(SpriteType.BodyAccessory, 20, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Deer1[24 + input.Actor.Unit.SpecialAccessoryType]);
        }); // Ears
        builder.RenderSingle(SpriteType.SecondaryAccessory, 22, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (!input.Actor.Unit.HasDick)
            {
            }
            else
            {
                output.Sprite(input.Sprites.Deer1[36 + input.Actor.Unit.BodyAccentType1]);
            }
        }); // Antlers

        builder.RenderSingle(SpriteType.Breasts, 17, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    output.Sprite(input.Sprites.Cockatrice2[31]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.Cockatrice2[30]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.Cockatrice2[29]);
                    return;
                }

                if (leftSize > 28)
                {
                    leftSize = 28;
                }

                output.Sprite(input.Sprites.Cockatrice2[0 + leftSize]);
            }
            else
            {
                output.Sprite(input.Sprites.Cockatrice2[0 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.SecondaryBreasts, 17, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.Cockatrice2[63]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.Cockatrice2[62]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.Cockatrice2[61]);
                    return;
                }

                if (rightSize > 28)
                {
                    rightSize = 28;
                }

                output.Sprite(input.Sprites.Cockatrice2[32 + rightSize]);
            }
            else
            {
                output.Sprite(input.Sprites.Cockatrice2[32 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, input.Actor.Unit.SkinColor));
            output.AddOffset(0, -1 * .625f);

            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(31, 0.7f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Cockatrice2[99]).AddOffset(0, -29 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Cockatrice2[98]).AddOffset(0, -29 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                {
                    output.Sprite(input.Sprites.Cockatrice2[97]).AddOffset(0, -29 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.Cockatrice2[96]).AddOffset(0, -29 * .625f);
                    return;
                }

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
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (!input.Actor.Unit.Furry || !Config.FurryGenitals)
            {
                output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, input.Actor.Unit.SkinColor));
            }

            if (input.Actor.Unit.Furry && Config.FurryGenitals)
            {
                output.AddOffset(0, -3 * .625f);

                if (input.Actor.IsErect())
                {
                    if (input.Actor.PredatorComponent?.VisibleFullness < .75f &&
                        (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize +
                                       input.Actor.GetRightBreastSize(32 * 32)) < 16 &&
                        (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize +
                                       input.Actor.GetLeftBreastSize(32 * 32)) < 16)
                    {
                        output.Layer(24);
                        if (input.Actor.IsCockVoring)
                        {
                            output.Sprite(input.Sprites.Deer3[54 + input.Actor.Unit.DickSize]);
                            return;
                        }

                        output.Sprite(input.Sprites.Deer3[38 + input.Actor.Unit.DickSize]);
                        return;
                    }

                    output.Layer(13);
                    if (input.Actor.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.Deer3[62 + input.Actor.Unit.DickSize]);
                        return;
                    }

                    output.Sprite(input.Sprites.Deer3[46 + input.Actor.Unit.DickSize]);
                    return;
                }

                output.Layer(11); //why dis here
                return;
            }

            if (input.Actor.IsErect())
            {
                if (input.Actor.PredatorComponent?.VisibleFullness < .75f &&
                    (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize +
                                   input.Actor.GetRightBreastSize(32 * 32)) < 16 &&
                    (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize +
                                   input.Actor.GetLeftBreastSize(32 * 32)) < 16)
                {
                    output.Layer(24);
                    if (input.Actor.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.Deer3[86 + input.Actor.Unit.DickSize]);
                        return;
                    }

                    output.Sprite(input.Sprites.Deer3[70 + input.Actor.Unit.DickSize]);
                    return;
                }

                output.Layer(13);
                if (input.Actor.IsCockVoring)
                {
                    output.Sprite(input.Sprites.Deer3[94 + input.Actor.Unit.DickSize]);
                    return;
                }

                output.Sprite(input.Sprites.Deer3[78 + input.Actor.Unit.DickSize]);
                return;
            }

            output.Sprite(input.Sprites.Deer3[78 + input.Actor.Unit.DickSize]).Layer(11);
        });

        builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.Furry && Config.FurryGenitals)
            {
                output.AddOffset(0, -3 * .625f);
            }

            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect() && input.Actor.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32)) < 16)
            {
                output.Layer(19);
            }
            else
            {
                output.Layer(10);
            }

            int size = input.Actor.Unit.DickSize;
            int offset = input.Actor.GetBallSize(28, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Deer3[139 - (input.Actor.Unit.Furry && Config.FurryGenitals ? 102 : 0)]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Deer3[138 - (input.Actor.Unit.Furry && Config.FurryGenitals ? 102 : 0)]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.Deer3[137 - (input.Actor.Unit.Furry && Config.FurryGenitals ? 102 : 0)]).AddOffset(0, -18 * .625f);
                return;
            }

            if (offset >= 26)
            {
                output.AddOffset(0, -15 * .625f);
            }
            else if (offset == 25)
            {
                output.AddOffset(0, -9 * .625f);
            }
            else if (offset == 24)
            {
                output.AddOffset(0, -7 * .625f);
            }
            else if (offset == 23)
            {
                output.AddOffset(0, -6 * .625f);
            }
            else if (offset == 22)
            {
                output.AddOffset(0, -3 * .625f);
            }
            else if (offset == 21)
            {
                output.AddOffset(0, -2 * .625f);
            }

            if (offset > 0)
            {
                output.Sprite(input.Sprites.Deer3[Math.Min(110 + offset, 136) - (input.Actor.Unit.Furry && Config.FurryGenitals ? 102 : 0)]);
                return;
            }

            output.Sprite(input.Sprites.Deer3[102 + size - (input.Actor.Unit.Furry && Config.FurryGenitals ? 102 : 0)]);
        });

        builder.RenderSingle(SpriteType.Weapon, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (!input.Actor.Unit.HasBreasts)
            {
                output.AddOffset(2 * .625f, 0);
            }

            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                output.Sprite(input.Sprites.Deer1[76 + input.Actor.GetWeaponSprite()]);
            }
        });

        builder.RandomCustom(data =>
        {
            Unit unit = data.Unit;
            Defaults.RandomCustom(data);


            if (State.Rand.Next(3) == 0)
            {
                unit.BodyAccentType2 = data.MiscRaceData.BodyAccentTypes2 - 1;
            }
            else
            {
                unit.BodyAccentType2 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes2 - 1);
            }

            unit.BodyAccentType1 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes1);
            unit.BodyAccentType3 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes3);

            unit.ClothingExtraType1 = 0;

            if (unit.HasDick && unit.HasBreasts)
            {
                if (Config.HermsOnlyUseFemaleHair)
                {
                    unit.HairStyle = State.Rand.Next(18);
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
                }
            }
            else if (unit.HasDick && Config.FemaleHairForMales)
            {
                unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
            }
            else if (unit.HasDick == false && Config.MaleHairForFemales)
            {
                unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
            }
            else
            {
                if (unit.HasDick)
                {
                    unit.HairStyle = 12 + State.Rand.Next(13);
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(18);
                }
            }

            if (unit.Type == UnitType.Leader)
            {
                unit.ClothingType2 = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedWaistTypes, LeaderClothes3);
                if (unit.HasBreasts)
                {
                    unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypes, LeaderClothes1);
                }
                else
                {
                    unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypes, LeaderClothes2);
                }
            }

            if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
            {
                unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypes, Rags);
                if (unit.ClothingType == 0) //Covers rags not in the list
                {
                    unit.ClothingType = data.MiscRaceData.AllowedMainClothingTypes.Count;
                }
            }
        });
    });


    private static class GenericTop1
    {
        internal static IClothing<IOverSizeParameters> GenericTop1Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians4[24];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1524;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Cockatrice3[56]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Cockatrice3[48 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop2
    {
        internal static IClothing<IOverSizeParameters> GenericTop2Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians4[34];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1534;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Cockatrice3[65]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Cockatrice3[57 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop3
    {
        internal static IClothing<IOverSizeParameters> GenericTop3Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians4[44];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1544;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Cockatrice3[74]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Cockatrice3[66 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop4
    {
        internal static IClothing<IOverSizeParameters> GenericTop4Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians4[55];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1555;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                output["Clothing2"].Layer(18);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].SetOffset(0, -2 * .625f);
                output["Clothing2"].SetOffset(0, -2 * .625f);

                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Sharks5[80]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Sharks5[72 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing2"].Sprite(input.Sprites.Sharks5[81]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop5
    {
        internal static IClothing<IOverSizeParameters> GenericTop5Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians4[74];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1574;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                output["Clothing2"].Layer(18);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].SetOffset(0, -2 * .625f);
                output["Clothing2"].SetOffset(0, -2 * .625f);

                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Sharks5[90]);
                    output["Clothing2"].Sprite(input.Sprites.Sharks5[99]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Sharks5[82 + input.Actor.Unit.BreastSize]);
                    output["Clothing2"].Sprite(input.Sprites.Sharks5[91 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop6
    {
        internal static IClothing<IOverSizeParameters> GenericTop6Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians4[88];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1588;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                output["Clothing1"].SetOffset(0, -2 * .625f);

                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Sharks5[104 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop7
    {
        internal static IClothing<IOverSizeParameters> GenericTop7Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians4[44];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1544;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Cockatrice3[95]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Cockatrice3[87 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class MaleTop
    {
        internal static IClothing MaleTopInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians4[79];
                output.MaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1579;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);

                if (input.Actor.HasBelly)
                {
                    output["Clothing1"].Sprite(input.Sprites.Cockatrice3[83 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Deer4[84 + input.Actor.Unit.BodySize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class MaleTop2
    {
        internal static IClothing MaleTop2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians4[79];
                output.MaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 1579;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                output["Clothing1"].Sprite(input.Sprites.Cockatrice3[75 + input.Actor.Unit.BodySize]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class Natural
    {
        internal static IClothing<IOverSizeParameters> NaturalInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(7);
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(null);
                    output["Clothing2"].Sprite(input.Sprites.Deer4[0]);
                    output["Clothing2"].SetOffset(0, 0);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Deer4[2 + input.Actor.Unit.BreastSize]);
                    output["Clothing2"].Sprite(input.Sprites.Deer4[0]);
                    output["Clothing2"].SetOffset(0, 0);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Deer4[1]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, input.Actor.Unit.SkinColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, input.Actor.Unit.SkinColor));
            });
        });
    }

    private static class Cuirass
    {
        internal static IClothing<IOverSizeParameters> CuirassInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Deer4[47];
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
                output.FixedColor = true;
                output.Type = 61701;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing4"].Layer(7);
                output["Clothing4"].Coloring(Color.white);
                output["Clothing3"].Layer(12);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing2"].Layer(7);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Deer4[64]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.Unit.BreastSize < 2)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Deer4[60]);
                    }
                    else if (input.Actor.Unit.BreastSize < 4)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Deer4[61]);
                    }
                    else if (input.Actor.Unit.BreastSize < 6)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Deer4[62]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Deer4[63]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Deer4[83]);
                }

                if (input.Actor.HasBelly)
                {
                    output["Clothing2"].Sprite(null);
                }
                else
                {
                    if (input.Actor.Unit.HasBreasts)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Deer4[73 + input.Actor.Unit.BodySize]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.Deer4[77 + input.Actor.Unit.BodySize]);
                    }
                }

                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing3"].Sprite(input.Sprites.Deer4[65 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing3"].Sprite(input.Sprites.Deer4[69 + input.Actor.Unit.BodySize]);
                }

                if (input.Actor.GetWeaponSprite() == 1)
                {
                    output["Clothing4"].Sprite(input.Sprites.Deer4[82]);
                }
                else
                {
                    output["Clothing4"].Sprite(input.Sprites.Deer4[81]);
                }
            });
        });
    }

    private static class Special1
    {
        internal static IClothing<IOverSizeParameters> Special1Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Deer4[104];
                output.BlocksBreasts = true;
                output.RevealsBreasts = true;
                output.FemaleOnly = true;
                output.RevealsDick = true;
                output.Type = 61708;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(17);
                output["Clothing2"].Layer(17);
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Deer4[103]);
                    output.BlocksBreasts = false;
                    output["Clothing2"].Sprite(null);
                    output["Clothing3"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output.BlocksBreasts = true;
                    output["Clothing1"].Sprite(input.Sprites.Deer4[96 + input.Actor.Unit.BreastSize]);
                    if (input.Actor.Unit.BreastSize == 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.DeerLeaderClothes[50]);
                        output["Clothing3"].Sprite(input.Sprites.DeerLeaderClothes[51]);
                    }
                    else if (input.Actor.Unit.BreastSize == 4)
                    {
                        output["Clothing2"].Sprite(input.Sprites.DeerLeaderClothes[52]);
                        output["Clothing3"].Sprite(input.Sprites.DeerLeaderClothes[53]);
                    }
                    else if (input.Actor.Unit.BreastSize == 5)
                    {
                        output["Clothing2"].Sprite(input.Sprites.DeerLeaderClothes[54]);
                        output["Clothing3"].Sprite(input.Sprites.DeerLeaderClothes[55]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.Cockatrice2[0 + input.Actor.Unit.BreastSize]);
                        output["Clothing3"].Sprite(input.Sprites.Cockatrice2[32 + input.Actor.Unit.BreastSize]);
                    }
                }
                else
                {
                    output.BlocksBreasts = true;
                }

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, input.Actor.Unit.SkinColor));
                output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, input.Actor.Unit.SkinColor));
            });
        });
    }

    private static class Special2
    {
        internal static IClothing Special2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Deer4[107];
                output.MaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 61709;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Deer4[108 + input.Actor.Unit.BodySize]);
            });
        });
    }

    private static class DeerRags
    {
        internal static IClothing DeerRagsInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Rags[23];
                output.RevealsDick = true;
                output.InFrontOfDick = true;
                output.RevealsBreasts = true;
                output.Type = 207;
                output.OccupiesAllSlots = true;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(12);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.Unit.BreastSize < 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Deer4[57]);
                    }
                    else if (input.Actor.Unit.BreastSize < 6)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Deer4[58]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Deer4[59]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Deer4[48 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Deer4[56]);
                    output["Clothing2"].Sprite(input.Sprites.Deer4[52 + input.Actor.Unit.BodySize]);
                }
            });
        });
    }

    private static class DeerLeader1
    {
        internal static IClothing<IOverSizeParameters> DeerLeader1Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.LeaderOnly = true;
                output.DiscardSprite = input.Sprites.DeerLeaderClothes[49];
                output.BlocksBreasts = true;
                output.RevealsBreasts = true;
                output.FemaleOnly = true;
                output.RevealsDick = true;
                output.Type = 61702;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(17);
                output["Clothing2"].Layer(17);
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.DeerLeaderClothes[63]);
                    output.BlocksBreasts = false;
                    output["Clothing2"].Sprite(null);
                    output["Clothing3"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output.BlocksBreasts = true;
                    output["Clothing1"].Sprite(input.Sprites.DeerLeaderClothes[Math.Min(56 + input.Actor.Unit.BreastSize, 66)]);
                    if (input.Actor.Unit.BreastSize == 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.DeerLeaderClothes[50]);
                        output["Clothing3"].Sprite(input.Sprites.DeerLeaderClothes[51]);
                    }
                    else if (input.Actor.Unit.BreastSize == 4)
                    {
                        output["Clothing2"].Sprite(input.Sprites.DeerLeaderClothes[52]);
                        output["Clothing3"].Sprite(input.Sprites.DeerLeaderClothes[53]);
                    }
                    else if (input.Actor.Unit.BreastSize == 5)
                    {
                        output["Clothing2"].Sprite(input.Sprites.DeerLeaderClothes[54]);
                        output["Clothing3"].Sprite(input.Sprites.DeerLeaderClothes[55]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.Cockatrice2[0 + input.Actor.Unit.BreastSize]);
                        output["Clothing3"].Sprite(input.Sprites.Cockatrice2[32 + input.Actor.Unit.BreastSize]);
                    }
                }
                
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, input.Actor.Unit.SkinColor));
                output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerSkin, input.Actor.Unit.SkinColor));
            });
        });
    }

    private static class DeerLeader2
    {
        internal static IClothing DeerLeader2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.LeaderOnly = true;
                output.DiscardSprite = input.Sprites.DeerLeaderClothes[65];
                output.MaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 61703;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.DeerLeaderClothes[64]);
            });
        });
    }

    private static class GenericBot1
    {
        internal static IClothing GenericBot1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians3[121];
                output.RevealsBreasts = true;
                output.Type = 1521;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(12);
                output["Clothing1"].Layer(13);
                if (input.Actor.Unit.DickSize > 0)
                {
                    if (input.Actor.Unit.DickSize < 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[20]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
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

                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing2"].Sprite(input.Sprites.Deer4[12 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Deer4[16 + input.Actor.Unit.BodySize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericBot2
    {
        internal static IClothing GenericBot2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians3[137];
                output.RevealsBreasts = true;
                output.Type = 1537;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(13);

                output["Clothing2"].Layer(12);

                output["Clothing2"].Coloring(Color.white);

                if (input.Actor.Unit.DickSize > 0)
                {
                    if (input.Actor.Unit.DickSize < 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[32]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
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

                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing2"].Sprite(input.Sprites.Deer4[20 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Deer4[24 + input.Actor.Unit.BodySize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericBot3
    {
        internal static IClothing GenericBot3Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians3[140];
                output.RevealsBreasts = true;
                output.Type = 1540;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(13);
                output["Clothing2"].Layer(12);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Cockatrice3[35]);

                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing2"].Sprite(input.Sprites.Deer4[20 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Deer4[24 + input.Actor.Unit.BodySize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericBot4
    {
        internal static IClothing GenericBot4Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Cockatrice3[47];
                output.RevealsBreasts = true;
                output.Type = 61602;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(12);

                output["Clothing2"].Coloring(Color.white);

                output["Clothing1"].Layer(13);

                output["Clothing1"].Coloring(Color.white);

                if (input.Actor.Unit.DickSize > 0)
                {
                    if (input.Actor.Unit.DickSize < 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[44]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
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

                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing2"].Sprite(input.Sprites.Deer4[28 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Deer4[32 + input.Actor.Unit.BodySize]);
                }
            });
        });
    }

    private static class GenericBot5
    {
        internal static IClothing GenericBot5Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Avians3[121];
                output.RevealsBreasts = true;
                output.Type = 1521;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(12);
                output["Clothing1"].Layer(13);
                if (input.Actor.Unit.DickSize > 0)
                {
                    output["Clothing1"].SetOffset(0, -1 * .625f);
                    if (input.Actor.Unit.DickSize < 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Deer4[44]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Deer4[46]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Deer4[45]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                output["Clothing1"].SetOffset(0, 0);

                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing2"].Sprite(input.Sprites.Deer4[36 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Deer4[40 + input.Actor.Unit.BodySize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class Loincloth
    {
        internal static IClothing LoinclothInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.DeerLeaderClothes[66];
                output.RevealsBreasts = true;
                output.Type = 61705;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(12);
                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.Sprites.DeerLeaderClothes[1 + 2 * input.Actor.Unit.BodySize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.DeerLeaderClothes[0 + 2 * input.Actor.Unit.BodySize]);
                    }
                }
                else
                {
                    if (input.Actor.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.Sprites.DeerLeaderClothes[9 + 2 * input.Actor.Unit.BodySize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.DeerLeaderClothes[8 + 2 * input.Actor.Unit.BodySize]);
                    }
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class DeerLeader3
    {
        internal static IClothing DeerLeader3Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.LeaderOnly = true;
                output.DiscardSprite = input.Sprites.DeerLeaderClothes[48];
                output.RevealsBreasts = true;
                output.Type = 61704;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(15);
                output["Clothing2"].Layer(12);
                output["Clothing1"].Layer(15);
                output["Clothing1"].Coloring(Color.white);
                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.HasBelly)
                    {
                        output["Clothing2"].Sprite(input.Sprites.DeerLeaderClothes[1 + 2 * input.Actor.Unit.BodySize]);

                        if (input.Actor.GetStomachSize(31, 0.7f) < 4)
                        {
                            output["Clothing1"].Sprite(input.Sprites.DeerLeaderClothes[17 + 2 * input.Actor.Unit.BodySize]);
                            output["Clothing3"].Sprite(input.Sprites.DeerLeaderClothes[33 + 2 * input.Actor.Unit.BodySize]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(null);
                            output["Clothing3"].Sprite(null);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.DeerLeaderClothes[16 + 2 * input.Actor.Unit.BodySize]);
                        output["Clothing2"].Sprite(input.Sprites.DeerLeaderClothes[0 + 2 * input.Actor.Unit.BodySize]);
                        output["Clothing3"].Sprite(input.Sprites.DeerLeaderClothes[32 + 2 * input.Actor.Unit.BodySize]);
                    }
                }
                else
                {
                    if (input.Actor.HasBelly)
                    {
                        output["Clothing2"].Sprite(input.Sprites.DeerLeaderClothes[9 + 2 * input.Actor.Unit.BodySize]);

                        if (input.Actor.GetStomachSize(31, 0.7f) < 4)
                        {
                            output["Clothing1"].Sprite(input.Sprites.DeerLeaderClothes[25 + 2 * input.Actor.Unit.BodySize]);
                            output["Clothing3"].Sprite(input.Sprites.DeerLeaderClothes[41 + 2 * input.Actor.Unit.BodySize]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(null);
                            output["Clothing3"].Sprite(null);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.DeerLeaderClothes[24 + 2 * input.Actor.Unit.BodySize]);
                        output["Clothing2"].Sprite(input.Sprites.DeerLeaderClothes[8 + 2 * input.Actor.Unit.BodySize]);
                        output["Clothing3"].Sprite(input.Sprites.DeerLeaderClothes[40 + 2 * input.Actor.Unit.BodySize]);
                    }
                }

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
                output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DeerLeaf, input.Actor.Unit.AccessoryColor));
            });
        });
    }

    private static class Scarf
    {
        internal static IClothing ScarfInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Deer4[106];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 61706;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(19);
                output["Clothing1"].Sprite(input.Sprites.Deer4[105]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class Necklace
    {
        internal static IClothing NecklaceInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Deer4[11];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 61707;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(19);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Deer4[10]);
            });
        });
    }
}
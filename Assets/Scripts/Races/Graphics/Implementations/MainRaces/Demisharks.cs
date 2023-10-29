﻿#region

using System;
using UnityEngine;

#endregion

internal static class Demisharks
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Default<OverSizeParameters>, builder =>
    {
        IClothing<IOverSizeParameters> LeaderClothes = DemisharkLeader.DemisharkLeaderInstance;
        IClothing Rags = DemisharkRags.DemisharkRagsInstance;


        builder.Setup(output =>
        {
            output.DickSizes = () => 8;
            output.BreastSizes = () => 8;

            output.BodySizes = 4;
            output.EyeTypes = 9;
            output.SpecialAccessoryCount = 12; // ears     
            output.HairStyles = 25;
            output.MouthTypes = 0;
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SharkSkin);
            output.HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.MermenHair);
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SharkSkin);
            output.BodyAccentTypes1 = 6; // body types
            output.BodyAccentTypes2 = 7; // pattern types
            output.BodyAccentTypes3 = 2; // nose types
            output.TailTypes = 12;

            output.ExtendedBreastSprites = true;

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
                PirateTop1.PirateTop1Instance,
                PirateTop2.PirateTop2Instance,
                Rags,
                LeaderClothes
            );
            output.AvoidedMainClothingTypes = 2;
            output.AvoidedEyeTypes = 0;
            output.AllowedWaistTypes.Set(
                GenericBot1.GenericBot1Instance,
                GenericBot2.GenericBot2Instance,
                GenericBot3.GenericBot3Instance,
                GenericBot4.GenericBot4Instance,
                PirateBot1.PirateBot1Instance,
                PirateBot2.PirateBot2Instance,
                PirateBot3.PirateBot3Instance
            );
            output.ExtraMainClothing1Types.Set( // hats
                PirateHat1.PirateHat1Instance,
                PirateHat2.PirateHat2Instance,
                PirateHat3.PirateHat3Instance
            );

            output.AllowedClothingHatTypes.Clear();

            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
        });


        builder.RunBefore((input, output) =>
        {
            CommonRaceCode.MakeBreastOversize(32 * 32).Invoke(input, output);
            Defaults.BasicBellyRunAfter.Invoke(input, output);
        });

        builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Sharks1[10 + 11 * input.Actor.Unit.BodyAccentType3 + 22 * input.Actor.Unit.BodyAccentType1]);
                return;
            }

            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Sharks1[9 + 11 * input.Actor.Unit.BodyAccentType3 + 22 * input.Actor.Unit.BodyAccentType1]);
                return;
            }

            output.Sprite(input.Sprites.Sharks1[8 + 11 * input.Actor.Unit.BodyAccentType3 + 22 * input.Actor.Unit.BodyAccentType1]);
        });

        builder.RenderSingle(SpriteType.Eyes, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            output.Sprite(input.Sprites.Sharks3[2 + input.Actor.Unit.EyeType]);
        });
        builder.RenderSingle(SpriteType.Mouth, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Sharks3[1]);
            }
            else if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Sharks3[0]);
            }
        });

        builder.RenderSingle(SpriteType.Hair, 20, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenHair, input.Actor.Unit.HairColor));
            if (input.Actor.Unit.HairStyle == 24)
            {
            }
            else if (input.Actor.Unit.ClothingExtraType1 == 3)
            {
                output.Sprite(input.Sprites.Sharks7[60 + input.Actor.Unit.HairStyle]);
            }
            else if (input.Actor.Unit.ClothingExtraType1 == 1)
            {
                output.Sprite(input.Sprites.Sharks7[12 + input.Actor.Unit.HairStyle]);
            }
            else if (input.Actor.Unit.ClothingExtraType1 == 2)
            {
                output.Sprite(input.Sprites.Sharks7[36 + input.Actor.Unit.HairStyle]);
            }
            else if (input.Actor.Unit.HairStyle < 12)
            {
                output.Sprite(input.Sprites.Sharks3[36 + input.Actor.Unit.HairStyle]);
            }
            else
            {
                output.Sprite(input.Sprites.Sharks3[48 + input.Actor.Unit.HairStyle]);
            }
        });

        builder.RenderSingle(SpriteType.Hair2, 0, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenHair, input.Actor.Unit.HairColor));
            if (input.Actor.Unit.HairStyle > 14)
            {
            }
            else if (input.Actor.Unit.HairStyle < 12)
            {
                output.Sprite(input.Sprites.Sharks3[48 + input.Actor.Unit.HairStyle]);
            }
            else
            {
                output.Sprite(input.Sprites.Sharks3[60 + input.Actor.Unit.HairStyle]);
            }
        });

        builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Sprites.Sharks1[0 + input.Actor.Unit.BodySize + 22 * input.Actor.Unit.BodyAccentType1]);
            }
            else
            {
                output.Sprite(input.Sprites.Sharks1[11 + input.Actor.Unit.BodySize + 22 * input.Actor.Unit.BodyAccentType1]);
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, input.Actor.Unit.SkinColor));
            if (!input.Actor.Unit.HasBreasts)
            {
                output.AddOffset(1 * .625f, 0);
            }

            if (input.Actor.Unit.HasWeapon == false)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Sharks1[7 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType1]);
                    return;
                }

                output.Sprite(input.Sprites.Sharks1[4 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType1]);
                return;
            }

            switch (input.Actor.GetWeaponSprite())
            {
                case 0:
                    output.Sprite(input.Sprites.Sharks1[5 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType1]);
                    return;
                case 1:
                    output.Sprite(input.Sprites.Sharks1[6 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType1]);
                    return;
                case 2:
                    output.Sprite(input.Sprites.Sharks1[5 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType1]);
                    return;
                case 3:
                    output.Sprite(input.Sprites.Sharks1[6 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType1]);
                    return;
                case 4:
                    output.Sprite(input.Sprites.Sharks1[5 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType1]);
                    return;
                case 5:
                    output.Sprite(input.Sprites.Sharks1[7 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType1]);
                    return;
                case 6:
                    output.Sprite(input.Sprites.Sharks1[5 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType1]);
                    return;
                case 7:
                    output.Sprite(input.Sprites.Sharks1[6 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType1]);
                    return;
                default:
                    output.Sprite(input.Sprites.Sharks1[4 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType1]);
                    return;
            }
        }); // Right Arm

        builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Sharks3[84 + input.Actor.Unit.TailType + 12 * (input.Actor.Unit.BodyAccentType1 == 0 ? 1 : 0)]);
        }); // Tail
        builder.RenderSingle(SpriteType.BodyAccent3, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType2 == 6)
            {
            }
            else if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Sprites.Sharks2[0 + input.Actor.Unit.BodySize + 22 * input.Actor.Unit.BodyAccentType2]);
            }
            else
            {
                output.Sprite(input.Sprites.Sharks2[11 + input.Actor.Unit.BodySize + 22 * input.Actor.Unit.BodyAccentType2]);
            }
        }); // Body Pattern

        builder.RenderSingle(SpriteType.BodyAccent4, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType2 == 6)
            {
                return;
            }

            if (!input.Actor.Unit.HasBreasts)
            {
                output.AddOffset(1 * .625f, 0);
            }

            if (input.Actor.Unit.HasWeapon == false)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Sharks2[7 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType2]);
                    return;
                }

                output.Sprite(input.Sprites.Sharks2[4 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType2]);
                return;
            }

            switch (input.Actor.GetWeaponSprite())
            {
                case 0:
                    output.Sprite(input.Sprites.Sharks2[4 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType2]);
                    return;
                case 1:
                    output.Sprite(input.Sprites.Sharks2[5 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType2]);
                    return;
                case 2:
                    output.Sprite(input.Sprites.Sharks2[4 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType2]);
                    return;
                case 3:
                    output.Sprite(input.Sprites.Sharks2[5 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType2]);
                    return;
                case 4:
                    output.Sprite(input.Sprites.Sharks2[4 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType2]);
                    return;
                case 5:
                    output.Sprite(input.Sprites.Sharks2[6 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType2]);
                    return;
                case 6:
                    output.Sprite(input.Sprites.Sharks2[4 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType2]);
                    return;
                case 7:
                    output.Sprite(input.Sprites.Sharks2[5 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType2]);
                    return;
                default:
                    output.Sprite(input.Sprites.Sharks2[4 + 11 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 22 * input.Actor.Unit.BodyAccentType2]);
                    return;
            }
        }); // Arm Pattern

        builder.RenderSingle(SpriteType.BodyAccent5, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType2 == 1)
            {
                output.Sprite(input.Sprites.Sharks2[7 + 11 * input.Actor.Unit.TailType]);
            }
            else if (input.Actor.Unit.BodyAccentType2 == 2)
            {
                output.Sprite(input.Sprites.Sharks2[8 + 11 * input.Actor.Unit.TailType]);
            }
            else if (input.Actor.Unit.BodyAccentType2 == 4)
            {
                output.Sprite(input.Sprites.Sharks2[9 + 11 * input.Actor.Unit.TailType]);
            }
            else if (input.Actor.Unit.BodyAccentType2 == 5)
            {
                output.Sprite(input.Sprites.Sharks2[10 + 11 * input.Actor.Unit.TailType]);
            }
        }); // Tail Pattern

        builder.RenderSingle(SpriteType.BodyAccent6, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.MermenHair, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Sharks3[11]);
        }); // Eyebrow
        builder.RenderSingle(SpriteType.BodyAccessory, 21, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Sharks3[12 + input.Actor.Unit.SpecialAccessoryType]);
        }); // Ears (main)
        builder.RenderSingle(SpriteType.SecondaryAccessory, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Sharks3[24 + input.Actor.Unit.SpecialAccessoryType]);
        }); // Ears (secondary)
        builder.RenderSingle(SpriteType.Breasts, 17, (input, output) =>
        {
            output.Coloring(SharkColor(input.Actor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    output.Sprite(input.Sprites.Sharks4[31]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.Sharks4[30]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.Sharks4[29]);
                    return;
                }

                if (leftSize > 28)
                {
                    leftSize = 28;
                }

                output.Sprite(input.Sprites.Sharks4[0 + leftSize]);
            }
            else
            {
                output.Sprite(input.Sprites.Sharks4[0 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.SecondaryBreasts, 17, (input, output) =>
        {
            output.Coloring(SharkColor(input.Actor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.Sharks4[63]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.Sharks4[62]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.Sharks4[61]);
                    return;
                }

                if (rightSize > 28)
                {
                    rightSize = 28;
                }

                output.Sprite(input.Sprites.Sharks4[32 + rightSize]);
            }
            else
            {
                output.Sprite(input.Sprites.Sharks4[32 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
        {
            output.Coloring(SharkColor(input.Actor));
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(31, 0.7f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Sharks4[99]).AddOffset(0, -29 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Sharks4[98]).AddOffset(0, -29 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                {
                    output.Sprite(input.Sprites.Sharks4[97]).AddOffset(0, -29 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.Sharks4[96]).AddOffset(0, -29 * .625f);
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

                output.Sprite(input.Sprites.Sharks4[64 + size]);
            }
        });

        builder.RenderSingle(SpriteType.Dick, 11, (input, output) =>
        {
            output.Coloring(SharkColor(input.Actor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect())
            {
                if (input.Actor.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32)) < 16)
                {
                    output.Layer(20);
                    if (input.Actor.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.Sharks3[132 + input.Actor.Unit.DickSize]);
                        return;
                    }

                    output.Sprite(input.Sprites.Sharks3[124 + input.Actor.Unit.DickSize]);
                    return;
                }

                output.Layer(13);
                if (input.Actor.IsCockVoring)
                {
                    output.Sprite(input.Sprites.Sharks3[116 + input.Actor.Unit.DickSize]);
                    return;
                }

                output.Sprite(input.Sprites.Sharks3[108 + input.Actor.Unit.DickSize]);
                return;
            }

            output.Sprite(input.Sprites.Sharks3[108 + input.Actor.Unit.DickSize]).Layer(11);
        });

        builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
        {
            output.Coloring(SharkColor(input.Actor));
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
                output.Sprite(input.Sprites.Sharks4[137]).AddOffset(0, -21 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Sharks4[136]).AddOffset(0, -19 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.Sharks4[135]).AddOffset(0, -17 * .625f);
                return;
            }

            if (offset >= 26)
            {
                output.AddOffset(0, -15 * .625f);
            }
            else if (offset == 25)
            {
                output.AddOffset(0, -11 * .625f);
            }
            else if (offset == 24)
            {
                output.AddOffset(0, -9 * .625f);
            }
            else if (offset == 23)
            {
                output.AddOffset(0, -8 * .625f);
            }
            else if (offset == 22)
            {
                output.AddOffset(0, -5 * .625f);
            }
            else if (offset == 21)
            {
                output.AddOffset(0, -4 * .625f);
            }
            else if (offset == 20)
            {
                output.AddOffset(0, -2 * .625f);
            }

            if (offset > 0)
            {
                output.Sprite(input.Sprites.Sharks4[Math.Min(108 + offset, 134)]);
                return;
            }

            output.Sprite(input.Sprites.Sharks4[100 + size]);
        });

        builder.RenderSingle(SpriteType.Weapon, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                if (!input.Actor.Unit.HasBreasts)
                {
                    output.AddOffset(1 * .625f, 0);
                }

                if (input.Actor.GetWeaponSprite() < 4 && input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Sharks3[75 + input.Actor.GetWeaponSprite()]).Layer(20);
                }
                else
                {
                    output.Sprite(input.Sprites.Sharks3[75 + input.Actor.GetWeaponSprite()]).Layer(3);
                }
            }
        });

        builder.RandomCustom(data =>
        {
            Unit unit = data.Unit;
            Defaults.RandomCustom(data);


            unit.AccessoryColor = unit.SkinColor;

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
            unit.TailType = State.Rand.Next(data.MiscRaceData.TailTypes);

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
                unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypes, LeaderClothes);
                unit.ClothingExtraType1 = 3;
            }
            else if (State.Rand.Next(3) == 0)
            {
                unit.ClothingExtraType1 = State.Rand.Next(3);
            }
            else
            {
                unit.ClothingExtraType1 = 0;
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

    private static ColorSwapPalette SharkColor(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType1 == 0)
        {
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkReversed, actor.Unit.SkinColor);
        }

        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, actor.Unit.SkinColor);
    }


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
                    output["Clothing1"].Sprite(input.Sprites.Sharks5[53]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Sharks5[45 + input.Actor.Unit.BreastSize]);
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
                    output["Clothing1"].Sprite(input.Sprites.Sharks5[62]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Sharks5[54 + input.Actor.Unit.BreastSize]);
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
                    output["Clothing1"].Sprite(input.Sprites.Sharks5[71]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Sharks5[63 + input.Actor.Unit.BreastSize]);
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
                    output["Clothing1"].Sprite(input.Sprites.Sharks5[140]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Sharks5[132 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class PirateTop1
    {
        internal static IClothing<IOverSizeParameters> PirateTop1Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Sharks6[68];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 61301;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(18);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing2"].Layer(17);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(16);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize || input.Actor.Unit.BreastSize == 7)
                {
                    output["Clothing1"].Sprite(null);
                    output["Clothing2"].Sprite(input.Sprites.Sharks6[48]);
                    output["Clothing3"].Sprite(input.Sprites.Sharks6[67]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[51 + input.Actor.Unit.BodySize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks7[84 + input.Actor.Unit.BodySize]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Sharks6[48]);
                    output["Clothing3"].Sprite(input.Sprites.Sharks6[60 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    if (input.Actor.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[56 + input.Actor.Unit.BodySize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks7[88 + input.Actor.Unit.BodySize]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Sharks6[49]);
                    output["Clothing3"].Sprite(null);
                }
            });
        });
    }

    private static class PirateTop2
    {
        internal static IClothing<IOverSizeParameters> PirateTop2Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Sharks6[109];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 61302;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(19);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing2"].Layer(15);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize || input.Actor.Unit.BreastSize == 7)
                {
                    if (input.Actor.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[74 + 3 * input.Actor.Unit.BodySize]);
                        output["Clothing1"].Layer(16);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[73 + 3 * input.Actor.Unit.BodySize]);
                        output["Clothing1"].Layer(16);
                    }

                    output["Clothing3"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[74 + 3 * input.Actor.Unit.BodySize]);
                        output["Clothing1"].Layer(18);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[72 + 3 * input.Actor.Unit.BodySize]);
                        output["Clothing1"].Layer(18);
                    }

                    output["Clothing3"].Sprite(input.Sprites.Sharks6[102 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    if (input.Actor.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[86 + 3 * input.Actor.Unit.BodySize]);
                        output["Clothing1"].Layer(18);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[84 + 3 * input.Actor.Unit.BodySize]);
                        output["Clothing1"].Layer(18);
                    }

                    output["Clothing3"].Sprite(null);
                }

                if (input.Actor.Unit.HasWeapon == false)
                {
                    if (input.Actor.IsAttacking)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Sharks6[98 + 3 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.Sharks6[96 + 3 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    }
                }
                else if (input.Actor.GetWeaponSprite() == 5)
                {
                    output["Clothing2"].Sprite(input.Sprites.Sharks6[98 + 3 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                }
                else if (input.Actor.GetWeaponSprite() == 1 || input.Actor.GetWeaponSprite() == 3 || input.Actor.GetWeaponSprite() == 7)
                {
                    output["Clothing2"].Sprite(input.Sprites.Sharks6[97 + 3 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Sharks6[96 + 3 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                }
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
                    output["Clothing1"].Sprite(input.Sprites.Sharks5[116 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Sharks5[112 + input.Actor.Unit.BodySize]);
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
                output["Clothing1"].Sprite(input.Sprites.Sharks5[100 + input.Actor.Unit.BodySize]);
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
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Sharks5[2 + input.Actor.Unit.BreastSize]);
                    output["Clothing2"].Sprite(input.Sprites.Sharks5[1]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Sharks5[0]);
                }

                if (input.Actor.Unit.BodyAccentType1 == 0)
                {
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkReversed, input.Actor.Unit.SkinColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkReversed, input.Actor.Unit.SkinColor));
                }
                else
                {
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, input.Actor.Unit.SkinColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SharkSkin, input.Actor.Unit.SkinColor));
                }
            });
        });
    }

    private static class DemisharkRags
    {
        internal static IClothing DemisharkRagsInstance = ClothingBuilder.Create(builder =>
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
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[129]);
                    }
                    else if (input.Actor.Unit.BreastSize < 6)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[130]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[131]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Sharks5[120 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Sharks5[128]);
                    output["Clothing2"].Sprite(input.Sprites.Sharks5[124 + input.Actor.Unit.BodySize]);
                }
            });
        });
    }

    private static class DemisharkLeader
    {
        internal static IClothing<IOverSizeParameters> DemisharkLeaderInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.LeaderOnly = true;
                output.DiscardSprite = input.Sprites.Sharks6[118];
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
                output.Type = 61303;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing5"].Layer(12);
                output["Clothing5"].Coloring(Color.white);
                output["Clothing4"].Layer(2);
                output["Clothing4"].Coloring(Color.white);
                output["Clothing3"].Layer(19);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing2"].Layer(15);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                if (input.Actor.GetStomachSize(31) > 9)
                {
                    output["Clothing1"].Sprite(null);
                    output["Clothing2"].Sprite(null);
                    output["Clothing3"].Sprite(null);
                    if (input.Actor.Unit.HasBreasts)
                    {
                        output["Clothing5"].Sprite(input.Sprites.Sharks6[36 + input.Actor.Unit.BodySize]);
                    }
                    else
                    {
                        output["Clothing5"].Sprite(input.Sprites.Sharks6[44 + input.Actor.Unit.BodySize]);
                    }
                }
                else if (input.Params.Oversize || input.Actor.Unit.BreastSize == 7)
                {
                    if (input.Actor.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[120 + 2 * input.Actor.Unit.BodySize]);
                        output["Clothing5"].Sprite(input.Sprites.Sharks6[36 + input.Actor.Unit.BodySize]);
                        output["Clothing1"].Layer(16);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[120 + 2 * input.Actor.Unit.BodySize]);
                        output["Clothing5"].Sprite(input.Sprites.Sharks6[32 + input.Actor.Unit.BodySize]);
                        output["Clothing1"].Layer(16);
                    }

                    output["Clothing3"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[120 + 2 * input.Actor.Unit.BodySize]);
                        output["Clothing5"].Sprite(input.Sprites.Sharks6[36 + input.Actor.Unit.BodySize]);
                        output["Clothing1"].Layer(18);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[119 + 2 * input.Actor.Unit.BodySize]);
                        output["Clothing5"].Sprite(input.Sprites.Sharks6[32 + input.Actor.Unit.BodySize]);
                        output["Clothing1"].Layer(18);
                    }

                    output["Clothing3"].Sprite(input.Sprites.Sharks6[110 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    if (input.Actor.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[131 + 2 * input.Actor.Unit.BodySize]);
                        output["Clothing5"].Sprite(input.Sprites.Sharks6[44 + input.Actor.Unit.BodySize]);
                        output["Clothing1"].Layer(18);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[130 + 2 * input.Actor.Unit.BodySize]);
                        output["Clothing5"].Sprite(input.Sprites.Sharks6[40 + input.Actor.Unit.BodySize]);
                        output["Clothing1"].Layer(18);
                    }

                    output["Clothing3"].Sprite(null);
                }

                if (input.Actor.GetStomachSize(31) > 9)
                {
                    output["Clothing2"].Sprite(null);
                }
                else if (input.Actor.Unit.HasWeapon == false)
                {
                    if (input.Actor.IsAttacking)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Sharks6[129 + 11 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.Sharks6[127 + 11 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    }
                }
                else if (input.Actor.GetWeaponSprite() == 5)
                {
                    output["Clothing2"].Sprite(input.Sprites.Sharks6[129 + 11 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                }
                else if (input.Actor.GetWeaponSprite() == 1 || input.Actor.GetWeaponSprite() == 3 || input.Actor.GetWeaponSprite() == 7)
                {
                    output["Clothing2"].Sprite(input.Sprites.Sharks6[128 + 11 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Sharks6[127 + 11 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                }

                if (input.Actor.GetStomachSize(31) > 9)
                {
                    output["Clothing4"].Sprite(null);
                }
                else
                {
                    output["Clothing4"].Sprite(input.Sprites.Sharks6[117]);
                }
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
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[18]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[20]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[19]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing2"].Sprite(input.Sprites.Sharks5[10 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Sharks5[14 + input.Actor.Unit.BodySize]);
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
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[30]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[32]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[31]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Sharks5[29]);
                }

                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing2"].Sprite(input.Sprites.Sharks5[21 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Sharks5[25 + input.Actor.Unit.BodySize]);
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
                output["Clothing1"].Sprite(input.Sprites.Sharks5[33]);

                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing2"].Sprite(input.Sprites.Sharks5[21 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Sharks5[25 + input.Actor.Unit.BodySize]);
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
                output.DiscardSprite = input.Sprites.Avians4[14];
                output.RevealsBreasts = true;
                output.Type = 1514;
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
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[42]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[44]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[43]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing2"].Sprite(input.Sprites.Sharks5[34 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Sharks5[38 + input.Actor.Unit.BodySize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class PirateBot1
    {
        internal static IClothing PirateBot1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Sharks6[69];
                output.RevealsBreasts = true;
                output.Type = 61304;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(12);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Sharks5[33]);

                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[4 + input.Actor.Unit.BodySize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[0 + input.Actor.Unit.BodySize]);
                    }
                }
                else
                {
                    if (input.Actor.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[12 + input.Actor.Unit.BodySize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[8 + input.Actor.Unit.BodySize]);
                    }
                }
            });
        });
    }

    private static class PirateBot2
    {
        internal static IClothing PirateBot2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Sharks6[70];
                output.RevealsBreasts = true;
                output.Type = 61305;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(12);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Sharks5[33]);

                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[20 + input.Actor.Unit.BodySize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[16 + input.Actor.Unit.BodySize]);
                    }
                }
                else
                {
                    if (input.Actor.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[28 + input.Actor.Unit.BodySize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[24 + input.Actor.Unit.BodySize]);
                    }
                }
            });
        });
    }

    private static class PirateBot3
    {
        internal static IClothing PirateBot3Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Sharks6[71];
                output.RevealsBreasts = true;
                output.Type = 61306;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(12);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Sharks5[33]);

                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[36 + input.Actor.Unit.BodySize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[32 + input.Actor.Unit.BodySize]);
                    }
                }
                else
                {
                    if (input.Actor.HasBelly)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[44 + input.Actor.Unit.BodySize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks6[40 + input.Actor.Unit.BodySize]);
                    }
                }
            });
        });
    }

    private static class PirateHat1
    {
        internal static IClothing PirateHat1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardSprite = input.Sprites.Sharks7[4];
                output.Type = 61307;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(1);
                output["Clothing1"].Layer(22);
                if (input.Actor.Unit.HairStyle > 17)
                {
                    output["Clothing1"].Sprite(input.Sprites.Sharks7[2]);
                    output["Clothing2"].Sprite(input.Sprites.Sharks7[3]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Sharks7[0]);
                    output["Clothing2"].Sprite(input.Sprites.Sharks7[1]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class PirateHat2
    {
        internal static IClothing PirateHat2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.DiscardSprite = input.Sprites.Sharks7[7];
                output.Type = 61308;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(1);

                output["Clothing2"].Coloring(Color.white);

                output["Clothing1"].Layer(22);

                output["Clothing1"].Coloring(Color.white);

                output["Clothing1"].Sprite(input.Sprites.Sharks7[5]);
                output["Clothing2"].Sprite(input.Sprites.Sharks7[6]);
            });
        });
    }

    private static class PirateHat3
    {
        internal static IClothing PirateHat3Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.LeaderOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(1);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(22);
                output["Clothing1"].Coloring(Color.white);
                if (input.Actor.Unit.HairStyle > 17)
                {
                    output["Clothing1"].Sprite(input.Sprites.Sharks7[10]);
                    output["Clothing2"].Sprite(input.Sprites.Sharks7[11]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Sharks7[8]);
                    output["Clothing2"].Sprite(input.Sprites.Sharks7[9]);
                }
            });
        });
    }
}
﻿#region

using System;
using UnityEngine;

#endregion

internal static class Demifrogs
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Default<OverSizeParameters>, builder =>
    {
        IClothing<IOverSizeParameters> LeaderClothes = DemifrogLeader.DemifrogLeaderInstance;
        IClothing Rags = DemifrogRags.DemifrogRagsInstance;


        builder.Setup(output =>
        {
            output.DickSizes = () => 8;
            output.BreastSizes = () => 8;

            output.BodySizes = 4;
            output.EyeTypes = 0;
            output.SpecialAccessoryCount = 8; // primary pattern types      
            output.HairStyles = 0;
            output.MouthTypes = 0;
            output.HairColors = 0;
            output.EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemifrogSkin);
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemifrogSkin);
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemifrogSkin); // Secondary pattern Colors
            output.BodyAccentTypes1 = 13; // secondary pattern types
            output.BodyAccentTypes2 = 2; // colored genitals/tits switch

            output.ExtendedBreastSprites = true;

            output.AllowedMainClothingTypes.Set(
                GenericTop1.GenericTop1Instance,
                GenericTop2.GenericTop2Instance,
                GenericTop3.GenericTop3Instance,
                GenericTop4.GenericTop4Instance,
                GenericTop5.GenericTop5Instance,
                GenericTop6.GenericTop6Instance,
                MaleTop.MaleTopInstance,
                MaleTop2.MaleTop2Instance,
                Natural.NaturalInstance,
                Tribal.TribalInstance,
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
                TribalBot.TribalBotInstance
            );

            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
        });


        builder.RunBefore((input, output) =>
        {
            CommonRaceCode.MakeBreastOversize(31 * 31).Invoke(input, output);
            Defaults.BasicBellyRunAfter.Invoke(input, output);
        });

        builder.RenderSingle(SpriteType.Head, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Demifrogs1[11]);
                return;
            }

            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Demifrogs1[10]);
                return;
            }

            output.Sprite(input.Sprites.Demifrogs1[9]);
        }); //Skin

        builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, input.Actor.Unit.EyeColor));
            output.Sprite(input.Sprites.Demifrogs1[8]);
        });
        builder.RenderSingle(SpriteType.Mouth, 21, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Demifrogs1[14]);
            }
            else if (input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Demifrogs1[13]);
            }
            else if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Demifrogs1[12]);
            }
        });

        builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Demifrogs1[0 + (input.Actor.IsAttacking ? 1 : 0) + 2 * input.Actor.Unit.BodySize]);
        }); //Skin

        builder.RenderSingle(SpriteType.BodyAccent, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.SpecialAccessoryType == 7)
            {
                return;
            }

            output.Sprite(input.Sprites.Demifrogs1[15 + (input.Actor.IsEating ? 2 : input.Actor.IsAttacking ? 1 : 0) + 3 * input.Actor.Unit.SpecialAccessoryType]);
        }); // Primary Pattern (head)

        builder.RenderSingle(SpriteType.BodyAccent2, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType1 == 12)
            {
                return;
            }

            output.Sprite(input.Sprites.Demifrogs2[0 + (input.Actor.IsAttacking ? 1 : 0) + 2 * input.Actor.Unit.BodySize + 8 * input.Actor.Unit.BodyAccentType1]);
        }); // Secondary Pattern (body)

        builder.RenderSingle(SpriteType.BodyAccent3, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType1 == 12)
            {
                return;
            }

            output.Sprite(input.Sprites.Demifrogs2[96 + (input.Actor.IsEating ? 2 : input.Actor.IsAttacking ? 1 : 0) + 3 * input.Actor.Unit.BodyAccentType1]);
        }); // Secondary Pattern (head)

        builder.RenderSingle(SpriteType.BodyAccent4, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.SpecialAccessoryType == 6 || input.Actor.Unit.HasDick || input.Actor.Unit.BodyAccentType2 == 1)
            {
                return;
            }

            if (input.Actor.Unit.BodySize > 2)
            {
                output.Sprite(input.Sprites.Demifrogs1[139]);
                return;
            }

            output.Sprite(input.Sprites.Demifrogs1[138]);
        }); // Tertiary Pattern (genitals)

        builder.RenderSingle(SpriteType.BodyAccent5, 18, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.SpecialAccessoryType == 6 || input.Actor.Unit.HasBreasts == false || input.Actor.PredatorComponent?.LeftBreastFullness > 0 || input.Actor.PredatorComponent?.RightBreastFullness > 0 || input.Actor.Unit.BodyAccentType2 == 1)
            {
                return;
            }

            output.Sprite(input.Sprites.Demifrogs2[132 + input.Actor.Unit.BreastSize]);
        }); // Tertiary Pattern (breasts)

        builder.RenderSingle(SpriteType.BodyAccessory, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.SpecialAccessoryType >= 7)
            {
                if (input.Actor.Unit.SpecialAccessoryType > 7)
                {
                    input.Actor.Unit.SpecialAccessoryType = 7;
                }

                return;
            }


            output.Sprite(input.Sprites.Demifrogs1[36 + (input.Actor.IsAttacking ? 1 : 0) + 2 * input.Actor.Unit.BodySize + 8 * input.Actor.Unit.SpecialAccessoryType]);
        }); // Primary Pattern (body)

        builder.RenderSingle(SpriteType.Breasts, 17, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.Unit.SpecialAccessoryType == 6)
            {
                if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
                {
                    int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(31 * 31));

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 31)
                    {
                        output.Sprite(input.Sprites.Demifrogs3alt[30]);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 29)
                    {
                        output.Sprite(input.Sprites.Demifrogs3alt[29]);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 27)
                    {
                        output.Sprite(input.Sprites.Demifrogs3alt[28]);
                        return;
                    }

                    if (leftSize > 27)
                    {
                        leftSize = 27;
                    }

                    output.Sprite(input.Sprites.Demifrogs3alt[0 + leftSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Demifrogs3alt[0 + input.Actor.Unit.BreastSize]);
                }
            }
            else
            {
                if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
                {
                    int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(31 * 31));

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 31)
                    {
                        output.Sprite(input.Sprites.Demifrogs3[30]);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 29)
                    {
                        output.Sprite(input.Sprites.Demifrogs3[29]);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 27)
                    {
                        output.Sprite(input.Sprites.Demifrogs3[28]);
                        return;
                    }

                    if (leftSize > 27)
                    {
                        leftSize = 27;
                    }

                    output.Sprite(input.Sprites.Demifrogs3[0 + leftSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Demifrogs3[0 + input.Actor.Unit.BreastSize]);
                }
            }
        });

        builder.RenderSingle(SpriteType.SecondaryBreasts, 17, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.Unit.SpecialAccessoryType == 6)
            {
                if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
                {
                    int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(31 * 31));

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 31)
                    {
                        output.Sprite(input.Sprites.Demifrogs3alt[61]);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 29)
                    {
                        output.Sprite(input.Sprites.Demifrogs3alt[60]);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 27)
                    {
                        output.Sprite(input.Sprites.Demifrogs3alt[59]);
                        return;
                    }

                    if (rightSize > 27)
                    {
                        rightSize = 27;
                    }

                    output.Sprite(input.Sprites.Demifrogs3alt[31 + rightSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Demifrogs3alt[31 + input.Actor.Unit.BreastSize]);
                }
            }
            else
            {
                if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
                {
                    int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(31 * 31));

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 31)
                    {
                        output.Sprite(input.Sprites.Demifrogs3[61]);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 29)
                    {
                        output.Sprite(input.Sprites.Demifrogs3[60]);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 27)
                    {
                        output.Sprite(input.Sprites.Demifrogs3[59]);
                        return;
                    }

                    if (rightSize > 27)
                    {
                        rightSize = 27;
                    }

                    output.Sprite(input.Sprites.Demifrogs3[31 + rightSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Demifrogs3[31 + input.Actor.Unit.BreastSize]);
                }
            }
        });

        builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(28, 0.6f);

                if (input.Actor.Unit.SpecialAccessoryType == 6)
                {
                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 28)
                    {
                        output.Sprite(input.Sprites.Demifrogs3alt[94]).AddOffset(0, -26 * .625f);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 28)
                    {
                        output.Sprite(input.Sprites.Demifrogs3alt[93]).AddOffset(0, -26 * .625f);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 27)
                    {
                        output.Sprite(input.Sprites.Demifrogs3alt[92]).AddOffset(0, -26 * .625f);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 26)
                    {
                        output.Sprite(input.Sprites.Demifrogs3alt[91]).AddOffset(0, -26 * .625f);
                        return;
                    }

                    switch (size)
                    {
                        case 23:
                            output.AddOffset(0, -5 * .625f);
                            break;
                        case 24:
                            output.AddOffset(0, -7 * .625f);
                            break;
                        case 25:
                            output.AddOffset(0, -12 * .625f);
                            break;
                        case 26:
                            output.AddOffset(0, -16 * .625f);
                            break;
                        case 27:
                            output.AddOffset(0, -20 * .625f);
                            break;
                        case 28:
                            output.AddOffset(0, -25 * .625f);
                            break;
                    }

                    output.Sprite(input.Sprites.Demifrogs3alt[62 + size]);
                }
                else
                {
                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 28)
                    {
                        output.Sprite(input.Sprites.Demifrogs3[94]).AddOffset(0, -26 * .625f);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 28)
                    {
                        output.Sprite(input.Sprites.Demifrogs3[93]).AddOffset(0, -26 * .625f);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 27)
                    {
                        output.Sprite(input.Sprites.Demifrogs3[92]).AddOffset(0, -26 * .625f);
                        return;
                    }

                    if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 26)
                    {
                        output.Sprite(input.Sprites.Demifrogs3[91]).AddOffset(0, -26 * .625f);
                        return;
                    }

                    switch (size)
                    {
                        case 23:
                            output.AddOffset(0, -5 * .625f);
                            break;
                        case 24:
                            output.AddOffset(0, -7 * .625f);
                            break;
                        case 25:
                            output.AddOffset(0, -12 * .625f);
                            break;
                        case 26:
                            output.AddOffset(0, -16 * .625f);
                            break;
                        case 27:
                            output.AddOffset(0, -20 * .625f);
                            break;
                        case 28:
                            output.AddOffset(0, -25 * .625f);
                            break;
                    }

                    output.Sprite(input.Sprites.Demifrogs3[62 + size]);
                }
            }
        });

        builder.RenderSingle(SpriteType.Dick, 11, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect())
            {
                if (input.Actor.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(31 * 31)) < 16 && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(31 * 31)) < 16)
                {
                    output.Layer(20);
                    output.Sprite(input.Actor.IsCockVoring ? input.Sprites.Demifrogs1[108 + input.Actor.Unit.DickSize] : input.Sprites.Demifrogs1[92 + input.Actor.Unit.DickSize]);
                }
                else
                {
                    output.Layer(13);
                    output.Sprite(input.Actor.IsCockVoring ? input.Sprites.Demifrogs1[116 + input.Actor.Unit.DickSize] : input.Sprites.Demifrogs1[100 + input.Actor.Unit.DickSize]);
                }
            }

            //output.Layer(11);
        });

        builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect() && input.Actor.PredatorComponent?.VisibleFullness < .75f &&
                (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize +
                               input.Actor.GetRightBreastSize(32 * 32)) < 16 &&
                (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize +
                               input.Actor.GetLeftBreastSize(32 * 32)) < 16)
            {
                output.Layer(19);
            }
            else
            {
                output.Layer(10);
            }

            int size = input.Actor.Unit.DickSize;
            int offset = input.Actor.GetBallSize(28, .8f);


            if (input.Actor.Unit.SpecialAccessoryType == 6)
            {
                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ??
                     false) && offset == 28)
                {
                    output.Sprite(input.Sprites.Demifrogs3alt[132]).AddOffset(0, -22 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ??
                     false) && offset == 28)
                {
                    output.Sprite(input.Sprites.Demifrogs3alt[131]).AddOffset(0, -20 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ??
                     false) && offset == 27)
                {
                    output.Sprite(input.Sprites.Demifrogs3alt[130]).AddOffset(0, -18 * .625f);
                    return;
                }

                if (offset >= 26)
                {
                    output.AddOffset(0, -16 * .625f);
                }
                else if (offset == 25)
                {
                    output.AddOffset(0, -12 * .625f);
                }
                else if (offset == 24)
                {
                    output.AddOffset(0, -10 * .625f);
                }
                else if (offset == 23)
                {
                    output.AddOffset(0, -9 * .625f);
                }
                else if (offset == 22)
                {
                    output.AddOffset(0, -6 * .625f);
                }
                else if (offset == 21)
                {
                    output.AddOffset(0, -5 * .625f);
                }
                else if (offset == 20)
                {
                    output.AddOffset(0, -3 * .625f);
                }

                if (offset > 0)
                {
                    output.Sprite(input.Sprites.Demifrogs3alt[Math.Min(103 + offset, 129)]);
                    return;
                }

                output.Sprite(input.Sprites.Demifrogs3alt[95 + size]);
            }
            else
            {
                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ??
                     false) && offset == 28)
                {
                    output.Sprite(input.Sprites.Demifrogs3[132]).AddOffset(0, -22 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ??
                     false) && offset == 28)
                {
                    output.Sprite(input.Sprites.Demifrogs3[131]).AddOffset(0, -20 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ??
                     false) && offset == 27)
                {
                    output.Sprite(input.Sprites.Demifrogs3[130]).AddOffset(0, -18 * .625f);
                    return;
                }

                if (offset >= 26)
                {
                    output.AddOffset(0, -16 * .625f);
                }
                else if (offset == 25)
                {
                    output.AddOffset(0, -12 * .625f);
                }
                else if (offset == 24)
                {
                    output.AddOffset(0, -10 * .625f);
                }
                else if (offset == 23)
                {
                    output.AddOffset(0, -9 * .625f);
                }
                else if (offset == 22)
                {
                    output.AddOffset(0, -6 * .625f);
                }
                else if (offset == 21)
                {
                    output.AddOffset(0, -5 * .625f);
                }
                else if (offset == 20)
                {
                    output.AddOffset(0, -3 * .625f);
                }

                if (offset > 0)
                {
                    output.Sprite(input.Sprites.Demifrogs3[Math.Min(103 + offset, 129)]);
                    return;
                }

                output.Sprite(input.Sprites.Demifrogs3[95 + size]);
            }
        });

        builder.RenderSingle(SpriteType.Weapon, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                switch (input.Actor.GetWeaponSprite())
                {
                    case 0:
                        if (input.Actor.Unit.BodySize == 3)
                        {
                            output.Sprite(input.Sprites.Demifrogs1[125]);
                            return;
                        }

                        output.Sprite(input.Sprites.Demifrogs1[124]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Demifrogs1[126]);
                        return;
                    case 2:
                        if (input.Actor.Unit.BodySize == 3)
                        {
                            output.Sprite(input.Sprites.Demifrogs1[128]);
                            return;
                        }

                        output.Sprite(input.Sprites.Demifrogs1[127]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Demifrogs1[129]);
                        return;
                    case 4:
                        if (input.Actor.Unit.BodySize == 3)
                        {
                            output.Sprite(input.Sprites.Demifrogs1[131]);
                            return;
                        }

                        output.Sprite(input.Sprites.Demifrogs1[130]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.Demifrogs1[132]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Demifrogs1[133 + input.Actor.Unit.BodySize]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.Demifrogs1[137]);
                        return;
                    default:
                        return;
                }
            }
        });

        builder.RandomCustom(data =>
        {
            Unit unit = data.Unit;
            Defaults.RandomCustom(data);


            unit.AccessoryColor = unit.SkinColor;

            if (State.Rand.Next(10) == 0)
            {
                unit.SpecialAccessoryType = data.MiscRaceData.SpecialAccessoryCount - 1;
            }
            else
            {
                unit.SpecialAccessoryType = State.Rand.Next(data.MiscRaceData.SpecialAccessoryCount - 1);
            }

            if (State.Rand.Next(3) == 0)
            {
                unit.BodyAccentType1 = data.MiscRaceData.BodyAccentTypes1 - 1;
            }
            else
            {
                unit.BodyAccentType1 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes1 - 1);
            }

            unit.BodyAccentType2 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes2);

            if (unit.Type == UnitType.Leader)
            {
                unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypes, LeaderClothes);
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
        internal static readonly IClothing<IOverSizeParameters> GenericTop1Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
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
                output["Clothing1"].Layer(19);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demifrogs4[56]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demifrogs4[48 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop2
    {
        internal static readonly IClothing<IOverSizeParameters> GenericTop2Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
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
                output["Clothing1"].Layer(19);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demifrogs4[65]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demifrogs4[57 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop3
    {
        internal static readonly IClothing<IOverSizeParameters> GenericTop3Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
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
                output["Clothing1"].Layer(19);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demifrogs4[74]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demifrogs4[66 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop4
    {
        internal static readonly IClothing<IOverSizeParameters> GenericTop4Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
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
                output["Clothing1"].Layer(19);
                output["Clothing2"].Layer(19);
                output["Clothing2"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demifrogs4[83]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demifrogs4[75 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing2"].Sprite(input.Sprites.Demifrogs4[84]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop5
    {
        internal static readonly IClothing<IOverSizeParameters> GenericTop5Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
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
                output["Clothing1"].Layer(19);
                output["Clothing2"].Layer(19);
                output["Clothing2"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demifrogs4[93]);
                    output["Clothing2"].Sprite(input.Sprites.Demifrogs4[102]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demifrogs4[85 + input.Actor.Unit.BreastSize]);
                    output["Clothing2"].Sprite(input.Sprites.Demifrogs4[94 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericTop6
    {
        internal static readonly IClothing<IOverSizeParameters> GenericTop6Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
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
                output["Clothing1"].Layer(19);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demifrogs4[107 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
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
                output.Type = 1579;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(19);

                output["Clothing1"].Sprite(input.Actor.HasBelly ? input.Sprites.Demifrogs4[119 + input.Actor.Unit.BodySize] : input.Sprites.Demifrogs4[115 + input.Actor.Unit.BodySize]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
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
                output.Type = 1579;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(19);
                output["Clothing1"].Sprite(input.Sprites.Demifrogs4[103 + input.Actor.Unit.BodySize]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class Natural
    {
        internal static readonly IClothing<IOverSizeParameters> NaturalInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
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
                output["Clothing1"].Layer(19);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Actor.Unit.SpecialAccessoryType == 6 ? input.Sprites.Demifrogs4[12 + input.Actor.Unit.BreastSize] : input.Sprites.Demifrogs4[2 + input.Actor.Unit.BreastSize]);
                }

                if (input.Actor.Unit.BodySize > 2)
                {
                    output["Clothing2"].Sprite(input.Actor.Unit.SpecialAccessoryType == 6 ? input.Sprites.Demifrogs4[11] : input.Sprites.Demifrogs4[1]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Actor.Unit.SpecialAccessoryType == 6 ? input.Sprites.Demifrogs4[10] : input.Sprites.Demifrogs4[0]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, input.Actor.Unit.SkinColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemifrogSkin, input.Actor.Unit.SkinColor));
            });
        });
    }

    private static class Tribal
    {
        internal static readonly IClothing<IOverSizeParameters> TribalInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Demifrogs4[143];
                output.RevealsBreasts = true;
                output.Type = 1176;
                output.OccupiesAllSlots = true;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(12);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(19);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demifrogs4[138]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demifrogs4[131 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing2"].Sprite(input.Sprites.Demifrogs4[139 + input.Actor.Unit.BodySize]);
            });
        });
    }

    private static class DemifrogRags
    {
        internal static readonly IClothing DemifrogRagsInstance = ClothingBuilder.Create(builder =>
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
                output["Clothing1"].Layer(19);
                output["Clothing1"].Coloring(Color.white);
                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.Unit.BreastSize < 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[127]);
                    }
                    else if (input.Actor.Unit.BreastSize < 6)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[128]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[129]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Demifrogs4[130]);
                }

                output["Clothing2"].Sprite(input.Sprites.Demifrogs4[123 + input.Actor.Unit.BodySize]);
            });
        });
    }

    private static class DemifrogLeader
    {
        internal static readonly IClothing<IOverSizeParameters> DemifrogLeaderInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.LeaderOnly = true;
                output.DiscardSprite = input.Sprites.Demifrogs5[20];
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
                output.Type = 1177;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing6"].Layer(9);
                output["Clothing6"].Coloring(Color.white);
                output["Clothing5"].Layer(0);
                output["Clothing5"].Coloring(Color.white);
                output["Clothing4"].Layer(4);
                output["Clothing4"].Coloring(Color.white);
                output["Clothing3"].Layer(19);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing2"].Layer(20);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(12);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Demifrogs5[0 + input.Actor.Unit.BodySize]);
                output["Clothing2"].Sprite(input.Sprites.Demifrogs5[4]);
                output["Clothing4"].Sprite(input.Sprites.Demifrogs5[10 + (input.Actor.IsAttacking ? 1 : 0) + 2 * input.Actor.Unit.BodySize]);
                output["Clothing5"].Sprite(input.Sprites.Demifrogs5[18]);
                output["Clothing6"].Sprite(input.Sprites.Demifrogs5[19]);

                if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Params.Oversize)
                    {
                        output["Clothing3"].Sprite(null);
                    }
                    else if (input.Actor.Unit.BreastSize < 3)
                    {
                        output["Clothing3"].Sprite(null);
                    }
                    else
                    {
                        output["Clothing3"].Sprite(input.Sprites.Demifrogs5[2 + input.Actor.Unit.BreastSize]);
                    }
                }
                else
                {
                    output["Clothing3"].Sprite(null);
                }
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
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[24]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[26]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[25]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                output["Clothing2"].Sprite(input.Sprites.Demifrogs4[20 + input.Actor.Unit.BodySize]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
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
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[32]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[34]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[33]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Demifrogs4[31]);
                }

                output["Clothing2"].Sprite(input.Sprites.Demifrogs4[27 + input.Actor.Unit.BodySize]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
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
                output.Type = 1540;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(13);
                output["Clothing2"].Layer(12);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Demifrogs4[35]);
                output["Clothing2"].Sprite(input.Sprites.Demifrogs4[27 + input.Actor.Unit.BodySize]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericBot4
    {
        internal static readonly IClothing GenericBot4Instance = ClothingBuilder.Create(builder =>
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
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[45]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[47]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[46]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                output["Clothing2"].Sprite(input.Sprites.Demifrogs4[41 + input.Actor.Unit.BodySize]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class TribalBot
    {
        internal static readonly IClothing TribalBotInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Demifrogs4[40];
                output.RevealsBreasts = true;
                output.Type = 1178;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(12);

                output["Clothing1"].Coloring(Color.white);

                output["Clothing1"].Sprite(input.Sprites.Demifrogs4[36 + input.Actor.Unit.BodySize]);
            });
        });
    }
}
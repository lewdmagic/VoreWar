#region

using System;
using UnityEngine;

#endregion

internal static class Cockatrice
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Default<OverSizeParameters>, builder =>
    {
        builder.Setup(output =>
        {
            output.DickSizes = () => 8;
            output.BreastSizes = () => 8;
            output.BodySizes = 4;
            output.EyeTypes = 12;
            output.SpecialAccessoryCount = 0;
            output.HairStyles = 24;
            output.MouthTypes = 6;
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.CockatriceSkin); // Feather Colors
            output.HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.CockatriceSkin);
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.CockatriceSkin);

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
                Cuirass.CuirassInstance
            );
            output.AvoidedMainClothingTypes = 0;
            output.AvoidedEyeTypes = 0;
            output.AllowedWaistTypes.Set(
                GenericBot1.GenericBot1Instance,
                GenericBot2.GenericBot2Instance,
                GenericBot3.GenericBot3Instance,
                GenericBot4.GenericBot4Instance
            );

            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
        });


        builder.RunBefore((input, output) =>
        {
            CommonRaceCode.MakeBreastOversize(32 * 32).Invoke(input, output);

            if (input.Actor.HasBelly)
            {
                output.changeSprite(SpriteType.Belly).SetActive(true).SetLocalScale(new Vector3(1, 1, 1));
            }
        });

        builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CockatriceSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Cockatrice1[54] : input.Sprites.Cockatrice1[55]);
        }); // Head - skin

        builder.RenderSingle(SpriteType.Eyes, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            output.Sprite(input.Sprites.Cockatrice1[72 + input.Actor.Unit.EyeType]);
        });
        builder.RenderSingle(SpriteType.Mouth, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CockatriceSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Cockatrice1[62 + 2 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                return;
            }

            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Cockatrice1[63 + 2 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                return;
            }

            output.Sprite(input.Sprites.Cockatrice1[66 + input.Actor.Unit.MouthType]);
        });

        builder.RenderSingle(SpriteType.Hair, 20, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CockatriceSkin, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Cockatrice1[96 + input.Actor.Unit.HairStyle]);
        });
        builder.RenderSingle(SpriteType.Hair2, 0, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CockatriceSkin, input.Actor.Unit.HairColor));
            if (input.Actor.Unit.HairStyle > 13)
            {
            }
            else
            {
                output.Sprite(input.Sprites.Cockatrice1[120 + input.Actor.Unit.HairStyle]);
            }
        });

        builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CockatriceSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Cockatrice1[0 + input.Actor.Unit.BodySize] : input.Sprites.Cockatrice1[4 + input.Actor.Unit.BodySize]);
        }); // Body - skin

        builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Cockatrice1[8 + input.Actor.Unit.BodySize] : input.Sprites.Cockatrice1[12 + input.Actor.Unit.BodySize]);
        }); // Body - scales

        builder.RenderSingle(SpriteType.BodyAccent2, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CockatriceSkin, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Cockatrice1[16 + input.Actor.Unit.BodySize] : input.Sprites.Cockatrice1[20 + input.Actor.Unit.BodySize]);
        }); // Legs - feathers

        builder.RenderSingle(SpriteType.BodyAccent3, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CockatriceSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.HasWeapon == false)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Cockatrice1[25 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                }

                output.Sprite(input.Sprites.Cockatrice1[24 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                return;
            }

            switch (input.Actor.GetWeaponSprite())
            {
                case 0:
                    output.Sprite(input.Sprites.Cockatrice1[25 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 1:
                    output.Sprite(input.Sprites.Cockatrice1[26 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 2:
                    output.Sprite(input.Sprites.Cockatrice1[24 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 3:
                    output.Sprite(input.Sprites.Cockatrice1[25 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 4:
                    output.Sprite(input.Sprites.Cockatrice1[24 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 5:
                    output.Sprite(input.Sprites.Cockatrice1[25 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 6:
                    output.Sprite(input.Sprites.Cockatrice1[24 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 7:
                    output.Sprite(input.Sprites.Cockatrice1[25 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                default:
                    output.Sprite(input.Sprites.Cockatrice1[24 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
            }
        }); // Arms - feathers

        builder.RenderSingle(SpriteType.BodyAccent4, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon == false)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Cockatrice1[37 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                }

                output.Sprite(input.Sprites.Cockatrice1[36 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                return;
            }

            switch (input.Actor.GetWeaponSprite())
            {
                case 0:
                    output.Sprite(input.Sprites.Cockatrice1[37 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 1:
                    output.Sprite(input.Sprites.Cockatrice1[38 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 2:
                    output.Sprite(input.Sprites.Cockatrice1[36 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 3:
                    output.Sprite(input.Sprites.Cockatrice1[37 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 4:
                    output.Sprite(input.Sprites.Cockatrice1[36 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 5:
                    output.Sprite(input.Sprites.Cockatrice1[37 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 6:
                    output.Sprite(input.Sprites.Cockatrice1[36 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 7:
                    output.Sprite(input.Sprites.Cockatrice1[37 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                default:
                    output.Sprite(input.Sprites.Cockatrice1[36 + 3 * (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 6 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
            }
        }); // Arms - scales

        builder.RenderSingle(SpriteType.BodyAccent5, 2, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Cockatrice1[48 + (input.Actor.Unit.BodySize > 1 ? 1 : 0) + 2 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
        }); // Legs - scales
        builder.RenderSingle(SpriteType.BodyAccent6, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CockatriceSkin, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Cockatrice1[52]);
        }); // Tail - feathers
        builder.RenderSingle(SpriteType.BodyAccent7, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Cockatrice1[53]);
        }); // Tail - scales
        builder.RenderSingle(SpriteType.BodyAccent8, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Cockatrice1[57 + 3 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                return;
            }

            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Cockatrice1[58 + 3 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                return;
            }

            output.Sprite(input.Sprites.Cockatrice1[56 + 3 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
        }); // Head - scales

        builder.RenderSingle(SpriteType.BodyAccent9, 2, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon == false)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Cockatrice1[145 + 4 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                }

                output.Sprite(input.Sprites.Cockatrice1[143 + 4 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                return;
            }

            switch (input.Actor.GetWeaponSprite())
            {
                case 0:
                    output.Sprite(input.Sprites.Cockatrice1[145 + 4 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 1:
                    output.Sprite(input.Sprites.Cockatrice1[146 + 4 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 2:
                    output.Sprite(input.Sprites.Cockatrice1[144 + 4 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 3:
                    output.Sprite(input.Sprites.Cockatrice1[145 + 4 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 4:
                    output.Sprite(input.Sprites.Cockatrice1[144 + 4 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 5:
                    output.Sprite(input.Sprites.Cockatrice1[145 + 4 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 6:
                    output.Sprite(input.Sprites.Cockatrice1[144 + 4 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                case 7:
                    output.Sprite(input.Sprites.Cockatrice1[145 + 4 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
                default:
                    output.Sprite(input.Sprites.Cockatrice1[144 + 4 * (!input.Actor.Unit.HasBreasts ? 1 : 0)]);
                    return;
            }
        }); // Hands - scales

        builder.RenderSingle(SpriteType.BodyAccent10, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CockatriceSkin, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Cockatrice1[84 + input.Actor.Unit.EyeType]);
        }); // Eyebrows
        builder.RenderSingle(SpriteType.BodyAccessory, 21, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Cockatrice1[134]);
        });
        builder.RenderSingle(SpriteType.Breasts, 17, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CockatriceSkin, input.Actor.Unit.SkinColor));
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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CockatriceSkin, input.Actor.Unit.SkinColor));
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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CockatriceSkin, input.Actor.Unit.SkinColor));
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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CockatriceSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            output.AddOffset(0, 1 * .625f);

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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CockatriceSkin, input.Actor.Unit.SkinColor));
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
                output.Sprite(input.Sprites.Cockatrice2[137]).AddOffset(0, -19 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Cockatrice2[136]).AddOffset(0, -19 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.Cockatrice2[135]).AddOffset(0, -19 * .625f);
                return;
            }

            if (offset >= 26)
            {
                output.AddOffset(0, -19 * .625f);
            }
            else if (offset == 25)
            {
                output.AddOffset(0, -10 * .625f);
            }
            else if (offset == 24)
            {
                output.AddOffset(0, -8 * .625f);
            }
            else if (offset == 23)
            {
                output.AddOffset(0, -7 * .625f);
            }
            else if (offset == 22)
            {
                output.AddOffset(0, -4 * .625f);
            }
            else if (offset == 21)
            {
                output.AddOffset(0, -3 * .625f);
            }
            else if (offset == 20)
            {
                output.AddOffset(0, -1 * .625f);
            }

            if (offset > 0)
            {
                output.Sprite(input.Sprites.Cockatrice2[Math.Min(108 + offset, 134)]);
                return;
            }

            output.Sprite(input.Sprites.Cockatrice2[100 + size]);
        });

        builder.RenderSingle(SpriteType.Weapon, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                output.Sprite(input.Sprites.Cockatrice1[135 + input.Actor.GetWeaponSprite()]);
                if (!input.Actor.Unit.HasBreasts)
                {
                    output.AddOffset(2 * .625f, 0);
                }
            }
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;


            unit.AccessoryColor = unit.SkinColor;
            unit.HairColor = unit.AccessoryColor;

            if (unit.HasDick && unit.HasBreasts)
            {
                unit.HairStyle = State.Rand.Next(Config.HermsOnlyUseFemaleHair ? 18 : data.MiscRaceData.HairStyles);
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
                    unit.HairStyle = 12 + State.Rand.Next(12);
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(18);
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
        internal static readonly IClothing<IOverSizeParameters> GenericTop7Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
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
                output["Clothing1"].Layer(18);

                output["Clothing1"].Sprite(input.Actor.HasBelly ? input.Sprites.Cockatrice3[83 + input.Actor.Unit.BodySize] : input.Sprites.Cockatrice3[79 + input.Actor.Unit.BodySize]);

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
                output["Clothing1"].Layer(18);
                output["Clothing1"].Sprite(input.Sprites.Cockatrice3[75 + input.Actor.Unit.BodySize]);
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
                output["Clothing1"].Layer(18);
                output["Clothing2"].Layer(7);
                output["Clothing2"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(null);
                    output["Clothing2"].SetOffset(0, 0);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Cockatrice3[4 + input.Actor.Unit.BreastSize]);
                    output["Clothing2"].SetOffset(0, 0);
                }
                else
                {
                    output["Clothing2"].SetOffset(0, -1 * .625f);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.CockatriceSkin, input.Actor.Unit.SkinColor));
                output["Clothing2"].Sprite(input.Sprites.Cockatrice3[0 + input.Actor.Unit.BodySize]);
            });
        });
    }

    private static class Cuirass
    {
        internal static readonly IClothing<IOverSizeParameters> CuirassInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Cockatrice3[120];
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
                output.FixedColor = true;
                output.Type = 61601;
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
                    output["Clothing1"].Sprite(input.Sprites.Cockatrice3[100]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.Unit.BreastSize < 2)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[96]);
                    }
                    else if (input.Actor.Unit.BreastSize < 4)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[97]);
                    }
                    else if (input.Actor.Unit.BreastSize < 6)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[98]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[99]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Cockatrice3[119]);
                }

                if (input.Actor.HasBelly)
                {
                    output["Clothing2"].Sprite(null);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Cockatrice3[109 + input.Actor.Unit.BodySize] : input.Sprites.Cockatrice3[113 + input.Actor.Unit.BodySize]);
                }

                output["Clothing3"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Cockatrice3[101 + input.Actor.Unit.BodySize] : input.Sprites.Cockatrice3[105 + input.Actor.Unit.BodySize]);

                output["Clothing4"].Sprite(input.Actor.GetWeaponSprite() == 1 ? input.Sprites.Cockatrice3[118] : input.Sprites.Cockatrice3[117]);
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

                output["Clothing2"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Cockatrice3[12 + input.Actor.Unit.BodySize] : input.Sprites.Cockatrice3[16 + input.Actor.Unit.BodySize]);

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

                output["Clothing2"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Cockatrice3[23 + input.Actor.Unit.BodySize] : input.Sprites.Cockatrice3[27 + input.Actor.Unit.BodySize]);

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
                output["Clothing1"].Sprite(input.Sprites.Cockatrice3[35]);

                output["Clothing2"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Cockatrice3[23 + input.Actor.Unit.BodySize] : input.Sprites.Cockatrice3[27 + input.Actor.Unit.BodySize]);

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

                output["Clothing2"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Cockatrice3[36 + input.Actor.Unit.BodySize] : input.Sprites.Cockatrice3[40 + input.Actor.Unit.BodySize]);
            });
        });
    }
}
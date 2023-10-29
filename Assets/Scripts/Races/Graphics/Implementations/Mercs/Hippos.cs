#region

using System;
using UnityEngine;

#endregion

internal static class Hippos
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Default<OverSizeParameters>, builder =>
    {
        builder.Setup(output =>
        {
            output.DickSizes = () => 8;
            output.BreastSizes = () => 8;

            output.BodySizes = 5;
            output.HairStyles = 0;
            output.EyeTypes = 5;
            output.SpecialAccessoryCount = 8; // ears  
            output.HairStyles = 0;
            output.MouthTypes = 0;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.HippoSkin);
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.HippoSkin); // tattoo/warpaint colors
            output.BodyAccentTypes1 = 6; // left arm tattoo/warpaint
            output.BodyAccentTypes2 = 6; // right arm tattoo/warpaint
            output.BodyAccentTypes3 = 6; // head tattoo/warpaint
            output.BodyAccentTypes4 = 6; // legs tattoo/warpaint
            output.ClothingColors = 0;

            output.ExtendedBreastSprites = true;

            output.AllowedMainClothingTypes.Set(
                HipposTop1.HipposTop1Instance,
                HipposTop2.HipposTop2Instance,
                HipposTop3.HipposTop3Instance,
                Natural.NaturalInstance
            );
            output.AllowedWaistTypes.Set(
                HipposBot1.HipposBot1Instance,
                HipposBot2.HipposBot2Instance,
                HipposBot3.HipposBot3Instance,
                HipposBot4.HipposBot4Instance
            );
            output.AllowedClothingHatTypes.Set(
                HipposHeadband1.HipposHeadband1Instance,
                HipposHeadband2.HipposHeadband2Instance,
                HipposHeadband3.HipposHeadband3Instance,
                HipposHeadband4.HipposHeadband4Instance,
                HipposHeadband5.HipposHeadband5Instance,
                HipposHeadband6.HipposHeadband6Instance,
                HipposHeadband7.HipposHeadband7Instance,
                HipposHeadband8.HipposHeadband8Instance
            );
            output.AllowedClothingAccessoryTypes.Set(
                HipposNecklace1.HipposNecklace1Instance,
                HipposNecklace2.HipposNecklace2Instance,
                HipposNecklace3.HipposNecklace3Instance,
                HipposNecklace4.HipposNecklace4Instance,
                HipposNecklace5.HipposNecklace5Instance,
                HipposNecklace6.HipposNecklace6Instance,
                HipposNecklace7.HipposNecklace7Instance,
                HipposNecklace8.HipposNecklace8Instance
            );
            output.AvoidedMainClothingTypes = 0;
            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.HippoSkin);
        });


        builder.RunBefore((input, output) =>
        {
            CommonRaceCode.MakeBreastOversize(32 * 32).Invoke(input, output);
            Defaults.Finalize.Invoke(input, output);
        });

        builder.RenderSingle(SpriteType.Head, 22, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Hippos[21]);
                return;
            }

            output.Sprite(input.Sprites.Hippos[20]);
        });

        builder.RenderSingle(SpriteType.Eyes, 25, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            output.Sprite(input.Sprites.Hippos[30 + (input.Actor.IsEating ? 1 : 0) + 2 * input.Actor.Unit.EyeType]);
        });
        builder.RenderSingle(SpriteType.Body, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Sprites.Hippos[0 + (input.Actor.IsAttacking ? 1 : 0) + 2 * input.Actor.Unit.BodySize]);
            }
            else
            {
                output.Sprite(input.Sprites.Hippos[10 + (input.Actor.IsAttacking ? 1 : 0) + 2 * input.Actor.Unit.BodySize]);
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Hippos2[0 + (input.Actor.IsAttacking ? 1 : 0) + 2 * input.Actor.Unit.BodyAccentType1]);
        }); // left arm tattoo/warpaint
        builder.RenderSingle(SpriteType.BodyAccent2, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Hippos2[12 + input.Actor.Unit.BodyAccentType2]);
        }); // right arm tattoo/warpaint
        builder.RenderSingle(SpriteType.BodyAccent3, 23, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Hippos2[18 + (input.Actor.IsEating ? 1 : 0) + 2 * input.Actor.Unit.BodyAccentType3]);
        }); // head tattoo/warpaint
        builder.RenderSingle(SpriteType.BodyAccent4, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Hippos2[30 + input.Actor.Unit.BodyAccentType4]);
        }); // legs tattoo/warpaint
        builder.RenderSingle(SpriteType.BodyAccent5, 24, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Hippos[40 + (input.Actor.IsEating ? 1 : 0) + 2 * input.Actor.Unit.EyeType]);
        }); // eyebrows
        builder.RenderSingle(SpriteType.BodyAccent6, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Hippos[120 + (input.Actor.IsAttacking ? 1 : 0)]);
        }); // left arm
        builder.RenderSingle(SpriteType.BodyAccessory, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Hippos[22 + input.Actor.Unit.SpecialAccessoryType]);
        }); // ears
        builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    output.Sprite(input.Sprites.Hippos3[31]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.Hippos3[30]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.Hippos3[29]);
                    return;
                }

                if (leftSize > 28)
                {
                    leftSize = 28;
                }

                output.Sprite(input.Sprites.Hippos3[0 + leftSize]);
            }
            else
            {
                output.Sprite(input.Sprites.Hippos3[0 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.SecondaryBreasts, 16, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.Hippos3[63]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.Hippos3[62]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.Hippos3[61]);
                    return;
                }

                if (rightSize > 28)
                {
                    rightSize = 28;
                }

                output.Sprite(input.Sprites.Hippos3[32 + rightSize]);
            }
            else
            {
                output.Sprite(input.Sprites.Hippos3[32 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(26, 0.7f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 26)
                {
                    output.Sprite(input.Sprites.Hippos3[94]).AddOffset(0, -9 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 26)
                {
                    output.Sprite(input.Sprites.Hippos3[93]).AddOffset(0, -9 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 25)
                {
                    output.Sprite(input.Sprites.Hippos3[92]).AddOffset(0, -9 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 24)
                {
                    output.Sprite(input.Sprites.Hippos3[91]).AddOffset(0, -9 * .625f);
                    return;
                }

                switch (size)
                {
                    case 24:
                        output.AddOffset(0, -3 * .625f);
                        break;
                    case 25:
                        output.AddOffset(0, -6 * .625f);
                        break;
                    case 26:
                        output.AddOffset(0, -9 * .625f);
                        break;
                }

                output.Sprite(input.Sprites.Hippos3[64 + size]);
            }
        });

        builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect())
            {
                if (input.Actor.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32)) < 16)
                {
                    output.Layer(18);
                    if (input.Actor.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.Hippos[82 + input.Actor.Unit.DickSize]);
                        return;
                    }

                    output.Sprite(input.Sprites.Hippos[66 + input.Actor.Unit.DickSize]);
                    return;
                }

                output.Layer(12);
                if (input.Actor.IsCockVoring)
                {
                    output.Sprite(input.Sprites.Hippos[50 + input.Actor.Unit.DickSize]);
                    return;
                }

                output.Sprite(input.Sprites.Hippos[58 + input.Actor.Unit.DickSize]);
                return;
            }

            output.Sprite(input.Sprites.Hippos[58 + input.Actor.Unit.DickSize]).Layer(9);
        });

        builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect() && input.Actor.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32)) < 16)
            {
                output.Layer(17);
            }
            else
            {
                output.Layer(8);
            }

            int baseSize = (input.Actor.Unit.DickSize + 1) / 3;
            int ballOffset = input.Actor.GetBallSize(26, .9f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && ballOffset == 26)
            {
                output.Sprite(input.Sprites.Hippos[119]).AddOffset(0, -26 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && ballOffset == 26)
            {
                output.Sprite(input.Sprites.Hippos[118]).AddOffset(0, -21 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && ballOffset >= 24)
            {
                output.Sprite(input.Sprites.Hippos[117]).AddOffset(0, -17 * .625f);
                return;
            }

            int combined = Math.Min(baseSize + ballOffset + 2, 26);
            if (combined == 26)
            {
                output.AddOffset(0, -13 * .625f);
            }
            else if (combined == 25)
            {
                output.AddOffset(0, -8 * .625f);
            }
            else if (combined == 24)
            {
                output.AddOffset(0, -4 * .625f);
            }

            if (ballOffset > 0)
            {
                output.Sprite(input.Sprites.Hippos[90 + combined]);
                return;
            }

            output.Sprite(input.Sprites.Hippos[90 + baseSize]);
        });

        builder.RenderSingle(SpriteType.Weapon, 2, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                output.Sprite(input.Sprites.Hippos[74 + input.Actor.GetWeaponSprite()]);
            }
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            if (State.Rand.Next(5) == 0)
            {
                unit.BodyAccentType1 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes1 - 1);
            }
            else
            {
                unit.BodyAccentType1 = data.MiscRaceData.BodyAccentTypes1 - 1;
            }

            if (State.Rand.Next(5) == 0)
            {
                unit.BodyAccentType2 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes2 - 1);
            }
            else
            {
                unit.BodyAccentType2 = data.MiscRaceData.BodyAccentTypes2 - 1;
            }

            if (State.Rand.Next(5) == 0)
            {
                unit.BodyAccentType3 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes3 - 1);
            }
            else
            {
                unit.BodyAccentType3 = data.MiscRaceData.BodyAccentTypes3 - 1;
            }

            if (State.Rand.Next(5) == 0)
            {
                unit.BodyAccentType4 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes4 - 1);
            }
            else
            {
                unit.BodyAccentType4 = data.MiscRaceData.BodyAccentTypes4 - 1;
            }
        });
    });


    private static class HipposTop1
    {
        internal static IClothing<IOverSizeParameters> HipposTop1Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Hippos2[60];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 84260;
                output.FixedColor = true;
            });
            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Hippos2[52 + input.Actor.Unit.BreastSize]);
                }
            });
        });
    }

    private static class HipposTop2
    {
        internal static IClothing<IOverSizeParameters> HipposTop2Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Hippos2[87];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 84287;
                output.FixedColor = true;
            });
            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Hippos2[79 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Hippos2[103]);
                }
            });
        });
    }

    private static class HipposTop3
    {
        internal static IClothing<IOverSizeParameters> HipposTop3Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Hippos2[96];
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 84296;
                output.FixedColor = true;
            });
            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Hippos2[88 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Hippos2[104]);
                }
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
                output["Clothing2"].Layer(10);
                output["Clothing1"].Layer(17);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Hippos3[96 + input.Actor.Unit.BreastSize]);
                    output["Clothing2"].Sprite(input.Sprites.Hippos3[95]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, input.Actor.Unit.SkinColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, input.Actor.Unit.SkinColor));
            });
        });
    }

    private static class HipposBot1
    {
        internal static IClothing HipposBot1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Hippos2[66];
                output.RevealsBreasts = true;
                output.Type = 84266;
                output.FixedColor = true;
            });
            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Hippos2[61 + input.Actor.Unit.BodySize]);
            });
        });
    }

    private static class HipposBot2
    {
        internal static IClothing HipposBot2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Hippos2[72];
                output.RevealsBreasts = true;
                output.Type = 84272;
                output.FixedColor = true;
            });
            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Hippos2[67 + input.Actor.Unit.BodySize]);
            });
        });
    }

    private static class HipposBot3
    {
        internal static IClothing HipposBot3Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Hippos2[78];
                output.RevealsBreasts = true;
                output.Type = 84278;
                output.FixedColor = true;
            });
            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Hippos2[73 + input.Actor.Unit.BodySize]);
            });
        });
    }

    private static class HipposBot4
    {
        internal static IClothing HipposBot4Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Hippos2[102];
                output.RevealsBreasts = true;
                output.Type = 84302;
                output.FixedColor = true;
            });
            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Hippos2[97 + input.Actor.Unit.BodySize]);
            });
        });
    }

    private static class HipposHeadband1
    {
        internal static IClothing HipposHeadband1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(26);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Hippos2[44]);
            });
        });
    }

    private static class HipposHeadband2
    {
        internal static IClothing HipposHeadband2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(26);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Hippos2[45]);
            });
        });
    }

    private static class HipposHeadband3
    {
        internal static IClothing HipposHeadband3Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(26);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Hippos2[46]);
            });
        });
    }

    private static class HipposHeadband4
    {
        internal static IClothing HipposHeadband4Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(26);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Hippos2[47]);
            });
        });
    }

    private static class HipposHeadband5
    {
        internal static IClothing HipposHeadband5Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(26);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Hippos2[48]);
            });
        });
    }

    private static class HipposHeadband6
    {
        internal static IClothing HipposHeadband6Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(26);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Hippos2[49]);
            });
        });
    }

    private static class HipposHeadband7
    {
        internal static IClothing HipposHeadband7Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(26);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Hippos2[50]);
            });
        });
    }

    private static class HipposHeadband8
    {
        internal static IClothing HipposHeadband8Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(26);
                output["Clothing1"].Sprite(input.Sprites.Hippos2[51]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class HipposNecklace1
    {
        internal static IClothing HipposNecklace1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(21);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Hippos2[36]);
            });
        });
    }

    private static class HipposNecklace2
    {
        internal static IClothing HipposNecklace2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(21);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Hippos2[37]);
            });
        });
    }

    private static class HipposNecklace3
    {
        internal static IClothing HipposNecklace3Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(21);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Hippos2[38]);
            });
        });
    }

    private static class HipposNecklace4
    {
        internal static IClothing HipposNecklace4Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(21);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Hippos2[39]);
            });
        });
    }

    private static class HipposNecklace5
    {
        internal static IClothing HipposNecklace5Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(21);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Hippos2[40]);
            });
        });
    }

    private static class HipposNecklace6
    {
        internal static IClothing HipposNecklace6Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(21);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Hippos2[41]);
            });
        });
    }

    private static class HipposNecklace7
    {
        internal static IClothing HipposNecklace7Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(21);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Hippos2[42]);
            });
        });
    }

    private static class HipposNecklace8
    {
        internal static IClothing HipposNecklace8Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(21);
                output["Clothing1"].Sprite(input.Sprites.Hippos2[43]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HippoSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }
}
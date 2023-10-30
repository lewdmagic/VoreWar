#region

using System;
using UnityEngine;

#endregion

internal static class Komodos
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Default<OverSizeParameters>, builder =>
    {
        builder.Setup(output =>
        {
            output.DickSizes = () => 8;
            output.BreastSizes = () => 8;

            output.BodySizes = 4;
            output.EyeTypes = 6;
            output.SpecialAccessoryCount = 5; // body patterns    
            output.HairStyles = 0;
            output.MouthTypes = 0;
            output.HairColors = 0;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.KomodosSkin);
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.KomodosSkin);
            output.BodyAccentTypes1 = 11; // head shapes
            output.BodyAccentTypes2 = 6; // secondary body pattern
            output.BodyAccentTypes3 = 2; // head pattern on/off

            output.ExtendedBreastSprites = true;

            output.AllowedClothingHatTypes.Clear();


            output.AllowedMainClothingTypes.Set(
                GenericTop1.GenericTop1Instance,
                GenericTop2.GenericTop2Instance,
                GenericTop3.GenericTop3Instance,
                GenericTop4.GenericTop4Instance,
                GenericTop5.GenericTop5Instance,
                Natural.NaturalInstance,
                Tribal.TribalInstance
            );
            output.AvoidedMainClothingTypes = 0;
            output.AvoidedEyeTypes = 0;
            output.AllowedWaistTypes.Set(
                GenericBot1.GenericBot1Instance,
                GenericBot2.GenericBot2Instance,
                GenericBot3.GenericBot3Instance
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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.SpecialAccessoryType == 4)
            {
                if (input.Actor.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Komodos1[61]);
                    return;
                }

                if (input.Actor.IsAttacking || input.Actor.IsEating)
                {
                    output.Sprite(input.Sprites.Komodos1[58]);
                    return;
                }

                output.Sprite(input.Sprites.Komodos1[55]);
            }
            else if (input.Actor.Unit.SpecialAccessoryType == 3)
            {
                if (input.Actor.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Komodos1[59]);
                    return;
                }

                if (input.Actor.IsAttacking || input.Actor.IsEating)
                {
                    output.Sprite(input.Sprites.Komodos1[56]);
                    return;
                }

                output.Sprite(input.Sprites.Komodos1[53]);
            }
            else
            {
                if (input.Actor.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Komodos1[60]);
                    return;
                }

                if (input.Actor.IsAttacking || input.Actor.IsEating)
                {
                    output.Sprite(input.Sprites.Komodos1[57]);
                    return;
                }

                output.Sprite(input.Sprites.Komodos1[54]);
            }
        });

        builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            output.Sprite(input.Sprites.Komodos1[65 + input.Actor.Unit.EyeType]);

            if (input.Actor.IsOralVoring)
            {
                output.AddOffset(0, 1 * .625f);
            }
        });

        builder.RenderSingle(SpriteType.Mouth, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Komodos1[63]);
                return;
            }

            if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Komodos1[62]);
            }
        });

        builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
            int sat = Mathf.Clamp(input.Actor.Unit.SpecialAccessoryType, 0, 4); //Protect against out of bounds from other unit types.  
            output.Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Komodos1[0 + input.Actor.Unit.BodySize + 10 * sat] : input.Sprites.Komodos1[4 + input.Actor.Unit.BodySize + 10 * sat]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Actor.IsAttacking ? input.Sprites.Komodos1[9 + 10 * input.Actor.Unit.SpecialAccessoryType] : input.Sprites.Komodos1[8 + 10 * input.Actor.Unit.SpecialAccessoryType]);
        }); // Right Arm

        builder.RenderSingle(SpriteType.BodyAccent2, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Komodos1[64]);
        }); // Toenails
        builder.RenderSingle(SpriteType.BodyAccent3, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.BodyAccentType1 == 10)
            {
            }
            else if (input.Actor.Unit.SpecialAccessoryType == 3)
            {
                output.Sprite(input.Sprites.Komodos3[10 + input.Actor.Unit.BodyAccentType1]);
            }
            else
            {
                output.Sprite(input.Sprites.Komodos3[0 + input.Actor.Unit.BodyAccentType1]);
            }
        }); // Head Shape

        builder.RenderSingle(SpriteType.BodyAccent4, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType2 == 5)
            {
            }
            else
            {
                output.Sprite(input.Sprites.Komodos3[20 + input.Actor.Unit.BodySize + 10 * input.Actor.Unit.BodyAccentType2]);
            }
        }); // Body Secondary Pattern

        builder.RenderSingle(SpriteType.BodyAccent5, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType3 == 1 || input.Actor.Unit.BodyAccentType2 == 5)
            {
            }
            else if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Komodos3[29 + 10 * input.Actor.Unit.BodyAccentType2]);
            }
            else if (input.Actor.IsAttacking || input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Komodos3[28 + 10 * input.Actor.Unit.BodyAccentType2]);
            }
            else
            {
                output.Sprite(input.Sprites.Komodos3[27 + 10 * input.Actor.Unit.BodyAccentType2]);
            }
        }); // Head Secondary Pattern

        builder.RenderSingle(SpriteType.BodyAccent6, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType2 == 5)
            {
            }
            else
            {
                output.Sprite(input.Sprites.Komodos3[26 + 10 * input.Actor.Unit.BodyAccentType2]);
            }
        }); // Tail Secondary Pattern

        builder.RenderSingle(SpriteType.BodyAccent7, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.BodyAccentType2 == 5)
            {
            }
            else if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Komodos3[25 + 10 * input.Actor.Unit.BodyAccentType2]);
            }
            else
            {
                output.Sprite(input.Sprites.Komodos3[24 + 10 * input.Actor.Unit.BodyAccentType2]);
            }
        }); // Right Arm Secondary Pattern

        builder.RenderSingle(SpriteType.BodyAccessory, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.SpecialAccessoryType == 4)
            {
                output.Sprite(input.Sprites.Komodos1[52]);
            }
            else if (input.Actor.Unit.SpecialAccessoryType == 3)
            {
                output.Sprite(input.Sprites.Komodos1[50]);
            }
            else
            {
                output.Sprite(input.Sprites.Komodos1[51]);
            }
        }); // Tail

        builder.RenderSingle(SpriteType.Breasts, 17, (input, output) =>
        {
            output.Coloring(KomodoColor(input.Actor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.LeftBreastFullness > 0)
            {
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(30 * 30));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.Komodos2[29]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.Komodos2[28]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 26)
                {
                    output.Sprite(input.Sprites.Komodos2[27]);
                    return;
                }

                if (leftSize > 26)
                {
                    leftSize = 26;
                }

                output.Sprite(input.Sprites.Komodos2[0 + leftSize]);
            }
            else
            {
                output.Sprite(input.Sprites.Komodos2[0 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.SecondaryBreasts, 17, (input, output) =>
        {
            output.Coloring(KomodoColor(input.Actor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.PredatorComponent?.RightBreastFullness > 0)
            {
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(30 * 30));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.Komodos2[59]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.Komodos2[58]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 26)
                {
                    output.Sprite(input.Sprites.Komodos2[57]);
                    return;
                }

                if (rightSize > 26)
                {
                    rightSize = 26;
                }

                output.Sprite(input.Sprites.Komodos2[30 + rightSize]);
            }
            else
            {
                output.Sprite(input.Sprites.Komodos2[30 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
        {
            output.Coloring(KomodoColor(input.Actor));
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(26, 0.7f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 26)
                {
                    output.Sprite(input.Sprites.Komodos2[90]).AddOffset(0, -12 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 26)
                {
                    output.Sprite(input.Sprites.Komodos2[89]).AddOffset(0, -12 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 25)
                {
                    output.Sprite(input.Sprites.Komodos2[88]).AddOffset(0, -12 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 24)
                {
                    output.Sprite(input.Sprites.Komodos2[87]).AddOffset(0, -12 * .625f);
                    return;
                }

                switch (size)
                {
                    case 24:
                        output.AddOffset(0, -4 * .625f);
                        break;
                    case 25:
                        output.AddOffset(0, -9 * .625f);
                        break;
                    case 26:
                        output.AddOffset(0, -12 * .625f);
                        break;
                }

                output.Sprite(input.Sprites.Komodos2[60 + size]);
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
                    output.Sprite(input.Actor.IsCockVoring ? input.Sprites.Komodos1[79 + input.Actor.Unit.DickSize] : input.Sprites.Komodos1[71 + input.Actor.Unit.DickSize]);
                }
                else
                {
                    output.Layer(13);
                    output.Sprite(input.Actor.IsCockVoring ? input.Sprites.Komodos1[95 + input.Actor.Unit.DickSize] : input.Sprites.Komodos1[87 + input.Actor.Unit.DickSize]);
                }
            }

            //output.Layer(11); 
        });

        builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
        {
            output.Coloring(KomodoColor(input.Actor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect() && input.Actor.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(30 * 30)) < 16 && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(30 * 30)) < 16)
            {
                output.Layer(19);
            }
            else
            {
                output.Layer(10);
            }

            int size = input.Actor.Unit.DickSize;
            int offset = input.Actor.GetBallSize(27, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.Komodos2[127]).AddOffset(0, -27 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.Komodos2[126]).AddOffset(0, -22 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 26)
            {
                output.Sprite(input.Sprites.Komodos2[125]).AddOffset(0, -18 * .625f);
                return;
            }

            if (offset >= 25)
            {
                output.AddOffset(0, -13 * .625f);
            }
            else if (offset == 24)
            {
                output.AddOffset(0, -8 * .625f);
            }
            else if (offset == 23)
            {
                output.AddOffset(0, -3 * .625f);
            }

            if (offset > 0)
            {
                output.Sprite(input.Sprites.Komodos2[Math.Min(99 + offset, 124)]);
                return;
            }

            output.Sprite(input.Sprites.Komodos2[91 + size]);
        });

        builder.RenderSingle(SpriteType.Weapon, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                switch (input.Actor.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Komodos1[103]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Komodos1[104]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Komodos1[105]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Komodos1[106]);
                        return;
                    default:
                        return;
                }
            }
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;


            unit.AccessoryColor = unit.SkinColor;

            if (State.Rand.Next(8) == 0)
            {
                unit.SpecialAccessoryType = 3 + State.Rand.Next(2);
            }
            else
            {
                unit.SpecialAccessoryType = State.Rand.Next(data.MiscRaceData.SpecialAccessoryCount - 2);
            }

            if (State.Rand.Next(2) == 0)
            {
                unit.BodyAccentType2 = data.MiscRaceData.BodyAccentTypes2 - 1;
            }
            else
            {
                unit.BodyAccentType2 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes2 - 1);
            }

            unit.BodyAccentType1 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes1);
            unit.BodyAccentType3 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes3);
        });
    });

    private static ColorSwapPalette KomodoColor(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType == 4)
        {
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosReversed, actor.Unit.SkinColor);
        }

        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, actor.Unit.SkinColor);
    }


    private static class GenericTop1
    {
        internal static readonly IClothing<IOverSizeParameters> GenericTop1Instance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Komodos4[48];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 61401;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Komodos4[47]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Komodos4[39 + input.Actor.Unit.BreastSize]);
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
                output.DiscardSprite = input.Sprites.Komodos4[58];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 61402;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Komodos4[57]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Komodos4[49 + input.Actor.Unit.BreastSize]);
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
                output.DiscardSprite = input.Sprites.Komodos4[68];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 61403;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                output["Clothing2"].Layer(18);
                output["Clothing2"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Komodos4[67]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Komodos4[59 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing2"].Sprite(input.Sprites.Komodos4[69]);
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
                output.DiscardSprite = input.Sprites.Komodos4[79];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 61404;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                output["Clothing2"].Layer(18);
                output["Clothing2"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(null);
                    output["Clothing2"].Sprite(input.Sprites.Komodos4[78]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Komodos4[80 + input.Actor.Unit.BreastSize]);
                    output["Clothing2"].Sprite(input.Sprites.Komodos4[70 + input.Actor.Unit.BreastSize]);
                }

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
                output.DiscardSprite = input.Sprites.Komodos4[97];
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.Type = 61405;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Komodos4[96]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Komodos4[88 + input.Actor.Unit.BreastSize]);
                }
                
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
                output["Clothing1"].Layer(18);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(null);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Komodos4[1 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing2"].Sprite(input.Sprites.Komodos4[0]);

                if (input.Actor.Unit.SpecialAccessoryType == 4)
                {
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosReversed, input.Actor.Unit.SkinColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosReversed, input.Actor.Unit.SkinColor));
                }
                else
                {
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.KomodosSkin, input.Actor.Unit.SkinColor));
                }
            });
        });
    }

    private static class Tribal
    {
        internal static readonly IClothing<IOverSizeParameters> TribalInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Komodos4[38];
                output.RevealsBreasts = true;
                output.Type = 61406;
                output.OccupiesAllSlots = true;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(12);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Komodos4[37]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Komodos4[29 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing2"].Sprite(input.Sprites.Komodos4[25 + input.Actor.Unit.BodySize]);
            });
        });
    }

    private static class GenericBot1
    {
        internal static readonly IClothing GenericBot1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Komodos4[9];
                output.RevealsBreasts = true;
                output.Type = 61407;
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
                        output["Clothing1"].Sprite(input.Sprites.Komodos4[15]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Komodos4[17]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Komodos4[16]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Komodos4[14]);
                }

                output["Clothing2"].Sprite(input.Sprites.Komodos4[10 + input.Actor.Unit.BodySize]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }

    private static class GenericBot2
    {
        internal static readonly IClothing GenericBot2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Komodos4[19];
                output.RevealsBreasts = true;
                output.Type = 61408;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(13);
                output["Clothing2"].Layer(12);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Komodos4[18]);
                output["Clothing2"].Sprite(input.Sprites.Komodos4[10 + input.Actor.Unit.BodySize]);

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
                output.DiscardSprite = input.Sprites.Komodos4[24];
                output.RevealsBreasts = true;
                output.Type = 61409;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(12);

                output["Clothing1"].Coloring(Color.white);

                output["Clothing1"].Sprite(input.Sprites.Komodos4[20 + input.Actor.Unit.BodySize]);
            });
        });
    }
}
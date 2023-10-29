#region

using System;
using UnityEngine;

#endregion

internal static class Ants
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Default<OverSizeParameters>, builder =>
    {
        IClothing Rags = DemiantRags.DemiantRagsInstance;


        builder.Setup(output =>
        {
            output.DickSizes = () => 8;
            output.BreastSizes = () => 8;

            output.BodySizes = 3;
            output.EyeTypes = 8;
            output.SpecialAccessoryCount = 12; // antennae        
            output.HairStyles = 24;
            output.MouthTypes = 3;
            output.EyeColors = 0;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemiantSkin);
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DemiantSkin);

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
                Cuirass.CuirassInstance,
                Rags
            );
            output.AvoidedMainClothingTypes = 1;
            output.AvoidedEyeTypes = 0;
            output.AllowedWaistTypes.Set(
                GenericBot1.GenericBot1Instance,
                GenericBot2.GenericBot2Instance,
                GenericBot3.GenericBot3Instance,
                GenericBot4.GenericBot4Instance
            );

            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AviansSkin);
        });


        builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Sprites.Demiants1[0 + input.Actor.Unit.BodySize]);
            }
            else
            {
                output.Sprite(input.Sprites.Demiants1[3 + input.Actor.Unit.BodySize]);
            }
        }); // Upper Body (White)

        builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.Demiants1[24 + input.Actor.Unit.EyeType]);
        });
        builder.RenderSingle(SpriteType.Mouth, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Demiants1[32]);
                return;
            }

            output.Sprite(input.Sprites.Demiants1[33 + input.Actor.Unit.MouthType]);
        });

        builder.RenderSingle(SpriteType.Hair, 18, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Demiants1[60 + input.Actor.Unit.HairStyle]);
        });
        builder.RenderSingle(SpriteType.Body, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Sprites.Demiants1[6 + input.Actor.Unit.BodySize]);
            }
            else
            {
                output.Sprite(input.Sprites.Demiants1[9 + input.Actor.Unit.BodySize]);
            }
        }); // Lower Body (black)

        builder.RenderSingle(SpriteType.BodyAccent, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Demiants1[18 + input.Actor.Unit.BodySize]);
        }); // Abdomen 2 (White)
        builder.RenderSingle(SpriteType.BodyAccent2, 19, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Sprites.Demiants1[36 + input.Actor.Unit.SpecialAccessoryType]);
            }
            else
            {
                output.Sprite(input.Sprites.Demiants1[48 + input.Actor.Unit.SpecialAccessoryType]);
            }
        }); // Antennae (black)

        builder.RenderSingle(SpriteType.BodyAccent3, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.HasWeapon == false)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Demiants1[17]);
                    return;
                }

                output.Sprite(input.Sprites.Demiants1[124]);
                return;
            }

            switch (input.Actor.GetWeaponSprite())
            {
                case 0:
                    output.Sprite(input.Sprites.Demiants1[16]);
                    return;
                case 1:
                    output.Sprite(input.Sprites.Demiants1[17]);
                    return;
                case 2:
                    output.Sprite(input.Sprites.Demiants1[124]);
                    return;
                case 3:
                    output.Sprite(input.Sprites.Demiants1[125]);
                    return;
                case 4:
                    output.Sprite(input.Sprites.Demiants1[15]);
                    return;
                case 5:
                    output.Sprite(input.Sprites.Demiants1[16]);
                    return;
                case 6:
                    output.Sprite(input.Sprites.Demiants1[15]);
                    return;
                case 7:
                    output.Sprite(input.Sprites.Demiants1[16]);
                    return;
                default:
                    output.Sprite(input.Sprites.Demiants1[124]);
                    return;
            }
        }); // Upper Front Arms (black)

        builder.RenderSingle(SpriteType.BodyAccent4, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.HasWeapon == false)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Demiants1[14]);
                    return;
                }

                output.Sprite(input.Sprites.Demiants1[14]);
                return;
            }

            switch (input.Actor.GetWeaponSprite())
            {
                case 0:
                    output.Sprite(input.Sprites.Demiants1[12]);
                    return;
                case 1:
                    output.Sprite(input.Sprites.Demiants1[12]);
                    return;
                case 2:
                    output.Sprite(input.Sprites.Demiants1[13]);
                    return;
                case 3:
                    output.Sprite(input.Sprites.Demiants1[14]);
                    return;
                case 4:
                    output.Sprite(input.Sprites.Demiants1[14]);
                    return;
                case 5:
                    output.Sprite(input.Sprites.Demiants1[14]);
                    return;
                case 6:
                    output.Sprite(input.Sprites.Demiants1[14]);
                    return;
                case 7:
                    output.Sprite(input.Sprites.Demiants1[14]);
                    return;
                default:
                    output.Sprite(input.Sprites.Demiants1[14]);
                    return;
            }
        }); // Lower Back Arms (black)

        builder.RenderSingle(SpriteType.BodyAccessory, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Demiants1[21 + input.Actor.Unit.BodySize]);
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
                int leftSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32));


                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.leftBreast) && leftSize >= 32)
                {
                    output.Sprite(input.Sprites.Demiants2[31]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 30)
                {
                    output.Sprite(input.Sprites.Demiants2[30]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.leftBreast) && leftSize >= 28)
                {
                    output.Sprite(input.Sprites.Demiants2[29]);
                    return;
                }

                if (leftSize > 28)
                {
                    leftSize = 28;
                }

                output.Sprite(input.Sprites.Demiants2[0 + leftSize]);
            }
            else
            {
                output.Sprite(input.Sprites.Demiants2[0 + input.Actor.Unit.BreastSize]);
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
                int rightSize = (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32));

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.rightBreast) && rightSize >= 32)
                {
                    output.Sprite(input.Sprites.Demiants2[63]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 30)
                {
                    output.Sprite(input.Sprites.Demiants2[62]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.rightBreast) && rightSize >= 28)
                {
                    output.Sprite(input.Sprites.Demiants2[61]);
                    return;
                }

                if (rightSize > 28)
                {
                    rightSize = 28;
                }

                output.Sprite(input.Sprites.Demiants2[32 + rightSize]);
            }
            else
            {
                output.Sprite(input.Sprites.Demiants2[32 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly)
            {
                int size = input.Actor.GetStomachSize(31, 0.8f);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Demiants2[99]).AddOffset(0, -32 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 31)
                {
                    output.Sprite(input.Sprites.Demiants2[98]).AddOffset(0, -32 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 30)
                {
                    output.Sprite(input.Sprites.Demiants2[97]).AddOffset(0, -32 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && size == 29)
                {
                    output.Sprite(input.Sprites.Demiants2[96]).AddOffset(0, -32 * .625f);
                    return;
                }

                switch (size)
                {
                    case 26:
                        output.AddOffset(0, -13 * .625f);
                        break;
                    case 27:
                        output.AddOffset(0, -17 * .625f);
                        break;
                    case 28:
                        output.AddOffset(0, -20 * .625f);
                        break;
                    case 29:
                        output.AddOffset(0, -24 * .625f);
                        break;
                    case 30:
                        output.AddOffset(0, -27 * .625f);
                        break;
                    case 31:
                        output.AddOffset(0, -32 * .625f);
                        break;
                }

                output.Sprite(input.Sprites.Demiants2[64 + size]);
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
                if (input.Actor.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.Actor.Unit.DefaultBreastSize * input.Actor.Unit.DefaultBreastSize + input.Actor.GetLeftBreastSize(32 * 32)) < 16)
                {
                    output.Layer(20);
                    if (input.Actor.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.Demiants1[100 + input.Actor.Unit.DickSize]);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.Demiants1[84 + input.Actor.Unit.DickSize]);
                    }
                }
                else
                {
                    output.Layer(13);
                    if (input.Actor.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.Demiants1[108 + input.Actor.Unit.DickSize]);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.Demiants1[92 + input.Actor.Unit.DickSize]);
                    }
                }
            }

            //output.Layer(11); why was this here in original code TODO
        });

        builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.SkinColor));
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
                output.Sprite(input.Sprites.Demiants2[137]).AddOffset(0, -21 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 28)
            {
                output.Sprite(input.Sprites.Demiants2[136]).AddOffset(0, -19 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls) ?? false) && offset == 27)
            {
                output.Sprite(input.Sprites.Demiants2[135]).AddOffset(0, -17 * .625f);
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
                output.Sprite(input.Sprites.Demiants2[Math.Min(108 + offset, 134)]);
                return;
            }

            output.Sprite(input.Sprites.Demiants2[100 + size]);
        }); // Balls (white)

        builder.RenderSingle(SpriteType.Weapon, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                output.Sprite(input.Sprites.Demiants1[116 + input.Actor.GetWeaponSprite()]);
            }
        });


        builder.RunBefore((input, output) =>
        {
            CommonRaceCode.MakeBreastOversize(32 * 32).Invoke(input, output);
            Defaults.BasicBellyRunAfter.Invoke(input, output);
        });

        builder.RandomCustom(data =>
        {
            Unit unit = data.Unit;
            Defaults.RandomCustom(data);


            unit.AccessoryColor = unit.SkinColor;

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
                    unit.HairStyle = 12 + State.Rand.Next(12);
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(18);
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
        internal static IClothing<OverSizeParameters> GenericTop1Instance = ClothingBuilder.Create<OverSizeParameters>(builder =>
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
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[46]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[38 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }


    private static class GenericTop1V2
    {
        internal static IClothing<OverSizeParameters> GenericTop1_V2 = ClothingBuilder.Create<OverSizeParameters>(builder =>
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
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[46]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[38 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });

        internal static IClothing<OverSizeParameters> GenericTop1Instance = ClothingBuilder.Create<OverSizeParameters>(builder =>
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
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[46]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[38 + input.Actor.Unit.BreastSize]);
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
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[55]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[47 + input.Actor.Unit.BreastSize]);
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
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[64]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[56 + input.Actor.Unit.BreastSize]);
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
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[73]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[65 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing2"].Sprite(input.Sprites.Demiants3[74]);
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
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[83]);
                    output["Clothing2"].Sprite(input.Sprites.Demiants3[92]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[75 + input.Actor.Unit.BreastSize]);
                    output["Clothing2"].Sprite(input.Sprites.Demiants3[84 + input.Actor.Unit.BreastSize]);
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
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[96 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(null);
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
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[107 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[104 + input.Actor.Unit.BodySize]);
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
                output["Clothing1"].Sprite(input.Sprites.Demiants3[93 + input.Actor.Unit.BodySize]);
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
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[0 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                output["Clothing2"].Sprite(input.Sprites.Demiants3[8]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.SkinColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DemiantSkin, input.Actor.Unit.SkinColor));
            });
        });
    }

    private static class Cuirass
    {
        internal static IClothing<IOverSizeParameters> CuirassInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Demiants3[138];
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
                output.FixedColor = true;
                output.Type = 195;
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
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[124]);
                }
                else if (input.Actor.Unit.HasBreasts)
                {
                    if (input.Actor.Unit.BreastSize < 2)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[120]);
                    }
                    else if (input.Actor.Unit.BreastSize < 4)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[121]);
                    }
                    else if (input.Actor.Unit.BreastSize < 6)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[122]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[123]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[125]);
                }

                if (input.Actor.HasBelly)
                {
                    output["Clothing2"].Sprite(null);
                }
                else
                {
                    if (input.Actor.Unit.HasBreasts)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Demiants3[126 + input.Actor.Unit.BodySize]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.Demiants3[129 + input.Actor.Unit.BodySize]);
                    }
                }

                output["Clothing3"].Sprite(input.Sprites.Demiants3[132 + input.Actor.Unit.BodySize]);

                if (input.Actor.GetWeaponSprite() == 1)
                {
                    output["Clothing4"].Sprite(input.Sprites.Demiants3[136]);
                }
                else if (input.Actor.GetWeaponSprite() == 3)
                {
                    output["Clothing4"].Sprite(input.Sprites.Demiants3[137]);
                }
                else
                {
                    output["Clothing4"].Sprite(input.Sprites.Demiants3[135]);
                }
            });
        });
    }

    private static class DemiantRags
    {
        internal static IClothing DemiantRagsInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Rags[23];
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
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[117]);
                    }
                    else if (input.Actor.Unit.BreastSize < 6)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[118]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[119]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Demiants3[110 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[116]);
                    output["Clothing2"].Sprite(input.Sprites.Demiants3[113 + input.Actor.Unit.BodySize]);
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
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[15]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[17]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[16]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing2"].Sprite(input.Sprites.Demiants3[9 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Demiants3[12 + input.Actor.Unit.BodySize]);
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
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[25]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[27]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[26]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[24]);
                }

                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing2"].Sprite(input.Sprites.Demiants3[18 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Demiants3[21 + input.Actor.Unit.BodySize]);
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
                output["Clothing1"].Sprite(input.Sprites.Demiants3[28]);

                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing2"].Sprite(input.Sprites.Demiants3[18 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Demiants3[21 + input.Actor.Unit.BodySize]);
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
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[35]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[37]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[36]);
                    }
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing2"].Sprite(input.Sprites.Demiants3[29 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Demiants3[32 + input.Actor.Unit.BodySize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AviansSkin, input.Actor.Unit.ClothingColor));
            });
        });
    }
}
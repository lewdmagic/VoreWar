﻿#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.MainRaces
{
    internal static class Demifrogs
    {
        private static Func<IClothingRenderInput, IOverSizeParameters> _paramsCalc = CommonRaceCode.MakeOversizeFunc(31 * 31);

        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            IClothing leaderClothes = DemifrogLeader.DemifrogLeaderInstance.Create(_paramsCalc);
            IClothing rags = DemifrogRags.DemifrogRagsInstance;


            builder.Setup((input, output) =>
            {
                output.Names("DemiFrog", "DemiFrogs");
                output.FlavorText(new FlavorText(
                    new Texts { },
                    new Texts { },
                    new Texts { "demi-frog", "amphibian", "frog" }, //new, many thanks to Flame_Valxsarion
                    new Dictionary<string, string>
                    {
                        [WeaponNames.Mace] = "WeaponNames.Mace",
                        [WeaponNames.Axe] = "WeaponNames.Axe",
                        [WeaponNames.SimpleBow] = "Slingshot",
                        [WeaponNames.CompoundBow] = "Feathered Bow",
                        [WeaponNames.Claw] = "Fist"
                    }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 14,
                    StomachSize = 18,
                    HasTail = false,
                    FavoredStat = Stat.Voracity,
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Pounce,
                        TraitType.HeavyPounce,
                        TraitType.RangedVore,
                        TraitType.Clumsy
                    },
                    RaceDescription = "",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.BodyAccessoryType, "Primary Pattern Type");
                    buttons.SetText(ButtonType.BodyAccentTypes1, "Secondary Pattern Type");
                    buttons.SetText(ButtonType.BodyAccentTypes2, "Extra Colors for Females");
                    buttons.SetText(ButtonType.BodyAccessoryColor, "Secondary Pattern Colors");
                });
                output.TownNames(new List<string>
                {
                    "Frogpolis",
                    "Buzzing Bog",
                    "Lily Pond",
                    "Evermire",
                    "Green Lake",
                    "Toadburg",
                    "Muddy Pool",
                    "Flycatcher Swamp",
                    "Tadpole City",
                    "Misty Tarn",
                    "Froppyville",
                    "Willow Wetlands",
                    "Loch Bufo",
                    "Murky Puddle",
                    "Soggy Marsh",
                    "Backwater Borough",
                    "Sawgrass Slough"
                });
                output.DickSizes = () => 8;
                output.BreastSizes = () => 8;

                output.BodySizes = 4;
                output.EyeTypes = 0;
                output.SpecialAccessoryCount = 8; // primary pattern types      
                output.HairStyles = 0;
                output.MouthTypes = 0;
                output.HairColors = 0;
                output.EyeColors = ColorPaletteMap.GetPaletteCount(SwapType.DemifrogSkin);
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.DemifrogSkin);
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.DemifrogSkin); // Secondary pattern Colors
                output.BodyAccentTypes1 = 13; // secondary pattern types
                output.BodyAccentTypes2 = 2; // colored genitals/tits switch

                output.ExtendedBreastSprites = true;

                output.AllowedMainClothingTypes.Set(
                    GenericTop1.GenericTop1Instance.Create(_paramsCalc),
                    GenericTop2.GenericTop2Instance.Create(_paramsCalc),
                    GenericTop3.GenericTop3Instance.Create(_paramsCalc),
                    GenericTop4.GenericTop4Instance.Create(_paramsCalc),
                    GenericTop5.GenericTop5Instance.Create(_paramsCalc),
                    GenericTop6.GenericTop6Instance.Create(_paramsCalc),
                    MaleTop.MaleTopInstance,
                    MaleTop2.MaleTop2Instance,
                    Natural.NaturalInstance.Create(_paramsCalc),
                    Tribal.TribalInstance.Create(_paramsCalc),
                    rags,
                    leaderClothes
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

                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.AviansSkin);
            });


            builder.RunBefore((input, output) => { Defaults.BasicBellyRunAfter.Invoke(input, output); });

            builder.RenderSingle(SpriteType.Head, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemifrogSkin, input.U.SkinColor));
                if (input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Demifrogs1[11]);
                    return;
                }

                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Demifrogs1[10]);
                    return;
                }

                output.Sprite(input.Sprites.Demifrogs1[9]);
            }); //Skin

            builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemifrogSkin, input.U.EyeColor));
                output.Sprite(input.Sprites.Demifrogs1[8]);
            });
            builder.RenderSingle(SpriteType.Mouth, 21, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Demifrogs1[14]);
                }
                else if (input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Demifrogs1[13]);
                }
                else if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Demifrogs1[12]);
                }
            });

            builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemifrogSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Demifrogs1[0 + (input.A.IsAttacking ? 1 : 0) + 2 * input.U.BodySize]);
            }); //Skin

            builder.RenderSingle(SpriteType.BodyAccent, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemifrogSkin, input.U.SkinColor));
                if (input.U.SpecialAccessoryType == 7)
                {
                    return;
                }

                output.Sprite(input.Sprites.Demifrogs1[15 + (input.A.IsEating ? 2 : input.A.IsAttacking ? 1 : 0) + 3 * input.U.SpecialAccessoryType]);
            }); // Primary Pattern (head)

            builder.RenderSingle(SpriteType.BodyAccent2, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemifrogSkin, input.U.AccessoryColor));
                if (input.U.BodyAccentType1 == 12)
                {
                    return;
                }

                output.Sprite(input.Sprites.Demifrogs2[0 + (input.A.IsAttacking ? 1 : 0) + 2 * input.U.BodySize + 8 * input.U.BodyAccentType1]);
            }); // Secondary Pattern (body)

            builder.RenderSingle(SpriteType.BodyAccent3, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemifrogSkin, input.U.AccessoryColor));
                if (input.U.BodyAccentType1 == 12)
                {
                    return;
                }

                output.Sprite(input.Sprites.Demifrogs2[96 + (input.A.IsEating ? 2 : input.A.IsAttacking ? 1 : 0) + 3 * input.U.BodyAccentType1]);
            }); // Secondary Pattern (head)

            builder.RenderSingle(SpriteType.BodyAccent4, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemifrogSkin, input.U.SkinColor));
                if (input.U.SpecialAccessoryType == 6 || input.U.HasDick || input.U.BodyAccentType2 == 1)
                {
                    return;
                }

                if (input.U.BodySize > 2)
                {
                    output.Sprite(input.Sprites.Demifrogs1[139]);
                    return;
                }

                output.Sprite(input.Sprites.Demifrogs1[138]);
            }); // Tertiary Pattern (genitals)

            builder.RenderSingle(SpriteType.BodyAccent5, 18, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemifrogSkin, input.U.SkinColor));
                if (input.U.SpecialAccessoryType == 6 || input.U.HasBreasts == false || input.A.PredatorComponent?.LeftBreastFullness > 0 || input.A.PredatorComponent?.RightBreastFullness > 0 || input.U.BodyAccentType2 == 1)
                {
                    return;
                }

                output.Sprite(input.Sprites.Demifrogs2[132 + input.U.BreastSize]);
            }); // Tertiary Pattern (breasts)

            builder.RenderSingle(SpriteType.BodyAccessory, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemifrogSkin, input.U.SkinColor));
                if (input.U.SpecialAccessoryType >= 7)
                {
                    if (input.U.SpecialAccessoryType > 7)
                    {
                        input.U.SpecialAccessoryType = 7;
                    }

                    return;
                }


                output.Sprite(input.Sprites.Demifrogs1[36 + (input.A.IsAttacking ? 1 : 0) + 2 * input.U.BodySize + 8 * input.U.SpecialAccessoryType]);
            }); // Primary Pattern (body)

            builder.RenderSingle(SpriteType.Breasts, 17, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemifrogSkin, input.U.SkinColor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.U.SpecialAccessoryType == 6)
                {
                    if (input.A.PredatorComponent?.LeftBreastFullness > 0)
                    {
                        int leftSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(31 * 31));

                        if (leftSize > 27)
                        {
                            leftSize = 27;
                        }

                        output.Sprite(input.Sprites.Demifrogs3Alt[0 + leftSize]);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.Demifrogs3Alt[0 + input.U.BreastSize]);
                    }
                }
                else
                {
                    if (input.A.PredatorComponent?.LeftBreastFullness > 0)
                    {
                        int leftSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(31 * 31));

                        if (leftSize > 27)
                        {
                            leftSize = 27;
                        }

                        output.Sprite(input.Sprites.Demifrogs3[0 + leftSize]);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.Demifrogs3[0 + input.U.BreastSize]);
                    }
                }
            });

            builder.RenderSingle(SpriteType.SecondaryBreasts, 17, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemifrogSkin, input.U.SkinColor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.U.SpecialAccessoryType == 6)
                {
                    if (input.A.PredatorComponent?.RightBreastFullness > 0)
                    {
                        int rightSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(31 * 31));

                        if (rightSize > 27)
                        {
                            rightSize = 27;
                        }

                        output.Sprite(input.Sprites.Demifrogs3Alt[31 + rightSize]);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.Demifrogs3Alt[31 + input.U.BreastSize]);
                    }
                }
                else
                {
                    if (input.A.PredatorComponent?.RightBreastFullness > 0)
                    {
                        int rightSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(31 * 31));

                        if (rightSize > 27)
                        {
                            rightSize = 27;
                        }

                        output.Sprite(input.Sprites.Demifrogs3[31 + rightSize]);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.Demifrogs3[31 + input.U.BreastSize]);
                    }
                }
            });

            builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemifrogSkin, input.U.SkinColor));
                if (input.A.HasBelly)
                {
                    int size = input.A.GetStomachSize(28, 0.6f);

                    if (input.U.SpecialAccessoryType == 6)
                    {
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

                        output.Sprite(input.Sprites.Demifrogs3Alt[62 + size]);
                    }
                    else
                    {
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
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect())
                {
                    if (input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(31 * 31)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(31 * 31)) < 16)
                    {
                        output.Layer(20);
                        output.Sprite(input.A.IsCockVoring ? input.Sprites.Demifrogs1[108 + input.U.DickSize] : input.Sprites.Demifrogs1[92 + input.U.DickSize]);
                    }
                    else
                    {
                        output.Layer(13);
                        output.Sprite(input.A.IsCockVoring ? input.Sprites.Demifrogs1[116 + input.U.DickSize] : input.Sprites.Demifrogs1[100 + input.U.DickSize]);
                    }
                }

                //output.Layer(11);
            });

            builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemifrogSkin, input.U.SkinColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect() && input.A.PredatorComponent?.VisibleFullness < .75f &&
                    (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize +
                                   input.A.GetRightBreastSize(32 * 32)) < 16 &&
                    (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize +
                                   input.A.GetLeftBreastSize(32 * 32)) < 16)
                {
                    output.Layer(19);
                }
                else
                {
                    output.Layer(10);
                }

                int size = input.U.DickSize;
                int offset = input.A.GetBallSize(28, .8f);


                if (input.U.SpecialAccessoryType == 6)
                {
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
                        output.Sprite(input.Sprites.Demifrogs3Alt[Math.Min(103 + offset, 129)]);
                        return;
                    }

                    output.Sprite(input.Sprites.Demifrogs3Alt[Math.Min(95 + size, 129)]);
                }
                else
                {
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
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    switch (input.A.GetWeaponSprite())
                    {
                        case 0:
                            if (input.U.BodySize == 3)
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
                            if (input.U.BodySize == 3)
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
                            if (input.U.BodySize == 3)
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
                            output.Sprite(input.Sprites.Demifrogs1[133 + input.U.BodySize]);
                            return;
                        case 7:
                            output.Sprite(input.Sprites.Demifrogs1[137]);
                            return;
                        default:
                            return;
                    }
                }
            });

            builder.RandomCustom((data, output) =>   
            {
                IUnitRead unit = data.Unit;
                Defaults.Randomize(data, output);


                unit.AccessoryColor = unit.SkinColor;

                if (State.Rand.Next(10) == 0)
                {
                    unit.SpecialAccessoryType = data.SetupOutput.SpecialAccessoryCount - 1;
                }
                else
                {
                    unit.SpecialAccessoryType = State.Rand.Next(data.SetupOutput.SpecialAccessoryCount - 1);
                }

                if (State.Rand.Next(3) == 0)
                {
                    unit.BodyAccentType1 = data.SetupOutput.BodyAccentTypes1 - 1;
                }
                else
                {
                    unit.BodyAccentType1 = State.Rand.Next(data.SetupOutput.BodyAccentTypes1 - 1);
                }

                unit.BodyAccentType2 = State.Rand.Next(data.SetupOutput.BodyAccentTypes2);

                if (unit.Type == UnitType.Leader)
                {
                    unit.ClothingType = 1 + Extensions.IndexOf(data.SetupOutput.AllowedMainClothingTypes, leaderClothes);
                }

                if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
                {
                    unit.ClothingType = 1 + Extensions.IndexOf(data.SetupOutput.AllowedMainClothingTypes, rags);
                    if (unit.ClothingType == 0) //Covers rags not in the list
                    {
                        unit.ClothingType = data.SetupOutput.AllowedMainClothingTypes.Count;
                    }
                }
            });
        });


        private static class GenericTop1
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop1Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[24];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.demifrogs/1524");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(19);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[56]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[48 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop2
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop2Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[34];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.demifrogs/1534");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(19);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[65]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[57 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop3
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop3Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[44];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.demifrogs/1544");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(19);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[74]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[66 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop4
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop4Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[55];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.demifrogs/1555");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(19);
                    output["Clothing2"].Layer(19);
                    output["Clothing2"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[83]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[75 + input.U.BreastSize]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Demifrogs4[84]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop5
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop5Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[74];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.demifrogs/1574");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(19);
                    output["Clothing2"].Layer(19);
                    output["Clothing2"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[93]);
                        output["Clothing2"].Sprite(input.Sprites.Demifrogs4[102]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[85 + input.U.BreastSize]);
                        output["Clothing2"].Sprite(input.Sprites.Demifrogs4[94 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop6
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop6Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[88];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.demifrogs/1588");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(19);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[107 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
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
                    output.ClothingId = new ClothingId("base.demifrogs/1579");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(19);

                    output["Clothing1"].Sprite(input.A.HasBelly ? input.Sprites.Demifrogs4[119 + input.U.BodySize] : input.Sprites.Demifrogs4[115 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
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
                    output.ClothingId = new ClothingId("base.demifrogs/1579");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(19);
                    output["Clothing1"].Sprite(input.Sprites.Demifrogs4[103 + input.U.BodySize]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class Natural
        {
            internal static readonly BindableClothing<IOverSizeParameters> NaturalInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.OccupiesAllSlots = true;
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(7);
                    output["Clothing1"].Layer(19);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.U.SpecialAccessoryType == 6 ? input.Sprites.Demifrogs4[12 + input.U.BreastSize] : input.Sprites.Demifrogs4[2 + input.U.BreastSize]);
                    }

                    if (input.U.BodySize > 2)
                    {
                        output["Clothing2"].Sprite(input.U.SpecialAccessoryType == 6 ? input.Sprites.Demifrogs4[11] : input.Sprites.Demifrogs4[1]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.U.SpecialAccessoryType == 6 ? input.Sprites.Demifrogs4[10] : input.Sprites.Demifrogs4[0]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.DemifrogSkin, input.U.SkinColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.DemifrogSkin, input.U.SkinColor));
                });
            });
        }

        private static class Tribal
        {
            internal static readonly BindableClothing<IOverSizeParameters> TribalInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Demifrogs4[143];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.demifrogs/1176");
                    output.OccupiesAllSlots = true;
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(12);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(19);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[138]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demifrogs4[131 + input.U.BreastSize]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Demifrogs4[139 + input.U.BodySize]);
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
                    output.ClothingId = new ClothingId("base.demifrogs/207");
                    output.OccupiesAllSlots = true;
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(12);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(19);
                    output["Clothing1"].Coloring(Color.white);
                    if (input.U.HasBreasts)
                    {
                        if (input.U.BreastSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Demifrogs4[127]);
                        }
                        else if (input.U.BreastSize < 6)
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

                    output["Clothing2"].Sprite(input.Sprites.Demifrogs4[123 + input.U.BodySize]);
                });
            });
        }

        private static class DemifrogLeader
        {
            internal static readonly BindableClothing<IOverSizeParameters> DemifrogLeaderInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.LeaderOnly = true;
                    output.DiscardSprite = input.Sprites.Demifrogs5[20];
                    output.RevealsBreasts = true;
                    output.OccupiesAllSlots = true;
                    output.ClothingId = new ClothingId("base.demifrogs/1177");
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output, extra) =>
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
                    output["Clothing1"].Sprite(input.Sprites.Demifrogs5[0 + input.U.BodySize]);
                    output["Clothing2"].Sprite(input.Sprites.Demifrogs5[4]);
                    output["Clothing4"].Sprite(input.Sprites.Demifrogs5[10 + (input.A.IsAttacking ? 1 : 0) + 2 * input.U.BodySize]);
                    output["Clothing5"].Sprite(input.Sprites.Demifrogs5[18]);
                    output["Clothing6"].Sprite(input.Sprites.Demifrogs5[19]);

                    if (input.U.HasBreasts)
                    {
                        if (extra.Oversize)
                        {
                            output["Clothing3"].Sprite(null);
                        }
                        else if (input.U.BreastSize < 3)
                        {
                            output["Clothing3"].Sprite(null);
                        }
                        else
                        {
                            output["Clothing3"].Sprite(input.Sprites.Demifrogs5[2 + input.U.BreastSize]);
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
                    output.ClothingId = new ClothingId("base.demifrogs/1521");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(12);
                    output["Clothing1"].Layer(13);
                    if (input.U.DickSize > 0)
                    {
                        if (input.U.DickSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Demifrogs4[24]);
                        }
                        else if (input.U.DickSize > 5)
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

                    output["Clothing2"].Sprite(input.Sprites.Demifrogs4[20 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
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
                    output.ClothingId = new ClothingId("base.demifrogs/1537");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(13);

                    output["Clothing2"].Layer(12);

                    output["Clothing2"].Coloring(Color.white);

                    if (input.U.DickSize > 0)
                    {
                        if (input.U.DickSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Demifrogs4[32]);
                        }
                        else if (input.U.DickSize > 5)
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

                    output["Clothing2"].Sprite(input.Sprites.Demifrogs4[27 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
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
                    output.ClothingId = new ClothingId("base.demifrogs/1540");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(13);
                    output["Clothing2"].Layer(12);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Sprite(input.Sprites.Demifrogs4[35]);
                    output["Clothing2"].Sprite(input.Sprites.Demifrogs4[27 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
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
                    output.ClothingId = new ClothingId("base.demifrogs/1514");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(12);

                    output["Clothing1"].Layer(13);

                    if (input.U.DickSize > 0)
                    {
                        if (input.U.DickSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Demifrogs4[45]);
                        }
                        else if (input.U.DickSize > 5)
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

                    output["Clothing2"].Sprite(input.Sprites.Demifrogs4[41 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
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
                    output.ClothingId = new ClothingId("base.demifrogs/1178");
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(12);

                    output["Clothing1"].Coloring(Color.white);

                    output["Clothing1"].Sprite(input.Sprites.Demifrogs4[36 + input.U.BodySize]);
                });
            });
        }
    }
}
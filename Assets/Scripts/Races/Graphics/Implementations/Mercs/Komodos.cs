#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.Mercs
{
    internal static class Komodos
    {
        private static Func<IClothingRenderInput, IOverSizeParameters> paramsCalc = CommonRaceCode.MakeOversizeFunc(32 * 32);
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Komodo", "Komodos");
                output.FlavorText(new FlavorText(
                    new Texts {  },
                    new Texts {  },
                    new Texts { "komodo", "komodo dragon", "komodo lizard" }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 18,
                    StomachSize = 18,
                    HasTail = true,
                    FavoredStat = Stat.Strength,
                    CanUseRangedWeapons = false,
                    PowerAdjustment = 1.4f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(16, 24),
                        Dexterity = new RaceStats.StatRange(6, 10),
                        Endurance = new RaceStats.StatRange(14, 24),
                        Mind = new RaceStats.StatRange(6, 12),
                        Will = new RaceStats.StatRange(8, 16),
                        Agility = new RaceStats.StatRange(10, 16),
                        Voracity = new RaceStats.StatRange(16, 24),
                        Stomach = new RaceStats.StatRange(12, 18),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Biter,
                        TraitType.VenomousBite,
                        TraitType.Intimidating,
                        TraitType.Resilient,
                    },
                    RaceDescription = "",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.BodyAccessoryType, "Body Pattern Type");
                    buttons.SetText(ButtonType.BodyAccentTypes1, "Head Shape");
                    buttons.SetText(ButtonType.BodyAccentTypes2, "Secondary Pattern Type");
                    buttons.SetText(ButtonType.BodyAccentTypes3, "Head Pattern on/off");
                });
                output.DickSizes = () => 8;
                output.BreastSizes = () => 8;

                output.BodySizes = 4;
                output.EyeTypes = 6;
                output.SpecialAccessoryCount = 5; // body patterns    
                output.HairStyles = 0;
                output.MouthTypes = 0;
                output.HairColors = 0;
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.KomodosSkin);
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.KomodosSkin);
                output.BodyAccentTypes1 = 11; // head shapes
                output.BodyAccentTypes2 = 6; // secondary body pattern
                output.BodyAccentTypes3 = 2; // head pattern on/off

                output.ExtendedBreastSprites = true;

                output.AllowedClothingHatTypes.Clear();


                output.AllowedMainClothingTypes.Set(
                    GenericTop1.GenericTop1Instance.Create(paramsCalc),
                    GenericTop2.GenericTop2Instance.Create(paramsCalc),
                    GenericTop3.GenericTop3Instance.Create(paramsCalc),
                    GenericTop4.GenericTop4Instance.Create(paramsCalc),
                    GenericTop5.GenericTop5Instance.Create(paramsCalc),
                    Natural.NaturalInstance.Create(paramsCalc),
                    Tribal.TribalInstance.Create(paramsCalc)
                );
                output.AvoidedMainClothingTypes = 0;
                output.AvoidedEyeTypes = 0;
                output.AllowedWaistTypes.Set(
                    GenericBot1.GenericBot1Instance,
                    GenericBot2.GenericBot2Instance,
                    GenericBot3.GenericBot3Instance
                );

                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.AviansSkin);
            });


            builder.RunBefore((input, output) =>
            {
                Defaults.BasicBellyRunAfter.Invoke(input, output);
            });

            builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
                if (input.U.SpecialAccessoryType == 4)
                {
                    if (input.A.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Komodos1[61]);
                        return;
                    }

                    if (input.A.IsAttacking || input.A.IsEating)
                    {
                        output.Sprite(input.Sprites.Komodos1[58]);
                        return;
                    }

                    output.Sprite(input.Sprites.Komodos1[55]);
                }
                else if (input.U.SpecialAccessoryType == 3)
                {
                    if (input.A.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Komodos1[59]);
                        return;
                    }

                    if (input.A.IsAttacking || input.A.IsEating)
                    {
                        output.Sprite(input.Sprites.Komodos1[56]);
                        return;
                    }

                    output.Sprite(input.Sprites.Komodos1[53]);
                }
                else
                {
                    if (input.A.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Komodos1[60]);
                        return;
                    }

                    if (input.A.IsAttacking || input.A.IsEating)
                    {
                        output.Sprite(input.Sprites.Komodos1[57]);
                        return;
                    }

                    output.Sprite(input.Sprites.Komodos1[54]);
                }
            });

            builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
                output.Sprite(input.Sprites.Komodos1[65 + input.U.EyeType]);

                if (input.A.IsOralVoring)
                {
                    output.AddOffset(0, 1 * .625f);
                }
            });

            builder.RenderSingle(SpriteType.Mouth, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Komodos1[63]);
                    return;
                }

                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Komodos1[62]);
                }
            });

            builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
                int sat = Mathf.Clamp(input.U.SpecialAccessoryType, 0, 4); //Protect against out of bounds from other unit types.  
                output.Sprite(input.U.HasBreasts ? input.Sprites.Komodos1[0 + input.U.BodySize + 10 * sat] : input.Sprites.Komodos1[4 + input.U.BodySize + 10 * sat]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
                output.Sprite(input.A.IsAttacking ? input.Sprites.Komodos1[9 + 10 * input.U.SpecialAccessoryType] : input.Sprites.Komodos1[8 + 10 * input.U.SpecialAccessoryType]);
            }); // Right Arm

            builder.RenderSingle(SpriteType.BodyAccent2, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Komodos1[64]);
            }); // Toenails
            builder.RenderSingle(SpriteType.BodyAccent3, 8, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
                if (input.U.BodyAccentType1 == 10)
                {
                }
                else if (input.U.SpecialAccessoryType == 3)
                {
                    output.Sprite(input.Sprites.Komodos3[10 + input.U.BodyAccentType1]);
                }
                else
                {
                    output.Sprite(input.Sprites.Komodos3[0 + input.U.BodyAccentType1]);
                }
            }); // Head Shape

            builder.RenderSingle(SpriteType.BodyAccent4, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.AccessoryColor));
                if (input.U.BodyAccentType2 == 5)
                {
                }
                else
                {
                    output.Sprite(input.Sprites.Komodos3[20 + input.U.BodySize + 10 * input.U.BodyAccentType2]);
                }
            }); // Body Secondary Pattern

            builder.RenderSingle(SpriteType.BodyAccent5, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.AccessoryColor));
                if (input.U.BodyAccentType3 == 1 || input.U.BodyAccentType2 == 5)
                {
                }
                else if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Komodos3[29 + 10 * input.U.BodyAccentType2]);
                }
                else if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Komodos3[28 + 10 * input.U.BodyAccentType2]);
                }
                else
                {
                    output.Sprite(input.Sprites.Komodos3[27 + 10 * input.U.BodyAccentType2]);
                }
            }); // Head Secondary Pattern

            builder.RenderSingle(SpriteType.BodyAccent6, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.AccessoryColor));
                if (input.U.BodyAccentType2 == 5)
                {
                }
                else
                {
                    output.Sprite(input.Sprites.Komodos3[26 + 10 * input.U.BodyAccentType2]);
                }
            }); // Tail Secondary Pattern

            builder.RenderSingle(SpriteType.BodyAccent7, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.AccessoryColor));
                if (input.U.BodyAccentType2 == 5)
                {
                }
                else if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Komodos3[25 + 10 * input.U.BodyAccentType2]);
                }
                else
                {
                    output.Sprite(input.Sprites.Komodos3[24 + 10 * input.U.BodyAccentType2]);
                }
            }); // Right Arm Secondary Pattern

            builder.RenderSingle(SpriteType.BodyAccessory, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
                if (input.U.SpecialAccessoryType == 4)
                {
                    output.Sprite(input.Sprites.Komodos1[52]);
                }
                else if (input.U.SpecialAccessoryType == 3)
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
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.LeftBreastFullness > 0)
                {
                    int leftSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(30 * 30));

                    if (leftSize > 26)
                    {
                        leftSize = 26;
                    }

                    output.Sprite(input.Sprites.Komodos2[0 + leftSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Komodos2[0 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.SecondaryBreasts, 17, (input, output) =>
            {
                output.Coloring(KomodoColor(input.Actor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.RightBreastFullness > 0)
                {
                    int rightSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(30 * 30));

                    if (rightSize > 26)
                    {
                        rightSize = 26;
                    }

                    output.Sprite(input.Sprites.Komodos2[30 + rightSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Komodos2[30 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
            {
                output.Coloring(KomodoColor(input.Actor));
                if (input.A.HasBelly)
                {
                    int size = input.A.GetStomachSize(26, 0.7f);

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
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect())
                {
                    if (input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(31 * 31)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(31 * 31)) < 16)
                    {
                        output.Layer(20);
                        output.Sprite(input.A.IsCockVoring ? input.Sprites.Komodos1[79 + input.U.DickSize] : input.Sprites.Komodos1[71 + input.U.DickSize]);
                    }
                    else
                    {
                        output.Layer(13);
                        output.Sprite(input.A.IsCockVoring ? input.Sprites.Komodos1[95 + input.U.DickSize] : input.Sprites.Komodos1[87 + input.U.DickSize]);
                    }
                }

                //output.Layer(11); 
            });

            builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
            {
                output.Coloring(KomodoColor(input.Actor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect() && input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(30 * 30)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(30 * 30)) < 16)
                {
                    output.Layer(19);
                }
                else
                {
                    output.Layer(10);
                }

                int size = input.U.DickSize;
                int offset = input.A.GetBallSize(27, .8f);

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
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    switch (input.A.GetWeaponSprite())
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
                return ColorPaletteMap.GetPalette(SwapType.KomodosReversed, actor.Unit.SkinColor);
            }

            return ColorPaletteMap.GetPalette(SwapType.KomodosSkin, actor.Unit.SkinColor);
        }


        private static class GenericTop1
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop1Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Komodos4[48];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.komodos/61401");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Komodos4[47]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Komodos4[39 + input.U.BreastSize]);
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
                    output.DiscardSprite = input.Sprites.Komodos4[58];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.komodos/61402");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Komodos4[57]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Komodos4[49 + input.U.BreastSize]);
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
                    output.DiscardSprite = input.Sprites.Komodos4[68];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.komodos/61403");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing2"].Layer(18);
                    output["Clothing2"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Komodos4[67]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Komodos4[59 + input.U.BreastSize]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Komodos4[69]);
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
                    output.DiscardSprite = input.Sprites.Komodos4[79];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.komodos/61404");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing2"].Layer(18);
                    output["Clothing2"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                        output["Clothing2"].Sprite(input.Sprites.Komodos4[78]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Komodos4[80 + input.U.BreastSize]);
                        output["Clothing2"].Sprite(input.Sprites.Komodos4[70 + input.U.BreastSize]);
                    }

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
                    output.DiscardSprite = input.Sprites.Komodos4[97];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.komodos/61405");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Komodos4[96]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Komodos4[88 + input.U.BreastSize]);
                    }
                
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
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Komodos4[1 + input.U.BreastSize]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Komodos4[0]);

                    if (input.U.SpecialAccessoryType == 4)
                    {
                        output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosReversed, input.U.SkinColor));
                        output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosReversed, input.U.SkinColor));
                    }
                    else
                    {
                        output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
                        output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.KomodosSkin, input.U.SkinColor));
                    }
                });
            });
        }

        private static class Tribal
        {
            internal static readonly BindableClothing<IOverSizeParameters> TribalInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Komodos4[38];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.komodos/61406");
                    output.OccupiesAllSlots = true;
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(12);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Komodos4[37]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Komodos4[29 + input.U.BreastSize]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Komodos4[25 + input.U.BodySize]);
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
                    output.ClothingId = new ClothingId("base.komodos/61407");
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
                            output["Clothing1"].Sprite(input.Sprites.Komodos4[15]);
                        }
                        else if (input.U.DickSize > 5)
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

                    output["Clothing2"].Sprite(input.Sprites.Komodos4[10 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
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
                    output.ClothingId = new ClothingId("base.komodos/61408");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(13);
                    output["Clothing2"].Layer(12);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Sprite(input.Sprites.Komodos4[18]);
                    output["Clothing2"].Sprite(input.Sprites.Komodos4[10 + input.U.BodySize]);

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
                    output.DiscardSprite = input.Sprites.Komodos4[24];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.komodos/61409");
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(12);

                    output["Clothing1"].Coloring(Color.white);

                    output["Clothing1"].Sprite(input.Sprites.Komodos4[20 + input.U.BodySize]);
                });
            });
        }
    }
}
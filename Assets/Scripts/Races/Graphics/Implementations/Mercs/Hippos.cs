#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.Mercs
{
    internal static class Hippos
    {
        private static Func<IClothingRenderInput, IOverSizeParameters> _paramsCalc = CommonRaceCode.MakeOversizeFunc(32 * 32);

        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Hippo", "Hippos");
                output.FlavorText(new FlavorText(
                    new Texts { },
                    new Texts { },
                    new Texts { "hippo", "hippopotamus", "pachyderm" },
                    new Dictionary<string, string>
                    {
                        [WeaponNames.Mace] = "Tribal Knife",
                        [WeaponNames.Axe] = "WeaponNames.Axe",
                        [WeaponNames.SimpleBow] = "Simple Bow",
                        [WeaponNames.CompoundBow] = "Compound Bow",
                        [WeaponNames.Claw] = "Fist"
                    }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 20,
                    StomachSize = 22,
                    HasTail = true,
                    FavoredStat = Stat.Endurance,
                    PowerAdjustment = 1.3f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(12, 20),
                        Dexterity = new RaceStats.StatRange(6, 14),
                        Endurance = new RaceStats.StatRange(18, 28),
                        Mind = new RaceStats.StatRange(6, 12),
                        Will = new RaceStats.StatRange(8, 16),
                        Agility = new RaceStats.StatRange(8, 14),
                        Voracity = new RaceStats.StatRange(12, 20),
                        Stomach = new RaceStats.StatRange(12, 18),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.StrongMelee,
                        TraitType.HardSkin,
                    },
                    RaceDescription = "",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.BodyAccessoryColor, "Accent Color");
                    buttons.SetText(ButtonType.BodyAccessoryType, "Ear Type");
                    buttons.SetText(ButtonType.HatType, "Headwear Type");
                    buttons.SetText(ButtonType.ClothingAccessoryType, "Necklace Type");
                    buttons.SetText(ButtonType.BodyAccentTypes1, "Left Arm Pattern");
                    buttons.SetText(ButtonType.BodyAccentTypes2, "Right Arm Pattern");
                    buttons.SetText(ButtonType.BodyAccentTypes3, "Head Pattern");
                    buttons.SetText(ButtonType.BodyAccentTypes4, "Leg Pattern");
                });
                output.DickSizes = () => 8;
                output.BreastSizes = () => 8;

                output.BodySizes = 5;
                output.HairStyles = 0;
                output.EyeTypes = 5;
                output.SpecialAccessoryCount = 8; // ears  
                output.HairStyles = 0;
                output.MouthTypes = 0;
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.HippoSkin);
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.HippoSkin); // tattoo/warpaint colors
                output.BodyAccentTypes1 = 6; // left arm tattoo/warpaint
                output.BodyAccentTypes2 = 6; // right arm tattoo/warpaint
                output.BodyAccentTypes3 = 6; // head tattoo/warpaint
                output.BodyAccentTypes4 = 6; // legs tattoo/warpaint
                output.ClothingColors = 0;

                output.ExtendedBreastSprites = true;

                output.AllowedMainClothingTypes.Set(
                    HipposTop1.HipposTop1Instance.Create(_paramsCalc),
                    HipposTop2.HipposTop2Instance.Create(_paramsCalc),
                    HipposTop3.HipposTop3Instance.Create(_paramsCalc),
                    Natural.NaturalInstance.Create(_paramsCalc)
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
                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.HippoSkin);
            });


            builder.RunBefore((input, output) => { Defaults.Finalize.Invoke(input, output); });

            builder.RenderSingle(SpriteType.Head, 22, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.HippoSkin, input.U.SkinColor));
                if (input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Hippos[21]);
                    return;
                }

                output.Sprite(input.Sprites.Hippos[20]);
            });

            builder.RenderSingle(SpriteType.Eyes, 25, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
                output.Sprite(input.Sprites.Hippos[30 + (input.A.IsEating ? 1 : 0) + 2 * input.U.EyeType]);
            });
            builder.RenderSingle(SpriteType.Body, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.HippoSkin, input.U.SkinColor));
                output.Sprite(input.U.HasBreasts ? input.Sprites.Hippos[0 + (input.A.IsAttacking ? 1 : 0) + 2 * input.U.BodySize] : input.Sprites.Hippos[10 + (input.A.IsAttacking ? 1 : 0) + 2 * input.U.BodySize]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.HippoSkin, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Hippos2[0 + (input.A.IsAttacking ? 1 : 0) + 2 * input.U.BodyAccentType1]);
            }); // left arm tattoo/warpaint
            builder.RenderSingle(SpriteType.BodyAccent2, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.HippoSkin, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Hippos2[12 + input.U.BodyAccentType2]);
            }); // right arm tattoo/warpaint
            builder.RenderSingle(SpriteType.BodyAccent3, 23, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.HippoSkin, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Hippos2[18 + (input.A.IsEating ? 1 : 0) + 2 * input.U.BodyAccentType3]);
            }); // head tattoo/warpaint
            builder.RenderSingle(SpriteType.BodyAccent4, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.HippoSkin, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Hippos2[30 + input.U.BodyAccentType4]);
            }); // legs tattoo/warpaint
            builder.RenderSingle(SpriteType.BodyAccent5, 24, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.HippoSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Hippos[40 + (input.A.IsEating ? 1 : 0) + 2 * input.U.EyeType]);
            }); // eyebrows
            builder.RenderSingle(SpriteType.BodyAccent6, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.HippoSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Hippos[120 + (input.A.IsAttacking ? 1 : 0)]);
            }); // left arm
            builder.RenderSingle(SpriteType.BodyAccessory, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.HippoSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Hippos[22 + input.U.SpecialAccessoryType]);
            }); // ears
            builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.HippoSkin, input.U.SkinColor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.LeftBreastFullness > 0)
                {
                    int leftSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(32 * 32));

                    if (leftSize > 28)
                    {
                        leftSize = 28;
                    }

                    output.Sprite(input.Sprites.Hippos3[0 + leftSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Hippos3[0 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.SecondaryBreasts, 16, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.HippoSkin, input.U.SkinColor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.RightBreastFullness > 0)
                {
                    int rightSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(32 * 32));

                    if (rightSize > 28)
                    {
                        rightSize = 28;
                    }

                    output.Sprite(input.Sprites.Hippos3[32 + rightSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Hippos3[32 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.HippoSkin, input.U.SkinColor));
                if (input.A.HasBelly)
                {
                    int size = input.A.GetStomachSize(26, 0.7f);

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
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.HippoSkin, input.U.SkinColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect())
                {
                    if (input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(32 * 32)) < 16)
                    {
                        output.Layer(18);
                        if (input.A.IsCockVoring)
                        {
                            output.Sprite(input.Sprites.Hippos[82 + input.U.DickSize]);
                            return;
                        }

                        output.Sprite(input.Sprites.Hippos[66 + input.U.DickSize]);
                        return;
                    }

                    output.Layer(12);
                    if (input.A.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.Hippos[50 + input.U.DickSize]);
                        return;
                    }

                    output.Sprite(input.Sprites.Hippos[58 + input.U.DickSize]);
                    return;
                }

                output.Sprite(input.Sprites.Hippos[58 + input.U.DickSize]).Layer(9);
            });

            builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.HippoSkin, input.U.SkinColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect() && input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(32 * 32)) < 16)
                {
                    output.Layer(17);
                }
                else
                {
                    output.Layer(8);
                }

                int baseSize = (input.U.DickSize + 1) / 3;
                int ballOffset = input.A.GetBallSize(26, .9f);

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
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    output.Sprite(input.Sprites.Hippos[74 + input.A.GetWeaponSprite()]);
                }
            });

            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                IUnitRead unit = data.Unit;

                if (State.Rand.Next(5) == 0)
                {
                    unit.BodyAccentType1 = State.Rand.Next(data.SetupOutput.BodyAccentTypes1 - 1);
                }
                else
                {
                    unit.BodyAccentType1 = data.SetupOutput.BodyAccentTypes1 - 1;
                }

                if (State.Rand.Next(5) == 0)
                {
                    unit.BodyAccentType2 = State.Rand.Next(data.SetupOutput.BodyAccentTypes2 - 1);
                }
                else
                {
                    unit.BodyAccentType2 = data.SetupOutput.BodyAccentTypes2 - 1;
                }

                if (State.Rand.Next(5) == 0)
                {
                    unit.BodyAccentType3 = State.Rand.Next(data.SetupOutput.BodyAccentTypes3 - 1);
                }
                else
                {
                    unit.BodyAccentType3 = data.SetupOutput.BodyAccentTypes3 - 1;
                }

                if (State.Rand.Next(5) == 0)
                {
                    unit.BodyAccentType4 = State.Rand.Next(data.SetupOutput.BodyAccentTypes4 - 1);
                }
                else
                {
                    unit.BodyAccentType4 = data.SetupOutput.BodyAccentTypes4 - 1;
                }
            });
        });


        private static class HipposTop1
        {
            internal static readonly BindableClothing<IOverSizeParameters> HipposTop1Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Hippos2[60];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.hippos/84260");
                    output.FixedColor = true;
                });
                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Hippos2[52 + input.U.BreastSize]);
                    }
                });
            });
        }

        private static class HipposTop2
        {
            internal static readonly BindableClothing<IOverSizeParameters> HipposTop2Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Hippos2[87];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.hippos/84287");
                    output.FixedColor = true;
                });
                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Hippos2[79 + input.U.BreastSize]);
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
            internal static readonly BindableClothing<IOverSizeParameters> HipposTop3Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Hippos2[96];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.hippos/84296");
                    output.FixedColor = true;
                });
                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Hippos2[88 + input.U.BreastSize]);
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
                    output["Clothing2"].Layer(10);
                    output["Clothing1"].Layer(17);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Hippos3[96 + input.U.BreastSize]);
                        output["Clothing2"].Sprite(input.Sprites.Hippos3[95]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.HippoSkin, input.U.SkinColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.HippoSkin, input.U.SkinColor));
                });
            });
        }

        private static class HipposBot1
        {
            internal static readonly IClothing HipposBot1Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Hippos2[66];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.hippos/84266");
                    output.FixedColor = true;
                });
                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Sprite(input.Sprites.Hippos2[61 + input.U.BodySize]);
                });
            });
        }

        private static class HipposBot2
        {
            internal static readonly IClothing HipposBot2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Hippos2[72];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.hippos/84272");
                    output.FixedColor = true;
                });
                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Sprite(input.Sprites.Hippos2[67 + input.U.BodySize]);
                });
            });
        }

        private static class HipposBot3
        {
            internal static readonly IClothing HipposBot3Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Hippos2[78];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.hippos/84278");
                    output.FixedColor = true;
                });
                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Sprite(input.Sprites.Hippos2[73 + input.U.BodySize]);
                });
            });
        }

        private static class HipposBot4
        {
            internal static readonly IClothing HipposBot4Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Hippos2[102];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.hippos/84302");
                    output.FixedColor = true;
                });
                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Sprite(input.Sprites.Hippos2[97 + input.U.BodySize]);
                });
            });
        }

        private static class HipposHeadband1
        {
            internal static readonly IClothing HipposHeadband1Instance = ClothingBuilder.Create(builder =>
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
            internal static readonly IClothing HipposHeadband2Instance = ClothingBuilder.Create(builder =>
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
            internal static readonly IClothing HipposHeadband3Instance = ClothingBuilder.Create(builder =>
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
            internal static readonly IClothing HipposHeadband4Instance = ClothingBuilder.Create(builder =>
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
            internal static readonly IClothing HipposHeadband5Instance = ClothingBuilder.Create(builder =>
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
            internal static readonly IClothing HipposHeadband6Instance = ClothingBuilder.Create(builder =>
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
            internal static readonly IClothing HipposHeadband7Instance = ClothingBuilder.Create(builder =>
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
            internal static readonly IClothing HipposHeadband8Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(26);
                    output["Clothing1"].Sprite(input.Sprites.Hippos2[51]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.HippoSkin, input.U.ClothingColor));
                });
            });
        }


        private static class HipposNecklace1
        {
            internal static readonly IClothing HipposNecklace1Instance = ClothingBuilder.Create(builder =>
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
            internal static readonly IClothing HipposNecklace2Instance = ClothingBuilder.Create(builder =>
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
            internal static readonly IClothing HipposNecklace3Instance = ClothingBuilder.Create(builder =>
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
            internal static readonly IClothing HipposNecklace4Instance = ClothingBuilder.Create(builder =>
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
            internal static readonly IClothing HipposNecklace5Instance = ClothingBuilder.Create(builder =>
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
            internal static readonly IClothing HipposNecklace6Instance = ClothingBuilder.Create(builder =>
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
            internal static readonly IClothing HipposNecklace7Instance = ClothingBuilder.Create(builder =>
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
            internal static readonly IClothing HipposNecklace8Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(21);
                    output["Clothing1"].Sprite(input.Sprites.Hippos2[43]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.HippoSkin, input.U.ClothingColor));
                });
            });
        }
    }
}
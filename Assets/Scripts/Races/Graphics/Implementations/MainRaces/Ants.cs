#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.MainRaces
{
    internal static class Ants
    {
        private static Func<IClothingRenderInput, IOverSizeParameters> paramsCalc = CommonRaceCode.MakeOversizeFunc(32 * 32);

        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            IClothing rags = DemiantRags.DemiantRagsInstance;


            builder.Setup(output =>
            {
                output.Names("Ant", "Ants");
                output.FlavorText(new FlavorText(
                    new Texts { },
                    new Texts { },
                    new Texts { },
                    new Dictionary<string, string>
                    {
                        [WeaponNames.Mace] = "Barbed Spear",
                        [WeaponNames.Axe] = "Quad Blades",
                        [WeaponNames.SimpleBow] = "Simple Bow",
                        [WeaponNames.CompoundBow] = "Compound Bow"
                    }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 9,
                    StomachSize = 12,
                    HasTail = false,
                    FavoredStat = Stat.Strength,
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.PackStrength,
                        TraitType.RangedIneptitude,
                        TraitType.AntPheromones
                    },
                    RaceDescription = "",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.BodyAccessoryColor, "Exoskeleton Color");
                    buttons.SetText(ButtonType.BodyAccessoryType, "Antennae Type");
                });
                output.TownNames(new List<string>
                {
                    "Queen's Mound",
                    "Formicia",
                    "Antville",
                    "Red Hill",
                    "Leafcutter Heap",
                    "Sugar Farm",
                    "Paraponera City",
                    "Myrmidon Point",
                    "Bivouac Nest",
                    "Fire Ant Fort",
                    "Larvaburg",
                    "Needlefields",
                    "Flik's Town",
                    "Lasius Rock"
                });
                output.DickSizes = () => 8;
                output.BreastSizes = () => 8;

                output.BodySizes = 3;
                output.EyeTypes = 8;
                output.SpecialAccessoryCount = 12; // antennae        
                output.HairStyles = 24;
                output.MouthTypes = 3;
                output.EyeColors = 0;
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.DemiantSkin);
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.DemiantSkin);

                output.ExtendedBreastSprites = true;

                output.AllowedMainClothingTypes.Set(
                    GenericTop1.GenericTop1Instance.Create(paramsCalc),
                    GenericTop2.GenericTop2Instance.Create(paramsCalc),
                    GenericTop3.GenericTop3Instance.Create(paramsCalc),
                    GenericTop4.GenericTop4Instance.Create(paramsCalc),
                    GenericTop5.GenericTop5Instance.Create(paramsCalc),
                    GenericTop6.GenericTop6Instance.Create(paramsCalc),
                    MaleTop.MaleTopInstance,
                    MaleTop2.MaleTop2Instance,
                    Natural.NaturalInstance.Create(paramsCalc),
                    Cuirass.CuirassInstance.Create(paramsCalc),
                    rags
                );
                output.AvoidedMainClothingTypes = 1;
                output.AvoidedEyeTypes = 0;
                output.AllowedWaistTypes.Set(
                    GenericBot1.GenericBot1Instance,
                    GenericBot2.GenericBot2Instance,
                    GenericBot3.GenericBot3Instance,
                    GenericBot4.GenericBot4Instance
                );

                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.AviansSkin);
            });


            builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.SkinColor));
                output.Sprite(input.U.HasBreasts ? input.Sprites.Demiants1[0 + input.U.BodySize] : input.Sprites.Demiants1[3 + input.U.BodySize]);
            }); // Upper Body (White)

            builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Demiants1[24 + input.U.EyeType]);
            });
            builder.RenderSingle(SpriteType.Mouth, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Demiants1[32]);
                    return;
                }

                output.Sprite(input.Sprites.Demiants1[33 + input.U.MouthType]);
            });

            builder.RenderSingle(SpriteType.Hair, 18, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.NormalHair, input.U.HairColor));
                output.Sprite(input.Sprites.Demiants1[60 + input.U.HairStyle]);
            });

            builder.RenderSingle(SpriteType.Body, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.AccessoryColor));
                output.Sprite(input.U.HasBreasts ? input.Sprites.Demiants1[6 + input.U.BodySize] : input.Sprites.Demiants1[9 + input.U.BodySize]);
            }); // Lower Body (black)

            builder.RenderSingle(SpriteType.BodyAccent, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Demiants1[18 + input.U.BodySize]);
            }); // Abdomen 2 (White)

            builder.RenderSingle(SpriteType.BodyAccent2, 19, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.AccessoryColor));
                output.Sprite(input.U.HasBreasts ? input.Sprites.Demiants1[36 + input.U.SpecialAccessoryType] : input.Sprites.Demiants1[48 + input.U.SpecialAccessoryType]);
            }); // Antennae (black)

            builder.RenderSingle(SpriteType.BodyAccent3, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.AccessoryColor));
                if (input.U.HasWeapon == false)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Demiants1[17]);
                        return;
                    }

                    output.Sprite(input.Sprites.Demiants1[124]);
                    return;
                }

                switch (input.A.GetWeaponSprite())
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
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.AccessoryColor));
                if (input.U.HasWeapon == false)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Demiants1[14]);
                        return;
                    }

                    output.Sprite(input.Sprites.Demiants1[14]);
                    return;
                }

                switch (input.A.GetWeaponSprite())
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
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Demiants1[21 + input.U.BodySize]);
            }); // Abdomen (black)
            builder.RenderSingle(SpriteType.Breasts, 17, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.SkinColor));
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

                    output.Sprite(input.Sprites.Demiants2[0 + leftSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Demiants2[0 + input.U.BreastSize]);
                }
            }); // Breasts (white)

            builder.RenderSingle(SpriteType.SecondaryBreasts, 17, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.SkinColor));
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

                    output.Sprite(input.Sprites.Demiants2[32 + rightSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Demiants2[32 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.SkinColor));
                if (input.A.HasBelly)
                {
                    int size = input.A.GetStomachSize(31, 0.8f);

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
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect())
                {
                    if (input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(32 * 32)) < 16)
                    {
                        output.Layer(20);
                        output.Sprite(input.A.IsCockVoring ? input.Sprites.Demiants1[100 + input.U.DickSize] : input.Sprites.Demiants1[84 + input.U.DickSize]);
                    }
                    else
                    {
                        output.Layer(13);
                        output.Sprite(input.A.IsCockVoring ? input.Sprites.Demiants1[108 + input.U.DickSize] : input.Sprites.Demiants1[92 + input.U.DickSize]);
                    }
                }

                //output.Layer(11); why was this here in original code TODO
            });

            builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.SkinColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect() && input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(32 * 32)) < 16)
                {
                    output.Layer(19);
                }
                else
                {
                    output.Layer(10);
                }

                int size = input.U.DickSize;
                int offset = input.A.GetBallSize(28, .8f);

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
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    output.Sprite(input.Sprites.Demiants1[116 + input.A.GetWeaponSprite()]);
                }
            });


            builder.RunBefore((input, output) => { Defaults.BasicBellyRunAfter.Invoke(input, output); });

            builder.RandomCustom(data =>
            {
                IUnitRead unit = data.Unit;
                Defaults.RandomCustom(data);


                unit.AccessoryColor = unit.SkinColor;

                if (unit.HasDick && unit.HasBreasts)
                {
                    unit.HairStyle = State.Rand.Next(Config.HermsOnlyUseFemaleHair ? 18 : data.SetupOutput.HairStyles);
                }
                else if (unit.HasDick && Config.FemaleHairForMales)
                {
                    unit.HairStyle = State.Rand.Next(data.SetupOutput.HairStyles);
                }
                else if (unit.HasDick == false && Config.MaleHairForFemales)
                {
                    unit.HairStyle = State.Rand.Next(data.SetupOutput.HairStyles);
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
                    output.ClothingId = new ClothingId("base.ants/1524");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[46]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[38 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }


        private static class GenericTop1V2
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop1_V2 = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[24];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.ants/1524");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[46]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[38 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });

            internal static readonly BindableClothing<IOverSizeParameters> GenericTop1Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[24];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.ants/1524");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[46]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[38 + input.U.BreastSize]);
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
                    output.ClothingId = new ClothingId("base.ants/1534");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[55]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[47 + input.U.BreastSize]);
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
                    output.ClothingId = new ClothingId("base.ants/1544");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[64]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[56 + input.U.BreastSize]);
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
                    output.ClothingId = new ClothingId("base.ants/1555");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing2"].Layer(18);
                    output["Clothing2"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[73]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[65 + input.U.BreastSize]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Demiants3[74]);
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
                    output.ClothingId = new ClothingId("base.ants/1574");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing2"].Layer(18);
                    output["Clothing2"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[83]);
                        output["Clothing2"].Sprite(input.Sprites.Demiants3[92]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[75 + input.U.BreastSize]);
                        output["Clothing2"].Sprite(input.Sprites.Demiants3[84 + input.U.BreastSize]);
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
                    output.ClothingId = new ClothingId("base.ants/1588");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[96 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(null);
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
                    output.ClothingId = new ClothingId("base.ants/1579");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);

                    output["Clothing1"].Sprite(input.A.HasBelly ? input.Sprites.Demiants3[107 + input.U.BodySize] : input.Sprites.Demiants3[104 + input.U.BodySize]);

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
                    output.ClothingId = new ClothingId("base.ants/1579");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[93 + input.U.BodySize]);
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
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[0 + input.U.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(null);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Demiants3[8]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.SkinColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.SkinColor));
                });
            });
        }

        private static class Cuirass
        {
            internal static readonly BindableClothing<IOverSizeParameters> CuirassInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Demiants3[138];
                    output.RevealsBreasts = true;
                    output.OccupiesAllSlots = true;
                    output.FixedColor = true;
                    output.ClothingId = new ClothingId("base.ants/195");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing4"].Layer(7);
                    output["Clothing4"].Coloring(Color.white);
                    output["Clothing3"].Layer(12);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing2"].Layer(7);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[124]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        if (input.U.BreastSize < 2)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Demiants3[120]);
                        }
                        else if (input.U.BreastSize < 4)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Demiants3[121]);
                        }
                        else if (input.U.BreastSize < 6)
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

                    if (input.A.HasBelly)
                    {
                        output["Clothing2"].Sprite(null);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Demiants3[126 + input.U.BodySize] : input.Sprites.Demiants3[129 + input.U.BodySize]);
                    }

                    output["Clothing3"].Sprite(input.Sprites.Demiants3[132 + input.U.BodySize]);

                    if (input.A.GetWeaponSprite() == 1)
                    {
                        output["Clothing4"].Sprite(input.Sprites.Demiants3[136]);
                    }
                    else if (input.A.GetWeaponSprite() == 3)
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
            internal static readonly IClothing DemiantRagsInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Rags[23];
                    output.InFrontOfDick = true;
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.ants/207");
                    output.OccupiesAllSlots = true;
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(12);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Coloring(Color.white);
                    if (input.U.HasBreasts)
                    {
                        if (input.U.BreastSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Demiants3[117]);
                        }
                        else if (input.U.BreastSize < 6)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Demiants3[118]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Demiants3[119]);
                        }

                        output["Clothing2"].Sprite(input.Sprites.Demiants3[110 + input.U.BodySize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Demiants3[116]);
                        output["Clothing2"].Sprite(input.Sprites.Demiants3[113 + input.U.BodySize]);
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
                    output.ClothingId = new ClothingId("base.ants/1521");
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
                            output["Clothing1"].Sprite(input.Sprites.Demiants3[15]);
                        }
                        else if (input.U.DickSize > 5)
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

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Demiants3[9 + input.U.BodySize] : input.Sprites.Demiants3[12 + input.U.BodySize]);

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
                    output.ClothingId = new ClothingId("base.ants/1537");
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
                            output["Clothing1"].Sprite(input.Sprites.Demiants3[25]);
                        }
                        else if (input.U.DickSize > 5)
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

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Demiants3[18 + input.U.BodySize] : input.Sprites.Demiants3[21 + input.U.BodySize]);

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
                    output.ClothingId = new ClothingId("base.ants/1540");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(13);
                    output["Clothing2"].Layer(12);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Sprite(input.Sprites.Demiants3[28]);

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Demiants3[18 + input.U.BodySize] : input.Sprites.Demiants3[21 + input.U.BodySize]);

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
                    output.ClothingId = new ClothingId("base.ants/1514");
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
                            output["Clothing1"].Sprite(input.Sprites.Demiants3[35]);
                        }
                        else if (input.U.DickSize > 5)
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

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Demiants3[29 + input.U.BodySize] : input.Sprites.Demiants3[32 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }
    }
}
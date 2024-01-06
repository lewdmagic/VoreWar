#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

internal static class Avians
{
    internal static readonly IRaceData Instance = RaceBuilder.CreateV2(Defaults.Default<OverSizeParameters>, builder =>
    {
        IClothing<IOverSizeParameters> leaderClothes = AvianLeader.AvianLeaderInstance;
        IClothing rags = AvianRags.AvianRagsInstance;


        builder.Setup(output =>
        {
            output.Names("Avian", "Avians");
            output.FlavorText(new FlavorText(
                new Texts {  },
                new Texts {  },
                new Texts {  },
                new Dictionary<string, string>
                {
                    [WeaponNames.Mace]        = "Knife",
                    [WeaponNames.Axe]         = "Sword",
                    [WeaponNames.SimpleBow]   = "Short Bow",
                    [WeaponNames.CompoundBow] = "Crossbow"
                }
            ));
            output.RaceTraits(new RaceTraits()
            {
                BodySize = 10,
                StomachSize = 14,
                HasTail = true,
                FavoredStat = Stat.Agility,
                RacialTraits = new List<Traits>()
                {
                    Traits.KeenShot,
                    Traits.Featherweight
                },
                RaceDescription = "",
            });
            output.CustomizeButtons((unit, buttons) =>
            {
                buttons.SetText(ButtonType.HairStyle, "Head Type");
                buttons.SetText(ButtonType.BodyAccessoryColor, "Beak Color");
                buttons.SetText(ButtonType.BodyAccessoryType, "Head Pattern");
                buttons.SetText(ButtonType.ExtraColor1, "Core Color");
                buttons.SetText(ButtonType.ExtraColor2, "Feather Color");
                buttons.SetText(ButtonType.BodyAccentTypes1, "Underwing Palettes");
            });
            output.TownNames(new List<string>
            {
                "Phoenix Nest",
                "Thunderbird City",
                "Swallow Falls",
                "Dovechester",
                "Garuda Rapids",
                "Duck Pond",
                "Cockatricefields",
                "Horus Springs",
                "Rocville",
                "Thothland",
                "Fort Aethon",
                "Siren Song",
                "Tengu Hills",
                "Strixport",
                "Eagleburg",
                "Flamingo Bay"
            });
            output.BodySizes = 4;
            output.HairStyles = 12;
            output.EyeTypes = 6;
            output.SpecialAccessoryCount = 4; // feather patterns
            output.HairColors = 0;
            output.MouthTypes = 0;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.AviansSkin); // claws color (black)
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.AviansSkin); // beak color (black)
            output.ExtraColors1 = ColorPaletteMap.GetPaletteCount(SwapType.AviansSkin); // primary feather colors (white)
            output.ExtraColors2 = ColorPaletteMap.GetPaletteCount(SwapType.AviansSkin); // secondary feather colors (grey)
            output.BodyAccentTypes1 = 4; // wings
            output.TailTypes = 16;

            output.ExtendedBreastSprites = true;

            output.AllowedMainClothingTypes.Set(
                GenericTop1.GenericTop1Instance,
                GenericTop2.GenericTop2Instance,
                GenericTop3.GenericTop3Instance,
                GenericTop4.GenericTop4Instance,
                GenericTop5.GenericTop5Instance,
                GenericTop6.GenericTop6Instance,
                MaleTop.MaleTopInstance,
                Natural.NaturalInstance,
                rags,
                leaderClothes
            );
            output.AvoidedMainClothingTypes = 2;

            output.AllowedWaistTypes.Set(
                GenericBot1.GenericBot1Instance,
                GenericBot2.GenericBot2Instance,
                GenericBot3.GenericBot3Instance,
                GenericBot4.GenericBot4Instance
            );

            output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.AviansSkin);
        });


        builder.RunBefore((input, output) =>
        {
            CommonRaceCode.MakeBreastOversize(32 * 32).Invoke(input, output);
            Defaults.BasicBellyRunAfter.Invoke(input, output);
        });

        builder.RenderSingle(SpriteType.Head, 14, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ExtraColor1));
            output.Sprite(input.Sprites.Avians2[0 + input.U.HairStyle + 12 * input.U.SpecialAccessoryType]);
        }); // head primary (white)
        builder.RenderSingle(SpriteType.Eyes, 15, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
            output.Sprite(input.Sprites.Avians2[132 + input.U.EyeType]);
        });
        builder.RenderSingle(SpriteType.SecondaryEyes, 16, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ExtraColor2));
            output.Sprite(input.Sprites.Avians2[138 + input.U.EyeType]);
        }); // eyebrows (grey/secondary)
        builder.RenderSingle(SpriteType.Mouth, 17, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.A.IsEating)
            {
                output.Sprite(input.Sprites.Avians2[120 + input.U.HairStyle]);
            }
        });

        builder.RenderSingle(SpriteType.Hair, 14, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ExtraColor2));
            output.Sprite(input.Sprites.Avians2[48 + input.U.HairStyle + 12 * Math.Min(input.U.SpecialAccessoryType, 4)]);
        }); // head secondary (grey)
        builder.RenderSingle(SpriteType.Body, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ExtraColor1));
            output.Sprite(input.U.HasBreasts ? input.Sprites.Avians1[0 + input.U.BodySize] : input.Sprites.Avians1[4 + input.U.BodySize]);
        }); // body (white/ primary)

        builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ExtraColor1));
            output.Sprite(input.A.IsAttacking ? input.Sprites.Avians1[37 + input.U.BodyAccentType1] : input.Sprites.Avians1[30 + input.U.BodyAccentType1]);
        }); // wings primary (white)

        builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ExtraColor2));
            output.Sprite(input.A.IsAttacking ? input.Sprites.Avians1[40 + input.U.BodyAccentType1] : input.Sprites.Avians1[33 + input.U.BodyAccentType1]);
        }); // wings secondary (grey)

        builder.RenderSingle(SpriteType.BodyAccent3, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ExtraColor1));
            if (input.U.TailType < 8)
            {
                output.Sprite(input.Sprites.Avians1[44 + input.U.TailType]);
            }
        }); // tail primary (white)

        builder.RenderSingle(SpriteType.BodyAccent4, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ExtraColor2));
            if (input.U.TailType >= 8)
            {
                output.Sprite(input.Sprites.Avians1[44 + input.U.TailType]);
            }
        }); // tail secondary (grey)

        builder.RenderSingle(SpriteType.BodyAccent5, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.SkinColor));
            output.Sprite(input.U.BodySize >= 2 ? input.Sprites.Avians1[29] : input.Sprites.Avians1[28]);
        }); // feet (black)

        builder.RenderSingle(SpriteType.BodyAccent6, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.SkinColor));
            if (input.A.IsAttacking)
            {
                output.Sprite(input.U.HasBreasts ? input.Sprites.Avians1[26] : input.Sprites.Avians1[27]);
            }
            else
            {
                output.Sprite(input.U.BodySize >= 2 ? input.Sprites.Avians1[25] : input.Sprites.Avians1[24]);
            }
        }); // claws (black)

        builder.RenderSingle(SpriteType.BodyAccent7, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ExtraColor2));
            output.Sprite(input.U.HasBreasts ? input.Sprites.Avians1[8 + input.U.BodySize] : input.Sprites.Avians1[12 + input.U.BodySize]);
        }); // legs (grey/ secondary)

        builder.RenderSingle(SpriteType.BodyAccent8, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ExtraColor2));
            if (input.A.IsAttacking)
            {
                output.Layer(3);
                if (input.U.HasBreasts)
                {
                    output.Sprite(input.U.BodySize >= 2 ? input.Sprites.Avians1[21] : input.Sprites.Avians1[20]);
                }
                else
                {
                    output.Sprite(input.U.BodySize >= 2 ? input.Sprites.Avians1[23] : input.Sprites.Avians1[22]);
                }
            }
            else
            {
                output.Layer(6);
                if (input.U.HasBreasts)
                {
                    output.Sprite(input.U.BodySize >= 2 ? input.Sprites.Avians1[17] : input.Sprites.Avians1[16]);
                }
                else
                {
                    output.Sprite(input.U.BodySize >= 2 ? input.Sprites.Avians1[19] : input.Sprites.Avians1[18]);
                }
            }
        }); // arms (grey/ secondary)

        builder.RenderSingle(SpriteType.BodyAccessory, 17, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.AccessoryColor));
            if (input.A.IsEating)
            {
                output.Sprite(input.Sprites.Avians2[108 + input.U.HairStyle]);
                return;
            }

            output.Sprite(input.Sprites.Avians2[96 + input.U.HairStyle]);
        }); // beak

        builder.RenderSingle(SpriteType.Breasts, 12, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ExtraColor1));
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

                output.Sprite(input.Sprites.Avians3[0 + leftSize]);
            }
            else
            {
                output.Sprite(input.Sprites.Avians3[0 + input.U.BreastSize]);
            }
        }); // breasts primary

        builder.RenderSingle(SpriteType.SecondaryBreasts, 12, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ExtraColor1));
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

                output.Sprite(input.Sprites.Avians3[32 + rightSize]);
            }
            else
            {
                output.Sprite(input.Sprites.Avians3[32 + input.U.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.Belly, 11, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ExtraColor1));
            if (input.A.HasBelly)
            {
                int size = input.A.GetStomachSize(31, 0.8f);
                
                switch (size)
                {
                    case 26:
                        output.AddOffset(0, -14 * .625f);
                        break;
                    case 27:
                        output.AddOffset(0, -17 * .625f);
                        break;
                    case 28:
                        output.AddOffset(0, -20 * .625f);
                        break;
                    case 29:
                        output.AddOffset(0, -25 * .625f);
                        break;
                    case 30:
                        output.AddOffset(0, -27 * .625f);
                        break;
                    case 31:
                        output.AddOffset(0, -33 * .625f);
                        break;
                }

                output.Sprite(input.Sprites.Avians3[64 + size]);
            }
        }); // belly primary

        builder.RenderSingle(SpriteType.Dick, 8, (input, output) =>
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
                    output.Layer(18);
                    output.Sprite(input.A.IsCockVoring ? input.Sprites.Avians1[84 + input.U.DickSize] : input.Sprites.Avians1[68 + input.U.DickSize]);
                }
                else
                {
                    output.Layer(10);
                    output.Sprite(input.A.IsCockVoring ? input.Sprites.Avians1[92 + input.U.DickSize] : input.Sprites.Avians1[76 + input.U.DickSize]);
                }
            }

            //output.Layer(8);
        });

        builder.RenderSingle(SpriteType.Balls, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ExtraColor1));
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
                output.Layer(7);
            }

            int size = input.U.DickSize;
            int offsetI = input.A.GetBallSize(28, .8f);

            if (offsetI >= 26)
            {
                output.AddOffset(0, -17 * .625f);
            }
            else if (offsetI == 25)
            {
                output.AddOffset(0, -13 * .625f);
            }
            else if (offsetI == 24)
            {
                output.AddOffset(0, -11 * .625f);
            }
            else if (offsetI == 23)
            {
                output.AddOffset(0, -10 * .625f);
            }
            else if (offsetI == 22)
            {
                output.AddOffset(0, -7 * .625f);
            }
            else if (offsetI == 21)
            {
                output.AddOffset(0, -6 * .625f);
            }
            else if (offsetI == 20)
            {
                output.AddOffset(0, -4 * .625f);
            }
            else if (offsetI == 19)
            {
                output.AddOffset(0, -1 * .625f);
            }

            if (offsetI > 0)
            {
                output.Sprite(input.Sprites.Avians1[Math.Min(108 + offsetI, 134)]);
                return;
            }

            output.Sprite(input.Sprites.Avians1[100 + size]);
        }); // balls primary

        builder.RenderSingle(SpriteType.Weapon, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.U.HasWeapon && input.A.Surrendered == false)
            {
                output.Sprite(input.Sprites.Avians1[60 + input.A.GetWeaponSprite()]);
            }
        });

        builder.RandomCustom(data =>
        {
            Unit unit = data.Unit;
            Defaults.RandomCustom(data);

            unit.BodyAccentType1 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes1);

            unit.AccessoryColor = unit.SkinColor;

            unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);

            unit.TailType = State.Rand.Next(data.MiscRaceData.TailTypes);

            if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
            {
                unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypesBasic, rags);
                if (unit.ClothingType == -1) //Covers rags not in the list
                {
                    unit.ClothingType = 1;
                }
            }

            if (unit.Type == UnitType.Leader)
            {
                unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypesBasic, leaderClothes);
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
                output["Clothing1"].Layer(13);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Avians4[23]);
                }
                else if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Avians4[15 + input.U.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
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
                output["Clothing1"].Layer(13);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Avians4[33]);
                }
                else if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Avians4[25 + input.U.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
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
                output["Clothing1"].Layer(13);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Avians4[43]);
                }
                else if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Avians4[35 + input.U.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
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
                output["Clothing1"].Layer(13);
                output["Clothing2"].Layer(13);
                output["Clothing2"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Avians4[53]);
                }
                else if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Avians4[45 + input.U.BreastSize]);
                }

                output["Clothing2"].Sprite(input.Sprites.Avians4[54]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
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
                output["Clothing1"].Layer(13);
                output["Clothing2"].Layer(13);
                output["Clothing2"].Coloring(Color.white);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(input.Sprites.Avians4[64]);
                    output["Clothing2"].Sprite(input.Sprites.Avians4[73]);
                }
                else if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Avians4[56 + input.U.BreastSize]);
                    output["Clothing2"].Sprite(input.Sprites.Avians4[65 + input.U.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
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
                output["Clothing1"].Layer(13);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(null);
                }
                else if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Avians4[80 + input.U.BreastSize]);
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
                output.Type = 1579;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(13);
                output["Clothing1"].Sprite(input.Sprites.Avians4[75 + input.U.BodySize]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
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
                output["Clothing2"].Layer(9);
                output["Clothing1"].Layer(13);
                if (input.Params.Oversize)
                {
                    output["Clothing1"].Sprite(null);
                    output["Clothing2"].Sprite(input.Sprites.Avians3[105]);
                }
                else if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Avians3[97 + input.U.BreastSize]);
                    output["Clothing2"].Sprite(input.Sprites.Avians3[105]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Avians3[106]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ExtraColor1));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ExtraColor1));
            });
        });
    }

    private static class AvianRags
    {
        internal static readonly IClothing AvianRagsInstance = ClothingBuilder.Create(builder =>
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
                output["Clothing3"].Layer(15);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing2"].Layer(9);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(13);
                output["Clothing1"].Coloring(Color.white);
                if (input.U.HasBreasts)
                {
                    if (input.U.BreastSize < 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Avians4[100]);
                    }
                    else if (input.U.BreastSize < 6)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Avians4[101]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Avians4[102]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Avians4[89 + input.U.BodySize]);
                    output["Clothing3"].Sprite(input.Sprites.Avians4[99]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Avians4[97]);
                    output["Clothing2"].Sprite(input.Sprites.Avians4[93 + input.U.BodySize]);
                    output["Clothing3"].Sprite(input.Sprites.Avians4[98]);
                }
            });
        });
    }

    private static class AvianLeader
    {
        internal static readonly IClothing<IOverSizeParameters> AvianLeaderInstance = ClothingBuilder.Create<IOverSizeParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.LeaderOnly = true;
                output.DiscardSprite = input.Sprites.Avians4[139];
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
                output.Type = 1539;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing4"].Layer(10);
                output["Clothing4"].Coloring(Color.white);
                output["Clothing3"].Layer(18);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing2"].Layer(9);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(13);
                output["Clothing1"].Coloring(Color.white);
                if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Params.Oversize ? input.Sprites.Avians4[111] : input.Sprites.Avians4[103 + input.U.BreastSize]);
                    output["Clothing2"].Sprite(input.Sprites.Avians4[112 + input.U.BodySize]);
                    output["Clothing3"].Sprite(input.Sprites.Avians4[120 + input.U.HairStyle]);
                }
                else
                {
                    if (input.U.DickSize < 3)
                    {
                        output["Clothing4"].Sprite(input.Sprites.Avians4[136]);
                    }
                    else if (input.U.DickSize > 5)
                    {
                        output["Clothing4"].Sprite(input.Sprites.Avians4[138]);
                    }
                    else
                    {
                        output["Clothing4"].Sprite(input.Sprites.Avians4[137]);
                    }

                    output["Clothing1"].Sprite(input.Sprites.Avians4[132 + input.U.BodySize]);
                    output["Clothing2"].Sprite(input.Sprites.Avians4[116 + input.U.BodySize]);
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
                output["Clothing2"].Layer(9);

                output["Clothing1"].Layer(10);

                if (input.U.HasBreasts)
                {
                    if (input.U.DickSize > 0)
                    {
                        if (input.U.DickSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Avians3[115]);
                        }
                        else if (input.U.DickSize > 5)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Avians3[117]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Avians3[116]);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(null);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Avians3[107 + input.U.BodySize]);
                }
                else
                {
                    if (input.U.DickSize < 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Avians3[118]);
                    }
                    else if (input.U.DickSize > 5)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Avians3[120]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Avians3[119]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Avians3[111 + input.U.BodySize]);
                }

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
                output.Type = 1537;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);

                output["Clothing2"].Layer(9);

                output["Clothing2"].Coloring(Color.white);

                if (input.U.HasBreasts)
                {
                    if (input.U.DickSize > 0)
                    {
                        if (input.U.DickSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Avians3[131]);
                        }
                        else if (input.U.DickSize > 5)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Avians3[133]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Avians3[132]);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Avians3[130]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Avians3[122 + input.U.BodySize]);
                }
                else
                {
                    if (input.U.DickSize < 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Avians3[134]);
                    }
                    else if (input.U.DickSize > 5)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Avians3[136]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Avians3[135]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Avians3[126 + input.U.BodySize]);
                }

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
                output.RevealsDick = true;
                output.InFrontOfDick = true;
                output.Type = 1540;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);

                output["Clothing2"].Layer(9);

                output["Clothing2"].Coloring(Color.white);

                if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Avians3[138]);
                    output["Clothing2"].Sprite(input.Sprites.Avians3[122 + input.U.BodySize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Avians3[139]);
                    output["Clothing2"].Sprite(input.Sprites.Avians3[126 + input.U.BodySize]);
                }

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
                output.Type = 1514;
                output.DiscardUsesPalettes = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(9);

                output["Clothing1"].Layer(10);

                if (input.U.HasBreasts)
                {
                    if (input.U.DickSize > 0)
                    {
                        if (input.U.DickSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Avians4[8]);
                        }
                        else if (input.U.DickSize > 5)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Avians4[10]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Avians4[9]);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(null);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Avians4[0 + input.U.BodySize]);
                }
                else
                {
                    if (input.U.DickSize < 3)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Avians4[11]);
                    }
                    else if (input.U.DickSize > 5)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Avians4[13]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Avians4[12]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Avians4[4 + input.U.BodySize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
            });
        });
    }
}
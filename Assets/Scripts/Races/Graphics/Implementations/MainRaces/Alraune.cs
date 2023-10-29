#region

using System;
using AlrauneClothing;
using UnityEngine;

#endregion

internal static class Alraune
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Default, builder =>
    {
        float yOffset = 10 * .625f;
        IClothing LeaderClothes = AlrauneLeader.AlrauneLeaderInstance;
        IClothing Rags = AlrauneRags.AlrauneRagsInstance;


        builder.RandomCustom(data =>
        {
            Unit unit = data.Unit;
            Defaults.RandomCustom(data);

            if (Config.RagsForSlaves && State.World?.MainEmpires != null &&
                (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) &&
                unit.ImmuneToDefections == false)
            {
                unit.ClothingType = 1 + data.MiscRaceData.AllowedMainClothingTypes.IndexOf(Rags);
                if (unit.ClothingType == -1) //Covers rags not in the list
                {
                    unit.ClothingType = 1;
                }
            }

            if (unit.Type == UnitType.Leader)
            {
                unit.ClothingType = 1 + data.MiscRaceData.AllowedMainClothingTypes.IndexOf(LeaderClothes);
            }

            if (unit.HasDick && unit.HasBreasts)
            {
                if (Config.HermsOnlyUseFemaleHair)
                {
                    unit.HairStyle = State.Rand.Next(5);
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
                    unit.HairStyle = 5 + State.Rand.Next(7);
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(5);
                }
            }

            if (State.Rand.Next(2) == 0)
            {
                unit.BodyAccentType1 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes1 - 1);
            }
            else
            {
                unit.BodyAccentType1 = data.MiscRaceData.BodyAccentTypes1 - 1;
            }

            unit.BodyAccentType2 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes2);
            unit.BodyAccentType3 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes3);
        });

        builder.Setup(output =>
        {
            output.BodySizes = 4;
            output.HairStyles = 12;
            output.SpecialAccessoryCount = 16;
            output.AccessoryColors =
                ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType
                    .AlrauneFoliage); // head flower and upper petals
            output.HairColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AlrauneHair);
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AlrauneSkin);
            output.ExtraColors1 =
                ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AlrauneFoliage); // lower petals and base roots
            output.BodyAccentTypes1 = 9; // upper petals
            output.BodyAccentTypes2 = 10; // lower petals
            output.BodyAccentTypes3 = 8; // base roots

            output.AllowedMainClothingTypes.Set(
                AlrauneLeafs.AlrauneLeafsInstance,
                AlrauneVines1.AlrauneVines1Instance,
                AlrauneVines2.AlrauneVines2Instance,
                AlrauneMoss.AlrauneMossInstance,
                AlrauneChristmas.AlrauneChristmasInstance,
                Rags,
                LeaderClothes
            );

            output.AllowedWaistTypes.Set(
            );

            output.AllowedClothingHatTypes.Clear();
            output.AvoidedMainClothingTypes = 2;
            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.AlrauneFoliage);
        });


        builder.RenderSingle(SpriteType.Head, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneSkin, input.Actor.Unit.SkinColor));
            if (input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Alraune[16]);
            }
        });

        builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Eyes].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            output.AddOffset(0, yOffset);
        });
        builder.RenderSingle(SpriteType.Mouth, 4, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Mouth].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneSkin, input.Actor.Unit.SkinColor));
            output.AddOffset(0, yOffset);
        });
        
        builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneHair, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Alraune[60 + input.Actor.Unit.HairStyle]);
        });
        builder.RenderSingle(SpriteType.Body, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneSkin, input.Actor.Unit.SkinColor));
                if (input.Actor.Unit.HasBreasts)
                {
                    output.Sprite(
                        input.Sprites.Alraune[0 + (input.Actor.IsAttacking ? 1 : 0) + 2 * input.Actor.Unit.BodySize]);
                }
                else
                {
                    output.Sprite(
                        input.Sprites.Alraune[8 + (input.Actor.IsAttacking ? 1 : 0) + 2 * input.Actor.Unit.BodySize]);
                }
            });

        builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, input.Actor.Unit.AccessoryColor));
                if (input.Actor.Unit.ClothingType != 5)
                {
                    output.Sprite(input.Sprites.Alraune[17 + input.Actor.Unit.BodyAccentType1]);
                }
            }); // upper petals

        builder.RenderSingle(SpriteType.BodyAccent2, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, input.Actor.Unit.ExtraColor1));
                if (input.Actor.Unit.ClothingType != 5)
                {
                    output.Sprite(input.Sprites.Alraune[26 + input.Actor.Unit.BodyAccentType2]);
                }
            }); //lower petals

        builder.RenderSingle(SpriteType.BodyAccent3, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, input.Actor.Unit.ExtraColor1));
                if (input.Actor.Unit.ClothingType != 5)
                {
                    output.Sprite(input.Sprites.Alraune[36 + input.Actor.Unit.BodyAccentType3]);
                }
            }); // base roots

        builder.RenderSingle(SpriteType.BodyAccent4, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneHair, input.Actor.Unit.HairColor));
                output.Sprite(
                    input.Sprites.Eyebrows[Math.Min(input.Actor.Unit.EyeType, input.Sprites.Eyebrows.Length - 1)]);
                output.AddOffset(0, yOffset);
            }); // eyebrows

        builder.RenderSingle(SpriteType.BodyAccent5, 7, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.ClothingType == 5)
            {
                output.Sprite(input.Sprites.AlrauneChristmas[12]);
            }
        }); // christmas head flower

        builder.RenderSingle(SpriteType.BodyAccent6, 2, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.ClothingType == 5)
            {
                output.Sprite(input.Sprites.AlrauneChristmas[1]);
            }
        }); // christmas lower petals

        builder.RenderSingle(SpriteType.BodyAccent7, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.ClothingType == 5)
            {
                output.Sprite(input.Sprites.AlrauneChristmas[0]);
            }
        }); // christmas base roots

        builder.RenderSingle(SpriteType.BodyAccessory, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, input.Actor.Unit.AccessoryColor));
                if (input.Actor.Unit.HasBreasts && input.Actor.Unit.ClothingType != 5)
                {
                    output.Sprite(input.Sprites.Alraune[44 + input.Actor.Unit.SpecialAccessoryType]);
                }
            }); // head flower

        builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
            {
                Defaults.SpriteGens2[SpriteType.Breasts].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneSkin, input.Actor.Unit.SkinColor));
                output.AddOffset(0, yOffset);
            });
        builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
            {
                Defaults.SpriteGens2[SpriteType.Belly].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneSkin, input.Actor.Unit.SkinColor));
                output.AddOffset(0, yOffset);
            });
        builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
            {
                Defaults.SpriteGens2[SpriteType.Dick].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneSkin, input.Actor.Unit.SkinColor));
                output.AddOffset(0, yOffset);
            });
        builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
            {
                Defaults.SpriteGens2[SpriteType.Balls].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneSkin, input.Actor.Unit.SkinColor));
                output.AddOffset(0, yOffset);
            });
        builder.RenderSingle(SpriteType.Weapon, 12, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, input.Actor.Unit.ClothingColor));
                if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
                {
                    output.Sprite(input.Sprites.Alraune[72 + input.Actor.GetWeaponSprite()]);
                }
            });
    });
}

namespace AlrauneClothing
{
    internal static class AlrauneLeafs
    {
        internal static IClothing AlrauneLeafsInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = null;
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(10);
                output["Clothing1"].Layer(17);
                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Alraune[84 + input.Actor.Unit.BreastSize]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, input.Actor.Unit.ClothingColor));
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }


                if (input.Actor.Unit.DickSize > 0)
                {
                    if (input.Actor.Unit.DickSize < 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[80]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[82]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[81]);
                    }
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Alraune[83]);
                }

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, input.Actor.Unit.ClothingColor));
            });
        });
    }

    internal static class AlrauneVines1
    {
        internal static IClothing AlrauneVines1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = null;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.InFrontOfDick = true;
                output.OccupiesAllSlots = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(10);
                output["Clothing1"].Layer(17);
                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Alraune[96 + input.Actor.Unit.BreastSize]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, input.Actor.Unit.ClothingColor));
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }


                if (input.Actor.Unit.DickSize > 0)
                {
                    if (input.Actor.Unit.DickSize < 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[92]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[94]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[93]);
                    }
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Alraune[95]);
                }

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, input.Actor.Unit.ClothingColor));
            });
        });
    }

    internal static class AlrauneVines2
    {
        internal static IClothing AlrauneVines2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = null;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.InFrontOfDick = true;
                output.OccupiesAllSlots = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(10);
                output["Clothing1"].Layer(17);
                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Alraune[108 + input.Actor.Unit.BreastSize]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, input.Actor.Unit.ClothingColor));
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }


                if (input.Actor.Unit.DickSize > 0)
                {
                    if (input.Actor.Unit.DickSize < 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[104]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[106]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[105]);
                    }
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Alraune[107]);
                }

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, input.Actor.Unit.ClothingColor));
            });
        });
    }

    internal static class AlrauneMoss
    {
        internal static IClothing AlrauneMossInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = null;
                output.OccupiesAllSlots = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(10);
                output["Clothing1"].Layer(17);
                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Alraune[120 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Alraune[120]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, input.Actor.Unit.ClothingColor));

                if (input.Actor.Unit.DickSize > 0)
                {
                    if (input.Actor.Unit.DickSize < 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[116]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[118]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[117]);
                    }
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Alraune[119]);
                }

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, input.Actor.Unit.ClothingColor));
            });
        });
    }

    internal static class AlrauneChristmas
    {
        internal static IClothing AlrauneChristmasInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = null;
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
                output.ReqWinterHoliday = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(17);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing2"].Layer(10);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.AlrauneChristmas[2 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                output["Clothing2"].Sprite(input.Sprites.AlrauneChristmas[10]);

                output["Clothing3"].Sprite(input.Sprites.AlrauneChristmas[11]);
            });
        });
    }

    internal static class AlrauneRags
    {
        internal static IClothing AlrauneRagsInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Rags[23];
                output.OccupiesAllSlots = true;
                output.Type = 207;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(18);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing2"].Layer(10);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Alraune[144 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                if (input.Actor.Unit.DickSize > 0)
                {
                    if (input.Actor.Unit.DickSize < 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[140]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[142]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[141]);
                    }
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Alraune[143]);
                }

                output["Clothing3"].Sprite(input.Sprites.Alraune[152]);
            });
        });
    }

    internal static class AlrauneLeader
    {
        internal static IClothing AlrauneLeaderInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.LeaderOnly = true;
                output.DiscardSprite = null;
                output.OccupiesAllSlots = true;
                output.RevealsBreasts = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(10);
                output["Clothing1"].Layer(17);
                if (input.Actor.Unit.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Alraune[132 + input.Actor.Unit.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Alraune[132]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, input.Actor.Unit.ClothingColor));

                if (input.Actor.Unit.DickSize > 0)
                {
                    if (input.Actor.Unit.DickSize < 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[128]);
                    }
                    else if (input.Actor.Unit.DickSize > 5)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[130]);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[129]);
                    }
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Alraune[131]);
                }

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneFoliage, input.Actor.Unit.ClothingColor));
            });
        });
    }
}
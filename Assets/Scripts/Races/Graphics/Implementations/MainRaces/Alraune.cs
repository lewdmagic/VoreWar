#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion
namespace Races.Graphics.Implementations.MainRaces
{


    internal static class Alraune
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {

            float yOffset = 10 * .625f;
            IClothing leaderClothes = AlrauneLeader.AlrauneLeaderInstance;
            IClothing rags = AlrauneRags.AlrauneRagsInstance;


            builder.RandomCustom(data =>
            {
                Unit unit = data.Unit;
                Defaults.RandomCustom(data);

                if (Config.RagsForSlaves && State.World?.MainEmpires != null &&
                    (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) &&
                    unit.ImmuneToDefections == false)
                {
                    unit.ClothingType = 1 + data.MiscRaceData.AllowedMainClothingTypes.IndexOf(rags);
                    if (unit.ClothingType == -1) //Covers rags not in the list
                    {
                        unit.ClothingType = 1;
                    }
                }

                if (unit.Type == UnitType.Leader)
                {
                    unit.ClothingType = 1 + data.MiscRaceData.AllowedMainClothingTypes.IndexOf(leaderClothes);
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
                output.Names("Alraune", "Alraune");
                output.FlavorText(new FlavorText(
                    new Texts {  },
                    new Texts {  },
                    new Texts { "plant", "demi-plant", "flowery being" },
                    new Dictionary<string, string>
                    {
                        [WeaponNames.Mace]        = "Vine Whip",
                        [WeaponNames.Axe]         = "Stem Blade",
                        [WeaponNames.SimpleBow]   = "Unbloomed Corolla",
                        [WeaponNames.CompoundBow] = "Blooming Flower"
                    }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 14,
                    StomachSize = 16,
                    HasTail = false,
                    FavoredStat = Stat.Endurance,
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Tempered,
                        TraitType.SlowAbsorption,
                        TraitType.PollenProjector
                    },
                    RaceDescription = "",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.BodyAccessoryType, "Hair Accessory");
                    buttons.SetText(ButtonType.ExtraColor1, "Plant Colors");
                    buttons.SetText(ButtonType.BodyAccentTypes1, "Inner Petals");
                    buttons.SetText(ButtonType.BodyAccentTypes2, "Outer Petals");
                    buttons.SetText(ButtonType.BodyAccentTypes3, "Plant Base");
                });
                output.TownNames(new List<string>
                {
                    "Yggdrasill",
                    "Evergarden",
                    "Rosewood",
                    "Apple Grove",
                    "Gracefields",
                    "Edenia",
                    "Ivydale",
                    "Magnolia",
                    "Cedarville",
                    "Fiore",
                    "Trees of Valinor",
                    "Green Haven",
                });
            
                output.BodySizes = 4;
                output.HairStyles = 12;
                output.SpecialAccessoryCount = 16;
                output.AccessoryColors =
                    ColorPaletteMap.GetPaletteCount(SwapType
                        .AlrauneFoliage); // head flower and upper petals
                output.HairColors = ColorPaletteMap.GetPaletteCount(SwapType.AlrauneHair);
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.AlrauneSkin);
                output.ExtraColors1 =
                    ColorPaletteMap.GetPaletteCount(SwapType.AlrauneFoliage); // lower petals and base roots
                output.BodyAccentTypes1 = 9; // upper petals
                output.BodyAccentTypes2 = 10; // lower petals
                output.BodyAccentTypes3 = 8; // base roots

                output.AllowedMainClothingTypes.Set(
                    AlrauneLeafs.AlrauneLeafsInstance,
                    AlrauneVines1.AlrauneVines1Instance,
                    AlrauneVines2.AlrauneVines2Instance,
                    AlrauneMoss.AlrauneMossInstance,
                    AlrauneChristmas.AlrauneChristmasInstance,
                    rags,
                    leaderClothes
                );

                output.AllowedWaistTypes.Set(
                );

                output.AllowedClothingHatTypes.Clear();
                output.AvoidedMainClothingTypes = 2;
                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.AlrauneFoliage);
            });


            builder.RenderSingle(SpriteType.Head, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneSkin, input.U.SkinColor));
                if (input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Alraune[16]);
                }
            });

            builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Eyes].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
                output.AddOffset(0, yOffset);
            });
            builder.RenderSingle(SpriteType.Mouth, 4, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Mouth].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneSkin, input.U.SkinColor));
                output.AddOffset(0, yOffset);
            });
        
            builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneHair, input.U.HairColor));
                output.Sprite(input.Sprites.Alraune[60 + input.U.HairStyle]);
            });
            builder.RenderSingle(SpriteType.Body, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneSkin, input.U.SkinColor));
                if (input.U.HasBreasts)
                {
                    output.Sprite(
                        input.Sprites.Alraune[0 + (input.A.IsAttacking ? 1 : 0) + 2 * input.U.BodySize]);
                }
                else
                {
                    output.Sprite(
                        input.Sprites.Alraune[8 + (input.A.IsAttacking ? 1 : 0) + 2 * input.U.BodySize]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneFoliage, input.U.AccessoryColor));
                if (input.U.ClothingType != 5)
                {
                    output.Sprite(input.Sprites.Alraune[17 + input.U.BodyAccentType1]);
                }
            }); // upper petals

            builder.RenderSingle(SpriteType.BodyAccent2, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneFoliage, input.U.ExtraColor1));
                if (input.U.ClothingType != 5)
                {
                    output.Sprite(input.Sprites.Alraune[26 + input.U.BodyAccentType2]);
                }
            }); //lower petals

            builder.RenderSingle(SpriteType.BodyAccent3, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneFoliage, input.U.ExtraColor1));
                if (input.U.ClothingType != 5)
                {
                    output.Sprite(input.Sprites.Alraune[36 + input.U.BodyAccentType3]);
                }
            }); // base roots

            builder.RenderSingle(SpriteType.BodyAccent4, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneHair, input.U.HairColor));
                output.Sprite(
                    input.Sprites.Eyebrows[Math.Min(input.U.EyeType, input.Sprites.Eyebrows.Length - 1)]);
                output.AddOffset(0, yOffset);
            }); // eyebrows

            builder.RenderSingle(SpriteType.BodyAccent5, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.ClothingType == 5)
                {
                    output.Sprite(input.Sprites.AlrauneChristmas[12]);
                }
            }); // christmas head flower

            builder.RenderSingle(SpriteType.BodyAccent6, 2, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.ClothingType == 5)
                {
                    output.Sprite(input.Sprites.AlrauneChristmas[1]);
                }
            }); // christmas lower petals

            builder.RenderSingle(SpriteType.BodyAccent7, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.ClothingType == 5)
                {
                    output.Sprite(input.Sprites.AlrauneChristmas[0]);
                }
            }); // christmas base roots

            builder.RenderSingle(SpriteType.BodyAccessory, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneFoliage, input.U.AccessoryColor));
                if (input.U.HasBreasts && input.U.ClothingType != 5)
                {
                    output.Sprite(input.Sprites.Alraune[44 + input.U.SpecialAccessoryType]);
                }
            }); // head flower

            builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Breasts].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneSkin, input.U.SkinColor));
                output.AddOffset(0, yOffset);
            });
            builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Belly].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneSkin, input.U.SkinColor));
                output.AddOffset(0, yOffset);
            });
            builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Dick].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneSkin, input.U.SkinColor));
                output.AddOffset(0, yOffset);
            });
            builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Balls].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneSkin, input.U.SkinColor));
                output.AddOffset(0, yOffset);
            });
            builder.RenderSingle(SpriteType.Weapon, 12, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneFoliage, input.U.ClothingColor));
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    output.Sprite(input.Sprites.Alraune[72 + input.A.GetWeaponSprite()]);
                }
            });
        });
    }

    internal static class AlrauneLeafs
    {
        internal static readonly IClothing AlrauneLeafsInstance = ClothingBuilder.Create(builder =>
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
                if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Alraune[84 + input.U.BreastSize]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneFoliage, input.U.ClothingColor));
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }


                if (input.U.DickSize > 0)
                {
                    if (input.U.DickSize < 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[80]);
                    }
                    else if (input.U.DickSize > 5)
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

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneFoliage, input.U.ClothingColor));
            });
        });
    }

    internal static class AlrauneVines1
    {
        internal static readonly IClothing AlrauneVines1Instance = ClothingBuilder.Create(builder =>
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
                if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Alraune[96 + input.U.BreastSize]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneFoliage, input.U.ClothingColor));
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }


                if (input.U.DickSize > 0)
                {
                    if (input.U.DickSize < 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[92]);
                    }
                    else if (input.U.DickSize > 5)
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

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneFoliage, input.U.ClothingColor));
            });
        });
    }

    internal static class AlrauneVines2
    {
        internal static readonly IClothing AlrauneVines2Instance = ClothingBuilder.Create(builder =>
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
                if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Alraune[108 + input.U.BreastSize]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneFoliage, input.U.ClothingColor));
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }


                if (input.U.DickSize > 0)
                {
                    if (input.U.DickSize < 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[104]);
                    }
                    else if (input.U.DickSize > 5)
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

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneFoliage, input.U.ClothingColor));
            });
        });
    }

    internal static class AlrauneMoss
    {
        internal static readonly IClothing AlrauneMossInstance = ClothingBuilder.Create(builder =>
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
                output["Clothing1"].Sprite(input.U.HasBreasts ? input.Sprites.Alraune[120 + input.U.BreastSize] : input.Sprites.Alraune[120]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneFoliage, input.U.ClothingColor));

                if (input.U.DickSize > 0)
                {
                    if (input.U.DickSize < 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[116]);
                    }
                    else if (input.U.DickSize > 5)
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

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneFoliage, input.U.ClothingColor));
            });
        });
    }

    internal static class AlrauneChristmas
    {
        internal static readonly IClothing AlrauneChristmasInstance = ClothingBuilder.Create(builder =>
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
                if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.AlrauneChristmas[2 + input.U.BreastSize]);
                }

                output["Clothing2"].Sprite(input.Sprites.AlrauneChristmas[10]);

                output["Clothing3"].Sprite(input.Sprites.AlrauneChristmas[11]);
            });
        });
    }

    internal static class AlrauneRags
    {
        internal static readonly IClothing AlrauneRagsInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.Rags[23];
                output.OccupiesAllSlots = true;
                output.ClothingId = new ClothingId("base.alraune/207");
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
                if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.Alraune[144 + input.U.BreastSize]);
                }

                if (input.U.DickSize > 0)
                {
                    if (input.U.DickSize < 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[140]);
                    }
                    else if (input.U.DickSize > 5)
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
        internal static readonly IClothing AlrauneLeaderInstance = ClothingBuilder.Create(builder =>
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
                output["Clothing1"].Sprite(input.U.HasBreasts ? input.Sprites.Alraune[132 + input.U.BreastSize] : input.Sprites.Alraune[132]);

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneFoliage, input.U.ClothingColor));

                if (input.U.DickSize > 0)
                {
                    if (input.U.DickSize < 3)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Alraune[128]);
                    }
                    else if (input.U.DickSize > 5)
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

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneFoliage, input.U.ClothingColor));
            });
        });
    }
}
#region

using System.Collections.Generic;
using UnityEngine;

namespace Races.Graphics.Implementations.MainRaces
{

    #endregion

    internal static class Driders
    {
        internal static readonly IRaceData Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            float yOffset = 30 * .625f;
            IClothing leaderClothes = DriderLeader.DriderLeaderInstance;


            builder.Setup(output =>
            {
                output.Names("Drider", "Driders");
                output.FlavorText(new FlavorText(
                    new Texts {  },
                    new Texts {  },
                    new Texts {  },
                    new Dictionary<string, string>
                    {
                        [WeaponNames.Mace]        = "Dagger",
                        [WeaponNames.Axe]         = "Short Sword",
                        [WeaponNames.SimpleBow]   = "Pistol Crossbow",
                        [WeaponNames.CompoundBow] = "Crossbow"
                    }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 20,
                    StomachSize = 20,
                    HasTail = false,
                    FavoredStat = Stat.Strength,
                    RacialTraits = new List<Traits>()
                    {
                        //Traits.StrongMelee,
                        Traits.NimbleClimber,
                        Traits.Webber,
                    },
                    RaceDescription = "",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.BodyAccessoryColor, "Spider Half Color");
                    buttons.SetText(ButtonType.ExtraColor1, "Spider Accent Color");
                });
                output.TownNames(new List<string>
                {
                    "Arachnos",
                    "Weaverville",
                    "Silkroad",
                    "Tarantulos",
                    "Shelob's Lair",
                    "Araneae",
                    "Webbington",
                    "Dark Caves",
                    "Flytrap",
                    "Net town",
                    "Aragog Forest",
                    "Spiderverse",
                });
                output.BodySizes = 5;
                output.EyeTypes = 8;
                output.HairStyles = 10;
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.DriderSkin); // abdomen and legs colors
                output.EyeColors = ColorPaletteMap.GetPaletteCount(SwapType.DriderEyes); // drider special eyes colors
                output.ExtraColors1 = ColorPaletteMap.GetPaletteCount(SwapType.DriderEyes); // abdomen patterns colors

                output.ClothingShift = new Vector3(0, yOffset, 0);
                output.AvoidedMainClothingTypes = 2;
                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.Clothing);
                output.AllowedMainClothingTypes.Set(
                    CommonClothing.BikiniTopInstance,
                    CommonClothing.BeltTopInstance,
                    CommonClothing.StrapTopInstance,
                    CommonClothing.BlackTopInstance,
                    CommonClothing.RagsInstance,
                    leaderClothes
                );
                output.AllowedWaistTypes.Set(
                    CommonClothing.LoinclothInstance
                );
            });


            //it should first start with appeareance of sprite 73 (open spinneret) then it should change into sprite 74 (web gathering in the spinneret). After that it should shoot the web (sprite 75)
            // additionaly bodyaccentsprite2, 5 and 6 should also switch accordingly to "voring" settings if possible. No need for headsprite to appear though

            builder.RenderSingle(SpriteType.Head, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DriderSkin, input.U.AccessoryColor));
                if (input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Driders[23]);
                }
            });

            builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DriderEyes, input.U.EyeColor));
                output.Sprite(input.Sprites.Driders[28 + input.U.EyeType]);
            });
            builder.RenderSingle(SpriteType.Mouth, 4, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Mouth].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
                output.AddOffset(0, yOffset);
            });
            builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.NormalHair, input.U.HairColor));
                output.Sprite(input.Sprites.Driders[38 + input.U.HairStyle]);
            });
            builder.RenderSingle(SpriteType.Body, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
                if (input.U.HasBreasts)
                {
                    output.Sprite(input.Sprites.Driders[0 + (input.A.IsAttacking ? 1 : 0) + 2 * input.U.BodySize]);
                }
                else
                {
                    if (input.U.BodySize < 1)
                    {
                        output.Sprite(input.Sprites.Driders[0 + (input.A.IsAttacking ? 1 : 0)]);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.Driders[8 + (input.A.IsAttacking ? 1 : 0) + 2 * input.U.BodySize]);
                    }
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DriderSkin, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Driders[24]);
            }); //Back Legs
            builder.RenderSingle(SpriteType.BodyAccent2, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DriderSkin, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Driders[25 + input.A.GetSimpleBodySprite()]);
            }); //Back of front Legs
            builder.RenderSingle(SpriteType.BodyAccent3, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DriderEyes, input.U.EyeColor));
                output.Sprite(input.Sprites.Driders[36]);
            }); // extra Eyes
            builder.RenderSingle(SpriteType.BodyAccent4, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.NormalHair, input.U.HairColor));
                output.Sprite(input.Sprites.Driders[37]);
            }); //eyebrow
            builder.RenderSingle(SpriteType.BodyAccent5, 18, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    output.Sprite(input.Sprites.Driders[61 + input.A.GetSimpleBodySprite() + 3 * (input.A.GetWeaponSprite() / 2)]);
                }
            }); //Extra Leg Accessories

            builder.RenderSingle(SpriteType.BodyAccent6, 17, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DriderSkin, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Driders[98 + input.A.GetSimpleBodySprite()]);
            }); //Front of front Legs
            //builder.SetSpriteInfo(SpriteType.BodyAccent7, 7, Defaults.WhiteColored, null, (input, output) => null); //For Spider Web attack animation???
            builder.RenderSingle(SpriteType.BodyAccessory, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DriderSkin, input.U.AccessoryColor));
                if (input.U.HasBreasts)
                {
                    output.Sprite(input.Sprites.Driders[18 + input.U.BodySize]);
                }
                else
                {
                    output.Sprite(input.U.BodySize < 2 ? input.Sprites.Driders[18] : input.Sprites.Driders[17 + input.U.BodySize]);
                }
            }); //abdomen

            builder.RenderSingle(SpriteType.SecondaryAccessory, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DriderEyes, input.U.ExtraColor1));
                if (input.U.HasBreasts)
                {
                    output.Sprite(input.Sprites.Driders[48 + input.U.BodySize]);
                }
                else
                {
                    if (input.U.BodySize < 2)
                    {
                        output.Sprite(input.Sprites.Driders[48]);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.Driders[47 + input.U.BodySize]);
                    }
                }
            }); // abdomen patterns

            builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Belly].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.AlrauneSkin, input.U.SkinColor));
                output.AddOffset(0, yOffset);
            });

            builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Breasts].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
                output.AddOffset(0, yOffset);
            });
            builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Belly].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
                output.AddOffset(0, yOffset);
            });
            builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Dick].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
                output.AddOffset(0, yOffset);
            });
            builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Balls].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Skin, input.U.SkinColor));
                output.AddOffset(0, yOffset);
            });
            builder.RenderSingle(SpriteType.Weapon, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    output.Sprite(input.Sprites.Driders[53 + input.A.GetWeaponSprite()]);
                }
            });

            builder.RandomCustom(data =>
            {
                Unit unit = data.Unit;
                Defaults.RandomCustom(data);

                if (unit.Type == UnitType.Leader)
                {
                    unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypesBasic, leaderClothes);
                }

                if (unit.HasDick && unit.HasBreasts)
                {
                    unit.HairStyle = State.Rand.Next(Config.HermsOnlyUseFemaleHair ? 5 : data.MiscRaceData.HairStyles);
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
                        unit.HairStyle = 5 + State.Rand.Next(5);
                    }
                    else
                    {
                        unit.HairStyle = State.Rand.Next(5);
                    }
                }
            });
        });
    }

    internal static class DriderLeader
    {
        internal static readonly IClothing DriderLeaderInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.LeaderOnly = true;
                output.FixedColor = true;
                output.RevealsDick = true;
                output.InFrontOfDick = true;
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
                output.DiscardSprite = input.Sprites.Driders[96];
                output.Type = 236;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(11);

                output["Clothing3"].Coloring(Color.white);

                output["Clothing2"].Layer(17);

                output["Clothing2"].Coloring(Color.white);

                output["Clothing1"].Layer(10);

                output["Clothing1"].Coloring(Color.white);

                if (input.U.HasBreasts)
                {
                    // output.DiscardSprite = input.Sprites.Driders[97];
                    // output.Type = 237;
                    // TODO this was not a robust way of doing this. Changing discards dynamically isnt currently supported 
                    output["Clothing1"].Sprite(input.Sprites.Driders[76]);
                    output["Clothing2"].Sprite(input.Sprites.Driders[87 + input.U.BreastSize]);
                    output["Clothing3"].Sprite(input.Sprites.Driders[82 + input.U.BodySize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Driders[95]);
                    output["Clothing2"].Sprite(null);
                    output["Clothing3"].Sprite(input.Sprites.Driders[77 + input.U.BodySize]);
                }
            });
        });
    }
}
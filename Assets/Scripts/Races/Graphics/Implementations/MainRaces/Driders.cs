#region

using DriderClothing;
using UnityEngine;

#endregion

internal static class Driders
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Default, builder =>
    {
        float yOffset = 30 * .625f;
        IClothing LeaderClothes = DriderLeader.DriderLeaderInstance;


        builder.Setup(output =>
        {
            output.BodySizes = 5;
            output.EyeTypes = 8;
            output.HairStyles = 10;
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DriderSkin); // abdomen and legs colors
            output.EyeColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DriderEyes); // drider special eyes colors
            output.ExtraColors1 = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.DriderEyes); // abdomen patterns colors

            output.ClothingShift = new Vector3(0, yOffset, 0);
            output.AvoidedMainClothingTypes = 2;
            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing);
            output.AllowedMainClothingTypes.Set(
                ClothingTypes.BikiniTopInstance,
                ClothingTypes.BeltTopInstance,
                ClothingTypes.StrapTopInstance,
                ClothingTypes.BlackTopInstance,
                ClothingTypes.RagsInstance,
                LeaderClothes
            );
            output.AllowedWaistTypes.Set(
                ClothingTypes.LoinclothInstance
            );
        });


        //it should first start with appeareance of sprite 73 (open spinneret) then it should change into sprite 74 (web gathering in the spinneret). After that it should shoot the web (sprite 75)
        // additionaly bodyaccentsprite2, 5 and 6 should also switch accordingly to "voring" settings if possible. No need for headsprite to appear though

        builder.RenderSingle(SpriteType.Head, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DriderSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.IsEating)
            {
                output.Sprite(input.Sprites.Driders[23]);
            }
        });

        builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DriderEyes, input.Actor.Unit.EyeColor));
            output.Sprite(input.Sprites.Driders[28 + input.Actor.Unit.EyeType]);
        });
        builder.RenderSingle(SpriteType.Mouth, 4, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Mouth].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
            output.AddOffset(0, yOffset);
        });
        builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Driders[38 + input.Actor.Unit.HairStyle]);
        });
        builder.RenderSingle(SpriteType.Body, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Sprites.Driders[0 + (input.Actor.IsAttacking ? 1 : 0) + 2 * input.Actor.Unit.BodySize]);
            }
            else
            {
                if (input.Actor.Unit.BodySize < 1)
                {
                    output.Sprite(input.Sprites.Driders[0 + (input.Actor.IsAttacking ? 1 : 0)]);
                }
                else
                {
                    output.Sprite(input.Sprites.Driders[8 + (input.Actor.IsAttacking ? 1 : 0) + 2 * input.Actor.Unit.BodySize]);
                }
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DriderSkin, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Driders[24]);
        }); //Back Legs
        builder.RenderSingle(SpriteType.BodyAccent2, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DriderSkin, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Driders[25 + input.Actor.GetSimpleBodySprite()]);
        }); //Back of front Legs
        builder.RenderSingle(SpriteType.BodyAccent3, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DriderEyes, input.Actor.Unit.EyeColor));
            output.Sprite(input.Sprites.Driders[36]);
        }); // extra Eyes
        builder.RenderSingle(SpriteType.BodyAccent4, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Driders[37]);
        }); //eyebrow
        builder.RenderSingle(SpriteType.BodyAccent5, 18, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                output.Sprite(input.Sprites.Driders[61 + input.Actor.GetSimpleBodySprite() + 3 * (input.Actor.GetWeaponSprite() / 2)]);
            }
        }); //Extra Leg Accessories

        builder.RenderSingle(SpriteType.BodyAccent6, 17, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DriderSkin, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Driders[98 + input.Actor.GetSimpleBodySprite()]);
        }); //Front of front Legs
        //builder.SetSpriteInfo(SpriteType.BodyAccent7, 7, Defaults.WhiteColored, null, (input, output) => null); //For Spider Web attack animation???
        builder.RenderSingle(SpriteType.BodyAccessory, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DriderSkin, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Sprites.Driders[18 + input.Actor.Unit.BodySize]);
            }
            else
            {
                if (input.Actor.Unit.BodySize < 2)
                {
                    output.Sprite(input.Sprites.Driders[18]);
                }
                else
                {
                    output.Sprite(input.Sprites.Driders[17 + input.Actor.Unit.BodySize]);
                }
            }
        }); //abdomen

        builder.RenderSingle(SpriteType.SecondaryAccessory, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.DriderEyes, input.Actor.Unit.ExtraColor1));
            if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Sprites.Driders[48 + input.Actor.Unit.BodySize]);
            }
            else
            {
                if (input.Actor.Unit.BodySize < 2)
                {
                    output.Sprite(input.Sprites.Driders[48]);
                }
                else
                {
                    output.Sprite(input.Sprites.Driders[47 + input.Actor.Unit.BodySize]);
                }
            }
        }); // abdomen patterns

        builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Belly].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.AlrauneSkin, input.Actor.Unit.SkinColor));
            output.AddOffset(0, yOffset);
        });

        builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Breasts].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
            output.AddOffset(0, yOffset);
        });
        builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Belly].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
            output.AddOffset(0, yOffset);
        });
        builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Dick].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
            output.AddOffset(0, yOffset);
        });
        builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Balls].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, input.Actor.Unit.SkinColor));
            output.AddOffset(0, yOffset);
        });
        builder.RenderSingle(SpriteType.Weapon, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                output.Sprite(input.Sprites.Driders[53 + input.Actor.GetWeaponSprite()]);
            }
        });

        builder.RandomCustom(data =>
        {
            Unit unit = data.Unit;
            Defaults.RandomCustom(data);

            if (unit.Type == UnitType.Leader)
            {
                unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypes, LeaderClothes);
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

namespace DriderClothing
{
    internal static class DriderLeader
    {
        internal static IClothing DriderLeaderInstance = ClothingBuilder.Create(builder =>
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

                if (input.Actor.Unit.HasBreasts)
                {
                    // output.DiscardSprite = input.Sprites.Driders[97];
                    // output.Type = 237;
                    // TODO this was not a robust way of doing this. Changing discards dynamically isnt currently supported 
                    output["Clothing1"].Sprite(input.Sprites.Driders[76]);
                    output["Clothing2"].Sprite(input.Sprites.Driders[87 + input.Actor.Unit.BreastSize]);
                    output["Clothing3"].Sprite(input.Sprites.Driders[82 + input.Actor.Unit.BodySize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Driders[95]);
                    output["Clothing2"].Sprite(null);
                    output["Clothing3"].Sprite(input.Sprites.Driders[77 + input.Actor.Unit.BodySize]);
                }
            });
        });
    }
}
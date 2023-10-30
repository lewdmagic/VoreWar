#region

using System;
using UnityEngine;

#endregion

internal static class Sergal
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Default, builder =>
    {
        builder.Setup(output =>
        {
            output.BreastSizes = () => 10;

            output.EyeTypes = 4;
            output.HairStyles = 8;
            output.BodySizes = 0;
            output.MouthTypes = 0;
            output.SkinColors = 0;

            output.AllowedMainClothingTypes.Set(
                BaseOutfit.BaseOutfitInstance,
                SergalBikiniTop.SergalBikiniTopInstance,
                SergalStrapTop.SergalStrapTopInstance,
                SergalBlackTop.SergalBlackTopInstance,
                SergalRags.SergalRagsInstance
            );
            output.AvoidedMainClothingTypes = 1;
            output.AvoidedEyeTypes = 0;
            output.AllowedWaistTypes.Set(
                SergalBikiniBottom.SergalBikiniBottomInstance,
                SergalLoincloth.SergalLoinclothInstance,
                SergalShorts.SergalShortsInstance
            );
        });


        builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, input.Actor.Unit.AccessoryColor));
                if (input.Actor.IsEating)
                {
                    output.Sprite(input.Sprites.Sergal[1]);
                }
            });

        builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            output.Sprite(input.Sprites.Sergal[4 + input.Actor.Unit.EyeType]);
        });
        builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Sergal[8 + input.Actor.Unit.HairStyle]);
        });
        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Sergal[0]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, input.Actor.Unit.AccessoryColor));
                if (input.Actor.IsAttacking)
                {
                    if (input.Actor.BestRanged != null)
                    {
                        output.Sprite(input.Sprites.Sergal[3]);
                        return;
                    }

                    output.Sprite(input.Sprites.Sergal[65]);
                    return;
                }

                output.Sprite(input.Sprites.Sergal[2]);
            });

        builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.Sergal[16 + input.Actor.Unit.BreastSize] : null);
        });
        builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, input.Actor.Unit.AccessoryColor));
            if (input.Actor.HasBelly == false)
            {
                return;
            }

            int size = input.Actor.GetStomachSize(18);
            if (size == 18 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true,
                    PreyLocation.stomach, PreyLocation.womb))
            {
                output.Sprite(input.Sprites.Sergal[45]);
                return;
            }

            output.Sprite(input.Sprites.Sergal[26 + size]);
        });

        builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Dick].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, input.Actor.Unit.AccessoryColor));
        });

        builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Balls].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, input.Actor.Unit.AccessoryColor));
        });


        builder.RenderSingle(SpriteType.Weapon, 14, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                output.Sprite(input.Sprites.Sergal[46 + input.Actor.GetWeaponSprite()]);
            }
        });

        builder.RunBefore((input, output) =>
        {
            output.ChangeSprite(SpriteType.Dick).AddOffset(0, 12 * .625f);
            output.ChangeSprite(SpriteType.Balls).AddOffset(0, 12 * .625f);
            output.ActorFurry = true;
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            if (Config.HairMatchesFur)
            {
                unit.HairColor = unit.AccessoryColor;
            }
        });
    });
}

internal static class BaseOutfit
{
    internal static readonly IClothing BaseOutfitInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.OccupiesAllSlots = true;
            output.RevealsDick = true;

            output.DiscardSprite = input.Sprites.Asura[39];
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing5"].Layer(0);
            output["Clothing4"].Layer(6);
            output["Clothing3"].Layer(13);
            output["Clothing2"].Layer(13);
            output["Clothing1"].Layer(13);
            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.FurStrict, input.Actor.Unit.AccessoryColor));
            output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, input.Actor.Unit.ClothingColor));
            output["Clothing1"].Layer(13);
            output.RevealsBreasts = false;
            if (input.Actor.Unit.BreastSize <= 2)
            {
                output["Clothing1"].Sprite(input.Sprites.Sergal[59]);
            }
            else if (input.Actor.Unit.BreastSize >= 7)
            {
                output.RevealsBreasts = true;
                output.ChangeSprite(SpriteType.Breasts).Layer(16);
                output["Clothing1"].Sprite(input.Sprites.Sergal[64]);
            }

            output["Clothing1"].Layer(17);
            output["Clothing1"].Sprite(input.Sprites.Sergal[57 + input.Actor.Unit.BreastSize]);
            output["Clothing2"].Sprite(input.Sprites.Sergal[54]);
            if (input.Actor.IsErect())
            {
                output["Clothing3"].Sprite(null);
            }

            output["Clothing3"].Sprite(input.Sprites.Sergal[55]);


            if (input.Actor.IsAttacking)
            {
                output["Clothing4"].Sprite(input.Actor.BestRanged != null ? input.Sprites.Sergal[58] : input.Sprites.Sergal[57]);
            }

            output["Clothing4"].Sprite(input.Sprites.Sergal[56]);
        });
    });
}

internal static class SergalBikiniTop
{
    internal static readonly IClothing SergalBikiniTopInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.BikiniTop[9];
            output.FemaleOnly = true;
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.Type = 205;
            output.DiscardUsesPalettes = true;
        });


        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(17);
            if (input.Actor.Unit.HasBreasts)
            {
                output["Clothing1"].Sprite(input.Sprites.SergalClothing[10 + input.Actor.Unit.BreastSize]);
                input.Actor.SquishedBreasts = true;
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, input.Actor.Unit.ClothingColor));
            }
        });
    });
}

internal static class SergalBlackTop
{
    internal static readonly IClothing SergalBlackTopInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = null;
            output.RevealsDick = true;
            output.Type = 208;
            output.FixedColor = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(17);
            output["Clothing1"].Coloring(Color.white);
            output["Clothing1"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.SergalClothing[input.Actor.Unit.BreastSize] : input.Sprites.SergalClothing[0]);
        });
    });
}

internal static class SergalStrapTop
{
    internal static readonly IClothing SergalStrapTopInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.Straps[9];
            output.FemaleOnly = true;
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.Type = 204;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(17);
            if (input.Actor.Unit.HasBreasts)
            {
                if (input.Actor.Unit.BreastSize < 2)
                {
                    output["Clothing1"].Sprite(input.Sprites.SergalClothing[20]);
                }
                else if (input.Actor.Unit.BreastSize < 4)
                {
                    output["Clothing1"].Sprite(input.Sprites.SergalClothing[21]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.SergalClothing[18 + input.Actor.Unit.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, input.Actor.Unit.ClothingColor));
                input.Actor.SquishedBreasts = true;
            }
        });
    });
}

internal static class SergalBikiniBottom
{
    internal static readonly IClothing SergalBikiniBottomInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.BikiniBottom[12];
            output.Type = 201;
            output.RevealsBreasts = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(9);
            output["Clothing1"].Sprite(input.Actor.Unit.DickSize > 3 ? input.Sprites.SergalClothing[29] : input.Sprites.SergalClothing[28]);

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, input.Actor.Unit.ClothingColor));
        });
    });
}

internal static class SergalShorts
{
    internal static readonly IClothing SergalShortsInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.Shorts[12];
            output.Type = 202;
            output.FixedColor = true;
            output.RevealsBreasts = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(10);
            output["Clothing1"].Coloring(Color.white);
            if (input.Actor.Unit.DickSize > 2)
            {
                output["Clothing1"].Sprite(input.Actor.Unit.DickSize > 4 ? input.Sprites.SergalClothing[39] : input.Sprites.SergalClothing[38]);
            }

            output["Clothing1"].Sprite(input.Sprites.SergalClothing[37]);
        });
    });
}

internal static class SergalLoincloth
{
    internal static readonly IClothing SergalLoinclothInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.Loincloths[10];
            output.Type = 200;
            output.RevealsDick = true;
            output.InFrontOfDick = true;
            output.DiscardUsesPalettes = true;
            output.RevealsBreasts = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(10);

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, input.Actor.Unit.ClothingColor));
            output["Clothing1"].Sprite(input.Sprites.SergalClothing[36]);
        });
    });
}


internal static class SergalRags
{
    internal static readonly IClothing SergalRagsInstance = ClothingBuilder.Create(builder =>
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
            output["Clothing2"].Layer(10);
            output["Clothing2"].Coloring(Color.white);
            output["Clothing1"].Layer(11);
            output["Clothing1"].Coloring(Color.white);
            output["Clothing1"].Sprite(input.Sprites.SergalClothing[34]);

            output["Clothing2"].Layer(10);

            if (input.Actor.Unit.BreastSize >= 0)
            {
                output["Clothing2"].SetOffset(0, 0);
                if (input.Actor.Unit.BreastSize < 2)
                {
                    output["Clothing2"].Sprite(input.Sprites.SergalClothing[30]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.SergalClothing[
                        Math.Min(29 + input.Actor.Unit.BreastSize, 33)]);
                }

                output["Clothing2"].Layer(18);
            }
            else
            {
                output["Clothing2"].Sprite(input.Sprites.Rags[11]);
                output["Clothing2"].SetOffset(0, 16 * .625f);
            }

            //return base.ConfigureIgnoreHidingRules(input.Actor);
        });
    });
}
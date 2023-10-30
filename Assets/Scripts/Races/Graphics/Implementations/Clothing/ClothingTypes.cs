#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

internal static class BikiniTop
{
    internal static readonly IClothing BikiniTopInstance = ClothingBuilder.Create(builder =>
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
                int breastMod = 0;
                if (input.Actor.Unit.Race == Race.Succubi)
                {
                    breastMod = 3;
                }

                output["Clothing1"].Sprite(input.Sprites.BikiniTop[input.Actor.Unit.BreastSize + breastMod]);
                input.Actor.SquishedBreasts = true;
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, input.Actor.Unit.ClothingColor));
            }
        });
    });
}

internal static class StrapTop
{
    internal static readonly IClothing StrapTopInstance = ClothingBuilder.Create(builder =>
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
                int breastMod = 0;
                if (input.Actor.Unit.Race == Race.Succubi)
                {
                    breastMod = 3;
                }

                output["Clothing1"].Sprite(input.Sprites.Straps[input.Actor.Unit.BreastSize + breastMod]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, input.Actor.Unit.ClothingColor));
                input.Actor.SquishedBreasts = true;
            }
        });
    });
}

internal static class BeltTop
{
    internal static readonly IClothing BeltTopInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.Belts[9];
            output.FemaleOnly = true;
            output.RevealsBreasts = true;
            output.RevealsDick = true;
            output.Type = 203;
            output.FixedColor = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(17);
            output["Clothing1"].Coloring(Color.white);
            if ((input.Actor.Unit.Race == Race.Lizards && input.Actor.IsAnalVoring) || input.Actor.IsUnbirthing)
            {
                output.RevealsDick = true;
                output["Clothing1"].Sprite(null);
            }
            else if (input.Actor.Unit.HasBreasts)
            {
                int breastMod = 0;
                if (input.Actor.Unit.Race == Race.Succubi)
                {
                    breastMod = 3;
                }

                output["Clothing1"].Sprite(input.Sprites.Belts[input.Actor.Unit.BreastSize + breastMod]);
                input.Actor.SquishedBreasts = true;
            }
        });
    });
}


internal static class BikiniBottom
{
    internal static readonly IClothing BikiniBottomInstance = ClothingBuilder.Create(builder =>
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
            output["Clothing2"].Layer(10);
            output["Clothing1"].Layer(9);
            int spr = 0;
            if ((input.Actor.Unit.Race == Race.Lizards && input.Actor.IsAnalVoring) || input.Actor.IsUnbirthing)
            {
                output.RevealsDick = true;
                output["Clothing1"].Sprite(null);
            }
            else if (input.Actor.Unit.Race == Race.Lizards)
            {
                spr = 8;
            }
            else if (input.Actor.Unit.Race == Race.Harpies)
            {
                spr = 9;
            }
            else if (input.Actor.Unit.Race == Race.Lamia)
            {
                spr = 3 + (input.Actor.Unit.HasBreasts ? 0 : 4);
            }
            else
            {
                if (input.Actor.GetBodyWeight() > 0)
                {
                    spr = (input.Actor.Unit.HasBreasts ? 0 : 4) + input.Actor.Unit.BodySize;
                }

                if (spr > 7)
                {
                    spr = 7;
                }
            }

            if (input.Actor.Unit.DickSize > 2)
            {
                if ((input.Actor.Unit.Race == Race.Lizards && input.Actor.IsAnalVoring) || input.Actor.IsUnbirthing)
                {
                    output.RevealsDick = true;
                    output["Clothing2"].Sprite(null);
                }
                else if (input.Actor.Unit.DickSize > 4)
                {
                    output["Clothing2"].Sprite(input.Sprites.BikiniBottom[11]);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.BikiniBottom[10]);
                }
            }
            else
            {
                output["Clothing2"].Sprite(null);
            }

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, input.Actor.Unit.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, input.Actor.Unit.ClothingColor));


            output["Clothing1"].Sprite(input.Sprites.BikiniBottom[spr]);
        });
    });
}

internal static class Shorts
{
    internal static readonly IClothing ShortsInstance = ClothingBuilder.Create(builder =>
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
            output["Clothing2"].Layer(11);
            output["Clothing2"].Coloring(Color.white);
            output["Clothing1"].Layer(10);
            output["Clothing1"].Coloring(Color.white);
            int spr = 0;
            if ((input.Actor.Unit.Race == Race.Lizards && input.Actor.IsAnalVoring) || input.Actor.IsUnbirthing)
            {
                output.RevealsDick = true;
                output["Clothing1"].Sprite(null);
            }
            else if (input.Actor.Unit.Race == Race.Lizards)
            {
                spr = 8;
            }
            else if (input.Actor.Unit.Race == Race.Harpies)
            {
                spr = 9;
            }
            else
            {
                if (input.Actor.GetBodyWeight() > 0)
                {
                    spr = (input.Actor.Unit.HasBreasts ? 0 : 4) + input.Actor.Unit.BodySize;
                }

                if (spr > 7)
                {
                    spr = 7;
                }
            }

            if (input.Actor.Unit.DickSize > 2)
            {
                output["Clothing2"].Sprite(input.Actor.Unit.DickSize > 4 ? input.Sprites.Shorts[11] : input.Sprites.Shorts[10]);
            }
            else
            {
                output["Clothing2"].Sprite(null);
            }

            output["Clothing1"].Sprite(input.Sprites.Shorts[spr]);
        });
    });
}

internal static class Loincloth
{
    internal static readonly IClothing LoinclothInstance = ClothingBuilder.Create(builder =>
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
            int spr = 0;
            if ((input.Actor.Unit.Race == Race.Lizards && input.Actor.IsAnalVoring) || input.Actor.IsUnbirthing)
            {
                output["Clothing1"].Sprite(null);
            }
            else if (input.Actor.Unit.Race == Race.Lizards)
            {
                spr = 8;
            }
            else if (input.Actor.Unit.Race == Race.Harpies)
            {
                spr = 9;
            }
            else if (input.Actor.Unit.Race == Race.Lamia)
            {
                spr = 3 + (input.Actor.Unit.HasBreasts ? 0 : 4);
            }
            else
            {
                if (input.Actor.GetBodyWeight() > 0)
                {
                    spr = (input.Actor.Unit.HasBreasts ? 0 : 4) + input.Actor.Unit.BodySize;
                }

                if (spr > 7)
                {
                    spr = 7;
                }
            }

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, input.Actor.Unit.ClothingColor));


            output["Clothing1"].Sprite(input.Sprites.Loincloths[spr]);
        });
    });
}

internal static class Leotard
{
    internal static readonly IClothing LeotardInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = input.Sprites.Leotards[9];
            output.Type = 206;
            //blocksBreasts = true;
            output.OccupiesAllSlots = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing3"].Layer(11);
            output["Clothing2"].Layer(17);
            output["Clothing1"].Layer(10);
            int spr = 0;
            if (input.Actor.Unit.Race == Race.Lizards)
            {
                spr = 8;
            }
            else if (input.Actor.Unit.Race == Race.Harpies)
            {
                spr = 9;
            }
            else
            {
                if (input.Actor.GetBodyWeight() > 0)
                {
                    spr = (input.Actor.Unit.HasBreasts ? 0 : 4) + input.Actor.Unit.BodySize;
                }

                if (spr > 7)
                {
                    spr = 7;
                }
            }

            if (input.Actor.Unit.DickSize > 2)
            {
                if ((input.Actor.Unit.Race == Race.Lizards && input.Actor.IsAnalVoring) || input.Actor.IsUnbirthing)
                {
                    output.RevealsDick = true;
                    output["Clothing3"].Sprite(null);
                }
                else if (input.Actor.Unit.DickSize > 4)
                {
                    output["Clothing3"].Sprite(input.Sprites.Leotards[11]);
                }
                else
                {
                    output["Clothing3"].Sprite(input.Sprites.Leotards[10]);
                }
            }
            else
            {
                output["Clothing3"].Sprite(null);
            }

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, input.Actor.Unit.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, input.Actor.Unit.ClothingColor));
            output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Clothing, input.Actor.Unit.ClothingColor));
            output.changeSprite(SpriteType.Belly).Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SkinToClothing, input.Actor.Unit.ClothingColor));

            if ((input.Actor.Unit.Race == Race.Lizards && input.Actor.IsAnalVoring) || input.Actor.IsUnbirthing)
            {
                output.RevealsDick = true;
                output["Clothing1"].Sprite(null);
            }
            else
            {
                output["Clothing1"].Sprite(input.Sprites.Leotards[spr]);
            }

            if (input.Actor.Unit.BreastSize >= 0)
            {
                if ((input.Actor.Unit.Race == Race.Lizards && input.Actor.IsAnalVoring) || input.Actor.IsUnbirthing)
                {
                    output.RevealsDick = true;
                    output["Clothing2"].Sprite(null);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Leotards[12 + input.Actor.Unit.BreastSize]);
                }
            }
            else
            {
                output["Clothing2"].Sprite(null);
            }
        });
    });
}

internal static class Rags
{
    internal static readonly IClothing RagsInstance = ClothingBuilder.Create(builder =>
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
            int spr = 0;
            if (input.Actor.Unit.Race == Race.Lizards)
            {
                spr = 8;
                //CompleteSprite.ChangeOffsetPlaceholder(SpriteType.Clothing2, new Vector2(0, 2.5f));
                // TODO this is a very poor approch
                // it's not going to be re-implemented so a different way should be found to
                // achieve whatever it was trying to do
            }
            else if (input.Actor.Unit.Race == Race.Harpies)
            {
                spr = 9;
            }
            else if (input.Actor.Unit.Race == Race.Lamia)
            {
                spr = 3 + (input.Actor.Unit.HasBreasts ? 0 : 4);
            }
            else if (input.Actor.Unit.Race == Race.Imps || input.Actor.Unit.Race == Race.Goblins)
            {
                spr = 10;
            }
            else
            {
                if (input.Actor.GetBodyWeight() > 0)
                {
                    spr = (input.Actor.Unit.HasBreasts ? 0 : 4) + input.Actor.Unit.BodySize;
                }

                if (spr > 7)
                {
                    spr = 7;
                }
            }

            //if ((BlocksDick || InFrontOfDick) && Config.CockVoreHidesClothes && input.Actor.PredatorComponent?.BallsFullness > 0)   
            // This was bugged, InFrontOfDick was always true anyway. 
            if ((false || true) && Config.CockVoreHidesClothes && input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                output["Clothing1"].Sprite(null);
            }
            else
            {
                output["Clothing1"].Sprite(input.Sprites.Rags[spr]);
            }

            output["Clothing2"].Layer(10);
            if (input.Actor.Unit.Race == Race.Imps || input.Actor.Unit.Race == Race.Goblins)
            {
                int spr2 = 21 + input.Actor.Unit.BreastSize; //-1 to 1
                if (spr2 > 22)
                {
                    spr2 = 22;
                }

                output["Clothing2"].Sprite(input.Sprites.Rags[spr2]);
                output["Clothing2"].Layer(18);
            }
            else if ((input.Actor.Unit.Race == Race.Lizards && input.Actor.IsAnalVoring) || input.Actor.IsUnbirthing)
            {
                output.RevealsDick = true;
                output["Clothing2"].Sprite(null);
            }
            else if (input.Actor.Unit.BreastSize >= 0)
            {
                output["Clothing2"].Sprite(input.Sprites.Rags[11 + input.Actor.Unit.BreastSize]);
                output["Clothing2"].Layer(18);
            }
            else
            {
                output["Clothing2"].Sprite(input.Sprites.Rags[11]);
            }

            output.SkipCheck = true;
        });
    });
}

internal static class BlackTop
{
    internal static readonly IClothing BlackTopInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = null;
            output.BlocksBreasts = true;
            output.RevealsDick = true;
            output.Type = 208;
            output.FixedColor = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(17);
            output["Clothing1"].Coloring(Color.white);
            switch (input.Actor.Unit.Race)
            {
                case Race.Goblins:
                case Race.Imps:
                    if (input.Actor.Unit.Race == Race.Goblins && input.Actor.Unit.BreastSize > 0)
                    {
                        output["Clothing1"].Sprite(input.Sprites.BlackTop[3 + input.Actor.Unit.BreastSize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.BlackTop[16 + Math.Min(input.Actor.Unit.BreastSize, 1)]);
                    }

                    break;
                default:
                    if (input.Actor.Unit.HasBreasts)
                    {
                        int breastMod = 0;
                        if (input.Actor.Unit.Race == Race.Succubi)
                        {
                            breastMod = 3;
                        }

                        output["Clothing1"].Sprite(input.Sprites.BlackTop[6 + input.Actor.Unit.BreastSize + breastMod]);
                    }
                    else
                    {
                        if (input.Actor.GetBodyWeight() <= 0)
                        {
                            output["Clothing1"].Sprite(input.Sprites.BlackTop[6]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.BlackTop[Math.Max(Math.Min(input.Actor.GetBodyWeight() - 1, 2), 0)]);
                        }
                    }

                    break;
            }
            //if (input.Actor.Unit.Race == Race.Lizards)
            //CompleteSprite.ChangeOffsetPlaceholder(SpriteType.Clothing, new Vector2(0, 2.5f));
            // TODO Find a replacement way to achieve this. 
        });
    });
}

internal static class FemaleVillager
{
    internal static readonly IClothing FemaleVillagerInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = null;
            output.Type = 209;
            output.BlocksBreasts = true;
            output.FemaleOnly = true;
            output.OccupiesAllSlots = true;
            output.FixedColor = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing4"].Layer(17);
            output["Clothing3"].Layer(10);
            output["Clothing2"].Layer(10);
            output["Clothing1"].Layer(10);
            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, input.Actor.Unit.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, input.Actor.Unit.ClothingColor));
            output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, input.Actor.Unit.ClothingColor));
            output["Clothing1"].Sprite(input.Sprites.FemaleVillager[input.Actor.IsAttacking ? 1 : 0]);
            output["Clothing2"].Sprite(input.Sprites.FemaleVillager[2]);
            output["Clothing3"].Sprite(input.Sprites.FemaleVillager[3 + input.Actor.GetBodyWeight()]);
            output["Clothing4"].Sprite(input.Actor.Unit.HasBreasts ? input.Sprites.FemaleVillager[8 + input.Actor.Unit.BreastSize] : null);

            output.changeSprite(CompleteSprite.AssumedFluffType).SetHide(true);
        });
    });
}

internal static class MaleVillager
{
    internal static readonly IClothing MaleVillagerInstance = ClothingBuilder.Create(builder =>
    {
        builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
        {
            output.DiscardSprite = null;
            output.Type = 210;
            output.BlocksBreasts = true;
            output.OccupiesAllSlots = true;
            output.MaleOnly = true;
            output.FixedColor = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(10);
            output["Clothing2"].Coloring(Color.white);
            output["Clothing1"].Layer(10);
            int attackFactor = input.Actor.IsAttacking ? 0 : 4;
            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, input.Actor.Unit.ClothingColor));
            output["Clothing1"].Sprite(input.Sprites.MaleVillager[Mathf.Min(input.Actor.GetBodyWeight(), 3) + attackFactor]);
            if (input.Actor.Unit.DickSize >= 2)
            {
                output["Clothing2"].Sprite(input.Sprites.MaleVillager[8]);
            }
            output.changeSprite(CompleteSprite.AssumedFluffType).SetHide(true);
        });
    });
}

internal static class ClothingTypes
{
    internal static readonly IClothing BikiniTopInstance = BikiniTop.BikiniTopInstance;
    internal static readonly IClothing BikiniBottomInstance = BikiniBottom.BikiniBottomInstance;
    internal static readonly IClothing StrapTopInstance = StrapTop.StrapTopInstance;
    internal static readonly IClothing BeltTopInstance = BeltTop.BeltTopInstance;
    internal static readonly IClothing ShortsInstance = Shorts.ShortsInstance;
    internal static readonly IClothing LoinclothInstance = Loincloth.LoinclothInstance;
    internal static readonly IClothing LeotardInstance = Leotard.LeotardInstance;
    internal static readonly IClothing RagsInstance = Rags.RagsInstance;
    internal static readonly IClothing BlackTopInstance = BlackTop.BlackTopInstance;
    internal static readonly IClothing FemaleVillagerInstance = FemaleVillager.FemaleVillagerInstance;
    internal static readonly IClothing MaleVillagerInstance = MaleVillager.MaleVillagerInstance;

    internal static readonly List<IClothing<IParameters>> All = new List<IClothing<IParameters>>
    {
        BikiniTopInstance,
        BikiniBottomInstance,
        StrapTopInstance,
        BeltTopInstance,
        ShortsInstance,
        LoinclothInstance,
        LeotardInstance,
        RagsInstance,
        BlackTopInstance,
        FemaleVillagerInstance,
        MaleVillagerInstance
    };
}
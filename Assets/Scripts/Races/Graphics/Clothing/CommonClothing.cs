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
            output.ClothingId = new ClothingId("common/205");
            output.DiscardUsesPalettes = true;
        });


        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(17);
            if (input.U.HasBreasts)
            {
                int breastMod = 0;
                if (Equals(input.U.GetRace, Race.Succubus))
                {
                    breastMod = 3;
                }

                output["Clothing1"].Sprite(input.Sprites.BikiniTop[input.U.BreastSize + breastMod]);
                input.A.SquishedBreasts = true;
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor));
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
            output.ClothingId = new ClothingId("common/204");
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(17);

            if (input.U.HasBreasts)
            {
                int breastMod = 0;
                if (Equals(input.U.GetRace, Race.Succubus))
                {
                    breastMod = 3;
                }

                output["Clothing1"].Sprite(input.Sprites.Straps[input.U.BreastSize + breastMod]);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor));
                input.A.SquishedBreasts = true;
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
            output.ClothingId = new ClothingId("common/203");
            output.FixedColor = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(17);
            output["Clothing1"].Coloring(Color.white);
            if ((Equals(input.U.GetRace, Race.Lizard) && input.A.IsAnalVoring) || input.A.IsUnbirthing)
            {
                output.RevealsDick = true;
                output["Clothing1"].Sprite(null);
            }
            else if (input.U.HasBreasts)
            {
                int breastMod = 0;
                if (Equals(input.U.GetRace, Race.Succubus))
                {
                    breastMod = 3;
                }

                output["Clothing1"].Sprite(input.Sprites.Belts[input.U.BreastSize + breastMod]);
                input.A.SquishedBreasts = true;
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
            output.ClothingId = new ClothingId("common/201");
            output.RevealsBreasts = true;
            output.DiscardUsesPalettes = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing2"].Layer(10);
            output["Clothing1"].Layer(9);
            int spr = 0;
            if ((Equals(input.U.GetRace, Race.Lizard) && input.A.IsAnalVoring) || input.A.IsUnbirthing)
            {
                output.RevealsDick = true;
                output["Clothing1"].Sprite(null);
            }
            else if (Equals(input.U.GetRace, Race.Lizard))
            {
                spr = 8;
            }
            else if (Equals(input.U.GetRace, Race.Harpy))
            {
                spr = 9;
            }
            else if (Equals(input.U.GetRace, Race.Lamia))
            {
                spr = 3 + (input.U.HasBreasts ? 0 : 4);
            }
            else
            {
                if (input.A.GetBodyWeight() > 0)
                {
                    spr = (input.U.HasBreasts ? 0 : 4) + input.U.BodySize;
                }

                if (spr > 7)
                {
                    spr = 7;
                }
            }

            if (input.U.DickSize > 2)
            {
                if ((Equals(input.U.GetRace, Race.Lizard) && input.A.IsAnalVoring) || input.A.IsUnbirthing)
                {
                    output.RevealsDick = true;
                    output["Clothing2"].Sprite(null);
                }
                else if (input.U.DickSize > 4)
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

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor));


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
            output.ClothingId = new ClothingId("common/202");
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
            if ((Equals(input.U.GetRace, Race.Lizard) && input.A.IsAnalVoring) || input.A.IsUnbirthing)
            {
                output.RevealsDick = true;
                output["Clothing1"].Sprite(null);
            }
            else if (Equals(input.U.GetRace, Race.Lizard))
            {
                spr = 8;
            }
            else if (Equals(input.U.GetRace, Race.Harpy))
            {
                spr = 9;
            }
            else
            {
                if (input.A.GetBodyWeight() > 0)
                {
                    spr = (input.U.HasBreasts ? 0 : 4) + input.U.BodySize;
                }

                if (spr > 7)
                {
                    spr = 7;
                }
            }

            if (input.U.DickSize > 2)
            {
                output["Clothing2"].Sprite(input.U.DickSize > 4 ? input.Sprites.Shorts[11] : input.Sprites.Shorts[10]);
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
            output.ClothingId = new ClothingId("common/200");
            output.RevealsDick = true;
            output.InFrontOfDick = true;
            output.DiscardUsesPalettes = true;
            output.RevealsBreasts = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(10);
            int spr = 0;
            if ((Equals(input.U.GetRace, Race.Lizard) && input.A.IsAnalVoring) || input.A.IsUnbirthing)
            {
                output["Clothing1"].Sprite(null);
            }
            else if (Equals(input.U.GetRace, Race.Lizard))
            {
                spr = 8;
            }
            else if (Equals(input.U.GetRace, Race.Harpy))
            {
                spr = 9;
            }
            else if (Equals(input.U.GetRace, Race.Lamia))
            {
                spr = 3 + (input.U.HasBreasts ? 0 : 4);
            }
            else
            {
                if (input.A.GetBodyWeight() > 0)
                {
                    spr = (input.U.HasBreasts ? 0 : 4) + input.U.BodySize;
                }

                if (spr > 7)
                {
                    spr = 7;
                }
            }

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor));


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
            output.ClothingId = new ClothingId("common/206");
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
            if (Equals(input.U.GetRace, Race.Lizard))
            {
                spr = 8;
            }
            else if (Equals(input.U.GetRace, Race.Harpy))
            {
                spr = 9;
            }
            else
            {
                if (input.A.GetBodyWeight() > 0)
                {
                    spr = (input.U.HasBreasts ? 0 : 4) + input.U.BodySize;
                }

                if (spr > 7)
                {
                    spr = 7;
                }
            }

            if (input.U.DickSize > 2)
            {
                if ((Equals(input.U.GetRace, Race.Lizard) && input.A.IsAnalVoring) || input.A.IsUnbirthing)
                {
                    output.RevealsDick = true;
                    output["Clothing3"].Sprite(null);
                }
                else if (input.U.DickSize > 4)
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

            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor));
            output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor));
            output.ChangeRaceSprite(SpriteType.Belly).Coloring(ColorPaletteMap.GetPalette(SwapType.SkinToClothing, input.U.ClothingColor));

            if ((Equals(input.U.GetRace, Race.Lizard) && input.A.IsAnalVoring) || input.A.IsUnbirthing)
            {
                output.RevealsDick = true;
                output["Clothing1"].Sprite(null);
            }
            else
            {
                output["Clothing1"].Sprite(input.Sprites.Leotards[spr]);
            }

            if (input.U.BreastSize >= 0)
            {
                if ((Equals(input.U.GetRace, Race.Lizard) && input.A.IsAnalVoring) || input.A.IsUnbirthing)
                {
                    output.RevealsDick = true;
                    output["Clothing2"].Sprite(null);
                }
                else
                {
                    output["Clothing2"].Sprite(input.Sprites.Leotards[12 + input.U.BreastSize]);
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
            output.ClothingId = new ClothingId("common/207");
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
            if (Equals(input.U.GetRace, Race.Lizard))
            {
                spr = 8;
                //CompleteSprite.ChangeOffsetPlaceholder(SpriteType.Clothing2, new Vector2(0, 2.5f));
                // TODO this is a very poor approch
                // it's not going to be re-implemented so a different way should be found to
                // achieve whatever it was trying to do
            }
            else if (Equals(input.U.GetRace, Race.Harpy))
            {
                spr = 9;
            }
            else if (Equals(input.U.GetRace, Race.Lamia))
            {
                spr = 3 + (input.U.HasBreasts ? 0 : 4);
            }
            else if (Equals(input.U.GetRace, Race.Imp) || Equals(input.U.GetRace, Race.Goblin))
            {
                spr = 10;
            }
            else
            {
                if (input.A.GetBodyWeight() > 0)
                {
                    spr = (input.U.HasBreasts ? 0 : 4) + input.U.BodySize;
                }

                if (spr > 7)
                {
                    spr = 7;
                }
            }

            //if ((BlocksDick || InFrontOfDick) && Config.CockVoreHidesClothes && input.A.PredatorComponent?.BallsFullness > 0)   
            // This was bugged, InFrontOfDick was always true anyway. 
            if ((false || true) && Config.CockVoreHidesClothes && input.A.PredatorComponent?.BallsFullness > 0)
            {
                output["Clothing1"].Sprite(null);
            }
            else
            {
                output["Clothing1"].Sprite(input.Sprites.Rags[spr]);
            }

            output["Clothing2"].Layer(10);
            if (Equals(input.U.GetRace, Race.Imp) || Equals(input.U.GetRace, Race.Goblin))
            {
                int spr2 = 21 + input.U.BreastSize; //-1 to 1
                if (spr2 > 22)
                {
                    spr2 = 22;
                }

                output["Clothing2"].Sprite(input.Sprites.Rags[spr2]);
                output["Clothing2"].Layer(18);
            }
            else if ((Equals(input.U.GetRace, Race.Lizard) && input.A.IsAnalVoring) || input.A.IsUnbirthing)
            {
                output.RevealsDick = true;
                output["Clothing2"].Sprite(null);
            }
            else if (input.U.BreastSize >= 0)
            {
                output["Clothing2"].Sprite(input.Sprites.Rags[11 + input.U.BreastSize]);
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
            output.ClothingId = new ClothingId("common/208");
            output.FixedColor = true;
        });

        builder.RenderAll((input, output) =>
        {
            output["Clothing1"].Layer(17);
            output["Clothing1"].Coloring(Color.white);

            // switch (RaceFuncs.RaceToSwitch(input.U.GetRace))
            // {
            //     case RaceNumbers.Goblins:
            //     case RaceNumbers.Imps:
            //         if (input.U.GetRace == Race.Goblins && input.U.BreastSize > 0)
            //         {
            //             output["Clothing1"].Sprite(input.Sprites.BlackTop[3 + input.U.BreastSize]);
            //         }
            //         else
            //         {
            //             output["Clothing1"].Sprite(input.Sprites.BlackTop[16 + Math.Min(input.U.BreastSize, 1)]);
            //         }
            //
            //         break;
            //     default:
            //         if (input.U.HasBreasts)
            //         {
            //             int breastMod = 0;
            //             if (input.U.GetRace == Race.Succubi)
            //             {
            //                 breastMod = 3;
            //             }
            //
            //             output["Clothing1"].Sprite(input.Sprites.BlackTop[6 + input.U.BreastSize + breastMod]);
            //         }
            //         else
            //         {
            //             if (input.A.GetBodyWeight() <= 0)
            //             {
            //                 output["Clothing1"].Sprite(input.Sprites.BlackTop[6]);
            //             }
            //             else
            //             {
            //                 output["Clothing1"].Sprite(input.Sprites.BlackTop[Math.Max(Math.Min(input.A.GetBodyWeight() - 1, 2), 0)]);
            //             }
            //         }
            //
            //         break;
            // }

            if (Equals(input.U.GetRace, Race.Goblin) || Equals(input.U.GetRace, Race.Imp))
            {
                if (Equals(input.U.GetRace, Race.Goblin) && input.U.BreastSize > 0)
                {
                    output["Clothing1"].Sprite(input.Sprites.BlackTop[3 + input.U.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.BlackTop[16 + Math.Min(input.U.BreastSize, 1)]);
                }
            }
            else
            {
                if (input.U.HasBreasts)
                {
                    int breastMod = 0;
                    if (Equals(input.U.GetRace, Race.Succubus))
                    {
                        breastMod = 3;
                    }

                    output["Clothing1"].Sprite(input.Sprites.BlackTop[6 + input.U.BreastSize + breastMod]);
                }
                else
                {
                    if (input.A.GetBodyWeight() <= 0)
                    {
                        output["Clothing1"].Sprite(input.Sprites.BlackTop[6]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.BlackTop[Math.Max(Math.Min(input.A.GetBodyWeight() - 1, 2), 0)]);
                    }
                }
            }

            //if (input.U.Race == Race.Lizards)
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
            output.ClothingId = new ClothingId("common/209");
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
            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrict, input.U.ClothingColor));
            output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrict, input.U.ClothingColor));
            output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrict, input.U.ClothingColor));
            output["Clothing1"].Sprite(input.Sprites.FemaleVillager[input.A.IsAttacking ? 1 : 0]);
            output["Clothing2"].Sprite(input.Sprites.FemaleVillager[2]);
            output["Clothing3"].Sprite(input.Sprites.FemaleVillager[3 + input.A.GetBodyWeight()]);
            output["Clothing4"].Sprite(input.U.HasBreasts ? input.Sprites.FemaleVillager[8 + input.U.BreastSize] : null);

            output.ChangeRaceSprite(RaceRenderer.AssumedFluffType).SetHide(true);
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
            output.ClothingId = new ClothingId("common/210");
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
            int attackFactor = input.A.IsAttacking ? 0 : 4;
            output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrict, input.U.ClothingColor));
            output["Clothing1"].Sprite(input.Sprites.MaleVillager[Mathf.Min(input.A.GetBodyWeight(), 3) + attackFactor]);
            if (input.U.DickSize >= 2)
            {
                output["Clothing2"].Sprite(input.Sprites.MaleVillager[8]);
            }

            output.ChangeRaceSprite(RaceRenderer.AssumedFluffType).SetHide(true);
        });
    });
}

internal static class CommonClothing
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

    internal static readonly List<IClothing> All = new List<IClothing>
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
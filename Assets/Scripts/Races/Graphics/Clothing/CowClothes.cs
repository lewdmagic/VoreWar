#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace TaurusClothes
{
    internal static class Overall
    {
        internal static readonly IClothing OverallInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.CowClothing[12];
                output.ClothingId = new ClothingId("common/81");
                output.OccupiesAllSlots = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);
                output["Clothing1"].Coloring(Color.white);
                if (Config.AllowTopless && input.A.PredatorComponent?.VisibleFullness > 0f)
                {
                    output.RevealsBreasts = true;
                    output["Clothing1"].Sprite(input.Sprites.CowClothing[10]);
                }
                else
                {
                    output.RevealsBreasts = false;
                }

                int spriteNum;
                if (input.U.HasBreasts)
                {
                    spriteNum = input.U.BreastSize;
                    output.ChangeRaceSprite(SpriteType.Breasts).Sprite(input.Sprites.CowClothing[5 + spriteNum]);
                }
                else
                {
                    spriteNum = 11;
                }

                output["Clothing1"].Sprite(input.Sprites.CowClothing[spriteNum]);
            });
        });
    }

    internal static class OverallBottom
    {
        internal static readonly IClothing OverallBottomInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardSprite = input.Sprites.CowClothing[12];
                output.RevealsBreasts = true;
                output.ClothingId = new ClothingId("common/80");
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.CowClothing[10]);
            });
        });
    }

    internal static class Bikini
    {
        internal static readonly IClothing BikiniInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.FemaleOnly = true;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.ClothingId = new ClothingId("common/82");
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(17);
                if (input.U.HasBreasts == false)
                {
                    output["Clothing1"].Sprite(input.Sprites.CowClothing[16 + input.U.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.CowClothing[16 + input.U.BreastSize]);
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor));
                input.A.SquishedBreasts = true;
            });
        });
    }

    internal static class LeaderOutfit
    {
        internal static readonly IClothing LeaderOutfitInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.ClothingId = new ClothingId("common/87");
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
                output.LeaderOnly = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing3"].Layer(17);
                output["Clothing3"].Coloring(Color.white);
                output["Clothing2"].Layer(11);
                output["Clothing1"].Layer(10);
                output["Clothing1"].Coloring(Color.white);
                if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.CowClothing[36]);
                    output["Clothing2"].Sprite(input.Sprites.CowClothing[37]);
                    output["Clothing3"].Sprite(input.Sprites.CowClothing[38 + input.U.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.CowClothing[31 + (input.A.IsAttacking ? 1 : 0)]);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing2"].Sprite(input.Sprites.CowClothing[35]);
                    output["Clothing3"].Sprite(input.Sprites.CowClothing[33 + (input.A.IsAttacking ? 1 : 0)]);
                }
            });
        });
    }

    internal static class Shirt
    {
        internal static readonly IClothing ShirtInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
                output.RevealsDick = true;
                output.ClothingId = new ClothingId("common/84");
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(17);
                var spriteNum = input.U.HasBreasts ? input.U.BreastSize : 5;

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor));
                output["Clothing1"].Sprite(input.Sprites.CowClothing[23 + spriteNum]);
            });
        });
    }

    internal static class BikiniBottom
    {
        internal static readonly IClothing BikiniBottomInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.ClothingId = new ClothingId("common/85");
                output.RevealsBreasts = true;
            });
            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(10);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(11);
                output["Clothing1"].Coloring(Color.white);
                if (input.U.HasDick)
                {
                    if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.CowClothing[44]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.CowClothing[15]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor));
                }
                else
                {
                    output["Clothing1"].Sprite(null);
                }

                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor));
                output["Clothing2"].Sprite(input.Sprites.CowClothing[13 + (input.U.HasBreasts ? 0 : 1)]);
            });
        });
    }

    internal static class Loincloth
    {
        internal static readonly IClothing LoinclothInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.ClothingId = new ClothingId("common/86");
                output.RevealsDick = true;
                output.RevealsBreasts = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing, input.U.ClothingColor));
                output["Clothing1"].Sprite(input.Sprites.CowClothing[21 + (input.U.HasBreasts ? 0 : 1)]);
            });
        });
    }


    internal static class CowBell
    {
        internal static readonly IClothing CowBellInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                { }
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(18);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.CowClothing[29 + (input.U.HasBreasts ? 0 : 1)]);
            });
        });
    }

    internal static class Hat
    {
        internal static readonly IClothing HatInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.CowClothing[43]);
            });
        });
    }

    internal static class HolidayHat
    {
        internal static readonly IClothing HolidayHatInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.ReqWinterHoliday = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(17);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.CowHoliday[0]);
            });
        });
    }

    internal static class HolidayOutfit
    {
        internal static readonly IClothing HolidayOutfitInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.ClothingId = new ClothingId("common/0");
                output.RevealsBreasts = true;
                output.OccupiesAllSlots = true;
                output.ReqWinterHoliday = true;
                //output.ClothingDefaults3 = new SpriteExtraInfo(17, null, WhiteColored);
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(17);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(10);
                output["Clothing1"].Coloring(Color.white);
                if (input.U.HasBreasts)
                {
                    output["Clothing1"].Sprite(input.Sprites.CowHoliday[7]);
                    output["Clothing2"].Sprite(input.Sprites.CowHoliday[1 + input.U.BreastSize]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.CowHoliday[8 + (input.A.IsAttacking ? 1 : 0)]);
                    //output.Clothing2.GetColor = WhiteColored;
                    //output.Clothing2.GetSprite = (s) => Out.Update(State.GameManager.SpriteDictionary.CowClothing[6]);
                    output["Clothing2"].Sprite(input.Sprites.CowHoliday[10 + (input.A.IsAttacking ? 1 : 0)]);
                }
            });
        });
    }

    internal static class TaurusClothingTypes
    {
        internal static readonly IClothing OverallInstance = Overall.OverallInstance;
        internal static readonly IClothing OverallBottomInstance = OverallBottom.OverallBottomInstance;
        internal static readonly IClothing ShirtInstance = Shirt.ShirtInstance;
        internal static readonly IClothing BikiniInstance = Bikini.BikiniInstance;
        internal static readonly IClothing HatInstance = Hat.HatInstance;
        internal static readonly IClothing HolidayHatInstance = HolidayHat.HolidayHatInstance;
        internal static readonly IClothing CowBellInstance = CowBell.CowBellInstance;
        internal static readonly IClothing BikiniBottomInstance = BikiniBottom.BikiniBottomInstance;
        internal static readonly IClothing LoinclothInstance = Loincloth.LoinclothInstance;
        internal static readonly IClothing HolidayOutfitInstance = HolidayOutfit.HolidayOutfitInstance;
        internal static readonly IClothing LeaderOutfitInstance = LeaderOutfit.LeaderOutfitInstance;


        internal static readonly List<IClothing> All = new List<IClothing>
        {
            OverallInstance, OverallBottomInstance, ShirtInstance, BikiniInstance, BikiniBottomInstance, LoinclothInstance, HolidayOutfitInstance, LeaderOutfitInstance
        };

        //internal static List<ClothingAccessory> Accessories = new List<IClothingConfigurator<BasicState>>()
        //{
        //    Hat, CowBell, HolidayHat
        //};
    }
}
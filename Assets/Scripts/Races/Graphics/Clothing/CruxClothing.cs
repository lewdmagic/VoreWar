#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace CruxClothing
{
    internal static class NecklaceGold
    {
        internal static readonly IClothing NecklaceGoldInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                { };
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(0);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(13);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Crux[310]);
                output["Clothing2"].Sprite(input.Sprites.Crux[311]);
            });
        });
    }

    internal static class NecklaceCrux
    {
        internal static readonly IClothing NecklaceCruxInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                { };
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(0);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(13);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Crux[312]);
                output["Clothing2"].Sprite(input.Sprites.Crux[313]);
            });
        });
    }

    internal static class TShirt
    {
        internal static readonly IClothing TShirtInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardUsesPalettes = false;
                output.DiscardSprite = input.Sprites.Crux[389];
                output.Type = 102;
                output.OccupiesAllSlots = false;
                output.RevealsDick = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(14);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Coloring(ColorMap.GetClothingColor(input.U.ClothingColor));

                if (input.U.HasBreasts)
                {
                    if (input.A.PredatorComponent?.VisibleFullness == 0)
                    {
                        if (input.U.BreastSize <= 1)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Crux[315]);
                        }
                        else if (input.U.BreastSize <= 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Crux[316]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Crux[317]);
                        }
                    }
                    else if (input.A.GetStomachSize(23) <= 4)
                    {
                        if (input.U.BreastSize <= 1)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Crux[319]);
                        }
                        else if (input.U.BreastSize <= 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Crux[320]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Crux[321]);
                        }
                    }
                    else
                    {
                        if (input.U.BreastSize <= 1)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Crux[323]);
                        }
                        else if (input.U.BreastSize <= 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Crux[324]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Crux[325]);
                        }
                    }
                }
                else
                {
                    if (input.A.PredatorComponent?.VisibleFullness == 0)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Crux[314]);
                    }
                    else if (input.A.GetStomachSize(23) <= 4)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Crux[318]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Crux[322]);
                    }
                }
            });
        });
    }

    internal static class NetShirt
    {
        internal static readonly IClothing NetShirtInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.MaleOnly = true;
                output.DiscardUsesPalettes = false;
                output.DiscardSprite = input.Sprites.Crux[390];
                output.Type = 103;
                output.OccupiesAllSlots = false;
                output.RevealsDick = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(14);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Coloring(ColorMap.GetClothingColor(input.U.ClothingColor));
                output["Clothing1"].Sprite(input.Sprites.Crux[326]);
            });
        });
    }

    internal static class RaggedBra
    {
        internal static readonly IClothing RaggedBraInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.FemaleOnly = true;
                output.DiscardUsesPalettes = false;
                output.DiscardSprite = input.Sprites.Crux[391];
                output.Type = 104;
                output.OccupiesAllSlots = false;
                output.RevealsBreasts = true;
                output.RevealsDick = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(14);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Coloring(ColorMap.GetClothingColor(input.U.ClothingColor));
                if (input.U.BreastSize <= 1)
                {
                    output["Clothing1"].Sprite(input.Sprites.Crux[327]);
                }
                else if (input.U.BreastSize <= 3)
                {
                    output["Clothing1"].Sprite(input.Sprites.Crux[328]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Crux[329]);
                }
            });
        });
    }

    internal static class LabCoat
    {
        internal static readonly IClothing LabCoatInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardUsesPalettes = false;
                output.DiscardSprite = input.Sprites.Crux[397];
                output.Type = 105;
                output.OccupiesAllSlots = false;
                output.RevealsDick = true;
                output.RevealsBreasts = true;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(1);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(14);
                output["Clothing1"].Coloring(Color.white);
                if (input.A.GetStomachSize(23) <= 5)
                {
                    output["Clothing1"].Layer(14);
                    output["Clothing1"].Sprite(input.U.BreastSize <= 3 ? input.Sprites.Crux[341] : input.Sprites.Crux[344]);
                }
                else if (input.A.GetStomachSize(23) <= 10)
                {
                    output["Clothing1"].Layer(14);
                    output["Clothing1"].Sprite(input.U.BreastSize <= 3 ? input.Sprites.Crux[342] : input.Sprites.Crux[345]);
                }
                else
                {
                    output["Clothing1"].Layer(11);
                    output["Clothing1"].Sprite(input.U.BreastSize <= 3 ? input.Sprites.Crux[343] : input.Sprites.Crux[346]);
                }

                output["Clothing2"].Sprite(input.Sprites.Crux[347]);
            });
        });
    }

    internal static class Boxers1
    {
        internal static readonly IClothing Boxers1Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardUsesPalettes = false;
                output.DiscardSprite = input.Sprites.Crux[392];
                output.Type = 106;
                output.OccupiesAllSlots = false;
                output.RevealsBreasts = true;
                output.DiscardUsesColor2 = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(9);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Coloring(ColorMap.GetClothingColor(input.U.ClothingColor2));
                if (input.U.DickSize == -1)
                {
                    output["Clothing1"].Sprite(input.Sprites.Crux[330]);
                }
                else if (input.U.DickSize <= 2)
                {
                    output["Clothing1"].Sprite(input.Sprites.Crux[331]);
                }
                else if (input.U.DickSize <= 5)
                {
                    output["Clothing1"].Sprite(input.Sprites.Crux[332]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Crux[333]);
                }
            });
        });
    }

    internal static class Boxers2
    {
        internal static readonly IClothing Boxers2Instance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardUsesPalettes = false;
                output.DiscardSprite = input.Sprites.Crux[393];
                output.Type = 107;
                output.OccupiesAllSlots = false;
                output.RevealsBreasts = true;
                output.DiscardUsesColor2 = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(9);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Coloring(ColorMap.GetClothingColor(input.U.ClothingColor2));
                if (input.U.DickSize == -1)
                {
                    output["Clothing1"].Sprite(input.Sprites.Crux[334]);
                }
                else if (input.U.DickSize <= 2)
                {
                    output["Clothing1"].Sprite(input.Sprites.Crux[335]);
                }
                else if (input.U.DickSize <= 5)
                {
                    output["Clothing1"].Sprite(input.Sprites.Crux[336]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Crux[337]);
                }
            });
        });
    }

    internal static class CruxJeans
    {
        internal static readonly IClothing CruxJeansInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardUsesPalettes = false;
                output.DiscardSprite = input.Sprites.Crux[396];
                output.Type = 108;
                output.OccupiesAllSlots = false;
                output.RevealsBreasts = true;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(9);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Crux[340]);
            });
        });
    }

    internal static class FannyBag
    {
        internal static readonly IClothing FannyBagInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardUsesPalettes = false;
                output.DiscardUsesColor2 = true;
                output.DiscardSprite = input.Sprites.Crux[395];
                output.Type = 109;
                output.OccupiesAllSlots = false;
                output.RevealsDick = true;
                output.RevealsBreasts = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(9);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Coloring(ColorMap.GetClothingColor(input.U.ClothingColor2));
                output["Clothing1"].Sprite(input.Sprites.Crux[339]);
            });
        });
    }

    internal static class BeltBags
    {
        internal static readonly IClothing BeltBagsInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardUsesPalettes = false;
                output.DiscardUsesColor2 = true;
                output.DiscardSprite = input.Sprites.Crux[394];
                output.Type = 110;
                output.OccupiesAllSlots = false;
                output.RevealsDick = true;
                output.RevealsBreasts = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(9);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Coloring(ColorMap.GetClothingColor(input.U.ClothingColor2));
                output["Clothing1"].Sprite(input.Sprites.Crux[338]);
            });
        });
    }

    internal static class Rags
    {
        internal static readonly IClothing RagsInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardUsesPalettes = false;
                output.DiscardSprite = input.Sprites.Crux[399];
                output.Type = 111;
                output.OccupiesAllSlots = true;
                output.RevealsBreasts = true;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(14);
                output["Clothing2"].Coloring(Color.white);
                output["Clothing1"].Layer(14);
                output["Clothing1"].Coloring(Color.white);
                if (input.U.HasBreasts)
                {
                    output["Clothing1"].Layer(14);
                    // TODO changing discard sprite dymanically is currently not supported
                    //output.DiscardSprite = input.Sprites.Crux[398];
                    output["Clothing1"].Sprite(input.Sprites.Crux[348]);
                    output["Clothing2"].Sprite(null);
                }
                else
                {
                    output["Clothing1"].Layer(9);
                    output["Clothing2"].Layer(14);
                    // TODO changing discard sprite dymanically is currently not supported
                    // output.DiscardSprite = input.Sprites.Crux[399];
                    output["Clothing1"].Sprite(input.Sprites.Crux[349]);
                    output["Clothing2"].Sprite(input.Sprites.Crux[350]);
                }
            });
        });
    }

    internal static class SlaveCollar
    {
        internal static readonly IClothing SlaveCollarInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.DiscardUsesPalettes = false;
                output.DiscardSprite = input.Sprites.Crux[400];
                output.Type = 112;
                output.OccupiesAllSlots = true;
                output.RevealsDick = true;
                output.RevealsBreasts = true;
                output.FixedColor = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(14);
                output["Clothing1"].Coloring(Color.white);
                output["Clothing1"].Sprite(input.Sprites.Crux[350]);
            });
        });
    }

    internal static class CruxClothingTypes
    {
        internal static readonly IClothing NecklaceGoldInstance = NecklaceGold.NecklaceGoldInstance;
        internal static readonly IClothing NecklaceCruxInstance = NecklaceCrux.NecklaceCruxInstance;
        internal static readonly IClothing TShirtInstance = TShirt.TShirtInstance;
        internal static readonly IClothing NetShirtInstance = NetShirt.NetShirtInstance;
        internal static readonly IClothing RaggedBraInstance = RaggedBra.RaggedBraInstance;
        internal static readonly IClothing LabCoatInstance = LabCoat.LabCoatInstance;
        internal static readonly IClothing Boxers1Instance = Boxers1.Boxers1Instance;
        internal static readonly IClothing Boxers2Instance = Boxers2.Boxers2Instance;
        internal static readonly IClothing CruxJeansInstance = CruxJeans.CruxJeansInstance;
        internal static readonly IClothing FannyBagInstance = FannyBag.FannyBagInstance;
        internal static readonly IClothing BeltBagsInstance = BeltBags.BeltBagsInstance;
        internal static readonly IClothing RagsInstance = Rags.RagsInstance;
        internal static readonly IClothing SlaveCollarInstance = SlaveCollar.SlaveCollarInstance;


        internal static readonly List<IClothing<IParameters>> All = new List<IClothing<IParameters>>
        {
            TShirtInstance, NetShirtInstance, RaggedBraInstance, LabCoatInstance, Boxers1Instance, Boxers2Instance, CruxJeansInstance, FannyBagInstance, BeltBagsInstance, RagsInstance, SlaveCollarInstance
        };

        internal static List<IClothing<IParameters>> Accessories = new List<IClothing<IParameters>>
        {
            NecklaceGoldInstance, NecklaceCruxInstance
        };
    }
}
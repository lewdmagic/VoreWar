#region

using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.Mercs
{
    internal static class DewSprites
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("DewSprite", "DewSprites");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 14,
                    StomachSize = 16,
                    HasTail = false,
                    FavoredStat = Stat.Endurance,
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Resilient,
                        TraitType.IronGut,
                        TraitType.EnthrallingDepths
                    },
                    RaceDescription = "",
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.BreastVore, VoreType.Anal },
                });
                output.BreastSizes = () => 9;

                output.BodySizes = 3;
                output.EyeTypes = 6;
                output.MouthTypes = 6;
                output.HairStyles = 3;
                output.CanBeGender = new List<Gender> { Gender.Female };
                output.AllowedMainClothingTypes.Set(
                    Top.TopInstance
                );
                output.AllowedWaistTypes.Set(
                    Bottom1.Bottom1Instance,
                    Bottom2.Bottom2Instance,
                    Bottom3.Bottom3Instance,
                    Bottom4.Bottom4Instance
                );
            });


            builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.DewSprite[36]);
            });
            builder.RenderSingle(SpriteType.Eyes, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.DewSprite[27 + input.U.EyeType]);
            });
            builder.RenderSingle(SpriteType.Mouth, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.DewSprite[45 + input.U.MouthType]);
            });
            builder.RenderSingle(SpriteType.Hair, 8, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.DewSprite[33 + input.U.HairStyle]);
            });
            builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.BodySize == 0)
                {
                    output.Sprite(input.Sprites.DewSprite[52]);
                }
                else if (input.U.BodySize == 1)
                {
                    output.Sprite(input.Sprites.DewSprite[21]);
                }
                else
                {
                    output.Sprite(input.Sprites.DewSprite[57]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent, 11, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                int sprite = input.A.GetWeaponSprite();
                switch (sprite)
                {
                    case 1:
                        output.Sprite(input.Sprites.DewSprite[43]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.DewSprite[41]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.DewSprite[37]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasBreasts)
                {
                    output.Sprite(input.Sprites.DewSprite[12 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.HasBelly)
                {
                    output.Sprite(input.Sprites.DewSprite[0 + input.A.GetStomachSize(11)]);
                }
            });

            builder.RenderSingle(SpriteType.Weapon, 10, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                int sprite = input.A.GetWeaponSprite();
                switch (sprite)
                {
                    case 0:
                        output.Sprite(input.Sprites.DewSprite[44]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.DewSprite[51]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.DewSprite[40]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.DewSprite[39]);
                        return;
                    case 4:
                    case 5:
                        output.Sprite(input.Sprites.DewSprite[42]);
                        return;
                    case 6:
                    case 7:
                        output.Sprite(input.Sprites.DewSprite[38]);
                        return;
                }

                output.Sprite(input.Sprites.DewSprite[51]);
            });


            builder.RunBefore(Defaults.Finalize);

            builder.RandomCustom(Defaults.RandomCustom);
        });

        private static class Bottom1
        {
            internal static readonly IClothing Bottom1Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(5);
                    output["Clothing1"].Coloring(Color.white);
                    if (input.U.BodySize == 0)
                    {
                        output["Clothing1"].Sprite(input.Sprites.DewSprite[53]);
                    }
                    else if (input.U.BodySize == 1)
                    {
                        output["Clothing1"].Sprite(input.Sprites.DewSprite[22]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.DewSprite[58]);
                    }
                });
            });
        }

        private static class Bottom2
        {
            internal static readonly IClothing Bottom2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { output.RevealsBreasts = true; });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(5);
                    output["Clothing1"].Coloring(Color.white);
                    if (input.U.BodySize == 0)
                    {
                        output["Clothing1"].Sprite(input.Sprites.DewSprite[54]);
                    }
                    else if (input.U.BodySize == 1)
                    {
                        output["Clothing1"].Sprite(input.Sprites.DewSprite[23]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.DewSprite[59]);
                    }
                });
            });
        }

        private static class Bottom3
        {
            internal static readonly IClothing Bottom3Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { output.RevealsBreasts = true; });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(5);
                    output["Clothing1"].Coloring(Color.white);
                    if (input.U.BodySize == 0)
                    {
                        output["Clothing1"].Sprite(input.Sprites.DewSprite[55]);
                    }
                    else if (input.U.BodySize == 1)
                    {
                        output["Clothing1"].Sprite(input.Sprites.DewSprite[24]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.DewSprite[60]);
                    }
                });
            });
        }

        private static class Bottom4
        {
            internal static readonly IClothing Bottom4Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { output.RevealsBreasts = true; });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(5);
                    output["Clothing1"].Coloring(Color.white);
                    if (input.U.BodySize == 0)
                    {
                        output["Clothing1"].Sprite(input.Sprites.DewSprite[56]);
                    }
                    else if (input.U.BodySize == 1)
                    {
                        output["Clothing1"].Sprite(input.Sprites.DewSprite[25]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.DewSprite[61]);
                    }
                });
            });
        }

        private static class Top
        {
            internal static readonly IClothing TopInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) => { output.RevealsBreasts = true; });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(17);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Sprite(input.U.HasBreasts ? input.Sprites.DewSprite[63 + input.U.BreastSize] : input.Sprites.DewSprite[62]);
                });
            });
        }
    }
}
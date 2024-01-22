#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.Mercs
{
    internal static class Puca
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
        
            builder.Setup(output =>
            {
                output.Names("Puca", "Puca");
                output.FlavorText(new FlavorText(
                    new Texts {  },
                    new Texts {  },
                    new Texts { "puca", "bunny", "lagomorph", "digger" },
                    new Dictionary<string, string>
                    {
                        [WeaponNames.Mace]        = "Shovel",
                        [WeaponNames.Axe]         = "Shovel",
                        [WeaponNames.SimpleBow]   = "Slingshot",
                        [WeaponNames.CompoundBow] = "Heavy Slingshot"
                    }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 7,
                    StomachSize = 14,
                    HasTail = true,
                    FavoredStat = Stat.Agility,
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.ArtfulDodge,
                        TraitType.Pounce,
                    },
                    RaceDescription = "A race of burrowers very true to their heritage, the Puca trust their shovels and feet above advanced technology. Many a foe has found themselves swallowed up by their deep dark tunnels.",
                });
                IClothing vest = Vest.VestInstance;
                IClothing loinCloth = LoinCloth.LoinClothInstance;
                IClothing shorts = Shorts.ShortsInstance;
                output.DickSizes = () => 3;

                output.AccessoryColors = 5;
                output.EyeTypes = 5;
                output.MouthTypes = 5;
                output.AllowedMainClothingTypes.Set(
                    vest
                );
                output.AllowedWaistTypes.Set(
                    loinCloth,
                    shorts
                );
            });


            builder.RenderSingle(SpriteType.Head, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Puca, input.U.AccessoryColor));
                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Puca[4]);
                    return;
                }

                output.Sprite(input.Sprites.Puca[3]);
            });

            builder.RenderSingle(SpriteType.Eyes, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Puca, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Puca[8 + input.U.EyeType]);
            });

            builder.RenderSingle(SpriteType.Mouth, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Puca, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Puca[16 + input.U.MouthType]);
            });

            builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Puca, input.U.AccessoryColor));
                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Puca[1]);
                }
                else if (input.A.IsAnalVoring)
                {
                    output.Sprite(input.Sprites.Puca[2]);
                }
                else
                {
                    output.Sprite(input.Sprites.Puca[0]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Puca, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Puca[23]);
            });

            builder.RenderSingle(SpriteType.BodyAccent2, 0, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Puca, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Puca[24]);
            });

            builder.RenderSingle(SpriteType.BodyAccent3, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Puca, input.U.AccessoryColor));
                if (input.U.HasBreasts)
                {
                    output.Sprite(input.Sprites.Puca[27]);
                }
            });

            builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Puca, input.U.AccessoryColor));
                if (input.U.HasBreasts)
                {
                    output.Sprite(input.Sprites.Puca[26]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Puca, input.U.AccessoryColor));
                if (input.A.HasBelly)
                {

                    output.Sprite(input.Sprites.Puca[37 + input.A.GetStomachSize(9)]);
                }
            });

            builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Puca, input.U.AccessoryColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect())
                {
                    if (input.A.PredatorComponent?.VisibleFullness < .75f)
                    {
                        output.Sprite(input.Sprites.Puca[33 + input.U.DickSize]).Layer(18);
                        return;
                    }

                    output.Sprite(input.Sprites.Puca[30 + input.U.DickSize]).Layer(12);
                    return;
                }

                output.Sprite(input.Sprites.Puca[30 + input.U.DickSize]).Layer(9);
            });

            builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.PucaBalls, input.U.AccessoryColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                output.AddOffset(0, -21 * .625f);

                int baseSize = input.U.DickSize / 3;
                int ballOffset = input.A.GetBallSize(21, .8f);
                int combined = Math.Min(baseSize + ballOffset + 2, 20);
                // Always false
                // if (combined == 21)
                // {
                //     output.AddOffset(0, -14 * .625f);
                // }
                // else if (combined == 20)
                if (combined == 20)
                {
                    output.AddOffset(0, -12 * .625f);
                }
                else if (combined >= 17)
                {
                    output.AddOffset(0, -8 * .625f);
                }

                if (ballOffset > 0)
                {
                    output.Sprite(input.Sprites.Balls[combined]);
                    return;
                }

                output.Sprite(input.Sprites.Balls[baseSize]);
            });

            builder.RenderSingle(SpriteType.Weapon, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    int pose = input.A.IsAttacking ? 1 : 0;
                    if (input.A.IsAnalVoring)
                    {
                        pose = 2;
                    }

                    if (input.A.GetWeaponSprite() < 4)
                    {
                        output.Sprite(input.Sprites.Puca[5 + pose]).Layer(3);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.Puca[13 + pose]).Layer(-1);
                    }
                }
            });


            builder.RunBefore((input, output) =>
            {
                if (input.U.Predator &&
                    (input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) ||
                     input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                    && input.A.GetStomachSize(9) == 9)
                {
                    float yOffset = 20 * .625f;
                    output.ChangeSprite(SpriteType.Body).AddOffset(0, yOffset);
                    output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, yOffset);
                    output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, yOffset);
                    output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, yOffset);
                    output.ChangeSprite(SpriteType.Head).AddOffset(0, yOffset);
                    output.ChangeSprite(SpriteType.Mouth).AddOffset(0, yOffset);
                    output.ChangeSprite(SpriteType.Eyes).AddOffset(0, yOffset);
                    output.ChangeSprite(SpriteType.Weapon).AddOffset(0, yOffset);
                    output.ChangeSprite(SpriteType.Breasts).AddOffset(0, yOffset);
                    output.ChangeSprite(SpriteType.Dick).AddOffset(0, yOffset);
                    output.ChangeSprite(SpriteType.Balls).AddOffset(0, yOffset);
                    output.ClothingShift = new Vector3(0, yOffset);
                }
                else
                {
                    output.ClothingShift = new Vector3();
                }

                if (input.A.HasBelly)
                {
                    Vector3 localScale;

                    if (input.A.PredatorComponent.VisibleFullness > 4)
                    {
                        float extraCap = input.A.PredatorComponent.VisibleFullness - 4;
                        float xScale = Mathf.Min(1 + extraCap / 5, 1.8f);
                        float yScale = Mathf.Min(1 + extraCap / 40, 1.1f);
                        localScale = new Vector3(xScale, yScale, 1);
                    }
                    else
                    {
                        localScale = new Vector3(1, 1, 1);
                    }

                    output.ChangeSprite(SpriteType.Belly).SetActive(true).SetLocalScale(localScale);
                }
            });

            builder.RandomCustom(Defaults.RandomCustom);
        });


        private static class Vest
        {
            internal static readonly IClothing VestInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(17);
                    int spriteNum;
                    int femaleMod = input.U.HasBreasts ? 19 : 0;
                    if (input.A.IsAnalVoring)
                    {
                        spriteNum = 29 + femaleMod;
                    }
                    else
                    {
                        spriteNum = 28 + femaleMod;
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrict, input.U.ClothingColor));
                    output["Clothing1"].Sprite(input.Sprites.Puca[spriteNum]);
                });
            });
        }

        private static class LoinCloth
        {
            internal static readonly IClothing LoinClothInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(10);
                    var spriteNum = input.A.IsAnalVoring ? 22 : 21;

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrict, input.U.ClothingColor));
                    output["Clothing1"].Sprite(input.Sprites.Puca[spriteNum]);
                });
            });
        }

        private static class Shorts
        {
            internal static readonly IClothing ShortsInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(10);
                    if (!input.A.IsAnalVoring)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Puca[36]);
                        output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrict, input.U.ClothingColor));
                    }
                });
            });
        }
    }
}
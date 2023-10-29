#region

using System;
using UnityEngine;

#endregion

internal static class Puca
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Puca, input.Actor.Unit.AccessoryColor));
            if (input.Actor.IsOralVoring)
            {
                output.Sprite(input.Sprites.Puca[4]);
                return;
            }

            output.Sprite(input.Sprites.Puca[3]);
        });

        builder.RenderSingle(SpriteType.Eyes, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Puca, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Puca[8 + input.Actor.Unit.EyeType]);
        });

        builder.RenderSingle(SpriteType.Mouth, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Puca, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Puca[16 + input.Actor.Unit.MouthType]);
        });

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Puca, input.Actor.Unit.AccessoryColor));
            if (input.Actor.IsAttacking)
            {
                output.Sprite(input.Sprites.Puca[1]);
            }
            else if (input.Actor.IsAnalVoring)
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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Puca, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Puca[23]);
        });

        builder.RenderSingle(SpriteType.BodyAccent2, 0, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Puca, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Puca[24]);
        });

        builder.RenderSingle(SpriteType.BodyAccent3, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Puca, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Sprites.Puca[27]);
            }
        });

        builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Puca, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.HasBreasts)
            {
                output.Sprite(input.Sprites.Puca[26]);
            }
        });

        builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Puca, input.Actor.Unit.AccessoryColor));
            if (input.Actor.HasBelly)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(9) == 9)
                {
                    output.Sprite(input.Sprites.Puca[50]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(9) == 9)
                {
                    output.Sprite(input.Sprites.Puca[49]);
                    return;
                }

                output.Sprite(input.Sprites.Puca[37 + input.Actor.GetStomachSize(9)]);
            }
        });

        builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Puca, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect())
            {
                if (input.Actor.PredatorComponent?.VisibleFullness < .75f)
                {
                    output.Sprite(input.Sprites.Puca[33 + input.Actor.Unit.DickSize]).Layer(18);
                    return;
                }

                output.Sprite(input.Sprites.Puca[30 + input.Actor.Unit.DickSize]).Layer(12);
                return;
            }

            output.Sprite(input.Sprites.Puca[30 + input.Actor.Unit.DickSize]).Layer(9);
        });

        builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.PucaBalls, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            output.AddOffset(0, -21 * .625f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .8f) == 21)
            {
                output.Sprite(input.Sprites.Balls[24]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .8f) == 21)
            {
                output.Sprite(input.Sprites.Balls[23]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .8f) == 20)
            {
                output.Sprite(input.Sprites.Balls[22]).AddOffset(0, -15 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .8f) == 19)
            {
                output.Sprite(input.Sprites.Balls[21]).AddOffset(0, -14 * .625f);
                return;
            }

            int baseSize = input.Actor.Unit.DickSize / 3;
            int ballOffset = input.Actor.GetBallSize(21, .8f);
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
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                int pose = input.Actor.IsAttacking ? 1 : 0;
                if (input.Actor.IsAnalVoring)
                {
                    pose = 2;
                }

                if (input.Actor.GetWeaponSprite() < 4)
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
            if (input.Actor.Unit.Predator &&
                (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) ||
                 input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                && input.Actor.GetStomachSize(9) == 9)
            {
                float yOffset = 20 * .625f;
                output.changeSprite(SpriteType.Body).AddOffset(0, yOffset);
                output.changeSprite(SpriteType.BodyAccent).AddOffset(0, yOffset);
                output.changeSprite(SpriteType.BodyAccent2).AddOffset(0, yOffset);
                output.changeSprite(SpriteType.BodyAccent3).AddOffset(0, yOffset);
                output.changeSprite(SpriteType.Head).AddOffset(0, yOffset);
                output.changeSprite(SpriteType.Mouth).AddOffset(0, yOffset);
                output.changeSprite(SpriteType.Eyes).AddOffset(0, yOffset);
                output.changeSprite(SpriteType.Weapon).AddOffset(0, yOffset);
                output.changeSprite(SpriteType.Breasts).AddOffset(0, yOffset);
                output.changeSprite(SpriteType.Dick).AddOffset(0, yOffset);
                output.changeSprite(SpriteType.Balls).AddOffset(0, yOffset);
                output.ClothingShift = new Vector3(0, yOffset);
            }
            else
            {
                output.ClothingShift = new Vector3();
            }

            if (input.Actor.HasBelly)
            {
                Vector3 localScale;

                if (input.Actor.PredatorComponent.VisibleFullness > 4)
                {
                    float extraCap = input.Actor.PredatorComponent.VisibleFullness - 4;
                    float xScale = Mathf.Min(1 + extraCap / 5, 1.8f);
                    float yScale = Mathf.Min(1 + extraCap / 40, 1.1f);
                    localScale = new Vector3(xScale, yScale, 1);
                }
                else
                {
                    localScale = new Vector3(1, 1, 1);
                }

                output.changeSprite(SpriteType.Belly).SetActive(true).SetLocalScale(localScale);
            }
        });

        builder.RandomCustom(Defaults.RandomCustom);
    });


    private static class Vest
    {
        internal static IClothing VestInstance = ClothingBuilder.Create(builder =>
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
                int femaleMod = input.Actor.Unit.HasBreasts ? 19 : 0;
                if (input.Actor.IsAnalVoring)
                {
                    spriteNum = 29 + femaleMod;
                }
                else
                {
                    spriteNum = 28 + femaleMod;
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, input.Actor.Unit.ClothingColor));
                output["Clothing1"].Sprite(input.Sprites.Puca[spriteNum]);
            });
        });
    }

    private static class LoinCloth
    {
        internal static IClothing LoinClothInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);
                int spriteNum;
                if (input.Actor.IsAnalVoring)
                {
                    spriteNum = 22;
                }
                else
                {
                    spriteNum = 21;
                }

                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, input.Actor.Unit.ClothingColor));
                output["Clothing1"].Sprite(input.Sprites.Puca[spriteNum]);
            });
        });
    }

    private static class Shorts
    {
        internal static IClothing ShortsInstance = ClothingBuilder.Create(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsBreasts = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);
                int spriteNum;
                if (input.Actor.IsAnalVoring)
                {
                    output["Clothing1"].Sprite(null);
                }
                else
                {
                    spriteNum = 36;
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, input.Actor.Unit.ClothingColor));
                    output["Clothing1"].Sprite(input.Sprites.Puca[spriteNum]);
                }
            });
        });
    }
}
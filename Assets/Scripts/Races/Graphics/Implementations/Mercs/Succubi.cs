#region

using System;
using UnityEngine;

#endregion

internal static class Succubi
{
    internal static IRaceData Instance = RaceBuilder.Create(Defaults.Default, builder =>
    {
        builder.Setup(output =>
        {
            output.BreastSizes = () => 4;
            output.WeightGainDisabled = true;
            output.SpecialAccessoryCount = 3;
            output.EyeTypes = 3;
            output.MouthTypes = 3;
            output.HairStyles = 4;

            output.BodySizes = 0;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.OldImp);
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.OldImpDark);
            output.ClothingShift = new Vector3(0, 32 * .625f, 0);
            output.AvoidedMainClothingTypes = 0;

            output.ClothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing);
            output.AllowedMainClothingTypes.Set(
                ClothingTypes.BikiniTopInstance,
                ClothingTypes.BeltTopInstance,
                ClothingTypes.StrapTopInstance,
                ClothingTypes.BlackTopInstance,
                RaceSpecificClothing.SuccubusDressInstance,
                RaceSpecificClothing.SuccubusLeotardInstance
            );


            output.AllowedWaistTypes.Set(
                ClothingTypes.BikiniBottomInstance,
                ClothingTypes.LoinclothInstance
            );
        });


        builder.RenderSingle(SpriteType.Head, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImp, input.Actor.Unit.SkinColor));
            output.Sprite(input.Sprites.Succubi[input.Actor.IsOralVoring ? 21 : 20]);
        });
        builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HairRedKeyStrict, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Succubi[27 + input.Actor.Unit.EyeType]);
        });
        builder.RenderSingle(SpriteType.Mouth, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImp, input.Actor.Unit.SkinColor));
            output.Sprite(input.Actor.IsOralVoring ? null : input.Sprites.Succubi[30 + input.Actor.Unit.MouthType]);
        });
        builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HairRedKeyStrict, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Succubi[Math.Min(75 + input.Actor.Unit.HairStyle, 78)]);
        });
        builder.RenderSingle(SpriteType.Hair2, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HairRedKeyStrict, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Succubi[Math.Min(79 + input.Actor.Unit.HairStyle, 82)]);
        });
        builder.RenderSingle(SpriteType.Hair3, 0, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HairRedKeyStrict, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Succubi[Math.Min(83 + input.Actor.Unit.HairStyle, 88)]);
        });
        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImp, input.Actor.Unit.SkinColor));
            if (input.Actor.IsUnbirthing || input.Actor.IsAnalVoring)
            {
                output.Sprite(input.Sprites.Succubi[4]);
            }
            else
            {
                //int sizeOffset = input.Actor.PredatorComponent?.VisibleFullness > .25f ? 1 : 0;
                int sizeOffset = 1;
                int attackingOffset = input.Actor.IsAttacking ? 2 : 0;
                output.Sprite(input.Sprites.Succubi[sizeOffset + attackingOffset]);
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpRedKey, input.Actor.Unit.AccessoryColor));
            if (input.Actor.IsUnbirthing || input.Actor.IsAnalVoring)
            {
                output.Sprite(input.Sprites.Succubi[9]);
            }
            else
            {
                //int sizeOffset = input.Actor.PredatorComponent?.VisibleFullness > .25f ? 1 : 0;
                int sizeOffset = 1;
                int attackingOffset = input.Actor.IsAttacking ? 2 : 0;
                output.Sprite(input.Sprites.Succubi[5 + sizeOffset + attackingOffset]);
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImpDark, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Succubi[22]);
        });
        builder.RenderSingle(SpriteType.BodyAccent3, 0, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImpDark, input.Actor.Unit.AccessoryColor));
            int sizeOffset = input.Actor.PredatorComponent?.TailFullness > 0 ? 1 + input.Actor.GetTailSize(2) : 0;
            if (input.Actor.IsTailVoring)
            {
                output.Sprite(input.Sprites.Succubi[37 + sizeOffset]);
            }
            else
            {
                output.Sprite(input.Sprites.Succubi[33 + sizeOffset]);
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent4, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.HairRedKeyStrict, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Succubi[23]);
        });
        builder.RenderSingle(SpriteType.BodyAccent5, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            int sizeOffset = input.Actor.PredatorComponent?.TailFullness > 0 ? 1 : 0;
            if (input.Actor.IsTailVoring)
            {
                output.Sprite(input.Sprites.Succubi[41 + sizeOffset]);
            }
        });

        builder.RenderSingle(SpriteType.BodyAccessory, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImpDark, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Succubi[24 + input.Actor.Unit.SpecialAccessoryType]);
        });
        builder.RenderSingle(SpriteType.Breasts, 15, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImp, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.SquishedBreasts)
            {
                output.Sprite(input.Sprites.Succubi[59 + input.Actor.Unit.BreastSize]);
                return;
            }

            output.Sprite(input.Sprites.Succubi[63 + input.Actor.Unit.BreastSize]);
        });

        builder.RenderSingle(SpriteType.BreastShadow, 16, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ImpRedKey, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.SquishedBreasts)
            {
                output.Sprite(input.Sprites.Succubi[67 + input.Actor.Unit.BreastSize]);
                return;
            }

            output.Sprite(input.Sprites.Succubi[71 + input.Actor.Unit.BreastSize]);
        });

        builder.RenderSingle(SpriteType.Belly, 13, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImp, input.Actor.Unit.SkinColor));
            if (input.Actor.HasBelly)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                {
                    output.Sprite(input.Sprites.Succubi[88]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                {
                    if (input.Actor.GetStomachSize(15, 0.7f) == 15)
                    {
                        output.Sprite(input.Sprites.Succubi[91]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(15, 0.8f) == 15)
                    {
                        output.Sprite(input.Sprites.Succubi[90]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(15, 0.9f) == 15)
                    {
                        output.Sprite(input.Sprites.Succubi[89]);
                        return;
                    }
                }

                output.Sprite(input.Sprites.Succubi[43 + input.Actor.GetStomachSize()]);
            }
        });

        builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
        {
            Defaults.SpriteGens2[SpriteType.Dick].Invoke(input, output);
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImp, input.Actor.Unit.SkinColor));
        });
        
        builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.OldImp, input.Actor.Unit.SkinColor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            int baseSize = input.Actor.Unit.DickSize / 3;
            int ballOffset = input.Actor.GetBallSize(21, .8f);
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


        builder.RunBefore((input, output) =>
        {
            output.changeSprite(SpriteType.Dick).AddOffset(0, 30 * .625f);
            output.changeSprite(SpriteType.Balls).AddOffset(0, 33 * .625f);

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


        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.BodySize = 1;
            unit.ClothingColor2 = State.Rand.Next(8);
        });
    });
}
#region

using System;
using UnityEngine;

#endregion

internal static class Kobolds
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank<FacingFrontParameters>, builder =>
    {
        builder.Setup(output =>
        {
            output.BreastSizes = () => 3;
            output.DickSizes = () => 3;
            output.GentleAnimation = true;
            output.AllowedMainClothingTypes.Set(
                BeltTop.BeltTopInstance,
                Tabard.TabardInstance,
                Rags.RagsInstance
            );
            output.AvoidedMainClothingTypes = 1;
            output.AllowedWaistTypes.Set(
                BikiniBottom.BikiniBottomInstance,
                LoinCloth.LoinClothInstance
            );


            output.TailTypes = 2;
            output.HeadTypes = 3;
            output.SpecialAccessoryCount = 5;
            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Kobold);
        });


        builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kobold, input.Actor.Unit.AccessoryColor));
            int spr = 7 + 3 * input.Actor.Unit.HeadType;
            if (input.Params.FacingFront)
            {
                output.Layer(4);
                if (input.Actor.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Kobolds[spr + 1]);
                    return;
                }

                output.Sprite(input.Sprites.Kobolds[spr]);
                return;
            }

            output.Sprite(input.Sprites.Kobolds[spr + 2]).Layer(1);
        });

        builder.RenderSingle(SpriteType.Body, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kobold, input.Actor.Unit.AccessoryColor));
            if (input.Params.FacingFront)
            {
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Kobolds[1]);
                    return;
                }

                output.Sprite(input.Sprites.Kobolds[0]);
                return;
            }

            output.Sprite(input.Sprites.Kobolds[2]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 0, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kobold, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Params.FacingFront ? null : input.Sprites.Kobolds[3]);
        });
        builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Params.FacingFront)
            {
                output.Layer(4);
                if (input.Actor.IsAttacking)
                {
                    output.Sprite(input.Sprites.Kobolds[5]);
                    return;
                }

                output.Sprite(input.Sprites.Kobolds[4]);
                return;
            }

            output.Sprite(input.Sprites.Kobolds[6]).Layer(1);
        });

        builder.RenderSingle(SpriteType.BodyAccent3, -2, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Params.FacingFront == false)
            {
                return;
            }

            if (input.Actor.BestRanged == null && input.Actor.Unit.HasWeapon && input.Actor.IsAttacking == false)
            {
                output.Sprite(input.Sprites.Kobolds[16]);
            }
        });

        builder.RenderSingle(SpriteType.BodyAccent4, 6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kobold, input.Actor.Unit.AccessoryColor));
            int spr = 21 + 2 * input.Actor.Unit.SpecialAccessoryType;
            if (input.Params.FacingFront)
            {
                output.Sprite(input.Actor.IsOralVoring ? input.Sprites.Kobolds[spr + 1] : input.Sprites.Kobolds[spr]);
            }
        });

        builder.RenderSingle(SpriteType.Breasts, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kobold, input.Actor.Unit.AccessoryColor));
            if (Config.LizardsHaveNoBreasts)
            {
                return;
            }

            if (input.Actor.Unit.HasBreasts && input.Params.FacingFront)
            {
                output.Sprite(input.Sprites.Kobolds[42 + input.Actor.Unit.BreastSize]);
            }
        });

        builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kobold, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.Predator == false || input.Actor.HasBelly == false)
            {
                return;
            }

            if (input.Params.FacingFront)
            {
                output.Layer(15);
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(12) == 12)
                {
                    output.Sprite(input.Sprites.Kobolds[84]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    if (input.Actor.GetStomachSize(12, 0.7f) == 12)
                    {
                        output.Sprite(input.Sprites.Kobolds[110]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(12, 0.8f) == 12)
                    {
                        output.Sprite(input.Sprites.Kobolds[109]);
                        return;
                    }

                    if (input.Actor.GetStomachSize(12, 0.9f) == 12)
                    {
                        output.Sprite(input.Sprites.Kobolds[108]);
                        return;
                    }
                }

                output.Sprite(input.Sprites.Kobolds[71 + input.Actor.GetStomachSize(12)]);
                return;
            }

            output.Layer(2);
            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(12) == 12)
            {
                output.Sprite(input.Sprites.Kobolds[102]);
                return;
            }

            if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
            {
                if (input.Actor.GetStomachSize(12, 0.7f) == 12)
                {
                    output.Sprite(input.Sprites.Kobolds[116]);
                    return;
                }

                if (input.Actor.GetStomachSize(12, 0.8f) == 12)
                {
                    output.Sprite(input.Sprites.Kobolds[115]);
                    return;
                }

                if (input.Actor.GetStomachSize(12, 0.9f) == 12)
                {
                    output.Sprite(input.Sprites.Kobolds[114]);
                    return;
                }
            }

            output.Sprite(input.Sprites.Kobolds[89 + input.Actor.GetStomachSize(12)]);
        });

        builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kobold, input.Actor.Unit.AccessoryColor));
            output.Layer(input.Params.FacingFront ? 9 : 17);
            if (input.Actor.GetBallSize(10) > 2)
            {
                output.Layer(input.Params.FacingFront ? 7 : 15);
            }

            if (input.Actor.Unit.DickSize >= 0)
            {
                int spr = 33 + 3 * input.Actor.Unit.DickSize;
                if (input.Params.FacingFront == false)
                {
                    output.Sprite(input.Sprites.Kobolds[spr + 2]);
                    return;
                }

                output.Sprite(input.Sprites.Kobolds[input.Actor.IsErect() ? spr + 1 : spr]);
                return;
            }

            //if (input.State.FacingFront == false)
            //    Dick.layer = 4;
            output.Sprite(input.Sprites.Kobolds[input.Params.FacingFront ? 31 : 32]);
        });

        builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Kobold, input.Actor.Unit.AccessoryColor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            int baseSize = input.Actor.Unit.DickSize;
            output.Layer(input.Params.FacingFront ? 8 : 18);
            if (input.Actor.PredatorComponent?.BallsFullness > 0)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) && input.Actor.GetBallSize(16) == 16)
                {
                    output.Sprite(input.Sprites.Kobolds[62]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    if (input.Actor.GetBallSize(16, 0.7f) == 16)
                    {
                        output.Sprite(input.Sprites.Kobolds[113]);
                        return;
                    }

                    if (input.Actor.GetBallSize(16, 0.8f) == 16)
                    {
                        output.Sprite(input.Sprites.Kobolds[112]);
                        return;
                    }

                    if (input.Actor.GetBallSize(16, 0.9f) == 16)
                    {
                        output.Sprite(input.Sprites.Kobolds[111]);
                        return;
                    }
                }

                output.Sprite(input.Sprites.Kobolds[45 + baseSize + input.Actor.GetBallSize(16 - baseSize)]);
                return;
            }

            output.Sprite(input.Sprites.Kobolds[45 + baseSize]);
        });

        builder.RenderSingle(SpriteType.Weapon, 14, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.Params.FacingFront == false)
            {
                return;
            }

            if (input.Actor.BestRanged != null)
            {
                output.Sprite(input.Actor.IsAttacking ? null : input.Sprites.Kobolds[19]);
                return;
            }

            if (input.Actor.Unit.HasWeapon)
            {
                output.Sprite(input.Actor.IsAttacking ? input.Sprites.Kobolds[18] : input.Sprites.Kobolds[17]);
            }
        });


        builder.RunBefore((input, output) =>
        {
            if (input.Actor.IsAnalVoring || input.Actor.IsUnbirthing || input.Actor.IsCockVoring)
            {
                output.Params.FacingFront = false;
            }
            else if (input.Actor.Unit.TailType == 0 || input.Actor.IsOralVoring || input.Actor.IsAttacking)
            {
                output.Params.FacingFront = true;
            }
            else
            {
                output.Params.FacingFront = true;
            }

            if (input.Actor.Unit.Predator)
            {
                float ballsYOffset = 0;
                int ballSize = input.Actor.Unit.DickSize + input.Actor.GetBallSize(16 - input.Actor.Unit.DickSize);
                if (ballSize == 13)
                {
                    ballsYOffset = 14;
                }

                if (ballSize == 14)
                {
                    ballsYOffset = 16;
                }

                if (ballSize == 15)
                {
                    ballsYOffset = 24;
                }

                if (ballSize == 16)
                {
                    ballsYOffset = 30;
                }

                if (ballSize == 16 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                {
                    ballsYOffset = 30;
                }

                if (ballSize == 16 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                {
                    ballsYOffset = 30;
                }

                bool onBalls = ballsYOffset > 0;
                float stomachYOffset = 0;
                int stomachSize = input.Actor.GetStomachSize(12);
                if (stomachSize == 10)
                {
                    stomachYOffset = 6;
                }

                if (stomachSize == 11)
                {
                    stomachYOffset = 10;
                }

                if (stomachSize == 12)
                {
                    stomachYOffset = 20;
                }

                if (stomachSize == 12 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                {
                    stomachYOffset = 20;
                }

                if (stomachSize == 12 && input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                {
                    stomachYOffset = 20;
                }

                ballsYOffset *= .625f; //Change from pixels to units
                stomachYOffset *= .625f; //Change from pixels to units
                output.ChangeSprite(SpriteType.Balls).AddOffset(0, -ballsYOffset);
                output.ChangeSprite(SpriteType.Belly).AddOffset(0, -stomachYOffset);

                output.WholeBodyOffset = new Vector2(0, Math.Max(ballsYOffset, stomachYOffset));
                //AddOffset(Body, 0, ballsYOffset);
                //AddOffset(Head, 0, ballsYOffset);
                //AddOffset(BodyAccent, 0, ballsYOffset);
                //AddOffset(BodyAccent2, 0, ballsYOffset);
                //AddOffset(BodyAccent3, 0, ballsYOffset);
                //AddOffset(BodyAccent4, 0, ballsYOffset);
                //AddOffset(Weapon, 0, ballsYOffset);
                //AddOffset(Breasts, 0, ballsYOffset);
                //AddOffset(Dick, 0, ballsYOffset);
                //if (OnBalls == false)
                //    AddOffset(Balls, 0, ballsYOffset);
                //ClothingShift = new Vector3(0, ballsYOffset);
            }
            else
            {
                output.WholeBodyOffset = new Vector2(0, 0);
            }
        });

        builder.RandomCustom(Defaults.RandomCustom);
    });

    private static class BikiniBottom
    {
        internal static readonly IClothing<FacingFrontParameters> BikiniBottomInstance = ClothingBuilder.Create<FacingFrontParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output ) =>
            {
                
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(11);
                output["Clothing1"].Layer(10);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, input.Actor.Unit.ClothingColor));
                output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, input.Actor.Unit.ClothingColor));
                if (input.Params.FacingFront)
                {
                    output["Clothing1"].Sprite(input.Sprites.Kobolds[105]);
                    if (input.Actor.Unit.HasDick)
                    {
                        output["Clothing2"].Sprite(input.Sprites.Kobolds[106]);
                    }
                }
                else
                {
                    output.RevealsDick = true;
                    output["Clothing1"].Sprite(input.Sprites.Kobolds[107]);
                }
            });
        });
    }

    private static class Tabard
    {
        internal static readonly IClothing<FacingFrontParameters> TabardInstance = ClothingBuilder.Create<FacingFrontParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsDick = true;
                output.RevealsBreasts = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(20);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, input.Actor.Unit.ClothingColor));
                if (input.Params.FacingFront)
                {
                    output["Clothing1"].Layer(20);
                    if (input.Actor.Unit.BreastSize > 1)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Kobolds[88]);
                    }
                    else if (input.Actor.Unit.BreastSize == 0)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Kobolds[87]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Kobolds[85]);
                    }
                }
                else
                {
                    output["Clothing1"].Layer(-1);
                    output["Clothing1"].Sprite(input.Sprites.Kobolds[86]);
                }
            });
        });
    }

    private static class LoinCloth
    {
        internal static readonly IClothing<FacingFrontParameters> LoinClothInstance = ClothingBuilder.Create<FacingFrontParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output ) =>
            {
                
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);
                output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.ClothingStrict, input.Actor.Unit.ClothingColor));
                if (input.Params.FacingFront)
                {
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Sprite(input.Sprites.Kobolds[68]);
                }
                else
                {
                    output["Clothing1"].Layer(1);
                    output.RevealsDick = true;
                    output["Clothing1"].Sprite(input.Sprites.Kobolds[70]);
                }
            });
        });
    }

    private static class Rags
    {
        internal static readonly IClothing<FacingFrontParameters> RagsInstance = ClothingBuilder.Create<FacingFrontParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output ) =>
            {
                output.RevealsDick = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing2"].Layer(11);
                output["Clothing1"].Layer(10);
                if (input.Params.FacingFront)
                {
                    output["Clothing1"].Sprite(input.Sprites.Kobolds[63]);
                    output["Clothing2"].Sprite(input.Sprites.Kobolds[66]);
                }
                else
                {
                    output["Clothing1"].Sprite(input.Sprites.Kobolds[65]);
                    output["Clothing2"].Sprite(null);
                }
            });
        });
    }

    private static class BeltTop
    {
        internal static readonly IClothing<FacingFrontParameters> BeltTopInstance = ClothingBuilder.Create<FacingFrontParameters>(builder =>
        {
            builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
            {
                output.RevealsDick = true;
                output.FemaleOnly = true;
            });

            builder.RenderAll((input, output) =>
            {
                output["Clothing1"].Layer(10);
                if (input.Params.FacingFront)
                {
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Sprite(input.Sprites.Kobolds[103]);
                }
                else
                {
                    output["Clothing1"].Layer(1);
                    output["Clothing1"].Sprite(input.Sprites.Kobolds[104]);
                }
            });
        });
    }
}
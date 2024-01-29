#region

using System;
using System.Collections.Generic;
using Races.Graphics.Implementations.MainRaces;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.Mercs
{
    internal static class Kobolds
    {

        private static bool IsFacingFront(Actor_Unit actor)
        {
            if (actor.IsAnalVoring || actor.IsUnbirthing || actor.IsCockVoring)
            {
                return false;
            }
            else if (actor.Unit.TailType == 0 || actor.IsOralVoring || actor.IsAttacking)
            {
                return true;
            }
            else
            {
                return true;
            }
        }

        private static readonly Func<IClothingRenderInput, FacingFrontParameters> FacingFrontCalc = renderInput =>
        {
            return new FacingFrontParameters()
            {
                FacingFront = IsFacingFront(renderInput.A)
            };
        };
        
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Kobold", "Kobolds");
                output.FlavorText(new FlavorText(
                    new Texts {  },
                    new Texts {  },
                    new Texts { "kobold", "little lizard", "little reptile" },
                    new Dictionary<string, string>
                    {
                        [WeaponNames.Mace]        = "Pickax",
                        [WeaponNames.Axe]         = "Pickax",
                        [WeaponNames.SimpleBow]   = "Dart",
                        [WeaponNames.CompoundBow] = "Dart",
                        [WeaponNames.Claw]        = "Fist"
                    }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 9,
                    StomachSize = 12,
                    HasTail = true,
                    FavoredStat = Stat.Agility,
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.ProlificBreeder,
                        TraitType.EasyToVore,
                        TraitType.Replaceable,
                    },
                    RaceDescription = "",
                    RaceAI = RaceAI.ServantRace
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.TailTypes, "Preferred Facing");
                });
                output.BreastSizes = () => 3;
                output.DickSizes = () => 3;
                output.GentleAnimation = true;
                output.AllowedMainClothingTypes.Set(
                    BeltTop.BeltTopInstance.Create(FacingFrontCalc),
                    Tabard.TabardInstance.Create(FacingFrontCalc),
                    Rags.RagsInstance.Create(FacingFrontCalc)
                );
                output.AvoidedMainClothingTypes = 1;
                output.AllowedWaistTypes.Set(
                    BikiniBottom.BikiniBottomInstance.Create(FacingFrontCalc),
                    LoinCloth.LoinClothInstance.Create(FacingFrontCalc)
                );


                output.TailTypes = 2;
                output.HeadTypes = 3;
                output.SpecialAccessoryCount = 5;
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.Kobold);
            });


            builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Kobold, input.U.AccessoryColor));
                int spr = 7 + 3 * input.U.HeadType;
                if (IsFacingFront(input.A))
                {
                    output.Layer(4);
                    if (input.A.IsOralVoring)
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
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Kobold, input.U.AccessoryColor));
                if (IsFacingFront(input.A))
                {
                    if (input.A.IsAttacking)
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
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Kobold, input.U.AccessoryColor));
                output.Sprite(IsFacingFront(input.A) ? null : input.Sprites.Kobolds[3]);
            });
            builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (IsFacingFront(input.A))
                {
                    output.Layer(4);
                    if (input.A.IsAttacking)
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
                if (IsFacingFront(input.A) == false)
                {
                    return;
                }

                if (input.A.BestRanged == null && input.U.HasWeapon && input.A.IsAttacking == false)
                {
                    output.Sprite(input.Sprites.Kobolds[16]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent4, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Kobold, input.U.AccessoryColor));
                int spr = 21 + 2 * input.U.SpecialAccessoryType;
                if (IsFacingFront(input.A))
                {
                    output.Sprite(input.A.IsOralVoring ? input.Sprites.Kobolds[spr + 1] : input.Sprites.Kobolds[spr]);
                }
            });

            builder.RenderSingle(SpriteType.Breasts, 8, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Kobold, input.U.AccessoryColor));
                if (Config.LizardsHaveNoBreasts)
                {
                    return;
                }

                if (input.U.HasBreasts && IsFacingFront(input.A))
                {
                    output.Sprite(input.Sprites.Kobolds[42 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Kobold, input.U.AccessoryColor));
                if (input.U.Predator == false || input.A.HasBelly == false)
                {
                    return;
                }

                if (IsFacingFront(input.A))
                {
                    output.Layer(15);
                    output.Sprite(input.Sprites.Kobolds[71 + input.A.GetStomachSize(12)]);
                    return;
                }

                output.Layer(2);
                output.Sprite(input.Sprites.Kobolds[89 + input.A.GetStomachSize(12)]);
            });

            builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Kobold, input.U.AccessoryColor));
                output.Layer(IsFacingFront(input.A) ? 9 : 17);
                if (input.A.GetBallSize(10) > 2)
                {
                    output.Layer(IsFacingFront(input.A) ? 7 : 15);
                }

                if (input.U.DickSize >= 0)
                {
                    int spr = 33 + 3 * input.U.DickSize;
                    if (IsFacingFront(input.A) == false)
                    {
                        output.Sprite(input.Sprites.Kobolds[spr + 2]);
                        return;
                    }

                    output.Sprite(input.Sprites.Kobolds[input.A.IsErect() ? spr + 1 : spr]);
                    return;
                }

                //if (input.State.FacingFront == false)
                //    Dick.layer = 4;
                output.Sprite(input.Sprites.Kobolds[IsFacingFront(input.A) ? 31 : 32]);
            });

            builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Kobold, input.U.AccessoryColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                int baseSize = input.U.DickSize;
                output.Layer(IsFacingFront(input.A) ? 8 : 18);
                if (input.A.PredatorComponent?.BallsFullness > 0)
                {
                    output.Sprite(input.Sprites.Kobolds[45 + baseSize + input.A.GetBallSize(16 - baseSize)]);
                    return;
                }

                output.Sprite(input.Sprites.Kobolds[45 + baseSize]);
            });

            builder.RenderSingle(SpriteType.Weapon, 14, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (IsFacingFront(input.A) == false)
                {
                    return;
                }

                if (input.A.BestRanged != null)
                {
                    output.Sprite(input.A.IsAttacking ? null : input.Sprites.Kobolds[19]);
                    return;
                }

                if (input.U.HasWeapon)
                {
                    output.Sprite(input.A.IsAttacking ? input.Sprites.Kobolds[18] : input.Sprites.Kobolds[17]);
                }
            });


            builder.RunBefore((input, output) =>
            {
                if (input.U.Predator)
                {
                    float ballsYOffset = 0;
                    int ballSize = input.U.DickSize + input.A.GetBallSize(16 - input.U.DickSize);
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

                    if (ballSize == 16 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.balls))
                    {
                        ballsYOffset = 30;
                    }

                    if (ballSize == 16 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls))
                    {
                        ballsYOffset = 30;
                    }

                    bool onBalls = ballsYOffset > 0;
                    float stomachYOffset = 0;
                    int stomachSize = input.A.GetStomachSize(12);
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

                    if (stomachSize == 12 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                    {
                        stomachYOffset = 20;
                    }

                    if (stomachSize == 12 && input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
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
            internal static readonly BindableClothing<FacingFrontParameters> BikiniBottomInstance = ClothingBuilder.CreateV2<FacingFrontParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output ) =>
                {
                
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(11);
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrict, input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrict, input.U.ClothingColor));
                    if (extra.FacingFront)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Kobolds[105]);
                        if (input.U.HasDick)
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
            internal static readonly BindableClothing<FacingFrontParameters> TabardInstance = ClothingBuilder.CreateV2<FacingFrontParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsDick = true;
                    output.RevealsBreasts = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(20);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrict, input.U.ClothingColor));
                    if (extra.FacingFront)
                    {
                        output["Clothing1"].Layer(20);
                        if (input.U.BreastSize > 1)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Kobolds[88]);
                        }
                        else if (input.U.BreastSize == 0)
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
            internal static readonly BindableClothing<FacingFrontParameters> LoinClothInstance = ClothingBuilder.CreateV2<FacingFrontParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output ) =>
                {
                
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(10);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.ClothingStrict, input.U.ClothingColor));
                    if (extra.FacingFront)
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
            internal static readonly BindableClothing<FacingFrontParameters> RagsInstance = ClothingBuilder.CreateV2<FacingFrontParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output ) =>
                {
                    output.RevealsDick = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(11);
                    output["Clothing1"].Layer(10);
                    if (extra.FacingFront)
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
            internal static readonly BindableClothing<FacingFrontParameters> BeltTopInstance = ClothingBuilder.CreateV2<FacingFrontParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsDick = true;
                    output.FemaleOnly = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(10);
                    if (extra.FacingFront)
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
}
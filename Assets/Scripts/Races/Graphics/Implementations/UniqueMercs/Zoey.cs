#region

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.UniqueMercs
{
    internal static class Zoey
    {

        private static BodyState CalcBodyState(Actor_Unit actor)
        {
            if (actor.AnimationController?.frameLists[0].currentlyActive ?? false)
            {
                if (actor.GetStomachSize(19) >= 17)
                {
                    return BodyState.SideBelly;
                }
                else
                {
                    return BodyState.SpinAttack;
                }
            }
            else if (actor.GetStomachSize(19) >= 18)
            {
                return BodyState.HighBelly;
            }
            else
            {
                return BodyState.Normal;
            }
        }
        
        private static readonly Func<IRenderInput, ZoeyParams> ZoeyCalc = renderInput =>
        {
            return new ZoeyParams()
            {
                BodyState = CalcBodyState(renderInput.A)
            };
        };
        
        internal enum BodyState
        {
            Normal,
            HighBelly,
            SideBelly,
            SpinAttack
        }

        internal class ZoeyParams : IParameters
        {
            internal BodyState BodyState = BodyState.Normal;
        }
    
        internal static readonly IRaceData Instance = RaceBuilder.CreateV2(Defaults.Blank, builder =>
        {
            RaceFrameList SpinEffect = new RaceFrameList(new int[2] { 25, 19 }, new float[2] { .375f, .375f });
            builder.Setup(output =>
            {
                output.Names("Zoey", "Zoey");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 12,
                    StomachSize = 40,
                    HasTail = true,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal },
                    ExpMultiplier = 1.6f,
                    PowerAdjustment = 3f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(14, 20),
                        Dexterity = new RaceStats.StatRange(8, 10),
                        Endurance = new RaceStats.StatRange(18, 20),
                        Mind = new RaceStats.StatRange(6, 10),
                        Will = new RaceStats.StatRange(12, 18),
                        Agility = new RaceStats.StatRange(14, 18),
                        Voracity = new RaceStats.StatRange(14, 18),
                        Stomach = new RaceStats.StatRange(14, 18),
                    },
                    RacialTraits = new List<Traits>()
                    {
                        Traits.Maul,
                        Traits.StrongGullet,
                        Traits.Biter,
                        Traits.Greedy,
                        Traits.BornToMove,
                        Traits.TailStrike,
                    },
                    RaceDescription = "An anthropomorphic tiger shark from another world.  Zoey is typically a lazy girl who loves watching movies and being a general couch-potato.  However, upon realizing she'd been isekai'd into the realm, her gluttony left her interested in trying to stomach the local warriors and monsters with some basic martial arts, joining whichever side would pay her first.",
                });
                output.BreastSizes = () => 5;

                output.CanBeGender = new List<Gender> { Gender.Female };
                output.GentleAnimation = true;
                output.ClothingColors = 0;

                output.AllowedMainClothingTypes.Set(
                    ZoeyTop.ZoeyTopInstance.Create(ZoeyCalc)
                );
            });


            builder.RenderSingle(SpriteType.Head, 5, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                switch (CalcBodyState(input.A))
                {
                    case BodyState.SpinAttack:
                    case BodyState.SideBelly:
                        return;
                    default:
                        if (input.A.IsOralVoring)
                        {
                            output.Sprite(input.Sprites.Zoey[3]);
                            return;
                        }

                        if (input.A.PredatorComponent?.Fullness > 2)
                        {
                            if (State.Rand.Next(650) == 0)
                            {
                                input.A.SetAnimationMode(1, .5f);
                            }

                            int specialMode = input.A.CheckAnimationFrame();
                            if (specialMode == 1)
                            {
                                output.Sprite(input.Sprites.Zoey[5]);
                                return;
                            }

                            output.Sprite(input.Sprites.Zoey[4]);
                            return;
                        }

                        output.Sprite(input.Sprites.Zoey[2]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.Hair, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                switch (CalcBodyState(input.A))
                {
                    case BodyState.SpinAttack:
                    case BodyState.SideBelly:
                        return;
                    default:
                        output.Sprite(input.Sprites.Zoey[7]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.Hair2, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                switch (CalcBodyState(input.A))
                {
                    case BodyState.SpinAttack:
                    case BodyState.SideBelly:
                        return;
                    default:
                        output.Sprite(input.Sprites.Zoey[8]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Layer(1);
                switch (CalcBodyState(input.A))
                {
                    case BodyState.HighBelly:
                        if (input.A.IsAttacking == false)
                        {
                            output.Sprite(input.Sprites.Zoey[9]);
                            return;
                        }

                        output.Sprite(input.Sprites.Zoey[10]);
                        return;
                    case BodyState.SideBelly:
                        output.Sprite(input.Sprites.Zoey[12]).Layer(4);
                        return;
                    case BodyState.SpinAttack:
                        output.Sprite(input.Sprites.Zoey[6]);
                        return;
                    default:
                        if (input.A.IsAttacking == false)
                        {
                            output.Sprite(input.Sprites.Zoey[0]);
                            return;
                        }

                        output.Sprite(input.Sprites.Zoey[1]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent, 0, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                switch (CalcBodyState(input.A))
                {
                    case BodyState.HighBelly:
                        output.Sprite(input.Sprites.Zoey[11]);
                        return;
                    case BodyState.SideBelly:
                        output.Sprite(input.Sprites.Zoey[13]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent2, 14, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.AnimationController.frameLists[0].currentlyActive)
                {
                    if (input.A.AnimationController.frameLists[0].currentTime >= SpinEffect.Times[input.A.AnimationController.frameLists[0].currentFrame])
                    {
                        input.A.AnimationController.frameLists[0].currentFrame++;
                        input.A.AnimationController.frameLists[0].currentTime = 0f;

                        if (input.A.AnimationController.frameLists[0].currentFrame >= SpinEffect.Frames.Length)
                        {
                            input.A.AnimationController.frameLists[0].currentlyActive = false;
                            input.A.AnimationController.frameLists[0].currentFrame = 0;
                            input.A.AnimationController.frameLists[0].currentTime = 0f;
                        }
                    }
                }
                else
                {
                    return;
                }

                output.Sprite(input.Sprites.Zoey[SpinEffect.Frames[input.A.AnimationController.frameLists[0].currentFrame]]);
            });

            builder.RenderSingle(SpriteType.Breasts, 10, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (CalcBodyState(input.A) == BodyState.SideBelly)
                {
                    output.Layer(3);
                }
                else
                {
                    output.Layer(10);
                }

                switch (CalcBodyState(input.A))
                {
                    case BodyState.SpinAttack:
                    case BodyState.SideBelly:
                        if (input.A.PredatorComponent.VisibleFullness > 1)
                        {
                            output.Sprite(input.Sprites.Zoey[26 + input.U.BreastSize]);
                            return;
                        }

                        output.Sprite(input.Sprites.Zoey[20 + input.U.BreastSize]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Zoey[14 + input.U.BreastSize]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.Belly, 2, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.HasBelly)
                {
                    switch (CalcBodyState(input.A))
                    {
                        case BodyState.SpinAttack:
                        case BodyState.SideBelly:
                            output.Sprite(input.Sprites.Zoey[52 + input.A.GetStomachSize(19)]);
                            return;
                        default:
                            output.Sprite(input.Sprites.Zoey[31 + input.A.GetStomachSize(19)]);
                            return;
                    }
                }
            });

            builder.RunBefore((input, output) =>
            {
                if (input.A.AnimationController.frameLists == null || Enumerable.Count(input.A.AnimationController.frameLists) == 0)
                {
                    SetUpAnimations(input.Actor);
                }

                output.ClothingShift = new Vector3(0, 0);
                switch (CalcBodyState(input.A))
                {
                    case BodyState.HighBelly:
                        if (input.A.GetStomachSize(19) == 19)
                        {
                            output.ChangeSprite(SpriteType.Head).AddOffset(0, 30 * .625f);
                            output.ChangeSprite(SpriteType.Hair).AddOffset(0, 30 * .625f);
                            output.ChangeSprite(SpriteType.Hair2).AddOffset(0, 30 * .625f);
                            output.ChangeSprite(SpriteType.Breasts).AddOffset(0, 30 * .625f);
                            output.ClothingShift = new Vector3(0, 30 * .625f);
                        }
                        else //18
                        {
                            output.ChangeSprite(SpriteType.Body).AddOffset(0, -14 * .625f);
                            output.ChangeSprite(SpriteType.Head).AddOffset(0, 16 * .625f);
                            output.ChangeSprite(SpriteType.Hair).AddOffset(0, 16 * .625f);
                            output.ChangeSprite(SpriteType.Hair2).AddOffset(0, 16 * .625f);
                            output.ChangeSprite(SpriteType.Breasts).AddOffset(0, 16 * .625f);
                            output.ClothingShift = new Vector3(0, 16 * .625f);
                        }

                        break;
                    case BodyState.SideBelly:
                        if (input.A.GetStomachSize(19) == 19)
                        {
                            output.ChangeSprite(SpriteType.Breasts).AddOffset(-5 * .625f, 12 * .625f);
                        }
                        else if (input.A.GetStomachSize(19) == 18)
                        {
                            output.ChangeSprite(SpriteType.Breasts).AddOffset(-2 * .625f, 0);
                            output.ChangeSprite(SpriteType.Body).AddOffset(0, -16 * .625f);
                            output.ChangeSprite(SpriteType.Head).AddOffset(0, -16 * .625f);
                            output.ChangeSprite(SpriteType.Hair).AddOffset(0, -16 * .625f);
                            output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, -16 * .625f);
                            output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, -16 * .625f);
                        }
                        else
                        {
                            output.ChangeSprite(SpriteType.Breasts).AddOffset(-5 * .625f, 0);
                            output.ChangeSprite(SpriteType.Body).AddOffset(0, -32 * .625f);
                            output.ChangeSprite(SpriteType.Head).AddOffset(0, -32 * .625f);
                            output.ChangeSprite(SpriteType.Hair).AddOffset(0, -32 * .625f);
                            output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, -32 * .625f);
                            output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, -32 * .625f);
                        }

                        break;
                }
            });

            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                Unit unit = data.Unit;

                unit.Name = "Zoey";
            });
        });

        internal static void SetUpAnimations(Actor_Unit actor)
        {
            actor.AnimationController.frameLists = new[]
            {
                new AnimationController.FrameList(0, 0, false)
            };
        }



        private static class ZoeyTop
        {
            internal static readonly BindableClothing<ZoeyParams> ZoeyTopInstance = ClothingBuilder.CreateV2<ZoeyParams>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = null;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.Type = 1417;
                    output.ReqWinterHoliday = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing4"].Layer(7);
                    output["Clothing4"].Coloring(Color.white);
                    output["Clothing3"].Layer(12);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing2"].Layer(12);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(11);
                    output["Clothing1"].Coloring(Color.white);
                    var state = CalcBodyState(input.A);
                    //Sweater
                    switch (state)
                    {
                        case BodyState.SpinAttack:
                            if ((input.U.BreastSize == 4) | (input.A.GetStomachSize(19) >= 4))
                            {
                                output["Clothing1"].Sprite(input.Sprites.ZoeyHoliday[6]);
                            }
                            else
                            {
                                output["Clothing1"].Sprite(input.Sprites.ZoeyHoliday[4]);
                            }

                            break;
                        case BodyState.SideBelly:
                            output["Clothing1"].Sprite(input.Sprites.ZoeyHoliday[5]);
                            break;
                        default:
                            if ((input.U.BreastSize == 4) | (input.A.GetStomachSize(19) >= 4))
                            {
                                output["Clothing1"].Sprite(input.Sprites.ZoeyHoliday[24 + (input.A.IsAttacking ? 1 : 0)]);
                            }
                            else
                            {
                                output["Clothing1"].Sprite(input.Sprites.ZoeyHoliday[22 + (input.A.IsAttacking ? 1 : 0)]);
                            }

                            break;
                    }

                    //Boobs
                    if (state == BodyState.SideBelly)
                    {
                        output["Clothing2"].Layer(4);
                    }
                    else
                    {
                        output["Clothing2"].Layer(12);
                    }

                    switch (state)
                    {
                        case BodyState.SpinAttack:
                        case BodyState.SideBelly:
                            if (input.A.PredatorComponent?.VisibleFullness > 1)
                            {
                                output["Clothing2"].Sprite(input.Sprites.ZoeyHoliday[15 + Math.Min(input.U.BreastSize, 3)]);
                            }
                            else
                            {
                                output["Clothing2"].Sprite(input.Sprites.ZoeyHoliday[11 + Math.Min(input.U.BreastSize, 3)]);
                            }

                            break;
                        default:
                            if ((input.U.BreastSize == 4) | (input.A.GetStomachSize(19) >= 4))
                            {
                                output["Clothing2"].Sprite(input.Sprites.ZoeyHoliday[24]);
                            }
                            else
                            {
                                output["Clothing2"].Sprite(input.Sprites.ZoeyHoliday[7 + Math.Min(input.U.BreastSize, 3)]);
                            }

                            break;
                    }

                    switch (state)
                    {
                        case BodyState.SpinAttack:
                            output["Clothing3"].Sprite(input.Sprites.ZoeyHoliday[20]);
                            break;
                        case BodyState.SideBelly:
                            output["Clothing3"].Sprite(input.Sprites.ZoeyHoliday[21]);
                            break;
                        default:
                            output["Clothing3"].Sprite(input.Sprites.ZoeyHoliday[19]);
                            break;
                    }

                    switch (state)
                    {
                        case BodyState.HighBelly:
                            output["Clothing4"].Sprite(input.Sprites.ZoeyHoliday[2 + (input.A.IsAttacking ? 1 : 0)]);
                            break;
                        case BodyState.SideBelly:
                            output["Clothing4"].Sprite(null);
                            break;
                        case BodyState.SpinAttack:
                            output["Clothing4"].Sprite(null);
                            break;
                        default:
                            output["Clothing4"].Sprite(input.Sprites.ZoeyHoliday[0 + (input.A.IsAttacking ? 1 : 0)]);
                            break;
                    }
                });
            });
        }
    }
}
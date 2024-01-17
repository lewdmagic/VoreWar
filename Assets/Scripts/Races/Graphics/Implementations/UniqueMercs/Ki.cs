#region

using System;
using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.UniqueMercs
{
    internal static class Ki
    {
        private const float PixelOffset = .3125f;


        internal static readonly IRaceData Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            RaceFrameList frameListTail = new RaceFrameList(new int[9] { 1, 2, 0, 3, 4, 3, 0, 2, 1 },
                new float[9] { 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f });
            RaceFrameList frameListFap = new RaceFrameList(
                new int[27] { 0, 1, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 1, 0 },
                new float[27]
                {
                    0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.3f, 0.3f, 0.3f, 0.3f, 0.2f, 0.2f, 0.15f, 0.15f, 0.15f, 0.15f,
                    0.15f, 0.15f, 0.15f, 0.15f, 0.15f, 0.2f, 0.25f, 0.3f, 0.35f, 0.4f, 0.4f
                });


            builder.Setup(output =>
            {
                output.Names("Ki", "Ki");
                output.FlavorText(new FlavorText(
                    new Texts {  },
                    new Texts {  },
                    new Texts { "small creature", "furry critter" },
                    "Jaws"
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 10,
                    StomachSize = 22,
                    HasTail = true,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.CockVore },
                    ExpMultiplier = 1.2f,
                    PowerAdjustment = 1.5f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(5, 7),
                        Dexterity = new RaceStats.StatRange(5, 7),
                        Endurance = new RaceStats.StatRange(9, 11),
                        Mind = new RaceStats.StatRange(8, 12),
                        Will = new RaceStats.StatRange(18, 22),
                        Agility = new RaceStats.StatRange(20, 24),
                        Voracity = new RaceStats.StatRange(18, 22),
                        Stomach = new RaceStats.StatRange(18, 22),
                    },
                    RacialTraits = new List<Traits>()
                    {
                        Traits.ArtfulDodge,
                        Traits.KeenReflexes,
                        Traits.StrongGullet,
                    },
                    RaceDescription = "A member of a race that uses its small size and unthreathening appearance to lure in potential prey, Ki decided that becoming a mercenary suited him fine. After all, he'd be paid for getting free meals!",
                });
                output.DickSizes = () => 1;

                output.GentleAnimation = true;
                output.CanBeGender = new List<Gender> { Gender.Male };
            });


            builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.AnimationController.frameLists == null)
                {
                    SetUpAnimations(input.Actor);
                }

                if (input.A.GetBallSize(9, 0.48f) > 0)
                {
                    if (!input.A.Targetable)
                    {
                        output.Sprite(input.Sprites.Ki[31]);
                        return;
                    }

                    if (input.A.AnimationController.frameLists[1].currentlyActive)
                    {
                        if (input.A.AnimationController.frameLists[1].currentTime >=
                            frameListFap.Times[input.A.AnimationController.frameLists[0].currentFrame])
                        {
                            input.A.AnimationController.frameLists[1].currentFrame++;
                            input.A.AnimationController.frameLists[1].currentTime = 0f;

                            if (input.A.AnimationController.frameLists[1].currentFrame >= frameListFap.Frames.Length)
                            {
                                input.A.AnimationController.frameLists[1].currentlyActive = false;
                                input.A.AnimationController.frameLists[1].currentFrame = 0;
                                input.A.AnimationController.frameLists[1].currentTime = 0f;
                            }
                        }

                        output.Sprite(input.Sprites.Ki[
                            31 + frameListFap.Frames[input.A.AnimationController.frameLists[1].currentFrame]]);
                        return;
                    }

                    if (input.A.PredatorComponent?.BallsFullness > 0 && State.Rand.Next(800) == 0)
                    {
                        input.A.AnimationController.frameLists[1].currentlyActive = true;
                    }

                    output.Sprite(input.Sprites.Ki[31]);
                    return;
                }

                int bellySize = BellySize(input.Actor);
                if (bellySize > 13)
                {
                    if (input.A.IsOralVoring)
                    {
                        output.Sprite(input.Sprites.Ki[7]);
                        return;
                    }

                    output.Sprite(input.Sprites.Ki[6]);
                    return;
                }

                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Ki[1]);
                    return;
                }

                output.Sprite(input.Sprites.Ki[0]);
            }); // Body on all poses.

            builder.RenderSingle(SpriteType.BodyAccent, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                int bellySize = BellySize(input.Actor);
                if (bellySize > 13)
                {
                    return;
                }

                if (bellySize > 9)
                {
                    output.Sprite(input.Sprites.Ki[4]);
                    return;
                }

                if (bellySize > 0)
                {
                    output.Sprite(input.Sprites.Ki[2]);
                }
            }); // Front legs on large belly sizes.

            builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.GetBallSize(9, 0.48f) > 0)
                {
                    if (!input.A.Targetable)
                    {
                        output.Sprite(input.Sprites.Ki[35]);
                        return;
                    }

                    if (input.A.AnimationController.frameLists[0].currentlyActive)
                    {
                        if (input.A.AnimationController.frameLists[0].currentTime >=
                            frameListTail.Times[input.A.AnimationController.frameLists[0].currentFrame])
                        {
                            input.A.AnimationController.frameLists[0].currentFrame++;
                            input.A.AnimationController.frameLists[0].currentTime = 0f;

                            if (input.A.AnimationController.frameLists[0].currentFrame >= frameListTail.Frames.Length)
                            {
                                input.A.AnimationController.frameLists[0].currentlyActive = false;
                                input.A.AnimationController.frameLists[0].currentFrame = 0;
                                input.A.AnimationController.frameLists[0].currentTime = 0f;
                            }
                        }

                        switch (frameListTail.Frames[input.A.AnimationController.frameLists[0].currentFrame])
                        {
                            case 0: return;
                            default:
                                output.Sprite(input.Sprites.Ki[
                                    34 + frameListTail.Frames[input.A.AnimationController.frameLists[0].currentFrame]]);
                                break;
                        }
                    }

                    if (State.Rand.Next(500) == 0)
                    {
                        input.A.AnimationController.frameLists[0].currentlyActive = true;
                    }

                    output.Sprite(input.Sprites.Ki[35]);
                    return;
                }

                int bellySize = BellySize(input.Actor);
                if (bellySize > 13)
                {
                    output.Sprite(input.Sprites.Ki[5]);
                }
            }); // Either rear body or tail, depending on pose.

            builder.RenderSingle(SpriteType.Belly, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.GetBallSize(9, 0.48f) > 0)
                {
                    return;
                }

                if (input.U.Predator == false || input.A.PredatorComponent.VisibleFullness == 0)
                {
                    return;
                }


                int bellySize = BellySize(input.Actor);

                output.Layer(bellySize > 13 ? 5 : 2);

                switch (bellySize)
                {
                    case 0:
                        output.Sprite(input.Sprites.Ki[8]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Ki[9]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Ki[10]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Ki[11]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.Ki[12]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.Ki[13]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Ki[14]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.Ki[15]);
                        return;
                    case 8:
                        output.Sprite(input.Sprites.Ki[16]);
                        return;
                    case 9:
                        output.Sprite(input.Sprites.Ki[17]);
                        return;
                    case 10:
                        output.Sprite(input.Sprites.Ki[18]);
                        return;
                    case 11:
                        output.Sprite(input.Sprites.Ki[19]);
                        return;
                    case 12:
                        output.Sprite(input.Sprites.Ki[20]);
                        return;
                    case 13:
                        output.Sprite(input.Sprites.Ki[21]);
                        return;
                    case 14:
                        output.Sprite(input.Sprites.Ki[22]);
                        return;
                    case 15:
                        output.Sprite(input.Sprites.Ki[23]);
                        return;
                    case 16:
                        output.Sprite(input.Sprites.Ki[24]);
                        return;
                    case 17:
                        output.Sprite(input.Sprites.Ki[25]);
                        return;
                    case 18:
                        output.Sprite(input.Sprites.Ki[26]);
                        return;
                    case 19:
                        output.Sprite(input.Sprites.Ki[27]);
                        return;
                    case 20:
                        output.Sprite(input.Sprites.Ki[28]);
                        return;
                    case 21:
                        output.Sprite(input.Sprites.Ki[29]);
                        return;
                    default: return;
                }
            });

            builder.RenderSingle(SpriteType.Dick, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.GetBallSize(9, 0.48f) >= 1)
                {
                    return;
                }

                if (input.A.IsCockVoring)
                {
                    output.Sprite(input.Sprites.Ki[30]);
                    return;
                }

                if (input.A.HasBelly)
                {
                    return;
                }

                if (!Config.HideCocks && (input.A.PredatorComponent?.VisibleFullness ?? 0) == 0)
                {
                    output.Sprite(input.Sprites.Ki[3]);
                }
            }); // Sheath or CV devour sprite. 

            builder.RenderSingle(SpriteType.Balls, 0, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.GetBallSize(9, 0.48f) <= 0)
                {
                    return;
                }

                if (input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) &&
                    input.A.GetBallSize(9, .48f) == 9)
                {
                    output.Sprite(input.Sprites.Ki[47]);
                    return;
                }

                switch (input.A.GetBallSize(9, 0.48f))
                {
                    case 1:
                        output.Sprite(input.Sprites.Ki[39]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Ki[40]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Ki[41]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.Ki[42]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.Ki[43]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Ki[44]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.Ki[45]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Ki[46]);
                        return;
                }
            }); // Only used with CV.


            builder.RunBefore((input, output) =>
            {
                float bodyAccent = 0;
                float bodyAccent2 = 0;
                float body = 0;

                if (input.A.GetBallSize(9, 0.48f) > 0)
                {
                    if (input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) &&
                        input.A.GetBallSize(9, .48f) == 9)
                    {
                        bodyAccent2 = 110;
                        body = 110;
                    }
                    else
                    {
                        switch (input.A.GetBallSize(9, 0.48f))
                        {
                            case 1:
                            {
                                bodyAccent2 = 0;
                                body = 0;
                                break;
                            }
                            case 2:
                            {
                                bodyAccent2 = 6;
                                body = 6;
                                break;
                            }
                            case 3:
                            {
                                bodyAccent2 = 12;
                                body = 12;
                                break;
                            }
                            case 4:
                            {
                                bodyAccent2 = 31;
                                body = 31;
                                break;
                            }
                            case 5:
                            {
                                bodyAccent2 = 59;
                                body = 59;
                                break;
                            }
                            case 6:
                            {
                                bodyAccent2 = 77;
                                body = 77;
                                break;
                            }
                            case 7:
                            {
                                bodyAccent2 = 78;
                                body = 78;
                                break;
                            }
                            default:
                            {
                                bodyAccent2 = 100;
                                body = 100;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    int bellySize = BellySize(input.Actor);
                    switch (bellySize)
                    {
                        case 2:
                        {
                            bodyAccent = 2;
                            body = 2;
                            break;
                        }
                        case 3:
                        {
                            bodyAccent = 5;
                            body = 5;
                            break;
                        }
                        case 4:
                        {
                            bodyAccent = 8;
                            body = 8;
                            break;
                        }
                        case 5:
                        {
                            bodyAccent = 12;
                            body = 12;
                            break;
                        }
                        case 6:
                        {
                            bodyAccent = 16;
                            body = 16;
                            break;
                        }
                        case 7:
                        {
                            bodyAccent = 22;
                            body = 22;
                            break;
                        }
                        case 8:
                        {
                            bodyAccent = 28;
                            body = 28;
                            break;
                        }
                        case 9:
                        {
                            bodyAccent = 35;
                            body = 35;
                            break;
                        }
                        case 10:
                        {
                            bodyAccent = 44;
                            body = 44;
                            break;
                        }
                        case 11:
                        {
                            bodyAccent = 50;
                            body = 50;
                            break;
                        }
                        case 12:
                        {
                            bodyAccent = 58;
                            body = 58;
                            break;
                        }
                        case 13:
                        {
                            bodyAccent = 70;
                            body = 70;
                            break;
                        }
                        case 14:
                        {
                            bodyAccent2 = 0;
                            bodyAccent = 0;
                            body = 0;
                            break;
                        }
                        case 15:
                        {
                            bodyAccent2 = 10;
                            bodyAccent = 10;
                            body = 10;
                            break;
                        }
                        case 16:
                        {
                            bodyAccent2 = 22;
                            bodyAccent = 22;
                            body = 22;
                            break;
                        }
                        case 17:
                        {
                            bodyAccent2 = 34;
                            bodyAccent = 34;
                            body = 34;
                            break;
                        }
                        case 18:
                        {
                            bodyAccent2 = 34;
                            bodyAccent = 34;
                            body = 34;
                            break;
                        }
                        case 19:
                        {
                            bodyAccent2 = 34;
                            bodyAccent = 34;
                            body = 34;
                            break;
                        }
                        case 20:
                        {
                            bodyAccent2 = 35;
                            bodyAccent = 35;
                            body = 35;
                            break;
                        }
                        case 21:
                        {
                            bodyAccent2 = 54;
                            bodyAccent = 54;
                            body = 54;
                            break;
                        }
                    }
                }

                output.ChangeSprite(SpriteType.Body).AddOffset(0, body * PixelOffset);
                output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, bodyAccent * PixelOffset);
                output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, bodyAccent2 * PixelOffset);
            });

            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                Unit unit = data.Unit;

                unit.Name = "Ki";
                unit.PreferredVoreType = VoreType.Oral;
            });
        });

        private static void SetUpAnimations(Actor_Unit actor)
        {
            actor.AnimationController.frameLists = new[]
            {
                new AnimationController.FrameList(0, 0, false), // Tail controller. Index 0.
                new AnimationController.FrameList(0, 0, false)
            }; // Fap controller. Index 1.
        }

        private static int BellySize(Actor_Unit actor)
        {
            if (actor.Unit.Predator == false)
            {
                return 0;
            }

            int bellySize = actor.GetStomachSize(21, 0.66f);

            if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true))
            {
            }
            else if (actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false))
            {
                bellySize = Math.Min(bellySize, 20);
            }
            else
            {
                bellySize = Math.Min(bellySize, 17);
            }

            return bellySize;
        }
    }
}
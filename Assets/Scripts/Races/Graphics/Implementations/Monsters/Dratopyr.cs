#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class Dratopyr
    {
        internal static RaceFrameList FrameListShake = new RaceFrameList(new int[5] { 0, 1, 0, 2, 0 }, new float[5] { 0.6f, 0.6f, 0.6f, 0.6f, 0.6f });

        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            RaceFrameList frameListTail = new RaceFrameList(new int[8] { 2, 1, 0, 1, 2, 3, 4, 3 }, new float[8] { 0.55f, 0.55f, 0.75f, 0.55f, 0.55f, 0.55f, 0.75f, 0.55f });
            RaceFrameList frameListEyes = new RaceFrameList(new int[3] { 1, 2, 1 }, new float[3] { 0.3f, 0.3f, 0.3f });
            RaceFrameList frameListWings = new RaceFrameList(new int[4] { 0, 1, 2, 1 }, new float[4] { 0.3f, 0.3f, 0.3f, 0.3f });
            RaceFrameList frameListEars = new RaceFrameList(new int[18] { 0, 1, 2, 1, 0, 1, 0, 1, 2, 1, 2, 1, 0, 1, 2, 1, 2, 1 }, new float[18] { 2.2f, 0.3f, 0.5f, 0.2f, 0.8f, 0.3f, 1.5f, 0.9f, 1.3f, 0.6f, 0.4f, 0.3f, 2.2f, 1.5f, 0.6f, 0.3f, 0.8f, 0.2f });


            builder.Setup(output =>
            {
                output.Names("Dratopyr", "Dratopyrs");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 9,
                    StomachSize = 15,
                    HasTail = true,
                    FavoredStat = Stat.Agility,
                    AllowedVoreTypes = new List<VoreType> { VoreType.CockVore, VoreType.Oral, VoreType.Unbirth },
                    ExpMultiplier = .95f,
                    PowerAdjustment = .95f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(8, 12),
                        Dexterity = new RaceStats.StatRange(6, 8),
                        Endurance = new RaceStats.StatRange(8, 12),
                        Mind = new RaceStats.StatRange(7, 9),
                        Will = new RaceStats.StatRange(10, 15),
                        Agility = new RaceStats.StatRange(9, 17),
                        Voracity = new RaceStats.StatRange(8, 14),
                        Stomach = new RaceStats.StatRange(8, 16),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.ArtfulDodge,
                        TraitType.Flight,
                        TraitType.Cruel
                    },
                    RaceDescription = "With an appearance reminiscent of a reptilian bat, the Dratopyr are likely a hybrid race. Smaller than most monsters but just as fierce, the Dratopyr specialize in weakening their prey while avoiding attempts to fend them off. Dratopyr are very fast breeders and would thus be a major threat to everyone, were it not for their tendency toward cannibalism.",
                });
                output.BreastSizes = () => 2;
                output.DickSizes = () => 2;

                output.SkinColors = ColorMap.DratopyrMainColorCount; // Majority of the unit
                output.AccessoryColors = ColorMap.DratopyrWingColorCount; // Wing Webbing
                output.ExtraColors1 = ColorMap.DratopyrFleshColorCount; // Flesh
                output.ExtraColors2 = ColorMap.DratopyrEyeColorCount; // Eye "Whites"
                output.EyeColors = ColorMap.EyeColorCount; // Eyes
                output.GentleAnimation = true;

                output.WeightGainDisabled = true;

                output.CanBeGender = new List<Gender> { Gender.Male, Gender.Female };
            });


            builder.RenderSingle(SpriteType.Head, 10, (input, output) =>
            {
                output.Coloring(ColorMap.GetDratopyrMainColor(input.U.SkinColor));
                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Dratopyr[5]);
                    return;
                }

                if (input.A.GetBallSize(22) > 0)
                {
                    output.Sprite(input.Sprites.Dratopyr[1]);
                    return;
                }

                output.Sprite(input.Sprites.Dratopyr[0]);
            }); // Head

            builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
            {
                output.Coloring(ColorMap.GetEyeColor(input.U.EyeColor));
                output.Sprite(input.Sprites.Dratopyr[2]);
            }); // Eye Iris
            builder.RenderSingle(SpriteType.Mouth, 12, (input, output) =>
            {
                output.Coloring(ColorMap.GetDratopyrFleshColor(input.U.ExtraColor1));
                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Dratopyr[6]);
                }
            }); // Inner Mouth

            builder.RenderSingle(SpriteType.Hair, 11, (input, output) =>
            {
                output.Coloring(ColorMap.GetDratopyrMainColor(input.U.SkinColor));
                if (input.A.IsOralVoring)
                {
                    return;
                }

                if (!input.A.Targetable)
                {
                    output.Sprite(input.Sprites.Dratopyr[14]);
                    return;
                }

                if (input.A.AnimationController.frameLists[4].currentTime >= frameListEars.Times[input.A.AnimationController.frameLists[4].currentFrame])
                {
                    input.A.AnimationController.frameLists[4].currentFrame++;
                    input.A.AnimationController.frameLists[4].currentTime = 0f;

                    if (input.A.AnimationController.frameLists[4].currentFrame >= frameListEars.Frames.Length)
                    {
                        input.A.AnimationController.frameLists[4].currentFrame = 0;
                        input.A.AnimationController.frameLists[4].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Dratopyr[14 + frameListEars.Frames[input.A.AnimationController.frameLists[4].currentFrame]]);
            }); // Ears

            builder.RenderSingle(SpriteType.Body, 9, (input, output) =>
            {
                output.Coloring(ColorMap.GetDratopyrMainColor(input.U.SkinColor));
                if (input.A.AnimationController.frameLists == null)
                {
                    SetUpAnimations(input.Actor);
                }

                if (input.A.IsAttacking || input.A.IsCockVoring || input.A.IsUnbirthing)
                {
                    output.Sprite(input.Sprites.Dratopyr[22]);
                    return;
                }

                output.Sprite(input.Sprites.Dratopyr[21]);
            }); // Upper Body & Arms

            builder.RenderSingle(SpriteType.BodyAccent, 0, (input, output) =>
            {
                output.Coloring(ColorMap.GetDratopyrMainColor(input.U.SkinColor));
                if (!input.A.Targetable)
                {
                    output.Sprite(input.Sprites.Dratopyr[28]);
                    return;
                }

                if (input.A.AnimationController.frameLists[0].currentlyActive)
                {
                    if (input.A.AnimationController.frameLists[0].currentTime >= frameListTail.Times[input.A.AnimationController.frameLists[0].currentFrame])
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

                    output.Sprite(input.Sprites.Dratopyr[26 + frameListTail.Frames[input.A.AnimationController.frameLists[0].currentFrame]]);
                    return;
                }

                if (State.Rand.Next(250) == 0)
                {
                    input.A.AnimationController.frameLists[0].currentlyActive = true;
                }

                output.Sprite(input.Sprites.Dratopyr[28]);
            }); // Tail

            builder.RenderSingle(SpriteType.BodyAccent2, 3, (input, output) =>
            {
                output.Coloring(ColorMap.GetDratopyrMainColor(input.U.SkinColor));
                if (!input.A.Targetable)
                {
                    output.Sprite(input.Sprites.Dratopyr[17]);
                    return;
                }

                if (input.A.IsAttacking || input.A.IsOralVoring || input.A.IsCockVoring || input.A.IsUnbirthing)
                {
                    input.A.AnimationController.frameLists[2].currentlyActive = false;
                    input.A.AnimationController.frameLists[2].currentFrame = 0;
                    input.A.AnimationController.frameLists[2].currentTime = 0f;

                    output.Sprite(input.Sprites.Dratopyr[20]);
                    return;
                }

                if (input.A.AnimationController.frameLists[2].currentlyActive)
                {
                    if (input.A.AnimationController.frameLists[2].currentTime >= FrameListShake.Times[input.A.AnimationController.frameLists[2].currentFrame])
                    {
                        input.A.AnimationController.frameLists[2].currentFrame++;
                        input.A.AnimationController.frameLists[2].currentTime = 0f;

                        if (input.A.AnimationController.frameLists[2].currentFrame >= FrameListShake.Frames.Length)
                        {
                            input.A.AnimationController.frameLists[2].currentlyActive = false;
                            input.A.AnimationController.frameLists[2].currentFrame = 0;
                            input.A.AnimationController.frameLists[2].currentTime = 0f;
                        }
                    }

                    output.Sprite(input.Sprites.Dratopyr[17 + FrameListShake.Frames[input.A.AnimationController.frameLists[2].currentFrame]]);
                    return;
                }

                if (State.Rand.Next(350) == 0)
                {
                    input.A.AnimationController.frameLists[2].currentlyActive = true;
                }

                output.Sprite(input.Sprites.Dratopyr[17]);
            }); // Legs

            builder.RenderSingle(SpriteType.BodyAccent3, 1, (input, output) =>
            {
                output.Coloring(ColorMap.GetDratopyrWingColor(input.U.ExtraColor1));
                if (!input.A.Targetable)
                {
                    output.Sprite(input.Sprites.Dratopyr[8]);
                    return;
                }

                if (input.A.AnimationController.frameLists[3].currentTime >= frameListWings.Times[input.A.AnimationController.frameLists[3].currentFrame])
                {
                    input.A.AnimationController.frameLists[3].currentFrame++;
                    input.A.AnimationController.frameLists[3].currentTime = 0f;

                    if (input.A.AnimationController.frameLists[3].currentFrame >= frameListWings.Frames.Length)
                    {
                        input.A.AnimationController.frameLists[3].currentFrame = 0;
                        input.A.AnimationController.frameLists[3].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Dratopyr[8 + frameListWings.Frames[input.A.AnimationController.frameLists[3].currentFrame]]);
            }); // Wing Webbing

            builder.RenderSingle(SpriteType.BodyAccent4, 2, (input, output) =>
            {
                output.Coloring(ColorMap.GetDratopyrMainColor(input.U.SkinColor));
                if (!input.A.Targetable)
                {
                    output.Sprite(input.Sprites.Dratopyr[11]);
                    return;
                }

                output.Sprite(input.Sprites.Dratopyr[11 + frameListWings.Frames[input.A.AnimationController.frameLists[3].currentFrame]]);
            }); // Wing Bones

            builder.RenderSingle(SpriteType.BodyAccent5, 7, (input, output) =>
            {
                output.Coloring(ColorMap.GetDratopyrEyeColor(input.U.ExtraColor2));
                output.Sprite(input.Sprites.Dratopyr[25]);
            }); // Eye Whites

            builder.RenderSingle(SpriteType.BodyAccent6, 9, (input, output) =>
            {
                output.Coloring(ColorMap.GetDratopyrMainColor(input.U.SkinColor));
                if (!input.A.Targetable)
                {
                    output.Sprite(input.Sprites.Dratopyr[4]);
                    return;
                }

                if (input.A.AnimationController.frameLists[1].currentlyActive)
                {
                    if (input.A.AnimationController.frameLists[1].currentTime >= frameListEyes.Times[input.A.AnimationController.frameLists[1].currentFrame])
                    {
                        input.A.AnimationController.frameLists[1].currentFrame++;
                        input.A.AnimationController.frameLists[1].currentTime = 0f;

                        if (input.A.AnimationController.frameLists[1].currentFrame >= frameListEyes.Frames.Length)
                        {
                            input.A.AnimationController.frameLists[1].currentlyActive = false;
                            input.A.AnimationController.frameLists[1].currentFrame = 0;
                            input.A.AnimationController.frameLists[1].currentTime = 0f;
                        }
                    }

                    if (frameListEyes.Frames[input.A.AnimationController.frameLists[1].currentFrame] == 0)
                    {
                        return;
                    }

                    output.Sprite(input.Sprites.Dratopyr[2 + frameListEyes.Frames[input.A.AnimationController.frameLists[1].currentFrame]]);
                    return;
                }

                if (State.Rand.Next(400) == 0)
                {
                    input.A.AnimationController.frameLists[1].currentlyActive = true;
                }
            }); // Eyelids

            builder.RenderSingle(SpriteType.BodyAccent7, 13, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Dratopyr[7]);
                }
            }); // Teeth

            builder.RenderSingle(SpriteType.BodyAccent8, 5, (input, output) =>
            {
                output.Coloring(ColorMap.GetDratopyrMainColor(input.U.SkinColor));
                if (input.U.DickSize >= 0)
                {
                    if (input.A.GetStomachSize(23, 0.7f) > 14)
                    {
                        if (!input.A.Targetable)
                        {
                            output.Sprite(input.Sprites.Dratopyr[34]);
                            return;
                        }

                        output.Sprite(input.Sprites.Dratopyr[34 + FrameListShake.Frames[input.A.AnimationController.frameLists[2].currentFrame]]);
                        return;
                    }

                    if (!input.A.Targetable)
                    {
                        output.Sprite(input.Sprites.Dratopyr[31]);
                        return;
                    }

                    output.Sprite(input.Sprites.Dratopyr[31 + FrameListShake.Frames[input.A.AnimationController.frameLists[2].currentFrame]]);
                }
            }); // Sheath

            builder.RenderSingle(SpriteType.Belly, 7, (input, output) =>
            {
                output.Coloring(ColorMap.GetDratopyrMainColor(input.U.SkinColor));
                int bellySize = input.A.GetStomachSize(23, 0.7f);
                int shake = FrameListShake.Frames[input.A.AnimationController.frameLists[2].currentFrame];

                if (!input.A.Targetable)
                {
                    shake = 0;
                }

                if (bellySize > 18)
                {
                    bellySize = 18;
                }

                output.Sprite(input.Sprites.Dratopyr[102 + bellySize * 3 + shake]);
            }); // Belly

            builder.RenderSingle(SpriteType.Dick, 10, (input, output) =>
            {
                output.Coloring(ColorMap.GetDratopyrFleshColor(input.U.ExtraColor1));
                if (input.U.DickSize >= 0)
                {
                    if (input.A.GetStomachSize(23, 0.7f) > 1)
                    {
                        output.Layer(6);

                        if (input.A.IsCockVoring)
                        {
                            output.Sprite(input.Sprites.Dratopyr[44]);
                            return;
                        }

                        if (input.A.IsErect())
                        {
                            if (!input.A.Targetable)
                            {
                                output.Sprite(input.Sprites.Dratopyr[41]);
                                return;
                            }

                            if (input.A.AnimationController.frameLists[2].currentlyActive)
                            {
                                output.Sprite(input.Sprites.Dratopyr[41 + FrameListShake.Frames[input.A.AnimationController.frameLists[2].currentFrame]]);
                                return;
                            }

                            output.Sprite(input.Sprites.Dratopyr[41]);
                            return;
                        }
                    }
                    else
                    {
                        output.Layer(10);

                        if (input.A.IsCockVoring)
                        {
                            output.Sprite(input.Sprites.Dratopyr[40]);
                            return;
                        }

                        if (input.A.IsErect())
                        {
                            if (!input.A.Targetable)
                            {
                                output.Sprite(input.Sprites.Dratopyr[37]);
                                return;
                            }

                            if (input.A.AnimationController.frameLists[2].currentlyActive)
                            {
                                output.Sprite(input.Sprites.Dratopyr[37 + FrameListShake.Frames[input.A.AnimationController.frameLists[2].currentFrame]]);
                                return;
                            }

                            output.Sprite(input.Sprites.Dratopyr[37]);
                            return;
                        }
                    }
                }

                if (input.U.DickSize == -1)
                {
                    if (input.A.IsUnbirthing)
                    {
                        output.Sprite(input.Sprites.Dratopyr[172]).Layer(6);
                        return;
                    }

                    output.Sprite(input.Sprites.Dratopyr[171]).Layer(6);
                }
            }); // Dick

            builder.RenderSingle(SpriteType.Balls, 4, (input, output) =>
            {
                output.Coloring(ColorMap.GetDratopyrMainColor(input.U.SkinColor));
                if (input.U.DickSize == -1)
                {
                    return;
                }

                if (Config.HideCocks)
                {
                    return;
                }

                int shake = FrameListShake.Frames[input.A.AnimationController.frameLists[2].currentFrame];
                int ballSize = input.A.GetBallSize(21, 0.6f);

                if (!input.A.Targetable)
                {
                    shake = 0;
                }

                if (ballSize > 13)
                {
                    ballSize = 13;
                }

                output.Sprite(input.Sprites.Dratopyr[45 + ballSize * 3 + shake]);
            }); // Balls


            builder.RunBefore((input, output) =>
            {
                output.ChangeSprite(SpriteType.Balls).AddOffset(0, -80 * .625f);
                output.ChangeSprite(SpriteType.Belly).AddOffset(0, -80 * .625f);
            });

            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                IUnitRead unit = data.Unit;


                int rand = State.Rand.Next(100);

                if (rand < 92)
                {
                    unit.ExtraColor2 = 0;
                }
                else if (rand < 97)
                {
                    unit.ExtraColor2 = 1;
                }
                else
                {
                    unit.ExtraColor2 = 2;
                }
            });
        });

        private static void SetUpAnimations(IActorUnit actor)
        {
            actor.AnimationController.frameLists = new[]
            {
                new AnimationController.FrameList(0, 0, false), // Tail controller. Index 0.
                new AnimationController.FrameList(0, 0, false), // Eye controller. Index 1.
                new AnimationController.FrameList(0, 0, false), // Shimmyshake controller. Index 2.
                new AnimationController.FrameList(State.Rand.Next(0, 3), 0, true), // Wing controller. Index 3.
                new AnimationController.FrameList(State.Rand.Next(0, 17), 0, true)
            }; // Ear controller. Index 4.
        }
    }
}
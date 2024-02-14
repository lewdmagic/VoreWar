#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class Raptor
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            RaceFrameList frameListTail = new RaceFrameList(new int[24] { 0, 4, 5, 6, 5, 4, 0, 3, 2, 1, 2, 3, 0, 4, 5, 6, 5, 4, 0, 3, 2, 1, 2, 3 }, new float[24] { 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f, 0.8f, 0.5f, 0.5f });


            builder.Setup(output =>
            {
                output.Names("Raptor", "Raptors");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 5,
                    StomachSize = 12,
                    HasTail = true,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.CockVore },
                    ExpMultiplier = .85f,
                    PowerAdjustment = .75f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(3, 7),
                        Dexterity = new RaceStats.StatRange(3, 5),
                        Endurance = new RaceStats.StatRange(6, 10),
                        Mind = new RaceStats.StatRange(5, 8),
                        Will = new RaceStats.StatRange(5, 8),
                        Agility = new RaceStats.StatRange(8, 16),
                        Voracity = new RaceStats.StatRange(6, 14),
                        Stomach = new RaceStats.StatRange(8, 20),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.ArtfulDodge,
                        TraitType.Pounce,
                        TraitType.SlowDigestion
                    },
                    RaceDescription = "Bigger cousins of the Compy, these rarer creatures often appear in smaller numbers among their lesser kin. While still relatively harmless compared to most monsters, the Raptors are at the edge of being a real danger to unprepared travelers, not least because they are at times known to be clever.",
                });
                output.GentleAnimation = true;
                output.CanBeGender = new List<Gender> { Gender.Male };
                output.SkinColors = ColorMap.LizardColorCount;
                output.ExtraColors1 = ColorMap.LizardColorCount;
            });


            builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
            {
                output.Coloring(ColorMap.GetLizardColor(input.U.ExtraColor1));
                output.Sprite(input.Sprites.Raptor[5]);
            });

            builder.RenderSingle(SpriteType.Mouth, 9, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking || input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Raptor[7]);
                    return;
                }

                if (input.A.GetBallSize(16) > 0)
                {
                    output.Sprite(input.Sprites.Raptor[9]);
                    return;
                }

                if (input.A.HasBelly)
                {
                    output.Sprite(input.Sprites.Raptor[8]);
                    return;
                }

                output.Sprite(input.Sprites.Raptor[6]);
            });

            builder.RenderSingle(SpriteType.Body, 8, (input, output) =>
            {
                output.Coloring(ColorMap.GetLizardColor(input.U.SkinColor));
                if (input.A.AnimationController.frameLists == null)
                {
                    SetUpAnimations(input.Actor);
                }

                if (input.A.IsAttacking || input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Raptor[1]);
                    return;
                }

                if (input.A.GetBallSize(16) > 0)
                {
                    output.Sprite(input.Sprites.Raptor[3]);
                    return;
                }

                if (input.A.HasBelly)
                {
                    output.Sprite(input.Sprites.Raptor[2]);
                    return;
                }

                output.Sprite(input.Sprites.Raptor[0]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
            {
                output.Coloring(ColorMap.GetLizardColor(input.U.SkinColor));
                if (input.A.IsAttacking || input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Raptor[48]);
                    return;
                }

                output.Sprite(input.Sprites.Raptor[4]);
            }); // Legs

            builder.RenderSingle(SpriteType.BodyAccent2, 10, (input, output) =>
            {
                output.Coloring(ColorMap.GetLizardColor(input.U.ExtraColor1));
                if (input.A.IsAttacking || input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Raptor[49]);
                    return;
                }

                output.Sprite(input.Sprites.Raptor[10]);
            }); // Body Stripes

            builder.RenderSingle(SpriteType.BodyAccent3, 4, (input, output) =>
            {
                output.Coloring(ColorMap.GetLizardColor(input.U.ExtraColor1));
                output.Sprite(input.Sprites.Raptor[11]);
            }); // Leg Stripes

            builder.RenderSingle(SpriteType.BodyAccent4, 1, (input, output) =>
            {
                output.Coloring(ColorMap.GetLizardColor(input.U.SkinColor));
                if (!input.A.Targetable)
                {
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

                    if (frameListTail.Frames[input.A.AnimationController.frameLists[0].currentFrame] == 0)
                    {
                        return;
                    }

                    output.Sprite(input.Sprites.Raptor[11 + frameListTail.Frames[input.A.AnimationController.frameLists[0].currentFrame]]);
                    return;
                }

                if (input.A.HasBelly || input.A.GetBallSize(18) > 0)
                {
                    if (State.Rand.Next(300) == 0)
                    {
                        input.A.AnimationController.frameLists[0].currentlyActive = true;
                    }
                }

                else if (State.Rand.Next(1200) == 0)
                {
                    input.A.AnimationController.frameLists[0].currentlyActive = true;
                }
            }); // Tail

            builder.RenderSingle(SpriteType.BodyAccent5, 2, (input, output) =>
            {
                output.Coloring(ColorMap.GetLizardColor(input.U.ExtraColor1));
                if (!input.A.Targetable)
                {
                    return;
                }

                if (input.A.AnimationController.frameLists[0].currentlyActive)
                {
                    if (frameListTail.Frames[input.A.AnimationController.frameLists[0].currentFrame] == 0)
                    {
                        return;
                    }

                    output.Sprite(input.Sprites.Raptor[17 + frameListTail.Frames[input.A.AnimationController.frameLists[0].currentFrame]]);
                }
            }); // Tail Stripes

            builder.RenderSingle(SpriteType.BodyAccent6, 0, (input, output) =>
            {
                output.Coloring(ColorMap.GetLizardColor(input.U.SkinColor));
                if (input.A.GetBallSize(24, 2) == 0 && Config.HideCocks == false)
                {
                    output.Sprite(input.Sprites.Raptor[52]);
                    return;
                }

                int size = input.A.GetBallSize(24, 2);

                if (size > 21)
                {
                    size = 21;
                }

                output.Sprite(input.Sprites.Raptor[51 + size]);
            }); // Balls

            builder.RenderSingle(SpriteType.Belly, 7, (input, output) =>
            {
                output.Coloring(ColorMap.GetLizardColor(input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    return;
                }

                int size = input.A.GetStomachSize(24, 2);

                if (size > 21)
                {
                    size = 21;
                }

                output.Sprite(input.Sprites.Raptor[23 + size]);
            });

            builder.RenderSingle(SpriteType.Dick, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsCockVoring)
                {
                    output.Sprite(input.Sprites.Raptor[51]);
                    return;
                }

                if (input.A.IsErect())
                {
                    output.Sprite(input.Sprites.Raptor[50]);
                }
            });


            builder.RunBefore((input, output) =>
            {
                if (input.A.HasBelly == false)
                {
                    output.ChangeSprite(SpriteType.Dick).AddOffset(0, 0 * .3125f);
                    output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 0 * .3125f);
                    output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 0 * .3125f);
                    output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 0 * .3125f);
                    output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 0 * .3125f);
                    output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 0 * .3125f);
                    output.ChangeSprite(SpriteType.Body).AddOffset(0, 0 * .3125f);
                    output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 0 * .3125f);
                    output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 0 * .3125f);
                }

                else
                {
                    int size = input.A.GetStomachSize(24, 2);

                    if (size == 24 && (input.A.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach) ?? false))
                    {
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, 176 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 176 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 176 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 176 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 176 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 176 * .3125f);
                        output.ChangeSprite(SpriteType.Body).AddOffset(0, 176 * .3125f);
                        output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 176 * .3125f);
                        output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 176 * .3125f);
                    }

                    else if (size >= 23 && (input.A.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
                    {
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, 168 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 168 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 168 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 168 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 168 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 168 * .3125f);
                        output.ChangeSprite(SpriteType.Body).AddOffset(0, 168 * .3125f);
                        output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 168 * .3125f);
                        output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 168 * .3125f);
                    }

                    else if (size == 22 && (input.A.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach) ?? false))
                    {
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, 152 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 152 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 152 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 152 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 152 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 152 * .3125f);
                        output.ChangeSprite(SpriteType.Body).AddOffset(0, 152 * .3125f);
                        output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 152 * .3125f);
                        output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 152 * .3125f);
                    }

                    else if (size >= 21)
                    {
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, 144 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 144 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 144 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 144 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 144 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 144 * .3125f);
                        output.ChangeSprite(SpriteType.Body).AddOffset(0, 144 * .3125f);
                        output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 144 * .3125f);
                        output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 144 * .3125f);
                    }

                    else if (size == 20)
                    {
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, 128 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 128 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 128 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 128 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 128 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 128 * .3125f);
                        output.ChangeSprite(SpriteType.Body).AddOffset(0, 128 * .3125f);
                        output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 128 * .3125f);
                        output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 128 * .3125f);
                    }

                    else if (size == 19)
                    {
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, 112 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 112 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 112 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 112 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 112 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 112 * .3125f);
                        output.ChangeSprite(SpriteType.Body).AddOffset(0, 112 * .3125f);
                        output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 112 * .3125f);
                        output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 112 * .3125f);
                    }

                    else if (size == 18)
                    {
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, 96 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 96 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 96 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 96 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 96 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 96 * .3125f);
                        output.ChangeSprite(SpriteType.Body).AddOffset(0, 96 * .3125f);
                        output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 96 * .3125f);
                        output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 96 * .3125f);
                    }

                    else if (size == 17)
                    {
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, 80 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 80 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 80 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 80 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 80 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 80 * .3125f);
                        output.ChangeSprite(SpriteType.Body).AddOffset(0, 80 * .3125f);
                        output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 80 * .3125f);
                        output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 80 * .3125f);
                    }

                    else if (size == 16)
                    {
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, 64 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 64 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 64 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 64 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 64 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 64 * .3125f);
                        output.ChangeSprite(SpriteType.Body).AddOffset(0, 64 * .3125f);
                        output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 64 * .3125f);
                        output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 64 * .3125f);
                    }

                    else if (size == 15)
                    {
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, 48 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 48 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 48 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 48 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 48 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 48 * .3125f);
                        output.ChangeSprite(SpriteType.Body).AddOffset(0, 48 * .3125f);
                        output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 48 * .3125f);
                        output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 48 * .3125f);
                    }

                    else if (size == 14)
                    {
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, 32 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 32 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 32 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 32 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 32 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 32 * .3125f);
                        output.ChangeSprite(SpriteType.Body).AddOffset(0, 32 * .3125f);
                        output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 32 * .3125f);
                        output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 32 * .3125f);
                    }

                    else if (size == 13)
                    {
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, 24 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 24 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 24 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 24 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 24 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 24 * .3125f);
                        output.ChangeSprite(SpriteType.Body).AddOffset(0, 24 * .3125f);
                        output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 24 * .3125f);
                        output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 24 * .3125f);
                    }

                    else if (size == 12)
                    {
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, 16 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 16 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 16 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 16 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 16 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 16 * .3125f);
                        output.ChangeSprite(SpriteType.Body).AddOffset(0, 16 * .3125f);
                        output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 16 * .3125f);
                        output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 16 * .3125f);
                    }

                    else if (size == 11)
                    {
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, 8 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 8 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 8 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 8 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 8 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 8 * .3125f);
                        output.ChangeSprite(SpriteType.Body).AddOffset(0, 8 * .3125f);
                        output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 8 * .3125f);
                        output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 8 * .3125f);
                    }

                    else
                    {
                        output.ChangeSprite(SpriteType.Dick).AddOffset(0, 0 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 0 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 0 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 0 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(0, 0 * .3125f);
                        output.ChangeSprite(SpriteType.BodyAccent5).AddOffset(0, 0 * .3125f);
                        output.ChangeSprite(SpriteType.Body).AddOffset(0, 0 * .3125f);
                        output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 0 * .3125f);
                        output.ChangeSprite(SpriteType.Mouth).AddOffset(0, 0 * .3125f);
                    }
                }
            });

            builder.RandomCustom(data =>
            {
                IUnitRead unit = data.Unit;

                unit.SkinColor = State.Rand.Next(data.SetupOutput.SkinColors);
                unit.ExtraColor1 = State.Rand.Next(data.SetupOutput.ExtraColors1);
                unit.DickSize = 1;
                unit.DefaultBreastSize = -1;
            });
        });

        private static void SetUpAnimations(IActorUnit actor)
        {
            actor.AnimationController.frameLists = new[]
            {
                new AnimationController.FrameList(0, 0, false) // Tail controller
            };
        }
    }
}
﻿#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class Compy
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            RaceFrameList frameListTail = new RaceFrameList(new[] { 2, 1, 0, 1, 2, 3, 4, 3 }, new[] { 0.5f, 0.4f, 0.8f, 0.4f, 0.4f, 0.4f, 0.8f, 0.4f });

            builder.Setup((input, output) =>
            {
                output.Names("Compy", "Compy");

                output.BonesInfo((unit) => new List<BoneInfo>()
                {
                    new BoneInfo(BoneType.Compy, unit.Name)
                });
                output.FlavorText(new FlavorText(
                    new Texts { "tiny", "chirping", "overambitious" },
                    new Texts { "energetic", "tanuki shaming", "ambitious" },
                    new Texts { "compy", "compsognathus", "dinosaur", "tiny dino" }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 3,
                    StomachSize = 13,
                    HasTail = false,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.CockVore },
                    ExpMultiplier = .75f,
                    PowerAdjustment = .5f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new StatRange(2, 6),
                        Dexterity = new StatRange(2, 4),
                        Endurance = new StatRange(6, 10),
                        Mind = new StatRange(2, 6),
                        Will = new StatRange(4, 8),
                        Agility = new StatRange(8, 18),
                        Voracity = new StatRange(6, 14),
                        Stomach = new StatRange(8, 20),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.ArtfulDodge,
                    },
                    RaceDescription = "No-one is certain where these tiny beings appeared from, but everyone agrees that they aren't much of a threat, though not for a lack of trying from their part. All travelers should be aware though, a small dinosaur humping your leg likely means there are more nearby.",
                });
                output.IndividualNames(new List<string>
                {
                    "Chicky",
                    "Pippi",
                    "Peep",
                    "Smiley",
                    "Micky",
                    "Dingy",
                    "Dongy",
                    "Wonky",
                    "Wanky",
                    "Ashby",
                    "Pop",
                    "Pin",
                    "Sticky",
                    "Smelly",
                    "Stinky",
                    "Chirp",
                    "Chip",
                    "Smore",
                    "Burpy",
                    "Scummy",
                    "Donny",
                    "Whin",
                    "Musti",
                    "Reksi",
                    "Rexy",
                    "Snack",
                    "Spot",
                    "Rip",
                    "Poopy",
                });
                output.CanBeGender = new List<Gender> { Gender.Male };
                output.SkinColors = ColorMap.WyvernColorCount;
            });


            builder.RenderSingle(SpriteType.Body, 3, (input, output) =>
            {
                output.Coloring(ColorMap.GetWyvernColor(input.U.SkinColor));
                if (input.A.AnimationController.FrameLists == null)
                {
                    SetUpAnimations(input.Actor);
                }

                if (input.A.IsAttacking || input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Compy[1]);
                    return;
                }

                output.Sprite(input.Sprites.Compy[0]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
            {
                output.Coloring(ColorMap.GetWyvernColor(input.U.SkinColor));
                if (!input.A.Targetable)
                {
                    output.Sprite(input.Sprites.Compy[33]);
                    return;
                }

                if (input.A.AnimationController.FrameLists[0].CurrentlyActive)
                {
                    if (input.A.AnimationController.FrameLists[0].CurrentTime >= frameListTail.Times[input.A.AnimationController.FrameLists[0].CurrentFrame])
                    {
                        input.A.AnimationController.FrameLists[0].CurrentFrame++;
                        input.A.AnimationController.FrameLists[0].CurrentTime = 0f;

                        if (input.A.AnimationController.FrameLists[0].CurrentFrame >= frameListTail.Frames.Length)
                        {
                            input.A.AnimationController.FrameLists[0].CurrentlyActive = false;
                            input.A.AnimationController.FrameLists[0].CurrentFrame = 0;
                            input.A.AnimationController.FrameLists[0].CurrentTime = 0f;
                        }
                    }

                    output.Sprite(input.Sprites.Compy[31 + frameListTail.Frames[input.A.AnimationController.FrameLists[0].CurrentFrame]]);
                    return;
                }

                if (input.A.GetBallSize(30) > 0)
                {
                    if (State.Rand.Next(300) == 0)
                    {
                        input.A.AnimationController.FrameLists[0].CurrentlyActive = true;
                    }
                }

                else if (State.Rand.Next(1500) == 0)
                {
                    input.A.AnimationController.FrameLists[0].CurrentlyActive = true;
                }

                output.Sprite(input.Sprites.Compy[33]);
            }); // Tail

            builder.RenderSingle(SpriteType.Dick, 2, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsCockVoring)
                {
                    output.Sprite(input.Sprites.Compy[3]);
                    return;
                }

                if (input.A.GetBallSize(30) > 0)
                {
                    output.Sprite(input.Sprites.Compy[2]);
                }
            });

            builder.RenderSingle(SpriteType.Balls, 0, (input, output) =>
            {
                output.Coloring(ColorMap.GetWyvernColor(input.U.SkinColor));
                if (input.A.GetBallSize(30) == 0)
                {
                    output.Sprite(input.Sprites.Compy[4]);
                    return;
                }

                int size = input.A.GetBallSize(30);

                if (size >= 21)
                {
                    size = 21;
                }

                output.Sprite(input.Sprites.Compy[3 + size]);
            });

            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom((data, output) =>   
            {
                IUnitRead unit = data.Unit;

                unit.SkinColor = State.Rand.Next(data.SetupOutput.SkinColors);
                unit.DickSize = 1;
                unit.DefaultBreastSize = -1;
            });
        });

        private static void SetUpAnimations(IActorUnit actor)
        {
            actor.AnimationController.FrameLists = new[]
            {
                new AnimationController.FrameList(0, 0, false) // Tail controller
            };
        }
    }
}
#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class Harvesters
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            RaceFrameList frameListEyes = new RaceFrameList(new int[5] { 0, 1, 2, 1, 0 }, new float[5] { .2f, .2f, .3f, .2f, .2f });
            RaceFrameList frameListArms = new RaceFrameList(new int[5] { 0, 1, 2, 1, 0 }, new float[5] { .2f, .5f, 1.5f, .5f, .2f });
            RaceFrameList frameListDick = new RaceFrameList(new int[6] { 0, 1, 0, 1, 0, 1 }, new float[6] { .2f, .2f, .2f, .2f, .3f, .4f });
            RaceFrameList frameListTongue = new RaceFrameList(new int[13] { 0, 1, 2, 3, 4, 2, 3, 4, 2, 3, 4, 1, 0 }, new float[13] { .2f, .2f, .2f, .3f, .2f, .3f, .2f, .2f, .2f, .2f, .3f, .3f, .3f });


            builder.Setup(output =>
            {
                output.Names("Harvester", "Harvesters");
                output.FlavorText(new FlavorText(
                    new Texts { },
                    new Texts { },
                    new Texts { "alien", "harvester" },
                    "Scythes"
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 18,
                    StomachSize = 30,
                    HasTail = true,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
                    ExpMultiplier = 1.5f,
                    PowerAdjustment = 2.2f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(12, 22),
                        Dexterity = new RaceStats.StatRange(6, 14),
                        Endurance = new RaceStats.StatRange(18, 24),
                        Mind = new RaceStats.StatRange(6, 10),
                        Will = new RaceStats.StatRange(10, 22),
                        Agility = new RaceStats.StatRange(18, 28),
                        Voracity = new RaceStats.StatRange(18, 24),
                        Stomach = new RaceStats.StatRange(10, 14),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.SlowDigestion,
                        TraitType.Intimidating,
                        TraitType.BornToMove,
                        TraitType.NimbleClimber,
                    },
                    RaceDescription = "A lifeform from far beyond the stars, the Harvesters saw the empty lands fill and felt rising hunger. How they made their way here is unknown, but their mission is readily understood. They are here to feed until the land is empty once more.",
                });
                output.DickSizes = () => 1;
                output.BreastSizes = () => 1;
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.Harvester);
                output.CanBeGender = new List<Gender> { Gender.Male };
            });

            builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Harvester, input.U.SkinColor));
                if (!input.A.Targetable)
                {
                    output.Sprite(input.Sprites.Harvester[2]);
                    return;
                }

                if (input.A.IsOralVoring)
                {
                    input.A.AnimationController.FrameLists[0].CurrentlyActive = false;
                    input.A.AnimationController.FrameLists[0].CurrentFrame = 0;
                    input.A.AnimationController.FrameLists[0].CurrentTime = 0f;
                    output.Sprite(input.Sprites.Harvester[5]);
                    return;
                }

                if (input.A.AnimationController.FrameLists[0].CurrentlyActive)
                {
                    if (input.A.AnimationController.FrameLists[0].CurrentTime >= frameListEyes.Times[input.A.AnimationController.FrameLists[0].CurrentFrame])
                    {
                        input.A.AnimationController.FrameLists[0].CurrentFrame++;
                        input.A.AnimationController.FrameLists[0].CurrentTime = 0f;

                        if (input.A.AnimationController.FrameLists[0].CurrentFrame >= frameListEyes.Frames.Length)
                        {
                            input.A.AnimationController.FrameLists[0].CurrentlyActive = false;
                            input.A.AnimationController.FrameLists[0].CurrentFrame = 0;
                            input.A.AnimationController.FrameLists[0].CurrentTime = 0f;
                        }
                    }

                    output.Sprite(input.Sprites.Harvester[2 + frameListEyes.Frames[input.A.AnimationController.FrameLists[0].CurrentFrame]]);
                    return;
                }

                if (State.Rand.Next(400) == 0)
                {
                    input.A.AnimationController.FrameLists[0].CurrentlyActive = true;
                }

                output.Sprite(input.Sprites.Harvester[2]);
            }); // Head

            builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Harvester, input.U.SkinColor));
                if (input.A.AnimationController.FrameLists == null)
                {
                    SetUpAnimations(input.Actor);
                }

                output.Sprite(input.Sprites.Harvester[0]);
            }); // Body

            builder.RenderSingle(SpriteType.BodyAccent, 0, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Harvester, input.U.SkinColor));
                output.Sprite(input.Sprites.Harvester[1]);
            }); // Tail

            builder.RenderSingle(SpriteType.BodyAccent2, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Harvester, input.U.SkinColor));
                if (!input.A.Targetable)
                {
                    output.Sprite(input.Sprites.Harvester[6]);
                    return;
                }

                if (input.A.IsAttacking)
                {
                    input.A.AnimationController.FrameLists[1].CurrentlyActive = false;
                    input.A.AnimationController.FrameLists[1].CurrentFrame = 0;
                    input.A.AnimationController.FrameLists[1].CurrentTime = 0f;
                    output.Sprite(input.Sprites.Harvester[9]);
                    return;
                }

                if (input.A.AnimationController.FrameLists[1].CurrentlyActive)
                {
                    if (input.A.AnimationController.FrameLists[1].CurrentTime >= frameListArms.Times[input.A.AnimationController.FrameLists[1].CurrentFrame])
                    {
                        input.A.AnimationController.FrameLists[1].CurrentFrame++;
                        input.A.AnimationController.FrameLists[1].CurrentTime = 0f;

                        if (input.A.AnimationController.FrameLists[1].CurrentFrame >= frameListArms.Frames.Length)
                        {
                            input.A.AnimationController.FrameLists[1].CurrentlyActive = false;
                            input.A.AnimationController.FrameLists[1].CurrentFrame = 0;
                            input.A.AnimationController.FrameLists[1].CurrentTime = 0f;
                        }
                    }

                    output.Sprite(input.Sprites.Harvester[6 + frameListArms.Frames[input.A.AnimationController.FrameLists[1].CurrentFrame]]);
                    return;
                }

                if (State.Rand.Next(600) == 0)
                {
                    input.A.AnimationController.FrameLists[1].CurrentlyActive = true;
                }

                output.Sprite(input.Sprites.Harvester[6]);
            }); // Arms

            builder.RenderSingle(SpriteType.BodyAccent3, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Harvester, input.U.SkinColor));
                if (!input.A.Targetable)
                {
                    return;
                }

                if (input.A.IsOralVoring)
                {
                    input.A.AnimationController.FrameLists[3].CurrentlyActive = false;
                    input.A.AnimationController.FrameLists[3].CurrentFrame = 0;
                    input.A.AnimationController.FrameLists[3].CurrentTime = 0f;
                    return;
                }

                if (input.A.AnimationController.FrameLists[3].CurrentlyActive)
                {
                    if (input.A.AnimationController.FrameLists[3].CurrentTime >= frameListTongue.Times[input.A.AnimationController.FrameLists[3].CurrentFrame])
                    {
                        input.A.AnimationController.FrameLists[3].CurrentFrame++;
                        input.A.AnimationController.FrameLists[3].CurrentTime = 0f;

                        if (input.A.AnimationController.FrameLists[3].CurrentFrame >= frameListTongue.Frames.Length)
                        {
                            input.A.AnimationController.FrameLists[3].CurrentlyActive = false;
                            input.A.AnimationController.FrameLists[3].CurrentFrame = 0;
                            input.A.AnimationController.FrameLists[3].CurrentTime = 0f;
                        }
                    }

                    output.Sprite(input.Sprites.Harvester[12 + frameListTongue.Frames[input.A.AnimationController.FrameLists[3].CurrentFrame]]);
                    return;
                }

                if (State.Rand.Next(500) == 0 && input.A.HasBelly)
                {
                    input.A.AnimationController.FrameLists[3].CurrentlyActive = true;
                }
            }); // Tongue

            builder.RenderSingle(SpriteType.Belly, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Harvester, input.U.SkinColor));
                if (!input.A.HasBelly)
                {
                    return;
                }

                int size = input.A.GetStomachSize(26);

                if (size > 18)
                {
                    size = 18;
                }

                output.Sprite(input.Sprites.Harvester[17 + size]);
            }); // Belly

            builder.RenderSingle(SpriteType.Dick, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Harvester, input.U.SkinColor));
                if (!input.A.IsErect())
                {
                    return;
                }

                if (!input.A.Targetable)
                {
                    return;
                }

                if (input.A.AnimationController.FrameLists[2].CurrentlyActive)
                {
                    if (input.A.AnimationController.FrameLists[2].CurrentTime >= frameListDick.Times[input.A.AnimationController.FrameLists[2].CurrentFrame])
                    {
                        input.A.AnimationController.FrameLists[2].CurrentFrame++;
                        input.A.AnimationController.FrameLists[2].CurrentTime = 0f;

                        if (input.A.AnimationController.FrameLists[2].CurrentFrame >= frameListDick.Frames.Length)
                        {
                            input.A.AnimationController.FrameLists[2].CurrentlyActive = false;
                            input.A.AnimationController.FrameLists[2].CurrentFrame = 0;
                            input.A.AnimationController.FrameLists[2].CurrentTime = 0f;
                        }
                    }

                    output.Sprite(input.Sprites.Harvester[10 + frameListDick.Frames[input.A.AnimationController.FrameLists[2].CurrentFrame]]);
                    return;
                }

                if (State.Rand.Next(300) == 0)
                {
                    input.A.AnimationController.FrameLists[2].CurrentlyActive = true;
                    input.A.AnimationController.FrameLists[2].CurrentFrame = 0;
                    input.A.AnimationController.FrameLists[2].CurrentTime = 0f;
                }

                if (input.A.IsErect())
                {
                    output.Sprite(input.Sprites.Harvester[10]);
                }
            }); // Dick

            builder.RunBefore(Defaults.Finalize);

            builder.RandomCustom(data =>
            {
                IUnitRead unit = data.Unit;

                unit.SkinColor = State.Rand.Next(0, data.SetupOutput.SkinColors);
                unit.DickSize = 0;
                unit.SetDefaultBreastSize(0);
            });
        });


        private static void SetUpAnimations(IActorUnit actor)
        {
            actor.AnimationController.FrameLists = new[]
            {
                new AnimationController.FrameList(0, 0, false), // Eye controller. Index 0.
                new AnimationController.FrameList(0, 0, false), // Arm controller. Index 1.
                new AnimationController.FrameList(0, 0, false), // Dick controller. Index 2.
                new AnimationController.FrameList(0, 0, false)
            }; // Tongue controller. Index 3.
        }
    }
}
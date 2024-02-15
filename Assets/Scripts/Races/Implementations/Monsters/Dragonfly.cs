#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class Dragonfly
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            RaceFrameList frameListWings = new RaceFrameList(new int[3] { 0, 1, 2 }, new float[3] { .02f, .02f, .02f });

            builder.Setup(output =>
            {
                output.Names("Dragonfly", "Dragonflies");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 9,
                    StomachSize = 9,
                    HasTail = false,
                    FavoredStat = Stat.Dexterity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
                    ExpMultiplier = 1.2f,
                    PowerAdjustment = 1.4f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(8, 10),
                        Dexterity = new RaceStats.StatRange(15, 20),
                        Endurance = new RaceStats.StatRange(8, 10),
                        Mind = new RaceStats.StatRange(6, 8),
                        Will = new RaceStats.StatRange(8, 12),
                        Agility = new RaceStats.StatRange(15, 20),
                        Voracity = new RaceStats.StatRange(8, 12),
                        Stomach = new RaceStats.StatRange(8, 10),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Flight,
                        TraitType.Tempered
                    },
                    RaceDescription = "The ambient energies that abound in this world sometimes cause normal creatures to grow to abnormal sizes. These dragonflies have adapted their diet to suit their new size and abilities, and are a terror to face unprepared."
                });
                output.CanBeGender = new List<Gender> { Gender.None };

                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.Dragonfly);
                output.GentleAnimation = true;
            });


            builder.RenderSingle(SpriteType.Head, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Dragonfly, input.U.SkinColor));
                if (input.A.IsOralVoring || input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Dragonfly[1]);
                    return;
                }

                output.Sprite(input.Sprites.Dragonfly[0]);
            }); // Head

            builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Dragonfly, input.U.SkinColor));
                if (input.A.AnimationController.FrameLists == null)
                {
                    SetUpAnimations(input.Actor);
                }

                output.Sprite(input.Sprites.Dragonfly[2]);
            }); // Body

            builder.RenderSingle(SpriteType.BodyAccent, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Dragonfly, input.U.SkinColor));
                if (input.A.AnimationController.FrameLists[0].CurrentTime >= frameListWings.Times[input.A.AnimationController.FrameLists[0].CurrentFrame] && input.U.IsDead == false)
                {
                    input.A.AnimationController.FrameLists[0].CurrentFrame++;
                    input.A.AnimationController.FrameLists[0].CurrentTime = 0f;

                    if (input.A.AnimationController.FrameLists[0].CurrentFrame >= frameListWings.Frames.Length)
                    {
                        input.A.AnimationController.FrameLists[0].CurrentFrame = 0;
                        input.A.AnimationController.FrameLists[0].CurrentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Dragonfly[3 + frameListWings.Frames[input.A.AnimationController.FrameLists[0].CurrentFrame]]);
            }); // Wings

            builder.RenderSingle(SpriteType.Belly, 0, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Dragonfly, input.U.SkinColor));
                if (input.U.Predator == false)
                {
                    return;
                }

                if (!input.A.HasBelly)
                {
                    output.Sprite(input.Sprites.Dragonfly[6]);
                    return;
                }

                output.Sprite(input.Sprites.Dragonfly[7 + input.A.GetStomachSize(19)]);
            }); // Belly

            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(Defaults.RandomCustom);
        });

        private static void SetUpAnimations(IActorUnit actor)
        {
            actor.AnimationController.FrameLists = new[]
            {
                new AnimationController.FrameList(State.Rand.Next(0, 3), 0, true)
            }; // Wing controller. Index 0.
        }
    }
}
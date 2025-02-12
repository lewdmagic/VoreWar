﻿#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class DarkSwallower
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            RaceFrameList frameListTail = new RaceFrameList(new int[8] { 0, 1, 2, 3, 4, 3, 2, 1 }, new float[8] { 1.2f, 1f, 1f, 1f, 1.2f, 1f, 1f, 1f });
            RaceFrameList frameListFins = new RaceFrameList(new int[4] { 0, 1, 2, 1 }, new float[4] { 1f, .8f, 1f, .8f });


            builder.Setup((input, output) =>
            {
                output.Names("Dark Swallower", "Dark Swallowers");
                output.BonesInfo((unit) => new List<BoneInfo>()
                {
                    new BoneInfo(BoneType.DarkSwallower, unit.Name)
                });
                output.FlavorText(new FlavorText(
                    new Texts { },
                    new Texts { },
                    new Texts { },
                    "Jaws"
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 10,
                    StomachSize = 20,
                    HasTail = true,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
                    ExpMultiplier = 1.5f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new StatRange(6, 14),
                        Dexterity = new StatRange(4, 8),
                        Endurance = new StatRange(10, 16),
                        Mind = new StatRange(6, 10),
                        Will = new StatRange(8, 14),
                        Agility = new StatRange(8, 12),
                        Voracity = new StatRange(12, 20),
                        Stomach = new StatRange(6, 12),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Flight,
                        TraitType.Ravenous
                    },
                    RaceDescription = "As the Scylla arrived in the new lands they brought some of their pets along. Not a year later the strange properties of the new realm had caused the fish to breed out of control, soon escaping and going wild.",
                });
                output.EyeTypes = 6;
                output.CanBeGender = new List<Gender> { Gender.None };
                output.SkinColors = ColorMap.SharkColorCount;
            });


            builder.RenderSingle(SpriteType.Eyes, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.DarkSwallower[2 + input.U.EyeType]);
            }); // Eyes.
            builder.RenderSingle(SpriteType.Mouth, 5, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.A.IsAttacking || input.A.IsEating ? input.Sprites.DarkSwallower[8] : null);
            }); // Mouth inside.
            builder.RenderSingle(SpriteType.Body, 3, (input, output) =>
            {
                output.Coloring(ColorMap.GetSharkColor(input.U.SkinColor));
                if (input.A.AnimationController.FrameLists == null)
                {
                    SetUpAnimations(input.Actor);
                }

                if (input.A.IsEating || input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.DarkSwallower[1]);
                    return;
                }

                output.Sprite(input.Sprites.DarkSwallower[0]);
            }); // Body, open mouth base.

            builder.RenderSingle(SpriteType.BodyAccent, 0, (input, output) =>
            {
                output.Coloring(ColorMap.GetSharkColor(input.U.SkinColor));
                if (!input.A.Targetable)
                {
                    output.Sprite(input.Sprites.DarkSwallower[9]);
                    return;
                }

                if (input.A.AnimationController.FrameLists[0].CurrentTime >= frameListTail.Times[input.A.AnimationController.FrameLists[0].CurrentFrame])
                {
                    input.A.AnimationController.FrameLists[0].CurrentFrame++;
                    input.A.AnimationController.FrameLists[0].CurrentTime = 0f;

                    if (input.A.AnimationController.FrameLists[0].CurrentFrame >= frameListTail.Frames.Length)
                    {
                        input.A.AnimationController.FrameLists[0].CurrentFrame = 0;
                    }
                }

                output.Sprite(input.Sprites.DarkSwallower[9 + frameListTail.Frames[input.A.AnimationController.FrameLists[0].CurrentFrame]]);
            }); // Tail.

            builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
            {
                output.Coloring(ColorMap.GetSharkColor(input.U.SkinColor));
                if (!input.A.Targetable)
                {
                    output.Sprite(input.Sprites.DarkSwallower[14]);
                    return;
                }

                if (input.A.AnimationController.FrameLists[1].CurrentTime >= frameListFins.Times[input.A.AnimationController.FrameLists[1].CurrentFrame])
                {
                    input.A.AnimationController.FrameLists[1].CurrentFrame++;
                    input.A.AnimationController.FrameLists[1].CurrentTime = 0f;

                    if (input.A.AnimationController.FrameLists[1].CurrentFrame >= frameListFins.Frames.Length)
                    {
                        input.A.AnimationController.FrameLists[1].CurrentFrame = 0;
                    }
                }

                output.Sprite(input.Sprites.DarkSwallower[14 + frameListFins.Frames[input.A.AnimationController.FrameLists[1].CurrentFrame]]);
            }); // Sidefins.

            builder.RenderSingle(SpriteType.BodyAccent3, 1, (input, output) =>
            {
                output.Coloring(ColorMap.GetSharkColor(input.U.SkinColor));
                if (!input.A.Targetable)
                {
                    output.Sprite(input.Sprites.DarkSwallower[17]);
                    return;
                }

                if (input.A.AnimationController.FrameLists[0].CurrentFrame % 2 == 0)
                {
                    output.Sprite(input.Sprites.DarkSwallower[17]);
                    return;
                }

                output.Sprite(input.Sprites.DarkSwallower[18]);
            }); // Bellyfins.

            builder.RenderSingle(SpriteType.Belly, 2, (input, output) =>
            {
                output.Coloring(ColorMap.GetSharkColor(input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    output.Sprite(input.Sprites.DarkSwallower[19]);
                    return;
                }

                int size = input.A.GetStomachSize(29);

                if (size > 21)
                {
                    size = 21;
                }

                output.Sprite(input.Sprites.DarkSwallower[19 + size]);
            }); // Belly.

            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(Defaults.Randomize);
        });

        private static void SetUpAnimations(IActorUnit actor)
        {
            actor.AnimationController.FrameLists = new[]
            {
                new AnimationController.FrameList(State.Rand.Next(0, 8), State.Rand.Next(1, 7) / 10f, true), // Tail controller. Index 0.
                new AnimationController.FrameList(State.Rand.Next(0, 4), State.Rand.Next(1, 9) / 10f, true)
            }; // Fin controller. Index 1.
        }
    }
}
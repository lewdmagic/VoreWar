#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class Earthworms
    {

        internal static readonly IRaceData Instance = RaceBuilder.CreateV2(Defaults.Blank<EarthWormParameters>, builder =>
        {
            RaceFrameList frameListHeadIdle = new RaceFrameList(new int[5] { 0, 1, 2, 1, 0 }, new float[5] { .5f, .5f, 1.5f, .5f, .5f });


            builder.Setup(output =>
            {
                output.Names("Earthworm", "Earthworms");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 12,
                    StomachSize = 16,
                    HasTail = false,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
                    ExpMultiplier = 1.0f,
                    PowerAdjustment = 1.0f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(8, 12),
                        Dexterity = new RaceStats.StatRange(8, 12),
                        Endurance = new RaceStats.StatRange(10, 16),
                        Mind = new RaceStats.StatRange(6, 10),
                        Will = new RaceStats.StatRange(6, 10),
                        Agility = new RaceStats.StatRange(10, 16),
                        Voracity = new RaceStats.StatRange(20, 28),
                        Stomach = new RaceStats.StatRange(16, 24),
                    },
                    RacialTraits = new List<Traits>()
                    {
                        Traits.EasyToVore,
                        Traits.SteadyStomach,
                        Traits.AllOutFirstStrike
                    },
                    RaceDescription = ""
                });
                output.CanBeGender = new List<Gender> { Gender.None };
                output.ClothingColors = 0;
                output.GentleAnimation = true;
                output.WeightGainDisabled = true;
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.EarthwormSkin);
            });


            builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EarthwormSkin, input.U.SkinColor));
                if (!input.A.Targetable)
                {
                    output.Sprite(input.Sprites.Earthworms[8]);
                    return;
                }

                if (input.A.IsAttacking || input.A.IsEating || input.Params.Position == Position.Underground)
                {
                    input.A.AnimationController.frameLists[0].currentlyActive = false;
                    input.A.AnimationController.frameLists[0].currentFrame = 0;
                    input.A.AnimationController.frameLists[0].currentTime = 0f;

                    if (input.A.IsEating || input.A.IsAttacking)
                    {
                        if (input.Params.Position == Position.Underground)
                        {
                            output.Sprite(input.Sprites.Earthworms[16]);
                            return;
                        }

                        output.Sprite(input.Sprites.Earthworms[11]);
                        return;
                    }

                    if (input.Params.Position == Position.Underground)
                    {
                        return;
                    }

                    output.Sprite(input.Sprites.Earthworms[8]);
                    return;
                }

                if (input.A.AnimationController.frameLists[0].currentlyActive)
                {
                    if (input.A.AnimationController.frameLists[0].currentTime >= frameListHeadIdle.Times[input.A.AnimationController.frameLists[0].currentFrame])
                    {
                        input.A.AnimationController.frameLists[0].currentFrame++;
                        input.A.AnimationController.frameLists[0].currentTime = 0f;

                        if (input.A.AnimationController.frameLists[0].currentFrame >= frameListHeadIdle.Frames.Length)
                        {
                            input.A.AnimationController.frameLists[0].currentlyActive = false;
                            input.A.AnimationController.frameLists[0].currentFrame = 0;
                            input.A.AnimationController.frameLists[0].currentTime = 0f;
                        }
                    }

                    output.Sprite(input.Sprites.Earthworms[8 + frameListHeadIdle.Frames[input.A.AnimationController.frameLists[0].currentFrame]]);
                    return;
                }

                if (State.Rand.Next(600) == 0)
                {
                    input.A.AnimationController.frameLists[0].currentlyActive = true;
                }

                output.Sprite(input.Sprites.Earthworms[8]);
            });

            builder.RenderSingle(SpriteType.Mouth, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (!input.A.Targetable)
                {
                    output.Sprite(input.Sprites.Earthworms[12]);
                    return;
                }

                switch (input.Params.Position)
                {
                    case Position.Underground:
                        if (input.A.IsEating || input.A.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Earthworms[17]);
                            return;
                        }

                        return;
                    case Position.Aboveground:
                        if (input.A.IsEating || input.A.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Earthworms[15]);
                            return;
                        }

                        output.Sprite(input.Sprites.Earthworms[12 + frameListHeadIdle.Frames[input.A.AnimationController.frameLists[0].currentFrame]]);
                        return;
                    default:
                        return;
                }
            });

            builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EarthwormSkin, input.U.SkinColor));
                if (input.A.AnimationController.frameLists == null)
                {
                    SetUpAnimations(input.Actor);
                }

                if (!input.A.Targetable)
                {
                    output.Sprite(input.Sprites.Earthworms[4]);
                    return;
                }

                switch (input.Params.Position)
                {
                    case Position.Underground:
                        if (input.A.IsEating || input.A.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Earthworms[1]);
                            return;
                        }

                        output.Sprite(input.Sprites.Earthworms[0]);
                        return;
                    case Position.Aboveground:
                        output.Sprite(input.Sprites.Earthworms[4]);
                        return;
                }

                int attackingOffset = input.A.IsAttacking ? 1 : 0;
                if (input.U.BodySize == 0)
                {
                    output.Sprite(input.Sprites.Bodies[attackingOffset]);
                    return;
                }

                int genderOffset = input.U.HasBreasts ? 0 : 8;

                output.Sprite(input.A.HasBodyWeight ? input.Sprites.Legs[(input.U.BodySize - 1) * 2 + genderOffset + attackingOffset] : null);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EarthwormSkin, input.U.SkinColor));
                if (!input.A.Targetable)
                {
                    return;
                }

                if (input.Params.Position == Position.Aboveground)
                {
                    output.Sprite(input.Sprites.Earthworms[6]);
                }
            }); // belly support

            builder.RenderSingle(SpriteType.BodyAccent2, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EarthwormSkin, input.U.SkinColor));
                if (!input.A.Targetable)
                {
                    output.Sprite(input.Sprites.Earthworms[7]);
                    return;
                }

                if (input.Params.Position == Position.Aboveground && input.A.HasBelly == false)
                {
                    output.Sprite(input.Sprites.Earthworms[7]);
                }
            }); // belly cover

            builder.RenderSingle(SpriteType.BodyAccent3, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EarthwormSkin, input.U.SkinColor));
                if (!input.A.Targetable)
                {
                    output.Sprite(input.Sprites.Earthworms[5]);
                    return;
                }

                if (input.Params.Position == Position.Aboveground)
                {
                    output.Sprite(input.Sprites.Earthworms[5]);
                }
            }); // tail

            builder.RenderSingle(SpriteType.BodyAccessory, 5, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (!input.A.Targetable)
                {
                    return;
                }

                switch (input.Params.Position)
                {
                    case Position.Underground:
                        if (input.A.IsEating || input.A.IsAttacking)
                        {
                            output.Sprite(input.Sprites.Earthworms[3]);
                            return;
                        }

                        output.Sprite(input.Sprites.Earthworms[2]);
                        return;
                    case Position.Aboveground:
                        return;
                    default:
                        return;
                }
            }); // rocks

            builder.RenderSingle(SpriteType.Belly, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EarthwormSkin, input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    return;
                }

                if (input.Params.Position == Position.Aboveground)
                {

                    output.Sprite(input.Sprites.Earthworms[18 + input.A.GetStomachSize(21)]);
                }
            });


            builder.RunBefore((input, output) =>
            {
                output.Params.Position = !input.A.HasAttackedThisCombat ? Position.Underground : Position.Aboveground;
                //base.RunFirst(data.Actor);

                output.ChangeSprite(SpriteType.Belly).AddOffset(0, -48 * .625f);
            });

            builder.RandomCustom(Defaults.RandomCustom);
        });

        private static void SetUpAnimations(Actor_Unit actor)
        {
            actor.AnimationController.frameLists = new[]
            {
                new AnimationController.FrameList(0, 0, false)
            }; // Index 0.
        }

        internal enum Position
        {
            Underground,
            Aboveground
        }
    
        internal class EarthWormParameters : IParameters
        {
            internal Position Position;
        }
    }
}
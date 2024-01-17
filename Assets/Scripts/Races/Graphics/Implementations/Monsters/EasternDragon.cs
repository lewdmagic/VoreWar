#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class EasternDragon
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            RaceFrameList frameListEyes = new RaceFrameList(new int[5] { 0, 1, 2, 1, 0 }, new float[5] { .2f, .2f, .3f, .2f, .2f });
            RaceFrameList frameListTongue = new RaceFrameList(new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 }, new float[8] { 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f });

            builder.Setup(output =>
            {
                output.Names("Eastern Dragon", "Eastern Dragons");
                output.FlavorText(new FlavorText(
                    new Texts { "tasty noodle", "noodle derg", "spaghetti-like", "easily-slurpable" },
                    new Texts {  }, // Missing in original
                    new Texts { "oriental dragon", "serpentine dragon", {"eastern dragoness", Gender.Female}, {"eastern dragon", Gender.Male} }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 20,
                    StomachSize = 20,
                    HasTail = true,
                    FavoredStat = Stat.Mind,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.CockVore, VoreType.Unbirth },
                    ExpMultiplier = 1.6f,
                    PowerAdjustment = 1.9f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(12, 22),
                        Dexterity = new RaceStats.StatRange(8, 16),
                        Endurance = new RaceStats.StatRange(12, 18),
                        Mind = new RaceStats.StatRange(14, 30),
                        Will = new RaceStats.StatRange(8, 18),
                        Agility = new RaceStats.StatRange(14, 28),
                        Voracity = new RaceStats.StatRange(12, 22),
                        Stomach = new RaceStats.StatRange(12, 22),
                    },
                    RacialTraits = new List<Traits>()
                    {
                        Traits.Flight,
                        Traits.Ravenous
                    },
                    RaceDescription = "A variety of dragons especially attuned to magic, the Eastern Dragons, or Lung Dragons as they are also known as, are able to fly without wings. Reminiscent of snakes, the Eastern Dragons are readily able to prove that the resemblance is more than skin deep, devouring large prey with ease.",

                });
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.EasternDragon); // Main body, legs, head, tail upper
                output.GentleAnimation = true;
                output.WeightGainDisabled = true;
                output.CanBeGender = new List<Gender> { Gender.Male, Gender.Female };
                output.BodySizes = 4; // Horn types
            });


            builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EasternDragon, input.U.SkinColor));
                if (!input.A.Targetable)
                {
                    output.Sprite(input.Sprites.EasternDragon[3]);
                    return;
                }

                if (input.A.IsOralVoring)
                {
                    input.A.AnimationController.frameLists[0].currentlyActive = false;
                    input.A.AnimationController.frameLists[0].currentFrame = 0;
                    input.A.AnimationController.frameLists[0].currentTime = 0f;
                    output.Sprite(input.Sprites.EasternDragon[4]);
                    return;
                }

                if (input.A.AnimationController.frameLists[0].currentlyActive)
                {
                    if (input.A.AnimationController.frameLists[0].currentTime >= frameListEyes.Times[input.A.AnimationController.frameLists[0].currentFrame])
                    {
                        input.A.AnimationController.frameLists[0].currentFrame++;
                        input.A.AnimationController.frameLists[0].currentTime = 0f;

                        if (input.A.AnimationController.frameLists[0].currentFrame >= frameListEyes.Frames.Length)
                        {
                            input.A.AnimationController.frameLists[0].currentlyActive = false;
                            input.A.AnimationController.frameLists[0].currentFrame = 0;
                            input.A.AnimationController.frameLists[0].currentTime = 0f;
                        }
                    }

                    output.Sprite(input.Sprites.EasternDragon[1 + frameListEyes.Frames[input.A.AnimationController.frameLists[0].currentFrame]]);
                    return;
                }

                if (State.Rand.Next(750) == 0)
                {
                    input.A.AnimationController.frameLists[0].currentlyActive = true;
                }

                output.Sprite(input.Sprites.EasternDragon[1]);
            }); // Head

            builder.RenderSingle(SpriteType.Mouth, 8, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.EasternDragon[5]);
                }
            }); // Inner Mouth

            builder.RenderSingle(SpriteType.Body, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EasternDragon, input.U.SkinColor));
                if (input.A.AnimationController.frameLists == null)
                {
                    SetUpAnimations(input.Actor);
                }

                output.Sprite(input.Sprites.EasternDragon[0]);
            }); // Body

            builder.RenderSingle(SpriteType.BodyAccent, 0, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EasternDragon, input.U.SkinColor));
                if (!Config.HideCocks && input.A.PredatorComponent?.BallsFullness > 0)
                {
                    output.Sprite(input.Sprites.EasternDragon[38]).AddOffset(128 * .3125f, 0);
                    return;
                }

                if (input.A.PredatorComponent?.WombFullness > 0)
                {
                    output.Sprite(input.Sprites.EasternDragon[38]).AddOffset(128 * .3125f, 0);
                    return;
                }

                if (input.A.IsCockVoring || input.A.IsUnbirthing)
                {
                    output.Sprite(input.Sprites.EasternDragon[38]).AddOffset(128 * .3125f, 0);
                    return;
                }

                output.Sprite(input.Sprites.EasternDragon[18]).AddOffset(0, 0);
            }); // Tail

            builder.RenderSingle(SpriteType.BodyAccent2, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EasternDragon, input.U.SkinColor));
                if (input.U.GetGender() == Gender.Male)
                {
                    if (input.U.DickSize < 0)
                    {
                        return;
                    }

                    if (Config.HideCocks)
                    {
                        return;
                    }

                    if (input.A.PredatorComponent?.BallsFullness > 0 || input.A.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.EasternDragon[39]);
                    }
                }
                else
                {
                    if (input.A.PredatorComponent?.WombFullness > 0 || input.A.IsUnbirthing)
                    {
                        output.Sprite(input.Sprites.EasternDragon[67]);
                    }
                }
            }); // Sheath/SnatchBase

            builder.RenderSingle(SpriteType.BodyAccent3, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsUnbirthing)
                {
                    output.Sprite(input.Sprites.EasternDragon[68]);
                }
            }); // Snatch

            builder.RenderSingle(SpriteType.BodyAccent4, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EasternDragon, input.U.SkinColor));
                if (input.A.GetWombSize(17) > 0)
                {
                    int sprite = input.A.GetWombSize(17, 0.8f);

                    output.Sprite(input.Sprites.EasternDragon[68 + sprite]);
                }
            }); // Womb

            builder.RenderSingle(SpriteType.SecondaryAccessory, 9, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (!input.A.Targetable)
                {
                    return;
                }

                if (input.A.IsAttacking || input.A.IsOralVoring)
                {
                    input.A.AnimationController.frameLists[1].currentlyActive = false;
                    input.A.AnimationController.frameLists[1].currentFrame = 0;
                    input.A.AnimationController.frameLists[1].currentTime = 0f;
                    return;
                }

                if (input.A.AnimationController.frameLists[1].currentlyActive)
                {
                    if (input.A.AnimationController.frameLists[1].currentTime >= frameListTongue.Times[input.A.AnimationController.frameLists[1].currentFrame])
                    {
                        input.A.AnimationController.frameLists[1].currentFrame++;
                        input.A.AnimationController.frameLists[1].currentTime = 0f;

                        if (input.A.AnimationController.frameLists[1].currentFrame >= frameListTongue.Frames.Length)
                        {
                            input.A.AnimationController.frameLists[1].currentlyActive = false;
                            input.A.AnimationController.frameLists[1].currentFrame = 0;
                            input.A.AnimationController.frameLists[1].currentTime = 0f;
                        }
                    }

                    output.Sprite(input.Sprites.EasternDragon[10 + frameListTongue.Frames[input.A.AnimationController.frameLists[1].currentFrame]]);
                    return;
                }

                if (input.A.PredatorComponent?.VisibleFullness > 0 && State.Rand.Next(1200) == 0)
                {
                    input.A.AnimationController.frameLists[1].currentlyActive = true;
                }
            }); // Tongue

            builder.RenderSingle(SpriteType.BodySize, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EasternDragon, input.U.SkinColor));
                output.Sprite(input.Sprites.EasternDragon[6 + input.U.BodySize]);
            }); // Horns
            builder.RenderSingle(SpriteType.Belly, 8, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EasternDragon, input.U.SkinColor));
                if (input.A.PredatorComponent?.ExclusiveStomachFullness > 0)
                {
                    int sprite = input.A.GetExclusiveStomachSize(16, 0.8f);

                    if (sprite >= 14)
                    {
                        output.Sprite(input.Sprites.EasternDragon[34]);
                        return;
                    }

                    output.Sprite(input.Sprites.EasternDragon[20 + sprite]);
                    return;
                }

                if (input.A.GetExclusiveStomachSize(1) == 0)
                {
                    output.Sprite(input.Sprites.EasternDragon[19]);
                    return;
                }

                output.Sprite(input.Sprites.EasternDragon[20 + input.A.GetExclusiveStomachSize(14, 0.8f)]);
            }); // Belly

            builder.RenderSingle(SpriteType.Dick, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.DickSize < 0)
                {
                    return;
                }

                if (Config.HideCocks)
                {
                    return;
                }

                if (input.A.IsCockVoring)
                {
                    output.Sprite(input.Sprites.EasternDragon[41]);
                    return;
                }

                if (input.A.IsErect())
                {
                    output.Sprite(input.Sprites.EasternDragon[40]);
                }
            }); // Dick, CV, UB

            builder.RenderSingle(SpriteType.Balls, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EasternDragon, input.U.SkinColor));
                if (input.U.DickSize < 0)
                {
                    return;
                }

                if (Config.HideCocks)
                {
                    return;
                }

                if (input.A.PredatorComponent?.BallsFullness > 0 || input.A.IsCockVoring)
                {
                    int sprite = input.A.GetBallSize(24, 0.8f);

                    if (sprite >= 22)
                    {
                        output.Sprite(input.Sprites.EasternDragon[63]);
                        return;
                    }

                    output.Sprite(input.Sprites.EasternDragon[41 + sprite]);
                }
            }); // Balls


            builder.RunBefore((input, output) =>
            {
                output.ChangeSprite(SpriteType.Balls).AddOffset(128 * .3125f, 0);
                output.ChangeSprite(SpriteType.Dick).AddOffset(128 * .3125f, 0);
                output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(128 * .3125f, 0);
                output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(128 * .3125f, 0);
                output.ChangeSprite(SpriteType.BodyAccent4).AddOffset(128 * .3125f, 0);
            });


            builder.RandomCustom(Defaults.RandomCustom);
        });


        private static void SetUpAnimations(Actor_Unit actor)
        {
            actor.AnimationController.frameLists = new[]
            {
                new AnimationController.FrameList(0, 0, false), // Eye controller. Index 0.
                new AnimationController.FrameList(0, 0, false)
            }; // Tongue controller. Index 1.
        }
    }
}
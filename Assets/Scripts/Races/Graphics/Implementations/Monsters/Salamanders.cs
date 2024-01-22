#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class Salamanders
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            RaceFrameList frameListSalamanderFlame = new RaceFrameList(new int[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, new float[10] { .1f, .1f, .1f, .1f, .1f, .1f, .1f, .1f, .1f, .1f });


            builder.Setup(output =>
            {
                output.Names("Salamander", "Salamanders");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 20,
                    StomachSize = 18,
                    HasTail = true,
                    FavoredStat = Stat.Mind,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
                    ExpMultiplier = 1.2f,
                    PowerAdjustment = 1.5f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(10, 16),
                        Dexterity = new RaceStats.StatRange(8, 14),
                        Endurance = new RaceStats.StatRange(8, 14),
                        Mind = new RaceStats.StatRange(12, 20),
                        Will = new RaceStats.StatRange(8, 16),
                        Agility = new RaceStats.StatRange(8, 14),
                        Voracity = new RaceStats.StatRange(12, 20),
                        Stomach = new RaceStats.StatRange(10, 16),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Biter,
                        TraitType.HotBlooded
                    },
                    InnateSpells = new List<SpellTypes>() { SpellTypes.Fireball },
                    RaceDescription = ""
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.Skintone, "Body Color");
                    buttons.SetText(ButtonType.BodyAccessoryColor, "Spine Color");
                    buttons.SetText(ButtonType.BodyAccessoryType, "Spine Type");
                });
                output.CanBeGender = new List<Gender> { Gender.None };
                output.EyeTypes = 6;
                output.SpecialAccessoryCount = 12; // Backside spikes/patterns
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.SalamanderSkin); // Backside spikes/pattern color
                output.EyeColors = ColorPaletteMap.GetPaletteCount(SwapType.SalamanderSkin);
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.SalamanderSkin);
                output.GentleAnimation = true;
                output.WeightGainDisabled = true;
                output.ClothingColors = 0;
            });


            builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SalamanderSkin, input.U.SkinColor));
                if (input.A.IsEating || input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Salamanders[1]);
                    return;
                }

                output.Sprite(input.Sprites.Salamanders[0]);
            });

            builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SalamanderSkin, input.U.EyeColor));
                output.Sprite(input.Sprites.Salamanders[4 + input.U.EyeType]);
            });
            builder.RenderSingle(SpriteType.Mouth, 5, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Salamanders[2]);
                    return;
                }

                if (input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Salamanders[3]);
                }
            });

            builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SalamanderSkin, input.U.SkinColor));
                if (input.A.AnimationController.frameLists == null)
                {
                    SetUpAnimations(input.Actor);
                }

                output.Sprite(input.Sprites.Salamanders[32]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.AnimationController.frameLists[0].currentTime >= frameListSalamanderFlame.Times[input.A.AnimationController.frameLists[0].currentFrame] && input.U.IsDead == false)
                {
                    input.A.AnimationController.frameLists[0].currentFrame++;
                    input.A.AnimationController.frameLists[0].currentTime = 0f;

                    if (input.A.AnimationController.frameLists[0].currentFrame >= frameListSalamanderFlame.Frames.Length)
                    {
                        input.A.AnimationController.frameLists[0].currentFrame = 0;
                        input.A.AnimationController.frameLists[0].currentTime = 0f;
                    }
                }

                output.Sprite(input.Sprites.Salamanders[22 + frameListSalamanderFlame.Frames[input.A.AnimationController.frameLists[0].currentFrame]]);
            }); // flame

            builder.RenderSingle(SpriteType.BodyAccent2, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SalamanderSkin, input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    output.Sprite(input.Sprites.Salamanders[33]);
                }
            }); // Belly cover up

            builder.RenderSingle(SpriteType.BodyAccent3, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SalamanderSkin, input.U.SkinColor));
                if (input.A.GetStomachSize(16) < 5)
                {
                    output.Sprite(input.Sprites.Salamanders[34]);
                    return;
                }

                if (input.A.GetStomachSize(16) >= 5 && input.A.GetStomachSize(16) < 9)
                {
                    output.Sprite(input.Sprites.Salamanders[35]);
                    return;
                }

                if (input.A.GetStomachSize(16) >= 9 && input.A.GetStomachSize(16) < 13)
                {
                    output.Sprite(input.Sprites.Salamanders[36]);
                    return;
                }

                if (input.A.GetStomachSize(16) >= 13 && input.A.GetStomachSize(16) < 16)
                {
                    output.Sprite(input.Sprites.Salamanders[37]);
                    return;
                }

                if (input.A.GetStomachSize(16) == 16)
                {
                    output.Sprite(input.Sprites.Salamanders[38]);
                    return;
                }

                output.Sprite(input.Sprites.Salamanders[34]);
            }); // right back leg

            builder.RenderSingle(SpriteType.BodyAccent4, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SalamanderSkin, input.U.SkinColor));
                if (input.A.GetStomachSize(16) < 5)
                {
                    output.Sprite(input.Sprites.Salamanders[39]);
                    return;
                }

                if (input.A.GetStomachSize(16) >= 5 && input.A.GetStomachSize(16) < 8)
                {
                    output.Sprite(input.Sprites.Salamanders[40]);
                    return;
                }

                if (input.A.GetStomachSize(16) >= 8 && input.A.GetStomachSize(16) < 10)
                {
                    output.Sprite(input.Sprites.Salamanders[41]);
                    return;
                }

                if (input.A.GetStomachSize(16) >= 10 && input.A.GetStomachSize(16) < 13)
                {
                    output.Sprite(input.Sprites.Salamanders[42]);
                    return;
                }

                if (input.A.GetStomachSize(16) >= 13 && input.A.GetStomachSize(16) < 15)
                {
                    output.Sprite(input.Sprites.Salamanders[43]);
                    return;
                }

                if (input.A.GetStomachSize(16) >= 15)
                {
                    output.Sprite(input.Sprites.Salamanders[44]);
                    return;
                }

                output.Sprite(input.Sprites.Salamanders[39]);
            }); // left front leg

            builder.RenderSingle(SpriteType.BodyAccent5, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SalamanderSkin, input.U.SkinColor));
                if (input.A.GetStomachSize(16) < 2)
                {
                    output.Sprite(input.Sprites.Salamanders[45]);
                    return;
                }

                if (input.A.GetStomachSize(16) >= 2 && input.A.GetStomachSize(16) < 5)
                {
                    output.Sprite(input.Sprites.Salamanders[46]);
                    return;
                }

                if (input.A.GetStomachSize(16) >= 5 && input.A.GetStomachSize(16) < 7)
                {
                    output.Sprite(input.Sprites.Salamanders[47]);
                    return;
                }

                if (input.A.GetStomachSize(16) >= 7 && input.A.GetStomachSize(16) < 9)
                {
                    output.Sprite(input.Sprites.Salamanders[48]);
                    return;
                }

                if (input.A.GetStomachSize(16) >= 9 && input.A.GetStomachSize(16) < 11)
                {
                    output.Sprite(input.Sprites.Salamanders[49]);
                    return;
                }

                if (input.A.GetStomachSize(16) >= 11 && input.A.GetStomachSize(16) < 13)
                {
                    output.Sprite(input.Sprites.Salamanders[50]);
                    return;
                }

                if (input.A.GetStomachSize(16) >= 13 && input.A.GetStomachSize(16) < 15)
                {
                    output.Sprite(input.Sprites.Salamanders[51]);
                    return;
                }

                if (input.A.GetStomachSize(16) >= 15)
                {
                    output.Sprite(input.Sprites.Salamanders[52]);
                    return;
                }

                output.Sprite(input.Sprites.Salamanders[45]);
            }); // right front leg

            builder.RenderSingle(SpriteType.BodyAccessory, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SalamanderSkin, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Salamanders[10 + input.U.SpecialAccessoryType]);
            }); // Backside spikes/patterns
            builder.RenderSingle(SpriteType.Belly, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SalamanderSkin, input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    return;
                }

                output.Sprite(input.Sprites.Salamanders[53 + input.A.GetStomachSize(16)]);
            });

            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(Defaults.RandomCustom);
        });

        private static void SetUpAnimations(Actor_Unit actor)
        {
            actor.AnimationController.frameLists = new[]
            {
                new AnimationController.FrameList(State.Rand.Next(0, 2), 0, true)
            };
        }
    }
}
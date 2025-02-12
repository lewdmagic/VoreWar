﻿#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class CoralSlugs
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup((input, output) =>
            {
                output.Names("Coral Slug", "Coral Slugs");
                output.BonesInfo(null);
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 10,
                    StomachSize = 16,
                    HasTail = false,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
                    ExpMultiplier = 1.2f,
                    PowerAdjustment = 1.5f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new StatRange(8, 12),
                        Dexterity = new StatRange(6, 8),
                        Endurance = new StatRange(10, 15),
                        Mind = new StatRange(4, 6),
                        Will = new StatRange(8, 12),
                        Agility = new StatRange(4, 6),
                        Voracity = new StatRange(16, 24),
                        Stomach = new StatRange(8, 16),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Paralyzer,
                        TraitType.Stinger,
                        TraitType.GelatinousBody, // or resilient
                        TraitType.SoftBody,
                        TraitType.SlowMovement,
                        TraitType.Toxic
                    },
                    InnateSpells = new List<SpellType>() { SpellType.Poison },
                    RaceDescription = ""
                });
                output.CanBeGender = new List<Gender> { Gender.None };
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.SlugSkin);
                output.GentleAnimation = true;
                output.ClothingColors = 0;
            });


            builder.RenderSingle(SpriteType.Head, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlugSkin, input.U.SkinColor));
                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.CoralSlug[1]);
                    return;
                }

                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.CoralSlug[2]);
                    return;
                }

                output.Sprite(input.Sprites.CoralSlug[0]);
            });

            builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlugSkin, input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    output.Sprite(input.Sprites.CoralSlug[5]);
                    return;
                }

                if (input.A.GetStomachSize(9) < 3)
                {
                    output.Sprite(input.Sprites.CoralSlug[5]);
                    return;
                }

                if (input.A.GetStomachSize(9) >= 3 && input.A.GetStomachSize(9) < 5)
                {
                    output.Sprite(input.Sprites.CoralSlug[6]);
                    return;
                }

                if (input.A.GetStomachSize(9) >= 5 && input.A.GetStomachSize(9) < 7)
                {
                    output.Sprite(input.Sprites.CoralSlug[7]);
                    return;
                }

                if (input.A.GetStomachSize(9) >= 7 && input.A.GetStomachSize(9) < 9)
                {
                    output.Sprite(input.Sprites.CoralSlug[8]);
                    return;
                }

                output.Sprite(input.Sprites.CoralSlug[9]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 10, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.CoralSlug[3]);
                }
            }); // acid

            builder.RenderSingle(SpriteType.BodySize, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlugSkin, input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    output.Sprite(input.Sprites.CoralSlug[4]);
                }
            }); // belly cover up

            builder.RenderSingle(SpriteType.Belly, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlugSkin, input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    return;
                }

                output.Sprite(input.Sprites.CoralSlug[10 + input.A.GetStomachSize(9)]);
            });
            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(Defaults.Randomize);
        });
    }
}
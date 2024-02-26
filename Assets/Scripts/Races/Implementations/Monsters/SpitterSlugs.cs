#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class SpitterSlugs
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Spitter Slug", "Spitter Slugs");
                output.BonesInfo(null);
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 12,
                    StomachSize = 20,
                    HasTail = false,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
                    ExpMultiplier = 1.2f,
                    PowerAdjustment = 1.5f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new StatRange(6, 8),
                        Dexterity = new StatRange(8, 12),
                        Endurance = new StatRange(10, 15),
                        Mind = new StatRange(4, 6),
                        Will = new StatRange(8, 12),
                        Agility = new StatRange(4, 6),
                        Voracity = new StatRange(20, 30),
                        Stomach = new StatRange(8, 16),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.BoggingSlime,
                        TraitType.GelatinousBody, // or resilient
                        TraitType.SoftBody,
                        TraitType.SlowMovement,
                        TraitType.GlueBomb
                    },
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
                    output.Sprite(input.Sprites.SpitterSlug[3]);
                    return;
                }

                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.SpitterSlug[4]);
                    return;
                }

                output.Sprite(input.Sprites.SpitterSlug[2]);
            });

            builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlugSkin, input.U.SkinColor));
                if (input.A.IsOralVoring || input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.SpitterSlug[1]);
                    return;
                }

                output.Sprite(input.Sprites.SpitterSlug[0]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 10, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.SpitterSlug[7]);
                    return;
                }

                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.SpitterSlug[8]);
                    return;
                }

                output.Sprite(input.Sprites.SpitterSlug[6]);
            }); // slime

            builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlugSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.SpitterSlug[23]);
            }); // tail end
            builder.RenderSingle(SpriteType.BodySize, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlugSkin, input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    output.Sprite(input.Sprites.SpitterSlug[22]);
                }
            }); // belly cover up

            builder.RenderSingle(SpriteType.Belly, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlugSkin, input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    return;
                }

                output.Sprite(input.Sprites.SpitterSlug[11 + input.A.GetStomachSize(9)]);
            });

            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(Defaults.RandomCustom);
        });
    }
}
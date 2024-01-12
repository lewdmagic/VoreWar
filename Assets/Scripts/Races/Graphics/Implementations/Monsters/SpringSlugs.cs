#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class SpringSlugs
    {
        internal static readonly IRaceData Instance = RaceBuilder.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Spring Slug", "Spring Slugs");
                output.BonesInfo(null);
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 6,
                    StomachSize = 12,
                    HasTail = false,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
                    ExpMultiplier = .9f,
                    PowerAdjustment = .9f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(8, 12),
                        Dexterity = new RaceStats.StatRange(6, 8),
                        Endurance = new RaceStats.StatRange(8, 12),
                        Mind = new RaceStats.StatRange(4, 6),
                        Will = new RaceStats.StatRange(8, 12),
                        Agility = new RaceStats.StatRange(4, 6),
                        Voracity = new RaceStats.StatRange(16, 24),
                        Stomach = new RaceStats.StatRange(8, 16),
                    },
                    RacialTraits = new List<Traits>()
                    {
                        Traits.BoggingSlime,
                        Traits.EasyToVore,
                        Traits.Replaceable,
                        Traits.Pounce,
                        Traits.SoftBody,
                        Traits.SlowMovement
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
                if (input.A.IsOralVoring || input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.SpringSlug[1]);
                    return;
                }

                output.Sprite(input.Sprites.SpringSlug[0]);
            });

            builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlugSkin, input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    output.Sprite(input.Sprites.SpringSlug[2]);
                    return;
                }

                output.Sprite(input.Sprites.SpringSlug[5 + input.A.GetStomachSize(9)]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlugSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.SpringSlug[4]);
            }); // tail end
            builder.RenderSingle(SpriteType.BodySize, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlugSkin, input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    output.Sprite(input.Sprites.SpringSlug[3]);
                }
            }); // belly cover up

            builder.RenderSingle(SpriteType.Belly, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlugSkin, input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    return;
                }

                output.Sprite(input.Sprites.SpringSlug[16 + input.A.GetStomachSize(9)]);
            });

            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(Defaults.RandomCustom);
        });
    }
}
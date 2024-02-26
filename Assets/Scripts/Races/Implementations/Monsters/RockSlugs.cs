#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class RockSlugs
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Rock Slug", "Rock Slugs");
                output.BonesInfo(null);
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 32,
                    StomachSize = 50,
                    HasTail = false,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
                    ExpMultiplier = 2.5f,
                    PowerAdjustment = 3.0f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new StatRange(16, 24),
                        Dexterity = new StatRange(6, 8),
                        Endurance = new StatRange(32, 40),
                        Mind = new StatRange(4, 6),
                        Will = new StatRange(8, 12),
                        Agility = new StatRange(4, 6),
                        Voracity = new StatRange(32, 40),
                        Stomach = new StatRange(12, 24),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Resilient,
                        TraitType.SoftBody,
                        TraitType.VerySlowMovement,
                        TraitType.HardSkin
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
                    output.Sprite(input.Sprites.RockSlug[1]);
                    return;
                }

                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.RockSlug[2]);
                    return;
                }

                output.Sprite(input.Sprites.RockSlug[0]);
            });

            builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlugSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.RockSlug[3]);
            });
            builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlugSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.RockSlug[4]);
            }); // tail end
            builder.RenderSingle(SpriteType.BodySize, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlugSkin, input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    output.Sprite(input.Sprites.RockSlug[5]);
                }
            }); // belly cover up

            builder.RenderSingle(SpriteType.Belly, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.SlugSkin, input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    return;
                }

                output.Sprite(input.Sprites.RockSlug[6 + input.A.GetStomachSize(11)]);
            });


            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(Defaults.RandomCustom);
        });
    }
}
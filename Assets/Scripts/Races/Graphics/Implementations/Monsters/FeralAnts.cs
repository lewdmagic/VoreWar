#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class FeralAnts
    {
        internal static readonly IRaceData Instance = RaceBuilder.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Feral Ant", "Feral Ants");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 3,
                    StomachSize = 12,
                    HasTail = false,
                    FavoredStat = Stat.Dexterity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
                    ExpMultiplier = 1.0f,
                    PowerAdjustment = 1.2f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(4, 6),
                        Dexterity = new RaceStats.StatRange(12, 15),
                        Endurance = new RaceStats.StatRange(6, 8),
                        Mind = new RaceStats.StatRange(20, 24),
                        Will = new RaceStats.StatRange(6, 10),
                        Agility = new RaceStats.StatRange(8, 12),
                        Voracity = new RaceStats.StatRange(6, 10),
                        Stomach = new RaceStats.StatRange(8, 10),
                    },
                    RacialTraits = new List<Traits>()
                    {
                        Traits.AcidResistant,
                        Traits.BornToMove,
                        Traits.SlowDigestion
                    },
                    RaceDescription = "Tiny insects grown to a slightly larger size, the Ants still wouldn't be considered dangerous were it not for their ability to swallow and carry things hundred times their own size."
                });
                output.CanBeGender = new List<Gender> { Gender.None };

                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.Ant);
                output.GentleAnimation = true;
            });

            builder.RenderSingle(SpriteType.Head, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Ant, input.U.SkinColor));
                if (input.A.IsOralVoring || input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Ant[1]);
                    return;
                }

                output.Sprite(input.Sprites.Ant[0]);
            }); // Head

            builder.RenderSingle(SpriteType.Belly, 0, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Ant, input.U.SkinColor));
                if (input.U.Predator == false)
                {
                    output.Sprite(input.Sprites.Ant[2]);
                    return;
                }

                int size = input.A.GetStomachSize(16, 0.75f);

                output.Sprite(input.Sprites.Ant[2 + input.A.GetStomachSize()]);
            }); // Belly

            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(Defaults.RandomCustom);
        });
    }
}
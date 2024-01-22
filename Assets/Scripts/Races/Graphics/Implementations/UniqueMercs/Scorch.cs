#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.UniqueMercs
{
    internal static class Scorch
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Scorch", "Scorch");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 24,
                    StomachSize = 30,
                    HasTail = true,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
                    ExpMultiplier = 2.4f,
                    PowerAdjustment = 4f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(20, 24),
                        Dexterity = new RaceStats.StatRange(6, 10),
                        Endurance = new RaceStats.StatRange(16, 24),
                        Mind = new RaceStats.StatRange(16, 20),
                        Will = new RaceStats.StatRange(12, 18),
                        Agility = new RaceStats.StatRange(16, 28),
                        Voracity = new RaceStats.StatRange(16, 24),
                        Stomach = new RaceStats.StatRange(16, 24),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Flight,
                        TraitType.Biter,
                        TraitType.FastAbsorption
                    },
                    InnateSpells = new List<SpellTypes>() { SpellTypes.Pyre },
                    RaceDescription = "A cruel gluttonous red wyvern",
                });
                output.CanBeGender = new List<Gender> { Gender.Male };
                output.GentleAnimation = true;
            });


            builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Scorch[input.A.IsAttacking || input.A.IsOralVoring ? 1 : 0]);
            });
            builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Scorch[2]);
            });
            builder.RenderSingle(SpriteType.Belly, 2, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.HasBelly == false)
                {
                    return;
                }

                output.Sprite(input.A.HasBelly ? input.Sprites.Scorch[3 + input.A.GetStomachSize(3)] : null);
            });

            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                Unit unit = data.Unit;

                unit.Name = "Scorch";
            });
        });
    }
}
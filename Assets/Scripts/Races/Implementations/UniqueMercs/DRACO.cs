#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.UniqueMercs
{
    internal static class Draco
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup((input, output) =>
            {
                output.Names("D.R.A.C.O.", "D.R.A.C.O.");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 22,
                    StomachSize = 30,
                    HasTail = true,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
                    ExpMultiplier = 2.4f,
                    PowerAdjustment = 4f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new StatRange(20, 24),
                        Dexterity = new StatRange(6, 10),
                        Endurance = new StatRange(16, 24),
                        Mind = new StatRange(16, 20),
                        Will = new StatRange(12, 18),
                        Agility = new StatRange(16, 28),
                        Voracity = new StatRange(16, 24),
                        Stomach = new StatRange(18, 26),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.MetalBody,
                        TraitType.KeenReflexes,
                        TraitType.BornToMove,
                        TraitType.Intimidating,
                    },
                    RaceDescription = "A corrupted D.r.a.c.o unit. Unlike other units from his line 008 has tampered with his coding and removed the safety on his stomach allowing him to digest his prisoners.",
                });
                output.CanBeGender = new List<Gender> { Gender.Male };
                output.GentleAnimation = true;
                output.ClothingColors = 0;
            });


            builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking || input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Draco[3]);
                    return;
                }

                output.Sprite(input.Sprites.Draco[2]);
            });

            builder.RenderSingle(SpriteType.Body, 5, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Draco[0]);
            });
            builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Draco[1]);
            });
            builder.RenderSingle(SpriteType.Belly, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.HasBelly == false)
                {
                    return;
                }

                output.Sprite(input.A.HasBelly ? input.Sprites.Draco[5 + input.A.GetStomachSize(4)] : null);
            });

            builder.RunBefore(Defaults.Finalize);

            builder.RandomCustom((data, output) =>   
            {
                Defaults.Randomize(data, output);
                IUnitRead unit = data.Unit;

                unit.Name = "DRACO";
            });
        });
    }
}
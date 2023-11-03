#region

using System.Collections.Generic;

#endregion

internal static class DRACO
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Names("D.R.A.C.O.", "D.R.A.C.O.");
        builder.RaceTraits(new RaceTraits()
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
                Strength = new RaceStats.StatRange(20, 24),
                Dexterity = new RaceStats.StatRange(6, 10),
                Endurance = new RaceStats.StatRange(16, 24),
                Mind = new RaceStats.StatRange(16, 20),
                Will = new RaceStats.StatRange(12, 18),
                Agility = new RaceStats.StatRange(16, 28),
                Voracity = new RaceStats.StatRange(16, 24),
                Stomach = new RaceStats.StatRange(18, 26),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.MetalBody,
                Traits.KeenReflexes,
                Traits.BornToMove,
                Traits.Intimidating,
            },
            RaceDescription = "A corrupted D.r.a.c.o unit. Unlike other units from his line 008 has tampered with his coding and removed the safety on his stomach allowing him to digest his prisoners.",
        });
        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.Male };
            output.GentleAnimation = true;
            output.ClothingColors = 0;
        });


        builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.A.IsAttacking || input.A.IsOralVoring)
            {
                output.Sprite(input.Sprites.DRACO[3]);
                return;
            }

            output.Sprite(input.Sprites.DRACO[2]);
        });

        builder.RenderSingle(SpriteType.Body, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.DRACO[0]);
        });
        builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(input.Sprites.DRACO[1]);
        });
        builder.RenderSingle(SpriteType.Belly, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.A.HasBelly == false)
            {
                return;
            }

            output.Sprite(input.A.HasBelly ? input.Sprites.DRACO[5 + input.A.GetStomachSize(4)] : null);
        });

        builder.RunBefore(Defaults.Finalize);

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;

            unit.Name = "DRACO";
        });
    });
}
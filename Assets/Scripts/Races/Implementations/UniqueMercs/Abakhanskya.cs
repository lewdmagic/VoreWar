#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.UniqueMercs
{
    internal static class Abakhanskya
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Abakhanskya", "Abakhanskya");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 90,
                    StomachSize = 40,
                    HasTail = true,
                    FavoredStat = Stat.Stomach,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth },
                    ExpMultiplier = 3.2f,
                    PowerAdjustment = 7f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new StatRange(24, 28),
                        Dexterity = new StatRange(18, 22),
                        Endurance = new StatRange(36, 42),
                        Mind = new StatRange(12, 16),
                        Will = new StatRange(16, 20),
                        Agility = new StatRange(20, 24),
                        Voracity = new StatRange(16, 20),
                        Stomach = new StatRange(24, 28),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.ForcefulBlow,
                        TraitType.StrongGullet,
                        TraitType.Pounce,
                        TraitType.HeavyPounce,
                    },
                    RaceDescription = "This girthy dragoness hails from a far away arid land, and excels at pressing the attack, with a great pair of skewers in place of where most dragons would have wings. With considerable grace despite her size, she exercises vigilance on the battlefield. ",
                });

                output.CanBeGender = new List<Gender> { Gender.Female };
            });


            builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Abakhanskya[2]);
                    return;
                }

                if (input.A.HasBelly)
                {
                    output.Sprite(input.Sprites.Abakhanskya[3]);
                }
            });

            builder.RenderSingle(SpriteType.Body, 5, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Abakhanskya[1]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 0, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Abakhanskya[5]);
            });

            builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Abakhanskya[4]);
            });

            builder.RenderSingle(SpriteType.BodyAccent3, 2, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Abakhanskya[6]);
            });

            builder.RenderSingle(SpriteType.BodyAccent4, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                var sprite = 7 + input.A.GetStomachSize(5);

                output.Sprite(input.Sprites.Abakhanskya[sprite]);
            });

            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                IUnitRead unit = data.Unit;

                unit.Name = "Abakhanskya";
            });
        });
    }
}
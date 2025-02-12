﻿#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class TwistedVines
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup((input, output) =>
            {
                output.Names("Twisted Vine", "Twisted Vines");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 8,
                    StomachSize = 11,
                    HasTail = false,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
                    ExpMultiplier = 1.2f,
                    PowerAdjustment = 1.4f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new StatRange(10, 16),
                        Dexterity = new StatRange(6, 8),
                        Endurance = new StatRange(8, 14),
                        Mind = new StatRange(4, 6),
                        Will = new StatRange(6, 12),
                        Agility = new StatRange(4, 8),
                        Voracity = new StatRange(8, 15),
                        Stomach = new StatRange(8, 13),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Tempered,
                        TraitType.SlowDigestion
                    },
                    RaceDescription = ""
                });
                output.CanBeGender = new List<Gender> { Gender.None };
                output.EyeTypes = 2;
            });


            builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                int headType = input.U.EyeType * 2;
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    headType++;
                }

                output.Sprite(input.Sprites.Plant[headType]);
            });

            builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Plant[4]);
            });
            builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Plant[5]);
            });

            builder.RenderSingle(SpriteType.Belly, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.HasBelly == false)
                {
                    return;
                }

                output.Sprite(input.Sprites.Plant[6 + input.A.GetStomachSize(8)]);
            });

            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(Defaults.Randomize);
        });
    }
}
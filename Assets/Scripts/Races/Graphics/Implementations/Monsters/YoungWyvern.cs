#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class YoungWyvern
    {
        internal const float StomachGainDivisor = 1.2f; //Higher is faster, should be balanced with stomach size to max out at 80-100 capacity

        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Young Wyvern", "Young Wyverns");
                output.BonesInfo((unit) => new List<BoneInfo>()
                {
                    new BoneInfo(BoneType.YoungWyvern, unit.Name)
                });
                output.FlavorText(new FlavorText(
                    new Texts { "plumb", "soft scaled", "stretchy" },
                    new Texts { "grinning", "expansive", "rubbery" },
                    new Texts { "young wyvern", "wyverling", "small wyvern" },
                    "Beak"
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 6,
                    StomachSize = 20,
                    HasTail = true,
                    FavoredStat = Stat.Stomach,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
                    ExpMultiplier = 1.25f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(4, 8),
                        Dexterity = new RaceStats.StatRange(6, 14),
                        Endurance = new RaceStats.StatRange(8, 16),
                        Mind = new RaceStats.StatRange(6, 14),
                        Will = new RaceStats.StatRange(4, 12),
                        Agility = new RaceStats.StatRange(10, 22),
                        Voracity = new RaceStats.StatRange(12, 20),
                        Stomach = new RaceStats.StatRange(8, 14),
                    },
                    RacialTraits = new List<TraitType>()
                    {

                    },
                    RaceDescription = "When young the Wyverns aren't good predators. They do have a healthy appetite though, and follow older Wyverns in hope of getting the better of prey weakened by the adults.",

                });
                output.IndividualNames(new List<string>
                {
                    "Smallwing",
                    "Greedytalon",
                    "Widebeak",
                    "Lazyback",
                });
                output.CanBeGender = new List<Gender> { Gender.None };
                output.GentleAnimation = true;
                output.SkinColors = ColorMap.SlimeColorCount;
                output.AccessoryColors = ColorMap.SlimeColorCount;
            });


            builder.RenderSingle(SpriteType.Mouth, 10, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.A.PredatorComponent?.VisibleFullness >= 5 / StomachGainDivisor && (input.A.IsEating || input.A.IsAttacking) == false ? input.Sprites.YoungWyvern[17] : null);
            });
            builder.RenderSingle(SpriteType.Body, 3, (input, output) =>
            {
                output.Coloring(ColorMap.GetSlimeColor(input.U.SkinColor));
                if (input.A.IsEating || input.A.IsAttacking)
                {
                    if (input.A.PredatorComponent?.VisibleFullness < 0.5f / StomachGainDivisor)
                    {
                        output.Sprite(input.Sprites.YoungWyvern[1]);
                        return;
                    }

                    if (input.A.PredatorComponent?.VisibleFullness < 1.75f / StomachGainDivisor)
                    {
                        output.Sprite(input.Sprites.YoungWyvern[5]);
                        return;
                    }

                    if (input.A.PredatorComponent?.VisibleFullness < 3 / StomachGainDivisor)
                    {
                        output.Sprite(input.Sprites.YoungWyvern[8]);
                        return;
                    }

                    if (input.A.PredatorComponent?.VisibleFullness < 4 / StomachGainDivisor)
                    {
                        output.Sprite(input.Sprites.YoungWyvern[11]);
                        return;
                    }

                    if (input.A.PredatorComponent?.VisibleFullness < 5 / StomachGainDivisor)
                    {
                        output.Sprite(input.Sprites.YoungWyvern[13]);
                        return;
                    }

                    output.Sprite(input.Sprites.YoungWyvern[15]);
                    return;
                }

                if (input.A.HasBelly == false)
                {
                    output.Sprite(input.Sprites.YoungWyvern[0]);
                    return;
                }

                if (input.A.PredatorComponent?.VisibleFullness < 0.5f / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[2]);
                    return;
                }

                if (input.A.PredatorComponent?.VisibleFullness < 1.75f / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[4]);
                    return;
                }

                if (input.A.PredatorComponent?.VisibleFullness < 3 / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[7]);
                    return;
                }

                if (input.A.PredatorComponent?.VisibleFullness < 4 / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[10]);
                    return;
                }

                if (input.A.PredatorComponent?.VisibleFullness < 5 / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[12]);
                    return;
                }

                output.Sprite(input.Sprites.YoungWyvern[14]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
            {
                output.Coloring(ColorMap.GetSlimeColor(input.U.AccessoryColor));
                if (input.A.PredatorComponent?.VisibleFullness < 0.5f / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[3]).Layer(5).Coloring(ColorMap.GetSlimeColor(input.U.AccessoryColor));
                    return;
                }

                output.Coloring(ColorMap.GetSlimeColor(input.U.SkinColor));
                output.Layer(0);
                if (input.A.PredatorComponent?.VisibleFullness < 1.75f / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[29]);
                    return;
                }

                if (input.A.PredatorComponent?.VisibleFullness < 3 / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[6]);
                    return;
                }

                if (input.A.PredatorComponent?.VisibleFullness < 4 / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[9]);
                    return;
                }

                if (input.A.PredatorComponent?.VisibleFullness >= 5 / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[16]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 2, (input, output) =>
            {
                output.Coloring(ColorMap.GetSlimeColor(input.U.AccessoryColor));
                if (input.A.HasBelly == false)
                {
                    return;
                }

                output.Layer(8);
                if (input.A.PredatorComponent.VisibleFullness < 0.2 / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[18]);
                    return;
                }

                if (input.A.PredatorComponent.VisibleFullness < 0.5 / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[19]);
                    return;
                }

                if (input.A.PredatorComponent.VisibleFullness < 1.2 / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[20]);
                    return;
                }

                if (input.A.PredatorComponent.VisibleFullness < 1.75 / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[21]);
                    return;
                }

                if (input.A.PredatorComponent.VisibleFullness < 2.5 / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[22]);
                    return;
                }

                if (input.A.PredatorComponent.VisibleFullness < 3 / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[23]);
                    return;
                }

                if (input.A.PredatorComponent.VisibleFullness < 3.5 / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[24]);
                    return;
                }

                output.Layer(2);

                if (input.A.PredatorComponent.VisibleFullness < 4 / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[25]);
                    return;
                }

                if (input.A.PredatorComponent.VisibleFullness < 4.5 / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[26]);
                    return;
                }

                if (input.A.PredatorComponent.VisibleFullness < 5 / StomachGainDivisor)
                {
                    output.Sprite(input.Sprites.YoungWyvern[27]);
                    return;
                }

                output.Sprite(input.Sprites.YoungWyvern[28]);
            });

            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(Defaults.RandomCustom);
        });
    }
}
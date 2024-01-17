#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class Schiwardez
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Schiwardez", "Schiwardez");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 10,
                    StomachSize = 10,
                    HasTail = true,
                    FavoredStat = Stat.Endurance,
                    AllowedVoreTypes = new List<VoreType> { VoreType.CockVore },
                    ExpMultiplier = 1.3f,
                    PowerAdjustment = 1.6f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(10, 14),
                        Dexterity = new RaceStats.StatRange(8, 12),
                        Endurance = new RaceStats.StatRange(12, 16),
                        Mind = new RaceStats.StatRange(4, 6),
                        Will = new RaceStats.StatRange(8, 12),
                        Agility = new RaceStats.StatRange(10, 14),
                        Voracity = new RaceStats.StatRange(10, 14),
                        Stomach = new RaceStats.StatRange(10, 14),
                    },
                    RacialTraits = new List<Traits>()
                    {
                        Traits.Resilient,
                        Traits.Disgusting
                    },
                    RaceDescription = "A tough, twisted creature. Hunts for pleasure rather than sustenance.",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.Skintone, "Body Color");
                });
                output.GentleAnimation = true;
                output.CanBeGender = new List<Gender> { Gender.Male };
                output.SkinColors = ColorMap.SchiwardezColorCount;
            });


            builder.RenderSingle(SpriteType.Head, 8, (input, output) =>
            {
                output.Coloring(ColorMap.GetSchiwardezColor(input.U.SkinColor));
                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Schiwardez[37]);
                    return;
                }

                if (input.A.GetBallSize(24) > 0)
                {
                    output.Sprite(input.Sprites.Schiwardez[5]);
                    return;
                }

                output.Sprite(input.Sprites.Schiwardez[4]);
            }); // Head       

            builder.RenderSingle(SpriteType.Body, 5, (input, output) =>
            {
                output.Coloring(ColorMap.GetSchiwardezColor(input.U.SkinColor));
                if (input.A.GetBallSize(24) > 17)
                {
                    output.Sprite(input.Sprites.Schiwardez[1]);
                    return;
                }

                output.Sprite(input.Sprites.Schiwardez[0]);
            }); // Body

            builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
            {
                output.Coloring(ColorMap.GetSchiwardezColor(input.U.SkinColor));
                output.Sprite(input.Sprites.Schiwardez[3]);
            }); // Closer Legs

            builder.RenderSingle(SpriteType.BodyAccent2, 0, (input, output) =>
            {
                output.Coloring(ColorMap.GetSchiwardezColor(input.U.SkinColor));
                output.Sprite(input.Sprites.Schiwardez[2]);
            }); // Far Legs

            builder.RenderSingle(SpriteType.BodyAccent3, 2, (input, output) =>
            {
                output.Coloring(ColorMap.GetSchiwardezColor(input.U.SkinColor));
                output.Sprite(input.Sprites.Schiwardez[8]);
            }); // Sheath

            builder.RenderSingle(SpriteType.BodyAccent4, 6, (input, output) =>
            {
                output.Coloring(ColorMap.GetSchiwardezColor(input.U.SkinColor));
                if (input.A.GetBallSize(24) > 17)
                {
                    output.Sprite(input.Sprites.Schiwardez[36]);
                    return;
                }

                if (input.A.GetBallSize(24) > 14)
                {
                    output.Sprite(input.Sprites.Schiwardez[35]);
                    return;
                }

                if (input.A.GetBallSize(24) > 12)
                {
                    output.Sprite(input.Sprites.Schiwardez[34]);
                    return;
                }

                output.Sprite(input.Sprites.Schiwardez[33]);
            }); // Tail

            builder.RenderSingle(SpriteType.BodyAccent5, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Schiwardez[38]);
                }
            }); // Mouth

            builder.RenderSingle(SpriteType.Dick, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsCockVoring)
                {
                    output.Sprite(input.Sprites.Schiwardez[7]);
                    return;
                }

                if (input.A.IsErect())
                {
                    output.Sprite(input.Sprites.Schiwardez[6]);
                }
            }); // Dick

            builder.RenderSingle(SpriteType.Balls, 1, (input, output) =>
            {
                output.Coloring(ColorMap.GetSchiwardezColor(input.U.SkinColor));
                if (input.A.GetBallSize(24) == 0 && Config.HideCocks == false)
                {
                    output.Sprite(input.Sprites.Schiwardez[9]);
                    return;
                }

                int size = input.A.GetBallSize(24);

                if (size > 18)
                {
                    size = 18;
                }

                output.Sprite(input.Sprites.Schiwardez[8 + size]);
            }); // Balls


            builder.RunBefore((input, output) =>
            {
                output.ChangeSprite(SpriteType.Balls).AddOffset(-125 * .5f, 0);
            });
            builder.RandomCustom(Defaults.RandomCustom);
        });
    }
}
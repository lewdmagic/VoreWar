#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class Voilin
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Voilin", "Voilins");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 6,
                    StomachSize = 10,
                    HasTail = true,
                    FavoredStat = Stat.Stomach,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
                    ExpMultiplier = 1.1f,
                    PowerAdjustment = 1.5f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(8, 12),
                        Dexterity = new RaceStats.StatRange(8, 12),
                        Endurance = new RaceStats.StatRange(10, 14),
                        Mind = new RaceStats.StatRange(4, 6),
                        Will = new RaceStats.StatRange(6, 10),
                        Agility = new RaceStats.StatRange(12, 16),
                        Voracity = new RaceStats.StatRange(8, 12),
                        Stomach = new RaceStats.StatRange(8, 12),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Resilient,
                        TraitType.Disgusting
                    },
                    RaceDescription = "This small creature is the basic grunt of the Mass, a disposable, nearly mindless slave to throw at potential prey to tire them down for worthier beings to devour.",
                });
                output.EyeTypes = 3;
                output.SkinColors = ColorMap.ExoticColorCount; // Under belly, head
                output.EyeColors = ColorMap.EyeColorCount; // Eye & Spine Colour
                output.ExtraColors1 = ColorMap.ExoticColorCount; // Plates
                output.ExtraColors2 = ColorMap.ExoticColorCount; // Limbs
                output.CanBeGender = new List<Gender> { Gender.None };
                output.GentleAnimation = true;
            });


            builder.RenderSingle(SpriteType.Eyes, 6, (input, output) =>
            {
                output.Coloring(ColorMap.GetEyeColor(input.U.EyeColor));
                output.Sprite(input.Sprites.Voilin[9 + input.U.EyeType]);
            }); // Eyes, Spines

            builder.RenderSingle(SpriteType.Hair, 5, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Voilin[13]);
                    return;
                }

                output.Sprite(input.Sprites.Voilin[12]);
            }); // Teeth

            builder.RenderSingle(SpriteType.Body, 0, (input, output) =>
            {
                output.Coloring(ColorMap.GetExoticColor(input.U.ExtraColor2));
                int size = input.A.GetStomachSize(23);

                if (size > 11)
                {
                    output.Sprite(input.Sprites.Voilin[6]);
                    return;
                }

                if (size > 8)
                {
                    output.Sprite(input.Sprites.Voilin[3]);
                    return;
                }

                output.Sprite(input.Sprites.Voilin[0]);
            }); // Base Body

            builder.RenderSingle(SpriteType.BodyAccent, 2, (input, output) =>
            {
                output.Coloring(ColorMap.GetExoticColor(input.U.ExtraColor2));
                int size = input.A.GetStomachSize(23);

                if (size > 11)
                {
                    if (input.A.IsAttacking || input.A.IsEating)
                    {
                        output.Sprite(input.Sprites.Voilin[8]);
                        return;
                    }

                    output.Sprite(input.Sprites.Voilin[7]);
                    return;
                }

                if (size > 8)
                {
                    if (input.A.IsAttacking || input.A.IsEating)
                    {
                        output.Sprite(input.Sprites.Voilin[5]);
                        return;
                    }

                    output.Sprite(input.Sprites.Voilin[4]);
                    return;
                }

                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Voilin[2]);
                    return;
                }

                output.Sprite(input.Sprites.Voilin[1]);
            }); // Closer Legs, Head

            builder.RenderSingle(SpriteType.BodyAccent2, 3, (input, output) =>
            {
                output.Coloring(ColorMap.GetExoticColor(input.U.ExtraColor1));
                output.Sprite(input.Sprites.Voilin[14]);
            }); // Back Plates

            builder.RenderSingle(SpriteType.BodyAccent3, 4, (input, output) =>
            {
                output.Coloring(ColorMap.GetExoticColor(input.U.SkinColor));
                int size = input.A.GetStomachSize(23);

                if (size > 11)
                {
                    if (input.A.IsAttacking || input.A.IsEating)
                    {
                        output.Sprite(input.Sprites.Voilin[21]);
                        return;
                    }

                    output.Sprite(input.Sprites.Voilin[20]);
                    return;
                }

                if (size > 8)
                {
                    if (input.A.IsAttacking || input.A.IsEating)
                    {
                        output.Sprite(input.Sprites.Voilin[19]);
                        return;
                    }

                    output.Sprite(input.Sprites.Voilin[18]);
                    return;
                }

                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Voilin[16]);
                    return;
                }

                output.Sprite(input.Sprites.Voilin[15]);
            }); // Below Head

            builder.RenderSingle(SpriteType.Belly, 1, (input, output) =>
            {
                output.Coloring(ColorMap.GetExoticColor(input.U.SkinColor));
                int size = input.A.GetStomachSize(23);

                if (size == 0)
                {
                    return;
                }

                if (size > 15)
                {
                    size = 15;
                }

                output.Sprite(input.Sprites.Voilin[21 + size]);
            }); // Belly       


            builder.RunBefore((input, output) =>
            {
                int size = input.A.GetStomachSize(23);
                if (size > 14)
                {
                    output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, (29 + (size - 15) * 2) * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, (12 + (size - 15) * 2) * .625f);
                    output.ChangeSprite(SpriteType.Hair).AddOffset(0, (29 + (size - 15) * 2) * .625f);
                    output.ChangeSprite(SpriteType.Eyes).AddOffset(0, (29 + (size - 15) * 2) * .625f);
                    output.ChangeSprite(SpriteType.Body).AddOffset(0, (12 + (size - 15) * 2) * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, (12 + (size - 15) * 2) * .625f);
                }

                else if (size > 11)
                {
                    output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, (17 + (size - 12) * 4) * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, (size - 12) * 4 * .625f);
                    output.ChangeSprite(SpriteType.Hair).AddOffset(0, (17 + (size - 12) * 4) * .625f);
                    output.ChangeSprite(SpriteType.Eyes).AddOffset(0, (17 + (size - 12) * 4) * .625f);
                    output.ChangeSprite(SpriteType.Body).AddOffset(0, (size - 12) * 4 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, (size - 12) * 4 * .625f);
                }

                else if (size > 8)
                {
                    output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 10 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 0 * .625f);
                    output.ChangeSprite(SpriteType.Hair).AddOffset(0, 10 * .625f);
                    output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 10 * .625f);
                    output.ChangeSprite(SpriteType.Body).AddOffset(0, 0 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 0 * .625f);
                }

                else
                {
                    output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, 0 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent3).AddOffset(0, 0 * .625f);
                    output.ChangeSprite(SpriteType.Hair).AddOffset(0, 0 * .625f);
                    output.ChangeSprite(SpriteType.Eyes).AddOffset(0, 0 * .625f);
                    output.ChangeSprite(SpriteType.Body).AddOffset(0, 0 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, 0 * .625f);
                }
            });
            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                IUnitRead unit = data.Unit;

                unit.SkinColor = State.Rand.Next(0, data.SetupOutput.SkinColors);
                unit.EyeColor = State.Rand.Next(0, data.SetupOutput.EyeColors);
                unit.ExtraColor1 = State.Rand.Next(0, data.SetupOutput.ExtraColors1);
                unit.ExtraColor2 = State.Rand.Next(0, data.SetupOutput.ExtraColors2);
            });
        });
    }
}
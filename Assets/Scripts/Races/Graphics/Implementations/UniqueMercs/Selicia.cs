#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.UniqueMercs
{
    internal static class Selicia
    {
        private const float BellyScale = 0.9f;

        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Selicia", "Selicia");
                output.FlavorText(new FlavorText(
                    new Texts { "mighty tasty", "smooth scaled", "huge", "flexible", "formerly mighty", "surprisingly edible" },
                    new Texts { "wide mawed", "smooth scaled", "stretchy", "huge", "impressive", "all-too-eager", "mighty" },
                    new Texts { "dragon", "salamander dragon", "derg" },
                    "Claws"
                ));
                output.BonesInfo((unit) => new List<BoneInfo>()
                {
                    new BoneInfo(BoneType.WyvernBonesWithoutHead, unit.Name),
                    new BoneInfo(BoneType.SeliciaSkull, unit.Name)
                });
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 60,
                    StomachSize = 30,
                    HasTail = true,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth },
                    ExpMultiplier = 4f,
                    PowerAdjustment = 7f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(22, 26),
                        Dexterity = new RaceStats.StatRange(10, 14),
                        Endurance = new RaceStats.StatRange(30, 36),
                        Mind = new RaceStats.StatRange(16, 20),
                        Will = new RaceStats.StatRange(6, 8),
                        Agility = new RaceStats.StatRange(20, 24),
                        Voracity = new RaceStats.StatRange(14, 18),
                        Stomach = new RaceStats.StatRange(12, 16),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.KeenReflexes,
                        TraitType.StrongGullet,
                        TraitType.NimbleClimber,
                    },
                    InnateSpells = new List<SpellType>() { SpellType.IceBlast },
                    RaceDescription = "A hybrid between a dragon and salamander whom excels in climbing and swimming but lacks any wings for flight.",
                });
                output.CanBeGender = new List<Gender> { Gender.Female };
                output.GentleAnimation = true;
            });


            builder.RenderSingle(SpriteType.Head, 10, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking || input.A.IsUnbirthing)
                {
                    output.Sprite(input.Sprites.Selicia[7]);
                    return;
                }

                if (input.A.IsOralVoring)
                {
                    output.Sprite(input.Sprites.Selicia[8]);
                    return;
                }

                if (input.A.GetStomachSize(14, BellyScale) < 5)
                {
                    output.Sprite(input.Sprites.Selicia[6]);
                    return;
                }

                if (State.Rand.Next(450) == 0)
                {
                    input.A.SetAnimationMode(1, .75f);
                }

                int specialMode = input.A.CheckAnimationFrame();
                if (specialMode == 1)
                {
                    output.Sprite(input.Sprites.Selicia[9]);
                    return;
                }

                output.Sprite(input.Sprites.Selicia[7]);
            });

            builder.RenderSingle(SpriteType.Body, 5, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                int size = input.A.GetStomachSize(14, BellyScale);
                output.Layer(6);
                if (size >= 12)
                {
                    output.Sprite(input.Sprites.Selicia[5]).Layer(3);
                    return;
                }

                if (size >= 5)
                {
                    output.Sprite(input.Sprites.Selicia[2]);
                    return;
                }

                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Selicia[2]);
                    return;
                }

                output.Sprite(input.Sprites.Selicia[1]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                int size = input.A.GetStomachSize(14, BellyScale);
                if (input.A.GetStomachSize(14, BellyScale) < 5)
                {
                    if (input.A.IsAttacking || input.A.IsEating)
                    {
                        output.Sprite(input.Sprites.Selicia[3]).Layer(7);
                        return;
                    }

                    output.Sprite(input.Sprites.Selicia[0]).Layer(1);
                    return;
                }

                if (size < 9)
                {
                    output.Sprite(input.Sprites.Selicia[3]).Layer(7);
                    return;
                }

                if (size < 12)
                {
                    output.Sprite(input.Sprites.Selicia[4]).Layer(7);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent2, 2, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.HasBelly)
                {
                    if (input.A.IsAttacking || input.A.IsEating || input.A.GetStomachSize(14, BellyScale) >= 5)
                    {
                        return;
                    }

                    output.Sprite(input.Sprites.Selicia[40 + input.A.GetStomachSize(14, BellyScale)]);
                }
            });

            builder.RenderSingle(SpriteType.BodySize, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.GetStomachSize(14, BellyScale) < 5)
                {
                    output.Sprite(input.Sprites.Selicia[45]);
                }
            });

            builder.RenderSingle(SpriteType.BreastShadow, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.GetStomachSize(14, BellyScale) >= 12)
                {
                    output.Sprite(input.Sprites.Selicia[34]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.HasBelly)
                {
                    if (input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb))
                    {
                        output.Sprite(input.Sprites.Selicia[33]);
                        return;
                    }

                    if (input.A.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb))
                    {
                        if (input.A.GetStomachSize(14, BellyScale * 0.7f) == 14)
                        {
                            output.Sprite(input.Sprites.Selicia[32]);
                            return;
                        }

                        if (input.A.GetStomachSize(14, BellyScale * 0.8f) == 14)
                        {
                            output.Sprite(input.Sprites.Selicia[31]);
                            return;
                        }

                        if (input.A.GetStomachSize(14, BellyScale * 0.9f) == 14)
                        {
                            output.Sprite(input.Sprites.Selicia[30]);
                            return;
                        }
                    }

                    if (input.A.IsAttacking || input.A.IsEating || input.A.GetStomachSize(14, BellyScale) >= 5)
                    {
                        output.Sprite(input.Sprites.Selicia[15 + input.A.GetStomachSize(14, BellyScale)]);
                        return;
                    }

                    output.Sprite(input.Sprites.Selicia[10 + input.A.GetStomachSize(14, BellyScale)]);
                }
            });


            builder.RunBefore((input, output) =>
            {
                int size = input.A.GetStomachSize(14, BellyScale);
                const float defaultYOffset = 20 * .625f;
                if (size == 14)
                {
                    output.ChangeSprite(SpriteType.Body).AddOffset(0, defaultYOffset + 0);
                    output.ChangeSprite(SpriteType.Head).AddOffset(16 * .625f, defaultYOffset + 16 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, defaultYOffset + 0);
                    output.ChangeSprite(SpriteType.BreastShadow).AddOffset(0, defaultYOffset + 0);
                    output.ChangeSprite(SpriteType.Belly).AddOffset(0, defaultYOffset);
                    output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, defaultYOffset);
                    output.ChangeSprite(SpriteType.BodySize).AddOffset(0, defaultYOffset);
                }
                else if (size == 13)
                {
                    output.ChangeSprite(SpriteType.Body).AddOffset(0, defaultYOffset + -8 * .625f);
                    output.ChangeSprite(SpriteType.Head).AddOffset(16 * .625f, defaultYOffset + 8 * .625f);
                    output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, defaultYOffset + -8 * .625f);
                    output.ChangeSprite(SpriteType.BreastShadow).AddOffset(0, defaultYOffset + -8 * .625f);
                    output.ChangeSprite(SpriteType.Belly).AddOffset(0, defaultYOffset);
                    output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, defaultYOffset);
                    output.ChangeSprite(SpriteType.BodySize).AddOffset(0, defaultYOffset);
                }
                else if (size == 12)
                {
                    output.ChangeSprite(SpriteType.Body).AddOffset(0, defaultYOffset + -16 * .625f);
                    output.ChangeSprite(SpriteType.Head).AddOffset(16 * .625f, defaultYOffset + 0);
                    output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, defaultYOffset + -16 * .625f);
                    output.ChangeSprite(SpriteType.BreastShadow).AddOffset(0, defaultYOffset + -16 * .625f);
                    output.ChangeSprite(SpriteType.Belly).AddOffset(0, defaultYOffset);
                    output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, defaultYOffset);
                    output.ChangeSprite(SpriteType.BodySize).AddOffset(0, defaultYOffset);
                }
                else
                {
                    output.ChangeSprite(SpriteType.Body).AddOffset(0, defaultYOffset);
                    output.ChangeSprite(SpriteType.Head).AddOffset(0, defaultYOffset);
                    output.ChangeSprite(SpriteType.BodyAccent).AddOffset(0, defaultYOffset);
                    output.ChangeSprite(SpriteType.BreastShadow).AddOffset(0, defaultYOffset);
                    output.ChangeSprite(SpriteType.Belly).AddOffset(0, defaultYOffset);
                    output.ChangeSprite(SpriteType.BodyAccent2).AddOffset(0, defaultYOffset);
                    output.ChangeSprite(SpriteType.BodySize).AddOffset(0, defaultYOffset);
                }
            });

            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                IUnitRead unit = data.Unit;

                unit.Name = "Selicia";
            });
        });
    }
}
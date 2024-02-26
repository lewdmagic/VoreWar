#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    /// <summary>
    ///     Is a skyshark.
    /// </summary>
    internal static class FeralSharks
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Feral Shark", "Feral Sharks");
                output.BonesInfo((unit) => new List<BoneInfo>()
                {
                    new BoneInfo(BoneType.Shark, unit.Name)
                });
                output.FlavorText(new FlavorText(
                    new Texts { "finned", "torpedo shaped", "chompy" },
                    new Texts { "large jawed", "rough scaled", "sharp finned" },
                    new Texts { "skyshark", "shark", "great fish" },
                    "Jaws"
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 16,
                    StomachSize = 20,
                    HasTail = true,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
                    ExpMultiplier = 1.75f,
                    PowerAdjustment = 1.75f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new StatRange(8, 20),
                        Dexterity = new StatRange(4, 8),
                        Endurance = new StatRange(14, 24),
                        Mind = new StatRange(6, 10),
                        Will = new StatRange(6, 12),
                        Agility = new StatRange(8, 16),
                        Voracity = new StatRange(14, 22),
                        Stomach = new StatRange(6, 12),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Flight,
                        TraitType.Biter
                    },
                    RaceDescription = "When the Scylla left their old realm the creatures that used to hunt them were left hungry. The Sharks, with their strong sense of smell, were able to track down the portals the Scylla used and followed close behind.",
                });
                output.CanBeGender = new List<Gender> { Gender.None };
                output.SkinColors = ColorMap.SharkColorCount;
                output.AccessoryColors = ColorMap.SharkBellyColorCount;
                output.ExtraColors1 = ColorMap.SlimeColorCount;
                output.EyeTypes = 3;
                output.SpecialAccessoryCount = 2;
            });


            builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
            {
                output.Coloring(ColorMap.GetSlimeColor(input.U.ExtraColor1));
                if (input.U.Level > 15)
                {
                    output.Sprite(input.Sprites.Shark[29]);
                }
            });

            builder.RenderSingle(SpriteType.Eyes, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Shark[9 + input.U.EyeType]);
            });
            builder.RenderSingle(SpriteType.Mouth, 5, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsEating || input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Shark[8]);
                }
            });
            builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
            {
                output.Coloring(ColorMap.GetSharkColor(input.U.SkinColor));
                if (input.A.IsEating || input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Shark[2]);
                    return;
                }

                output.Sprite(input.Sprites.Shark[0]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
            {
                output.Coloring(ColorMap.GetSharkBellyColor(input.U.AccessoryColor));
                if (input.A.IsEating || input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Shark[3]);
                    return;
                }

                output.Sprite(input.Sprites.Shark[1]);
            });

            builder.RenderSingle(SpriteType.BodyAccent2, 0, (input, output) =>
            {
                output.Coloring(ColorMap.GetSharkColor(input.U.SkinColor));
                if (!input.A.Targetable)
                {
                    output.Sprite(input.Sprites.Shark[4 + input.U.SpecialAccessoryType]);
                    return;
                }

                if (State.Rand.Next(200) == 0)
                {
                    input.A.SetAnimationMode(1, 1.5f);
                }

                int specialMode = input.A.CheckAnimationFrame();
                if (specialMode == 1)
                {
                    output.Sprite(input.Sprites.Shark[6 + input.U.SpecialAccessoryType]);
                    return;
                }

                output.Sprite(input.Sprites.Shark[4 + input.U.SpecialAccessoryType]);
            });

            builder.RenderSingle(SpriteType.Belly, 1, (input, output) =>
            {
                output.Coloring(ColorMap.GetSharkBellyColor(input.U.AccessoryColor));
                if (input.A.HasBelly == false)
                {
                    output.Sprite(input.Sprites.Shark[12]);
                    return;
                }

                int size = input.A.GetStomachSize(22);

                if (size > 16)
                {
                    size = 16;
                }

                output.Sprite(input.Sprites.Shark[12 + size]);
            });

            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(Defaults.RandomCustom);
        });
    }
}
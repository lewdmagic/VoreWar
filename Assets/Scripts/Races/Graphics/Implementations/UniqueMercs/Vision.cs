#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.UniqueMercs
{
    internal static class Vision
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Vision", "Vision");
                output.BonesInfo((unit) => new List<BoneInfo>()
                {
                    new BoneInfo(BoneType.WyvernBonesWithoutHead, unit.Name),
                    new BoneInfo(BoneType.VisionSkull, unit.Name)
                });
                output.FlavorText(new FlavorText(
                    new Texts {  },
                    new Texts {  },
                    new Texts { "alien", "dinosaur" }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 16,
                    StomachSize = 30,
                    HasTail = true,
                    FavoredStat = Stat.Voracity,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
                    ExpMultiplier = 2f,
                    PowerAdjustment = 4f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(20, 22),
                        Dexterity = new RaceStats.StatRange(16, 18),
                        Endurance = new RaceStats.StatRange(24, 26),
                        Mind = new RaceStats.StatRange(14, 18),
                        Will = new RaceStats.StatRange(12, 16),
                        Agility = new RaceStats.StatRange(18, 24),
                        Voracity = new RaceStats.StatRange(14, 20),
                        Stomach = new RaceStats.StatRange(12, 16),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.Ravenous,
                        TraitType.StrongGullet,
                        TraitType.Intimidating,

                    },
                    RaceDescription = "A Xeno-Spinosaurid about the size of a small horse or large dog. They eat about half or even double their body weight at minimum a day, but have been known to eat things larger than themselves. Because of their huge appetite, their digestive tract is mostly stomach, what they can't digest they regurgitate as an owl-like pellet",

                });
                output.CanBeGender = new List<Gender> { Gender.Male };
                output.GentleAnimation = true;
            });

            builder.RenderSingle(SpriteType.Body, 5, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Vision[input.A.IsOralVoring ? 1 : 0]);
            });
            builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Vision[2]);
            });
            builder.RenderSingle(SpriteType.Belly, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.HasBelly == false)
                {
                    return;
                }

                output.Sprite(input.A.HasBelly ? input.Sprites.Vision[3 + input.A.GetStomachSize(5)] : null);
            });

            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                IUnitRead unit = data.Unit;

                unit.Name = "Vision";
            });
        });
    }
}
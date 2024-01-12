#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class FeralWolves
    {
        internal static readonly IRaceData Instance = RaceBuilder.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Feral Wolf", "Feral Wolves");
                output.FlavorText(new FlavorText(
                    new Texts { "shaggy", "gamey", "growling" },
                    new Texts { "long furred", "spirited", "panting" },
                    new Texts { "feral", "canine", {"wolfess", Gender.Female}, {"wolf", Gender.Male} },
                    "Fangs"
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 15,
                    StomachSize = 18,
                    HasTail = true,
                    FavoredStat = Stat.Strength,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal },
                    ExpMultiplier = 1.75f,
                    PowerAdjustment = 1.75f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(10, 22),
                        Dexterity = new RaceStats.StatRange(4, 8),
                        Endurance = new RaceStats.StatRange(12, 22),
                        Mind = new RaceStats.StatRange(6, 12),
                        Will = new RaceStats.StatRange(6, 12),
                        Agility = new RaceStats.StatRange(8, 16),
                        Voracity = new RaceStats.StatRange(8, 16),
                        Stomach = new RaceStats.StatRange(6, 14),
                    },
                    RacialTraits = new List<Traits>()
                    {
                        Traits.Biter,
                        Traits.PackStrength,
                    },
                    RaceDescription = "Natives of this realm, the wolves were more than happy for a chance to welcome the newcomers to their bellies. While likely related to their bipedal cousins, the ferals only consider them as familiar smelling food.",
                });
                output.CanBeGender = new List<Gender> { Gender.None };
                output.SkinColors = 6;
                output.HairColors = 6;
                output.GentleAnimation = true;
            });

            builder.RenderSingle(SpriteType.Head, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralWolfFur, input.U.SkinColor));
                output.Sprite(input.Sprites.FeralWolf[input.A.IsAttacking || input.A.IsEating ? 1 : 0]);
            });
            builder.RenderSingle(SpriteType.Eyes, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralWolfMane, input.U.HairColor));
                output.Sprite(input.Sprites.FeralWolf[input.A.IsAttacking || input.A.IsEating ? 3 : 2]);
            });
            builder.RenderSingle(SpriteType.Hair, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralWolfMane, input.U.HairColor));
                output.Sprite(input.Sprites.FeralWolf[6]);
            });
            builder.RenderSingle(SpriteType.Hair2, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralWolfMane, input.U.HairColor));
                output.Sprite(input.Sprites.FeralWolf[7]);
            });
            builder.RenderSingle(SpriteType.Body, 0, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralWolfFur, input.U.SkinColor));
                output.Sprite(input.Sprites.FeralWolf[4]);
            });
            builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralWolfFur, input.U.SkinColor));
                output.Sprite(input.Sprites.FeralWolf[5]);
            });
            builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.FeralWolf[8]);
            });
            builder.RenderSingle(SpriteType.BodyAccent3, 2, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.FeralWolf[9]);
            });
            builder.RenderSingle(SpriteType.BodyAccessory, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralWolfFur, input.U.SkinColor));

                if (input.A.GetStomachSize(4) == 4)
                {
                    output.Sprite(input.Sprites.FeralWolf[15]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.FeralWolfFur, input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    return;
                }

                output.Sprite(input.Sprites.FeralWolf[10 + input.A.GetStomachSize(4)]);
            });
            builder.RandomCustom(Defaults.RandomCustom);
        });
    }
}
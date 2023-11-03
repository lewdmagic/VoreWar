#region

using System;
using System.Collections.Generic;

#endregion

internal static class Serpents
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Names("Serpent", "Serpents");
        builder.FlavorText(new FlavorText(
            new Texts { "limbless", "noodly", "slithery" },
            new Texts { "scaly", "long bodied", "slithering" },
            new Texts { "serpent", "snake", "slitherer" },
            "Tail Blade"
        ));
        builder.RaceTraits(new RaceTraits()
        {
            BodySize = 12,
            StomachSize = 15,
            HasTail = true,
            FavoredStat = Stat.Voracity,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 1.25f,
            PowerAdjustment = .9f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(8, 20),
                Dexterity = new RaceStats.StatRange(6, 14),
                Endurance = new RaceStats.StatRange(10, 18),
                Mind = new RaceStats.StatRange(6, 14),
                Will = new RaceStats.StatRange(4, 8),
                Agility = new RaceStats.StatRange(6, 12),
                Voracity = new RaceStats.StatRange(8, 16),
                Stomach = new RaceStats.StatRange(8, 16),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.SlowAbsorption,
                Traits.Biter
            },
            RaceDescription = "When the lizard folk emerged from their portal to this land, some young snakes from their old world managed to slip along. Growing fast under the effect of this new realm, the Serpents soon emerged as a ravenous horde.",
        });
        builder.IndividualNames(new List<string>
        {
            "Snaky",
            "Snoot",
            "Snek",
            "Slither",
            "Viper",
            "Tongy",
            "Bity",
            "Bulgy",
            "Huggy",
            "Nippy",
        });
        builder.Setup(output =>
        {
            output.CanBeGender = new List<Gender> { Gender.None };
            output.EyeColors = ColorPaletteMap.GetPaletteCount(SwapType.EyeColor);
            output.EyeTypes = 4;
            output.AvoidedEyeTypes = 1;
            output.GentleAnimation = true;
            output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.LizardMain);
        });

        builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
            output.Sprite(input.Sprites.Serpents[Math.Min(16 + input.U.EyeType, 19)]);
        });
        builder.RenderSingle(SpriteType.Mouth, 10, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.A.GetSimpleBodySprite() == 0 && input.A.Targetable)
            {
                if (State.Rand.Next(400) == 0)
                {
                    input.A.SetAnimationMode(2, .25f);
                }
            }

            int animationFrame = input.A.CheckAnimationFrame();
            if (animationFrame == 2)
            {
                output.Sprite(input.Sprites.Serpents[6]);
                return;
            }

            if (animationFrame == 1)
            {
                output.Sprite(input.Sprites.Serpents[7]);
                return;
            }

            output.Sprite(input.A.IsEating ? input.Sprites.Serpents[5] : null);
        });

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.SkinColor));
            output.Sprite(input.Sprites.Serpents[input.A.IsAttacking || input.A.IsEating ? 2 : 0]);
        });
        builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardLight, input.U.SkinColor));
            output.Sprite(input.Sprites.Serpents[input.A.IsAttacking || input.A.IsEating ? 3 : 1]);
        });
        builder.RenderSingle(SpriteType.BodyAccessory, 9, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardLight, input.U.SkinColor));
            output.Sprite(input.A.IsEating ? input.Sprites.Serpents[4] : null);
        });


        builder.RenderSingle(SpriteType.BodySize, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardMain, input.U.SkinColor));
            if (input.A.HasBelly == false)
            {
                return;
            }

            output.Sprite(input.Sprites.Serpents[8 + input.A.GetStomachSize(3, 3)]);
        });

        builder.RenderSingle(SpriteType.Belly, 8, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.LizardLight, input.U.SkinColor));
            if (input.A.HasBelly == false)
            {
                return;
            }

            output.Sprite(input.Sprites.Serpents[12 + input.A.GetStomachSize(3, 3)]);
        });

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(Defaults.RandomCustom);
    });
}
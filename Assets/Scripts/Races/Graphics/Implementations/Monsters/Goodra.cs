using System.Collections.Generic;

internal static class Goodra
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Blank, builder =>
    {
        builder.Names("Goodra", "Goodras");
        builder.RaceTraits(new RaceTraits()
        {
            BodySize = 32,
            StomachSize = 50,
            HasTail = true,
            FavoredStat = Stat.Stomach,
            AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
            ExpMultiplier = 6f,
            PowerAdjustment = 12f,
            RaceStats = new RaceStats()
            {
                Strength = new RaceStats.StatRange(16, 24),
                Dexterity = new RaceStats.StatRange(8, 14),
                Endurance = new RaceStats.StatRange(32, 40),
                Mind = new RaceStats.StatRange(12, 20),
                Will = new RaceStats.StatRange(16, 24),
                Agility = new RaceStats.StatRange(6, 10),
                Voracity = new RaceStats.StatRange(16, 24),
                Stomach = new RaceStats.StatRange(32, 40),
            },
            RacialTraits = new List<Traits>()
            {
                Traits.BoggingSlime,
                Traits.Endosoma,
                Traits.Resilient,
                Traits.SoftBody,
                Traits.VerySlowMovement,
                Traits.HardSkin
            },
            RaceDescription = "Goodra, the Slug Dragon Pokemon. Goodra are large soft dragon type pokemon coated in slime. They love to give hugs and often confuse friends from food."
        });
        builder.TownNames(new List<string>
        {
            "Goodra",
            "Sligoo",
            "Goomy",
            "Gooey",
            "Hugs",
            "Huggy",
            "Slimy",
            "Pudding",
            "Gumdrops",
            "Jelly",
            "Dragooze",
            "Escargoo",
            "Squishy",
            "Goober",
            "Oozy",
            "Goopy",
            "Nuri",
            "Dragoonite",
            "Spygoo",
            "Dragoo",
            "Gooigi",
        });
        builder.Setup(output =>
        {
            output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.GoodraSkin);
            output.EyeTypes = 4;
            output.GentleAnimation = true;
            output.ClothingColors = 0;
        });


        /*
        Body = new SpriteExtraInfo(1, RaceSpriteGeneratorSet.BodySprite, WhiteColored); // Body
        Belly = new SpriteExtraInfo(2, null, WhiteColored); // Belly
        BodyAccent = new SpriteExtraInfo(3, RaceSpriteGeneratorSet.BodyAccentSprite, WhiteColored); // Leg
        Eyes = new SpriteExtraInfo(4, RaceSpriteGeneratorSet.EyesSprite, WhiteColored); // Face
        Hair = new SpriteExtraInfo(5, RaceSpriteGeneratorSet.HeadSprite, WhiteColored); // Attack Frame
        */

        builder.RenderSingle(SpriteType.Head, 4, (input, output) =>
        {
            output.Coloring(Defaults.FurryColor(input.Actor));
            if (input.A.IsAttacking)
            {
                output.Sprite(input.Sprites.Goodra[3]);
            }
        });

        builder.RenderSingle(SpriteType.Eyes, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.GoodraSkin, input.U.SkinColor));
            if (input.A.IsOralVoring)
            {
                return;
            }

            output.Sprite(input.Sprites.Goodra[4 + input.U.EyeType]);
        }); // Face

        builder.RenderSingle(SpriteType.Hair, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.GoodraSkin, input.U.SkinColor));
            if (input.A.IsAttacking)
            {
                output.Sprite(input.Sprites.Goodra[3]);
            }
        }); // Attack Frame (This is headSprite set to Hair for some reason). Might be a mistake

        builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.GoodraSkin, input.U.SkinColor));
            if (input.A.IsAttacking)
            {
                output.Sprite(input.Sprites.Goodra[2]);
                return;
            }

            if (input.A.IsOralVoring)
            {
                output.Sprite(input.Sprites.Goodra[1]);
                return;
            }

            output.Sprite(input.Sprites.Goodra[0]);
        }); // Body

        builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.GoodraSkin, input.U.SkinColor));
            output.Sprite(input.Sprites.Goodra[9]);
        }); // Leg
        builder.RenderSingle(SpriteType.Belly, 2, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.GoodraSkin, input.U.SkinColor));
            if (input.A.HasBelly == false)
            {
                return;
            }

            output.Sprite(input.Sprites.Goodra[10 + input.A.GetStomachSize(6)]);
        }); // Belly

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(Defaults.RandomCustom);
    });
}
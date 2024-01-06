#region

using System.Collections.Generic;

#endregion

internal static class Cake
{
    internal static readonly IRaceData Instance = RaceBuilder.CreateV2(Defaults.Blank, builder =>
    {
        builder.Setup(output =>
        {
            output.Names("Cake", "Cakes");
            output.BonesInfo((unit) => new List<BoneInfo>()
            {
                new BoneInfo(BoneTypes.Cake, unit.Name)
            });
            output.FlavorText(new FlavorText(
                new Texts {  },
                new Texts {  },
                new Texts { "cake", "baked good", "ghostly confectionary", "delicious dessert" }
            ));
            output.RaceTraits(new RaceTraits()
            {
                BodySize = 30,
                StomachSize = 20,
                HasTail = false,
                FavoredStat = Stat.Voracity,
                AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
                ExpMultiplier = 2.5f,
                PowerAdjustment = 2.5f,
                RaceStats = new RaceStats()
                {
                    Strength = new RaceStats.StatRange(10, 26),
                    Dexterity = new RaceStats.StatRange(4, 8),
                    Endurance = new RaceStats.StatRange(20, 30),
                    Mind = new RaceStats.StatRange(4, 10),
                    Will = new RaceStats.StatRange(6, 12),
                    Agility = new RaceStats.StatRange(6, 10),
                    Voracity = new RaceStats.StatRange(10, 18),
                    Stomach = new RaceStats.StatRange(8, 16),
                },
                RacialTraits = new List<Traits>()
                {
                    Traits.Frenzy,
                    Traits.Tasty,
                    Traits.SoftBody,
                    Traits.SlowDigestion

                },
                RaceDescription = "A wizard baking a cake cut his hand and a drop of blood fell in the batter. His guests arrived while the cake was in the oven, eagerly waiting for their treat. But having got a taste of him, the Cake, once baked, ate the wizard and his guests instead.",

            });
            output.CanBeGender = new List<Gender> { Gender.None };
            output.ExtraColors1 = ColorMap.PastelColorCount;
            output.ExtraColors2 = ColorMap.PastelColorCount;
            output.ExtraColors3 = ColorMap.PastelColorCount;
            output.ExtraColors4 = ColorMap.PastelColorCount;
        });


        builder.RenderSingle(SpriteType.Head, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.A.IsEating || input.A.IsAttacking)
            {
                output.Sprite(input.Sprites.Cake[8]);
                return;
            }

            output.Sprite(input.Sprites.Cake[7]);
        }); // Teeth.

        builder.RenderSingle(SpriteType.Eyes, 6, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.A.HasBelly == false)
            {
                return;
            }

            output.Sprite(input.Sprites.Cake[9 + input.A.GetStomachSize(7)]);
        }); // Flames.

        builder.RenderSingle(SpriteType.Mouth, 4, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if (input.A.IsEating || input.A.IsAttacking)
            {
                if (Config.Bones || Config.ScatBones)
                {
                    output.Sprite(input.Sprites.Cake[6]);
                    return;
                }

                output.Sprite(input.Sprites.Cake[5]);
            }
        }); // Mouth.

        builder.RenderSingle(SpriteType.Body, 0, (input, output) =>
        {
            output.Coloring(ColorMap.GetPastelColor(input.U.ExtraColor1));
            if (input.A.IsEating || input.A.IsAttacking)
            {
                output.Sprite(input.Sprites.Cake[1]);
                return;
            }

            output.Sprite(input.Sprites.Cake[0]);
        }); // Body.

        builder.RenderSingle(SpriteType.BodyAccent, 1, (input, output) =>
        {
            output.Coloring(ColorMap.GetPastelColor(input.U.ExtraColor2));
            output.Sprite(input.Sprites.Cake[4]);
        }); // Ring.
        builder.RenderSingle(SpriteType.BodyAccent2, 2, (input, output) =>
        {
            output.Coloring(ColorMap.GetPastelColor(input.U.ExtraColor3));
            output.Sprite(input.Sprites.Cake[2]);
        }); // The balls around the top.
        builder.RenderSingle(SpriteType.BodyAccent3, 3, (input, output) =>
        {
            output.Coloring(ColorMap.GetPastelColor(input.U.ExtraColor4));
            output.Sprite(input.Sprites.Cake[3]);
        }); // Candles.

        builder.RunBefore(Defaults.Finalize);
        builder.RandomCustom(Defaults.RandomCustom);
    });
}
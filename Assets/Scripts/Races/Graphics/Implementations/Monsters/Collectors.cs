#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class Collectors
    {
        internal static readonly IRaceData Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Collector", "Collectors");
                output.FlavorText(new FlavorText(
                    new Texts {  },
                    new Texts {  },
                    new Texts { "alien", "quadpod" },
                    "Maw"
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 20,
                    StomachSize = 30,
                    HasTail = true,
                    FavoredStat = Stat.Stomach,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
                    ExpMultiplier = 1.5f,
                    PowerAdjustment = 2.2f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(11, 20),
                        Dexterity = new RaceStats.StatRange(8, 16),
                        Endurance = new RaceStats.StatRange(16, 22),
                        Mind = new RaceStats.StatRange(6, 12),
                        Will = new RaceStats.StatRange(8, 16),
                        Agility = new RaceStats.StatRange(12, 20),
                        Voracity = new RaceStats.StatRange(14, 18),
                        Stomach = new RaceStats.StatRange(16, 24),
                    },
                    RacialTraits = new List<Traits>()
                    {
                        Traits.SlowDigestion,
                        Traits.SlowAbsorption,
                        Traits.BornToMove,
                        Traits.NimbleClimber,
                    },
                    RaceDescription = "These large, long limbed creatures seem to be pets or beasts of burden for the Harvesters. While very capable of hunting on their own, they mostly collect the prey the Harvesters have already brought low, filling their low hanging saggy bellies with dozens of prey.",

                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.ExtraColor2, "Mouth / Dick Color");
                });
                output.SkinColors = ColorMap.LizardColorCount; // Majority of the body
                output.EyeColors = ColorMap.EyeColorCount; // Eyes
                output.ExtraColors1 = ColorMap.LizardColorCount; // Plates, claws
                output.ExtraColors2 = ColorMap.WyvernColorCount; // Flesh in mouth, dicks
                output.CanBeGender = new List<Gender> { Gender.Male };
                output.GentleAnimation = true;
            });


            builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
            {
                output.Coloring(ColorMap.GetWyvernColor(input.U.ExtraColor2));
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Collector[7]);
                }
            }); // Mouth Parts

            builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
            {
                output.Coloring(ColorMap.GetEyeColor(input.U.EyeColor));
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    return;
                }

                output.Sprite(input.Sprites.Collector[6]);
            }); // Eyes

            builder.RenderSingle(SpriteType.Hair, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Collector[8]);
                }
            }); // Teeth

            builder.RenderSingle(SpriteType.Body, 0, (input, output) =>
            {
                output.Coloring(ColorMap.GetLizardColor(input.U.SkinColor));
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Collector[1]);
                    return;
                }

                output.Sprite(input.Sprites.Collector[0]);
            }); // Body

            builder.RenderSingle(SpriteType.BodyAccent, 2, (input, output) =>
            {
                output.Coloring(ColorMap.GetLizardColor(input.U.ExtraColor1));
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Collector[5]);
                    return;
                }

                output.Sprite(input.Sprites.Collector[4]);
            }); // Plates

            builder.RenderSingle(SpriteType.BodyAccent2, 4, (input, output) =>
            {
                output.Coloring(ColorMap.GetLizardColor(input.U.SkinColor));
                output.Sprite(input.Sprites.Collector[2]);
            }); // Closer Legs

            builder.RenderSingle(SpriteType.BodyAccent3, 5, (input, output) =>
            {
                output.Coloring(ColorMap.GetLizardColor(input.U.ExtraColor1));
                output.Sprite(input.Sprites.Collector[3]);
            }); // Closer Legs Claws

            builder.RenderSingle(SpriteType.Belly, 3, (input, output) =>
            {
                output.Coloring(ColorMap.GetLizardColor(input.U.SkinColor));

                if (input.A.HasBelly == false)
                {
                    return;
                }

                output.Sprite(input.Sprites.Collector[9 + input.A.GetStomachSize(9, .8f)]);
            }); // Belly

            builder.RenderSingle(SpriteType.Dick, 1, (input, output) =>
            {
                output.Coloring(ColorMap.GetWyvernColor(input.U.ExtraColor2));
                if (Config.ErectionsFromVore && input.A.HasBelly)
                {
                    output.Sprite(input.Sprites.Collector[20]);
                }
            }); // Dicks

            builder.RunBefore(Defaults.Finalize);

            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                Unit unit = data.Unit;

                unit.SkinColor = State.Rand.Next(0, data.MiscRaceData.SkinColors);
                unit.EyeColor = State.Rand.Next(0, data.MiscRaceData.EyeColors);
            });
        });
    }
}
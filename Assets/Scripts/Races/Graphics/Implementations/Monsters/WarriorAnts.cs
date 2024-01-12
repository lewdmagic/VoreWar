#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class WarriorAnts
    {
        internal static readonly IRaceData Instance = RaceBuilder.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Warrior Ant", "Warrior Ants");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 8,
                    StomachSize = 14,
                    HasTail = false,
                    FavoredStat = Stat.Strength,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral },
                    ExpMultiplier = 1.1f,
                    PowerAdjustment = 1.3f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(8, 12),
                        Dexterity = new RaceStats.StatRange(12, 15),
                        Endurance = new RaceStats.StatRange(8, 12),
                        Mind = new RaceStats.StatRange(16, 20),
                        Will = new RaceStats.StatRange(6, 10),
                        Agility = new RaceStats.StatRange(8, 12),
                        Voracity = new RaceStats.StatRange(8, 12),
                        Stomach = new RaceStats.StatRange(8, 12),
                    },
                    RacialTraits = new List<Traits>()
                    {
                        Traits.AcidResistant,
                        Traits.PackStrength,
                        Traits.SlowDigestion
                    },
                    RaceDescription = ""
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.Skintone, "Body Color");
                    buttons.SetText(ButtonType.BodyAccessoryType, "Antennae Type");
                });
                output.CanBeGender = new List<Gender> { Gender.None };
                output.SpecialAccessoryCount = 9; // antennae
                output.ClothingColors = 0;
                output.GentleAnimation = true;
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.DemiantSkin);
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.DemiantSkin);
            });


            builder.RenderSingle(SpriteType.Head, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.SkinColor));
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.WarriorAnt[1]);
                    return;
                }

                output.Sprite(input.Sprites.WarriorAnt[0]);
            });

            builder.RenderSingle(SpriteType.Eyes, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.WarriorAnt[15]);
            });
            builder.RenderSingle(SpriteType.Mouth, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.WarriorAnt[2]);
                }
            });

            builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    output.Sprite(input.Sprites.WarriorAnt[16]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.AccessoryColor));
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.WarriorAnt[4]);
                    return;
                }

                output.Sprite(input.Sprites.WarriorAnt[3]);
            }); // mandibles

            builder.RenderSingle(SpriteType.BodyAccent2, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.AccessoryColor));
                output.Sprite(input.Sprites.WarriorAnt[5]);
            }); // legs
            builder.RenderSingle(SpriteType.BodyAccessory, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.AccessoryColor));
                output.Sprite(input.Sprites.WarriorAnt[6 + input.U.SpecialAccessoryType]);
            }); // antennae
            builder.RenderSingle(SpriteType.Belly, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DemiantSkin, input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    return;
                }

                output.Sprite(input.Sprites.WarriorAnt[17 + input.A.GetStomachSize(16)]);
            });


            builder.RunBefore((input, output) =>
            {
                output.ChangeSprite(SpriteType.Body).AddOffset(20 * .625f, 0);
                output.ChangeSprite(SpriteType.Belly).AddOffset(20 * .625f, 0);
            });

            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                Unit unit = data.Unit;

                unit.AccessoryColor = unit.SkinColor;
            });
        });
    }
}
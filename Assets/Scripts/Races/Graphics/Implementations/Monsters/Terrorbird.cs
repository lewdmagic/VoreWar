using System.Collections.Generic;

namespace Races.Graphics.Implementations.Monsters
{
    internal static class Terrorbird
    {
        internal static readonly IRaceData Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Terrorbird", "Terrorbirds");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 18,
                    StomachSize = 18,
                    HasTail = true,
                    FavoredStat = Stat.Agility,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.TailVore },
                    ExpMultiplier = 1.5f,
                    PowerAdjustment = 1.75f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(12, 20),
                        Dexterity = new RaceStats.StatRange(8, 16),
                        Endurance = new RaceStats.StatRange(8, 16),
                        Mind = new RaceStats.StatRange(6, 12),
                        Will = new RaceStats.StatRange(6, 12),
                        Agility = new RaceStats.StatRange(12, 20),
                        Voracity = new RaceStats.StatRange(10, 18),
                        Stomach = new RaceStats.StatRange(8, 16),
                    },
                    RacialTraits = new List<Traits>()
                    {
                        Traits.Intimidating,
                        Traits.ArtfulDodge,
                        Traits.Tenacious
                    },
                    RaceDescription = ""
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.BodyAccessoryType, "Head Plumage Type");
                });
                output.SpecialAccessoryCount = 8; // head plumage type
                output.ClothingColors = 0;
                output.GentleAnimation = true;
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.TerrorbirdSkin);
                output.EyeColors = ColorPaletteMap.GetPaletteCount(SwapType.TerrorbirdSkin);
            });


            builder.RenderSingle(SpriteType.Head, 9, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Terrorbird[10]);
                    return;
                }

                output.Sprite(input.Sprites.Terrorbird[9]);
            });

            builder.RenderSingle(SpriteType.Eyes, 10, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.TerrorbirdSkin, input.U.SkinColor));
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Terrorbird[12]);
                    return;
                }

                output.Sprite(input.Sprites.Terrorbird[11]);
            });

            builder.RenderSingle(SpriteType.Body, 8, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.TerrorbirdSkin, input.U.SkinColor));
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Terrorbird[1]);
                    return;
                }

                output.Sprite(input.Sprites.Terrorbird[0]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 9, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.TerrorbirdSkin, input.U.SkinColor));
                if (input.A.GetStomachSize(29) >= 27)
                {
                    output.Sprite(input.Sprites.Terrorbird[14]);
                    return;
                }

                output.Sprite(input.Sprites.Terrorbird[6]);
            }); // front legs feathers

            builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.TerrorbirdSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Terrorbird[7]);
            }); // back leg feathers
            builder.RenderSingle(SpriteType.BodyAccent3, 10, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.GetStomachSize(29) >= 27)
                {
                    output.Sprite(input.Sprites.Terrorbird[15]);
                    return;
                }

                output.Sprite(input.Sprites.Terrorbird[4]);
            }); // front leg claws

            builder.RenderSingle(SpriteType.BodyAccent4, 2, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Terrorbird[5]);
            }); // back leg claws
            builder.RenderSingle(SpriteType.BodyAccent5, 11, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.TerrorbirdSkin, input.U.SkinColor));
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Terrorbird[3]);
                    return;
                }

                output.Sprite(input.Sprites.Terrorbird[2]);
            }); // wings

            builder.RenderSingle(SpriteType.BodyAccent6, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.TerrorbirdSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Terrorbird[13]);
            }); // back tail feather
            builder.RenderSingle(SpriteType.BodyAccent7, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.TerrorbirdSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Terrorbird[8]);
            }); // belly cover
            builder.RenderSingle(SpriteType.BodyAccent8, 12, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.TerrorbirdSkin, input.U.SkinColor));
                int sizet = input.A.GetTailSize(4);
                if (input.U.Predator == false || input.A.PredatorComponent?.TailFullness == 0)
                {
                    return;
                }

                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Terrorbird[67 + 2 * sizet]);
                    return;
                }

                output.Sprite(input.Sprites.Terrorbird[66 + 2 * sizet]);
            }); // crop

            builder.RenderSingle(SpriteType.BodyAccessory, 13, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.TerrorbirdSkin, input.U.SkinColor));
                if (input.A.IsAttacking || input.A.IsEating)
                {
                    output.Sprite(input.Sprites.Terrorbird[24 + input.U.SpecialAccessoryType]);
                    return;
                }

                output.Sprite(input.Sprites.Terrorbird[16 + input.U.SpecialAccessoryType]);
            }); // head plumage

            builder.RenderSingle(SpriteType.Belly, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.TerrorbirdSkin, input.U.SkinColor));
                if (input.A.HasBelly == false)
                {
                    return;
                }

                output.Sprite(input.Sprites.Terrorbird[32 + input.A.GetStomachSize(29)]);
            });

            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(Defaults.RandomCustom);
        });
    }
}
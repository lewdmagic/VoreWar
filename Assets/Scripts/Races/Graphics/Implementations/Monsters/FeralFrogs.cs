#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Monsters
{
    internal static class FeralFrogs
    {
        private enum Position
        {
            Front,
            Pouncing,
            Standing
        }

        private static Position CalcPosition(Actor_Unit actor)
        {
            if (actor.IsPouncingFrog)
            {
                return Position.Pouncing;
            }
            else if (actor.IsEating)
            {
                return Position.Standing;
            }
            else
            {
                return Position.Front;
            }
        }
        
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Feral Frog", "Feral Frogs");
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 15,
                    StomachSize = 14,
                    HasTail = false,
                    FavoredStat = Stat.Stomach,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Anal, VoreType.Unbirth },
                    ExpMultiplier = 1.2f,
                    PowerAdjustment = 1.5f,
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(12, 16),
                        Dexterity = new RaceStats.StatRange(4, 8),
                        Endurance = new RaceStats.StatRange(12, 16),
                        Mind = new RaceStats.StatRange(6, 8),
                        Will = new RaceStats.StatRange(8, 12),
                        Agility = new RaceStats.StatRange(12, 16),
                        Voracity = new RaceStats.StatRange(10, 14),
                        Stomach = new RaceStats.StatRange(8, 12),
                    },
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.RangedVore,
                        TraitType.Pounce,
                        TraitType.HeavyPounce,
                        TraitType.Clumsy,
                    },
                    RaceDescription = ""
                });
                output.CanBeGender = new List<Gender> { Gender.Female };
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.Frog);

                output.EyeTypes = 4;
                output.MouthTypes = 4;
            });


            builder.RenderSingle(SpriteType.Eyes, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
                switch (CalcPosition(input.A))
                {
                    case Position.Front:
                        output.Sprite(input.Sprites.Frogs[1 + input.U.EyeType]);
                        return;
                    case Position.Pouncing:
                        return;
                    case Position.Standing:
                        output.Sprite(input.Sprites.Frogs[17 + input.U.EyeType]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.Mouth, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Frog, input.U.AccessoryColor));
                switch (CalcPosition(input.A))
                {
                    case Position.Front:
                        output.Sprite(input.Sprites.Frogs[5 + input.U.MouthType]);
                        return;
                    case Position.Pouncing:
                        return;
                    case Position.Standing:
                        output.Sprite(input.Sprites.Frogs[21 + input.U.MouthType]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.Body, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Frog, input.U.AccessoryColor));
                switch (CalcPosition(input.A))
                {
                    case Position.Front:
                        output.Sprite(input.Sprites.Frogs[0]);
                        return;
                    case Position.Pouncing:
                        output.Sprite(input.Sprites.Frogs[32]);
                        return;
                    case Position.Standing:
                        output.Sprite(input.Sprites.Frogs[10]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Frog, input.U.AccessoryColor));
                switch (CalcPosition(input.A))
                {
                    case Position.Front:
                        return;
                    case Position.Pouncing:
                        return;
                    case Position.Standing:
                        if (input.A.HasBelly)
                        {
                            output.Sprite(input.Sprites.Frogs[13 + input.A.GetStomachSize(3, .75f)]);
                        }

                        break;
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent2, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Frog, input.U.AccessoryColor));
                switch (CalcPosition(input.A))
                {
                    case Position.Front:
                        if (input.A.HasBelly)
                        {
                            output.Sprite(input.Sprites.Frogs[33 + input.A.GetStomachSize(3, .75f)]);
                        }

                        break;
                    case Position.Pouncing:
                        if (input.A.HasBelly)
                        {
                            output.Sprite(input.Sprites.Frogs[27 + input.A.GetStomachSize(3, .75f)]);
                        }

                        break;
                    case Position.Standing:
                        if (input.A.HasBelly)
                        {
                            output.Sprite(input.Sprites.Frogs[11 + input.A.GetStomachSize(2, .75f)]);
                        }

                        break;
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent3, 9, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Frog, input.U.AccessoryColor));
                if (input.A.IsOralVoring == false)
                {
                    return;
                }

                if (input.A.IsOralVoringHalfOver == false)
                {
                    return;
                }

                switch (CalcPosition(input.A))
                {
                    case Position.Front:
                        if (input.A.HasBelly)
                        {
                            output.Sprite(input.Sprites.Frogs[9]);
                        }

                        break;
                    case Position.Pouncing:
                        return;
                    case Position.Standing:
                        if (input.A.HasBelly)
                        {
                            output.Sprite(input.Sprites.Frogs[26]);
                        }

                        break;
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent4, 8, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (CalcPosition(input.A) == Position.Front && input.A.IsAttacking)
                {
                    output.Sprite(input.Sprites.Frogs[38]);
                    return;
                }

                if (input.A.IsOralVoring == false)
                {
                    return;
                }

                if (input.A.IsOralVoringHalfOver)
                {
                    return;
                }

                switch (CalcPosition(input.A))
                {
                    case Position.Front:
                        if (input.A.HasBelly)
                        {
                            output.Sprite(input.Sprites.Frogs[38]);
                        }

                        break;
                    case Position.Pouncing:
                        return;
                    case Position.Standing:
                        if (input.A.HasBelly && input.A.GetStomachSize(3, .75f) != 3)
                        {
                            output.Sprite(input.Sprites.Frogs[25]);
                        }

                        break;
                }
            });


            builder.RunBefore((input, output) =>
            {
                Defaults.Finalize.Invoke(input, output);
            });

            builder.RandomCustom(Defaults.RandomCustom);
        });


    }
}
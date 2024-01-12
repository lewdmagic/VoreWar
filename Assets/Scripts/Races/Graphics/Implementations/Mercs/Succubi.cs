#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.Mercs
{
    internal static class Succubi
    {
        internal static readonly IRaceData Instance = RaceBuilder.CreateV2(Defaults.Default, builder =>
        {
        
        
            builder.Setup(output =>
            {
                output.Names((input) =>
                {
                    if (input.GetGender() == Gender.Female)
                    {
                        return "Succubus";
                    }
                    else if (input.GetGender() == Gender.Male)
                    {
                        return "Incubus";
                    }
                    else
                    {
                        return "Concubus";
                    }
                }, (input) =>
                {
                    if (input.GetGender() == Gender.Female)
                    {
                        return "Succubi";
                    }
                    else if (input.GetGender() == Gender.Male)
                    {
                        return "Incubi";
                    }
                    else
                    {
                        return "Concubi";
                    }
                });
                output.FlavorText(new FlavorText(
                    new Texts { "devilishly tasty", "beguiling", "batty" },
                    new Texts { "demonic", "beguiling", "bat-winged" },
                    new Texts { "succubus", "demon", "hellish being" }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 10,
                    StomachSize = 12,
                    HasTail = true,
                    FavoredStat = Stat.Will,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.BreastVore, VoreType.Anal, VoreType.TailVore, VoreType.CockVore },
                    RacialTraits = new List<Traits>()
                    {
                        Traits.Dazzle,
                        Traits.Flight,
                        Traits.EnthrallingDepths,
                        Traits.PleasurableTouch,
                        Traits.Charmer
                    },
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(3, 6),
                        Dexterity = new RaceStats.StatRange(8, 14),
                        Endurance = new RaceStats.StatRange(8, 14),
                        Mind = new RaceStats.StatRange(8, 14),
                        Will = new RaceStats.StatRange(12, 20),
                        Agility = new RaceStats.StatRange(6, 14),
                        Voracity = new RaceStats.StatRange(8, 14),
                        Stomach = new RaceStats.StatRange(8, 14),
                    },
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetActive(ButtonType.ClothingColor2, true);
                });
                output.BreastSizes = () => 4;
                output.WeightGainDisabled = true;
                output.SpecialAccessoryCount = 3;
                output.EyeTypes = 3;
                output.MouthTypes = 3;
                output.HairStyles = 4;

                output.BodySizes = 0;
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.OldImp);
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.OldImpDark);
                output.ClothingShift = new Vector3(0, 32 * .625f, 0);
                output.AvoidedMainClothingTypes = 0;

                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.Clothing);
                output.AllowedMainClothingTypes.Set(
                    ClothingTypes.BikiniTopInstance,
                    ClothingTypes.BeltTopInstance,
                    ClothingTypes.StrapTopInstance,
                    ClothingTypes.BlackTopInstance,
                    RaceSpecificClothing.SuccubusDressInstance,
                    RaceSpecificClothing.SuccubusLeotardInstance
                );


                output.AllowedWaistTypes.Set(
                    ClothingTypes.BikiniBottomInstance,
                    ClothingTypes.LoinclothInstance
                );
            });


            builder.RenderSingle(SpriteType.Head, 3, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.OldImp, input.U.SkinColor));
                output.Sprite(input.Sprites.Succubi[input.A.IsOralVoring ? 21 : 20]);
            });
            builder.RenderSingle(SpriteType.Eyes, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.HairRedKeyStrict, input.U.HairColor));
                output.Sprite(input.Sprites.Succubi[27 + input.U.EyeType]);
            });
            builder.RenderSingle(SpriteType.Mouth, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.OldImp, input.U.SkinColor));
                output.Sprite(input.A.IsOralVoring ? null : input.Sprites.Succubi[30 + input.U.MouthType]);
            });
            builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.HairRedKeyStrict, input.U.HairColor));
                output.Sprite(input.Sprites.Succubi[Math.Min(75 + input.U.HairStyle, 78)]);
            });
            builder.RenderSingle(SpriteType.Hair2, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.HairRedKeyStrict, input.U.HairColor));
                output.Sprite(input.Sprites.Succubi[Math.Min(79 + input.U.HairStyle, 82)]);
            });
            builder.RenderSingle(SpriteType.Hair3, 0, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.HairRedKeyStrict, input.U.HairColor));
                output.Sprite(input.Sprites.Succubi[Math.Min(83 + input.U.HairStyle, 88)]);
            });
            builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.OldImp, input.U.SkinColor));
                if (input.A.IsUnbirthing || input.A.IsAnalVoring)
                {
                    output.Sprite(input.Sprites.Succubi[4]);
                }
                else
                {
                    //int sizeOffset = input.A.PredatorComponent?.VisibleFullness > .25f ? 1 : 0;
                    int sizeOffset = 1;
                    int attackingOffset = input.A.IsAttacking ? 2 : 0;
                    output.Sprite(input.Sprites.Succubi[sizeOffset + attackingOffset]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ImpRedKey, input.U.AccessoryColor));
                if (input.A.IsUnbirthing || input.A.IsAnalVoring)
                {
                    output.Sprite(input.Sprites.Succubi[9]);
                }
                else
                {
                    //int sizeOffset = input.A.PredatorComponent?.VisibleFullness > .25f ? 1 : 0;
                    int sizeOffset = 1;
                    int attackingOffset = input.A.IsAttacking ? 2 : 0;
                    output.Sprite(input.Sprites.Succubi[5 + sizeOffset + attackingOffset]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent2, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.OldImpDark, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Succubi[22]);
            });
            builder.RenderSingle(SpriteType.BodyAccent3, 0, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.OldImpDark, input.U.AccessoryColor));
                int sizeOffset = input.A.PredatorComponent?.TailFullness > 0 ? 1 + input.A.GetTailSize(2) : 0;
                output.Sprite(input.A.IsTailVoring ? input.Sprites.Succubi[37 + sizeOffset] : input.Sprites.Succubi[33 + sizeOffset]);
            });

            builder.RenderSingle(SpriteType.BodyAccent4, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.HairRedKeyStrict, input.U.HairColor));
                output.Sprite(input.Sprites.Succubi[23]);
            });
            builder.RenderSingle(SpriteType.BodyAccent5, 1, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                int sizeOffset = input.A.PredatorComponent?.TailFullness > 0 ? 1 : 0;
                if (input.A.IsTailVoring)
                {
                    output.Sprite(input.Sprites.Succubi[41 + sizeOffset]);
                }
            });

            builder.RenderSingle(SpriteType.BodyAccessory, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.OldImpDark, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Succubi[24 + input.U.SpecialAccessoryType]);
            });
            builder.RenderSingle(SpriteType.Breasts, 15, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.OldImp, input.U.SkinColor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.SquishedBreasts)
                {
                    output.Sprite(input.Sprites.Succubi[59 + input.U.BreastSize]);
                    return;
                }

                output.Sprite(input.Sprites.Succubi[63 + input.U.BreastSize]);
            });

            builder.RenderSingle(SpriteType.BreastShadow, 16, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.ImpRedKey, input.U.AccessoryColor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.SquishedBreasts)
                {
                    output.Sprite(input.Sprites.Succubi[67 + input.U.BreastSize]);
                    return;
                }

                output.Sprite(input.Sprites.Succubi[71 + input.U.BreastSize]);
            });

            builder.RenderSingle(SpriteType.Belly, 13, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.OldImp, input.U.SkinColor));
                if (input.A.HasBelly)
                {

                    output.Sprite(input.Sprites.Succubi[43 + input.A.GetStomachSize()]);
                }
            });

            builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
            {
                Defaults.SpriteGens3[SpriteType.Dick].Invoke(input, output);
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.OldImp, input.U.SkinColor));
            });
        
            builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.OldImp, input.U.SkinColor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                int baseSize = input.U.DickSize / 3;
                int ballOffset = input.A.GetBallSize(21, .8f);

                int combined = Math.Min(baseSize + ballOffset + 2, 20);
                // Always false
                // if (combined == 21)
                // {
                //     output.AddOffset(0, -14 * .625f);
                // }
                // else if (combined == 20)
                if (combined == 20)
                {
                    output.AddOffset(0, -12 * .625f);
                }
                else if (combined >= 17)
                {
                    output.AddOffset(0, -8 * .625f);
                }

                if (ballOffset > 0)
                {
                    output.Sprite(input.Sprites.Balls[combined]);
                    return;
                }


                output.Sprite(input.Sprites.Balls[baseSize]);
            });


            builder.RunBefore((input, output) =>
            {
                output.ChangeSprite(SpriteType.Dick).AddOffset(0, 30 * .625f);
                output.ChangeSprite(SpriteType.Balls).AddOffset(0, 33 * .625f);

                if (input.A.HasBelly)
                {
                    Vector3 localScale;

                    if (input.A.PredatorComponent.VisibleFullness > 4)
                    {
                        float extraCap = input.A.PredatorComponent.VisibleFullness - 4;
                        float xScale = Mathf.Min(1 + extraCap / 5, 1.8f);
                        float yScale = Mathf.Min(1 + extraCap / 40, 1.1f);
                        localScale = new Vector3(xScale, yScale, 1);
                    }
                    else
                    {
                        localScale = new Vector3(1, 1, 1);
                    }

                    output.ChangeSprite(SpriteType.Belly).SetActive(true).SetLocalScale(localScale);
                }
            });


            builder.RandomCustom(data =>
            {
                Defaults.RandomCustom(data);
                Unit unit = data.Unit;

                unit.BodySize = 1;
                unit.ClothingColor2 = State.Rand.Next(8);
            });
        });
    }
}
#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.MainRaces
{
    internal static class Humans
    {
        private static Func<IClothingRenderInput, IOverSizeParameters> paramsCalc = CommonRaceCode.MakeOversizeFunc(32 * 32);
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Human", "Humans");
                output.FlavorText(new FlavorText(
                    new Texts { "bare skinned", "soft", "shouting" },
                    new Texts { "adaptive", "clever", "resourceful" },
                    new Texts { "human", "humanoid", {"woman", Gender.Female}, {"man", Gender.Male} }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 10,
                    StomachSize = 15,
                    HasTail = false,
                    FavoredStat = Stat.Agility,
                    RacialTraits = new List<Traits>()
                    {
                        Traits.AdeptLearner,
                        Traits.Clever
                    },
                    RaceDescription = "These nearly hairless, soft skinned creatures suffer from a lack in the way of physical abilities, but have proven capable of improving their skills at a great speed.",
                });
                output.TownNames(new List<string>
                {
                    "Bastion",
                    "Bulwark",
                    "The Iron Stronghold",
                    "Forlorn Citadel",
                    "Fort Dauntless",
                    "Homestead",
                    "The Partition",
                    "Gold Hand Redoubt",
                    "Fort Hera",
                    "Boldenholm",
                    "Grace",
                });
                output.DickSizes = () => 6;
                output.BreastSizes = () => 8;

                output.BodySizes = 3;
                output.EyeTypes = 6;
                output.SpecialAccessoryCount = 0;
                output.HairStyles = 36;
                output.MouthTypes = 12;
                output.AccessoryColors = 0;
                output.HairColors = ColorPaletteMap.GetPaletteCount(SwapType.UniversalHair);
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.RedSkin);
                output.EyeColors = ColorPaletteMap.GetPaletteCount(SwapType.EyeColor);
                output.BodyAccentTypes1 = 6; // eyebrows
                output.BeardStyles = 7;

                output.ExtendedBreastSprites = true;
                output.AllowedMainClothingTypes.Set(
                    GenericTop1.GenericTop1Instance.Create(paramsCalc),
                    GenericTop2.GenericTop2Instance.Create(paramsCalc),
                    GenericTop3.GenericTop3Instance.Create(paramsCalc),
                    GenericTop4.GenericTop4Instance.Create(paramsCalc),
                    GenericTop5.GenericTop5Instance.Create(paramsCalc),
                    GenericTop6.GenericTop6Instance.Create(paramsCalc),
                    GenericTop7.GenericTop7Instance.Create(paramsCalc),
                    MaleTop.MaleTopInstance,
                    MaleTop2.MaleTop2Instance,
                    MaleTop3.MaleTop3Instance,
                    MaleTop4.MaleTop4Instance,
                    MaleTop5.MaleTop5Instance,
                    MaleTop6.MaleTop6Instance,
                    Uniform1.Uniform1Instance.Create(paramsCalc),
                    FemaleOnePiece1.FemaleOnePiece1Instance.Create(paramsCalc),
                    FemaleOnePiece2.FemaleOnePiece2Instance.Create(paramsCalc),
                    FemaleOnePiece3.FemaleOnePiece3Instance.Create(paramsCalc),
                    FemaleOnePiece4.FemaleOnePiece4Instance.Create(paramsCalc)
                );
                output.AvoidedMainClothingTypes = 3;
                output.AvoidedEyeTypes = 0;
                output.AllowedWaistTypes.Set(
                    GenericBot1.GenericBot1Instance,
                    GenericBot2.GenericBot2Instance,
                    GenericBot3.GenericBot3Instance,
                    GenericBot4.GenericBot4Instance,
                    GenericBot5.GenericBot5Instance,
                    GenericBot6.GenericBot6Instance,
                    Uniform2.Uniform2Instance,
                    BigLoin.BigLoinInstance,
                    Pants1.Pants1Instance,
                    Pants2.Pants2Instance,
                    Skirt.SkirtInstance
                );
                output.ExtraMainClothing1Types.Set(
                );

                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.Clothing50Spaced);
            });

            builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
            {
                output.Coloring(FurryColor(input.Actor));
                if (input.A.IsEating)
                {
                    if (input.U.HasBreasts)
                    {
                        output.Sprite(input.U.BodySize > 1 ? input.Sprites.HumansBodySprites2[4] : input.Sprites.HumansBodySprites2[1]);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.HumansBodySprites2[7 + input.U.BodySize * 3]);
                    }
                }
                else if (input.A.IsAttacking)
                {
                    if (input.U.HasBreasts)
                    {
                        output.Sprite(input.U.BodySize > 1 ? input.Sprites.HumansBodySprites2[5] : input.Sprites.HumansBodySprites2[2]);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.HumansBodySprites2[8 + input.U.BodySize * 3]);
                    }
                }
                else
                {
                    if (input.U.HasBreasts)
                    {
                        output.Sprite(input.U.BodySize > 1 ? input.Sprites.HumansBodySprites2[3] : input.Sprites.HumansBodySprites2[0]);
                    }
                    else
                    {
                        output.Sprite(input.Sprites.HumansBodySprites2[6 + input.U.BodySize * 3]);
                    }
                }
            });

            builder.RenderSingle(SpriteType.Eyes, 8, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.IsDead && input.U.Items != null)
                {
                    output.Sprite(input.Sprites.HumansBodySprites2[69]);
                }
                else
                {
                    output.Sprite(input.Sprites.HumansBodySprites3[24 + 4 * input.U.EyeType + (input.A.IsAttacking || input.A.IsEating ? 0 : 2)]);
                }
            });

            builder.RenderSingle(SpriteType.SecondaryEyes, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
                if (input.U.IsDead && input.U.Items != null)
                {
                }
                else
                {
                    output.Sprite(input.Sprites.HumansBodySprites3[25 + 4 * input.U.EyeType + (input.A.IsAttacking || input.A.IsEating ? 0 : 2)]);
                }
            });


            builder.RenderSingle(SpriteType.Mouth, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.A.IsEating || input.A.IsAttacking)
                {
                }
                else
                {
                    output.Sprite(input.Sprites.HumansBodySprites3[108 + input.U.MouthType]);
                }
            });

            builder.RenderSingle(SpriteType.Hair, 21, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.UniversalHair, input.U.HairColor));
                output.Sprite(input.Sprites.HumansBodySprites2[71 + 2 * input.U.HairStyle]);
            });

            builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.UniversalHair, input.U.HairColor));
                output.Sprite(input.Sprites.HumansBodySprites2[72 + 2 * input.U.HairStyle]);
            });

            builder.RenderSingle(SpriteType.Hair3, 9, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.UniversalHair, input.U.HairColor));
                output.Sprite(input.Sprites.HumansBodySprites3[120 + input.U.BodyAccentType1]);
            }); // Eyebrows

            builder.RenderSingle(SpriteType.Beard, 9, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.UniversalHair, input.U.HairColor));
                if (input.U.BeardStyle == 6)
                {
                }
                else
                {
                    output.Sprite(input.Sprites.HumansBodySprites3[126 + input.U.BeardStyle]);
                }
            });

            builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
            {
                output.Coloring(FurryColor(input.Actor));
                if (input.U.HasWeapon == false)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.HumansBodySprites1[3 + input.U.BodySize * 4 + (input.U.HasBreasts ? 0 : 12)]);
                        return;
                    }

                    output.Sprite(input.Sprites.HumansBodySprites1[0 + input.U.BodySize * 4 + (input.U.HasBreasts ? 0 : 12)]);
                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.HumansBodySprites1[2 + input.U.BodySize * 4 + (input.U.HasBreasts ? 0 : 12)]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.HumansBodySprites1[3 + input.U.BodySize * 4 + (input.U.HasBreasts ? 0 : 12)]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.HumansBodySprites1[1 + input.U.BodySize * 4 + (input.U.HasBreasts ? 0 : 12)]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.HumansBodySprites1[3 + input.U.BodySize * 4 + (input.U.HasBreasts ? 0 : 12)]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.HumansBodySprites1[2 + input.U.BodySize * 4 + (input.U.HasBreasts ? 0 : 12)]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.HumansBodySprites1[1 + input.U.BodySize * 4 + (input.U.HasBreasts ? 0 : 12)]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.HumansBodySprites1[2 + input.U.BodySize * 4 + (input.U.HasBreasts ? 0 : 12)]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.HumansBodySprites1[1 + input.U.BodySize * 4 + (input.U.HasBreasts ? 0 : 12)]);
                        return;
                    default:
                        output.Sprite(input.Sprites.HumansBodySprites1[0 + input.U.BodySize * 4 + (input.U.HasBreasts ? 0 : 12)]);
                        return;
                }
            });

            builder.RenderSingle(SpriteType.BodyAccent, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon == false)
                {
                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        return;
                    case 1:
                        return;
                    case 2:
                        return;
                    case 3:
                        return;
                    case 4:
                        output.Sprite(input.Sprites.HumansBodySprites3[137]);
                        return;
                    case 5:
                        return;
                    case 6:
                        output.Sprite(input.Sprites.HumansBodySprites3[140]);
                        return;
                    case 7:
                        return;
                    default:
                        return;
                }
            }); // Extra weapon sprite

            builder.RenderSingle(SpriteType.BodyAccent2, 0, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon == false)
                {
                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.HumansBodySprites3[143]).Layer(22);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.HumansBodySprites3[143]).Layer(22);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.HumansBodySprites3[143]).Layer(22);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.HumansBodySprites3[143]).Layer(22);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.HumansBodySprites3[142]).Layer(0);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.HumansBodySprites3[142]).Layer(0);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.HumansBodySprites3[142]).Layer(0);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.HumansBodySprites3[142]).Layer(0);
                        return;
                    default:
                        return;
                }
            }); // Back weapon sprite

            builder.RenderSingle(SpriteType.BodyAccessory, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.RedSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.HumansBodySprites3[0]);
            }); // Ears
            builder.RenderSingle(SpriteType.Breasts, 17, (input, output) =>
            {
                output.Coloring(FurryColor(input.Actor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.LeftBreastFullness > 0)
                {
                    int leftSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(32 * 32));

                    if (leftSize > 28)
                    {
                        leftSize = 28;
                    }

                    output.Sprite(input.Sprites.HumansVoreSprites[0 + leftSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[0 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.SecondaryBreasts, 17, (input, output) =>
            {
                output.Coloring(FurryColor(input.Actor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.PredatorComponent?.RightBreastFullness > 0)
                {
                    int rightSize = (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(32 * 32));

                    if (rightSize > 28)
                    {
                        rightSize = 28;
                    }

                    output.Sprite(input.Sprites.HumansVoreSprites[32 + rightSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[32 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
            {
                output.Coloring(FurryColor(input.Actor));
                if (input.A.HasBelly)
                {
                    int size = input.A.GetStomachSize(31, 0.7f);

                    switch (size)
                    {
                        case 26:
                            output.AddOffset(0, -14 * .625f);
                            break;
                        case 27:
                            output.AddOffset(0, -17 * .625f);
                            break;
                        case 28:
                            output.AddOffset(0, -20 * .625f);
                            break;
                        case 29:
                            output.AddOffset(0, -25 * .625f);
                            break;
                        case 30:
                            output.AddOffset(0, -27 * .625f);
                            break;
                        case 31:
                            output.AddOffset(0, -32 * .625f);
                            break;
                    }

                    output.Sprite(input.Sprites.HumansVoreSprites[70 + size]);
                }
            });

            builder.RenderSingle(SpriteType.Dick, 11, (input, output) =>
            {
                output.Coloring(FurryColor(input.Actor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect())
                {
                    if (input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(32 * 32)) < 16)
                    {
                        output.Sprite(input.Sprites.HumansBodySprites4[1 + input.U.DickSize * 2 + (input.U.BodySize > 1 ? 12 : 0) + (!input.U.HasBreasts ? 24 : 0)]).Layer(20);
                        return;
                    }

                    output.Sprite(input.Sprites.HumansBodySprites4[0 + input.U.DickSize * 2 + (input.U.BodySize > 1 ? 12 : 0) + (!input.U.HasBreasts ? 24 : 0)]).Layer(13);
                    return;
                }

                output.Sprite(input.Sprites.HumansBodySprites4[0 + input.U.DickSize * 2 + (input.U.BodySize > 1 ? 12 : 0) + (!input.U.HasBreasts ? 24 : 0)]).Layer(11);
            });

            builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
            {
                output.Coloring(FurryColor(input.Actor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect() && input.A.PredatorComponent?.VisibleFullness < .75f && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetRightBreastSize(32 * 32)) < 16 && (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize + input.A.GetLeftBreastSize(32 * 32)) < 16)
                {
                    output.Layer(19);
                }
                else
                {
                    output.Layer(10);
                }

                int size = input.U.DickSize;
                int offset = input.A.GetBallSize(28, .8f);
            
                if (offset >= 26)
                {
                    output.AddOffset(0, -22 * .625f);
                }
                else if (offset == 25)
                {
                    output.AddOffset(0, -16 * .625f);
                }
                else if (offset == 24)
                {
                    output.AddOffset(0, -13 * .625f);
                }
                else if (offset == 23)
                {
                    output.AddOffset(0, -11 * .625f);
                }
                else if (offset == 22)
                {
                    output.AddOffset(0, -10 * .625f);
                }
                else if (offset == 21)
                {
                    output.AddOffset(0, -7 * .625f);
                }
                else if (offset == 20)
                {
                    output.AddOffset(0, -6 * .625f);
                }
                else if (offset == 19)
                {
                    output.AddOffset(0, -4 * .625f);
                }
                else if (offset == 18)
                {
                    output.AddOffset(0, -1 * .625f);
                }

                if (offset > 0)
                {
                    output.Sprite(input.Sprites.HumansVoreSprites[Math.Min(112 + offset, 138)]);
                    return;
                }

                output.Sprite(input.Sprites.HumansVoreSprites[106 + size]);
            });

            builder.RenderSingle(SpriteType.Weapon, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    switch (input.A.GetWeaponSprite())
                    {
                        case 0:
                            output.Sprite(input.Sprites.HumansBodySprites3[132]);
                            return;
                        case 1:
                            output.Sprite(input.Sprites.HumansBodySprites3[133]);
                            return;
                        case 2:
                            output.Sprite(input.Sprites.HumansBodySprites3[134]);
                            return;
                        case 3:
                            output.Sprite(input.Sprites.HumansBodySprites3[135]);
                            return;
                        case 4:
                            output.Sprite(input.Sprites.HumansBodySprites3[136]);
                            return;
                        case 5:
                            output.Sprite(input.Sprites.HumansBodySprites3[138]);
                            return;
                        case 6:
                            output.Sprite(input.Sprites.HumansBodySprites3[139]);
                            return;
                        case 7:
                            output.Sprite(input.Sprites.HumansBodySprites3[141]);
                            return;
                        default:
                            return;
                    }
                }
            });


            builder.RunBefore((input, output) =>
            {
                if (input.U.HasBreasts)
                {
                    if (input.U.BodySize > 1)
                    {
                        output.ChangeSprite(SpriteType.Balls).AddOffset(0, 3 * .625f);
                        output.ChangeSprite(SpriteType.Belly).AddOffset(0, 1 * .625f);
                    }
                    else
                    {
                        output.ChangeSprite(SpriteType.Balls).AddOffset(0, 3 * .625f);
                        output.ChangeSprite(SpriteType.Belly).AddOffset(0, 1 * .625f);
                    }
                }
                else
                {
                    if (input.U.BodySize > 1)
                    {
                        output.ChangeSprite(SpriteType.Balls).AddOffset(0, 1 * .625f);
                        output.ChangeSprite(SpriteType.Belly).AddOffset(0, 1 * .625f);
                    }
                    else
                    {
                        output.ChangeSprite(SpriteType.Balls).AddOffset(0, 0);
                        output.ChangeSprite(SpriteType.Belly).AddOffset(0, 1 * .625f);
                    }
                }

                if (input.A.GetWeaponSprite() == 0 || input.A.GetWeaponSprite() == 4 ||
                    input.A.GetWeaponSprite() == 6)
                {
                    if (input.U.HasBreasts)
                    {
                        if (input.U.BodySize > 1)
                        {
                            output.ChangeSprite(SpriteType.Weapon).AddOffset(-1 * .625f, 0);
                        }
                        else
                        {
                            output.ChangeSprite(SpriteType.Weapon).AddOffset(0, 0);
                        }
                    }
                    else
                    {
                        if (input.U.BodySize > 1)
                        {
                            output.ChangeSprite(SpriteType.Weapon).AddOffset(1 * .625f, -1 * .625f);
                        }
                        else
                        {
                            output.ChangeSprite(SpriteType.Weapon).AddOffset(0, -1 * .625f);
                        }
                    }
                }
                else if (input.A.GetWeaponSprite() == 1 || input.A.GetWeaponSprite() == 3)
                {
                    if (input.U.HasBreasts)
                    {
                        if (input.U.BodySize > 1)
                        {
                            output.ChangeSprite(SpriteType.Weapon).AddOffset(0, -1 * .625f);
                        }
                        else
                        {
                            output.ChangeSprite(SpriteType.Weapon).AddOffset(0, 0);
                        }
                    }
                    else
                    {
                        if (input.U.BodySize > 1)
                        {
                            output.ChangeSprite(SpriteType.Weapon).AddOffset(3 * .625f, -3 * .625f);
                        }
                        else
                        {
                            output.ChangeSprite(SpriteType.Weapon).AddOffset(3 * .625f, -4 * .625f);
                        }
                    }
                }
                else if (input.A.GetWeaponSprite() == 2)
                {
                    if (input.U.HasBreasts)
                    {
                        if (input.U.BodySize > 1)
                        {
                            output.ChangeSprite(SpriteType.Weapon).AddOffset(-1 * .625f, 2 * .625f);
                        }
                        else
                        {
                            output.ChangeSprite(SpriteType.Weapon).AddOffset(-2 * .625f, 3 * .625f);
                        }
                    }
                    else
                    {
                        if (input.U.BodySize > 1)
                        {
                            output.ChangeSprite(SpriteType.Weapon).AddOffset(0, 0);
                        }
                        else
                        {
                            output.ChangeSprite(SpriteType.Weapon).AddOffset(0, 0);
                        }
                    }
                }
                else if (input.A.GetWeaponSprite() == 5 || input.A.GetWeaponSprite() == 7)
                {
                    if (input.U.HasBreasts)
                    {
                        if (input.U.BodySize > 1)
                        {
                            output.ChangeSprite(SpriteType.Weapon).AddOffset(1 * .625f, -1 * .625f);
                        }
                        else
                        {
                            output.ChangeSprite(SpriteType.Weapon).AddOffset(0, 0);
                        }
                    }
                    else
                    {
                        if (input.U.BodySize > 1)
                        {
                            output.ChangeSprite(SpriteType.Weapon).AddOffset(2 * .625f, -3 * .625f);
                        }
                        else
                        {
                            output.ChangeSprite(SpriteType.Weapon).AddOffset(2 * .625f, -3 * .625f);
                        }
                    }
                }
                
                Defaults.BasicBellyRunAfter.Invoke(input, output);
            });


            builder.RandomCustom(data =>
            {
                Unit unit = data.Unit;
                Defaults.RandomCustom(data);

                if (unit.HasDick && unit.HasBreasts)
                {
                    unit.HairStyle = State.Rand.Next(Config.HermsOnlyUseFemaleHair ? 18 : data.MiscRaceData.HairStyles);
                }
                else if (unit.HasDick && Config.FemaleHairForMales)
                {
                    unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
                }
                else if (unit.HasDick == false && Config.MaleHairForFemales)
                {
                    unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
                }
                else
                {
                    if (unit.HasDick)
                    {
                        unit.HairStyle = 18 + State.Rand.Next(18);
                    }
                    else
                    {
                        unit.HairStyle = State.Rand.Next(18);
                    }
                }

                if (unit.HasBreasts)
                {
                    unit.BeardStyle = 6;
                }
                else
                {
                    unit.BeardStyle = State.Rand.Next(6);
                }
            });
        });


        private static void ClothingShared1(IClothingRenderInput input, IRaceRenderOutput clothing4)
        {
            if (input.U.BodySize == 2)
            {
                if (input.A.HasBelly)
                {
                    if (input.A.GetStomachSize(31, 0.7f) > 12)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[36]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 11)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[35]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 10)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[34]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 9)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[33]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 8)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[32]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 7)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[31]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 6)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[30]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 5)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[29]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[28]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[27]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[26]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[25]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[24]);
                    }
                    else
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[23]);
                    }
                }
                else
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[22]);
                }
            }
            else
            {
                if (input.A.HasBelly)
                {
                    if (input.A.GetStomachSize(31, 0.7f) > 12)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[14]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 11)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[13]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 10)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[12]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 9)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[11]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 8)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[10]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 7)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[9]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 6)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[8]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 5)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[7]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 4)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[6]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 3)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[5]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 2)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[4]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 1)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[3]);
                    }
                    else if (input.A.GetStomachSize(31, 0.7f) > 0)
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[2]);
                    }
                    else
                    {
                        clothing4.Sprite(input.Sprites.HumenFOnePieces[1]);
                    }
                }
                else
                {
                    clothing4.Sprite(input.Sprites.HumenFOnePieces[0]);
                }
            }
        }

        private static ColorSwapPalette FurryColor(Actor_Unit actor)
        {
            if (actor.Unit.Furry)
            {
                return ColorPaletteMap.GetPalette(SwapType.RedFur, actor.Unit.AccessoryColor);
            }

            return ColorPaletteMap.GetPalette(SwapType.RedSkin, actor.Unit.SkinColor);
        }


        private static class GenericTop1
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop1Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenFundertops[57];
                    output.BlocksBreasts = true;
                    output.RevealsBreasts = true;
                    output.FemaleOnly = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.humans/60001");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing3"].Layer(17);
                    output["Clothing2"].Layer(17);
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.HumenFundertops[56]);
                        output.BlocksBreasts = false;
                        output["Clothing2"].Sprite(null);
                        output["Clothing3"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output.BlocksBreasts = true;
                        output["Clothing1"].Sprite(input.Sprites.HumenFundertops[0 + input.U.BreastSize]);
                        if (input.U.BreastSize == 3)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                        }
                        else if (input.U.BreastSize == 4)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                        }
                        else if (input.U.BreastSize == 5)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.U.BreastSize]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.U.BreastSize]);
                        }
                    }
                    else
                    {
                        output.BlocksBreasts = true;
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                    output["Clothing2"].Coloring(FurryColor(input.Actor));
                    output["Clothing3"].Coloring(FurryColor(input.Actor));
                });
            });
        }

        private static class GenericTop2
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop2Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenFundertops[58];
                    output.BlocksBreasts = true;
                    output.RevealsBreasts = true;
                    output.FemaleOnly = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.humans/60002");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing3"].Layer(17);
                    output["Clothing2"].Layer(17);
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                        output.BlocksBreasts = false;
                        output["Clothing2"].Sprite(null);
                        output["Clothing3"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output.BlocksBreasts = true;
                        output["Clothing1"].Sprite(input.Sprites.HumenFundertops[8 + input.U.BreastSize]);
                        if (input.U.BreastSize == 3)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                        }
                        else if (input.U.BreastSize == 4)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                        }
                        else if (input.U.BreastSize == 5)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.U.BreastSize]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.U.BreastSize]);
                        }
                    }
                    else
                    {
                        output.BlocksBreasts = true;
                    }

                    output["Clothing2"].Coloring(FurryColor(input.Actor));
                    output["Clothing3"].Coloring(FurryColor(input.Actor));
                });
            });
        }

        private static class GenericTop3
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop3Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenFundertops[60];
                    output.BlocksBreasts = true;
                    output.RevealsBreasts = true;
                    output.FemaleOnly = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.humans/60003");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing3"].Layer(17);
                    output["Clothing2"].Layer(17);
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.HumenFundertops[59]);
                        output.BlocksBreasts = false;
                        output["Clothing2"].Sprite(null);
                        output["Clothing3"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output.BlocksBreasts = true;
                        output["Clothing1"].Sprite(input.Sprites.HumenFundertops[16 + input.U.BreastSize]);
                        if (input.U.BreastSize == 3)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                        }
                        else if (input.U.BreastSize == 4)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                        }
                        else if (input.U.BreastSize == 5)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.U.BreastSize]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.U.BreastSize]);
                        }
                    }
                    else
                    {
                        output.BlocksBreasts = true;
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                    output["Clothing2"].Coloring(FurryColor(input.Actor));
                    output["Clothing3"].Coloring(FurryColor(input.Actor));
                });
            });
        }

        private static class GenericTop4
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop4Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenFundertops[62];
                    output.BlocksBreasts = true;
                    output.RevealsBreasts = true;
                    output.FemaleOnly = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.humans/60004");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing3"].Layer(17);
                    output["Clothing2"].Layer(17);
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.HumenFundertops[61]);
                        output.BlocksBreasts = false;
                        output["Clothing2"].Sprite(null);
                        output["Clothing3"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output.BlocksBreasts = true;
                        output["Clothing1"].Sprite(input.Sprites.HumenFundertops[24 + input.U.BreastSize]);
                        if (input.U.BreastSize == 3)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                        }
                        else if (input.U.BreastSize == 4)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                        }
                        else if (input.U.BreastSize == 5)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.U.BreastSize]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.U.BreastSize]);
                        }
                    }
                    else
                    {
                        output.BlocksBreasts = true;
                    }

                    output["Clothing2"].Coloring(FurryColor(input.Actor));
                    output["Clothing3"].Coloring(FurryColor(input.Actor));
                });
            });
        }

        private static class GenericTop5
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop5Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenFundertops[64];
                    output.BlocksBreasts = true;
                    output.RevealsBreasts = true;
                    output.FemaleOnly = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.humans/60005");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing3"].Layer(17);
                    output["Clothing2"].Layer(17);
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.HumenFundertops[63]);
                        output.BlocksBreasts = false;
                        output["Clothing2"].Sprite(null);
                        output["Clothing3"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output.BlocksBreasts = true;
                        output["Clothing1"].Sprite(input.Sprites.HumenFundertops[32 + input.U.BreastSize]);
                        if (input.U.BreastSize == 3)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                        }
                        else if (input.U.BreastSize == 4)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                        }
                        else if (input.U.BreastSize == 5)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.U.BreastSize]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.U.BreastSize]);
                        }
                    }
                    else
                    {
                        output.BlocksBreasts = true;
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                    output["Clothing2"].Coloring(FurryColor(input.Actor));
                    output["Clothing3"].Coloring(FurryColor(input.Actor));
                });
            });
        }

        private static class GenericTop6
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop6Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenFundertops[66];
                    output.BlocksBreasts = true;
                    output.RevealsBreasts = true;
                    output.FemaleOnly = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.humans/60006");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing3"].Layer(17);
                    output["Clothing2"].Layer(17);
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.HumenFundertops[65]);
                        output.BlocksBreasts = false;
                        output["Clothing2"].Sprite(null);
                        output["Clothing3"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output.BlocksBreasts = true;
                        output["Clothing1"].Sprite(input.Sprites.HumenFundertops[40 + input.U.BreastSize]);
                        if (input.U.BreastSize == 3)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                        }
                        else if (input.U.BreastSize == 4)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                        }
                        else if (input.U.BreastSize == 5)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.U.BreastSize]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.U.BreastSize]);
                        }
                    }
                    else
                    {
                        output.BlocksBreasts = true;
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                    output["Clothing2"].Coloring(FurryColor(input.Actor));
                    output["Clothing3"].Coloring(FurryColor(input.Actor));
                });
            });
        }

        private static class GenericTop7
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop7Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenFundertops[68];
                    output.BlocksBreasts = true;
                    output.RevealsBreasts = true;
                    output.FemaleOnly = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.humans/60007");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing3"].Layer(17);
                    output["Clothing2"].Layer(17);
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.HumenFundertops[67]);
                        output.BlocksBreasts = false;
                        output["Clothing2"].Sprite(null);
                        output["Clothing3"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output.BlocksBreasts = true;
                        output["Clothing1"].Sprite(input.Sprites.HumenFundertops[48 + input.U.BreastSize]);
                        if (input.U.BreastSize == 3)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                        }
                        else if (input.U.BreastSize == 4)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                        }
                        else if (input.U.BreastSize == 5)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.U.BreastSize]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.U.BreastSize]);
                        }
                    }
                    else
                    {
                        output.BlocksBreasts = true;
                    }

                    output["Clothing2"].Coloring(FurryColor(input.Actor));
                    output["Clothing3"].Coloring(FurryColor(input.Actor));
                });
            });
        }

        private static class MaleTop
        {
            internal static readonly IClothing MaleTopInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenMundertops[5];
                    output.MaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.humans/60008");
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Sprite(input.Sprites.HumenMundertops[0]);
                });
            });
        }

        private static class MaleTop2
        {
            internal static readonly IClothing MaleTop2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenMundertops[5];
                    output.MaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.humans/60009");
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);

                    output["Clothing1"].Coloring(Color.white);

                    output["Clothing1"].Sprite(input.A.HasBelly ? input.Sprites.HumenMundertops[4] : input.Sprites.HumenMundertops[1 + input.U.BodySize]);
                });
            });
        }

        private static class MaleTop3
        {
            internal static readonly IClothing MaleTop3Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenMundertops[11];
                    output.MaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.humans/60010");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Sprite(input.Sprites.HumenMundertops[6]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });
        }

        private static class MaleTop4
        {
            internal static readonly IClothing MaleTop4Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenMundertops[11];
                    output.MaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.humans/60011");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);

                    output["Clothing1"].Sprite(input.A.HasBelly ? input.Sprites.HumenMundertops[10] : input.Sprites.HumenMundertops[7 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });
        }

        private static class MaleTop5
        {
            internal static readonly IClothing MaleTop5Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenMundertops[14];
                    output.MaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.humans/60012");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);

                    output["Clothing1"].Sprite(input.U.BodySize == 2 ? input.Sprites.HumenMundertops[13] : input.Sprites.HumenMundertops[12]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });
        }

        private static class MaleTop6
        {
            internal static readonly IClothing MaleTop6Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenMundertops[16];
                    output.MaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.humans/60013");
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);

                    output["Clothing1"].Coloring(Color.white);

                    output["Clothing1"].Sprite(input.Sprites.HumenMundertops[15]);
                });
            });
        }

        private static class Uniform1
        {
            internal static readonly BindableClothing<IOverSizeParameters> Uniform1Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenUniform1[42];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.humans/60025");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing3"].Layer(5);
                    output["Clothing2"].Layer(15);
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.HumenUniform2[6]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.U.BreastSize > 5 ? input.Sprites.HumenUniform2[6] : input.Sprites.HumenUniform2[0 + input.U.BreastSize]);
                    }
                
                    if (input.A.HasBelly)
                    {
                        if (input.A.GetStomachSize(31, 0.7f) > 4)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumenUniform2[13 + 7 * input.U.BodySize + 21 * (!input.U.HasBreasts ? 1 : 0)]);
                        }
                        else if (input.A.GetStomachSize(31, 0.7f) > 3)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumenUniform2[12 + 7 * input.U.BodySize + 21 * (!input.U.HasBreasts ? 1 : 0)]);
                        }
                        else if (input.A.GetStomachSize(31, 0.7f) > 2)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumenUniform2[11 + 7 * input.U.BodySize + 21 * (!input.U.HasBreasts ? 1 : 0)]);
                        }
                        else if (input.A.GetStomachSize(31, 0.7f) > 1)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumenUniform2[10 + 7 * input.U.BodySize + 21 * (!input.U.HasBreasts ? 1 : 0)]);
                        }
                        else if (input.A.GetStomachSize(31, 0.7f) > 0)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumenUniform2[9 + 7 * input.U.BodySize + 21 * (!input.U.HasBreasts ? 1 : 0)]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumenUniform2[8 + 7 * input.U.BodySize + 21 * (!input.U.HasBreasts ? 1 : 0)]);
                        }
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.HumenUniform2[7 + 7 * input.U.BodySize + 21 * (!input.U.HasBreasts ? 1 : 0)]);
                    }

                    if (input.U.HasWeapon == false)
                    {
                        if (input.A.IsAttacking)
                        {
                            output["Clothing3"].Sprite(input.Sprites.HumenUniform1[3 + 4 * input.U.BodySize + 12 * (!input.U.HasBreasts ? 1 : 0)]);
                        }
                        else
                        {
                            output["Clothing3"].Sprite(input.Sprites.HumenUniform1[0 + 4 * input.U.BodySize + 12 * (!input.U.HasBreasts ? 1 : 0)]);
                        }
                    }
                    else if (input.A.GetWeaponSprite() == 0 || input.A.GetWeaponSprite() == 4 || input.A.GetWeaponSprite() == 6)
                    {
                        output["Clothing3"].Sprite(input.Sprites.HumenUniform1[2 + 4 * input.U.BodySize + 12 * (!input.U.HasBreasts ? 1 : 0)]);
                    }
                    else if (input.A.GetWeaponSprite() == 1 || input.A.GetWeaponSprite() == 3)
                    {
                        output["Clothing3"].Sprite(input.Sprites.HumenUniform1[3 + 4 * input.U.BodySize + 12 * (!input.U.HasBreasts ? 1 : 0)]);
                    }
                    else
                    {
                        output["Clothing3"].Sprite(input.Sprites.HumenUniform1[1 + 4 * input.U.BodySize + 12 * (!input.U.HasBreasts ? 1 : 0)]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                    output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });
        }

        private static class FemaleOnePiece1
        {
            internal static readonly BindableClothing<IOverSizeParameters> FemaleOnePiece1Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenFOnePieces[81];
                    output.BlocksBreasts = true;
                    output.RevealsBreasts = true;
                    output.FemaleOnly = true;
                    output.OccupiesAllSlots = true;
                    output.ClothingId = new ClothingId("base.humans/60014");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing4"].Layer(15);
                    output["Clothing3"].Layer(17);
                    output["Clothing2"].Layer(17);
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.HumenFOnePieces[51]);
                        output.BlocksBreasts = false;
                        output["Clothing2"].Sprite(null);
                        output["Clothing3"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output.BlocksBreasts = true;
                        output["Clothing1"].Sprite(input.Sprites.HumenFOnePieces[43 + input.U.BreastSize]);
                        if (input.U.BreastSize == 3)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                        }
                        else if (input.U.BreastSize == 4)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                        }
                        else if (input.U.BreastSize == 5)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.U.BreastSize]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.U.BreastSize]);
                        }
                    }
                    else
                    {
                        output.BlocksBreasts = true;
                    }

                    if (input.U.BodySize == 2)
                    {
                        if (input.A.HasBelly)
                        {
                            if (input.A.GetStomachSize(31, 0.7f) > 3)
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[42]);
                            }
                            else if (input.A.GetStomachSize(31, 0.7f) > 2)
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[41]);
                            }
                            else if (input.A.GetStomachSize(31, 0.7f) > 1)
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[40]);
                            }
                            else if (input.A.GetStomachSize(31, 0.7f) > 0)
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[39]);
                            }
                            else
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[38]);
                            }
                        }
                        else
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[37]);
                        }
                    }
                    else
                    {
                        if (input.A.HasBelly)
                        {
                            if (input.A.GetStomachSize(31, 0.7f) > 4)
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[21]);
                            }
                            else if (input.A.GetStomachSize(31, 0.7f) > 3)
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[20]);
                            }
                            else if (input.A.GetStomachSize(31, 0.7f) > 2)
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[19]);
                            }
                            else if (input.A.GetStomachSize(31, 0.7f) > 1)
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[18]);
                            }
                            else if (input.A.GetStomachSize(31, 0.7f) > 0)
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[17]);
                            }
                            else
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[16]);
                            }
                        }
                        else
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[15]);
                        }
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                    output["Clothing2"].Coloring(FurryColor(input.Actor));
                    output["Clothing3"].Coloring(FurryColor(input.Actor));
                    output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });
        }

        private static class FemaleOnePiece2
        {
            internal static readonly BindableClothing<IOverSizeParameters> FemaleOnePiece2Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenFOnePieces[80];
                    output.BlocksBreasts = true;
                    output.RevealsBreasts = true;
                    output.FemaleOnly = true;
                    output.OccupiesAllSlots = true;
                    output.ClothingId = new ClothingId("base.humans/60015");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing4"].Layer(15);
                    output["Clothing3"].Layer(17);
                    output["Clothing2"].Layer(17);
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                        output.BlocksBreasts = false;
                        output["Clothing2"].Sprite(null);
                        output["Clothing3"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output.BlocksBreasts = true;
                        output["Clothing1"].Sprite(input.Sprites.HumenFOnePieces[52 + input.U.BreastSize]);
                        if (input.U.BreastSize == 3)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                        }
                        else if (input.U.BreastSize == 4)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                        }
                        else if (input.U.BreastSize == 5)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.U.BreastSize]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.U.BreastSize]);
                        }
                    }
                    else
                    {
                        output.BlocksBreasts = true;
                    }

                    ClothingShared1(input, output["Clothing4"]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                    output["Clothing2"].Coloring(FurryColor(input.Actor));
                    output["Clothing3"].Coloring(FurryColor(input.Actor));
                    output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });
        }

        private static class FemaleOnePiece3
        {
            internal static readonly BindableClothing<IOverSizeParameters> FemaleOnePiece3Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenFOnePieces[79];
                    output.BlocksBreasts = true;
                    output.RevealsBreasts = true;
                    output.FemaleOnly = true;
                    output.OccupiesAllSlots = true;
                    output.ClothingId = new ClothingId("base.humans/60016");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing4"].Layer(15);
                    output["Clothing3"].Layer(17);
                    output["Clothing2"].Layer(17);
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.HumenFOnePieces[69]);
                        output.BlocksBreasts = false;
                        output["Clothing2"].Sprite(null);
                        output["Clothing3"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output.BlocksBreasts = true;
                        output["Clothing1"].Sprite(input.Sprites.HumenFOnePieces[61 + input.U.BreastSize]);
                        if (input.U.BreastSize == 3)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                        }
                        else if (input.U.BreastSize == 4)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                        }
                        else if (input.U.BreastSize == 5)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.U.BreastSize]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.U.BreastSize]);
                        }
                    }
                    else
                    {
                        output.BlocksBreasts = true;
                    }

                    ClothingShared1(input, output["Clothing4"]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                    output["Clothing2"].Coloring(FurryColor(input.Actor));
                    output["Clothing3"].Coloring(FurryColor(input.Actor));
                    output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });
        }

        private static class FemaleOnePiece4
        {
            internal static readonly BindableClothing<IOverSizeParameters> FemaleOnePiece4Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenFOnePieces[82];
                    output.BlocksBreasts = true;
                    output.RevealsBreasts = true;
                    output.FemaleOnly = true;
                    output.OccupiesAllSlots = true;
                    output.ClothingId = new ClothingId("base.humans/60017");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing4"].Layer(15);
                    output["Clothing3"].Layer(17);
                    output["Clothing2"].Layer(17);
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.HumenFOnePieces[78]);
                        output.BlocksBreasts = false;
                        output["Clothing2"].Sprite(null);
                        output["Clothing3"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output.BlocksBreasts = true;
                        output["Clothing1"].Sprite(input.Sprites.HumenFOnePieces[70 + input.U.BreastSize]);
                        if (input.U.BreastSize == 3)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[64]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[67]);
                        }
                        else if (input.U.BreastSize == 4)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[65]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[68]);
                        }
                        else if (input.U.BreastSize == 5)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[66]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[69]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumansVoreSprites[0 + input.U.BreastSize]);
                            output["Clothing3"].Sprite(input.Sprites.HumansVoreSprites[32 + input.U.BreastSize]);
                        }
                    }
                    else
                    {
                        output.BlocksBreasts = true;
                    }

                    if (input.U.BodySize == 2)
                    {
                        if (input.A.HasBelly)
                        {
                            if (input.A.GetStomachSize(31, 0.7f) > 3)
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[42]);
                            }
                            else if (input.A.GetStomachSize(31, 0.7f) > 2)
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[41]);
                            }
                            else if (input.A.GetStomachSize(31, 0.7f) > 1)
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[40]);
                            }
                            else if (input.A.GetStomachSize(31, 0.7f) > 0)
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[39]);
                            }
                            else
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[38]);
                            }
                        }
                        else
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[37]);
                        }
                    }
                    else
                    {
                        if (input.A.HasBelly)
                        {
                            if (input.A.GetStomachSize(31, 0.7f) > 4)
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[21]);
                            }
                            else if (input.A.GetStomachSize(31, 0.7f) > 3)
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[20]);
                            }
                            else if (input.A.GetStomachSize(31, 0.7f) > 2)
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[19]);
                            }
                            else if (input.A.GetStomachSize(31, 0.7f) > 1)
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[18]);
                            }
                            else if (input.A.GetStomachSize(31, 0.7f) > 0)
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[17]);
                            }
                            else
                            {
                                output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[16]);
                            }
                        }
                        else
                        {
                            output["Clothing4"].Sprite(input.Sprites.HumenFOnePieces[15]);
                        }
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                    output["Clothing2"].Coloring(FurryColor(input.Actor));
                    output["Clothing3"].Coloring(FurryColor(input.Actor));
                    output["Clothing4"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });
        }

        private static class GenericBot1
        {
            internal static readonly IClothing GenericBot1Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenUnderbottoms[6];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.humans/60018");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(12);
                    output["Clothing1"].Layer(13);
                    if (input.U.DickSize > 0)
                    {
                        output["Clothing1"].Sprite(input.U.DickSize < 4 ? input.Sprites.HumenUnderbottoms[60] : input.Sprites.HumenUnderbottoms[61]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(null);
                    }

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.HumenUnderbottoms[0 + input.U.BodySize] : input.Sprites.HumenUnderbottoms[3 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });
        }

        private static class GenericBot2
        {
            internal static readonly IClothing GenericBot2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenUnderbottoms[13];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.humans/60019");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(12);
                    output["Clothing1"].Layer(13);
                    if (input.U.DickSize > 0)
                    {
                        if (input.U.DickSize < 4)
                        {
                            output["Clothing1"].Sprite(input.Sprites.HumenUnderbottoms[60]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.HumenUnderbottoms[61]);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(null);
                    }

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.HumenUnderbottoms[7 + input.U.BodySize] : input.Sprites.HumenUnderbottoms[10 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });
        }

        private static class GenericBot3
        {
            internal static readonly IClothing GenericBot3Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenUnderbottoms[26];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.humans/60020");
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(12);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(13);
                    output["Clothing1"].Coloring(Color.white);
                    if (input.U.DickSize > 0)
                    {
                        output["Clothing1"].Sprite(input.U.DickSize < 4 ? input.Sprites.HumenUnderbottoms[62] : input.Sprites.HumenUnderbottoms[63]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(null);
                    }

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.HumenUnderbottoms[20 + input.U.BodySize] : input.Sprites.HumenUnderbottoms[23 + input.U.BodySize]);
                });
            });
        }

        private static class GenericBot4
        {
            internal static readonly IClothing GenericBot4Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenUnderbottoms[39];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.humans/60021");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(12);
                    output["Clothing1"].Layer(13);
                    if (input.U.DickSize > 0)
                    {
                        output["Clothing1"].Sprite(input.U.DickSize < 4 ? input.Sprites.HumenUnderbottoms[60] : input.Sprites.HumenUnderbottoms[61]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(null);
                    }

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.HumenUnderbottoms[33 + input.U.BodySize] : input.Sprites.HumenUnderbottoms[36 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });
        }

        private static class GenericBot5
        {
            internal static readonly IClothing GenericBot5Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenUnderbottoms[52];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.humans/60022");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(12);
                    output["Clothing1"].Layer(13);
                    if (input.U.DickSize > 0)
                    {
                        output["Clothing1"].Sprite(input.U.DickSize < 4 ? input.Sprites.HumenUnderbottoms[60] : input.Sprites.HumenUnderbottoms[61]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(null);
                    }

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.HumenUnderbottoms[46 + input.U.BodySize] : input.Sprites.HumenUnderbottoms[49 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });
        }

        private static class GenericBot6
        {
            internal static readonly IClothing GenericBot6Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenUnderbottoms[59];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.humans/60023");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(12);
                    output["Clothing1"].Layer(13);
                    if (input.U.DickSize > 0)
                    {
                        output["Clothing1"].Sprite(input.U.DickSize < 4 ? input.Sprites.HumenUnderbottoms[60] : input.Sprites.HumenUnderbottoms[61]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(null);
                    }

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.HumenUnderbottoms[53 + input.U.BodySize] : input.Sprites.HumenUnderbottoms[56 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });
        }

        private static class Uniform2
        {
            internal static readonly IClothing Uniform2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenUniform1[43];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.humans/60024");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(12);
                    output["Clothing1"].Sprite(input.U.HasBreasts ? input.Sprites.HumenUniform1[24 + input.U.BodySize] : input.Sprites.HumenUniform1[33 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });
        }

        private static class BigLoin
        {
            internal static readonly IClothing BigLoinInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenBigLoin[12];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.humans/60026");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(12);
                    if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.HumenBigLoin[0 + 2 * input.U.BodySize + (input.A.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.HumenBigLoin[6 + 2 * input.U.BodySize + (input.A.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });
        }

        private static class Pants1
        {
            internal static readonly IClothing Pants1Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenPants[28];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.humans/60027");
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(13);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(12);
                    output["Clothing1"].Coloring(Color.white);
                    if (input.U.HasBreasts)
                    {
                        if (input.U.DickSize > 0)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumenPants[24 + (input.A.GetStomachSize(31, 0.7f) > 3 ? 2 : 0)]);
                        }

                        output["Clothing1"].Sprite(input.Sprites.HumenPants[0 + 2 * input.U.BodySize + (input.A.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.HumenPants[6 + 2 * input.U.BodySize + (input.A.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)]);
                        output["Clothing2"].Sprite(input.Sprites.HumenPants[25 + (input.A.GetStomachSize(31, 0.7f) > 3 ? 2 : 0)]);
                    }
                });
            });
        }

        private static class Pants2
        {
            internal static readonly IClothing Pants2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenPants[33];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.humans/60028");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(13);
                    output["Clothing1"].Layer(12);
                    if (input.U.HasBreasts)
                    {
                        if (input.U.DickSize > 0)
                        {
                            output["Clothing2"].Sprite(input.Sprites.HumenPants[29 + (input.A.GetStomachSize(31, 0.7f) > 3 ? 2 : 0)]);
                        }

                        output["Clothing1"].Sprite(input.Sprites.HumenPants[12 + 2 * input.U.BodySize + (input.A.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.HumenPants[18 + 2 * input.U.BodySize + (input.A.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)]);
                        output["Clothing2"].Sprite(input.Sprites.HumenPants[30 + (input.A.GetStomachSize(31, 0.7f) > 3 ? 2 : 0)]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });
        }

        private static class Skirt
        {
            internal static readonly IClothing SkirtInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.HumenSkirt[6];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.humans/60029");
                    output.FemaleOnly = true;
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(12);
                    output["Clothing1"].Sprite(input.Sprites.HumenSkirt[0 + 2 * input.U.BodySize + (input.A.GetStomachSize(31, 0.7f) > 3 ? 1 : 0)]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.Clothing50Spaced, input.U.ClothingColor));
                });
            });
        }
    }
}
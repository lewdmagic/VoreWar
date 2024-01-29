#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.MainRaces
{
    internal static class Deer
    {

        private static Func<IClothingRenderInput, IOverSizeParameters> paramsCalc = CommonRaceCode.MakeOversizeFunc(29 * 29);
        
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            IClothing leaderClothes1 = DeerLeader1.DeerLeader1Instance.Create(paramsCalc);
            IClothing leaderClothes2 = DeerLeader2.DeerLeader2Instance;
            IClothing leaderClothes3 = DeerLeader3.DeerLeader3Instance;
            IClothing rags = DeerRags.DeerRagsInstance;


            builder.Setup(output =>
            {
                output.Names("Deer", "Deer");
                output.FlavorText(new FlavorText(
                    new Texts {  },
                    new Texts {  },
                    new Texts { "faun", "deer", {"doe", Gender.Female, 0.5/5 }, {"roe", Gender.Female, 0.5/5 }, {"buck", Gender.Male, 0.5/5 }, {"stag", Gender.Male, 0.5/5 }, {"hart", Gender.Male, 0.5/5 } } 
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 10,
                    StomachSize = 15,
                    HasTail = true,
                    FavoredStat = Stat.Agility,
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.EvasiveBattler,
                        TraitType.ArtfulDodge,
                        TraitType.PackDefense,
                    },
                    RaceDescription = "",
                });
                output.CustomizeButtons((unit, buttons) =>
                {
                    buttons.SetText(ButtonType.BodyAccessoryType, "Ear Type");
                    buttons.SetText(ButtonType.BodyAccentTypes1, "Antlers Type");
                    buttons.SetText(ButtonType.BodyAccentTypes2, "Body Pattern Type");
                    buttons.SetText(ButtonType.BodyAccentTypes3, "Leg Type");
                });
                output.TownNames(new List<string>
                {
                    "Artemis Woods",
                    "The Golden Stag",
                    "Elkfurt",
                    "Hoovechester",
                    "Buckville",
                    "The White Doe",
                    "Cernunnos",
                    "Dappled Hide",
                    "Ceryneia",
                    "Hindburg",
                    "Peryton",
                    "Antlertown",
                    "Swiftbrook",
                    "Red Hart",
                    "Eikthyrnir",
                    "Actaeon",
                    "Darbywood",
                    "Rohit",
                    "Furfur",
                    "Achlis"
                });
                output.DickSizes = () => 8;
                output.BreastSizes = () => 8;

                output.BodySizes = 4;
                output.EyeTypes = 5;
                output.SpecialAccessoryCount = 12; // ears     
                output.HairStyles = 25;
                output.MouthTypes = 6;
                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.DeerLeaf);
                output.HairColors = ColorPaletteMap.GetPaletteCount(SwapType.UniversalHair);
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.DeerSkin);
                output.BodyAccentTypes1 = 12; // antlers
                output.BodyAccentTypes2 = 7; // pattern types
                output.BodyAccentTypes3 = 2; // leg types

                output.ExtendedBreastSprites = true;
                output.FurCapable = true;

                output.AllowedMainClothingTypes.Set(
                    GenericTop1.GenericTop1InstanceV2.Create(paramsCalc),
                    //GenericTop1.GenericTop1Instance,
                    GenericTop2.GenericTop2Instance.Create(paramsCalc),
                    GenericTop3.GenericTop3Instance.Create(paramsCalc),
                    GenericTop4.GenericTop4Instance.Create(paramsCalc),
                    GenericTop5.GenericTop5Instance.Create(paramsCalc),
                    GenericTop6.GenericTop6Instance.Create(paramsCalc),
                    GenericTop7.GenericTop7Instance.Create(paramsCalc),
                    MaleTop.MaleTopInstance,
                    MaleTop2.MaleTop2Instance,
                    Natural.NaturalInstance.Create(paramsCalc),
                    Cuirass.CuirassInstance.Create(paramsCalc),
                    Special1.Special1Instance.Create(paramsCalc),
                    Special2.Special2Instance,
                    rags,
                    leaderClothes1,
                    leaderClothes2
                );
                output.AvoidedMainClothingTypes = 3;
                output.AvoidedEyeTypes = 0;
                output.AllowedWaistTypes.Set(
                    GenericBot1.GenericBot1Instance,
                    GenericBot2.GenericBot2Instance,
                    GenericBot3.GenericBot3Instance,
                    GenericBot4.GenericBot4Instance,
                    GenericBot5.GenericBot5Instance,
                    Loincloth.LoinclothInstance,
                    leaderClothes3
                );
                output.ExtraMainClothing1Types.Set(
                    Scarf.ScarfInstance,
                    Necklace.NecklaceInstance
                );

                output.ClothingColors = ColorPaletteMap.GetPaletteCount(SwapType.AviansSkin);
            });


            builder.RunBefore((input, output) =>
            {
                Defaults.BasicBellyRunAfter.Invoke(input, output);
            });


            builder.RenderSingle(SpriteType.Head, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DeerSkin, input.U.SkinColor));
                if (input.U.Furry)
                {
                    if (input.U.HasBreasts)
                    {
                        output.Sprite(input.A.IsEating ? input.Sprites.Deer1[53] : input.Sprites.Deer1[52]);
                    }
                    else
                    {
                        output.Sprite(input.A.IsEating ? input.Sprites.Deer1[55] : input.Sprites.Deer1[54]);
                    }
                }
                else
                {
                    if (input.U.HasBreasts)
                    {
                        output.Sprite(input.A.IsEating ? input.Sprites.Deer1[49] : input.Sprites.Deer1[48]);
                    }
                    else
                    {
                        output.Sprite(input.A.IsEating ? input.Sprites.Deer1[51] : input.Sprites.Deer1[50]);
                    }
                }
            });

            builder.RenderSingle(SpriteType.Eyes, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
                if (input.U.Furry)
                {
                    output.AddOffset(0, 1 * .625f);
                }

                output.Sprite(input.Sprites.Deer1[71 + input.U.EyeType]);
            });
            builder.RenderSingle(SpriteType.Mouth, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.Furry)
                {
                    if (input.A.IsEating)
                    {
                        output.Sprite(input.Sprites.Deer1[60]);
                    }
                    else if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Deer1[62]);
                    }
                }
                else
                {
                    if (input.A.IsEating)
                    {
                        output.Sprite(input.Sprites.Deer1[56]);
                    }
                    else if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Deer1[58]);
                    }
                }
            });

            builder.RenderSingle(SpriteType.Hair, 21, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.UniversalHair, input.U.HairColor));
                if (input.U.HairStyle == 24 || input.U.Furry)
                {
                }
                else
                {
                    output.Sprite(input.Sprites.Deer1[84 + input.U.HairStyle]);
                }
            });

            builder.RenderSingle(SpriteType.Hair2, 0, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.UniversalHair, input.U.HairColor));
                if (input.U.HairStyle == 24 || input.U.Furry)
                {
                }
                else
                {
                    output.Sprite(input.Sprites.Deer1[108 + input.U.HairStyle]);
                }
            });

            builder.RenderSingle(SpriteType.Hair3, 8, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.UniversalHair, input.U.HairColor));
                if (input.U.Furry)
                {
                }
                else
                {
                    output.Sprite(input.U.HasBreasts ? input.Sprites.Deer1[132] : input.Sprites.Deer1[133]);
                }
            }); // Eyebrows

            builder.RenderSingle(SpriteType.Body, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DeerSkin, input.U.SkinColor));
                output.Sprite(input.U.HasBreasts ? input.Sprites.Deer1[0 + input.U.BodySize] : input.Sprites.Deer1[12 + input.U.BodySize]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DeerSkin, input.U.SkinColor));
                if (input.U.HasWeapon == false)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Deer1[6 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 12 : 0)]);
                        return;
                    }

                    output.Sprite(input.Sprites.Deer1[4 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 12 : 0)]);
                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Deer1[5 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 12 : 0)]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Deer1[6 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 12 : 0)]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Deer1[5 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 12 : 0)]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Deer1[6 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 12 : 0)]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.Deer1[4 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 12 : 0)]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.Deer1[5 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 12 : 0)]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Deer1[4 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 12 : 0)]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.Deer1[5 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 12 : 0)]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Deer1[4 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 12 : 0)]);
                        return;
                }
            }); // Right Arm

            builder.RenderSingle(SpriteType.BodyAccent2, 2, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DeerSkin, input.U.SkinColor));
                if (input.U.HasWeapon == false)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Deer1[23]);
                        return;
                    }

                    output.Sprite(input.Sprites.Deer1[10]);
                    return;
                }

                if (!input.U.HasBreasts)
                {
                    output.AddOffset(2 * .625f, 0);
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Deer1[22]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Deer1[23]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Deer1[22]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Deer1[23]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.Deer1[11]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.Deer1[22]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Deer1[11]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.Deer1[22]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Deer1[10]);
                        return;
                }
            }); // Right Hand

            builder.RenderSingle(SpriteType.BodyAccent3, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DeerSkin, input.U.SkinColor));
                if (input.U.BodyAccentType2 >= 6) //Changed to >= to hopefully prevent a rare exception
                {
                }
                else if (input.U.HasBreasts)
                {
                    output.Sprite(input.Sprites.Deer2[0 + input.U.BodySize + 20 * input.U.BodyAccentType2]);
                }
                else
                {
                    output.Sprite(input.Sprites.Deer2[10 + input.U.BodySize + 20 * input.U.BodyAccentType2]);
                }
            }); // Body Pattern

            builder.RenderSingle(SpriteType.BodyAccent4, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DeerSkin, input.U.SkinColor));
                if (input.U.BodyAccentType2 == 6)
                {
                    return;
                }

                if (input.U.HasWeapon == false)
                {
                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Deer2[6 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 10 : 0) + 20 * input.U.BodyAccentType2]);
                        return;
                    }

                    output.Sprite(input.Sprites.Deer2[4 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 10 : 0) + 20 * input.U.BodyAccentType2]);
                    return;
                }

                switch (input.A.GetWeaponSprite())
                {
                    case 0:
                        output.Sprite(input.Sprites.Deer2[5 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 10 : 0) + 20 * input.U.BodyAccentType2]);
                        return;
                    case 1:
                        output.Sprite(input.Sprites.Deer2[6 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 10 : 0) + 20 * input.U.BodyAccentType2]);
                        return;
                    case 2:
                        output.Sprite(input.Sprites.Deer2[5 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 10 : 0) + 20 * input.U.BodyAccentType2]);
                        return;
                    case 3:
                        output.Sprite(input.Sprites.Deer2[6 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 10 : 0) + 20 * input.U.BodyAccentType2]);
                        return;
                    case 4:
                        output.Sprite(input.Sprites.Deer2[4 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 10 : 0) + 20 * input.U.BodyAccentType2]);
                        return;
                    case 5:
                        output.Sprite(input.Sprites.Deer2[5 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 10 : 0) + 20 * input.U.BodyAccentType2]);
                        return;
                    case 6:
                        output.Sprite(input.Sprites.Deer2[4 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 10 : 0) + 20 * input.U.BodyAccentType2]);
                        return;
                    case 7:
                        output.Sprite(input.Sprites.Deer2[5 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 10 : 0) + 20 * input.U.BodyAccentType2]);
                        return;
                    default:
                        output.Sprite(input.Sprites.Deer2[4 + (input.U.BodySize > 1 ? 3 : 0) + (!input.U.HasBreasts ? 10 : 0) + 20 * input.U.BodyAccentType2]);
                        return;
                }
            }); // Arm Pattern

            builder.RenderSingle(SpriteType.BodyAccent5, 7, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.Furry)
                {
                    output.Sprite(input.U.HasBreasts ? input.Sprites.Deer1[136] : input.Sprites.Deer1[137]);
                }
                else
                {
                    output.Sprite(input.Sprites.Deer1[135]);
                }
            }); // Nose

            builder.RenderSingle(SpriteType.BodyAccent6, 6, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Deer1[134]);
            }); // Hoofs
            builder.RenderSingle(SpriteType.BodyAccent7, 8, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DeerSkin, input.U.SkinColor));
                if (input.U.Furry)
                {
                    if (input.A.IsEating)
                    {
                        output.Sprite(input.Sprites.Deer1[61]);
                        return;
                    }

                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Deer1[63]);
                        return;
                    }

                    output.Sprite(input.Sprites.Deer1[70]);
                }
                else
                {
                    if (input.A.IsEating)
                    {
                        output.Sprite(input.Sprites.Deer1[57]);
                        return;
                    }

                    if (input.A.IsAttacking)
                    {
                        output.Sprite(input.Sprites.Deer1[59]);
                        return;
                    }

                    output.Sprite(input.Sprites.Deer1[64 + input.U.MouthType]);
                }
            }); // Mouth external

            builder.RenderSingle(SpriteType.BodyAccent8, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DeerSkin, input.U.SkinColor));
                if (input.U.BodyAccentType3 == 1)
                {
                }
                else if (input.U.HasBreasts)
                {
                    output.Sprite(input.Sprites.Deer4[88 + input.U.BodySize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Deer4[92 + input.U.BodySize]);
                }
            }); // alternative legs

            builder.RenderSingle(SpriteType.BodyAccessory, 20, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DeerSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Deer1[24 + input.U.SpecialAccessoryType]);
            }); // Ears
            builder.RenderSingle(SpriteType.SecondaryAccessory, 22, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (!input.U.HasDick)
                {
                }
                else
                {
                    output.Sprite(input.Sprites.Deer1[36 + input.U.BodyAccentType1]);
                }
            }); // Antlers

            builder.RenderSingle(SpriteType.Breasts, 17, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DeerSkin, input.U.SkinColor));
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

                    output.Sprite(input.Sprites.Cockatrice2[0 + leftSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Cockatrice2[0 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.SecondaryBreasts, 17, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DeerSkin, input.U.SkinColor));
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

                    output.Sprite(input.Sprites.Cockatrice2[32 + rightSize]);
                }
                else
                {
                    output.Sprite(input.Sprites.Cockatrice2[32 + input.U.BreastSize]);
                }
            });

            builder.RenderSingle(SpriteType.Belly, 14, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DeerSkin, input.U.SkinColor));
                output.AddOffset(0, -1 * .625f);

                if (input.A.HasBelly)
                {
                    int size = input.A.GetStomachSize(31, 0.7f);
                
                    switch (size)
                    {
                        case 26:
                            output.AddOffset(0, -3 * .625f);
                            break;
                        case 27:
                            output.AddOffset(0, -8 * .625f);
                            break;
                        case 28:
                            output.AddOffset(0, -13 * .625f);
                            break;
                        case 29:
                            output.AddOffset(0, -16 * .625f);
                            break;
                        case 30:
                            output.AddOffset(0, -22 * .625f);
                            break;
                        case 31:
                            output.AddOffset(0, -28 * .625f);
                            break;
                    }

                    output.Sprite(input.Sprites.Cockatrice2[64 + size]);
                }
            });

            builder.RenderSingle(SpriteType.Dick, 11, (input, output) =>
            {
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (!input.U.Furry || !Config.FurryGenitals)
                {
                    output.Coloring(ColorPaletteMap.GetPalette(SwapType.DeerSkin, input.U.SkinColor));
                }

                if (input.U.Furry && Config.FurryGenitals)
                {
                    output.AddOffset(0, -3 * .625f);

                    if (input.A.IsErect())
                    {
                        if (input.A.PredatorComponent?.VisibleFullness < .75f &&
                            (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize +
                                           input.A.GetRightBreastSize(32 * 32)) < 16 &&
                            (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize +
                                           input.A.GetLeftBreastSize(32 * 32)) < 16)
                        {
                            output.Layer(24);
                            if (input.A.IsCockVoring)
                            {
                                output.Sprite(input.Sprites.Deer3[54 + input.U.DickSize]);
                                return;
                            }

                            output.Sprite(input.Sprites.Deer3[38 + input.U.DickSize]);
                            return;
                        }

                        output.Layer(13);
                        if (input.A.IsCockVoring)
                        {
                            output.Sprite(input.Sprites.Deer3[62 + input.U.DickSize]);
                            return;
                        }

                        output.Sprite(input.Sprites.Deer3[46 + input.U.DickSize]);
                        return;
                    }

                    output.Layer(11); //why dis here
                    return;
                }

                if (input.A.IsErect())
                {
                    if (input.A.PredatorComponent?.VisibleFullness < .75f &&
                        (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize +
                                       input.A.GetRightBreastSize(32 * 32)) < 16 &&
                        (int)Math.Sqrt(input.U.DefaultBreastSize * input.U.DefaultBreastSize +
                                       input.A.GetLeftBreastSize(32 * 32)) < 16)
                    {
                        output.Layer(24);
                        if (input.A.IsCockVoring)
                        {
                            output.Sprite(input.Sprites.Deer3[86 + input.U.DickSize]);
                            return;
                        }

                        output.Sprite(input.Sprites.Deer3[70 + input.U.DickSize]);
                        return;
                    }

                    output.Layer(13);
                    if (input.A.IsCockVoring)
                    {
                        output.Sprite(input.Sprites.Deer3[94 + input.U.DickSize]);
                        return;
                    }

                    output.Sprite(input.Sprites.Deer3[78 + input.U.DickSize]);
                    return;
                }

                output.Sprite(input.Sprites.Deer3[78 + input.U.DickSize]).Layer(11);
            });

            builder.RenderSingle(SpriteType.Balls, 10, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.DeerSkin, input.U.SkinColor));
                if (input.U.Furry && Config.FurryGenitals)
                {
                    output.AddOffset(0, -3 * .625f);
                }

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
                    output.AddOffset(0, -15 * .625f);
                }
                else if (offset == 25)
                {
                    output.AddOffset(0, -9 * .625f);
                }
                else if (offset == 24)
                {
                    output.AddOffset(0, -7 * .625f);
                }
                else if (offset == 23)
                {
                    output.AddOffset(0, -6 * .625f);
                }
                else if (offset == 22)
                {
                    output.AddOffset(0, -3 * .625f);
                }
                else if (offset == 21)
                {
                    output.AddOffset(0, -2 * .625f);
                }

                if (offset > 0)
                {
                    output.Sprite(input.Sprites.Deer3[Math.Min(110 + offset, 136) - (input.U.Furry && Config.FurryGenitals ? 102 : 0)]);
                    return;
                }

                output.Sprite(input.Sprites.Deer3[102 + size - (input.U.Furry && Config.FurryGenitals ? 102 : 0)]);
            });

            builder.RenderSingle(SpriteType.Weapon, 3, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (!input.U.HasBreasts)
                {
                    output.AddOffset(2 * .625f, 0);
                }

                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    output.Sprite(input.Sprites.Deer1[76 + input.A.GetWeaponSprite()]);
                }
            });

            builder.RandomCustom(data =>
            {
                Unit unit = data.Unit;
                Defaults.RandomCustom(data);


                if (State.Rand.Next(3) == 0)
                {
                    unit.BodyAccentType2 = data.MiscRaceData.BodyAccentTypes2 - 1;
                }
                else
                {
                    unit.BodyAccentType2 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes2 - 1);
                }

                unit.BodyAccentType1 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes1);
                unit.BodyAccentType3 = State.Rand.Next(data.MiscRaceData.BodyAccentTypes3);

                unit.ClothingExtraType1 = 0;

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
                        unit.HairStyle = 12 + State.Rand.Next(13);
                    }
                    else
                    {
                        unit.HairStyle = State.Rand.Next(18);
                    }
                }

                if (unit.Type == UnitType.Leader)
                {
                    unit.ClothingType2 = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedWaistTypes, leaderClothes3);
                    if (unit.HasBreasts)
                    {
                        unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypes, leaderClothes1);
                    }
                    else
                    {
                        unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypes, leaderClothes2);
                    }
                }

                if (Config.RagsForSlaves && State.World?.MainEmpires != null && (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) && unit.ImmuneToDefections == false)
                {
                    unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypes, rags);
                    if (unit.ClothingType == 0) //Covers rags not in the list
                    {
                        unit.ClothingType = data.MiscRaceData.AllowedMainClothingTypes.Count;
                    }
                }
            });
        });


        private static class GenericTop1
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop1Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[24];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.deer/1524");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[56]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[48 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
            
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop1InstanceV2 = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[24];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.deer/1524");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[56]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[48 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop2
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop2Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[34];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.deer/1534");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[65]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[57 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop3
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop3Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[44];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.deer/1544");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[74]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[66 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop4
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop4Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[55];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.deer/1555");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing2"].Layer(18);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].SetOffset(0, -2 * .625f);
                    output["Clothing2"].SetOffset(0, -2 * .625f);

                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[80]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[72 + input.U.BreastSize]);
                    }

                    output["Clothing2"].Sprite(input.Sprites.Sharks5[81]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop5
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop5Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[74];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.deer/1574");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing2"].Layer(18);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].SetOffset(0, -2 * .625f);
                    output["Clothing2"].SetOffset(0, -2 * .625f);

                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[90]);
                        output["Clothing2"].Sprite(input.Sprites.Sharks5[99]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[82 + input.U.BreastSize]);
                        output["Clothing2"].Sprite(input.Sprites.Sharks5[91 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop6
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop6Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[88];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.deer/1588");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].SetOffset(0, -2 * .625f);

                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Sharks5[104 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericTop7
        {
            internal static readonly BindableClothing<IOverSizeParameters> GenericTop7Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[44];
                    output.FemaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.deer/1544");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[95]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[87 + input.U.BreastSize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class MaleTop
        {
            internal static readonly IClothing MaleTopInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[79];
                    output.MaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.deer/1579");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);

                    output["Clothing1"].Sprite(input.A.HasBelly ? input.Sprites.Cockatrice3[83 + input.U.BodySize] : input.Sprites.Deer4[84 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class MaleTop2
        {
            internal static readonly IClothing MaleTop2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians4[79];
                    output.MaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.deer/1579");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Sprite(input.Sprites.Cockatrice3[75 + input.U.BodySize]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class Natural
        {
            internal static readonly BindableClothing<IOverSizeParameters> NaturalInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.RevealsBreasts = true;
                    output.OccupiesAllSlots = true;
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing2"].Layer(7);
                    output["Clothing1"].Layer(18);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(null);
                        output["Clothing2"].Sprite(input.Sprites.Deer4[0]);
                        output["Clothing2"].SetOffset(0, 0);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Deer4[2 + input.U.BreastSize]);
                        output["Clothing2"].Sprite(input.Sprites.Deer4[0]);
                        output["Clothing2"].SetOffset(0, 0);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.Sprites.Deer4[1]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.DeerSkin, input.U.SkinColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.DeerSkin, input.U.SkinColor));
                });
            });
        }

        private static class Cuirass
        {
            internal static readonly BindableClothing<IOverSizeParameters> CuirassInstance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Deer4[47];
                    output.RevealsBreasts = true;
                    output.OccupiesAllSlots = true;
                    output.FixedColor = true;
                    output.ClothingId = new ClothingId("base.deer/61701");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing4"].Layer(7);
                    output["Clothing4"].Coloring(Color.white);
                    output["Clothing3"].Layer(12);
                    output["Clothing3"].Coloring(Color.white);
                    output["Clothing2"].Layer(7);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Deer4[64]);
                    }
                    else if (input.U.HasBreasts)
                    {
                        if (input.U.BreastSize < 2)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Deer4[60]);
                        }
                        else if (input.U.BreastSize < 4)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Deer4[61]);
                        }
                        else if (input.U.BreastSize < 6)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Deer4[62]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Deer4[63]);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Deer4[83]);
                    }

                    if (input.A.HasBelly)
                    {
                        output["Clothing2"].Sprite(null);
                    }
                    else
                    {
                        output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Deer4[73 + input.U.BodySize] : input.Sprites.Deer4[77 + input.U.BodySize]);
                    }

                    output["Clothing3"].Sprite(input.U.HasBreasts ? input.Sprites.Deer4[65 + input.U.BodySize] : input.Sprites.Deer4[69 + input.U.BodySize]);

                    output["Clothing4"].Sprite(input.A.GetWeaponSprite() == 1 ? input.Sprites.Deer4[82] : input.Sprites.Deer4[81]);
                });
            });
        }

        private static class Special1
        {
            internal static readonly BindableClothing<IOverSizeParameters> Special1Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Deer4[104];
                    output.BlocksBreasts = true;
                    output.RevealsBreasts = true;
                    output.FemaleOnly = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.deer/61708");
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing3"].Layer(17);
                    output["Clothing2"].Layer(17);
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.Deer4[103]);
                        output.BlocksBreasts = false;
                        output["Clothing2"].Sprite(null);
                        output["Clothing3"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output.BlocksBreasts = true;
                        output["Clothing1"].Sprite(input.Sprites.Deer4[96 + input.U.BreastSize]);
                        if (input.U.BreastSize == 3)
                        {
                            output["Clothing2"].Sprite(input.Sprites.DeerLeaderClothes[50]);
                            output["Clothing3"].Sprite(input.Sprites.DeerLeaderClothes[51]);
                        }
                        else if (input.U.BreastSize == 4)
                        {
                            output["Clothing2"].Sprite(input.Sprites.DeerLeaderClothes[52]);
                            output["Clothing3"].Sprite(input.Sprites.DeerLeaderClothes[53]);
                        }
                        else if (input.U.BreastSize == 5)
                        {
                            output["Clothing2"].Sprite(input.Sprites.DeerLeaderClothes[54]);
                            output["Clothing3"].Sprite(input.Sprites.DeerLeaderClothes[55]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(input.Sprites.Cockatrice2[0 + input.U.BreastSize]);
                            output["Clothing3"].Sprite(input.Sprites.Cockatrice2[32 + input.U.BreastSize]);
                        }
                    }
                    else
                    {
                        output.BlocksBreasts = true;
                    }

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.DeerSkin, input.U.SkinColor));
                    output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(SwapType.DeerSkin, input.U.SkinColor));
                });
            });
        }

        private static class Special2
        {
            internal static readonly IClothing Special2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Deer4[107];
                    output.MaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.deer/61709");
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Sprite(input.Sprites.Deer4[108 + input.U.BodySize]);
                });
            });
        }

        private static class DeerRags
        {
            internal static readonly IClothing DeerRagsInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Rags[23];
                    output.RevealsDick = true;
                    output.InFrontOfDick = true;
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.deer/207");
                    output.OccupiesAllSlots = true;
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(12);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Coloring(Color.white);
                    if (input.U.HasBreasts)
                    {
                        if (input.U.BreastSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Deer4[57]);
                        }
                        else if (input.U.BreastSize < 6)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Deer4[58]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Deer4[59]);
                        }

                        output["Clothing2"].Sprite(input.Sprites.Deer4[48 + input.U.BodySize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Deer4[56]);
                        output["Clothing2"].Sprite(input.Sprites.Deer4[52 + input.U.BodySize]);
                    }
                });
            });
        }

        private static class DeerLeader1
        {
            internal static readonly BindableClothing<IOverSizeParameters> DeerLeader1Instance = ClothingBuilder.CreateV2<IOverSizeParameters>(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.LeaderOnly = true;
                    output.DiscardSprite = input.Sprites.DeerLeaderClothes[49];
                    output.BlocksBreasts = true;
                    output.RevealsBreasts = true;
                    output.FemaleOnly = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.deer/61702");
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output, extra) =>
                {
                    output["Clothing3"].Layer(17);
                    output["Clothing2"].Layer(17);
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Coloring(Color.white);
                    if (extra.Oversize)
                    {
                        output["Clothing1"].Sprite(input.Sprites.DeerLeaderClothes[63]);
                        output.BlocksBreasts = false;
                        output["Clothing2"].Sprite(null);
                        output["Clothing3"].Sprite(null);
                    }
                    else if (input.U.HasBreasts)
                    {
                        output.BlocksBreasts = true;
                        output["Clothing1"].Sprite(input.Sprites.DeerLeaderClothes[Math.Min(56 + input.U.BreastSize, 66)]);
                        if (input.U.BreastSize == 3)
                        {
                            output["Clothing2"].Sprite(input.Sprites.DeerLeaderClothes[50]);
                            output["Clothing3"].Sprite(input.Sprites.DeerLeaderClothes[51]);
                        }
                        else if (input.U.BreastSize == 4)
                        {
                            output["Clothing2"].Sprite(input.Sprites.DeerLeaderClothes[52]);
                            output["Clothing3"].Sprite(input.Sprites.DeerLeaderClothes[53]);
                        }
                        else if (input.U.BreastSize == 5)
                        {
                            output["Clothing2"].Sprite(input.Sprites.DeerLeaderClothes[54]);
                            output["Clothing3"].Sprite(input.Sprites.DeerLeaderClothes[55]);
                        }
                        else
                        {
                            output["Clothing2"].Sprite(input.Sprites.Cockatrice2[0 + input.U.BreastSize]);
                            output["Clothing3"].Sprite(input.Sprites.Cockatrice2[32 + input.U.BreastSize]);
                        }
                    }
                
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.DeerSkin, input.U.SkinColor));
                    output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(SwapType.DeerSkin, input.U.SkinColor));
                });
            });
        }

        private static class DeerLeader2
        {
            internal static readonly IClothing DeerLeader2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.LeaderOnly = true;
                    output.DiscardSprite = input.Sprites.DeerLeaderClothes[65];
                    output.MaleOnly = true;
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.deer/61703");
                    output.FixedColor = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(18);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Sprite(input.Sprites.DeerLeaderClothes[64]);
                });
            });
        }

        private static class GenericBot1
        {
            internal static readonly IClothing GenericBot1Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians3[121];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.deer/1521");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(12);
                    output["Clothing1"].Layer(13);
                    if (input.U.DickSize > 0)
                    {
                        if (input.U.DickSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[20]);
                        }
                        else if (input.U.DickSize > 5)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[22]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[21]);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(null);
                    }

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Deer4[12 + input.U.BodySize] : input.Sprites.Deer4[16 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericBot2
        {
            internal static readonly IClothing GenericBot2Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians3[137];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.deer/1537");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(13);

                    output["Clothing2"].Layer(12);

                    output["Clothing2"].Coloring(Color.white);

                    if (input.U.DickSize > 0)
                    {
                        if (input.U.DickSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[32]);
                        }
                        else if (input.U.DickSize > 5)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[34]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[33]);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.Sprites.Cockatrice3[31]);
                    }

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Deer4[20 + input.U.BodySize] : input.Sprites.Deer4[24 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericBot3
        {
            internal static readonly IClothing GenericBot3Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians3[140];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.deer/1540");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(13);
                    output["Clothing2"].Layer(12);
                    output["Clothing2"].Coloring(Color.white);
                    output["Clothing1"].Sprite(input.Sprites.Cockatrice3[35]);

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Deer4[20 + input.U.BodySize] : input.Sprites.Deer4[24 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class GenericBot4
        {
            internal static readonly IClothing GenericBot4Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Cockatrice3[47];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.deer/61602");
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
                        if (input.U.DickSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[44]);
                        }
                        else if (input.U.DickSize > 5)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[46]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Cockatrice3[45]);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(null);
                    }

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Deer4[28 + input.U.BodySize] : input.Sprites.Deer4[32 + input.U.BodySize]);
                });
            });
        }

        private static class GenericBot5
        {
            internal static readonly IClothing GenericBot5Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Avians3[121];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.deer/1521");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing2"].Layer(12);
                    output["Clothing1"].Layer(13);
                    if (input.U.DickSize > 0)
                    {
                        output["Clothing1"].SetOffset(0, -1 * .625f);
                        if (input.U.DickSize < 3)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Deer4[44]);
                        }
                        else if (input.U.DickSize > 5)
                        {
                            output["Clothing1"].Sprite(input.Sprites.Deer4[46]);
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.Deer4[45]);
                        }
                    }
                    else
                    {
                        output["Clothing1"].Sprite(null);
                    }

                    output["Clothing1"].SetOffset(0, 0);

                    output["Clothing2"].Sprite(input.U.HasBreasts ? input.Sprites.Deer4[36 + input.U.BodySize] : input.Sprites.Deer4[40 + input.U.BodySize]);

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class Loincloth
        {
            internal static readonly IClothing LoinclothInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.DeerLeaderClothes[66];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.deer/61705");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(12);
                    if (input.U.HasBreasts)
                    {
                        output["Clothing1"].Sprite(input.A.HasBelly ? input.Sprites.DeerLeaderClothes[1 + 2 * input.U.BodySize] : input.Sprites.DeerLeaderClothes[0 + 2 * input.U.BodySize]);
                    }
                    else
                    {
                        output["Clothing1"].Sprite(input.A.HasBelly ? input.Sprites.DeerLeaderClothes[9 + 2 * input.U.BodySize] : input.Sprites.DeerLeaderClothes[8 + 2 * input.U.BodySize]);
                    }

                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class DeerLeader3
        {
            internal static readonly IClothing DeerLeader3Instance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.LeaderOnly = true;
                    output.DiscardSprite = input.Sprites.DeerLeaderClothes[48];
                    output.RevealsBreasts = true;
                    output.ClothingId = new ClothingId("base.deer/61704");
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing3"].Layer(15);
                    output["Clothing2"].Layer(12);
                    output["Clothing1"].Layer(15);
                    output["Clothing1"].Coloring(Color.white);
                    if (input.U.HasBreasts)
                    {
                        if (input.A.HasBelly)
                        {
                            output["Clothing2"].Sprite(input.Sprites.DeerLeaderClothes[1 + 2 * input.U.BodySize]);

                            if (input.A.GetStomachSize(31, 0.7f) < 4)
                            {
                                output["Clothing1"].Sprite(input.Sprites.DeerLeaderClothes[17 + 2 * input.U.BodySize]);
                                output["Clothing3"].Sprite(input.Sprites.DeerLeaderClothes[33 + 2 * input.U.BodySize]);
                            }
                            else
                            {
                                output["Clothing1"].Sprite(null);
                                output["Clothing3"].Sprite(null);
                            }
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.DeerLeaderClothes[16 + 2 * input.U.BodySize]);
                            output["Clothing2"].Sprite(input.Sprites.DeerLeaderClothes[0 + 2 * input.U.BodySize]);
                            output["Clothing3"].Sprite(input.Sprites.DeerLeaderClothes[32 + 2 * input.U.BodySize]);
                        }
                    }
                    else
                    {
                        if (input.A.HasBelly)
                        {
                            output["Clothing2"].Sprite(input.Sprites.DeerLeaderClothes[9 + 2 * input.U.BodySize]);

                            if (input.A.GetStomachSize(31, 0.7f) < 4)
                            {
                                output["Clothing1"].Sprite(input.Sprites.DeerLeaderClothes[25 + 2 * input.U.BodySize]);
                                output["Clothing3"].Sprite(input.Sprites.DeerLeaderClothes[41 + 2 * input.U.BodySize]);
                            }
                            else
                            {
                                output["Clothing1"].Sprite(null);
                                output["Clothing3"].Sprite(null);
                            }
                        }
                        else
                        {
                            output["Clothing1"].Sprite(input.Sprites.DeerLeaderClothes[24 + 2 * input.U.BodySize]);
                            output["Clothing2"].Sprite(input.Sprites.DeerLeaderClothes[8 + 2 * input.U.BodySize]);
                            output["Clothing3"].Sprite(input.Sprites.DeerLeaderClothes[40 + 2 * input.U.BodySize]);
                        }
                    }

                    output["Clothing2"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                    output["Clothing3"].Coloring(ColorPaletteMap.GetPalette(SwapType.DeerLeaf, input.U.AccessoryColor));
                });
            });
        }

        private static class Scarf
        {
            internal static readonly IClothing ScarfInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Deer4[106];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.deer/61706");
                    output.DiscardUsesPalettes = true;
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(19);
                    output["Clothing1"].Sprite(input.Sprites.Deer4[105]);
                    output["Clothing1"].Coloring(ColorPaletteMap.GetPalette(SwapType.AviansSkin, input.U.ClothingColor));
                });
            });
        }

        private static class Necklace
        {
            internal static readonly IClothing NecklaceInstance = ClothingBuilder.Create(builder =>
            {
                builder.Setup(ClothingBuilder.DefaultMisc, (input, output) =>
                {
                    output.DiscardSprite = input.Sprites.Deer4[11];
                    output.RevealsBreasts = true;
                    output.RevealsDick = true;
                    output.ClothingId = new ClothingId("base.deer/61707");
                });

                builder.RenderAll((input, output) =>
                {
                    output["Clothing1"].Layer(19);
                    output["Clothing1"].Coloring(Color.white);
                    output["Clothing1"].Sprite(input.Sprites.Deer4[10]);
                });
            });
        }
    }
}
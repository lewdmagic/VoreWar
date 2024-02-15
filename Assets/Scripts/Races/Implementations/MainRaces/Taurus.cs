#region

using System;
using System.Collections.Generic;
using TaurusClothes;
using UnityEngine;

#endregion

namespace Races.Graphics.Implementations.MainRaces
{
    internal static class Taurus
    {
        internal static readonly RaceDataMaker Instance = RaceBuilderStatic.CreateV2(Defaults.Default, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Cow", "Cows");
                output.WallType(WallType.WoodenPallisade);
                output.FlavorText(new FlavorText(
                    new Texts { "mooing", "bulky", "hooved" },
                    new Texts { "multi-stomached", "heavy", "strong legged" },
                    new Texts { "bovine", "taurus", { "cow", Gender.Female }, { "bull", Gender.Male } },
                    new Dictionary<string, string>
                    {
                        [WeaponNames.Mace] = "Hammer",
                        [WeaponNames.Axe] = "Glaive",
                        [WeaponNames.SimpleBow] = "Revolver",
                        [WeaponNames.CompoundBow] = "Shotgun",
                        [WeaponNames.Claw] = "Fist"
                    }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 15,
                    StomachSize = 15,
                    HasTail = true,
                    FavoredStat = Stat.Strength,
                    RacialTraits = new List<TraitType>()
                    {
                        TraitType.StrongMelee,
                        TraitType.ForcefulBlow
                    },
                    RaceDescription = "Once mere cattle, a drop of minotaur blood slumbered in their veins. Rising and butchering their \"masters\", the Taurus took what they could from their old ranches and fled through mysterious portals that had heralded their rise. While intelligent, the Taurus trust in their physical might and great size, tossing their enemies aside as they trample on.",
                });
                output.CustomizeButtons((unit, buttons) => { buttons.SetText(ButtonType.EyeType, "Face Expression"); });
                output.TownNames(new List<string>
                {
                    "Minos",
                    "Fourbelly",
                    "Beefsburg",
                    "Udderlife",
                    "Rangeton",
                    "Salisbury",
                    "Cuddington",
                });
                output.BreastSizes = () => 5;
                output.DickSizes = () => 5;

                output.AccessoryColors = ColorPaletteMap.GetPaletteCount(SwapType.Fur);
                output.BodySizes = 0;
                output.MouthTypes = 0;

                output.BeardStyles = 3;

                output.EyeTypes = 4;
                output.HairStyles = 11;
                output.FurCapable = true;

                output.AvoidedMainClothingTypes = 0;
                //RestrictedClothingTypes = 0;
                //clothingColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing);
                output.AllowedMainClothingTypes.Set(
                    TaurusClothingTypes.OverallInstance,
                    TaurusClothingTypes.ShirtInstance,
                    TaurusClothingTypes.BikiniInstance,
                    TaurusClothingTypes.LeaderOutfitInstance,
                    TaurusClothingTypes.HolidayOutfitInstance
                );
                output.AllowedWaistTypes.Set(
                    TaurusClothingTypes.BikiniBottomInstance,
                    TaurusClothingTypes.LoinclothInstance,
                    TaurusClothingTypes.OverallBottomInstance
                );
                output.AllowedClothingHatTypes.Set(
                    TaurusClothingTypes.HatInstance,
                    TaurusClothingTypes.HolidayHatInstance
                );
                output.AllowedClothingAccessoryTypes.Set(
                    TaurusClothingTypes.CowBellInstance
                );
            });


            builder.RenderSingle(SpriteType.Head, 3, (input, output) =>
            {
                output.Coloring(FurryColor(input.Actor));
                int sprite = 32;
                if (input.U.Furry)
                {
                    sprite += 4;
                }

                if (input.U.HasBreasts)
                {
                    sprite += 2;
                }

                if (input.A.IsOralVoring)
                {
                    sprite += 1;
                }

                output.Sprite(input.Sprites.Cows[sprite]);
            });

            builder.RenderSingle(SpriteType.Eyes, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.U.EyeColor));
                if (input.U.Furry)
                {
                    if (input.U.HasBreasts)
                    {
                        output.Sprite(input.Sprites.Cows[63]);
                        return;
                    }

                    output.Sprite(input.Sprites.Cows[76]);
                    return;
                }

                int sprite = 48;
                sprite += 3 * input.U.EyeType;
                if (input.U.HasBreasts == false)
                {
                    sprite += 16;
                    if (sprite > 80)
                    {
                        sprite = 80;
                    }
                }

                output.Sprite(input.Sprites.Cows[sprite]);
            });

            builder.RenderSingle(SpriteType.SecondaryEyes, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.NormalHair, input.U.HairColor));
                if (input.U.Furry)
                {
                    return;
                }

                int sprite = 50;
                sprite += 3 * input.U.EyeType;
                if (input.U.HasBreasts == false)
                {
                    sprite += 16;
                    if (sprite > 82)
                    {
                        sprite = 82;
                    }
                }

                output.Sprite(input.Sprites.Cows[sprite]);
            });

            builder.RenderSingle(SpriteType.Mouth, 4, (input, output) =>
            {
                output.Coloring(FurryColor(input.Actor));
                if (input.U.Furry)
                {
                    return;
                }

                int sprite = 49;
                sprite += 3 * input.U.EyeType;
                if (input.U.HasBreasts == false)
                {
                    sprite += 16;
                    if (sprite > 81)
                    {
                        sprite = 81;
                    }
                }

                output.Sprite(input.Sprites.Cows[sprite]);
            });

            builder.RenderSingle(SpriteType.Hair, 6, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.NormalHair, input.U.HairColor));
                output.Sprite(input.Sprites.Cows[77 + input.U.HairStyle]);
            });
            builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.NormalHair, input.U.HairColor));
                if (input.U.HairStyle <= 6)
                {
                    output.Sprite(input.Sprites.Cows[90 + input.U.HairStyle]);
                }
            });

            builder.RenderSingle(SpriteType.Hair3, 7, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.NormalHair, input.U.HairColor));
                if (input.U.HairStyle == 6)
                {
                    output.Sprite(input.Sprites.Cows[97]);
                }
            });

            builder.RenderSingle(SpriteType.Beard, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.NormalHair, input.U.HairColor));
                if (input.U.Furry)
                {
                    return;
                }

                if (input.U.BeardStyle > 0)
                {
                    output.Sprite(input.Sprites.Cows[87 + input.U.BeardStyle]);
                }
            });

            builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
            {
                output.Coloring(FurryColor(input.Actor));
                int sprite = input.A.IsAttacking ? 1 : 0;
                if (input.A.GetWeaponSprite() == 2)
                {
                    sprite += 2;
                }

                if (input.U.HasBreasts == false)
                {
                    sprite += 9;
                }

                output.Sprite(input.Sprites.Cows[sprite]);
            });

            builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Fur, input.U.AccessoryColor));
                if (input.A.GetWeaponSprite() == 2)
                {
                    if ((Config.FurryHandsAndFeet || input.U.Furry) == false)
                    {
                        output.Sprite(input.Sprites.Cows[20]);
                        return;
                    }

                    output.Sprite(input.Sprites.Cows[input.U.HasBreasts ? 21 : 22]);
                    return;
                }

                if ((Config.FurryHandsAndFeet || input.U.Furry) == false)
                {
                    return;
                }

                int sprite = input.A.IsAttacking ? 5 : 4;
                if (input.U.HasBreasts == false)
                {
                    sprite += 9;
                }

                output.Sprite(input.Sprites.Cows[sprite]);
            });

            builder.RenderSingle(SpriteType.BodyAccent2, 5, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if ((Config.FurryHandsAndFeet || input.U.Furry) == false || Config.FurryFluff == false)
                {
                    return;
                }

                if (input.A.GetWeaponSprite() == 2)
                {
                    output.Sprite(input.Sprites.Cows[input.U.HasBreasts ? 23 : 24]);
                    return;
                }

                int sprite = input.A.IsAttacking ? 7 : 6;
                if (input.U.HasBreasts == false)
                {
                    sprite += 9;
                }

                output.Sprite(input.Sprites.Cows[sprite]);
            });

            builder.RenderSingle(SpriteType.BodyAccent3, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Fur, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Cows[18]);
            });
            builder.RenderSingle(SpriteType.BodyAccent4, 5, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.Fur, input.U.AccessoryColor));
                output.Sprite(input.Sprites.Cows[12]);
            });
            builder.RenderSingle(SpriteType.BodyAccent5, 5, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(Config.FurryHandsAndFeet || input.U.Furry || Config.FurryFluff == false ? input.Sprites.Cows[19] : null);
            });
            builder.RenderSingle(SpriteType.BodyAccessory, 2, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Cows[3]);
            });
            builder.RenderSingle(SpriteType.SecondaryAccessory, 4, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                output.Sprite(input.Sprites.Cows[25]);
            });
            builder.RenderSingle(SpriteType.BodySize, 3, (input, output) =>
            {
                output.Coloring(FurryBellyColor(input.Actor));
                output.Sprite(input.Sprites.Cows[input.U.HasBreasts ? 8 : 17]);
            });
            builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
            {
                output.Coloring(FurryBellyColor(input.Actor));
                if (input.U.HasBreasts == false)
                {
                    return;
                }

                if (input.A.SquishedBreasts)
                {
                    output.Sprite(input.Sprites.Cows[Math.Max(114 + input.U.BreastSize, 115)]);
                    return;
                }

                output.Sprite(input.Sprites.Cows[110 + input.U.BreastSize]);
            });

            builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
            {
                output.Coloring(FurryBellyColor(input.Actor));
                if (input.A.HasBelly)
                {
                    output.Sprite(input.Sprites.Cows[98 + input.A.GetStomachSize(11, .95f)]);
                }
            });

            builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
            {
                output.Coloring(FurryColor(input.Actor));
                if (input.U.HasDick == false)
                {
                    return;
                }

                if (input.A.IsErect())
                {
                    if (input.A.PredatorComponent?.VisibleFullness < .5f)
                    {
                        output.Layer(18);
                        if (input.U.DickSize == 4)
                        {
                            output.Sprite(input.Sprites.Cows[123]);
                            return;
                        }

                        if (input.U.DickSize == 3)
                        {
                            output.Sprite(input.Sprites.Cows[121]);
                            return;
                        }

                        output.Sprite(input.Sprites.Cows[29 + input.U.DickSize]);
                        return;
                    }

                    output.Layer(12);
                    if (input.U.DickSize == 4)
                    {
                        output.Sprite(input.Sprites.Cows[122]);
                        return;
                    }

                    if (input.U.DickSize == 3)
                    {
                        output.Sprite(input.Sprites.Cows[120]);
                        return;
                    }

                    output.Sprite(input.Sprites.Cows[26 + input.U.DickSize]);
                    return;
                }

                output.Layer(9);
                if (input.U.DickSize == 4)
                {
                    output.Sprite(input.Sprites.Cows[122]);
                    return;
                }

                if (input.U.DickSize == 3)
                {
                    output.Sprite(input.Sprites.Cows[119]);
                    return;
                }

                output.Sprite(input.Sprites.Cows[26 + input.U.DickSize]);
            });

            builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
            {
                output.Coloring(FurryColor(input.Actor));
                if (input.U.HasDick == false)
                {
                    return;
                }
                //if (input.U.Furry && Config.FurryGenitals)
                //{
                //    int size = input.U.DickSize;
                //    int offset = (int)((input.A.PredatorComponent?.BallsFullness ?? 0) * 3);
                //    if (offset > 0)
                //        return Out.Update(State.GameManager.SpriteDictionary.FurryDicks[Math.Min(12 + offset, 23)]);
                //    return Out.Update(State.GameManager.SpriteDictionary.FurryDicks[size]);
                //}

                int baseSize = 2;
                if (input.U.DickSize == 4)
                {
                    baseSize = 8;
                }

                int ballOffset = input.A.GetBallSize(21, .8f);

                int combined = Math.Min(baseSize + ballOffset, 20);
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

            builder.RenderSingle(SpriteType.Weapon, 12, (input, output) =>
            {
                output.Coloring(Defaults.WhiteColored);
                if (input.U.HasWeapon && input.A.Surrendered == false)
                {
                    int weaponSprite = input.A.GetWeaponSprite();
                    switch (weaponSprite)
                    {
                        case 1:
                            if (input.U.DickSize < 0)
                            {
                                output.AddOffset(0, 0);
                            }
                            else
                            {
                                output.AddOffset(5 * .625f, 1 * .625f);
                            }

                            break;
                        case 3:
                            if (input.U.DickSize < 0)
                            {
                                output.AddOffset(0, 11 * .625f);
                            }
                            else
                            {
                                output.AddOffset(5 * .625f, 12 * .625f);
                            }

                            break;
                        case 5:
                            output.AddOffset(2 * .625f, 0);
                            break;
                        case 7:
                            output.AddOffset(11 * .625f, 0);
                            break;
                        default:
                            output.AddOffset(0, 0);
                            break;
                    }

                    output.Sprite(input.Sprites.Cows[40 + input.A.GetWeaponSprite()]);
                }
            });


            builder.RunBefore((input, output) =>
            {
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
                IUnitRead unit = data.Unit;


                if (unit.Type == UnitType.Leader)
                {
                    unit.ClothingHatType = 1;
                    unit.ClothingType = 1 + Extensions.IndexOf(data.SetupOutput.AllowedMainClothingTypes, TaurusClothingTypes.LeaderOutfitInstance);
                }
                else
                {
                    unit.ClothingHatType = 0;
                }

                if (unit.ClothingType == 5)
                {
                    unit.ClothingHatType = 2;
                }

                if (unit.HasDick && unit.HasBreasts)
                {
                    unit.HairStyle = State.Rand.Next(Config.HermsOnlyUseFemaleHair ? 7 : data.SetupOutput.HairStyles);
                }
                else if (unit.HasDick && Config.FemaleHairForMales)
                {
                    unit.HairStyle = State.Rand.Next(data.SetupOutput.HairStyles);
                }
                else if (unit.HasDick == false && Config.MaleHairForFemales)
                {
                    unit.HairStyle = State.Rand.Next(data.SetupOutput.HairStyles);
                }
                else
                {
                    if (unit.HasDick)
                    {
                        unit.HairStyle = 7 + State.Rand.Next(4);
                    }
                    else
                    {
                        unit.HairStyle = State.Rand.Next(7);
                    }
                }
            });
        });


        private static ColorSwapPalette FurryColor(IActorUnit actor)
        {
            if (actor.Unit.Furry)
            {
                return ColorPaletteMap.GetPalette(SwapType.Fur, actor.Unit.AccessoryColor);
            }

            return ColorPaletteMap.GetPalette(SwapType.Skin, actor.Unit.SkinColor);
        }

        private static ColorSwapPalette FurryBellyColor(IActorUnit actor)
        {
            if (actor.Unit.Furry)
            {
                return ColorPaletteMap.FurryBellySwap;
            }

            return ColorPaletteMap.GetPalette(SwapType.Skin, actor.Unit.SkinColor);
        }
    }
}
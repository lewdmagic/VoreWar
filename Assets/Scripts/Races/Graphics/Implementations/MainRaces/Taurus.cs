#region

using System;
using TaurusClothes;
using UnityEngine;

#endregion

internal static class Taurus
{
    internal static readonly IRaceData Instance = RaceBuilder.Create(Defaults.Default, builder =>
    {
        builder.Setup(output =>
        {
            output.BreastSizes = () => 5;
            output.DickSizes = () => 5;

            output.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur);
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
            if (input.Actor.Unit.Furry)
            {
                sprite += 4;
            }

            if (input.Actor.Unit.HasBreasts)
            {
                sprite += 2;
            }

            if (input.Actor.IsOralVoring)
            {
                sprite += 1;
            }

            output.Sprite(input.Sprites.Cows[sprite]);
        });

        builder.RenderSingle(SpriteType.Eyes, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            if (input.Actor.Unit.Furry)
            {
                if (input.Actor.Unit.HasBreasts)
                {
                    output.Sprite(input.Sprites.Cows[63]);
                    return;
                }

                output.Sprite(input.Sprites.Cows[76]);
                return;
            }

            int sprite = 48;
            sprite += 3 * input.Actor.Unit.EyeType;
            if (input.Actor.Unit.HasBreasts == false)
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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
            if (input.Actor.Unit.Furry)
            {
                return;
            }

            int sprite = 50;
            sprite += 3 * input.Actor.Unit.EyeType;
            if (input.Actor.Unit.HasBreasts == false)
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
            if (input.Actor.Unit.Furry)
            {
                return;
            }

            int sprite = 49;
            sprite += 3 * input.Actor.Unit.EyeType;
            if (input.Actor.Unit.HasBreasts == false)
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
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
            output.Sprite(input.Sprites.Cows[77 + input.Actor.Unit.HairStyle]);
        });
        builder.RenderSingle(SpriteType.Hair2, 1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
            if (input.Actor.Unit.HairStyle <= 6)
            {
                output.Sprite(input.Sprites.Cows[90 + input.Actor.Unit.HairStyle]);
            }
        });

        builder.RenderSingle(SpriteType.Hair3, 7, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
            if (input.Actor.Unit.HairStyle == 6)
            {
                output.Sprite(input.Sprites.Cows[97]);
            }
        });

        builder.RenderSingle(SpriteType.Beard, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
            if (input.Actor.Unit.Furry)
            {
                return;
            }

            if (input.Actor.Unit.BeardStyle > 0)
            {
                output.Sprite(input.Sprites.Cows[87 + input.Actor.Unit.BeardStyle]);
            }
        });

        builder.RenderSingle(SpriteType.Body, 2, (input, output) =>
        {
            output.Coloring(FurryColor(input.Actor));
            int sprite = input.Actor.IsAttacking ? 1 : 0;
            if (input.Actor.GetWeaponSprite() == 2)
            {
                sprite += 2;
            }

            if (input.Actor.Unit.HasBreasts == false)
            {
                sprite += 9;
            }

            output.Sprite(input.Sprites.Cows[sprite]);
        });

        builder.RenderSingle(SpriteType.BodyAccent, 4, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, input.Actor.Unit.AccessoryColor));
            if (input.Actor.GetWeaponSprite() == 2)
            {
                if ((Config.FurryHandsAndFeet || input.Actor.Unit.Furry) == false)
                {
                    output.Sprite(input.Sprites.Cows[20]);
                    return;
                }

                output.Sprite(input.Sprites.Cows[input.Actor.Unit.HasBreasts ? 21 : 22]);
                return;
            }

            if ((Config.FurryHandsAndFeet || input.Actor.Unit.Furry) == false)
            {
                return;
            }

            int sprite = input.Actor.IsAttacking ? 5 : 4;
            if (input.Actor.Unit.HasBreasts == false)
            {
                sprite += 9;
            }

            output.Sprite(input.Sprites.Cows[sprite]);
        });

        builder.RenderSingle(SpriteType.BodyAccent2, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            if ((Config.FurryHandsAndFeet || input.Actor.Unit.Furry) == false || Config.FurryFluff == false)
            {
                return;
            }

            if (input.Actor.GetWeaponSprite() == 2)
            {
                output.Sprite(input.Sprites.Cows[input.Actor.Unit.HasBreasts ? 23 : 24]);
                return;
            }

            int sprite = input.Actor.IsAttacking ? 7 : 6;
            if (input.Actor.Unit.HasBreasts == false)
            {
                sprite += 9;
            }

            output.Sprite(input.Sprites.Cows[sprite]);
        });

        builder.RenderSingle(SpriteType.BodyAccent3, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Cows[18]);
        });
        builder.RenderSingle(SpriteType.BodyAccent4, 5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, input.Actor.Unit.AccessoryColor));
            output.Sprite(input.Sprites.Cows[12]);
        });
        builder.RenderSingle(SpriteType.BodyAccent5, 5, (input, output) =>
        {
            output.Coloring(Defaults.WhiteColored);
            output.Sprite(Config.FurryHandsAndFeet || input.Actor.Unit.Furry || Config.FurryFluff == false ? input.Sprites.Cows[19] : null);
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
            output.Sprite(input.Sprites.Cows[input.Actor.Unit.HasBreasts ? 8 : 17]);
        });
        builder.RenderSingle(SpriteType.Breasts, 16, (input, output) =>
        {
            output.Coloring(FurryBellyColor(input.Actor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.SquishedBreasts)
            {
                output.Sprite(input.Sprites.Cows[Math.Max(114 + input.Actor.Unit.BreastSize, 115)]);
                return;
            }

            output.Sprite(input.Sprites.Cows[110 + input.Actor.Unit.BreastSize]);
        });

        builder.RenderSingle(SpriteType.Belly, 15, (input, output) =>
        {
            output.Coloring(FurryBellyColor(input.Actor));
            if (input.Actor.HasBelly)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(11, .95f) == 11)
                {
                    output.Sprite(input.Sprites.CowsSeliciaBelly[1]);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize(11, .95f) == 11)
                {
                    output.Sprite(input.Sprites.CowsSeliciaBelly[0]);
                    return;
                }

                output.Sprite(input.Sprites.Cows[98 + input.Actor.GetStomachSize(11, .95f)]);
            }
        });

        builder.RenderSingle(SpriteType.Dick, 9, (input, output) =>
        {
            output.Coloring(FurryColor(input.Actor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.IsErect())
            {
                if (input.Actor.PredatorComponent?.VisibleFullness < .5f)
                {
                    output.Layer(18);
                    if (input.Actor.Unit.DickSize == 4)
                    {
                        output.Sprite(input.Sprites.Cows[123]);
                        return;
                    }

                    if (input.Actor.Unit.DickSize == 3)
                    {
                        output.Sprite(input.Sprites.Cows[121]);
                        return;
                    }

                    output.Sprite(input.Sprites.Cows[29 + input.Actor.Unit.DickSize]);
                    return;
                }

                output.Layer(12);
                if (input.Actor.Unit.DickSize == 4)
                {
                    output.Sprite(input.Sprites.Cows[122]);
                    return;
                }

                if (input.Actor.Unit.DickSize == 3)
                {
                    output.Sprite(input.Sprites.Cows[120]);
                    return;
                }

                output.Sprite(input.Sprites.Cows[26 + input.Actor.Unit.DickSize]);
                return;
            }

            output.Layer(9);
            if (input.Actor.Unit.DickSize == 4)
            {
                output.Sprite(input.Sprites.Cows[122]);
                return;
            }

            if (input.Actor.Unit.DickSize == 3)
            {
                output.Sprite(input.Sprites.Cows[119]);
                return;
            }

            output.Sprite(input.Sprites.Cows[26 + input.Actor.Unit.DickSize]);
        });

        builder.RenderSingle(SpriteType.Balls, 8, (input, output) =>
        {
            output.Coloring(FurryColor(input.Actor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }
            //if (input.Actor.Unit.Furry && Config.FurryGenitals)
            //{
            //    int size = input.Actor.Unit.DickSize;
            //    int offset = (int)((input.Actor.PredatorComponent?.BallsFullness ?? 0) * 3);
            //    if (offset > 0)
            //        return Out.Update(State.GameManager.SpriteDictionary.FurryDicks[Math.Min(12 + offset, 23)]);
            //    return Out.Update(State.GameManager.SpriteDictionary.FurryDicks[size]);
            //}

            int baseSize = 2;
            if (input.Actor.Unit.DickSize == 4)
            {
                baseSize = 8;
            }

            int ballOffset = input.Actor.GetBallSize(21, .8f);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .8f) == 21)
            {
                output.Sprite(input.Sprites.Balls[24]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .8f) == 21)
            {
                output.Sprite(input.Sprites.Balls[23]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .8f) == 20)
            {
                output.Sprite(input.Sprites.Balls[22]).AddOffset(0, -15 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .8f) == 19)
            {
                output.Sprite(input.Sprites.Balls[21]).AddOffset(0, -14 * .625f);
                return;
            }

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
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                int weaponSprite = input.Actor.GetWeaponSprite();
                switch (weaponSprite)
                {
                    case 1:
                        if (input.Actor.Unit.DickSize < 0)
                        {
                            output.AddOffset(0, 0);
                        }
                        else
                        {
                            output.AddOffset(5 * .625f, 1 * .625f);
                        }

                        break;
                    case 3:
                        if (input.Actor.Unit.DickSize < 0)
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

                output.Sprite(input.Sprites.Cows[40 + input.Actor.GetWeaponSprite()]);
            }
        });


        builder.RunBefore((input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                Vector3 localScale;

                if (input.Actor.PredatorComponent.VisibleFullness > 4)
                {
                    float extraCap = input.Actor.PredatorComponent.VisibleFullness - 4;
                    float xScale = Mathf.Min(1 + extraCap / 5, 1.8f);
                    float yScale = Mathf.Min(1 + extraCap / 40, 1.1f);
                    localScale = new Vector3(xScale, yScale, 1);
                }
                else
                {
                    localScale = new Vector3(1, 1, 1);
                }

                output.changeSprite(SpriteType.Belly).SetActive(true).SetLocalScale(localScale);
            }
        });

        builder.RandomCustom(data =>
        {
            Defaults.RandomCustom(data);
            Unit unit = data.Unit;


            if (unit.Type == UnitType.Leader)
            {
                unit.ClothingHatType = 1;
                unit.ClothingType = 1 + Extensions.IndexOf(data.MiscRaceData.AllowedMainClothingTypes, TaurusClothingTypes.LeaderOutfitInstance);
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
                unit.HairStyle = State.Rand.Next(Config.HermsOnlyUseFemaleHair ? 7 : data.MiscRaceData.HairStyles);
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
                    unit.HairStyle = 7 + State.Rand.Next(4);
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(7);
                }
            }
        });
    });


    private static ColorSwapPalette FurryColor(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, actor.Unit.AccessoryColor);
        }

        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, actor.Unit.SkinColor);
    }

    private static ColorSwapPalette FurryBellyColor(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
        {
            return ColorPaletteMap.FurryBellySwap;
        }

        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, actor.Unit.SkinColor);
    }
}
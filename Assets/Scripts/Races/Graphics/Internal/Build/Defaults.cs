#region

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

internal static class Defaults
{
    internal static readonly SpriteTypeIndexed<Action<IRaceRenderInput, IRaceRenderOutput>> SpriteGens2 = new SpriteTypeIndexed<Action<IRaceRenderInput, IRaceRenderOutput>>();
    internal static readonly SpriteTypeIndexed<SingleRenderFunc<IParameters>> SpriteGens3 = new SpriteTypeIndexed<SingleRenderFunc<IParameters>>();

    internal static readonly Func<Actor_Unit, ColorSwapPalette> FurryColor = actor =>
    {
        if (actor.Unit.Furry)
        {
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, actor.Unit.AccessoryColor);
        }

        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, actor.Unit.SkinColor);
    };

    internal static readonly Func<Actor_Unit, ColorSwapPalette> FurryColor2 = actor =>
    {
        if (actor.Unit.Furry)
        {
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, actor.Unit.AccessoryColor);
        }

        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, actor.Unit.SkinColor);
    };


    internal static readonly Func<Actor_Unit, ColorSwapPalette> FurryBellyColor = actor =>
    {
        if (actor.Unit.Furry)
        {
            return ColorPaletteMap.FurryBellySwap;
        }

        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Skin, actor.Unit.SkinColor);
    };

    internal static readonly Action<IRunInput, IRunOutput> Finalize = (input, output) =>
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
    };

    internal static readonly Action<IRandomCustomInput> RandomCustom = input =>
    {
        Unit unit = input.Unit;
        if (input.MiscRaceData.BodySizes > 0)
        {
            if (State.RaceSettings.GetOverrideWeight(unit.Race))
            {
                int min = State.RaceSettings.Get(unit.Race).MinWeight;
                int max = State.RaceSettings.Get(unit.Race).MaxWeight;
                unit.BodySize = min + State.Rand.Next(max - min);
                unit.BodySize = Mathf.Clamp(unit.BodySize, 0, input.MiscRaceData.BodySizes - 1);
            }
            else
            {
                unit.BodySize = Mathf.Min(Config.DefaultStartingWeight, input.MiscRaceData.BodySizes - 1);
            }
        }
        else
        {
            unit.BodySize = 0;
        }

        if (input.MiscRaceData.HairStyles == 15)
        {
            if (unit.HasDick && unit.HasBreasts)
            {
                if (Config.HermsOnlyUseFemaleHair)
                {
                    unit.HairStyle = State.Rand.Next(8);
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(input.MiscRaceData.HairStyles);
                }
            }
            else if (unit.HasDick && Config.FemaleHairForMales)
            {
                unit.HairStyle = State.Rand.Next(input.MiscRaceData.HairStyles);
            }
            else if (unit.HasDick == false && Config.MaleHairForFemales)
            {
                unit.HairStyle = State.Rand.Next(input.MiscRaceData.HairStyles);
            }
            else
            {
                if (unit.HasDick)
                {
                    unit.HairStyle = 8 + State.Rand.Next(7);
                }
                else
                {
                    unit.HairStyle = State.Rand.Next(8);
                }
            }
        }
        else //Failsafe incase you forget to override it
        {
            unit.HairStyle = State.Rand.Next(input.MiscRaceData.HairStyles);
        }

        unit.HairColor = State.Rand.Next(input.MiscRaceData.HairColors);
        unit.AccessoryColor = State.Rand.Next(input.MiscRaceData.AccessoryColors);

        if (Config.ExtraRandomHairColors)
        {
            if (input.MiscRaceData.HairColors == ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.NormalHair))
            {
                unit.HairColor = State.Rand.Next(input.MiscRaceData.HairColors);
            }

            if (input.MiscRaceData.AccessoryColors == ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur))
            {
                unit.AccessoryColor = State.Rand.Next(input.MiscRaceData.AccessoryColors);
            }
        }
        else
        {
            if (input.MiscRaceData.HairColors == ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.NormalHair))
            {
                unit.HairColor = State.Rand.Next(ColorPaletteMap.MixedHairColors);
            }

            if (input.MiscRaceData.AccessoryColors == ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur))
            {
                unit.AccessoryColor = State.Rand.Next(ColorPaletteMap.MixedHairColors);
            }
        }

        unit.SkinColor = State.Rand.Next(input.MiscRaceData.SkinColors);
        unit.EyeColor = State.Rand.Next(input.MiscRaceData.EyeColors);
        unit.ExtraColor1 = State.Rand.Next(input.MiscRaceData.ExtraColors1);
        unit.ExtraColor2 = State.Rand.Next(input.MiscRaceData.ExtraColors2);
        unit.ExtraColor3 = State.Rand.Next(input.MiscRaceData.ExtraColors3);
        unit.ExtraColor4 = State.Rand.Next(input.MiscRaceData.ExtraColors4);
        unit.EyeType = State.Rand.Next(Math.Max(input.MiscRaceData.EyeTypes - input.MiscRaceData.AvoidedEyeTypes, 0));
        unit.ClothingColor = State.Rand.Next(ColorMap.ClothingColorCount);
        unit.MouthType =
            State.Rand.Next(Math.Max(input.MiscRaceData.MouthTypes - input.MiscRaceData.AvoidedMouthTypes, 0));
        unit.SpecialAccessoryType = State.Rand.Next(input.MiscRaceData.SpecialAccessoryCount);

        if (unit.HasDick && unit.HasBreasts == false)
        {
            unit.BeardStyle = State.Rand.Next(input.MiscRaceData.BeardStyles);
        }

        if (input.MiscRaceData.ClothingAccessoryTypesCount > 1)
        {
            unit.ClothingAccessoryType = State.Rand.Next(input.MiscRaceData.ClothingAccessoryTypesCount);
            for (int i = 0; i < 50; i++)
            {
                if (unit.ClothingAccessoryType > 0)
                {
                    if (input.MiscRaceData.AllowedClothingAccessoryTypes[unit.ClothingAccessoryType - 1].CanWear(unit))
                    {
                        break;
                    }
                }

                unit.ClothingAccessoryType = State.Rand.Next(input.MiscRaceData.ClothingAccessoryTypesCount);
            }

            if (unit.ClothingAccessoryType > 0 &&
                input.MiscRaceData.AllowedClothingAccessoryTypes[unit.ClothingAccessoryType - 1].CanWear(unit) == false)
            {
                unit.ClothingAccessoryType = 0;
            }
        }

        if (input.MiscRaceData.ClothingHatTypesCount > 1)
        {
            if (input.MiscRaceData.AllowedClothingHatTypes.Contains(MainAccessories.SantaHatInstance) && Config.WinterActive())
            {
                if (State.Rand.Next(2) == 0)
                {
                    unit.ClothingHatType = 1;
                }
            }
            else
            {
                unit.ClothingHatType = State.Rand.Next(input.MiscRaceData.ClothingHatTypesCount);
                for (int i = 0; i < 50; i++)
                {
                    if (unit.ClothingHatType > 0)
                    {
                        if (input.MiscRaceData.AllowedClothingHatTypes[unit.ClothingHatType - 1].CanWear(unit))
                        {
                            break;
                        }
                    }

                    unit.ClothingHatType = State.Rand.Next(input.MiscRaceData.ClothingHatTypesCount);
                }

                if (unit.ClothingHatType > 0 &&
                    input.MiscRaceData.AllowedClothingHatTypes[unit.ClothingHatType - 1].CanWear(unit) == false)
                {
                    unit.ClothingHatType = 0;
                }
            }
        }


        if (input.MiscRaceData.MainClothingTypesCount > 1)
        {
            float fraction = State.RaceSettings.GetOverrideClothed(unit.Race)
                ? State.RaceSettings.Get(unit.Race).clothedFraction
                : Config.ClothedFraction;
            if (State.Rand.NextDouble() < fraction)
            {
                unit.ClothingType = State.Rand.Next(Mathf.Max(
                    input.MiscRaceData.MainClothingTypesCount - input.MiscRaceData.AvoidedMainClothingTypes, 0));
                for (int i = 0; i < 50; i++)
                {
                    if (unit.ClothingType > 0)
                    {
                        if (input.MiscRaceData.AllowedMainClothingTypes[unit.ClothingType - 1].CanWear(unit))
                        {
                            break;
                        }
                    }

                    unit.ClothingType = State.Rand.Next(Mathf.Max(
                        input.MiscRaceData.MainClothingTypesCount - input.MiscRaceData.AvoidedMainClothingTypes, 0));
                }

                if (unit.ClothingType > 0 &&
                    input.MiscRaceData.AllowedMainClothingTypes[unit.ClothingType - 1].CanWear(unit) == false)
                {
                    unit.ClothingType = 0;
                }

                if (input.MiscRaceData.WaistClothingTypesCount > 0)
                {
                    unit.ClothingType2 = State.Rand.Next(input.MiscRaceData.WaistClothingTypesCount);
                    for (int i = 0; i < 50; i++)
                    {
                        if (unit.ClothingType2 > 0)
                        {
                            if (input.MiscRaceData.AllowedWaistTypes[unit.ClothingType2 - 1].CanWear(unit))
                            {
                                break;
                            }
                        }

                        unit.ClothingType2 = State.Rand.Next(input.MiscRaceData.WaistClothingTypesCount);
                    }

                    if (unit.ClothingType2 > 0 &&
                        input.MiscRaceData.AllowedWaistTypes[unit.ClothingType2 - 1].CanWear(unit) == false)
                    {
                        unit.ClothingType2 = 0;
                    }
                }

                if (input.MiscRaceData.ExtraMainClothing1Count > 0)
                {
                    unit.ClothingExtraType1 = State.Rand.Next(input.MiscRaceData.ExtraMainClothing1Count);
                    for (int i = 0; i < 50; i++)
                    {
                        if (unit.ClothingExtraType1 > 0)
                        {
                            if (input.MiscRaceData.ExtraMainClothing1Types[unit.ClothingExtraType1 - 1].CanWear(unit))
                            {
                                break;
                            }
                        }

                        unit.ClothingExtraType1 = State.Rand.Next(input.MiscRaceData.ExtraMainClothing1Count);
                    }

                    if (unit.ClothingExtraType1 > 0 &&
                        input.MiscRaceData.ExtraMainClothing1Types[unit.ClothingExtraType1 - 1].CanWear(unit) == false)
                    {
                        unit.ClothingExtraType1 = 0;
                    }
                }

                if (input.MiscRaceData.ExtraMainClothing2Count > 0)
                {
                    unit.ClothingExtraType2 = State.Rand.Next(input.MiscRaceData.ExtraMainClothing2Count);
                    for (int i = 0; i < 50; i++)
                    {
                        if (unit.ClothingExtraType2 > 0)
                        {
                            if (input.MiscRaceData.ExtraMainClothing2Types[unit.ClothingExtraType2 - 1].CanWear(unit))
                            {
                                break;
                            }
                        }

                        unit.ClothingExtraType2 = State.Rand.Next(input.MiscRaceData.ExtraMainClothing2Count);
                    }

                    if (unit.ClothingExtraType2 > 0 &&
                        input.MiscRaceData.ExtraMainClothing2Types[unit.ClothingExtraType2 - 1].CanWear(unit) == false)
                    {
                        unit.ClothingExtraType2 = 0;
                    }
                }

                if (input.MiscRaceData.ExtraMainClothing3Count > 0)
                {
                    unit.ClothingExtraType3 = State.Rand.Next(input.MiscRaceData.ExtraMainClothing3Count);
                    for (int i = 0; i < 50; i++)
                    {
                        if (unit.ClothingExtraType3 > 0)
                        {
                            if (input.MiscRaceData.ExtraMainClothing3Types[unit.ClothingExtraType3 - 1].CanWear(unit))
                            {
                                break;
                            }
                        }

                        unit.ClothingExtraType3 = State.Rand.Next(input.MiscRaceData.ExtraMainClothing3Count);
                    }

                    if (unit.ClothingExtraType3 > 0 &&
                        input.MiscRaceData.ExtraMainClothing3Types[unit.ClothingExtraType3 - 1].CanWear(unit) == false)
                    {
                        unit.ClothingExtraType3 = 0;
                    }
                }

                if (input.MiscRaceData.ExtraMainClothing4Count > 0)
                {
                    unit.ClothingExtraType4 = State.Rand.Next(input.MiscRaceData.ExtraMainClothing4Count);
                    for (int i = 0; i < 50; i++)
                    {
                        if (unit.ClothingExtraType4 > 0)
                        {
                            if (input.MiscRaceData.ExtraMainClothing4Types[unit.ClothingExtraType4 - 1].CanWear(unit))
                            {
                                break;
                            }
                        }

                        unit.ClothingExtraType4 = State.Rand.Next(input.MiscRaceData.ExtraMainClothing4Count);
                    }

                    if (unit.ClothingExtraType4 > 0 &&
                        input.MiscRaceData.ExtraMainClothing1Types[unit.ClothingExtraType4 - 1].CanWear(unit) == false)
                    {
                        unit.ClothingExtraType4 = 0;
                    }
                }

                if (input.MiscRaceData.ExtraMainClothing5Count > 0)
                {
                    unit.ClothingExtraType5 = State.Rand.Next(input.MiscRaceData.ExtraMainClothing5Count);
                    for (int i = 0; i < 50; i++)
                    {
                        if (unit.ClothingExtraType5 > 0)
                        {
                            if (input.MiscRaceData.ExtraMainClothing5Types[unit.ClothingExtraType5 - 1].CanWear(unit))
                            {
                                break;
                            }
                        }

                        unit.ClothingExtraType5 = State.Rand.Next(input.MiscRaceData.ExtraMainClothing5Count);
                    }

                    if (unit.ClothingExtraType5 > 0 &&
                        input.MiscRaceData.ExtraMainClothing5Types[unit.ClothingExtraType5 - 1].CanWear(unit) == false)
                    {
                        unit.ClothingExtraType5 = 0;
                    }
                }

                if (Config.AllowTopless)
                {
                    if (State.Rand.Next(5) == 0)
                    {
                        unit.ClothingType = 0;
                    }
                }

                if (unit.Race == Race.Lizards && Config.LizardsHaveNoBreasts)
                {
                    unit.ClothingType = 0;
                }
            }
            else
            {
                unit.ClothingType = 0;
            }

            if (Config.RagsForSlaves && State.World?.MainEmpires != null &&
                (State.World.GetEmpireOfRace(unit.Race)?.IsEnemy(State.World.GetEmpireOfSide(unit.Side)) ?? false) &&
                unit.ImmuneToDefections == false)
            {
                unit.ClothingType = 1 + input.MiscRaceData.AllowedMainClothingTypes.IndexOf(ClothingTypes.RagsInstance);
                if (unit.ClothingType == 0) //Covers rags not in the list
                {
                    if (input.MiscRaceData.AllowedMainClothingTypes.Last()?.FixedData.ReqWinterHoliday ==
                        false) //Avoid bugs where the winter holiday is the last.
                    {
                        unit.ClothingType = input.MiscRaceData.AllowedMainClothingTypes.Count;
                    }
                }
            }
        }
        else
        {
            unit.ClothingType = 0;
        }

        if (input.MiscRaceData.FurCapable)
        {
            var raceStats = State.RaceSettings.Get(unit.Race);
            float furryFraction;
            if (raceStats.OverrideFurry)
            {
                furryFraction = raceStats.furryFraction;
            }
            else
            {
                furryFraction = Config.FurryFraction;
            }

            unit.Furry = State.Rand.NextDouble() < furryFraction;
        }
        else
        {
            unit.Furry = false;
        }


        if (unit.HasDick)
        {
            if (State.RaceSettings.GetOverrideDick(unit.Race))
            {
                int min = State.RaceSettings.Get(unit.Race).MinDick;
                int max = State.RaceSettings.Get(unit.Race).MaxDick;
                unit.DickSize = min + State.Rand.Next(max - min);
                unit.DickSize = Mathf.Clamp(unit.DickSize, 0, input.MiscRaceData.DickSizes() - 1);
            }
            else
            {
                unit.DickSize =
                    Mathf.Clamp(
                        State.Rand.Next(input.MiscRaceData.DickSizes()) +
                        Config.CockSizeModifier * input.MiscRaceData.DickSizes() / 6, 0,
                        input.MiscRaceData.DickSizes() - 1);
            }
        }

        if (unit.HasBreasts)
        {
            if (State.RaceSettings.GetOverrideBreasts(unit.Race))
            {
                int min = State.RaceSettings.Get(unit.Race).MinBoob;
                int max = State.RaceSettings.Get(unit.Race).MaxBoob;
                unit.SetDefaultBreastSize(min + State.Rand.Next(max - min));
                unit.SetDefaultBreastSize(Mathf.Clamp(unit.DefaultBreastSize, 0, input.MiscRaceData.BreastSizes() - 1));
            }
            else
            {
                if (unit.HasDick)
                {
                    unit.SetDefaultBreastSize(Mathf.Clamp(
                        State.Rand.Next(input.MiscRaceData.BreastSizes()) +
                        Config.HermBreastSizeModifier * input.MiscRaceData.BreastSizes() / 6, 0,
                        input.MiscRaceData.BreastSizes() - 1));
                }
                else
                {
                    unit.SetDefaultBreastSize(Mathf.Clamp(
                        State.Rand.Next(input.MiscRaceData.BreastSizes()) +
                        Config.BreastSizeModifier * input.MiscRaceData.BreastSizes() / 6, 0,
                        input.MiscRaceData.BreastSizes() - 1));
                }
            }
        }

        if (Config.HairMatchesFur && input.MiscRaceData.FurCapable)
        {
            unit.HairColor = unit.AccessoryColor;
        }
    };


    public static Action<IRunInput, IRunOutput> BasicBellyRunAfter = (input, output) =>
    {
        if (input.Actor.HasBelly)
        {
            output.changeSprite(SpriteType.Belly).SetActive(true).SetLocalScale(new Vector3(1, 1, 1));
        }
    };

    internal static Color WhiteColored2 = Color.white;

    static Defaults()
    {

        SpriteGens3[SpriteType.Body] = new SingleRenderFunc<IParameters>(2, (input, output) =>
        {
            output.Coloring(FurryColor(input.Actor));
            int attackingOffset = input.Actor.IsAttacking ? 1 : 0;
            if (input.Actor.Unit.BodySize == 0)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Bodies[attackingOffset]);
                return;
            }

            int GenderOffset = input.Actor.Unit.HasBreasts ? 0 : 8;

            output.Sprite(input.Actor.HasBodyWeight ? State.GameManager.SpriteDictionary.Legs[(input.Actor.Unit.BodySize - 1) * 2 + GenderOffset + attackingOffset] : null);
        });


        SpriteGens3[SpriteType.Head] = new SingleRenderFunc<IParameters>(4, (input, output) =>
        {
            output.Coloring(FurryColor(input.Actor));
            int eatingOffset = input.Actor.IsEating ? 1 : 0;

            if (input.Actor.Unit.Furry)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Bodies[6 + eatingOffset]);
            }
            else if (input.Actor.Unit.BreastSize >= 0)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Bodies[2 + eatingOffset]);
            }
            else
            {
                output.Sprite(State.GameManager.SpriteDictionary.Bodies[4 + eatingOffset]);
            }
        });

        SpriteGens3[SpriteType.Hair] = new SingleRenderFunc<IParameters>(6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
            output.Sprite(State.GameManager.SpriteDictionary.Hair[input.Actor.Unit.HairStyle]);
        });

        SpriteGens3[SpriteType.Hair2] = new SingleRenderFunc<IParameters>(1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
            if (input.Actor.Unit.HairStyle == 1)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Hair[input.RaceData.HairStyles]);
                return;
            }

            if (input.Actor.Unit.HairStyle == 2)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Hair[input.RaceData.HairStyles + 1]);
                return;
            }

            if (input.Actor.Unit.HairStyle == 5)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Hair[input.RaceData.HairStyles + 3]);
                return;
            }

            if (input.Actor.Unit.HairStyle == 6 || input.Actor.Unit.HairStyle == 7)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Hair[input.RaceData.HairStyles + 2]);
            }
        });

        SpriteGens3[SpriteType.Mouth] = new SingleRenderFunc<IParameters>(5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Mouth, input.Actor.Unit.SkinColor));
            if (input.BaseBody)
            {
                if (input.Actor.Unit.HasBreasts)
                {
                    output.AddOffset(0, 0);
                }
                else
                {
                    output.AddOffset(0, -.625f);
                }
            }

            output.Sprite(input.Actor.IsEating == false ? State.GameManager.SpriteDictionary.Mouths[input.Actor.Unit.MouthType] : null);
        });

        SpriteGens3[SpriteType.Belly] = new SingleRenderFunc<IParameters>(15, (input, output) =>
        {
            output.Coloring(FurryBellyColor(input.Actor));
            if (input.Actor.HasBelly)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.Bellies[17]).AddOffset(0, -30 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.Bellies[16]).AddOffset(0, -30 * .625f);
                    return;
                }

                output.Sprite(State.GameManager.SpriteDictionary.Bellies[input.Actor.GetStomachSize()]);
            }
        });

        SpriteGens3[SpriteType.Weapon] = new SingleRenderFunc<IParameters>(1, (input, output) =>
        {
            output.Coloring(WhiteColored2);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Weapons[input.Actor.GetWeaponSprite()]);
            }
        });

        SpriteGens3[SpriteType.BodySize] = new SingleRenderFunc<IParameters>(3, (input, output) =>
        {    
            output.Coloring(ColorPaletteMap.FurryBellySwap);
            output.Sprite(input.Actor.Unit.Furry ? State.GameManager.SpriteDictionary.FurryTorsos[Mathf.Clamp(input.Actor.GetBodyWeight(), 0, 3)] : null);
        });

        SpriteGens3[SpriteType.Eyes] = new SingleRenderFunc<IParameters>(5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.EyeColor, input.Actor.Unit.EyeColor));
            output.Sprite(State.GameManager.SpriteDictionary.Eyes[Math.Min(input.Actor.Unit.EyeType, input.RaceData.EyeTypes - 1)]);

        });

        SpriteGens3[SpriteType.Breasts] = new SingleRenderFunc<IParameters>(16, (input, output) =>
        {
            output.Coloring(FurryBellyColor(input.Actor));
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.SquishedBreasts && input.Actor.Unit.BreastSize >= 3 && input.Actor.Unit.BreastSize <= 6)
            {
                output.Sprite(State.GameManager.SpriteDictionary.SquishedBreasts[input.Actor.Unit.BreastSize - 3]);
                return;
            }

            output.Sprite(State.GameManager.SpriteDictionary.Breasts[input.Actor.Unit.BreastSize]);
        });

        SpriteGens3[SpriteType.Dick] = new SingleRenderFunc<IParameters>(9, (input, output) =>
        {
            output.Coloring(FurryColor(input.Actor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.Unit.Furry && Config.FurryGenitals)
            {
                if (input.Actor.IsErect() == false)
                {
                    return;
                }

                int type = 0;
                if (input.Actor.IsCockVoring)
                {
                    type = 5;
                }
                else
                {
                    switch (input.Actor.Unit.Race)
                    {
                        case Race.Bunnies:
                            type = 0;
                            break;
                        case Race.Foxes:
                        case Race.Dogs:
                        case Race.Wolves:
                            type = 1;
                            break;
                        case Race.Cats:
                        case Race.Tigers:
                            type = 2;
                            break;
                    }
                }

                output.Coloring(WhiteColored);
                if (input.Actor.PredatorComponent?.VisibleFullness < .75f)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[24 + type]).Layer(18);
                    return;
                }

                output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[30 + type]).Layer(12);
                return;
            }

            if (input.Actor.IsErect())
            {
                if (input.Actor.PredatorComponent?.VisibleFullness < .75f)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.ErectDicks[input.Actor.Unit.DickSize]).Layer(18);
                    return;
                }

                output.Sprite(State.GameManager.SpriteDictionary.Dicks[input.Actor.Unit.DickSize]).Layer(12);
                return;
            }


            try
            {
                output.Sprite(State.GameManager.SpriteDictionary.Dicks[input.Actor.Unit.DickSize]).Layer(9);
            }
            catch (Exception e)
            {
                Debug.Log(input.Actor.Unit.Race);
                Debug.Log(e);
                Debug.Log(State.GameManager.SpriteDictionary.Dicks.Length + " vs " + input.Actor.Unit.DickSize);
                throw e;
            }
        });

        SpriteGens3[SpriteType.Balls] = new SingleRenderFunc<IParameters>(8, (input, output) =>
        {
            output.Coloring(FurryColor(input.Actor));
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.Unit.Furry && Config.FurryGenitals)
            {
                int size = input.Actor.Unit.DickSize;
                int offset = input.Actor.GetBallSize(18, .8f);
                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 18)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[42]).AddOffset(0, -23 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && offset == 18)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[41]).AddOffset(0, -23 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && offset == 17)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[40]).AddOffset(0, -20 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && offset == 16)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[39]).AddOffset(0, -19 * .625f);
                    return;
                }

                if (offset >= 15)
                {
                    output.AddOffset(0, -16 * .625f);
                }
                else if (offset >= 13)
                {
                    output.AddOffset(0, -13 * .625f);
                }
                else if (offset == 12)
                {
                    output.AddOffset(0, -8 * .625f);
                }
                else if (offset == 11)
                {
                    output.AddOffset(0, -5 * .625f);
                }
                else if (offset == 10)
                {
                    output.AddOffset(0, -1 * .625f);
                }

                if (offset > 0 && offset <= 12)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[Math.Min(11 + offset, 23)]);
                    return;
                }

                if (offset > 12)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[Math.Min(23 + offset, 38)]);
                    return;
                }

                output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[size]);
                return;
            }

            int baseSize = input.Actor.Unit.DickSize / 3;
            int ballOffset = input.Actor.GetBallSize(21);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .9f) == 21)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Balls[24]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .9f) == 21)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Balls[23]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .9f) == 20)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Balls[22]).AddOffset(0, -15 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .9f) == 19)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Balls[21]).AddOffset(0, -14 * .625f);
                return;
            }

            int combined = Math.Min(baseSize + ballOffset + 3, 20);
            if (false) // combined == 21 is always false 
            {
                output.AddOffset(0, -14 * .625f);
            }

            if (combined == 20)
            {
                output.AddOffset(0, -12 * .625f);
            }
            else if (combined >= 17 && true /*combined <= 19*/)
            {
                output.AddOffset(0, -8 * .625f);
            }

            if (ballOffset > 0)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Balls[combined]);
                return;
            }

            output.Sprite(State.GameManager.SpriteDictionary.Balls[baseSize]);
        });

        SpriteGens3[SpriteType.BodyAccent] = new SingleRenderFunc<IParameters>(6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, input.Actor.Unit.AccessoryColor));
            int thinOffset = input.Actor.Unit.BodySize < 2 ? 8 : 0;
            output.Sprite(Config.FurryHandsAndFeet || input.Actor.Unit.Furry ? State.GameManager.SpriteDictionary.FurryHandsAndFeet[thinOffset + (input.Actor.IsAttacking ? 1 : 0)] : null);

        });

        SpriteGens3[SpriteType.BodyAccent2] = new SingleRenderFunc<IParameters>(6, (input, output) =>
        {
            output.Coloring(WhiteColored2);
            int thinOffset = input.Actor.Unit.BodySize < 2 ? 8 : 0;
            output.Sprite(Config.FurryHandsAndFeet || input.Actor.Unit.Furry ? State.GameManager.SpriteDictionary.FurryHandsAndFeet[2 + thinOffset + (input.Actor.IsAttacking ? 1 : 0)] : null);

        });

        SpriteGens3[SpriteType.BodyAccent3] = new SingleRenderFunc<IParameters>(7, (input, output) =>
        {
            output.Coloring(WhiteColored2);
            if (Config.FurryFluff == false)
            {
                return;
            }

            int thinOffset = input.Actor.Unit.BodySize < 2 ? 8 : 0;
            output.Sprite(Config.FurryHandsAndFeet || input.Actor.Unit.Furry ? State.GameManager.SpriteDictionary.FurryHandsAndFeet[4 + thinOffset + (input.Actor.IsAttacking ? 1 : 0)] : null);

        });

        SpriteGens3[SpriteType.BodyAccent4] = new SingleRenderFunc<IParameters>(5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.NormalHair, input.Actor.Unit.HairColor));
            output.Sprite(State.GameManager.SpriteDictionary.Eyebrows[Math.Min(input.Actor.Unit.EyeType, State.GameManager.SpriteDictionary.Eyebrows.Length - 1)]);
        });
        
        
        
        
        
        
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SpriteGens2[SpriteType.Body] = (input, output) =>
        {
            int attackingOffset = input.Actor.IsAttacking ? 1 : 0;
            if (input.Actor.Unit.BodySize == 0)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Bodies[attackingOffset]);
                return;
            }

            int GenderOffset = input.Actor.Unit.HasBreasts ? 0 : 8;

            output.Sprite(input.Actor.HasBodyWeight ? State.GameManager.SpriteDictionary.Legs[(input.Actor.Unit.BodySize - 1) * 2 + GenderOffset + attackingOffset] : null);
        };

        SpriteGens2[SpriteType.Head] = (input, output) =>
        {
            int eatingOffset = input.Actor.IsEating ? 1 : 0;

            if (input.Actor.Unit.Furry)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Bodies[6 + eatingOffset]);
            }
            else if (input.Actor.Unit.BreastSize >= 0)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Bodies[2 + eatingOffset]);
            }
            else
            {
                output.Sprite(State.GameManager.SpriteDictionary.Bodies[4 + eatingOffset]);
            }
        };


        SpriteGens2[SpriteType.Hair] = (input, output) =>
        {
            output.Sprite(State.GameManager.SpriteDictionary.Hair[input.Actor.Unit.HairStyle]);
        };


        SpriteGens2[SpriteType.Hair2] = (input, output) =>
        {
            if (input.Actor.Unit.HairStyle == 1)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Hair[input.RaceData.HairStyles]);
                return;
            }

            if (input.Actor.Unit.HairStyle == 2)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Hair[input.RaceData.HairStyles + 1]);
                return;
            }

            if (input.Actor.Unit.HairStyle == 5)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Hair[input.RaceData.HairStyles + 3]);
                return;
            }

            if (input.Actor.Unit.HairStyle == 6 || input.Actor.Unit.HairStyle == 7)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Hair[input.RaceData.HairStyles + 2]);
            }
        };

        SpriteGens2[SpriteType.Mouth] = (input, output) =>
        {
            if (input.BaseBody)
            {
                if (input.Actor.Unit.HasBreasts)
                {
                    output.AddOffset(0, 0);
                }
                else
                {
                    output.AddOffset(0, -.625f);
                }
            }

            output.Sprite(input.Actor.IsEating == false ? State.GameManager.SpriteDictionary.Mouths[input.Actor.Unit.MouthType] : null);
        };


        SpriteGens2[SpriteType.Belly] = (input, output) =>
        {
            if (input.Actor.HasBelly)
            {
                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.Bellies[17]).AddOffset(0, -30 * .625f);
                    return;
                }

                if (input.Actor.PredatorComponent.IsUnitOfSpecificationInPrey(Race.Selicia, false, PreyLocation.stomach, PreyLocation.womb) && input.Actor.GetStomachSize() == 15)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.Bellies[16]).AddOffset(0, -30 * .625f);
                    return;
                }

                output.Sprite(State.GameManager.SpriteDictionary.Bellies[input.Actor.GetStomachSize()]);
            }
        };


        SpriteGens2[SpriteType.Weapon] = (input, output) =>
        {
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Weapons[input.Actor.GetWeaponSprite()]);
            }
        };

        SpriteGens2[SpriteType.BodySize] = (input, output) =>
        {
            output.Sprite(input.Actor.Unit.Furry ? State.GameManager.SpriteDictionary.FurryTorsos[Mathf.Clamp(input.Actor.GetBodyWeight(), 0, 3)] : null);
        };

        SpriteGens2[SpriteType.Eyes] = (input, output) =>
        {
            output.Sprite(State.GameManager.SpriteDictionary.Eyes[Math.Min(input.Actor.Unit.EyeType, input.RaceData.EyeTypes - 1)]);
        };

        SpriteGens2[SpriteType.Breasts] = (input, output) =>
        {
            if (input.Actor.Unit.HasBreasts == false)
            {
                return;
            }

            if (input.Actor.SquishedBreasts && input.Actor.Unit.BreastSize >= 3 && input.Actor.Unit.BreastSize <= 6)
            {
                output.Sprite(State.GameManager.SpriteDictionary.SquishedBreasts[input.Actor.Unit.BreastSize - 3]);
                return;
            }

            output.Sprite(State.GameManager.SpriteDictionary.Breasts[input.Actor.Unit.BreastSize]);
        };

        SpriteGens2[SpriteType.Dick] = (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            output.Coloring(FurryColor(input.Actor));

            if (input.Actor.Unit.Furry && Config.FurryGenitals)
            {
                if (input.Actor.IsErect() == false)
                {
                    return;
                }

                int type = 0;
                if (input.Actor.IsCockVoring)
                {
                    type = 5;
                }
                else
                {
                    switch (input.Actor.Unit.Race)
                    {
                        case Race.Bunnies:
                            type = 0;
                            break;
                        case Race.Foxes:
                        case Race.Dogs:
                        case Race.Wolves:
                            type = 1;
                            break;
                        case Race.Cats:
                        case Race.Tigers:
                            type = 2;
                            break;
                    }
                }

                output.Coloring(WhiteColored);
                if (input.Actor.PredatorComponent?.VisibleFullness < .75f)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[24 + type]).Layer(18);
                    return;
                }

                output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[30 + type]).Layer(12);
                return;
            }

            if (input.Actor.IsErect())
            {
                if (input.Actor.PredatorComponent?.VisibleFullness < .75f)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.ErectDicks[input.Actor.Unit.DickSize]).Layer(18);
                    return;
                }

                output.Sprite(State.GameManager.SpriteDictionary.Dicks[input.Actor.Unit.DickSize]).Layer(12);
                return;
            }


            try
            {
                output.Sprite(State.GameManager.SpriteDictionary.Dicks[input.Actor.Unit.DickSize]).Layer(9);
            }
            catch (Exception e)
            {
                Debug.Log(input.Actor.Unit.Race);
                Debug.Log(e);
                Debug.Log(State.GameManager.SpriteDictionary.Dicks.Length + " vs " + input.Actor.Unit.DickSize);
                throw e;
            }
        };

        SpriteGens2[SpriteType.Balls] = (input, output) =>
        {
            if (input.Actor.Unit.HasDick == false)
            {
                return;
            }

            if (input.Actor.Unit.Furry && Config.FurryGenitals)
            {
                int size = input.Actor.Unit.DickSize;
                int offset = input.Actor.GetBallSize(18, .8f);
                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && offset == 18)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[42]).AddOffset(0, -23 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && offset == 18)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[41]).AddOffset(0, -23 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && offset == 17)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[40]).AddOffset(0, -20 * .625f);
                    return;
                }

                if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && offset == 16)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[39]).AddOffset(0, -19 * .625f);
                    return;
                }

                if (offset >= 15)
                {
                    output.AddOffset(0, -16 * .625f);
                }
                else if (offset >= 13)
                {
                    output.AddOffset(0, -13 * .625f);
                }
                else if (offset == 12)
                {
                    output.AddOffset(0, -8 * .625f);
                }
                else if (offset == 11)
                {
                    output.AddOffset(0, -5 * .625f);
                }
                else if (offset == 10)
                {
                    output.AddOffset(0, -1 * .625f);
                }

                if (offset > 0 && offset <= 12)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[Math.Min(11 + offset, 23)]);
                    return;
                }

                if (offset > 12)
                {
                    output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[Math.Min(23 + offset, 38)]);
                    return;
                }

                output.Sprite(State.GameManager.SpriteDictionary.FurryDicks[size]);
                return;
            }

            int baseSize = input.Actor.Unit.DickSize / 3;
            int ballOffset = input.Actor.GetBallSize(21);
            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .9f) == 21)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Balls[24]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .9f) == 21)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Balls[23]).AddOffset(0, -18 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .9f) == 20)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Balls[22]).AddOffset(0, -15 * .625f);
                return;
            }

            if ((input.Actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, PreyLocation.balls) ?? false) && input.Actor.GetBallSize(21, .9f) == 19)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Balls[21]).AddOffset(0, -14 * .625f);
                return;
            }

            int combined = Math.Min(baseSize + ballOffset + 3, 20);
            if (false) // combined == 21 is always false 
            {
                output.AddOffset(0, -14 * .625f);
            }

            if (combined == 20)
            {
                output.AddOffset(0, -12 * .625f);
            }
            else if (combined >= 17 && true /*combined <= 19*/)
            {
                output.AddOffset(0, -8 * .625f);
            }

            if (ballOffset > 0)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Balls[combined]);
                return;
            }

            output.Sprite(State.GameManager.SpriteDictionary.Balls[baseSize]);
        };

        SpriteGens2[SpriteType.BodyAccent] = (input, output) =>
        {
            int thinOffset = input.Actor.Unit.BodySize < 2 ? 8 : 0;
            output.Sprite(Config.FurryHandsAndFeet || input.Actor.Unit.Furry ? State.GameManager.SpriteDictionary.FurryHandsAndFeet[thinOffset + (input.Actor.IsAttacking ? 1 : 0)] : null);
        };

        SpriteGens2[SpriteType.BodyAccent2] = (input, output) =>
        {
            int thinOffset = input.Actor.Unit.BodySize < 2 ? 8 : 0;
            output.Sprite(Config.FurryHandsAndFeet || input.Actor.Unit.Furry ? State.GameManager.SpriteDictionary.FurryHandsAndFeet[2 + thinOffset + (input.Actor.IsAttacking ? 1 : 0)] : null);
        };

        SpriteGens2[SpriteType.BodyAccent3] = (input, output) =>
        {
            if (Config.FurryFluff == false)
            {
                return;
            }

            int thinOffset = input.Actor.Unit.BodySize < 2 ? 8 : 0;
            output.Sprite(Config.FurryHandsAndFeet || input.Actor.Unit.Furry ? State.GameManager.SpriteDictionary.FurryHandsAndFeet[4 + thinOffset + (input.Actor.IsAttacking ? 1 : 0)] : null);
        };

        SpriteGens2[SpriteType.BodyAccent4] = (input, output) =>
        {
            output.Sprite(State.GameManager.SpriteDictionary.Eyebrows[Math.Min(input.Actor.Unit.EyeType, State.GameManager.SpriteDictionary.Eyebrows.Length - 1)]);
        };
    }


    public static MiscRaceDataReadable<IParameters> Default()
    {
        return Default<IParameters>();
    }

    public static MiscRaceDataReadable<T> Default<T>() where T : IParameters
    {
        MiscRaceDataReadable<T> miscRaceDataReadable = new MiscRaceDataReadable<T>(
            () => Config.AllowHugeBreasts ? State.GameManager.SpriteDictionary.Breasts.Length : State.GameManager.SpriteDictionary.Breasts.Length - 3,
            () => Config.AllowHugeDicks ? State.GameManager.SpriteDictionary.Dicks.Length : State.GameManager.SpriteDictionary.Dicks.Length - 3,
            false,
            new List<Gender> { Gender.Female, Gender.Male, Gender.Hermaphrodite, Gender.Gynomorph, Gender.Maleherm, Gender.Andromorph, Gender.Agenic },
            false,
            false,
            false,
            false,
            ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.NormalHair),
            15,
            ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Skin),
            ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.NormalHair),
            8,
            2,
            ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.EyeColor),
            1,
            5,
            0,
            0,
            State.GameManager.SpriteDictionary.Mouths.Length,
            1,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            1,
            1,
            1,
            1,
            3,
            ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing),
            new Vector2(),
            new Vector3()
        );

        miscRaceDataReadable.AllowedMainClothingTypes.Set(
            (IClothing<T>)ClothingTypes.BikiniTopInstance,
            (IClothing<T>)ClothingTypes.BeltTopInstance,
            (IClothing<T>)ClothingTypes.StrapTopInstance,
            (IClothing<T>)ClothingTypes.LeotardInstance,
            (IClothing<T>)ClothingTypes.BlackTopInstance,
            (IClothing<T>)ClothingTypes.RagsInstance,
            (IClothing<T>)ClothingTypes.FemaleVillagerInstance,
            (IClothing<T>)ClothingTypes.MaleVillagerInstance
        );

        miscRaceDataReadable.AllowedWaistTypes.Set(
            (IClothing<T>)ClothingTypes.BikiniBottomInstance,
            (IClothing<T>)ClothingTypes.LoinclothInstance,
            (IClothing<T>)ClothingTypes.ShortsInstance
        );

        miscRaceDataReadable.AllowedClothingHatTypes.Set(
            (IClothing<T>)MainAccessories.SantaHatInstance
        );

        return miscRaceDataReadable;
    }

    public static MiscRaceDataReadable<IParameters> Blank()
    {
        return Blank<IParameters>();
    }

    public static MiscRaceDataReadable<T> Blank<T>() where T : IParameters
    {
        MiscRaceDataReadable<T> miscRaceDataReadable = new MiscRaceDataReadable<T>(
            () => Config.AllowHugeBreasts ? State.GameManager.SpriteDictionary.Breasts.Length : State.GameManager.SpriteDictionary.Breasts.Length - 3,
            () => Config.AllowHugeDicks ? State.GameManager.SpriteDictionary.Dicks.Length : State.GameManager.SpriteDictionary.Dicks.Length - 3,
            false,
            new List<Gender> { Gender.Female, Gender.Male, Gender.Hermaphrodite, Gender.Gynomorph, Gender.Maleherm, Gender.Andromorph, Gender.Agenic },
            false,
            false,
            false,
            false,
            1, // Different
            1, // Different
            1, // Different
            1, // Different
            1, // Different
            0, // Different
            1, // Different
            1,
            0, // Different
            0,
            0,
            0, // Different
            0, // Different
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            1,
            1,
            1,
            1,
            0, // Different
            ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Clothing),
            new Vector2(),
            new Vector3()
        );

        return miscRaceDataReadable;
    }


    public static Color WhiteColored = Color.white;

    public static Color WhiteColored3(Actor_Unit actor)
    {
        return Color.white;
    }
}
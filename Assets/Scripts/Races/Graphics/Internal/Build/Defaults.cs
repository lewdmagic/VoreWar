#region

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#endregion

public static class Defaults
{
    internal static readonly SpriteTypeIndexed<SingleRenderFunc> SpriteGens3 = new SpriteTypeIndexed<SingleRenderFunc>();

    
    internal static void ModifySingleRender(SpriteType spriteType, ModdingMode mode, Action<IRaceRenderInput, IRaceRenderOutput> generator)
    {
        SingleRenderFunc current = SpriteGens3[spriteType];
        if (current != null)
        {
            if (mode == ModdingMode.Before)
            {
                current.ModBefore(generator);
            }

            if (mode == ModdingMode.After)
            {
                current.ModAfter(generator);
            }
        }
        else
        {
            throw new Exception("Tried to modify " + spriteType + " which does not exist. Use ReplaceSingleRender instead");
        }
    }
    
    
    public static readonly Func<IActorUnit, ColorSwapPalette> FurryColor = actor =>
    {
        if (actor.Unit.Furry)
        {
            return ColorPaletteMap.GetPalette(SwapType.Fur, actor.Unit.AccessoryColor);
        }

        return ColorPaletteMap.GetPalette(SwapType.Skin, actor.Unit.SkinColor);
    };

    public static readonly Func<IActorUnit, ColorSwapPalette> FurryColor2 = actor =>
    {
        if (actor.Unit.Furry)
        {
            return ColorPaletteMap.GetPalette(SwapType.Fur, actor.Unit.AccessoryColor);
        }

        return ColorPaletteMap.GetPalette(SwapType.Skin, actor.Unit.SkinColor);
    };


    private static readonly Func<IActorUnit, ColorSwapPalette> FurryBellyColor = actor =>
    {
        if (actor.Unit.Furry)
        {
            return ColorPaletteMap.FurryBellySwap;
        }

        return ColorPaletteMap.GetPalette(SwapType.Skin, actor.Unit.SkinColor);
    };

    public static readonly Action<IRunInput, IRunOutput> Finalize = (input, output) =>
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

            output.ChangeSprite(SpriteType.Belly).SetActive(true).SetLocalScale(localScale);
        }
    };

    public static readonly Action<IRandomCustomInput> RandomCustom = input =>
    {
        IUnitRead unit = input.Unit;
        if (input.SetupOutput.BodySizes > 0)
        {
            if (State.RaceSettings.GetOverrideWeight(unit.Race))
            {
                int min = State.RaceSettings.Get(unit.Race).MinWeight;
                int max = State.RaceSettings.Get(unit.Race).MaxWeight;
                unit.BodySize = min + State.Rand.Next(max - min);
                unit.BodySize = Mathf.Clamp(unit.BodySize, 0, input.SetupOutput.BodySizes - 1);
            }
            else
            {
                unit.BodySize = Mathf.Min(Config.DefaultStartingWeight, input.SetupOutput.BodySizes - 1);
            }
        }
        else
        {
            unit.BodySize = 0;
        }

        if (input.SetupOutput.HairStyles == 15)
        {
            if (unit.HasDick && unit.HasBreasts)
            {
                unit.HairStyle = State.Rand.Next(Config.HermsOnlyUseFemaleHair ? 8 : input.SetupOutput.HairStyles);
            }
            else if (unit.HasDick && Config.FemaleHairForMales)
            {
                unit.HairStyle = State.Rand.Next(input.SetupOutput.HairStyles);
            }
            else if (unit.HasDick == false && Config.MaleHairForFemales)
            {
                unit.HairStyle = State.Rand.Next(input.SetupOutput.HairStyles);
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
            unit.HairStyle = State.Rand.Next(input.SetupOutput.HairStyles);
        }

        unit.HairColor = State.Rand.Next(input.SetupOutput.HairColors);
        unit.AccessoryColor = State.Rand.Next(input.SetupOutput.AccessoryColors);

        if (Config.ExtraRandomHairColors)
        {
            if (input.SetupOutput.HairColors == ColorPaletteMap.GetPaletteCount(SwapType.NormalHair))
            {
                unit.HairColor = State.Rand.Next(input.SetupOutput.HairColors);
            }

            if (input.SetupOutput.AccessoryColors == ColorPaletteMap.GetPaletteCount(SwapType.Fur))
            {
                unit.AccessoryColor = State.Rand.Next(input.SetupOutput.AccessoryColors);
            }
        }
        else
        {
            if (input.SetupOutput.HairColors == ColorPaletteMap.GetPaletteCount(SwapType.NormalHair))
            {
                unit.HairColor = State.Rand.Next(ColorPaletteMap.MixedHairColors);
            }

            if (input.SetupOutput.AccessoryColors == ColorPaletteMap.GetPaletteCount(SwapType.Fur))
            {
                unit.AccessoryColor = State.Rand.Next(ColorPaletteMap.MixedHairColors);
            }
        }

        unit.SkinColor = State.Rand.Next(input.SetupOutput.SkinColors);
        unit.EyeColor = State.Rand.Next(input.SetupOutput.EyeColors);
        unit.ExtraColor1 = State.Rand.Next(input.SetupOutput.ExtraColors1);
        unit.ExtraColor2 = State.Rand.Next(input.SetupOutput.ExtraColors2);
        unit.ExtraColor3 = State.Rand.Next(input.SetupOutput.ExtraColors3);
        unit.ExtraColor4 = State.Rand.Next(input.SetupOutput.ExtraColors4);
        unit.EyeType = State.Rand.Next(Math.Max(input.SetupOutput.EyeTypes - input.SetupOutput.AvoidedEyeTypes, 0));
        unit.ClothingColor = State.Rand.Next(ColorMap.ClothingColorCount);
        unit.MouthType =
            State.Rand.Next(Math.Max(input.SetupOutput.MouthTypes - input.SetupOutput.AvoidedMouthTypes, 0));
        unit.SpecialAccessoryType = State.Rand.Next(input.SetupOutput.SpecialAccessoryCount);

        if (unit.HasDick && unit.HasBreasts == false)
        {
            unit.BeardStyle = State.Rand.Next(input.SetupOutput.BeardStyles);
        }

        if (input.SetupOutput.ClothingAccessoryTypesCount > 1)
        {
            unit.ClothingAccessoryType = State.Rand.Next(input.SetupOutput.ClothingAccessoryTypesCount);
            for (int i = 0; i < 50; i++)
            {
                if (unit.ClothingAccessoryType > 0)
                {
                    if (input.SetupOutput.AllowedClothingAccessoryTypes[unit.ClothingAccessoryType - 1].CanWear(unit))
                    {
                        break;
                    }
                }

                unit.ClothingAccessoryType = State.Rand.Next(input.SetupOutput.ClothingAccessoryTypesCount);
            }

            if (unit.ClothingAccessoryType > 0 &&
                input.SetupOutput.AllowedClothingAccessoryTypes[unit.ClothingAccessoryType - 1].CanWear(unit) == false)
            {
                unit.ClothingAccessoryType = 0;
            }
        }

        if (input.SetupOutput.ClothingHatTypesCount > 1)
        {
            if (input.SetupOutput.AllowedClothingHatTypes.Contains(MainAccessories.SantaHatInstance) && Config.WinterActive())
            {
                if (State.Rand.Next(2) == 0)
                {
                    unit.ClothingHatType = 1;
                }
            }
            else
            {
                unit.ClothingHatType = State.Rand.Next(input.SetupOutput.ClothingHatTypesCount);
                for (int i = 0; i < 50; i++)
                {
                    if (unit.ClothingHatType > 0)
                    {
                        if (input.SetupOutput.AllowedClothingHatTypes[unit.ClothingHatType - 1].CanWear(unit))
                        {
                            break;
                        }
                    }

                    unit.ClothingHatType = State.Rand.Next(input.SetupOutput.ClothingHatTypesCount);
                }

                if (unit.ClothingHatType > 0 &&
                    input.SetupOutput.AllowedClothingHatTypes[unit.ClothingHatType - 1].CanWear(unit) == false)
                {
                    unit.ClothingHatType = 0;
                }
            }
        }


        if (input.SetupOutput.MainClothingTypesCount > 1)
        {
            float fraction = State.RaceSettings.GetOverrideClothed(unit.Race)
                ? State.RaceSettings.Get(unit.Race).clothedFraction
                : Config.ClothedFraction;
            if (State.Rand.NextDouble() < fraction)
            {
                unit.ClothingType = State.Rand.Next(Mathf.Max(
                    input.SetupOutput.MainClothingTypesCount - input.SetupOutput.AvoidedMainClothingTypes, 0));
                for (int i = 0; i < 50; i++)
                {
                    if (unit.ClothingType > 0)
                    {
                        if (input.SetupOutput.AllowedMainClothingTypes[unit.ClothingType - 1].CanWear(unit))
                        {
                            break;
                        }
                    }

                    unit.ClothingType = State.Rand.Next(Mathf.Max(
                        input.SetupOutput.MainClothingTypesCount - input.SetupOutput.AvoidedMainClothingTypes, 0));
                }

                if (unit.ClothingType > 0 &&
                    input.SetupOutput.AllowedMainClothingTypes[unit.ClothingType - 1].CanWear(unit) == false)
                {
                    unit.ClothingType = 0;
                }

                if (input.SetupOutput.WaistClothingTypesCount > 0)
                {
                    unit.ClothingType2 = State.Rand.Next(input.SetupOutput.WaistClothingTypesCount);
                    for (int i = 0; i < 50; i++)
                    {
                        if (unit.ClothingType2 > 0)
                        {
                            if (input.SetupOutput.AllowedWaistTypes[unit.ClothingType2 - 1].CanWear(unit))
                            {
                                break;
                            }
                        }

                        unit.ClothingType2 = State.Rand.Next(input.SetupOutput.WaistClothingTypesCount);
                    }

                    if (unit.ClothingType2 > 0 &&
                        input.SetupOutput.AllowedWaistTypes[unit.ClothingType2 - 1].CanWear(unit) == false)
                    {
                        unit.ClothingType2 = 0;
                    }
                }

                if (input.SetupOutput.ExtraMainClothing1Count > 0)
                {
                    unit.ClothingExtraType1 = State.Rand.Next(input.SetupOutput.ExtraMainClothing1Count);
                    for (int i = 0; i < 50; i++)
                    {
                        if (unit.ClothingExtraType1 > 0)
                        {
                            if (input.SetupOutput.ExtraMainClothing1Types[unit.ClothingExtraType1 - 1].CanWear(unit))
                            {
                                break;
                            }
                        }

                        unit.ClothingExtraType1 = State.Rand.Next(input.SetupOutput.ExtraMainClothing1Count);
                    }

                    if (unit.ClothingExtraType1 > 0 &&
                        input.SetupOutput.ExtraMainClothing1Types[unit.ClothingExtraType1 - 1].CanWear(unit) == false)
                    {
                        unit.ClothingExtraType1 = 0;
                    }
                }

                if (input.SetupOutput.ExtraMainClothing2Count > 0)
                {
                    unit.ClothingExtraType2 = State.Rand.Next(input.SetupOutput.ExtraMainClothing2Count);
                    for (int i = 0; i < 50; i++)
                    {
                        if (unit.ClothingExtraType2 > 0)
                        {
                            if (input.SetupOutput.ExtraMainClothing2Types[unit.ClothingExtraType2 - 1].CanWear(unit))
                            {
                                break;
                            }
                        }

                        unit.ClothingExtraType2 = State.Rand.Next(input.SetupOutput.ExtraMainClothing2Count);
                    }

                    if (unit.ClothingExtraType2 > 0 &&
                        input.SetupOutput.ExtraMainClothing2Types[unit.ClothingExtraType2 - 1].CanWear(unit) == false)
                    {
                        unit.ClothingExtraType2 = 0;
                    }
                }

                if (input.SetupOutput.ExtraMainClothing3Count > 0)
                {
                    unit.ClothingExtraType3 = State.Rand.Next(input.SetupOutput.ExtraMainClothing3Count);
                    for (int i = 0; i < 50; i++)
                    {
                        if (unit.ClothingExtraType3 > 0)
                        {
                            if (input.SetupOutput.ExtraMainClothing3Types[unit.ClothingExtraType3 - 1].CanWear(unit))
                            {
                                break;
                            }
                        }

                        unit.ClothingExtraType3 = State.Rand.Next(input.SetupOutput.ExtraMainClothing3Count);
                    }

                    if (unit.ClothingExtraType3 > 0 &&
                        input.SetupOutput.ExtraMainClothing3Types[unit.ClothingExtraType3 - 1].CanWear(unit) == false)
                    {
                        unit.ClothingExtraType3 = 0;
                    }
                }

                if (input.SetupOutput.ExtraMainClothing4Count > 0)
                {
                    unit.ClothingExtraType4 = State.Rand.Next(input.SetupOutput.ExtraMainClothing4Count);
                    for (int i = 0; i < 50; i++)
                    {
                        if (unit.ClothingExtraType4 > 0)
                        {
                            if (input.SetupOutput.ExtraMainClothing4Types[unit.ClothingExtraType4 - 1].CanWear(unit))
                            {
                                break;
                            }
                        }

                        unit.ClothingExtraType4 = State.Rand.Next(input.SetupOutput.ExtraMainClothing4Count);
                    }

                    if (unit.ClothingExtraType4 > 0 &&
                        input.SetupOutput.ExtraMainClothing1Types[unit.ClothingExtraType4 - 1].CanWear(unit) == false)
                    {
                        unit.ClothingExtraType4 = 0;
                    }
                }

                if (input.SetupOutput.ExtraMainClothing5Count > 0)
                {
                    unit.ClothingExtraType5 = State.Rand.Next(input.SetupOutput.ExtraMainClothing5Count);
                    for (int i = 0; i < 50; i++)
                    {
                        if (unit.ClothingExtraType5 > 0)
                        {
                            if (input.SetupOutput.ExtraMainClothing5Types[unit.ClothingExtraType5 - 1].CanWear(unit))
                            {
                                break;
                            }
                        }

                        unit.ClothingExtraType5 = State.Rand.Next(input.SetupOutput.ExtraMainClothing5Count);
                    }

                    if (unit.ClothingExtraType5 > 0 &&
                        input.SetupOutput.ExtraMainClothing5Types[unit.ClothingExtraType5 - 1].CanWear(unit) == false)
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

                if (Equals(unit.Race, Race.Lizards) && Config.LizardsHaveNoBreasts)
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
                unit.ClothingType = 1 + input.SetupOutput.AllowedMainClothingTypes.IndexOf(CommonClothing.RagsInstance);
                if (unit.ClothingType == 0) //Covers rags not in the list
                {
                    if (input.SetupOutput.AllowedMainClothingTypes.Last()?.FixedData.ReqWinterHoliday ==
                        false) //Avoid bugs where the winter holiday is the last.
                    {
                        unit.ClothingType = input.SetupOutput.AllowedMainClothingTypes.Count;
                    }
                }
            }
        }
        else
        {
            unit.ClothingType = 0;
        }

        if (input.SetupOutput.FurCapable)
        {
            var raceStats = State.RaceSettings.Get(unit.Race);
            var furryFraction = raceStats.OverrideFurry ? raceStats.furryFraction : Config.FurryFraction;

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
                unit.DickSize = Mathf.Clamp(unit.DickSize, 0, input.SetupOutput.DickSizes() - 1);
            }
            else
            {
                unit.DickSize =
                    Mathf.Clamp(
                        State.Rand.Next(input.SetupOutput.DickSizes()) +
                        Config.CockSizeModifier * input.SetupOutput.DickSizes() / 6, 0,
                        input.SetupOutput.DickSizes() - 1);
            }
        }

        if (unit.HasBreasts)
        {
            if (State.RaceSettings.GetOverrideBreasts(unit.Race))
            {
                int min = State.RaceSettings.Get(unit.Race).MinBoob;
                int max = State.RaceSettings.Get(unit.Race).MaxBoob;
                unit.SetDefaultBreastSize(min + State.Rand.Next(max - min));
                unit.SetDefaultBreastSize(Mathf.Clamp(unit.DefaultBreastSize, 0, input.SetupOutput.BreastSizes() - 1));
            }
            else
            {
                if (unit.HasDick)
                {
                    unit.SetDefaultBreastSize(Mathf.Clamp(
                        State.Rand.Next(input.SetupOutput.BreastSizes()) +
                        Config.HermBreastSizeModifier * input.SetupOutput.BreastSizes() / 6, 0,
                        input.SetupOutput.BreastSizes() - 1));
                }
                else
                {
                    unit.SetDefaultBreastSize(Mathf.Clamp(
                        State.Rand.Next(input.SetupOutput.BreastSizes()) +
                        Config.BreastSizeModifier * input.SetupOutput.BreastSizes() / 6, 0,
                        input.SetupOutput.BreastSizes() - 1));
                }
            }
        }

        if (Config.HairMatchesFur && input.SetupOutput.FurCapable)
        {
            unit.HairColor = unit.AccessoryColor;
        }
    };


    // TODO
    // Likely unnecessary 
    public static readonly Action<IRunInput, IRunOutput> BasicBellyRunAfter = (input, output) =>
    {
        if (input.Actor.HasBelly)
        {
            output.ChangeSprite(SpriteType.Belly).SetActive(true).SetLocalScale(new Vector3(1, 1, 1));
        }
    };

    static Defaults()
    {

        SpriteGens3[SpriteType.Body] = new SingleRenderFunc(2, (input, output) =>
        {
            output.Coloring(FurryColor(input.Actor));
            int attackingOffset = input.Actor.IsAttacking ? 1 : 0;
            if (input.Actor.Unit.BodySize == 0)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Bodies[attackingOffset]);
                return;
            }

            int genderOffset = input.Actor.Unit.HasBreasts ? 0 : 8;

            output.Sprite(input.Actor.HasBodyWeight ? State.GameManager.SpriteDictionary.Legs[(input.Actor.Unit.BodySize - 1) * 2 + genderOffset + attackingOffset] : null);
        });


        SpriteGens3[SpriteType.Head] = new SingleRenderFunc(4, (input, output) =>
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

        SpriteGens3[SpriteType.Hair] = new SingleRenderFunc(6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.NormalHair, input.Actor.Unit.HairColor));
            output.Sprite(State.GameManager.SpriteDictionary.Hair[input.Actor.Unit.HairStyle]);
        });

        SpriteGens3[SpriteType.Hair2] = new SingleRenderFunc(1, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.NormalHair, input.Actor.Unit.HairColor));
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

        SpriteGens3[SpriteType.Mouth] = new SingleRenderFunc(5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.Mouth, input.Actor.Unit.SkinColor));
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

        SpriteGens3[SpriteType.Belly] = new SingleRenderFunc(15, (input, output) =>
        {
            output.Coloring(FurryBellyColor(input.Actor));
            if (input.Actor.HasBelly)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Bellies[input.Actor.GetStomachSize()]);
            }
        });

        SpriteGens3[SpriteType.Weapon] = new SingleRenderFunc(1, (input, output) =>
        {
            output.Coloring(Color.white);
            if (input.Actor.Unit.HasWeapon && input.Actor.Surrendered == false)
            {
                output.Sprite(State.GameManager.SpriteDictionary.Weapons[input.Actor.GetWeaponSprite()]);
            }
        });

        SpriteGens3[SpriteType.BodySize] = new SingleRenderFunc(3, (input, output) =>
        {    
            output.Coloring(ColorPaletteMap.FurryBellySwap);
            output.Sprite(input.Actor.Unit.Furry ? State.GameManager.SpriteDictionary.FurryTorsos[Mathf.Clamp(input.Actor.GetBodyWeight(), 0, 3)] : null);
        });

        SpriteGens3[SpriteType.Eyes] = new SingleRenderFunc(5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.EyeColor, input.Actor.Unit.EyeColor));
            output.Sprite(State.GameManager.SpriteDictionary.Eyes[Math.Min(input.Actor.Unit.EyeType, input.RaceData.EyeTypes - 1)]);

        });

        SpriteGens3[SpriteType.Breasts] = new SingleRenderFunc(16, (input, output) =>
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

        SpriteGens3[SpriteType.Dick] = new SingleRenderFunc(9, (input, output) =>
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
                throw;
            }
        });

        SpriteGens3[SpriteType.Balls] = new SingleRenderFunc(8, (input, output) =>
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

        SpriteGens3[SpriteType.BodyAccent] = new SingleRenderFunc(6, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.Fur, input.Actor.Unit.AccessoryColor));
            int thinOffset = input.Actor.Unit.BodySize < 2 ? 8 : 0;
            output.Sprite(Config.FurryHandsAndFeet || input.Actor.Unit.Furry ? State.GameManager.SpriteDictionary.FurryHandsAndFeet[thinOffset + (input.Actor.IsAttacking ? 1 : 0)] : null);

        });

        SpriteGens3[SpriteType.BodyAccent2] = new SingleRenderFunc(6, (input, output) =>
        {
            output.Coloring(Color.white);
            int thinOffset = input.Actor.Unit.BodySize < 2 ? 8 : 0;
            output.Sprite(Config.FurryHandsAndFeet || input.Actor.Unit.Furry ? State.GameManager.SpriteDictionary.FurryHandsAndFeet[2 + thinOffset + (input.Actor.IsAttacking ? 1 : 0)] : null);

        });

        SpriteGens3[SpriteType.BodyAccent3] = new SingleRenderFunc(7, (input, output) =>
        {
            output.Coloring(Color.white);
            if (Config.FurryFluff == false)
            {
                return;
            }

            int thinOffset = input.Actor.Unit.BodySize < 2 ? 8 : 0;
            output.Sprite(Config.FurryHandsAndFeet || input.Actor.Unit.Furry ? State.GameManager.SpriteDictionary.FurryHandsAndFeet[4 + thinOffset + (input.Actor.IsAttacking ? 1 : 0)] : null);

        });

        SpriteGens3[SpriteType.BodyAccent4] = new SingleRenderFunc(5, (input, output) =>
        {
            output.Coloring(ColorPaletteMap.GetPalette(SwapType.NormalHair, input.Actor.Unit.HairColor));
            output.Sprite(State.GameManager.SpriteDictionary.Eyebrows[Math.Min(input.Actor.Unit.EyeType, State.GameManager.SpriteDictionary.Eyebrows.Length - 1)]);
        });
    }
    

    internal static SetupOutput Default()
    {
        return Default<IParameters>();
    }

    internal static SetupOutput Default<T>() where T : IParameters
    {
        
        /*
         *
    Func<int> breastSizes, 
    Func<int> dickSizes, 
    bool furCapable, 
    List<Gender> canBeGender, 
    bool extendedBreastSprites, 
    bool gentleAnimation, 
    bool baseBody, 
    bool weightGainDisabled, 
    int hairColors, 
    int hairStyles, 
    int skinColors, 
    int accessoryColors, 
    int eyeTypes, 
    int avoidedEyeTypes, 
    int eyeColors, 
    int secondaryEyeColors, 
    int bodySizes, 
    int specialAccessoryCount, 
    int beardStyles, 
    int mouthTypes, 
    int avoidedMouthTypes, 
    int extraColors1, 
    int extraColors2, 
    int extraColors3, 
    int extraColors4, 
    int headTypes, 
    int tailTypes, 
    int furTypes, 
    int earTypes, 
    int bodyAccentTypes1, 
    int bodyAccentTypes2, 
    int bodyAccentTypes3, 
    int bodyAccentTypes4, 
    int bodyAccentTypes5, 
    int ballsSizes, 
    int vulvaTypes, 
    int basicMeleeWeaponTypes, 
    int advancedMeleeWeaponTypes, 
    int basicRangedWeaponTypes, 
    int advancedRangedWeaponTypes, 
    int avoidedMainClothingTypes, 
    int clothingColors, 
    Vector2 wholeBodyOffset, 
    Vector3 clothingShift)
         * 
         */
        SetupOutput setupOutput = new SetupOutput(
            breastSizes: () => Config.AllowHugeBreasts ? State.GameManager.SpriteDictionary.Breasts.Length : State.GameManager.SpriteDictionary.Breasts.Length - 3,
            dickSizes: () => Config.AllowHugeDicks ? State.GameManager.SpriteDictionary.Dicks.Length : State.GameManager.SpriteDictionary.Dicks.Length - 3,
            furCapable: false,
            canBeGender: new List<Gender> { Gender.Female, Gender.Male, Gender.Hermaphrodite, Gender.Gynomorph, Gender.Maleherm, Gender.Andromorph, Gender.Agenic },
            extendedBreastSprites: false,
            gentleAnimation: false,
            baseBody: false,
            weightGainDisabled: false,
            hairColors: ColorPaletteMap.GetPaletteCount(SwapType.NormalHair),
            hairStyles: 15,
            skinColors: ColorPaletteMap.GetPaletteCount(SwapType.Skin),
            accessoryColors: ColorPaletteMap.GetPaletteCount(SwapType.NormalHair),
            eyeTypes: 8,
            avoidedEyeTypes : 2,
            eyeColors: ColorPaletteMap.GetPaletteCount(SwapType.EyeColor),
            secondaryEyeColors: 1,
            bodySizes: 5,
            specialAccessoryCount: 0,
            beardStyles: 0,
            mouthTypes: State.GameManager.SpriteDictionary.Mouths.Length,
            avoidedMouthTypes: 1,
            extraColors1: 0,
            extraColors2: 0,
            extraColors3: 0,
            extraColors4: 0,
            headTypes: 0,
            tailTypes: 0,
            furTypes: 0,
            earTypes: 0,
            bodyAccentTypes1: 0,
            bodyAccentTypes2: 0,
            bodyAccentTypes3: 0,
            bodyAccentTypes4: 0,
            bodyAccentTypes5: 0,
            ballsSizes: 0,
            vulvaTypes: 0,
            basicMeleeWeaponTypes: 1,
            advancedMeleeWeaponTypes: 1,
            basicRangedWeaponTypes: 1,
            advancedRangedWeaponTypes: 1,
            avoidedMainClothingTypes: 3,
            clothingColors: ColorPaletteMap.GetPaletteCount(SwapType.Clothing),
            wholeBodyOffset: new Vector2(),
            clothingShift: new Vector3()
        );

        setupOutput.AllowedMainClothingTypes.Set(
            CommonClothing.BikiniTopInstance,
            CommonClothing.BeltTopInstance,
            CommonClothing.StrapTopInstance,
            CommonClothing.LeotardInstance,
            CommonClothing.BlackTopInstance,
            CommonClothing.RagsInstance,
            CommonClothing.FemaleVillagerInstance,
            CommonClothing.MaleVillagerInstance
        );

        setupOutput.AllowedWaistTypes.Set(
            CommonClothing.BikiniBottomInstance,
            CommonClothing.LoinclothInstance,
            CommonClothing.ShortsInstance
        );

        setupOutput.AllowedClothingHatTypes.Set(
            MainAccessories.SantaHatInstance
        );

        return setupOutput;
    }

    internal static SetupOutput Blank()
    {
        return Blank<IParameters>();
    }

    internal static SetupOutput Blank<T>() where T : IParameters
    {
        SetupOutput setupOutput = new SetupOutput(
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
            ColorPaletteMap.GetPaletteCount(SwapType.Clothing),
            new Vector2(),
            new Vector3()
        );

        return setupOutput;
    }


    public static Color WhiteColored = Color.white;

    public static Color WhiteColored3(Actor_Unit actor)
    {
        return Color.white;
    }
}
﻿#region

using System.Collections.Generic;

#endregion

namespace Races.Graphics.Implementations.Mercs
{
    internal static class Youko // TODO extend humans
    {
        // TODO Whisp used as placeholder
        // Recode Youko to extend (not inheritance) human behavior 
        internal static IRaceData Instance = RaceBuilder.CreateV2(Defaults.Blank, builder =>
        {
            builder.Setup(output =>
            {
                output.Names("Whisp", "Whisps");
                output.WallType(WallType.Fox);
                output.FlavorText(new FlavorText(
                    new Texts { "fluffy tailed", "squirming", "whimpering" },
                    new Texts { "cunning", "grinning", "sly" },
                    new Texts { "fox", "vulpine", "canid", {"vixen", Gender.Female}, {"tod", Gender.Male} }
                ));
                output.RaceTraits(new RaceTraits()
                {
                    BodySize = 10,
                    StomachSize = 15,
                    HasTail = true,
                    FavoredStat = Stat.Mind,
                    AllowedVoreTypes = new List<VoreType> { VoreType.Oral, VoreType.Unbirth, VoreType.CockVore, VoreType.BreastVore, VoreType.Anal, VoreType.TailVore },
                    RacialTraits = new List<Traits>()
                    {
                        Traits.Charmer,
                        Traits.Temptation,
                        Traits.Possession,
                        Traits.ForceFeeder,
                        Traits.ManaDrain,
                        Traits.CreateSpawn,
                    },
                    RaceStats = new RaceStats()
                    {
                        Strength = new RaceStats.StatRange(6, 14),
                        Dexterity = new RaceStats.StatRange(6, 14),
                        Endurance = new RaceStats.StatRange(6, 14),
                        Mind = new RaceStats.StatRange(14, 22),
                        Will = new RaceStats.StatRange(8, 16),
                        Agility = new RaceStats.StatRange(8, 18),
                        Voracity = new RaceStats.StatRange(12, 18),
                        Stomach = new RaceStats.StatRange(12, 18),
                    },
                    SpawnRace = Race.Whisp,
                    ConversionRace = Race.Foxes,
                    RaceDescription = "Foxes that were changed by spirit energy",
                });
                output.TownNames(new List<string>
                {
                    "Takama-ga-hara",
                    "Yamato-no-shi",
                    "Inari-no-shi",
                    "Zenko-no-machi",
                    "Yakan-no-machi",
                    "Ooji-no-machi",
                    "Onji-no-machi",
                });
                output.CanBeGender = new List<Gender> { Gender.None };
                output.SkinColors = ColorPaletteMap.GetPaletteCount(SwapType.MermenSkin);
            });


            builder.RenderSingle(SpriteType.Body, 1, (input, output) =>
            {
                output.Coloring(ColorPaletteMap.GetPalette(SwapType.MermenSkin, input.U.SkinColor));
                output.Sprite(input.Sprites.Whisp[0]);
            });

            builder.RunBefore(Defaults.Finalize);
            builder.RandomCustom(Defaults.RandomCustom);
        });

        /*
    readonly Sprite[] Sprites2 = State.GameManager.SpriteDictionary.HumansBodySprites2;
    readonly Sprite[] Sprites3 = State.GameManager.SpriteDictionary.HumansBodySprites3;
    readonly Sprite[] Tails = State.GameManager.SpriteDictionary.YoukoTails;

    static ColorSwapPalette FurryColor(Actor_Unit actor)
    {
        if (actor.Unit.Furry)
            return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, actor.Unit.AccessoryColor);
        return ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedSkin, actor.Unit.SkinColor);
    }

    public Youko() : base()
    {
        MiscRaceData.FurCapable = true;
        MiscRaceData.AccessoryColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.Fur);

        RaceSpriteSet[SpriteType.BodyAccessory] = new SpriteExtraInfo(7, null, (s ) =>
{
 ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedSkin, s.Unit.SkinColor), (input ) =>
{
 output.Sprite(Sprites3[8];
});
}); // Ears
        RaceSpriteSet[SpriteType.Head] = new SpriteExtraInfo(6, null, FurryColor, (input) =>
        {
            if (input.A.IsEating)
            {
                if (input.U.Furry)
                    return output.Sprite(Sprites2[47]);
                else if (input.U.HasBreasts)
                {
                    if (input.U.BodySize > 1)
                    {
                        return output.Sprite(Sprites2[4]);
                    }
                    else
                    {
                        return output.Sprite(Sprites2[1]);
                    }
                }
                else
                {
                    return output.Sprite(Sprites2[7 + (input.U.BodySize * 3)]);
                }
            }
            else if (input.A.IsAttacking)
            {
                if (input.U.Furry)
                    return output.Sprite(Sprites2[49]);
                else if (input.U.HasBreasts)
                {
                    if (input.U.BodySize > 1)
                    {
                        return output.Sprite(Sprites2[5]);
                    }
                    else
                    {
                        return output.Sprite(Sprites2[2]);
                    }
                }
                else
                {
                    return output.Sprite(Sprites2[8 + (input.U.BodySize * 3)]);
                }
            }
            else
            {
                if (input.U.Furry)
                    return output.Sprite(Sprites2[45]);
                else if (input.U.HasBreasts)
                {
                    if (input.U.BodySize > 1)
                    {
                        return output.Sprite(Sprites2[3]);
                    }
                    else
                    {
                        return output.Sprite(Sprites2[0]);
                    }
                }
                else
                {
                    return output.Sprite(Sprites2[6 + (input.U.BodySize * 3)]);
                }

            }
        });

        RaceSpriteSet[SpriteType.BodyAccessory] = new SpriteExtraInfo(7, null, (s ) =>
{
 ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.RedFur, s.Unit.AccessoryColor), (input ) =>
{
 output.Sprite(Sprites3[0];
});
}); //
        RaceSpriteSet[SpriteType.SecondaryAccessory] = new SpriteExtraInfo(1, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.Fur, s.Unit.AccessoryColor), (input) =>
        {
            int tailCount = GetNumTails(input.Actor);
            if (input.U.Predator && input.A.PredatorComponent.TailFullness > 0)
            {
                if(tailCount >= 7)
                    return output.Sprite(Tails[10]);
                return output.Sprite(Tails[9]);
            }
            return output.Sprite(Tails[tailCount]);
        }); // Tail;

        RandomCustom = (data) =>
        {
            Unit unit = data.Unit;
            Defaults.RandomCustom(data);

            if (unit.HasDick && unit.HasBreasts)
            {
                if (Config.HermsOnlyUseFemaleHair)
                    unit.HairStyle = State.Rand.Next(18);
                else
                    unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
            }
            else if (unit.HasDick && Config.FemaleHairForMales)
                unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
            else if (unit.HasDick == false && Config.MaleHairForFemales)
                unit.HairStyle = State.Rand.Next(data.MiscRaceData.HairStyles);
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
        };

        MiscRaceData.BeardStyles = 0;
    }

    private int GetNumTails(Actor_Unit actor)
    {
        int StatTotal = actor.Unit.GetStatTotal();
        if (StatTotal < 85)
            return 0;
        return Math.Min((int)(StatTotal - 85) / 15, 7) + 1;

    }

    public bool CheckVore(Actor_Unit actor, Actor_Unit target, PreyLocation location)
    {
        if(location == PreyLocation.tail)
        {
            int tailCount = GetNumTails(actor);
            if ((target != null) && (actor.PredatorComponent.TailFullness < 1))
                if ((float)target.Bulk() < (actor.PredatorComponent.TotalCapacity() / 2))
                    return (tailCount >= 4);
                else
                    return (tailCount >= 7);
            return (tailCount >= 4) && (actor.PredatorComponent.TailFullness < 1);
        }
        return true;
    }
    */
    }
}
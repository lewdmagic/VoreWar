using System;
using System.Collections.Generic;
using BidirectionalMap;
using UnityEngine;

public static class RaceFuncs
{

    public class MonsterData
    {
        internal readonly int BannerType;
        internal readonly  StrategyAIType StrategicAI;
        internal readonly  TacticalAIType TacticalAI;
        internal readonly  int Team;
        internal readonly  int MaxArmySize;
        internal readonly  int MaxGarrisonSize;

        public MonsterData(int bannerType, StrategyAIType strategicAI, TacticalAIType tacticalAI, int team, int maxArmySize, int maxGarrisonSize)
        {
            BannerType = bannerType;
            StrategicAI = strategicAI;
            TacticalAI = tacticalAI;
            Team = team;
            MaxArmySize = maxArmySize;
            MaxGarrisonSize = maxGarrisonSize;
        }
    }
    
    public static IReadOnlyDictionary<Race, int> TooltipValues = new Dictionary<Race, int>()
    {
        { Race.Vagrants, 23 },
        { Race.Serpents, 88 },
        { Race.Wyvern, 89 },
        { Race.Compy, 90 },
        { Race.FeralSharks, 92 },
        { Race.FeralWolves, 102 },
        { Race.Cake, 108 },
        { Race.Harvesters, 129 },
        { Race.Voilin, 144 },
        { Race.FeralBats, 145 },
        { Race.FeralFrogs, 153 },
        { Race.Dragon, 160 },
        { Race.Dragonfly, 161 },
        { Race.TwistedVines, 170 },
        { Race.Fairies, 171 },
        { Race.FeralAnts, 178 },
        { Race.Gryphons, 191 },
        { Race.RockSlugs, 194 },
        { Race.Salamanders, 198 },
        { Race.Mantis, 203 },
        { Race.EasternDragon, 204 },
        { Race.Catfish, 208 },
        { Race.Gazelle, 210 },
        { Race.Earthworms, 225 },
        { Race.FeralLizards, 230 },
        { Race.Monitors, 233 },
        { Race.Schiwardez, 234 },
        { Race.Terrorbird, 238 },
        { Race.Dratopyr, 247 },
        { Race.FeralLions, 248 },
        { Race.Goodra, 257 }
    };
    
    public static IReadOnlyDictionary<Race, MonsterData> SpawnerElligibleMonsterRaces = new Dictionary<Race, MonsterData>()
    {
        {Race.Vagrants, new MonsterData(9, StrategyAIType.Monster, TacticalAIType.Full, 996, 32, 0)},
        {Race.Serpents, new MonsterData(22, StrategyAIType.Monster, TacticalAIType.Full, 997, 32, 0)},
        {Race.Wyvern, new MonsterData(23, StrategyAIType.Monster, TacticalAIType.Full, 998, 32, 0)},
        {Race.Compy, new MonsterData(25, StrategyAIType.Monster, TacticalAIType.Full, 999, 32, 0)},
        {Race.FeralSharks, new MonsterData(26, StrategyAIType.Monster, TacticalAIType.Full, 1000, 32, 0)},
        {Race.FeralWolves, new MonsterData(27, StrategyAIType.Monster, TacticalAIType.Full, 1001, 32, 0)},
        {Race.Cake, new MonsterData(28, StrategyAIType.Monster, TacticalAIType.Full, 1002, 32, 0)},
        {Race.Goblins, new MonsterData(30, StrategyAIType.Goblin, TacticalAIType.Full, -200, 32, 0)},
        {Race.Harvesters, new MonsterData(31, StrategyAIType.Monster, TacticalAIType.Full, 1003, 32, 0)},
        {Race.Voilin, new MonsterData(32, StrategyAIType.Monster, TacticalAIType.Full, 1004, 32, 0)},
        {Race.FeralBats, new MonsterData(33, StrategyAIType.Monster, TacticalAIType.Full, 1005, 32, 0)},
        {Race.FeralFrogs, new MonsterData(34, StrategyAIType.Monster, TacticalAIType.Full, 1006, 32, 0)},
        {Race.Dragon, new MonsterData(35, StrategyAIType.Monster, TacticalAIType.Full, 1007, 32, 0)},
        {Race.Dragonfly, new MonsterData(36, StrategyAIType.Monster, TacticalAIType.Full, 1008, 32, 0)},
        {Race.TwistedVines, new MonsterData(41, StrategyAIType.Monster, TacticalAIType.Full, 1009, 32, 0)},
        {Race.Fairies, new MonsterData(42, StrategyAIType.Monster, TacticalAIType.Full, 1010, 32, 0)},
        {Race.FeralAnts, new MonsterData(43, StrategyAIType.Monster, TacticalAIType.Full, 1011, 32, 0)},
        {Race.Gryphons, new MonsterData(44, StrategyAIType.Monster, TacticalAIType.Full, 1012, 32, 0)},
        {Race.RockSlugs, new MonsterData(45, StrategyAIType.Monster, TacticalAIType.Full, 1013, 32, 0)},
        {Race.Salamanders, new MonsterData(46, StrategyAIType.Monster, TacticalAIType.Full, 1014, 32, 0)},
        {Race.Mantis, new MonsterData(47, StrategyAIType.Monster, TacticalAIType.Full, 1015, 32, 0)},
        {Race.EasternDragon, new MonsterData(48, StrategyAIType.Monster, TacticalAIType.Full, 1016, 32, 0)},
        {Race.Catfish, new MonsterData(49, StrategyAIType.Monster, TacticalAIType.Full, 1017, 32, 0)},
        {Race.Gazelle, new MonsterData(50, StrategyAIType.Monster, TacticalAIType.Full, 1018, 32, 0)},
        {Race.Earthworms, new MonsterData(51, StrategyAIType.Monster, TacticalAIType.Full, 1019, 32, 0)},
        {Race.FeralLizards, new MonsterData(52, StrategyAIType.Monster, TacticalAIType.Full, 1020, 32, 0)},
        {Race.Monitors, new MonsterData(53, StrategyAIType.Monster, TacticalAIType.Full, 1021, 32, 0)},
        {Race.Schiwardez, new MonsterData(54, StrategyAIType.Monster, TacticalAIType.Full, 1022, 32, 0)},
        {Race.Terrorbird, new MonsterData(55, StrategyAIType.Monster, TacticalAIType.Full, 1023, 32, 0)},
        {Race.Dratopyr, new MonsterData(56, StrategyAIType.Monster, TacticalAIType.Full, 1024, 32, 0)},
        {Race.FeralLions, new MonsterData(57, StrategyAIType.Monster, TacticalAIType.Full, 1337, 32, 0)},
        {Race.Goodra, new MonsterData(58, StrategyAIType.Monster, TacticalAIType.Full, 1025, 32, 0)}
    };
    

    public static IEnumerable<Race> RaceEnumerable()
    {
        //return EnumUtil.GetValues<Race>();
        return Race2.RaceIdList;
    }

    public static int MainRaceCount => MainRaceEnumerable().Count;
    
    public static IReadOnlyList<Race> MainRaceEnumerable()
    {
        List<Race> races = new List<Race>();
        foreach (Race race in Race2.RaceIdList)
        {
            if (IsPlayableRace(race))
            {
                races.Add(race);
            }
        }

        return races;
    }
    
    public static IReadOnlyList<Race> AllMonstersRangeRaceEnumerable()
    {
        List<Race> races = new List<Race>();
        foreach (Race race in Race2.RaceIdList)
        {
            if (race.HasTag(RaceTag.Monster))
            {
                races.Add(race);
            }
        }

        return races;
    }
    
    // public static IReadOnlyList<Race> AllMonstersRangeRaceEnumerable()
    // {
    //     //return EnumUtil.GetValues<Race>();
    //     //return Race2.RaceIdList;
    //
    //     List<Race> races = new List<Race>();
    //     for (int i = 100; i <= 139; i++)
    //     {
    //         races.Add(IntToRace(i));
    //     }
    //
    //     return races;
    // }
    
    public static IReadOnlyList<Race> MainRaceEnumerable2()
    {
        //return EnumUtil.GetValues<Race>();
        //return Race2.RaceIdList;

        List<Race> races = new List<Race>();
        foreach (Race race in RaceEnumerable())
        {
            if (!Equals(race, Race.TrueNone)) races.Add(race);
        }

        return races;
    }



    internal static bool TryParse(string word, out Race race)
    {
        if (Race2.StringMap.TryGetValue(word, out race))
        {
            return true;
        }
        else
        {
            race = null;
            return false;
        }
    }

    private static Dictionary<Race, Func<Sprite>> BannerSprites = new Dictionary<Race, Func<Sprite>>()
    {
        { Race.Cats, () => State.GameManager.StrategyMode.Banners[25 + 0] },
        { Race.Dogs, () => State.GameManager.StrategyMode.Banners[25 + 1] },
        { Race.Foxes, () => State.GameManager.StrategyMode.Banners[25 + 2] },
        { Race.Wolves, () => State.GameManager.StrategyMode.Banners[25 + 3] },
        { Race.Bunnies, () => State.GameManager.StrategyMode.Banners[25 + 4] },
        { Race.Lizards, () => State.GameManager.StrategyMode.Banners[25 + 5] },
        { Race.Slimes, () => State.GameManager.StrategyMode.Banners[25 + 6] },
        { Race.Scylla, () => State.GameManager.StrategyMode.Banners[25 + 7] },
        { Race.Harpies, () => State.GameManager.StrategyMode.Banners[25 + 8] },
        { Race.Imps, () => State.GameManager.StrategyMode.Banners[25 + 9] },
        { Race.Humans, () => State.GameManager.StrategyMode.Banners[25 + 10] },
        { Race.Crypters, () => State.GameManager.StrategyMode.Banners[25 + 11] },
        { Race.Lamia, () => State.GameManager.StrategyMode.Banners[25 + 12] },
        { Race.Kangaroos, () => State.GameManager.StrategyMode.Banners[25 + 13] },
        { Race.Taurus, () => State.GameManager.StrategyMode.Banners[25 + 14] },
        { Race.Crux, () => State.GameManager.StrategyMode.Banners[25 + 15] },
        { Race.Equines, () => State.GameManager.StrategyMode.Banners[25 + 16] },
        { Race.Sergal, () => State.GameManager.StrategyMode.Banners[25 + 17] },
        { Race.Bees, () => State.GameManager.StrategyMode.Banners[25 + 18] },
        { Race.Driders, () => State.GameManager.StrategyMode.Banners[25 + 19] },
        { Race.Alraune, () => State.GameManager.StrategyMode.Banners[25 + 20] },
        { Race.DemiBats, () => State.GameManager.StrategyMode.Banners[25 + 21] },
        { Race.Panthers, () => State.GameManager.StrategyMode.Banners[25 + 22] },
        { Race.Merfolk, () => State.GameManager.StrategyMode.Banners[25 + 23] },
        { Race.Avians, () => State.GameManager.StrategyMode.Banners[25 + 24] },
        { Race.Ants, () => State.GameManager.StrategyMode.Banners[25 + 25] },
        { Race.Demifrogs, () => State.GameManager.StrategyMode.Banners[25 + 26] },
        { Race.Demisharks, () => State.GameManager.StrategyMode.Banners[25 + 27] },
        { Race.Deer, () => State.GameManager.StrategyMode.Banners[25 + 28] },
        { Race.Aabayx, () => State.GameManager.StrategyMode.Banners[25 + 29] },
    };

    private const int TeamNumberOffset = 1100;
    
    internal static Sprite BannerSprite(Race race)
    {
        if (BannerSprites.TryGetValue(race, out Func<Sprite> sprite))
        {
            return sprite();
        }
        else
        {
            return State.GameManager.StrategyMode.Banners[24];
        }
    }
    
    internal static Race IntToRace(int value)
    {
        throw new NotImplementedException();
    }
    
    internal static int RaceToInt(Race race)
    {
        throw new NotImplementedException();
    }    
    
    // internal static Race IntToRace(int value)
    // {
    //     if (value == -1) return Race.none;
    //     if (Race2.IntMap.TryGetValue(value, out Race race))
    //     {
    //         return race;
    //     }
    //     else
    //     {
    //         Debug.Log(new Exception("Null race for id: " + value));
    //         return null;
    //     }
    // }
    //
    // internal static int RaceToInt(Race race)
    // {
    //     return race.IntId;
    // }
    
    internal static int RaceToTempNumber(Race race)
    {
        return race.RaceNumber;
    }
    
    internal static int RaceToIntForTeam(Race race)
    {
        return TeamNumberOffset + race.RaceNumber;
    }

    internal static bool isHumanoid(Race race)
    {
        return race.HasTag(RaceTag.Humanoid);
    }

    // RaceFuncs.RaceToIntForCompare(race) < RaceFuncs.RaceToIntForCompare(Race.Selicia) 
    internal static bool isNotUniqueMerc(Race race)
    {
        return !race.HasTag(RaceTag.UniqueMerc);
    }

    // RaceFuncs.RaceToIntForCompare(race) >= RaceFuncs.RaceToIntForCompare(Race.Vagrants)
    internal static bool isMosnterOrUniqueMerc(Race race)
    {
        return race.HasTag(RaceTag.UniqueMerc) || race.HasTag(RaceTag.Monster);
    }

    // RaceFuncs.RaceToIntForCompare(unit.Race) >= RaceFuncs.RaceToIntForCompare(Race.Selicia)
    internal static bool isUniqueMerc(Race race)
    {
        return race.HasTag(RaceTag.UniqueMerc);
    }
    
    internal static bool isUniqueMerc(Side side)
    {
        return side.HasTag(RaceTag.UniqueMerc);
    }
    
    // RaceFuncs.RaceToIntForCompare(race) < RaceFuncs.RaceToIntForCompare(Race.Vagrants)
    internal static bool isMainRaceOrMerc(Race race)
    {
        return race.HasTag(RaceTag.MainRace) || race.HasTag(RaceTag.Merc);
    }
    
    
    // RaceFuncs.RaceToIntForCompare(s) < RaceFuncs.RaceToIntForCompare(Race.Succubi)
    internal static bool IsMainRace(Race race)
    {
        return race.HasTag(RaceTag.MainRace);
    }    
    
    internal static bool IsMainRace(Side side)
    {
        return side.HasTag(RaceTag.MainRace);
    }
    
    internal static bool isNotNone(Race race)
    {
        return !Equals(race, Race.TrueNone);
    }
    
    internal static bool isNotNone(Side side)
    {
        return !Equals(side, Race.TrueNoneSide);
    }
    
    internal static bool isNone(Race race)
    {
        return Equals(race, Race.TrueNone);
    }
    
    internal static bool isNone(Side side)
    {
        return Equals(side, Race.TrueNoneSide);
    }
    
    // RaceFuncs.RaceToIntForCompare(race) >= RaceFuncs.RaceToIntForCompare(Race.Vagrants) && RaceFuncs.RaceToIntForCompare(race) < RaceFuncs.RaceToIntForCompare(Race.Selicia)
    internal static bool isMonster(Race race)
    {
        return race.HasTag(RaceTag.Monster);
    }
    internal static bool isMonster(Side side)
    {
        return side.HasTag(RaceTag.Monster);
    }
    
    internal static bool isMainRaceOrMercOrMonster(Side side)
    {
        return side.HasTag(RaceTag.MainRace) || side.HasTag(RaceTag.Merc) || side.HasTag(RaceTag.Monster);
    }
    
    // RaceFuncs.RaceToIntForCompare(s) >= RaceFuncs.RaceToIntForCompare(Race.Succubi) && RaceFuncs.RaceToIntForCompare(s) < RaceFuncs.RaceToIntForCompare(Race.Vagrants)
    internal static bool IsMerc(Race race)
    {
        return race.HasTag(RaceTag.Merc);
    }    
    internal static bool IsMerc(Side side)
    {
        return side.HasTag(RaceTag.Merc);
    }
    
    // TODO this is probably a bug, but I copied it as is. 
    // RaceFuncs.RaceToIntForCompare(Unit.Race) <= RaceFuncs.RaceToIntForCompare(Race.Goblins)
    internal static bool isMainRaceOrTigerOrSuccubiOrGoblin(Race race)
    {
        return race.HasTag(RaceTag.MainRace) || Equals(race, Race.Tigers) || Equals(race, Race.Succubi) || Equals(race, Race.Goblins);
    }


    internal static bool IsPlayableRace(Race race)
    {
        // Can't make monster races playable yet because of many special Monster Empire conditions
        return race.HasTag(RaceTag.MainRace) || race.HasTag(RaceTag.Merc);// || race.HasTag(RaceTag.Monster);
    }
    
    
    internal static bool IsRebelOrBandit(Side side)
    {
        return Equals(side, Race.RebelSide) || Equals(side, Race.BanditSide);
    }

    // Formerly IsOver500
    internal static bool IsRebelOrBandit2(Side side)
    {
        return IsRebelOrBandit(side);
    }
    
    // Formerly IsOver600
    internal static bool IsRebelOrBandit3(Side side)
    {
        return IsRebelOrBandit(side);
    }
    
    // Formerly IsOver300
    internal static bool IsRebelOrBandit4(Side side)
    {
        return IsRebelOrBandit(side);
    }
    
    // Formerly MoreOrEqual700
    internal static bool IsRebelOrBandit5(Side side)
    {
        return IsRebelOrBandit(side);
    }
    
    // Formerly MoreOrEqual50 
    internal static bool NotMainRace(Side side)
    {
        return !IsMainRace(side);
    }

    // Formerly LessThan30
    internal static bool IsMainRace3(Side side)
    {
        return IsMainRace(side);
    }
    
    // Formerly LessThan700
    internal static bool NotRebelOrBandit(Side side)
    {
        return !IsRebelOrBandit(side);
    }
    
    // Formerly LessThan100
    internal static bool IsMainRaceOrMerc(Side side)
    {
        return IsMainRace(side) || IsMerc(side);
    }
    
    // Formerly LessThan50
    internal static bool IsMainRace2(Side side)
    {
        return IsMainRace(side);
    }
    
    // TODO this is probably originally a bug, keeping same behavior for now
    // Formerly LessOrEqual700
    internal static bool IsNotBandit(Side side)
    {
        return !Equals(side, Race.BanditSide);
    }
    
    // Formerly MoreOrEqual100
    internal static bool IsMonstersOrUniqueMercsOrRebelsOrBandits(Side side)
    {
        return isMonster(side) || isUniqueMerc(side) || IsRebelOrBandit(side);
    }
    
    // Formerly >= RaceFuncs.RaceToInt(Race.Selicia)
    internal static bool IsUniqueMercsOrRebelsOrBandits(Side side)
    {
        return isUniqueMerc(side) || IsRebelOrBandit(side);
    }

    /*

    internal static bool isHumanoid(Race race)
    {
        return race.HasTag(RaceTag.Humanoid);
    }

    // RaceFuncs.RaceToIntForCompare(race) < RaceFuncs.RaceToIntForCompare(Race.Selicia) 
    internal static bool isNotUniqueMerc(Race race)
    {
        return !race.HasTag(RaceTag.UniqueMerc);
    }

    // RaceFuncs.RaceToIntForCompare(race) >= RaceFuncs.RaceToIntForCompare(Race.Vagrants)
    internal static bool isMosnterOrUniqueMerc(Race race)
    {
        return race.HasTag(RaceTag.UniqueMerc) || race.HasTag(RaceTag.Monster);
    }

    // RaceFuncs.RaceToIntForCompare(unit.Race) >= RaceFuncs.RaceToIntForCompare(Race.Selicia)
    internal static bool isUniqueMerc(Race race)
    {
        return race.HasTag(RaceTag.UniqueMerc);
    }
    
    internal static bool isUniqueMerc(Side side)
    {
        return side.ToRace().HasTag(RaceTag.UniqueMerc);
    }
    
    // RaceFuncs.RaceToIntForCompare(race) < RaceFuncs.RaceToIntForCompare(Race.Vagrants)
    internal static bool isMainRaceOrMerc(Race race)
    {
        return race.HasTag(RaceTag.MainRace) || race.HasTag(RaceTag.Merc);
    }
    
    
    // RaceFuncs.RaceToIntForCompare(s) < RaceFuncs.RaceToIntForCompare(Race.Succubi)
    internal static bool IsMainRace(Race race)
    {
        return race.HasTag(RaceTag.MainRace);
    }    
    
    internal static bool IsMainRace(Side side)
    {
        return side.ToRace().HasTag(RaceTag.MainRace);
    }
    
    internal static bool isNotNone(Race race)
    {
        return !Equals(race, Race.none);
    }
    
    internal static bool isNone(Race race)
    {
        return Equals(race, Race.none);
    }
    
    // RaceFuncs.RaceToIntForCompare(race) >= RaceFuncs.RaceToIntForCompare(Race.Vagrants) && RaceFuncs.RaceToIntForCompare(race) < RaceFuncs.RaceToIntForCompare(Race.Selicia)
    internal static bool isMonster(Race race)
    {
        return race.HasTag(RaceTag.Monster);
    }
    internal static bool isMonster(Side side)
    {
        return side.ToRace().HasTag(RaceTag.Monster);
    }
    
    // RaceFuncs.RaceToIntForCompare(s) >= RaceFuncs.RaceToIntForCompare(Race.Succubi) && RaceFuncs.RaceToIntForCompare(s) < RaceFuncs.RaceToIntForCompare(Race.Vagrants)
    internal static bool IsMerc(Race race)
    {
        return race.HasTag(RaceTag.Merc);
    }    
    internal static bool IsMerc(Side side)
    {
        return side.ToRace().HasTag(RaceTag.Merc);
    }
    
    // TODO this is probably a bug, but I copied it as is. 
    // RaceFuncs.RaceToIntForCompare(Unit.Race) <= RaceFuncs.RaceToIntForCompare(Race.Goblins)
    internal static bool isMainRaceOrTigerOrSuccubiOrGoblin(Race race)
    {
        return race.HasTag(RaceTag.MainRace) || Equals(race, Race.Tigers) || Equals(race, Race.Succubi) || Equals(race, Race.Goblins);
    }


    internal static bool IsPlayableRace(Race race)
    {
        // Can't make monster races playable yet because of many special Monster Empire conditions
        return race.HasTag(RaceTag.MainRace) || race.HasTag(RaceTag.Merc);// || race.HasTag(RaceTag.Monster);
    }
    
    
    internal static bool IsRebelOrBandit(Side side)
    {
        return Equals(side, Race.RebelSide) || Equals(side, Race.BanditSide);
    }

    // Formerly IsOver500
    internal static bool IsRebelOrBandit2(Side side)
    {
        return IsRebelOrBandit(side);
    }
    
    // Formerly IsOver600
    internal static bool IsRebelOrBandit3(Side side)
    {
        return IsRebelOrBandit(side);
    }
    
    // Formerly IsOver300
    internal static bool IsRebelOrBandit4(Side side)
    {
        return IsRebelOrBandit(side);
    }
    
    // Formerly MoreOrEqual700
    internal static bool IsRebelOrBandit5(Side side)
    {
        return IsRebelOrBandit(side);
    }
    
    // Formerly MoreOrEqual50 
    internal static bool NotMainRace(Side side)
    {
        return !IsMainRace(side);
    }

    // Formerly LessThan30
    internal static bool IsMainRace3(Side side)
    {
        return IsMainRace(side);
    }
    
    // Formerly LessThan700
    internal static bool NotRebelOrBandit(Side side)
    {
        return !IsRebelOrBandit(side);
    }
    
    // Formerly LessThan100
    internal static bool IsMainRaceOrMerc(Side side)
    {
        return IsMainRace(side) || IsMerc(side);
    }
    
    // Formerly LessThan50
    internal static bool IsMainRace2(Side side)
    {
        return IsMainRace(side);
    }
    
    // TODO this is probably originally a bug, keeping same behavior for now
    // Formerly LessOrEqual700
    internal static bool IsNotBandit(Side side)
    {
        return !Equals(side, Race.BanditSide);
    }
    
    // Formerly MoreOrEqual100
    internal static bool IsMonstersOrUniqueMercsOrRebelsOrBandits(Side side)
    {
        return isMonster(side) || isUniqueMerc(side) || IsRebelOrBandit(side);
    }

     */
    
    
}

// public static class RaceFuncs
// {
//
//     public static IEnumerable<Race> RaceEnumerable()
//     {
//         return EnumUtil.GetValues<Race>();
//
//     }
//
//     internal static Race IntToRace(int value)
//     {
//         return (Race)value;
//     }
//
//     internal static bool TryParse(string word, out Race race)
//     {
//         return Enum.TryParse<Race>(word, out race);
//     }
//
//     internal static int RaceToInt(Race race)
//     {
//         return (int)race;
//     }
//     
//     internal static int RaceToSwitch(Race race)
//     {
//         return (int)race;
//     }
//
//     internal static int RaceToIntForCompare(Race race)
//     {
//         return (int)race;
//     }
//     
// }

public enum RaceTag
{
    MainRace,
    Merc,
    Monster,
    UniqueMerc,
    
    
    Humanoid,
}

public class Race : IComparable<Race>
{
    private static int NextRaceNumber = 0;
    public readonly string Id;
    
    private readonly HashSet<RaceTag> _tags = new HashSet<RaceTag>();


    /// <summary>
    /// THIS IS NOT AN ID. 
    /// This is just an incremental number assigned to each Race.
    /// IT IS ABSOLUTELY NOT GUARANTEED TO STAY THE SAME ACROSS VERSIONS
    /// OR EVEN GAME LAUNCHES. NOT TO BE USED AS AN ID. 
    /// </summary>
    public readonly int RaceNumber;
    
    public Side ToSide()
    {
        if (RaceSideMap.Forward.ContainsKey(this))
        {
            return RaceSideMap.Forward[this];
        }
        else
        {
            throw new Exception();
        }
    }
    
    public override bool Equals(object obj)
    {
        Race otherRace = obj as Race;
        if (otherRace == null)
        {
            return false;
        }
        
        return Id.Equals(otherRace.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return Id;
    }

    public int CompareTo(Race other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (ReferenceEquals(null, other))
        {
            return 1;
        }

        return String.Compare(Id, other.Id, StringComparison.Ordinal);
    }


    private Race(string id, RaceTag[] tags = null)
    {
        if (tags != null)
        {
            foreach (RaceTag tag in tags)
            {
                _tags.Add(tag);
            }
        }
        
        Id = id;
        Race2.RaceIdList.Add(this);
        Race2.StringMap[id] = this;
        RaceSideMap.Add(this, new Side(id, tags));
        RaceNumber = NextRaceNumber++;
    }

    public bool HasTag(RaceTag tag)
    {
        return _tags.Contains(tag);
    }

    public static BiMap<Race, Side> RaceSideMap = new BiMap<Race, Side>();
        
    public static Race TrueNone = null;
    public static Side TrueNoneSide = null;
        
    public static Race Cats = new Race("cat",             new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Dogs = new Race("dog",             new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Foxes = new Race("fox",            new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Wolves = new Race("wolf",          new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Bunnies = new Race("bunny",        new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Lizards = new Race("lizard",       new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Slimes = new Race("slime",         new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Scylla = new Race("scylla",        new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Harpies = new Race("harpy",        new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Imps = new Race("imp",             new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Humans = new Race("human",         new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Crypters = new Race("crypter",     new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Lamia = new Race("lamia",          new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Kangaroos = new Race("kangaroo",   new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Taurus = new Race("taurus",        new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Crux = new Race("crux",            new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Equines = new Race("equines",      new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Sergal = new Race("sergal",        new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Bees = new Race("bees",            new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Driders = new Race("driders",      new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Alraune = new Race("alraune",      new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race DemiBats = new Race("demiBats",    new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Panthers = new Race("panthers",    new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Merfolk = new Race("merfolk",      new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Avians = new Race("avians",        new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Ants = new Race("ants",            new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Demifrogs = new Race("frogs",      new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Demisharks = new Race("sharks",    new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Deer = new Race("deer",            new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Aabayx = new Race("aabayx",        new[]{ RaceTag.Humanoid, RaceTag.MainRace });

    public static Race Succubi = new Race("succubus",      new[]{ RaceTag.Humanoid, RaceTag.Merc });
    public static Race Tigers = new Race("tiger",          new[]{ RaceTag.Humanoid, RaceTag.Merc });
    public static Race Goblins = new Race("goblin",        new[]{ RaceTag.Humanoid, RaceTag.Merc });
    public static Race Alligators = new Race("alligator",  new[]{ RaceTag.Humanoid, RaceTag.Merc });
    public static Race Puca = new Race("puca",             new[]{ RaceTag.Humanoid, RaceTag.Merc });
    public static Race Kobolds = new Race("Kobold",        new[]{ RaceTag.Humanoid, RaceTag.Merc });
    public static Race DewSprites = new Race("dewSprite",  new[]{ RaceTag.Humanoid, RaceTag.Merc });
    public static Race Hippos = new Race("hippos",         new[]{ RaceTag.Humanoid, RaceTag.Merc });
    public static Race Vipers = new Race("vipers",         new[]{ RaceTag.Humanoid, RaceTag.Merc });
    public static Race Komodos = new Race("komodos",       new[]{ RaceTag.Humanoid, RaceTag.Merc });
    public static Race Cockatrice = new Race("cockatrice", new[]{ RaceTag.Humanoid, RaceTag.Merc });
    public static Race Vargul = new Race("vargul",         new[]{ RaceTag.Humanoid, RaceTag.Merc });
    public static Race Youko = new Race("youko",           new[]{ RaceTag.Humanoid, RaceTag.Merc });

    public static Race Vagrants = new Race("vagrants",           new[]{ RaceTag.Monster });
    public static Race Serpents = new Race("serpents",           new[]{ RaceTag.Monster });
    public static Race Wyvern = new Race("wyvern",               new[]{ RaceTag.Monster });
    public static Race YoungWyvern = new Race("youngWyvern",     new[]{ RaceTag.Monster });
    public static Race Compy = new Race("compy",                 new[]{ RaceTag.Monster });
    public static Race FeralSharks = new Race("feralSharks",     new[]{ RaceTag.Monster });
    public static Race FeralWolves = new Race("feralWolves",     new[]{ RaceTag.Monster });
    public static Race DarkSwallower = new Race("darkSwallower", new[]{ RaceTag.Monster });
    public static Race Cake = new Race("cake",                   new[]{ RaceTag.Monster });
    public static Race Harvesters = new Race("harvesters",       new[]{ RaceTag.Monster });
    public static Race Collectors = new Race("collectors",       new[]{ RaceTag.Monster });
    public static Race Voilin = new Race("voilin",               new[]{ RaceTag.Monster });
    public static Race FeralBats = new Race("feralBats",         new[]{ RaceTag.Monster });
    public static Race FeralFrogs = new Race("feralFrogs",       new[]{ RaceTag.Monster });
    public static Race Dragon = new Race("dragon",               new[]{ RaceTag.Monster });
    public static Race Dragonfly = new Race("dragonfly",         new[]{ RaceTag.Monster });
    public static Race TwistedVines = new Race("twistedVines",   new[]{ RaceTag.Monster });
    public static Race Fairies = new Race("fairies",             new[]{ RaceTag.Monster });
    public static Race FeralAnts = new Race("feralAnts",         new[]{ RaceTag.Monster });
    public static Race Gryphons = new Race("gryphons",           new[]{ RaceTag.Monster });
    public static Race SpitterSlugs = new Race("spitterSlugs",   new[]{ RaceTag.Monster });
    public static Race SpringSlugs = new Race("springSlugs",     new[]{ RaceTag.Monster });
    public static Race RockSlugs = new Race("rockSlugs",         new[]{ RaceTag.Monster });
    public static Race CoralSlugs = new Race("coralSlugs",       new[]{ RaceTag.Monster });
    public static Race Salamanders = new Race("salamanders",     new[]{ RaceTag.Monster });
    public static Race Mantis = new Race("mantis",               new[]{ RaceTag.Monster });
    public static Race EasternDragon = new Race("easternDragon", new[]{ RaceTag.Monster });
    public static Race Catfish = new Race("catfish",             new[]{ RaceTag.Monster });
    public static Race Raptor = new Race("raptor",               new[]{ RaceTag.Monster });
    public static Race WarriorAnts = new Race("warriorAnts",     new[]{ RaceTag.Monster });
    public static Race Gazelle = new Race("gazelle",             new[]{ RaceTag.Monster });
    public static Race Earthworms = new Race("earthworms",       new[]{ RaceTag.Monster });
    public static Race FeralLizards = new Race("feralLizards",   new[]{ RaceTag.Monster });
    public static Race Monitors = new Race("monitors",           new[]{ RaceTag.Monster });
    public static Race Schiwardez = new Race("schiwardez",       new[]{ RaceTag.Monster });
    public static Race Terrorbird = new Race("terrorbird",       new[]{ RaceTag.Monster });
    public static Race Dratopyr = new Race("dratopyr",           new[]{ RaceTag.Monster });
    public static Race FeralLions = new Race("feralLions",       new[]{ RaceTag.Monster });
    public static Race Goodra = new Race("goodra",               new[]{ RaceTag.Monster });
    public static Race Whisp = new Race("whisp",                 new[]{ RaceTag.Monster });

    public static Race Selicia = new Race("selicia",         new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Vision = new Race("vision",           new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Ki = new Race("ki",                   new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Scorch = new Race("scorch",           new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Asura = new Race("asura",             new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race DRACO = new Race("draco",             new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Zoey = new Race("zoey",               new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Abakhanskya = new Race("abakhanskya", new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Zera = new Race("zera",               new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Auri = new Race("auri",               new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Erin = new Race("erin",               new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Salix = new Race("salix",             new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });

    public static Side RebelSide = new Side("Rebels", null);
    public static Side BanditSide = new Side("Bandits", null);
}

public static class Race2
{
    internal static List<Race> RaceIdList = new List<Race>();
    internal static Dictionary<string, Race> StringMap = new Dictionary<string, Race>();
    internal static Dictionary<int, Race> IntMap = new Dictionary<int, Race>();

}

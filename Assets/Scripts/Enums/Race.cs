using System;
using System.Collections.Generic;
using BidirectionalMap;
using OdinSerializer;
using UnityEngine;

public static class RaceFuncs
{
    public class MonsterData
    {
        internal readonly int BannerType;
        internal readonly StrategyAIType StrategicAI;
        internal readonly TacticalAIType TacticalAI;
        internal readonly int Team;
        internal readonly int MaxArmySize;
        internal readonly int MaxGarrisonSize;

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
        { Race.Vagrant, 23 },
        { Race.Serpent, 88 },
        { Race.Wyvern, 89 },
        { Race.Compy, 90 },
        { Race.FeralShark, 92 },
        { Race.FeralWolve, 102 },
        { Race.Cake, 108 },
        { Race.Harvester, 129 },
        { Race.Voilin, 144 },
        { Race.FeralBat, 145 },
        { Race.FeralFrog, 153 },
        { Race.Dragon, 160 },
        { Race.Dragonfly, 161 },
        { Race.TwistedVine, 170 },
        { Race.Fairy, 171 },
        { Race.FeralAnt, 178 },
        { Race.Gryphon, 191 },
        { Race.RockSlug, 194 },
        { Race.Salamander, 198 },
        { Race.Manti, 203 },
        { Race.EasternDragon, 204 },
        { Race.Catfish, 208 },
        { Race.Gazelle, 210 },
        { Race.Earthworms, 225 },
        { Race.FeralLizard, 230 },
        { Race.Monitor, 233 },
        { Race.Schiwardez, 234 },
        { Race.Terrorbird, 238 },
        { Race.Dratopyr, 247 },
        { Race.FeralLion, 248 },
        { Race.Goodra, 257 }
    };

    public static IReadOnlyDictionary<Race, MonsterData> SpawnerElligibleMonsterRaces = new Dictionary<Race, MonsterData>()
    {
        { Race.Vagrant, new MonsterData(9, StrategyAIType.Monster, TacticalAIType.Full, 996, 32, 0) },
        { Race.Serpent, new MonsterData(22, StrategyAIType.Monster, TacticalAIType.Full, 997, 32, 0) },
        { Race.Wyvern, new MonsterData(23, StrategyAIType.Monster, TacticalAIType.Full, 998, 32, 0) },
        { Race.Compy, new MonsterData(25, StrategyAIType.Monster, TacticalAIType.Full, 999, 32, 0) },
        { Race.FeralShark, new MonsterData(26, StrategyAIType.Monster, TacticalAIType.Full, 1000, 32, 0) },
        { Race.FeralWolve, new MonsterData(27, StrategyAIType.Monster, TacticalAIType.Full, 1001, 32, 0) },
        { Race.Cake, new MonsterData(28, StrategyAIType.Monster, TacticalAIType.Full, 1002, 32, 0) },
        { Race.Goblin, new MonsterData(30, StrategyAIType.Goblin, TacticalAIType.Full, -200, 32, 0) },
        { Race.Harvester, new MonsterData(31, StrategyAIType.Monster, TacticalAIType.Full, 1003, 32, 0) },
        { Race.Voilin, new MonsterData(32, StrategyAIType.Monster, TacticalAIType.Full, 1004, 32, 0) },
        { Race.FeralBat, new MonsterData(33, StrategyAIType.Monster, TacticalAIType.Full, 1005, 32, 0) },
        { Race.FeralFrog, new MonsterData(34, StrategyAIType.Monster, TacticalAIType.Full, 1006, 32, 0) },
        { Race.Dragon, new MonsterData(35, StrategyAIType.Monster, TacticalAIType.Full, 1007, 32, 0) },
        { Race.Dragonfly, new MonsterData(36, StrategyAIType.Monster, TacticalAIType.Full, 1008, 32, 0) },
        { Race.TwistedVine, new MonsterData(41, StrategyAIType.Monster, TacticalAIType.Full, 1009, 32, 0) },
        { Race.Fairy, new MonsterData(42, StrategyAIType.Monster, TacticalAIType.Full, 1010, 32, 0) },
        { Race.FeralAnt, new MonsterData(43, StrategyAIType.Monster, TacticalAIType.Full, 1011, 32, 0) },
        { Race.Gryphon, new MonsterData(44, StrategyAIType.Monster, TacticalAIType.Full, 1012, 32, 0) },
        { Race.RockSlug, new MonsterData(45, StrategyAIType.Monster, TacticalAIType.Full, 1013, 32, 0) },
        { Race.Salamander, new MonsterData(46, StrategyAIType.Monster, TacticalAIType.Full, 1014, 32, 0) },
        { Race.Manti, new MonsterData(47, StrategyAIType.Monster, TacticalAIType.Full, 1015, 32, 0) },
        { Race.EasternDragon, new MonsterData(48, StrategyAIType.Monster, TacticalAIType.Full, 1016, 32, 0) },
        { Race.Catfish, new MonsterData(49, StrategyAIType.Monster, TacticalAIType.Full, 1017, 32, 0) },
        { Race.Gazelle, new MonsterData(50, StrategyAIType.Monster, TacticalAIType.Full, 1018, 32, 0) },
        { Race.Earthworms, new MonsterData(51, StrategyAIType.Monster, TacticalAIType.Full, 1019, 32, 0) },
        { Race.FeralLizard, new MonsterData(52, StrategyAIType.Monster, TacticalAIType.Full, 1020, 32, 0) },
        { Race.Monitor, new MonsterData(53, StrategyAIType.Monster, TacticalAIType.Full, 1021, 32, 0) },
        { Race.Schiwardez, new MonsterData(54, StrategyAIType.Monster, TacticalAIType.Full, 1022, 32, 0) },
        { Race.Terrorbird, new MonsterData(55, StrategyAIType.Monster, TacticalAIType.Full, 1023, 32, 0) },
        { Race.Dratopyr, new MonsterData(56, StrategyAIType.Monster, TacticalAIType.Full, 1024, 32, 0) },
        { Race.FeralLion, new MonsterData(57, StrategyAIType.Monster, TacticalAIType.Full, 1337, 32, 0) },
        { Race.Goodra, new MonsterData(58, StrategyAIType.Monster, TacticalAIType.Full, 1025, 32, 0) }
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
        { Race.Cat, () => State.GameManager.StrategyMode.Banners[25 + 0] },
        { Race.Dog, () => State.GameManager.StrategyMode.Banners[25 + 1] },
        { Race.Fox, () => State.GameManager.StrategyMode.Banners[25 + 2] },
        { Race.Wolf, () => State.GameManager.StrategyMode.Banners[25 + 3] },
        { Race.Bunny, () => State.GameManager.StrategyMode.Banners[25 + 4] },
        { Race.Lizard, () => State.GameManager.StrategyMode.Banners[25 + 5] },
        { Race.Slime, () => State.GameManager.StrategyMode.Banners[25 + 6] },
        { Race.Scylla, () => State.GameManager.StrategyMode.Banners[25 + 7] },
        { Race.Harpy, () => State.GameManager.StrategyMode.Banners[25 + 8] },
        { Race.Imp, () => State.GameManager.StrategyMode.Banners[25 + 9] },
        { Race.Human, () => State.GameManager.StrategyMode.Banners[25 + 10] },
        { Race.Crypter, () => State.GameManager.StrategyMode.Banners[25 + 11] },
        { Race.Lamia, () => State.GameManager.StrategyMode.Banners[25 + 12] },
        { Race.Kangaroo, () => State.GameManager.StrategyMode.Banners[25 + 13] },
        { Race.Taurus, () => State.GameManager.StrategyMode.Banners[25 + 14] },
        { Race.Crux, () => State.GameManager.StrategyMode.Banners[25 + 15] },
        // TODO { Race.Equine, () => State.GameManager.StrategyMode.Banners[25 + 16] },
        { Race.Sergal, () => State.GameManager.StrategyMode.Banners[25 + 17] },
        { Race.Bee, () => State.GameManager.StrategyMode.Banners[25 + 18] },
        { Race.Drider, () => State.GameManager.StrategyMode.Banners[25 + 19] },
        { Race.Alraune, () => State.GameManager.StrategyMode.Banners[25 + 20] },
        { Race.Bat, () => State.GameManager.StrategyMode.Banners[25 + 21] },
        { Race.Panther, () => State.GameManager.StrategyMode.Banners[25 + 22] },
        { Race.Merfolk, () => State.GameManager.StrategyMode.Banners[25 + 23] },
        { Race.Avian, () => State.GameManager.StrategyMode.Banners[25 + 24] },
        { Race.Ant, () => State.GameManager.StrategyMode.Banners[25 + 25] },
        { Race.Frog, () => State.GameManager.StrategyMode.Banners[25 + 26] },
        { Race.Shark, () => State.GameManager.StrategyMode.Banners[25 + 27] },
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

    private static int _unknownVillageColoredSpriteId = 8;
    private static int _unknownVillageSpriteId = 2;
    private static int _unknownVillageWithWallSpriteId = 3;


    internal static Sprite ColoredVillageIconForRace(Race race)
    {
        if (_villageIcons.TryGetValue(race, out var ids))
        {
            return State.GameManager.StrategyMode.VillageSprites[ids.Item1];
        }
        else
        {
            return State.GameManager.StrategyMode.VillageSprites[_unknownVillageColoredSpriteId];
        }
    }

    internal static Sprite VillageIconForRace(Race race)
    {
        if (_villageIcons.TryGetValue(race, out var ids))
        {
            return State.GameManager.StrategyMode.VillageSprites[ids.Item2];
        }
        else
        {
            return State.GameManager.StrategyMode.VillageSprites[_unknownVillageSpriteId];
        }
    }

    internal static Sprite VillageIconWithWall(Race race)
    {
        if (_villageIcons.TryGetValue(race, out var ids))
        {
            return State.GameManager.StrategyMode.VillageSprites[ids.Item3];
        }
        else
        {
            return State.GameManager.StrategyMode.VillageSprites[_unknownVillageWithWallSpriteId];
        }
    }


    private static int _offset1 = 8;
    private static int _offset2 = 9;
    private static int _offset3 = 10;
    private static int _gap = 3;

    private static Dictionary<Race, Tuple<int, int, int>> _villageIcons = new Dictionary<Race, Tuple<int, int, int>>()
    {
        { Race.Cat, new Tuple<int, int, int>(_offset1 + _gap * 0, _offset2 + _gap * 0, _offset3 + _gap * 0) },
        { Race.Dog, new Tuple<int, int, int>(_offset1 + _gap * 1, _offset2 + _gap * 1, _offset3 + _gap * 1) },
        { Race.Fox, new Tuple<int, int, int>(_offset1 + _gap * 2, _offset2 + _gap * 2, _offset3 + _gap * 2) },
        { Race.Wolf, new Tuple<int, int, int>(_offset1 + _gap * 3, _offset2 + _gap * 3, _offset3 + _gap * 3) },
        { Race.Bunny, new Tuple<int, int, int>(_offset1 + _gap * 4, _offset2 + _gap * 4, _offset3 + _gap * 4) },
        { Race.Lizard, new Tuple<int, int, int>(_offset1 + _gap * 5, _offset2 + _gap * 5, _offset3 + _gap * 5) },
        { Race.Slime, new Tuple<int, int, int>(_offset1 + _gap * 6, _offset2 + _gap * 6, _offset3 + _gap * 6) },
        { Race.Scylla, new Tuple<int, int, int>(_offset1 + _gap * 7, _offset2 + _gap * 7, _offset3 + _gap * 7) },
        { Race.Harpy, new Tuple<int, int, int>(_offset1 + _gap * 8, _offset2 + _gap * 8, _offset3 + _gap * 8) },
        { Race.Imp, new Tuple<int, int, int>(_offset1 + _gap * 9, _offset2 + _gap * 9, _offset3 + _gap * 9) },
        { Race.Human, new Tuple<int, int, int>(_offset1 + _gap * 10, _offset2 + _gap * 10, _offset3 + _gap * 10) },
        { Race.Crypter, new Tuple<int, int, int>(_offset1 + _gap * 11, _offset2 + _gap * 11, _offset3 + _gap * 11) },
        { Race.Lamia, new Tuple<int, int, int>(_offset1 + _gap * 12, _offset2 + _gap * 12, _offset3 + _gap * 12) },
        { Race.Kangaroo, new Tuple<int, int, int>(_offset1 + _gap * 13, _offset2 + _gap * 13, _offset3 + _gap * 13) },
        { Race.Taurus, new Tuple<int, int, int>(_offset1 + _gap * 14, _offset2 + _gap * 14, _offset3 + _gap * 14) },
        { Race.Crux, new Tuple<int, int, int>(_offset1 + _gap * 15, _offset2 + _gap * 15, _offset3 + _gap * 15) },
        // TODO { Race.Equine,       new Tuple<int, int, int>(_offset1 + _gap * 16, _offset2 + _gap * 16,  _offset3 + _gap * 16) },
        { Race.Sergal, new Tuple<int, int, int>(_offset1 + _gap * 17, _offset2 + _gap * 17, _offset3 + _gap * 17) },
        { Race.Bee, new Tuple<int, int, int>(_offset1 + _gap * 18, _offset2 + _gap * 18, _offset3 + _gap * 18) },
        { Race.Drider, new Tuple<int, int, int>(_offset1 + _gap * 19, _offset2 + _gap * 19, _offset3 + _gap * 19) },
        { Race.Alraune, new Tuple<int, int, int>(_offset1 + _gap * 20, _offset2 + _gap * 20, _offset3 + _gap * 20) },
        { Race.Bat, new Tuple<int, int, int>(_offset1 + _gap * 21, _offset2 + _gap * 21, _offset3 + _gap * 21) },
        { Race.Panther, new Tuple<int, int, int>(_offset1 + _gap * 22, _offset2 + _gap * 22, _offset3 + _gap * 22) },
        { Race.Merfolk, new Tuple<int, int, int>(_offset1 + _gap * 23, _offset2 + _gap * 23, _offset3 + _gap * 23) },
        { Race.Avian, new Tuple<int, int, int>(_offset1 + _gap * 24, _offset2 + _gap * 24, _offset3 + _gap * 24) },
        { Race.Ant, new Tuple<int, int, int>(_offset1 + _gap * 25, _offset2 + _gap * 25, _offset3 + _gap * 25) },
        { Race.Frog, new Tuple<int, int, int>(_offset1 + _gap * 26, _offset2 + _gap * 26, _offset3 + _gap * 26) },
        { Race.Shark, new Tuple<int, int, int>(_offset1 + _gap * 27, _offset2 + _gap * 27, _offset3 + _gap * 27) },
        { Race.Deer, new Tuple<int, int, int>(_offset1 + _gap * 28, _offset2 + _gap * 28, _offset3 + _gap * 28) }
    };


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
        return Race2.GetBasic(race).RaceNumber;
    }

    internal static int RaceToIntForTeam(Race race)
    {
        return TeamNumberOffset + Race2.GetBasic(race).RaceNumber;
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
        return !Equals(side, Side.TrueNoneSide);
    }

    internal static bool isNone(Race race)
    {
        return Equals(race, Race.TrueNone);
    }

    internal static bool isNone(Side side)
    {
        return Equals(side, Side.TrueNoneSide);
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
        return race.HasTag(RaceTag.MainRace) || Equals(race, Race.Tiger) || Equals(race, Race.Succubus) || Equals(race, Race.Goblin);
    }


    internal static bool IsPlayableRace(Race race)
    {
        // Can't make monster races playable yet because of many special Monster Empire conditions
        return race.HasTag(RaceTag.MainRace) || race.HasTag(RaceTag.Merc); // || race.HasTag(RaceTag.Monster);
    }


    internal static bool IsRebelOrBandit(Side side)
    {
        return Equals(side, Side.RebelSide) || Equals(side, Side.BanditSide);
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
        return !Equals(side, Side.BanditSide);
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


public static class Race2
{
    internal static int NextRaceNumber = 0;
    internal static List<Race> RaceIdList = new List<Race>();
    internal static Dictionary<string, Race> StringMap = new Dictionary<string, Race>();
    public static BiMap<Race, Side> RaceSideMap = new BiMap<Race, Side>();

    internal static readonly Dictionary<Race, VeryBasicRaceData> BasicDataMap = new Dictionary<Race, VeryBasicRaceData>();

    internal static VeryBasicRaceData GetBasic(Race race)
    {
        return BasicDataMap[race];
    }

    public static void LoadHardcodedRaces()
    {
        foreach (Race race in RaceIdList)
        {
            race.Init();
        }
    }
}

public class VeryBasicRaceData
{
    internal RaceDataMaker RaceDataMaker;
    internal IRaceData RaceData;
    internal readonly HashSet<RaceTag> _tags = new HashSet<RaceTag>();

    /// <summary>
    ///     THIS IS NOT AN ID.
    ///     This is just an incremental number assigned to each Race.
    ///     IT IS ABSOLUTELY NOT GUARANTEED TO STAY THE SAME ACROSS VERSIONS
    ///     OR EVEN GAME LAUNCHES. NOT TO BE USED AS AN ID.
    /// </summary>
    public int RaceNumber;
}


public class Race : IComparable<Race>
{
    [OdinSerialize]
    internal readonly string Id;

    internal IRaceData RaceData => Race2.GetBasic(this).RaceData;

    internal static Race RegisterRace(string id, RaceDataMaker raceDataMaker, RaceTag[] tags)
    {
        Race race = new Race(id);

        VeryBasicRaceData basicRaceData = new VeryBasicRaceData();

        if (tags != null)
        {
            foreach (RaceTag tag in tags)
            {
                basicRaceData._tags.Add(tag);
            }
        }

        basicRaceData.RaceDataMaker = raceDataMaker;
        basicRaceData.RaceNumber = Race2.NextRaceNumber++;

        Race2.BasicDataMap[race] = basicRaceData;

        Race2.RaceIdList.Add(race);
        Race2.StringMap[id] = race;
        Race2.RaceSideMap.Add(race, new Side(id));

        return race;
    }

    public Side ToSide()
    {
        if (Race2.RaceSideMap.Forward.ContainsKey(this))
        {
            return Race2.RaceSideMap.Forward[this];
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

        return string.Compare(Id, other.Id, StringComparison.Ordinal);
    }


    internal Race(string id)
    {
        Id = id;
    }

    internal void Init()
    {
        Race2.GetBasic(this).RaceData = Race2.GetBasic(this).RaceDataMaker.Create();
    }

    internal static void CreateRace(string id, RaceDataMaker raceDataMaker, RaceTag[] tags = null)
    {
        Race race = RegisterRace(id, raceDataMaker, tags);
        race.Init();
    }

    public bool HasTag(RaceTag tag)
    {
        return Race2.GetBasic(this)._tags.Contains(tag);
    }

    public static Race TrueNone = null;

    // FIX Circular referrence static initialization
    public static Race Cat = RegisterRace("cat", Races.Graphics.Implementations.MainRaces.Cats.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Dog = RegisterRace("dog", Races.Graphics.Implementations.MainRaces.Dogs.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Fox = RegisterRace("fox", Races.Graphics.Implementations.MainRaces.Foxes.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Wolf = RegisterRace("wolf", Races.Graphics.Implementations.MainRaces.Wolves.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Bunny = RegisterRace("bunny", Races.Graphics.Implementations.MainRaces.Bunnies.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Lizard = RegisterRace("lizard", Races.Graphics.Implementations.MainRaces.Lizards.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Slime = RegisterRace("slime", Races.Graphics.Implementations.MainRaces.Slimes.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Scylla = RegisterRace("scylla", Races.Graphics.Implementations.MainRaces.Scylla.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Harpy = RegisterRace("harpy", Races.Graphics.Implementations.MainRaces.Harpies.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Imp = RegisterRace("imp", Races.Graphics.Implementations.MainRaces.Imps.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Human = RegisterRace("human", Races.Graphics.Implementations.MainRaces.Humans.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Crypter = RegisterRace("crypter", Races.Graphics.Implementations.MainRaces.Crypters.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Lamia = RegisterRace("lamia", Races.Graphics.Implementations.MainRaces.Lamia.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Kangaroo = RegisterRace("kangaroo", Races.Graphics.Implementations.MainRaces.Kangaroos.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Taurus = RegisterRace("taurus", Races.Graphics.Implementations.MainRaces.Taurus.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Crux = RegisterRace("crux", Races.Graphics.Implementations.MainRaces.Crux.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Equine = null; // TODO RegisterRace("equine", Races.Graphics.Implementations.MainRaces.EquinesImrpoved.Instance,    new[]{ RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Sergal = RegisterRace("sergal", Races.Graphics.Implementations.MainRaces.Sergal.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Bee = RegisterRace("bee", Races.Graphics.Implementations.MainRaces.Bees.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Drider = RegisterRace("drider", Races.Graphics.Implementations.MainRaces.Driders.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Alraune = RegisterRace("alraune", Races.Graphics.Implementations.MainRaces.Alraune.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Bat = RegisterRace("bat", Races.Graphics.Implementations.MainRaces.DemiBats.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Panther = RegisterRace("panther", Races.Graphics.Implementations.MainRaces.Panthers.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Merfolk = RegisterRace("merfolk", Races.Graphics.Implementations.MainRaces.Merfolk.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Avian = RegisterRace("avian", Races.Graphics.Implementations.MainRaces.Avians.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Ant = RegisterRace("ant", Races.Graphics.Implementations.MainRaces.Ants.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Frog = RegisterRace("frog", Races.Graphics.Implementations.MainRaces.Demifrogs.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Shark = RegisterRace("shark", Races.Graphics.Implementations.MainRaces.Demisharks.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Deer = RegisterRace("deer", Races.Graphics.Implementations.MainRaces.Deer.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });
    public static Race Aabayx = RegisterRace("aabayx", Races.Graphics.Implementations.MainRaces.Aabayx.Instance, new[] { RaceTag.Humanoid, RaceTag.MainRace });

    public static Race Succubus = RegisterRace("succubu", Races.Graphics.Implementations.Mercs.Succubi.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Tiger = RegisterRace("tiger", Races.Graphics.Implementations.Mercs.Tigers.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Goblin = RegisterRace("goblin", Races.Graphics.Implementations.Mercs.Goblins.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Alligator = RegisterRace("alligator", Races.Graphics.Implementations.Mercs.Alligators.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Puca = RegisterRace("puca", Races.Graphics.Implementations.Mercs.Puca.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Kobold = RegisterRace("Kobold", Races.Graphics.Implementations.Mercs.Kobolds.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race DewSprite = RegisterRace("dewSprite", Races.Graphics.Implementations.Mercs.DewSprites.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Hippo = RegisterRace("hippo", Races.Graphics.Implementations.Mercs.Hippos.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Viper = RegisterRace("viper", Races.Graphics.Implementations.Mercs.Vipers.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Komodo = RegisterRace("komodo", Races.Graphics.Implementations.Mercs.Komodos.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Cockatrice = RegisterRace("cockatrice", Races.Graphics.Implementations.Mercs.Cockatrice.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Vargul = RegisterRace("vargul", Races.Graphics.Implementations.Mercs.Vargul.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });
    public static Race Youko = RegisterRace("youko", Races.Graphics.Implementations.Mercs.Youko.Instance, new[] { RaceTag.Humanoid, RaceTag.Merc });

    public static Race Vagrant = RegisterRace("vagrant", Races.Graphics.Implementations.Monsters.Vagrants.Instance, new[] { RaceTag.Monster });
    public static Race Serpent = RegisterRace("serpent", Races.Graphics.Implementations.Monsters.Serpents.Instance, new[] { RaceTag.Monster });
    public static Race Wyvern = RegisterRace("wyvern", Races.Graphics.Implementations.Monsters.Wyvern.Instance, new[] { RaceTag.Monster });
    public static Race YoungWyvern = RegisterRace("youngWyvern", Races.Graphics.Implementations.Monsters.YoungWyvern.Instance, new[] { RaceTag.Monster });
    public static Race Compy = RegisterRace("compy", Races.Graphics.Implementations.Monsters.Compy.Instance, new[] { RaceTag.Monster });
    public static Race FeralShark = RegisterRace("feralShark", Races.Graphics.Implementations.Monsters.FeralSharks.Instance, new[] { RaceTag.Monster });
    public static Race FeralWolve = RegisterRace("feralWolve", Races.Graphics.Implementations.Monsters.FeralWolves.Instance, new[] { RaceTag.Monster });
    public static Race DarkSwallower = RegisterRace("darkSwallower", Races.Graphics.Implementations.Monsters.DarkSwallower.Instance, new[] { RaceTag.Monster });
    public static Race Cake = RegisterRace("cake", Races.Graphics.Implementations.Monsters.Cake.Instance, new[] { RaceTag.Monster });
    public static Race Harvester = RegisterRace("harvester", Races.Graphics.Implementations.Monsters.Harvesters.Instance, new[] { RaceTag.Monster });
    public static Race Collector = RegisterRace("collector", Races.Graphics.Implementations.Monsters.Collectors.Instance, new[] { RaceTag.Monster });
    public static Race Voilin = RegisterRace("voilin", Races.Graphics.Implementations.Monsters.Voilin.Instance, new[] { RaceTag.Monster });
    public static Race FeralBat = RegisterRace("feralBat", Races.Graphics.Implementations.Monsters.FeralBats.Instance, new[] { RaceTag.Monster });
    public static Race FeralFrog = RegisterRace("feralFrog", Races.Graphics.Implementations.Monsters.FeralFrogs.Instance, new[] { RaceTag.Monster });
    public static Race Dragon = RegisterRace("dragon", Races.Graphics.Implementations.Monsters.Dragon.Instance, new[] { RaceTag.Monster });
    public static Race Dragonfly = RegisterRace("dragonfly", Races.Graphics.Implementations.Monsters.Dragonfly.Instance, new[] { RaceTag.Monster });
    public static Race TwistedVine = RegisterRace("twistedVine", Races.Graphics.Implementations.Monsters.TwistedVines.Instance, new[] { RaceTag.Monster });
    public static Race Fairy = RegisterRace("fairy", Races.Graphics.Implementations.Monsters.Fairies.Instance, new[] { RaceTag.Monster });
    public static Race FeralAnt = RegisterRace("feralAnt", Races.Graphics.Implementations.Monsters.FeralAnts.Instance, new[] { RaceTag.Monster });
    public static Race Gryphon = RegisterRace("gryphon", Races.Graphics.Implementations.Monsters.Gryphons.Instance, new[] { RaceTag.Monster });
    public static Race SpitterSlug = RegisterRace("spitterSlug", Races.Graphics.Implementations.Monsters.SpitterSlugs.Instance, new[] { RaceTag.Monster });
    public static Race SpringSlug = RegisterRace("springSlug", Races.Graphics.Implementations.Monsters.SpringSlugs.Instance, new[] { RaceTag.Monster });
    public static Race RockSlug = RegisterRace("rockSlug", Races.Graphics.Implementations.Monsters.RockSlugs.Instance, new[] { RaceTag.Monster });
    public static Race CoralSlug = RegisterRace("coralSlug", Races.Graphics.Implementations.Monsters.CoralSlugs.Instance, new[] { RaceTag.Monster });
    public static Race Salamander = RegisterRace("salamander", Races.Graphics.Implementations.Monsters.Salamanders.Instance, new[] { RaceTag.Monster });
    public static Race Manti = RegisterRace("mantis", Races.Graphics.Implementations.Monsters.Mantis.Instance, new[] { RaceTag.Monster });
    public static Race EasternDragon = RegisterRace("easternDragon", Races.Graphics.Implementations.Monsters.EasternDragon.Instance, new[] { RaceTag.Monster });
    public static Race Catfish = RegisterRace("catfish", Races.Graphics.Implementations.Monsters.Catfish.Instance, new[] { RaceTag.Monster });
    public static Race Raptor = RegisterRace("raptor", Races.Graphics.Implementations.Monsters.Raptor.Instance, new[] { RaceTag.Monster });
    public static Race WarriorAnt = RegisterRace("warriorAnt", Races.Graphics.Implementations.Monsters.WarriorAnts.Instance, new[] { RaceTag.Monster });
    public static Race Gazelle = RegisterRace("gazelle", Races.Graphics.Implementations.Monsters.Gazelle.Instance, new[] { RaceTag.Monster });
    public static Race Earthworms = RegisterRace("earthworm", Races.Graphics.Implementations.Monsters.Earthworms.Instance, new[] { RaceTag.Monster });
    public static Race FeralLizard = RegisterRace("feralLizard", Races.Graphics.Implementations.Monsters.FeralLizards.Instance, new[] { RaceTag.Monster });
    public static Race Monitor = RegisterRace("monitor", Races.Graphics.Implementations.Monsters.Monitors.Instance, new[] { RaceTag.Monster });
    public static Race Schiwardez = RegisterRace("schiwardez", Races.Graphics.Implementations.Monsters.Schiwardez.Instance, new[] { RaceTag.Monster });
    public static Race Terrorbird = RegisterRace("terrorbird", Races.Graphics.Implementations.Monsters.Terrorbird.Instance, new[] { RaceTag.Monster });
    public static Race Dratopyr = RegisterRace("dratopyr", Races.Graphics.Implementations.Monsters.Dratopyr.Instance, new[] { RaceTag.Monster });
    public static Race FeralLion = RegisterRace("feralLion", Races.Graphics.Implementations.Monsters.FeralLions.Instance, new[] { RaceTag.Monster });
    public static Race Goodra = RegisterRace("goodra", Races.Graphics.Implementations.Monsters.Goodra.Instance, new[] { RaceTag.Monster });
    public static Race Whisp = RegisterRace("whisp", Races.Graphics.Implementations.Monsters.Whisp.Instance, new[] { RaceTag.Monster });

    public static Race Selicia = RegisterRace("selicia", Races.Graphics.Implementations.UniqueMercs.Selicia.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Vision = RegisterRace("vision", Races.Graphics.Implementations.UniqueMercs.Vision.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Ki = RegisterRace("ki", Races.Graphics.Implementations.UniqueMercs.Ki.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Scorch = RegisterRace("scorch", Races.Graphics.Implementations.UniqueMercs.Scorch.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Asura = RegisterRace("asura", Races.Graphics.Implementations.UniqueMercs.Asura.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race DRACO = RegisterRace("draco", Races.Graphics.Implementations.UniqueMercs.DRACO.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Zoey = RegisterRace("zoey", Races.Graphics.Implementations.UniqueMercs.Zoey.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Abakhanskya = RegisterRace("abakhanskya", Races.Graphics.Implementations.UniqueMercs.Abakhanskya.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Zera = RegisterRace("zera", Races.Graphics.Implementations.UniqueMercs.Zera.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Auri = RegisterRace("auri", Races.Graphics.Implementations.UniqueMercs.Auri.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Erin = RegisterRace("erin", Races.Graphics.Implementations.UniqueMercs.Erin.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });
    public static Race Salix = RegisterRace("salix", Races.Graphics.Implementations.UniqueMercs.Salix.Instance, new[] { RaceTag.Humanoid, RaceTag.UniqueMerc });


    /*


    public static Race Cats;
    public static Race Dogs;
    public static Race Foxes;
    public static Race Wolves;
    public static Race Bunnies;
    public static Race Lizards;
    public static Race Slimes;
    public static Race Scylla;
    public static Race Harpies;
    public static Race Imps;
    public static Race Humans;
    public static Race Crypters;
    public static Race Lamia;
    public static Race Kangaroos;
    public static Race Taurus;
    public static Race Crux;
    public static Race Equines;
    public static Race Sergal;
    public static Race Bees;
    public static Race Driders;
    public static Race Alraune;
    public static Race DemiBats;
    public static Race Panthers;
    public static Race Merfolk;
    public static Race Avians;
    public static Race Ants;
    public static Race Demifrogs;
    public static Race Demisharks;
    public static Race Deer;
    public static Race Aabayx;

    public static Race Succubi;
    public static Race Tigers;
    public static Race Goblins;
    public static Race Alligators;
    public static Race Puca;
    public static Race Kobolds;
    public static Race DewSprites;
    public static Race Hippos;
    public static Race Vipers;
    public static Race Komodos;
    public static Race Cockatrice;
    public static Race Vargul;
    public static Race Youko;

    public static Race Vagrants;
    public static Race Serpents;
    public static Race Wyvern;
    public static Race YoungWyvern;
    public static Race Compy;
    public static Race FeralSharks;
    public static Race FeralWolves;
    public static Race DarkSwallower;
    public static Race Cake;
    public static Race Harvesters;
    public static Race Collectors;
    public static Race Voilin;
    public static Race FeralBats;
    public static Race FeralFrogs;
    public static Race Dragon;
    public static Race Dragonfly;
    public static Race TwistedVines;
    public static Race Fairies;
    public static Race FeralAnts;
    public static Race Gryphons;
    public static Race SpitterSlugs;
    public static Race SpringSlugs;
    public static Race RockSlugs;
    public static Race CoralSlugs;
    public static Race Salamanders;
    public static Race Mantis;
    public static Race EasternDragon;
    public static Race Catfish;
    public static Race Raptor;
    public static Race WarriorAnts;
    public static Race Gazelle;
    public static Race Earthworms;
    public static Race FeralLizards;
    public static Race Monitors;
    public static Race Schiwardez;
    public static Race Terrorbird;
    public static Race Dratopyr;
    public static Race FeralLions;
    public static Race Goodra;
    public static Race Whisp;

    public static Race Selicia;
    public static Race Vision;
    public static Race Ki;
    public static Race Scorch;
    public static Race Asura;
    public static Race DRACO;
    public static Race Zoey;
    public static Race Abakhanskya;
    public static Race Zera;
    public static Race Auri;
    public static Race Erin;
    public static Race Salix;

    public static void LoadHardcodedRaces()
    {
        Cats = new Race("cat", Races.Graphics.Implementations.MainRaces.Cats.Instance,                    new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Dogs = new Race("dog", Races.Graphics.Implementations.MainRaces.Dogs.Instance,                    new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Foxes = new Race("fox", Races.Graphics.Implementations.MainRaces.Foxes.Instance,                  new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Wolves = new Race("wolf", Races.Graphics.Implementations.MainRaces.Wolves.Instance,               new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Bunnies = new Race("bunny", Races.Graphics.Implementations.MainRaces.Bunnies.Instance,            new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Lizards = new Race("lizard", Races.Graphics.Implementations.MainRaces.Lizards.Instance,           new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Slimes = new Race("slime", Races.Graphics.Implementations.MainRaces.Slimes.Instance,              new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Scylla = new Race("scylla", Races.Graphics.Implementations.MainRaces.Scylla.Instance,             new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Harpies = new Race("harpy", Races.Graphics.Implementations.MainRaces.Harpies.Instance,            new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Imps = new Race("imp", Races.Graphics.Implementations.MainRaces.Imps.Instance,                    new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Humans = new Race("human", Races.Graphics.Implementations.MainRaces.Humans.Instance,              new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Crypters = new Race("crypter", Races.Graphics.Implementations.MainRaces.Crypters.Instance,        new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Lamia = new Race("lamia", Races.Graphics.Implementations.MainRaces.Lamia.Instance,                new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Kangaroos = new Race("kangaroo", Races.Graphics.Implementations.MainRaces.Kangaroos.Instance,     new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Taurus = new Race("taurus", Races.Graphics.Implementations.MainRaces.Taurus.Instance,             new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Crux = new Race("crux", Races.Graphics.Implementations.MainRaces.Crux.Instance,                   new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Equines = new Race("equines", Races.Graphics.Implementations.MainRaces.EquinesLua.Instance,       new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Sergal = new Race("sergal", Races.Graphics.Implementations.MainRaces.Sergal.Instance,             new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Bees = new Race("bees", Races.Graphics.Implementations.MainRaces.Bees.Instance,                   new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Driders = new Race("driders", Races.Graphics.Implementations.MainRaces.Driders.Instance,          new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Alraune = new Race("alraune", Races.Graphics.Implementations.MainRaces.Alraune.Instance,          new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        DemiBats = new Race("demiBats", Races.Graphics.Implementations.MainRaces.DemiBats.Instance,       new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Panthers = new Race("panthers", Races.Graphics.Implementations.MainRaces.Panthers.Instance,       new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Merfolk = new Race("merfolk", Races.Graphics.Implementations.MainRaces.Merfolk.Instance,          new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Avians = new Race("avians", Races.Graphics.Implementations.MainRaces.Avians.Instance,             new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Ants = new Race("ants", Races.Graphics.Implementations.MainRaces.Ants.Instance,                   new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Demifrogs = new Race("frogs", Races.Graphics.Implementations.MainRaces.Demifrogs.Instance,        new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Demisharks = new Race("sharks", Races.Graphics.Implementations.MainRaces.Demisharks.Instance,     new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Deer = new Race("deer", Races.Graphics.Implementations.MainRaces.Deer.Instance,                   new[]{ RaceTag.Humanoid, RaceTag.MainRace });
        Aabayx = new Race("aabayx", Races.Graphics.Implementations.MainRaces.Aabayx.Instance,             new[]{ RaceTag.Humanoid, RaceTag.MainRace });

        Succubi = new Race("succubus", Races.Graphics.Implementations.Mercs.Succubi.Instance,         new[]{ RaceTag.Humanoid, RaceTag.Merc });
        Tigers = new Race("tiger", Races.Graphics.Implementations.Mercs.Tigers.Instance,              new[]{ RaceTag.Humanoid, RaceTag.Merc });
        Goblins = new Race("goblin", Races.Graphics.Implementations.Mercs.Goblins.Instance,           new[]{ RaceTag.Humanoid, RaceTag.Merc });
        Alligators = new Race("alligator", Races.Graphics.Implementations.Mercs.Alligators.Instance,  new[]{ RaceTag.Humanoid, RaceTag.Merc });
        Puca = new Race("puca", Races.Graphics.Implementations.Mercs.Puca.Instance,                   new[]{ RaceTag.Humanoid, RaceTag.Merc });
        Kobolds = new Race("Kobold", Races.Graphics.Implementations.Mercs.Kobolds.Instance,           new[]{ RaceTag.Humanoid, RaceTag.Merc });
        DewSprites = new Race("dewSprite", Races.Graphics.Implementations.Mercs.DewSprites.Instance,  new[]{ RaceTag.Humanoid, RaceTag.Merc });
        Hippos = new Race("hippos", Races.Graphics.Implementations.Mercs.Hippos.Instance,             new[]{ RaceTag.Humanoid, RaceTag.Merc });
        Vipers = new Race("vipers", Races.Graphics.Implementations.Mercs.Vipers.Instance,             new[]{ RaceTag.Humanoid, RaceTag.Merc });
        Komodos = new Race("komodos", Races.Graphics.Implementations.Mercs.Komodos.Instance,          new[]{ RaceTag.Humanoid, RaceTag.Merc });
        Cockatrice = new Race("cockatrice", Races.Graphics.Implementations.Mercs.Cockatrice.Instance, new[]{ RaceTag.Humanoid, RaceTag.Merc });
        Vargul = new Race("vargul", Races.Graphics.Implementations.Mercs.Vargul.Instance,             new[]{ RaceTag.Humanoid, RaceTag.Merc });
        Youko = new Race("youko", Races.Graphics.Implementations.Mercs.Youko.Instance,                new[]{ RaceTag.Humanoid, RaceTag.Merc });

        Vagrants = new Race("vagrants", Races.Graphics.Implementations.Monsters.Vagrants.Instance,                new[]{ RaceTag.Monster });
        Serpents = new Race("serpents", Races.Graphics.Implementations.Monsters.Serpents.Instance,                new[]{ RaceTag.Monster });
        Wyvern = new Race("wyvern", Races.Graphics.Implementations.Monsters.Wyvern.Instance,                      new[]{ RaceTag.Monster });
        YoungWyvern = new Race("youngWyvern", Races.Graphics.Implementations.Monsters.YoungWyvern.Instance,       new[]{ RaceTag.Monster });
        Compy = new Race("compy", Races.Graphics.Implementations.Monsters.Compy.Instance,                         new[]{ RaceTag.Monster });
        FeralSharks = new Race("feralSharks", Races.Graphics.Implementations.Monsters.FeralSharks.Instance,       new[]{ RaceTag.Monster });
        FeralWolves = new Race("feralWolves", Races.Graphics.Implementations.Monsters.FeralWolves.Instance,       new[]{ RaceTag.Monster });
        DarkSwallower = new Race("darkSwallower", Races.Graphics.Implementations.Monsters.DarkSwallower.Instance, new[]{ RaceTag.Monster });
        Cake = new Race("cake", Races.Graphics.Implementations.Monsters.Cake.Instance,                            new[]{ RaceTag.Monster });
        Harvesters = new Race("harvesters", Races.Graphics.Implementations.Monsters.Harvesters.Instance,          new[]{ RaceTag.Monster });
        Collectors = new Race("collectors", Races.Graphics.Implementations.Monsters.Collectors.Instance,          new[]{ RaceTag.Monster });
        Voilin = new Race("voilin", Races.Graphics.Implementations.Monsters.Voilin.Instance,                      new[]{ RaceTag.Monster });
        FeralBats = new Race("feralBats", Races.Graphics.Implementations.Monsters.FeralBats.Instance,             new[]{ RaceTag.Monster });
        FeralFrogs = new Race("feralFrogs", Races.Graphics.Implementations.Monsters.FeralFrogs.Instance,          new[]{ RaceTag.Monster });
        Dragon = new Race("dragon", Races.Graphics.Implementations.Monsters.Dragon.Instance,                      new[]{ RaceTag.Monster });
        Dragonfly = new Race("dragonfly", Races.Graphics.Implementations.Monsters.Dragonfly.Instance,             new[]{ RaceTag.Monster });
        TwistedVines = new Race("twistedVines", Races.Graphics.Implementations.Monsters.TwistedVines.Instance,    new[]{ RaceTag.Monster });
        Fairies = new Race("fairies", Races.Graphics.Implementations.Monsters.Fairies.Instance,                   new[]{ RaceTag.Monster });
        FeralAnts = new Race("feralAnts", Races.Graphics.Implementations.Monsters.FeralAnts.Instance,             new[]{ RaceTag.Monster });
        Gryphons = new Race("gryphons", Races.Graphics.Implementations.Monsters.Gryphons.Instance,                new[]{ RaceTag.Monster });
        SpitterSlugs = new Race("spitterSlugs", Races.Graphics.Implementations.Monsters.SpitterSlugs.Instance,    new[]{ RaceTag.Monster });
        SpringSlugs = new Race("springSlugs", Races.Graphics.Implementations.Monsters.SpringSlugs.Instance,       new[]{ RaceTag.Monster });
        RockSlugs = new Race("rockSlugs", Races.Graphics.Implementations.Monsters.RockSlugs.Instance,             new[]{ RaceTag.Monster });
        CoralSlugs = new Race("coralSlugs", Races.Graphics.Implementations.Monsters.CoralSlugs.Instance,          new[]{ RaceTag.Monster });
        Salamanders = new Race("salamanders", Races.Graphics.Implementations.Monsters.Salamanders.Instance,       new[]{ RaceTag.Monster });
        Mantis = new Race("mantis", Races.Graphics.Implementations.Monsters.Mantis.Instance,                      new[]{ RaceTag.Monster });
        EasternDragon = new Race("easternDragon", Races.Graphics.Implementations.Monsters.EasternDragon.Instance, new[]{ RaceTag.Monster });
        Catfish = new Race("catfish", Races.Graphics.Implementations.Monsters.Catfish.Instance,                   new[]{ RaceTag.Monster });
        Raptor = new Race("raptor", Races.Graphics.Implementations.Monsters.Raptor.Instance,                      new[]{ RaceTag.Monster });
        WarriorAnts = new Race("warriorAnts", Races.Graphics.Implementations.Monsters.WarriorAnts.Instance,       new[]{ RaceTag.Monster });
        Gazelle = new Race("gazelle", Races.Graphics.Implementations.Monsters.Gazelle.Instance,                   new[]{ RaceTag.Monster });
        Earthworms = new Race("earthworms", Races.Graphics.Implementations.Monsters.Earthworms.Instance,          new[]{ RaceTag.Monster });
        FeralLizards = new Race("feralLizards", Races.Graphics.Implementations.Monsters.FeralLizards.Instance,    new[]{ RaceTag.Monster });
        Monitors = new Race("monitors", Races.Graphics.Implementations.Monsters.Monitors.Instance,                new[]{ RaceTag.Monster });
        Schiwardez = new Race("schiwardez", Races.Graphics.Implementations.Monsters.Schiwardez.Instance,          new[]{ RaceTag.Monster });
        Terrorbird = new Race("terrorbird", Races.Graphics.Implementations.Monsters.Terrorbird.Instance,          new[]{ RaceTag.Monster });
        Dratopyr = new Race("dratopyr", Races.Graphics.Implementations.Monsters.Dratopyr.Instance,                new[]{ RaceTag.Monster });
        FeralLions = new Race("feralLions", Races.Graphics.Implementations.Monsters.FeralLions.Instance,          new[]{ RaceTag.Monster });
        Goodra = new Race("goodra", Races.Graphics.Implementations.Monsters.Goodra.Instance,                      new[]{ RaceTag.Monster });
        Whisp = new Race("whisp", Races.Graphics.Implementations.Monsters.Whisp.Instance,                         new[]{ RaceTag.Monster });

        Selicia = new Race("selicia", Races.Graphics.Implementations.UniqueMercs.Selicia.Instance,             new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
        Vision = new Race("vision", Races.Graphics.Implementations.UniqueMercs.Vision.Instance,                new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
        Ki = new Race("ki", Races.Graphics.Implementations.UniqueMercs.Ki.Instance,                            new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
        Scorch = new Race("scorch", Races.Graphics.Implementations.UniqueMercs.Scorch.Instance,                new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
        Asura = new Race("asura", Races.Graphics.Implementations.UniqueMercs.Asura.Instance,                   new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
        DRACO = new Race("draco", Races.Graphics.Implementations.UniqueMercs.DRACO.Instance,                   new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
        Zoey = new Race("zoey", Races.Graphics.Implementations.UniqueMercs.Zoey.Instance,                      new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
        Abakhanskya = new Race("abakhanskya", Races.Graphics.Implementations.UniqueMercs.Abakhanskya.Instance, new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
        Zera = new Race("zera", Races.Graphics.Implementations.UniqueMercs.Zera.Instance,                      new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
        Auri = new Race("auri", Races.Graphics.Implementations.UniqueMercs.Auri.Instance,                      new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
        Erin = new Race("erin", Races.Graphics.Implementations.UniqueMercs.Erin.Instance,                      new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
        Salix = new Race("salix", Races.Graphics.Implementations.UniqueMercs.Salix.Instance,                   new[]{ RaceTag.Humanoid, RaceTag.UniqueMerc });
    }


     */
}
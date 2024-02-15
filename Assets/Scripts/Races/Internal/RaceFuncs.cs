using System;
using System.Collections.Generic;
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

    // TODO move to race implementations
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

    // TODO move to race implementations
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

    // TODO move to race implementations
    private static Dictionary<Race, Func<Sprite>> _bannerSprites = new Dictionary<Race, Func<Sprite>>()
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
        if (_bannerSprites.TryGetValue(race, out Func<Sprite> sprite))
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

    internal static int RaceToTempNumber(Race race)
    {
        return Race2.GetBasic(race).RaceNumber;
    }

    internal static int RaceToIntForTeam(Race race)
    {
        return TeamNumberOffset + Race2.GetBasic(race).RaceNumber;
    }

    internal static bool IsHumanoid(Race race)
    {
        return race.HasTag(RaceTag.Humanoid);
    }

    // RaceFuncs.RaceToIntForCompare(race) < RaceFuncs.RaceToIntForCompare(Race.Selicia) 
    internal static bool IsNotUniqueMerc(Race race)
    {
        return !race.HasTag(RaceTag.UniqueMerc);
    }

    // RaceFuncs.RaceToIntForCompare(race) >= RaceFuncs.RaceToIntForCompare(Race.Vagrants)
    internal static bool IsMosnterOrUniqueMerc(Race race)
    {
        return race.HasTag(RaceTag.UniqueMerc) || race.HasTag(RaceTag.Monster);
    }

    // RaceFuncs.RaceToIntForCompare(unit.Race) >= RaceFuncs.RaceToIntForCompare(Race.Selicia)
    internal static bool IsUniqueMerc(Race race)
    {
        return race.HasTag(RaceTag.UniqueMerc);
    }

    internal static bool IsUniqueMerc(Side side)
    {
        return side.HasTag(RaceTag.UniqueMerc);
    }

    // RaceFuncs.RaceToIntForCompare(race) < RaceFuncs.RaceToIntForCompare(Race.Vagrants)
    internal static bool IsMainRaceOrMerc(Race race)
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

    internal static bool IsNotNone(Race race)
    {
        return !Equals(race, Race.TrueNone);
    }

    internal static bool IsNotNone(Side side)
    {
        return !Equals(side, Side.TrueNoneSide);
    }

    internal static bool IsNone(Race race)
    {
        return Equals(race, Race.TrueNone);
    }

    internal static bool IsNone(Side side)
    {
        return Equals(side, Side.TrueNoneSide);
    }

    // RaceFuncs.RaceToIntForCompare(race) >= RaceFuncs.RaceToIntForCompare(Race.Vagrants) && RaceFuncs.RaceToIntForCompare(race) < RaceFuncs.RaceToIntForCompare(Race.Selicia)
    internal static bool IsMonster(Race race)
    {
        return race.HasTag(RaceTag.Monster);
    }

    internal static bool IsMonster(Side side)
    {
        return side.HasTag(RaceTag.Monster);
    }

    internal static bool IsMainRaceOrMercOrMonster(Side side)
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
    internal static bool IsMainRaceOrTigerOrSuccubiOrGoblin(Race race)
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
        return IsMonster(side) || IsUniqueMerc(side) || IsRebelOrBandit(side);
    }

    // Formerly >= RaceFuncs.RaceToInt(Race.Selicia)
    internal static bool IsUniqueMercsOrRebelsOrBandits(Side side)
    {
        return IsUniqueMerc(side) || IsRebelOrBandit(side);
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
    internal static IRaceData GetRace(Unit unit)
    {
        // TODO not sure how to improve this. 
        if (Equals(unit.Race, Race.Slime) && unit.Type == UnitType.Leader)
        {
            // TODO come up with a way to implement this neatly
            //return SlimeQueen.Instance;
        }

        if (Equals(unit.Race, Race.Ant) && unit.Type == UnitType.Leader)
        {
            //return AntQueen.Instance;
        }

        return GetRace(unit.Race);
    }

    /// <summary>
    ///     This version can't do the slime queen check, but is fine anywhere else
    /// </summary>
    internal static IRaceData GetRace(Race race)
    {
        if (race == null)
        {
            Debug.Log("called get race with a null");
            return null;
        }

        return Race2.GetBasic(race).RaceData;
    }
}
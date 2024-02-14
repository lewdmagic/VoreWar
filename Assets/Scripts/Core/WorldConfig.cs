using OdinSerializer;
using System;
using System.Collections.Generic;


public class WorldConfig
{
    [OdinSerialize]
    internal Dictionary<string, bool> Toggles = new Dictionary<string, bool>();

    [OdinSerialize]
    internal Dictionary<Race, SpawnerInfo> SpawnerInfo = new Dictionary<Race, SpawnerInfo>();

    [OdinSerialize]
    internal readonly Dictionary<Race, int> VillagesPerEmpire = MakeVillagesPerEmpire();

    internal void resetVillagesPerEmpire()
    {
        foreach (Race race in RaceFuncs.MainRaceEnumerable())
        {
            VillagesPerEmpire[race] = 0;
        }
    }

    private static Dictionary<Race, int> MakeVillagesPerEmpire()
    {
        Dictionary<Race, int> villagesPerEmpire = new Dictionary<Race, int>();
        foreach (Race race in RaceFuncs.MainRaceEnumerable())
        {
            villagesPerEmpire[race] = 0;
        }

        return villagesPerEmpire;
    }

    [OdinSerialize]
    private int _strategicWorldSizeX = 32;

    internal int StrategicWorldSizeX { get => _strategicWorldSizeX; set => _strategicWorldSizeX = value; }

    [OdinSerialize]
    private int _strategicWorldSizeY = 32;

    internal int StrategicWorldSizeY { get => _strategicWorldSizeY; set => _strategicWorldSizeY = value; }

    [OdinSerialize]
    private bool _autoScaleTactical = true;

    internal bool AutoScaleTactical { get => _autoScaleTactical; set => _autoScaleTactical = value; }

    [OdinSerialize]
    private int _tacticalSizeX = 24;

    internal int TacticalSizeX { get => _tacticalSizeX; set => _tacticalSizeX = value; }

    [OdinSerialize]
    private int _tacticalSizeY = 24;

    internal int TacticalSizeY { get => _tacticalSizeY; set => _tacticalSizeY = value; }

    [OdinSerialize, AllowEditing, ProperName("Experience Per Level"), IntegerRange(0, 9999), Description("Base Exp required per level")]
    internal int ExperiencePerLevel = 20;

    [OdinSerialize, AllowEditing, ProperName("Additional Experience Per Level"), IntegerRange(0, 9999), Description("How much extra exp is required per level")]
    internal int AdditionalExperiencePerLevel = 1;

    [OdinSerialize, AllowEditing, ProperName("Village Income Percent"), IntegerRange(0, 9999), Description("Multiplier to Village income")]
    internal int VillageIncomePercent = 100;

    [OdinSerialize, AllowEditing, ProperName("Villagers Per Farm"), IntegerRange(0, 9999), Description("Doesn't take effect until a new turn")]
    internal int VillagersPerFarm = 6;

    [OdinSerialize, AllowEditing, ProperName("Soft Level Cap"), IntegerRange(0, 9999), Description("After this level exp required spikes sharply")]
    internal int SoftLevelCap = 0;

    [OdinSerialize, AllowEditing, ProperName("Hard Level Cap"), IntegerRange(0, 9999), Description("After this level there are no more levels")]
    internal int HardLevelCap = 0;

    [OdinSerialize, AllowEditing, ProperName("Army Maintenance"), IntegerRange(0, 999), Description("Each unit in an active army costs this much gold per turn.  Default is 3.")]
    internal int ArmyUpkeep = 0;

    [OdinSerialize, AllowEditing, ProperName("Cap Max Garrison Increase"), IntegerRange(0, 9999), Description("Will make it so that the maximum increase to a garrison's size from buildings or a capital is 150% of the base value.   Basically designed to better support small army/garrison sizes, so if your max garrison size is 4, the capital will be a reasonable 6, instead of getting the full +8 and becoming 12.")]
    internal bool CapMaxGarrisonIncrease = true;

    [OdinSerialize]
    private int _maxSpellLevelDrop = 4;

    internal int MaxSpellLevelDrop { get => _maxSpellLevelDrop; set => _maxSpellLevelDrop = value; }

    [OdinSerialize]
    private int _armyMP = 3;

    internal int ArmyMP { get => _armyMP; set => _armyMP = value; }

    [OdinSerialize]
    private int _maxArmies = 32;

    internal int MaxArmies { get => _maxArmies; set => _maxArmies = value; }

    [OdinSerialize, AllowEditing, ProperName("Gold Mine Income"), IntegerRange(0, 9999), Description("Gold provided by gold mines")]
    internal int GoldMineIncome = 40;

    [OdinSerialize]
    private int _voreRate = 1;

    internal int VoreRate { get => _voreRate; set => _voreRate = value; }

    [OdinSerialize]
    private int _escapeRate = 1;

    internal int EscapeRate { get => _escapeRate; set => _escapeRate = value; }

    [OdinSerialize]
    private int _randomEventRate = 0;

    internal int RandomEventRate { get => _randomEventRate; set => _randomEventRate = value; }

    [OdinSerialize]
    private int _randomAIEventRate = 0;

    internal int RandomAIEventRate { get => _randomAIEventRate; set => _randomAIEventRate = value; }

    [OdinSerialize]
    private int _fogDistance = 2;

    internal int FogDistance { get => _fogDistance; set => _fogDistance = value; }


    [OdinSerialize]
    private float _weightLossFractionBreasts = 0;

    internal float WeightLossFractionBreasts { get => _weightLossFractionBreasts; set => _weightLossFractionBreasts = value; }

    [OdinSerialize]
    private float _weightLossFractionBody = 0;

    internal float WeightLossFractionBody { get => _weightLossFractionBody; set => _weightLossFractionBody = value; }

    [OdinSerialize]
    private float _weightLossFractionDick = 0;

    internal float WeightLossFractionDick { get => _weightLossFractionDick; set => _weightLossFractionDick = value; }

    [OdinSerialize]
    private float _growthDecayIncreaseRate = 0.04f;

    internal float GrowthDecayIncreaseRate { get => _growthDecayIncreaseRate; set => _growthDecayIncreaseRate = value; }

    [OdinSerialize]
    private float _growthDecayOffset = 0f;

    internal float GrowthDecayOffset { get => _growthDecayOffset; set => _growthDecayOffset = value; }

    [OdinSerialize]
    private float _growthMod = 1f;

    internal float GrowthMod { get => _growthMod; set => _growthMod = value; }

    [OdinSerialize]
    private float _growthCap = 5f;

    internal float GrowthCap { get => _growthCap; set => _growthCap = value; }

    [OdinSerialize]
    private float _autoSurrenderChance = 1;

    internal float AutoSurrenderChance { get => _autoSurrenderChance; set => _autoSurrenderChance = value; }

    [OdinSerialize]
    private float _autoSurrenderDefectChance = 0.25f;

    internal float AutoSurrenderDefectChance { get => _autoSurrenderDefectChance; set => _autoSurrenderDefectChance = value; }

    [OdinSerialize]
    private float _maleFraction = 0;

    internal float MaleFraction { get => _maleFraction; set => _maleFraction = value; }

    [OdinSerialize]
    private float _hermFraction = 0;

    internal float HermFraction { get => _hermFraction; set => _hermFraction = value; }

    [OdinSerialize]
    private float _clothedFraction = 0;

    internal float ClothedFraction { get => _clothedFraction; set => _clothedFraction = value; }

    [OdinSerialize]
    private float _furryFraction = 0;

    internal float FurryFraction { get => _furryFraction; set => _furryFraction = value; }

    [OdinSerialize]
    private float _hermNameFraction = 0;

    internal float HermNameFraction { get => _hermNameFraction; set => _hermNameFraction = value; }

    [OdinSerialize]
    private float _overallMonsterSpawnRateModifier = 1;

    internal float OverallMonsterSpawnRateModifier { get => _overallMonsterSpawnRateModifier; set => _overallMonsterSpawnRateModifier = value; }

    [OdinSerialize]
    private float _overallMonsterCapModifier = 1;

    internal float OverallMonsterCapModifier { get => _overallMonsterCapModifier; set => _overallMonsterCapModifier = value; }

    [OdinSerialize]
    private float _tacticalWaterValue = 0;

    internal float TacticalWaterValue { get => _tacticalWaterValue; set => _tacticalWaterValue = value; }

    [OdinSerialize]
    private float _tacticalTerrainFrequency = 0;

    internal float TacticalTerrainFrequency { get => _tacticalTerrainFrequency; set => _tacticalTerrainFrequency = value; }

    [OdinSerialize]
    private int _startingPopulation = 99999;

    internal int StartingPopulation { get => _startingPopulation; set => _startingPopulation = value; }

    [OdinSerialize]
    private List<TraitType> _leaderTraits;

    internal List<TraitType> LeaderTraits { get => _leaderTraits; set => _leaderTraits = value; }

    [OdinSerialize]
    private List<TraitType> _maleTraits;

    internal List<TraitType> MaleTraits { get => _maleTraits; set => _maleTraits = value; }

    [OdinSerialize]
    private List<TraitType> _femaleTraits;

    internal List<TraitType> FemaleTraits { get => _femaleTraits; set => _femaleTraits = value; }

    [OdinSerialize]
    private List<TraitType> _hermTraits;

    internal List<TraitType> HermTraits { get => _hermTraits; set => _hermTraits = value; }

    [OdinSerialize]
    private List<TraitType> _spawnTraits;

    internal List<TraitType> SpawnTraits { get => _spawnTraits; set => _spawnTraits = value; }

    [OdinSerialize]
    private float _customEventFrequency = 0;

    internal float CustomEventFrequency { get => _customEventFrequency; set => _customEventFrequency = value; }

    [OdinSerialize]
    private Orientation _malesLike = 0;

    internal Orientation MalesLike { get => _malesLike; set => _malesLike = value; }

    [OdinSerialize]
    private Orientation _femalesLike = 0;

    internal Orientation FemalesLike { get => _femalesLike; set => _femalesLike = value; }

    [OdinSerialize]
    private FairyBVType _fairyBVType = 0;

    internal FairyBVType FairyBVType { get => _fairyBVType; set => _fairyBVType = value; }

    [OdinSerialize]
    private FeedingType _feedingType = 0;

    internal FeedingType FeedingType { get => _feedingType; set => _feedingType = value; }

    [OdinSerialize]
    private FourthWallBreakType _fourthWallBreakType = 0;

    internal FourthWallBreakType FourthWallBreakType { get => _fourthWallBreakType; set => _fourthWallBreakType = value; }

    [OdinSerialize]
    private UBConversion _uBConversion = 0;

    internal UBConversion UBConversion { get => _uBConversion; set => _uBConversion = value; }

    [OdinSerialize]
    private SucklingPermission _sucklingPermission = 0;

    internal SucklingPermission SucklingPermission { get => _sucklingPermission; set => _sucklingPermission = value; }

    [OdinSerialize]
    private DiplomacyScale _diplomacyScale = 0;

    internal DiplomacyScale DiplomacyScale { get => _diplomacyScale; set => _diplomacyScale = value; }

    [OdinSerialize]
    internal Config.SeasonalType WinterStuff = 0;

    [OdinSerialize, AllowEditing, ProperName("Victory Condition"), Description("The condition required for victory")]
    internal Config.VictoryType VictoryCondition;

    [OdinSerialize]
    internal Config.MonsterConquestType MonsterConquest;

    [OdinSerialize]
    private int _monsterConquestTurns;

    internal int MonsterConquestTurns { get => _monsterConquestTurns; set => _monsterConquestTurns = value; }

    [OdinSerialize]
    private int _breastSizeModifier = 0;

    internal int BreastSizeModifier { get => _breastSizeModifier; set => _breastSizeModifier = value; }

    [OdinSerialize]
    private int _hermBreastSizeModifier = 0;

    internal int HermBreastSizeModifier { get => _hermBreastSizeModifier; set => _hermBreastSizeModifier = value; }

    [OdinSerialize]
    private int _cockSizeModifier = 0;

    internal int CockSizeModifier { get => _cockSizeModifier; set => _cockSizeModifier = value; }

    [OdinSerialize]
    private int _defaultStartingWeight = 3;

    internal int DefaultStartingWeight { get => _defaultStartingWeight; set => _defaultStartingWeight = value; }

    // DayNight configuration
    [OdinSerialize, AllowEditing, IntegerRange(1, 10), Description("It will be night for the entire round every X round. (Set to 1 for every round, 2 for every other, etc.)")]
    internal int NightRounds = 2;

    [OdinSerialize, AllowEditing, ProperName("Base Night Chance"), FloatRange(0, 1), Description("The % chance that it will be night on a given turn. Night will only last that Empire's turn and can occur multiple times per round.")]
    internal float BaseNightChance = 0.01f;

    [OdinSerialize, AllowEditing, ProperName("Night Chance Increase"), FloatRange(0, 1), Description("The increase of the % chance it will be night on a given turn, increasing every turn it is not night.  (% chance resets to the base chance after a night turn)")]
    internal float NightChanceIncrease = 0.01f;

    [OdinSerialize, AllowEditing, ProperName("Defualt Vision Radius"), IntegerRange(1, 5), Description("Radius of a unit's vision at night. Things like traits can also increase this.")]
    internal int DefualtTacticalSightRange = 1;

    [OdinSerialize, AllowEditing, ProperName("Defualt Vision Radius"), IntegerRange(0, 7), Description("Radius of a unit's vision at night. Things like traits can also increase this.")]
    internal int NightStrategicSightReduction = 1;

    [OdinSerialize, AllowEditing, ProperName("Reveal Turn"), Description("The tactical turn where every unit is revealed.")]
    internal int RevealTurn = 50;

    // CombatComplications configuration
    // Critical strikes
    [OdinSerialize, AllowEditing, ProperName("Base Critical Chance"), FloatRange(0, 1), Description("Base chance for a critical strike if not calculated from stats. If 'Stat Based Crit' is enabled with this, the chance will never be lower than this percentage, but it can be higher. Set to 0 to disable.")]
    internal float BaseCritChance = 0.05f;

    [OdinSerialize, AllowEditing, ProperName("Critical Damage Multiplier"), FloatRange(0, 1), Description("Damage is multiplied by this number. At default value (1.5), 10 damage is modified to 15")]
    internal float CritDamageMod = 1.5f;

    // Graze
    [OdinSerialize, AllowEditing, ProperName("Base Graze Chance"), FloatRange(0, 1), Description("Base chance for a graze if not calculated from stats. If 'Stat Based Graze' is enabled, the chance will never be lower than this percentage, but it can be higher. Set to 0 to disable.")]
    internal float BaseGrazeChance = 0.10f;

    [OdinSerialize, AllowEditing, ProperName("Graze Damage Multiplier"), FloatRange(0, 1), Description("Damage is multiplied by this number. At default value (0.3), 10 damage is modified to 3")]
    internal float GrazeDamageMod = 0.30f;

    [OdinSerialize]
    private bool _factionLeaders;

    internal bool FactionLeaders { get => _factionLeaders; set => _factionLeaders = value; }

    [OdinSerialize]
    private int _itemSlots;

    internal int ItemSlots { get => _itemSlots; set => _itemSlots = value; }

    [OdinSerialize]
    private float _burpFraction = .1f;

    internal float BurpFraction { get => _burpFraction; set => _burpFraction = value; }

    [OdinSerialize]
    private float _fartFraction = .1f;

    internal float FartFraction { get => _fartFraction; set => _fartFraction = value; }

    [OdinSerialize, AllowEditing, FloatRange(0, 1), ProperName("Leader death exp loss Percentage"), Description("On death they will lose this % of their total experience")]
    internal float LeaderLossExpPct = 0;

    [OdinSerialize, AllowEditing, ProperName("Leader levels lost on death"), IntegerRange(0, 9999), Description("On death they will this many levels")]
    internal int LeaderLossLevels = 1;

    [OdinSerialize]
    private int _oralWeight = 1;

    internal int OralWeight { get => _oralWeight; set => _oralWeight = value; }

    [OdinSerialize]
    private int _breastWeight = 1;

    internal int BreastWeight { get => _breastWeight; set => _breastWeight = value; }

    [OdinSerialize]
    private int _unbirthWeight = 1;

    internal int UnbirthWeight { get => _unbirthWeight; set => _unbirthWeight = value; }

    [OdinSerialize]
    private int _cockWeight = 1;

    internal int CockWeight { get => _cockWeight; set => _cockWeight = value; }

    [OdinSerialize]
    private int _tailWeight = 1;

    internal int TailWeight { get => _tailWeight; set => _tailWeight = value; }

    [OdinSerialize]
    private int _analWeight = 1;

    internal int AnalWeight { get => _analWeight; set => _analWeight = value; }


    internal bool GetValue(string name)
    {
        if (Toggles == null)
        {
            ResetDictionary();
        }

        if (Toggles.TryGetValue(name, out bool value)) return value;
        if (name == "ClothingDiscards")
        {
            Toggles[name] = true;
            return true;
        }
        else
            Toggles[name] = false;

        return false;
    }

    internal SpawnerInfo GetSpawner(Race race)
    {
        if (SpawnerInfo == null)
        {
            ResetSpawnerDictionary();
        }

        if (SpawnerInfo.TryGetValue(race, out SpawnerInfo value))
        {
            if (value.SpawnAttempts == 0) value.SpawnAttempts = 1;
            return value;
        }
        else
        {
            var obj = new SpawnerInfo(false, 4, .15f, 40, 900 + RaceFuncs.RaceToIntForTeam(race), 1, true, 6f, 8, 12, 40);
            SpawnerInfo[race] = obj;
            return obj;
        }
    }

    internal SpawnerInfo GetSpawnerWithoutGeneration(Race race)
    {
        if (SpawnerInfo.TryGetValue(race, out SpawnerInfo value))
        {
            if (value.SpawnAttempts == 0) value.SpawnAttempts = 1;
            return value;
        }

        return null;
    }

    internal void ResetSpawnerDictionary()
    {
        SpawnerInfo = new Dictionary<Race, SpawnerInfo>();
        foreach (Race race in RaceFuncs.AllMonstersRangeRaceEnumerable())
        {
            SpawnerInfo[race] = new SpawnerInfo(false, 4, .15f, 40, 900 + RaceFuncs.RaceToIntForTeam(race), 1, true, 6f, 8, 12, 40);
        }
    }

    internal void ResetDictionary()
    {
        Toggles = new Dictionary<string, bool>
        {
            ["RaceTraitsEnabled"] = true,
            ["FriendlyRegurgitation"] = true,
            ["Unbirth"] = false,
            ["CockVore"] = false,
            ["CockVoreHidesClothes"] = false,
            ["BreastVore"] = false,
            ["TailVore"] = false,
            ["KuroTenkoEnabled"] = false,
            ["OverhealEXP"] = true,
            ["TransferAllowed"] = true,
            ["CumGestation"] = true,
            ["RagsForSlaves"] = true,
            ["VisibleCorpses"] = true,
            ["EdibleCorpses"] = false,
            ["WeightGain"] = true,
            ["FurryFluff"] = true,
            ["FurryHandsAndFeet"] = true,
            ["FurryGenitals"] = false,
            ["AllowHugeBreasts"] = false,
            ["AllowHugeDicks"] = false,
            ["HairMatchesFur"] = false,
            ["MaleHairForFemales"] = true,
            ["FemaleHairForMales"] = true,
            ["HideBreasts"] = false,
            ["HideCocks"] = false,
            ["LamiaUseTailAsSecondBelly"] = false,
            ["AllowTopless"] = false,
            ["VagrantsEnabled"] = false,
            ["AnimatedBellies"] = true,
            ["DigestionSkulls"] = true,
            ["Bones"] = true,
            ["Scat"] = false,
            ["ScatBones"] = false,
            ["CondomsForCV"] = false,
            ["AutoSurrender"] = false,
            ["EatSurrenderedAllies"] = false,
            ["NewGraphics"] = true,
            ["ErectionsFromVore"] = false,
            ["ErectionsFromCockVore"] = false,
            ["FogOfWar"] = false,
            ["LeadersUseCustomizations"] = false,
            ["HermsCanUB"] = false,
            ["FlatExperience"] = false,
            ["BoostedAccuracy"] = false,
            ["ClothingDiscards"] = true,
            ["HideViperSlit"] = false,
            ["BurpOnDigest"] = false,
            ["FartOnAbsorb"] = false,
            ["StatBoostsAffectMaxHP"] = false,
            ["DayNightEnabled"] = true,
            ["DayNightCosmetic"] = false,
            ["DayNightSchedule"] = true,
            ["DayNightRandom"] = true,
            ["NightMonsters"] = false,
            ["NightMoveMonsters"] = false,
            ["CombatComplicationsEnabled"] = false,
            ["StatCrit"] = false,
            ["StatGraze"] = false,
        };

        foreach (Race race in RaceFuncs.RaceEnumerable())
        {
            Toggles[$"Merc {race}"] = true;
        }
        //Disable any not-implemented races
        //Toggles[$"Merc {Race.Succubi}"] = false;
    }
}
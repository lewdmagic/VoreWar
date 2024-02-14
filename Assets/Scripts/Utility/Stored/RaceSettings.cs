using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;


internal class RaceSettings
{
    [OdinSerialize]
    private Dictionary<Race, RaceSettingsItem> _races;

    public RaceSettings()
    {
        _races = new Dictionary<Race, RaceSettingsItem>();
        _races[Race.FeralBat] = new RaceSettingsItem(Race.FeralBat);
        _races[Race.FeralBat].MaleTraits = new List<TraitType> { TraitType.Small };
    }

    internal void Sanitize()
    {
        foreach (KeyValuePair<Race, RaceSettingsItem> entry in _races)
        {
            if (Equals(entry.Value.ConversionRace, Race.TrueNone)) entry.Value.ConversionRace = RaceParameters.GetRaceTraits(entry.Key).ConversionRace;
            if (Equals(entry.Value.LeaderRace, Race.TrueNone)) entry.Value.LeaderRace = RaceParameters.GetRaceTraits(entry.Key).LeaderRace;
            if (Equals(entry.Value.SpawnRace, Race.TrueNone)) entry.Value.SpawnRace = RaceParameters.GetRaceTraits(entry.Key).SpawnRace;
            if (Equals(entry.Value.ConversionRace, Race.TrueNone) && Equals(entry.Value.LeaderRace, Race.Cat) && Equals(entry.Value.SpawnRace, Race.Cat) && !Equals(entry.Key, Race.Cat))
            {
                entry.Value.ConversionRace = RaceParameters.GetRaceTraits(entry.Key).ConversionRace;
                entry.Value.LeaderRace = RaceParameters.GetRaceTraits(entry.Key).LeaderRace;
                entry.Value.SpawnRace = RaceParameters.GetRaceTraits(entry.Key).SpawnRace;
            }
        }

        foreach (RaceSettingsItem item in _races.Values)
        {
            if (item.Stats.Strength.Roll < 1) item.Stats.Strength.Roll = 1;
            if (item.Stats.Dexterity.Roll < 1) item.Stats.Dexterity.Roll = 1;
            if (item.Stats.Agility.Roll < 1) item.Stats.Agility.Roll = 1;
            if (item.Stats.Endurance.Roll < 1) item.Stats.Endurance.Roll = 1;
            if (item.Stats.Mind.Roll < 1) item.Stats.Mind.Roll = 1;
            if (item.Stats.Will.Roll < 1) item.Stats.Will.Roll = 1;
            if (item.Stats.Stomach.Roll < 1) item.Stats.Stomach.Roll = 1;
            if (item.Stats.Strength.Roll < 1) item.Stats.Strength.Roll = 1;
        }

        if (_races.ContainsKey(Race.FeralBat) == false)
        {
            _races[Race.FeralBat] = new RaceSettingsItem(Race.FeralBat);
            _races[Race.FeralBat].MaleTraits = new List<TraitType> { TraitType.Small };
        }
    }

    internal RaceSettingsItem Get(Race race)
    {
        if (race == null)
        {
            return null;
        }

        if (_races.TryGetValue(race, out RaceSettingsItem item))
        {
            return item;
        }

        _races[race] = new RaceSettingsItem(race);
        return _races[race];
    }

    internal bool Exists(Race race)
    {
        return _races.TryGetValue(race, out RaceSettingsItem item);
    }

    internal bool GetOverrideGender(Race race)
    {
        if (_races.ContainsKey(race)) return Get(race).OverrideGender;
        return false;
    }

    internal bool GetOverrideFurry(Race race)
    {
        if (_races.ContainsKey(race)) return Get(race).OverrideFurry;
        return false;
    }

    internal bool GetOverrideClothed(Race race)
    {
        if (_races.ContainsKey(race)) return Get(race).OverrideClothes;
        return false;
    }

    internal bool GetOverrideWeight(Race race)
    {
        if (_races.ContainsKey(race)) return Get(race).OverrideWeight;
        return false;
    }

    internal bool GetOverrideBreasts(Race race)
    {
        if (_races.ContainsKey(race)) return Get(race).OverrideBoob;
        return false;
    }

    internal bool GetOverrideDick(Race race)
    {
        if (_races.ContainsKey(race)) return Get(race).OverrideDick;
        return false;
    }


    internal int GetBodySize(Race race)
    {
        if (_races.ContainsKey(race)) return Get(race).BodySize;
        return RaceParameters.GetRaceTraits(race).BodySize;
    }

    internal int GetStomachSize(Race race)
    {
        if (_races.ContainsKey(race)) return Get(race).StomachSize;
        return RaceParameters.GetRaceTraits(race).StomachSize;
    }

    internal List<TraitType> GetRaceTraits(Race race)
    {
        if (race == null)
        {
            return null;
        }

        if (_races.ContainsKey(race)) return Get(race).RaceTraits;
        return RaceParameters.GetRaceTraits(race).RacialTraits;
    }

    internal List<TraitType> GetMaleRaceTraits(Race race)
    {
        if (race == null)
        {
            return null;
        }

        if (_races.ContainsKey(race)) return Get(race).MaleTraits;
        return null;
    }

    internal List<TraitType> GetFemaleRaceTraits(Race race)
    {
        if (race == null)
        {
            return null;
        }

        if (_races.ContainsKey(race)) return Get(race).FemaleTraits;
        return null;
    }

    internal List<TraitType> GetHermRaceTraits(Race race)
    {
        if (race == null)
        {
            return null;
        }

        if (_races.ContainsKey(race)) return Get(race).HermTraits;
        return null;
    }

    internal List<TraitType> GetSpawnRaceTraits(Race race)
    {
        if (race == null)
        {
            return null;
        }

        if (_races.ContainsKey(race)) return Get(race).SpawnTraits;
        return RaceParameters.GetRaceTraits(race).SpawnTraits;
    }

    internal List<TraitType> GetLeaderRaceTraits(Race race)
    {
        if (race == null)
        {
            return null;
        }

        if (_races.ContainsKey(race)) return Get(race).LeaderTraits;
        return RaceParameters.GetRaceTraits(race).LeaderTraits;
    }

    internal Stat GetFavoredStat(Race race)
    {
        if (_races.ContainsKey(race))
        {
            var par = Get(race);
            if (par.FavoredStatSet)
            {
                return par.FavoredStat;
            }
        }

        return RaceParameters.GetRaceTraits(race).FavoredStat;
    }

    internal SpellType GetInnateSpell(Race race)
    {
        if (_races.ContainsKey(race))
        {
            return Get(race).InnateSpell;
        }

        return SpellType.None;
    }

    internal RaceAI GetRaceAI(Race race)
    {
        if (_races.ContainsKey(race))
        {
            return Get(race).RaceAI;
        }

        return RaceAI.Standard;
    }

    internal string ListTraits(Race race)
    {
        var tags = RaceParameters.GetRaceTraits(race).RacialTraits;
        if (tags.Count == 0) return "";
        string ret = "";
        for (int i = 0; i < tags.Count; i++)
        {
            ret += tags[i].ToString();
            if (i + 1 < tags.Count) ret += "\n";
        }

        return ret;
    }

    internal List<VoreType> GetVoreTypes(Race race)
    {
        if (_races.ContainsKey(race))
        {
            return Get(race).AllowedVoreTypes;
        }

        return RaceParameters.GetRaceTraits(race).AllowedVoreTypes;
    }

    internal RaceStats GetRaceStats(Race race)
    {
        if (_races.ContainsKey(race)) return Get(race).Stats;
        return RaceParameters.GetRaceTraits(race).RaceStats;
    }

    internal Race GetSpawnRace(Race race)
    {
        Race spawnRace = Race.TrueNone;
        if (_races.ContainsKey(race)) spawnRace = Get(race).SpawnRace;
        if (Equals(spawnRace, Race.TrueNone)) spawnRace = RaceParameters.GetRaceTraits(race).SpawnRace;
        return Equals(spawnRace, Race.TrueNone) ? race : spawnRace;
    }

    internal Race GetConversionRace(Race race)
    {
        Race conversionRace = Race.TrueNone;
        if (_races.ContainsKey(race)) conversionRace = Get(race).ConversionRace;
        if (Equals(conversionRace, Race.TrueNone)) conversionRace = RaceParameters.GetRaceTraits(race).ConversionRace;
        return Equals(conversionRace, Race.TrueNone) ? race : conversionRace;
    }

    internal Race GetLeaderRace(Race race)
    {
        Race leaderRace = Race.TrueNone;
        if (_races.ContainsKey(race)) leaderRace = Get(race).LeaderRace;
        if (Equals(leaderRace, Race.TrueNone)) leaderRace = RaceParameters.GetRaceTraits(race).LeaderRace;
        return Equals(leaderRace, Race.TrueNone) ? race : leaderRace;
    }

    //internal Race GetDisplayedGraphic(Race race)
    //{
    //    if (Races.ContainsKey(race))
    //        return Get(race).DisplayGraphics;
    //    return race;
    //}

    internal void Reset(Race race)
    {
        _races.Remove(race);
        if (Equals(race, Race.FeralBat))
        {
            _races[Race.FeralBat] = new RaceSettingsItem(Race.FeralBat);
            _races[Race.FeralBat].MaleTraits = new List<TraitType> { TraitType.Small };
        }
    }

    internal void ResetAll()
    {
        _races = new Dictionary<Race, RaceSettingsItem>();
        _races[Race.FeralBat] = new RaceSettingsItem(Race.FeralBat);
        _races[Race.FeralBat].MaleTraits = new List<TraitType> { TraitType.Small };
    }
}

internal class RaceSettingsItem
{
    [OdinSerialize]
    private bool _overrideGender;

    internal bool OverrideGender { get => _overrideGender; set => _overrideGender = value; }

    [OdinSerialize]
    private float _maleFraction;

    internal float MaleFraction { get => _maleFraction; set => _maleFraction = value; }

    [OdinSerialize]
    private float _hermFraction;

    internal float HermFraction { get => _hermFraction; set => _hermFraction = value; }

    [OdinSerialize]
    private bool _overrideFurry;

    internal bool OverrideFurry { get => _overrideFurry; set => _overrideFurry = value; }

    [OdinSerialize]
    private float _furryFraction;

    internal float FurryFraction { get => _furryFraction; set => _furryFraction = value; }

    [OdinSerialize]
    private int _bodySize;

    internal int BodySize { get => _bodySize; set => _bodySize = value; }

    [OdinSerialize]
    private int _stomachSize;

    internal int StomachSize { get => _stomachSize; set => _stomachSize = value; }

    [OdinSerialize]
    private List<TraitType> _raceTraits;

    internal List<TraitType> RaceTraits { get => _raceTraits; set => _raceTraits = value; }

    [OdinSerialize]
    private List<VoreType> _allowedVoreTypes;

    internal List<VoreType> AllowedVoreTypes { get => _allowedVoreTypes; set => _allowedVoreTypes = value; }

    [OdinSerialize]
    private Race _spawnRace;

    internal Race SpawnRace { get => _spawnRace; set => _spawnRace = value; }

    [OdinSerialize]
    private Race _conversionRace;

    internal Race ConversionRace { get => _conversionRace; set => _conversionRace = value; }

    [OdinSerialize]
    private Race _leaderRace;

    internal Race LeaderRace { get => _leaderRace; set => _leaderRace = value; }

    [OdinSerialize]
    private RaceStats _stats;

    internal RaceStats Stats { get => _stats; set => _stats = value; }

    [OdinSerialize]
    private bool _overrideClothes;

    internal bool OverrideClothes { get => _overrideClothes; set => _overrideClothes = value; }

    [OdinSerialize]
    private float _clothedFraction;

    internal float ClothedFraction { get => _clothedFraction; set => _clothedFraction = value; }

    [OdinSerialize]
    private int _bannerType;

    internal int BannerType { get => _bannerType; set => _bannerType = value; }

    [OdinSerialize]
    private bool _overrideWeight;

    internal bool OverrideWeight { get => _overrideWeight; set => _overrideWeight = value; }

    [OdinSerialize]
    private int _minWeight;

    internal int MinWeight { get => _minWeight; set => _minWeight = value; }

    [OdinSerialize]
    private int _maxWeight;

    internal int MaxWeight { get => _maxWeight; set => _maxWeight = value; }

    [OdinSerialize]
    private bool _overrideBoob;

    internal bool OverrideBoob { get => _overrideBoob; set => _overrideBoob = value; }

    [OdinSerialize]
    private int _minBoob;

    internal int MinBoob { get => _minBoob; set => _minBoob = value; }

    [OdinSerialize]
    private int _maxBoob;

    internal int MaxBoob { get => _maxBoob; set => _maxBoob = value; }

    [OdinSerialize]
    private bool _overrideDick;

    internal bool OverrideDick { get => _overrideDick; set => _overrideDick = value; }

    [OdinSerialize]
    private int _minDick;

    internal int MinDick { get => _minDick; set => _minDick = value; }

    [OdinSerialize]
    private int _maxDick;

    internal int MaxDick { get => _maxDick; set => _maxDick = value; }

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
    private List<TraitType> _leaderTraits;

    internal List<TraitType> LeaderTraits { get => _leaderTraits; set => _leaderTraits = value; }

    [OdinSerialize]
    private bool _favoredStatSet;

    internal bool FavoredStatSet { get => _favoredStatSet; set => _favoredStatSet = value; }

    [OdinSerialize]
    private Stat _favoredStat;

    internal Stat FavoredStat { get => _favoredStat; set => _favoredStat = value; }

    [OdinSerialize]
    private SpellType _innateSpell;

    internal SpellType InnateSpell { get => _innateSpell; set => _innateSpell = value; }

    [OdinSerialize]
    private RaceAI _raceAI;

    internal RaceAI RaceAI { get => _raceAI; set => _raceAI = value; }

    [OdinSerialize]
    private float _powerAdjustment;

    internal float PowerAdjustment { get => _powerAdjustment; set => _powerAdjustment = value; }


    //[OdinSerialize]
    //internal Race DisplayGraphics;

    public RaceSettingsItem(Race race)
    {
        OverrideGender = false;
        MaleFraction = Config.MaleFraction;
        HermFraction = Config.HermFraction;
        OverrideFurry = false;
        FurryFraction = Config.FurryFraction;

        var racePar = RaceParameters.GetRaceTraits(race);
        var raceData = Races2.GetRace(race);

        BodySize = racePar.BodySize;
        StomachSize = racePar.StomachSize;

        RaceTraits = racePar.RacialTraits.ToList();
        AllowedVoreTypes = racePar.AllowedVoreTypes.ToList();

        SpawnRace = racePar.SpawnRace;
        ConversionRace = racePar.ConversionRace;
        LeaderRace = racePar.ConversionRace;

        var baseStats = racePar.RaceStats;

        Stats = new RaceStats() //Generates a clean copy instead of a reference.  
        {
            Strength = baseStats.Strength,
            Dexterity = baseStats.Dexterity,
            Endurance = baseStats.Endurance,
            Mind = baseStats.Mind,
            Will = baseStats.Will,
            Agility = baseStats.Agility,
            Voracity = baseStats.Voracity,
            Stomach = baseStats.Stomach,
        };

        OverrideClothes = false;
        ClothedFraction = Config.ClothedFraction;

        BannerType = 0;

        OverrideWeight = false;
        OverrideBoob = false;
        OverrideDick = false;

        MinBoob = 0;
        MaxBoob = Math.Max(raceData.SetupOutput.BreastSizes() - 1, 0);

        MinDick = 0;
        MaxDick = Math.Max(raceData.SetupOutput.DickSizes() - 1, 0);

        MinWeight = 0;
        MaxWeight = Math.Max(raceData.SetupOutput.BodySizes - 1, 0);

        MaleTraits = new List<TraitType>();
        FemaleTraits = new List<TraitType>();
        HermTraits = new List<TraitType>();
        SpawnTraits = new List<TraitType>();
        LeaderTraits = new List<TraitType>();

        FavoredStatSet = true;
        FavoredStat = racePar.FavoredStat;

        RaceAI = racePar.RaceAI;

        PowerAdjustment = racePar.PowerAdjustment;

        //DisplayGraphics = race;
    }
}
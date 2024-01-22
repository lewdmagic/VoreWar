using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Linq;


class RaceSettings
{
    [OdinSerialize]
    private Dictionary<Race, RaceSettingsItem> Races;

    public RaceSettings()
    {
        Races = new Dictionary<Race, RaceSettingsItem>();
        Races[Race.FeralBats] = new RaceSettingsItem(Race.FeralBats);
        Races[Race.FeralBats].MaleTraits = new List<TraitType> { TraitType.Small };
    }

    internal void Sanitize()
    {
        foreach (KeyValuePair<Race, RaceSettingsItem> entry in Races)
        {
            if (Equals(entry.Value.ConversionRace, Race.TrueNone)) entry.Value.ConversionRace = RaceParameters.GetRaceTraits(entry.Key).ConversionRace;
            if (Equals(entry.Value.LeaderRace, Race.TrueNone)) entry.Value.LeaderRace = RaceParameters.GetRaceTraits(entry.Key).LeaderRace;
            if (Equals(entry.Value.SpawnRace, Race.TrueNone)) entry.Value.SpawnRace = RaceParameters.GetRaceTraits(entry.Key).SpawnRace;
            if (Equals(entry.Value.ConversionRace, Race.TrueNone) && Equals(entry.Value.LeaderRace, Race.Cats) && Equals(entry.Value.SpawnRace, Race.Cats) && !Equals(entry.Key, Race.Cats))
            {
                entry.Value.ConversionRace = RaceParameters.GetRaceTraits(entry.Key).ConversionRace;
                entry.Value.LeaderRace = RaceParameters.GetRaceTraits(entry.Key).LeaderRace;
                entry.Value.SpawnRace = RaceParameters.GetRaceTraits(entry.Key).SpawnRace;
            }
        }
        foreach (RaceSettingsItem item in Races.Values)
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
        if (Races.ContainsKey(Race.FeralBats) == false)
        {
            Races[Race.FeralBats] = new RaceSettingsItem(Race.FeralBats);
            Races[Race.FeralBats].MaleTraits = new List<TraitType> { TraitType.Small };
        }
    }

    internal RaceSettingsItem Get(Race race)
    {
        if (Races.TryGetValue(race, out RaceSettingsItem item))
        {
            return item;
        }
        Races[race] = new RaceSettingsItem(race);
        return Races[race];
    }

    internal bool Exists(Race race)
    {
        return Races.TryGetValue(race, out RaceSettingsItem item);
    }

    internal bool GetOverrideGender(Race race)
    {
        if (Races.ContainsKey(race))
            return Get(race).OverrideGender;
        return false;
    }

    internal bool GetOverrideFurry(Race race)
    {
        if (Races.ContainsKey(race))
            return Get(race).OverrideFurry;
        return false;
    }

    internal bool GetOverrideClothed(Race race)
    {
        if (Races.ContainsKey(race))
            return Get(race).overrideClothes;
        return false;
    }

    internal bool GetOverrideWeight(Race race)
    {
        if (Races.ContainsKey(race))
            return Get(race).overrideWeight;
        return false;
    }

    internal bool GetOverrideBreasts(Race race)
    {
        if (Races.ContainsKey(race))
            return Get(race).overrideBoob;
        return false;
    }

    internal bool GetOverrideDick(Race race)
    {
        if (Races.ContainsKey(race))
            return Get(race).overrideDick;
        return false;
    }


    internal int GetBodySize(Race race)
    {
        if (Races.ContainsKey(race))
            return Get(race).BodySize;
        return RaceParameters.GetRaceTraits(race).BodySize;
    }

    internal int GetStomachSize(Race race)
    {
        if (Races.ContainsKey(race))
            return Get(race).StomachSize;
        return RaceParameters.GetRaceTraits(race).StomachSize;
    }

    internal List<TraitType> GetRaceTraits(Race race)
    {
        if (race == null)
            return null;
        if (Races.ContainsKey(race))
            return Get(race).RaceTraits;
        return RaceParameters.GetRaceTraits(race).RacialTraits;
    }

    internal List<TraitType> GetMaleRaceTraits(Race race)
    {
        if (Races.ContainsKey(race))
            return Get(race).MaleTraits;
        return null;
    }

    internal List<TraitType> GetFemaleRaceTraits(Race race)
    {
        if (Races.ContainsKey(race))
            return Get(race).FemaleTraits;
        return null;
    }

    internal List<TraitType> GetHermRaceTraits(Race race)
    {
        if (Races.ContainsKey(race))
            return Get(race).HermTraits;
        return null;
    }

    internal List<TraitType> GetSpawnRaceTraits(Race race)
    {
        if (Races.ContainsKey(race))
            return Get(race).SpawnTraits;
        return RaceParameters.GetRaceTraits(race).SpawnTraits;
    }

    internal List<TraitType> GetLeaderRaceTraits(Race race)
    {
        if (Races.ContainsKey(race))
            return Get(race).LeaderTraits;
        return RaceParameters.GetRaceTraits(race).LeaderTraits;
    }

    internal Stat GetFavoredStat(Race race)
    {
        if (Races.ContainsKey(race))
        {
            var par = Get(race);
            if (par.FavoredStatSet)
            {
                return par.FavoredStat;
            }
        }

        return RaceParameters.GetRaceTraits(race).FavoredStat;
    }

    internal SpellTypes GetInnateSpell(Race race)
    {
        if (Races.ContainsKey(race))
        {
            return Get(race).InnateSpell;
        }
        return SpellTypes.None;
    }

    internal RaceAI GetRaceAI(Race race)
    {
        if (Races.ContainsKey(race))
        {
            return Get(race).RaceAI;
        }
        return RaceAI.Standard;
    }

    internal string ListTraits(Race race)
    {
        var Tags = RaceParameters.GetRaceTraits(race).RacialTraits;
        if (Tags.Count == 0)
            return "";
        string ret = "";
        for (int i = 0; i < Tags.Count; i++)
        {
            ret += Tags[i].ToString();
            if (i + 1 < Tags.Count)
                ret += "\n";
        }
        return ret;
    }

    internal List<VoreType> GetVoreTypes(Race race)
    {
        if (Races.ContainsKey(race))
        {
            return Get(race).AllowedVoreTypes;
        }

        return RaceParameters.GetRaceTraits(race).AllowedVoreTypes;
    }

    internal RaceStats GetRaceStats(Race race)
    {
        if (Races.ContainsKey(race))
            return Get(race).Stats;
        return RaceParameters.GetRaceTraits(race).RaceStats;
    }

    internal Race GetSpawnRace(Race race)
    {
        Race spawnRace = Race.TrueNone;
        if (Races.ContainsKey(race))
            spawnRace = Get(race).SpawnRace;
        if (Equals(spawnRace, Race.TrueNone))
            spawnRace = RaceParameters.GetRaceTraits(race).SpawnRace; 
        return (Equals(spawnRace, Race.TrueNone)) ? race : spawnRace;
    }

    internal Race GetConversionRace(Race race)
    {
        Race conversionRace = Race.TrueNone;
        if (Races.ContainsKey(race))
            conversionRace = Get(race).ConversionRace;
        if (Equals(conversionRace, Race.TrueNone))
            conversionRace = RaceParameters.GetRaceTraits(race).ConversionRace; 
        return (Equals(conversionRace, Race.TrueNone)) ? race : conversionRace;
    }

    internal Race GetLeaderRace(Race race)
    {
        Race leaderRace = Race.TrueNone;
        if (Races.ContainsKey(race))
            leaderRace = Get(race).LeaderRace;
        if (Equals(leaderRace, Race.TrueNone))
            leaderRace = RaceParameters.GetRaceTraits(race).LeaderRace; 
        return (Equals(leaderRace, Race.TrueNone)) ? race : leaderRace;
    }

    //internal Race GetDisplayedGraphic(Race race)
    //{
    //    if (Races.ContainsKey(race))
    //        return Get(race).DisplayGraphics;
    //    return race;
    //}

    internal void Reset(Race race)
    {
        Races.Remove(race);
        if (Equals(race, Race.FeralBats))
        {
            Races[Race.FeralBats] = new RaceSettingsItem(Race.FeralBats);
            Races[Race.FeralBats].MaleTraits = new List<TraitType> { TraitType.Small };
        }


    }

    internal void ResetAll()
    {
        Races = new Dictionary<Race, RaceSettingsItem>();
        Races[Race.FeralBats] = new RaceSettingsItem(Race.FeralBats);
        Races[Race.FeralBats].MaleTraits = new List<TraitType> { TraitType.Small };

    }
}

class RaceSettingsItem
{

    [OdinSerialize]
    internal bool OverrideGender;
    [OdinSerialize]
    internal float maleFraction;
    [OdinSerialize]
    internal float hermFraction;
    [OdinSerialize]
    internal bool OverrideFurry;
    [OdinSerialize]
    internal float furryFraction;

    [OdinSerialize]
    internal int BodySize;
    [OdinSerialize]
    internal int StomachSize;
    [OdinSerialize]
    internal List<TraitType> RaceTraits;

    [OdinSerialize]
    internal List<VoreType> AllowedVoreTypes;

    [OdinSerialize]
    internal Race SpawnRace;
    [OdinSerialize]
    internal Race ConversionRace;
    [OdinSerialize]
    internal Race LeaderRace;

    [OdinSerialize]
    internal RaceStats Stats;

    [OdinSerialize]
    internal bool overrideClothes;
    [OdinSerialize]
    internal float clothedFraction;

    [OdinSerialize]
    internal int BannerType;

    [OdinSerialize]
    internal bool overrideWeight;
    [OdinSerialize]
    internal int MinWeight;
    [OdinSerialize]
    internal int MaxWeight;

    [OdinSerialize]
    internal bool overrideBoob;
    [OdinSerialize]
    internal int MinBoob;
    [OdinSerialize]
    internal int MaxBoob;

    [OdinSerialize]
    internal bool overrideDick;
    [OdinSerialize]
    internal int MinDick;
    [OdinSerialize]
    internal int MaxDick;

    [OdinSerialize]
    internal List<TraitType> MaleTraits;
    [OdinSerialize]
    internal List<TraitType> FemaleTraits;
    [OdinSerialize]
    internal List<TraitType> HermTraits;
    [OdinSerialize]
    internal List<TraitType> SpawnTraits;
    [OdinSerialize]
    internal List<TraitType> LeaderTraits;

    [OdinSerialize]
    internal bool FavoredStatSet;
    [OdinSerialize]
    internal Stat FavoredStat;

    [OdinSerialize]
    internal SpellTypes InnateSpell;

    [OdinSerialize]
    internal RaceAI RaceAI;

    [OdinSerialize]
    internal float PowerAdjustment;



    //[OdinSerialize]
    //internal Race DisplayGraphics;

    public RaceSettingsItem(Race race)
    {
        OverrideGender = false;
        maleFraction = Config.MaleFraction;
        hermFraction = Config.HermFraction;
        OverrideFurry = false;
        furryFraction = Config.FurryFraction;

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

        overrideClothes = false;
        clothedFraction = Config.ClothedFraction;

        BannerType = 0;

        overrideWeight = false;
        overrideBoob = false;
        overrideDick = false;

        MinBoob = 0;
        MaxBoob = Math.Max(raceData.MiscRaceData.BreastSizes() - 1, 0);

        MinDick = 0;
        MaxDick = Math.Max(raceData.MiscRaceData.DickSizes() - 1, 0);

        MinWeight = 0;
        MaxWeight = Math.Max(raceData.MiscRaceData.BodySizes - 1, 0);

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


using System.Collections.Generic;

public class RaceTraits
{
    /// <summary>
    /// Controls the size of the body, used for determining how much space they take up in a belly
    /// </summary>
    public int BodySize;

    public RaceAI RaceAI;
    /// <summary>
    /// Controls the base stomach size, at 12 stomach, the capacity will equal this value, and increases or decreases linearly
    /// </summary>
    public int StomachSize;
    public bool HasTail;
    public Stat FavoredStat;
    public List<TraitType> RacialTraits;
    public List<TraitType> LeaderTraits;
    public List<TraitType> SpawnTraits;
    //internal List<Traits> RandomTraits;
    public List<VoreType> AllowedVoreTypes = new List<VoreType> { VoreType.Anal, VoreType.Oral, VoreType.CockVore, VoreType.BreastVore, VoreType.Unbirth };
    public Race SpawnRace = Race.TrueNone;
    public Race ConversionRace = Race.TrueNone;
    public Race LeaderRace = Race.TrueNone;
    public List<SpellTypes> InnateSpells = new List<SpellTypes>();
    public RaceStats RaceStats = new RaceStats();
    /// <summary>
    /// Attacks against this race will have their experience gained modified by this ratio
    /// </summary>
    public float ExpMultiplier = 1f;
    /// <summary>
    /// A straight multiplier to a unit's perceived power
    /// Eventually, this may be incorporated as reading stats instead
    /// </summary>
    public float PowerAdjustment = 1f;
    public bool CanUseRangedWeapons = true;
    public string RaceDescription = "";

}
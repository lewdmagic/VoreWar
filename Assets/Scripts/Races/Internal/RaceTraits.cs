using System.Collections.Generic;

public class RaceTraits : IRaceTraits
{
    /// <summary>
    ///     Controls the size of the body, used for determining how much space they take up in a belly
    /// </summary>
    public int BodySize { get; set; }

    public RaceAI RaceAI { get; set; }

    /// <summary>
    ///     Controls the base stomach size, at 12 stomach, the capacity will equal this value, and increases or decreases
    ///     linearly
    /// </summary>
    public int StomachSize { get; set; }

    public bool HasTail { get; set; }
    public Stat FavoredStat { get; set; }
    public List<TraitType> RacialTraits { get; set; }
    public List<TraitType> LeaderTraits { get; set; }

    public List<TraitType> SpawnTraits { get; set; }

    //internal List<Traits> RandomTraits;
    public List<VoreType> AllowedVoreTypes { get; set; } = new List<VoreType> { VoreType.Anal, VoreType.Oral, VoreType.CockVore, VoreType.BreastVore, VoreType.Unbirth };
    public Race SpawnRace { get; set; } = Race.TrueNone;
    public Race ConversionRace { get; set; } = Race.TrueNone;
    public Race LeaderRace { get; set; } = Race.TrueNone;
    public List<SpellType> InnateSpells { get; set; } = new List<SpellType>();
    public RaceStats RaceStats { get; set; } = new RaceStats();

    /// <summary>
    ///     Attacks against this race will have their experience gained modified by this ratio
    /// </summary>
    public float ExpMultiplier { get; set; } = 1f;

    /// <summary>
    ///     A straight multiplier to a unit's perceived power
    ///     Eventually, this may be incorporated as reading stats instead
    /// </summary>
    public float PowerAdjustment { get; set; } = 1f;

    public bool CanUseRangedWeapons { get; set; } = true;
    public string RaceDescription { get; set; } = "";
}
using System.Collections.Generic;

public interface IRaceTraits
{
    /// <summary>
    /// Controls the size of the body, used for determining how much space they take up in a belly
    /// </summary>
    int BodySize { get; set; }

    RaceAI RaceAI { get; set; }

    /// <summary>
    /// Controls the base stomach size, at 12 stomach, the capacity will equal this value, and increases or decreases linearly
    /// </summary>
    int StomachSize { get; set; }

    bool HasTail { get; set; }
    Stat FavoredStat { get; set; }
    List<TraitType> RacialTraits { get; set; }
    List<TraitType> LeaderTraits { get; set; }
    List<TraitType> SpawnTraits { get; set; }
    List<VoreType> AllowedVoreTypes { get; set; }
    Race SpawnRace { get; set; }
    Race ConversionRace { get; set; }
    Race LeaderRace { get; set; }
    List<SpellType> InnateSpells { get; set; }
    RaceStats RaceStats { get; set; }

    /// <summary>
    /// Attacks against this race will have their experience gained modified by this ratio
    /// </summary>
    float ExpMultiplier { get; set; }

    /// <summary>
    /// A straight multiplier to a unit's perceived power
    /// Eventually, this may be incorporated as reading stats instead
    /// </summary>
    float PowerAdjustment { get; set; }

    bool CanUseRangedWeapons { get; set; }
    string RaceDescription { get; set; }
}
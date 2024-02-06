using System.Collections.Generic;

internal static class RaceParameters
{
    internal static readonly RaceTraits Default = new RaceTraits()
    {
        BodySize = 10,
        StomachSize = 12,
        HasTail = false,
        FavoredStat = Stat.None,
        RacialTraits = new List<TraitType>(),
        RaceDescription = "Whip, whup, brrr. I'm a Default. Phear me!",
    };
    
    /// <summary>
    /// Mostly used as a helper function, for the village race population growth
    /// </summary>
    /// <param name="race"></param>
    /// <returns></returns>
    internal static IRaceTraits GetRaceTraits(Race race)
    {
        if (Config.RaceTraitsEnabled == false)
        {
            return Default;
        }

        return Races2.GetRace(race)?.RaceTraits() ?? Default;
    }

    internal static IRaceTraits GetTraitData(Unit unit)
    {
        return GetRaceTraits(unit.Race);
    }
}
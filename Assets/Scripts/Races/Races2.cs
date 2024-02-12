using System;
using System.Collections.Generic;
using Races.Graphics.Implementations.MainRaces;
using Races.Graphics.Implementations.Mercs;
using Races.Graphics.Implementations.Monsters;
using Races.Graphics.Implementations.UniqueMercs;
using UnityEngine;

static class Races2
{
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
    /// This version can't do the slime queen check, but is fine anywhere else
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


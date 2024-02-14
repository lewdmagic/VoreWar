using System;
using System.Collections.Generic;

public static class RaceAIType
{
    public static Dictionary<RaceAI, Type> Dict = new Dictionary<RaceAI, Type>()
    {
        { RaceAI.Standard, typeof(StandardTacticalAI) },
        { RaceAI.Hedonist, typeof(HedonistTacticalAI) },
        { RaceAI.ServantRace, typeof(RaceServantTacticalAI) },
    };
}
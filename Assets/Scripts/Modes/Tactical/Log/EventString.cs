using System;

internal class EventString
{
    internal Race ActorRace;
    internal Race TargetRace;
    internal Func<EventLog, string> GetString;
    internal Predicate<EventLog> Conditional;
    internal int Priority;

    public EventString(
        Func<EventLog, string> getString,
        int priority = 0,
        Race actorRace = null,
        Race targetRace = null,
        Predicate<EventLog> conditional = null)
    {
        Priority = priority;
        ActorRace = actorRace;
        TargetRace = targetRace;
        GetString = getString;
        if (conditional == null)
            Conditional = (s) => true;
        else
            Conditional = conditional;
    }
}
using OdinSerializer;


class StatusEffect
{
    [OdinSerialize]
    internal StatusEffectType Type;
    [OdinSerialize]
    internal float Strength;
    
    /// <summary>
    /// Used when the status needs to carry a Side parameter.
    /// Current use cases are hypnosis and charm
    /// </summary>
    [OdinSerialize]
    internal Side Side = null;
    [OdinSerialize]
    internal int Duration;

    public StatusEffect(StatusEffectType type, float strength, int duration, Side side = null)
    {
        Type = type;
        Strength = strength;
        Duration = duration;
        Side = side;
    }

}


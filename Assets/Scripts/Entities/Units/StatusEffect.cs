using OdinSerializer;


class StatusEffect
{
    [OdinSerialize]
    private StatusEffectType _type;
    internal StatusEffectType Type { get => _type; set => _type = value; }
    [OdinSerialize]
    private float _strength;
    internal float Strength { get => _strength; set => _strength = value; }
    
    /// <summary>
    /// Used when the status needs to carry a Side parameter.
    /// Current use cases are hypnosis and charm
    /// </summary>
    [OdinSerialize]
    private Side _side = null;
    internal Side Side { get => _side; set => _side = value; }
    [OdinSerialize]
    private int _duration;
    internal int Duration { get => _duration; set => _duration = value; }

    public StatusEffect(StatusEffectType type, float strength, int duration, Side side = null)
    {
        Type = type;
        Strength = strength;
        Duration = duration;
        Side = side;
    }

}


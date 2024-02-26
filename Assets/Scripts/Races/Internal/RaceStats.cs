using OdinSerializer;


public class RaceStats : IRaceStats
{
    [OdinSerialize]
    private StatRange _strength;
    public StatRange Strength { get => _strength; set => _strength = value; }

    [OdinSerialize]
    private StatRange _dexterity;
    public StatRange Dexterity { get => _dexterity; set => _dexterity = value; }

    [OdinSerialize]
    private StatRange _voracity;
    public StatRange Voracity { get => _voracity; set => _voracity = value; }

    [OdinSerialize]
    private StatRange _mind;
    public StatRange Mind { get => _mind; set => _mind = value; }

    [OdinSerialize]
    private StatRange _agility;
    public StatRange Agility { get => _agility; set => _agility = value; }

    [OdinSerialize]
    private StatRange _stomach;
    public StatRange Stomach { get => _stomach; set => _stomach = value; }

    [OdinSerialize]
    private StatRange _endurance;
    public StatRange Endurance { get => _endurance; set => _endurance = value; }

    [OdinSerialize]
    private StatRange _will;
    public StatRange Will { get => _will; set => _will = value; }

    public RaceStats() //Default Stats
    {
        Strength = new StatRange(6, 14);
        Dexterity = new StatRange(6, 14);
        Endurance = new StatRange(8, 13);
        Mind = new StatRange(6, 13);
        Will = new StatRange(6, 13);
        Agility = new StatRange(6, 10);
        Voracity = new StatRange(6, 11);
        Stomach = new StatRange(12, 15);
    }
}

/// <summary>
///     Sets the minimum and maximum stats
/// </summary>
public class StatRange
{
    [OdinSerialize]
    private int _minimum;
    internal int Minimum { get => _minimum; set => _minimum = value; }

    [OdinSerialize]
    private int _roll;
    internal int Roll { get => _roll; set => _roll = value; }

    /// <summary>
    ///     Sets up the stat range
    /// </summary>
    /// <param name="minimum">the lowest the stat will be</param>
    /// <param name="maximum">the highest, inclusive</param>
    public StatRange(int minimum, int maximum)
    {
        Minimum = minimum;
        Roll = 1 + maximum - minimum;
        if (Roll < 1)
        {
            UnityEngine.Debug.LogWarning("Maximum is less than minimum for one of the Stat Ranges");
            Roll = 1;
        }
    }
}
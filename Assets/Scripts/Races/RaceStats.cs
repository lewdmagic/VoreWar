using OdinSerializer;

public class RaceStats
{
    /// <summary>
    ///     Sets the minimum and maximum stats
    /// </summary>
    internal struct StatRange
    {
        [OdinSerialize]
        internal int Minimum;

        [OdinSerialize]
        internal int Roll;

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

        public StatRange(bool junk, int minimum, int roll)
        {
            Minimum = minimum;
            Roll = roll;
        }
    }

    [OdinSerialize]
    internal StatRange Strength;

    [OdinSerialize]
    internal StatRange Dexterity;

    [OdinSerialize]
    internal StatRange Voracity;

    [OdinSerialize]
    internal StatRange Mind;

    [OdinSerialize]
    internal StatRange Agility;

    [OdinSerialize]
    internal StatRange Stomach;

    [OdinSerialize]
    internal StatRange Endurance;

    [OdinSerialize]
    internal StatRange Will;

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
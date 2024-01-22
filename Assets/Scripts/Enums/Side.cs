using System;
using OdinSerializer;

public class Side : IComparable<Side>
{
    [OdinSerialize]
    public readonly string Id;

    internal Side(string id)
    {
        Id = id;
    }
    
    // This is very scuffed because Sides should not have tags in the first place
    // This will need to be eventually removed. 
    public bool HasTag(RaceTag tag)
    {
        if (Race2.RaceSideMap.Reverse.ContainsKey(this))
        {
            return Race2.GetBasic(ToRace())._tags.Contains(tag);
        }
        else
        {
            return false;
        }
    }

    internal Race ToRace()
    {
        if (Race2.RaceSideMap.Reverse.ContainsKey(this))
        {
            return Race2.RaceSideMap.Reverse[this];
        }
        else
        {
            throw new Exception($"cant convert {this.Id} to side");
        }
    }


    public int CompareTo(Side other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (ReferenceEquals(null, other))
        {
            return 1;
        }

        return String.Compare(Id, other.Id, StringComparison.Ordinal);
    }
    
    public override bool Equals(object obj)
    {
        Side other = obj as Side;
        if (other == null)
        {
            return false;
        }
        
        return Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return Id;
    }

    public static Side TrueNoneSide = null;
    public static Side RebelSide = new Side("Rebels");
    public static Side BanditSide = new Side("Bandits");
}



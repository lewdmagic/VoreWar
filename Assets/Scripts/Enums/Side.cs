using System;
using System.Collections.Generic;

public class Side : IComparable<Side>
{
    public readonly string Id;
    private readonly HashSet<RaceTag> _tags = new HashSet<RaceTag>();

    internal Side(string id, RaceTag[] tags)
    {
        Id = id;
        
        if (tags != null)
        {
            foreach (RaceTag tag in tags)
            {
                _tags.Add(tag);
            }
        }
    }
    
    public bool HasTag(RaceTag tag)
    {
        return _tags.Contains(tag);
    }

    internal Race ToRace()
    {
        if (Race.RaceSideMap.Reverse.ContainsKey(this))
        {
            return Race.RaceSideMap.Reverse[this];
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

        return Id.CompareTo(other.Id);
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
        return Id.ToString();
    }
}



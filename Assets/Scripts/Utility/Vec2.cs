using System;
using UnityEngine;

internal struct Vec2 : IComparable<Vec2>, IEquatable<Vec2>
{
    public int X, Y;

    public Vec2(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public static Vec2 operator +(Vec2 a, Vec2 b)
    {
        return new Vec2(a.X + b.X, a.Y + b.Y);
    }

    public static bool operator ==(Vec2 a, Vec2 b)
    {
        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator !=(Vec2 a, Vec2 b)
    {
        return !(a == b);
    }

    public override string ToString()
    {
        return "(" + X + ", " + Y + ")";
    }

    public int CompareTo(Vec2 other)
    {
        if (X == other.X)
        {
            return Y.CompareTo(other.Y);
        }
        else
        {
            return X.CompareTo(other.X);
        }
    }

    public bool Equals(Vec2 other)
    {
        return other == this;
    }

    public override int GetHashCode()
    {
        return (X.GetHashCode()
                ^ (Y.GetHashCode() << 1)) >> 1;
    }

    public override bool Equals(object obj)
    {
        if (obj is Vec2)
        {
            return (Vec2)obj == this;
        }

        return false;
    }

    public static implicit operator Vec2(Vec2I obj)
    {
        if (obj == null)
        {
            Debug.Log("Vec2 Passed null comparison");
            return new Vec2(0, 0);
        }

        return new Vec2(obj.X, obj.Y);
    }
}
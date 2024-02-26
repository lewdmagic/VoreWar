using OdinSerializer;
using System;
using UnityEngine;

public class Vec2I
{
    [OdinSerialize]
    private int _x;

    public int X { get => _x; set => _x = value; }

    [OdinSerialize]
    private int _y;

    public int Y { get => _y; set => _y = value; }

    public Vec2I(int xin, int yin)
    {
        X = xin;
        Y = yin;
    }

    public float GetDistance(Vec2I p)
    {
        float xe = Math.Abs(X - p.X);
        float ye = Math.Abs(Y - p.Y);
        xe = xe * xe;
        ye = ye * ye;

        return (float)Math.Sqrt(xe + ye);
    }

    public int GetNumberOfMovesDistance(Vec2I p) => Math.Max(Math.Abs(p.X - X), Math.Abs(p.Y - Y));

    internal int GetNumberOfMovesDistance(Vec2 p) => Math.Max(Math.Abs(p.X - X), Math.Abs(p.Y - Y));

    public int GetNumberOfMovesDistance(int altX, int altY) => Math.Max(Math.Abs(altX - X), Math.Abs(altY - Y));

    public bool Matches(Vec2I other)
    {
        return X == other.X && Y == other.Y;
    }

    public bool Matches(int otherX, int otherY)
    {
        return X == otherX && Y == otherY;
    }

    public static implicit operator Vector2(Vec2I v)
    {
        return new Vector2(v.X, v.Y);
    }
}
using System;


public class Rand
{
    private Random _rand = new Random();

    internal int Next()
    {
        return _rand.Next();
    }


    internal int Next(int maxValue)
    {
        if (maxValue < 1) maxValue = 1;
        return _rand.Next(maxValue);
    }

    internal int Next(int minValue, int maxValue)
    {
        if (maxValue < minValue) maxValue = minValue;
        if (maxValue < 1) maxValue = 1;
        return _rand.Next(minValue, maxValue);
    }

    internal double NextDouble()
    {
        return _rand.NextDouble();
    }
}
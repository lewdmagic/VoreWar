using System;

public class EnumIndexedArray<TEnum, T> where TEnum : struct, Enum
{
    private readonly T[] _data = new T[Enum.GetNames(typeof(TEnum)).Length];

    /// <summary>
    ///     Returns null if value is not set
    /// </summary>
    /// <param name="index"></param>
    public T this[TEnum index] { get => _data[Convert.ToInt32(index)]; set => _data[Convert.ToInt32(index)] = value; }

    /// <summary>
    ///     Returns null if value is not set
    /// </summary>
    /// <param name="index"></param>
    public T Get(TEnum index)
    {
        return _data[Convert.ToInt32(index)];
    }

    public void Set(TEnum index, T value)
    {
        _data[Convert.ToInt32(index)] = value;
    }
}
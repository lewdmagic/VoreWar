#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

internal class SpriteTypeIndexed<T> : IEnumerable<T>
{
    private T[] _data = new T[Enum.GetNames(typeof(SpriteType)).Length];
    internal IEnumerable<KeyValuePair<SpriteType, T>> KeyValues;

    /// <summary>
    ///     Returns null if value is not set
    /// </summary>
    /// <param name="index"></param>
    internal T this[SpriteType index] { get => _data[(int)index]; set => _data[(int)index] = value; }

    public SpriteTypeIndexed()
    {
        KeyValues = new KvEnumerator<T>(_data);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return Cast<T>(_data.GetEnumerator());
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    // Safe way to cast generic array enumerator to a generic one
    // https://stackoverflow.com/questions/828342/what-is-the-best-way-to-convert-an-ienumerator-to-a-generic-ienumerator
    internal static IEnumerator<TU> Cast<TU>(IEnumerator iterator)
    {
        while (iterator.MoveNext())
        {
            yield return (TU)iterator.Current;
        }
    }


    private class KvEnumerator<TU> : IEnumerable<KeyValuePair<SpriteType, TU>>
    {
        private readonly TU[] _data;

        public KvEnumerator(TU[] data)
        {
            _data = data;
        }

        public IEnumerator<KeyValuePair<SpriteType, TU>> GetEnumerator()
        {
            foreach (SpriteType spriteType in EnumUtil.GetValues<SpriteType>())
            {
                TU value = _data[(int)spriteType];
                if (value != null)
                {
                    yield return new KeyValuePair<SpriteType, TU>(spriteType, value);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
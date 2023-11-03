#region

using System.Collections;
using System.Collections.Generic;

#endregion

public class WrappedList<T> : IWriteOnlyList<T>, IReadOnlyList<T>
{
    internal readonly List<T> List;

    public WrappedList() : this(new List<T>())
    {
    }

    public WrappedList(List<T> list)
    {
        List = list;
    }

    public void Set(IEnumerable<T> values)
    {
        List.Clear();
        List.AddRange(values);
    }

    public void Set(params T[] values)
    {
        List.Clear();
        List.AddRange(values);
    }

    public void Insert(int position, T value)
    {
        List.Insert(position, value);
    }

    public void Add(params T[] values)
    {
        List.AddRange(values);
    }

    public void Add(IEnumerable<T> values)
    {
        List.AddRange(values);
    }

    public void Clear()
    {
        List.Clear();
    }

    public int Count => List.Count;
    
    public IEnumerator<T> GetEnumerator()
    {
        return List.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public T this[int index] => List[index];
}
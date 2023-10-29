#region

using System.Collections.Generic;

#endregion

internal interface IWriteOnlyList<in T>
{
    void Insert(int position, T value);
    void Add(params T[] values);
    void Add(IEnumerable<T> values);
    void Set(params T[] values);
    void Set(IEnumerable<T> values);
    void Clear();
}
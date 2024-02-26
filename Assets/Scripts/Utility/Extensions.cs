using System;
using System.Collections.Generic;

public static class Extensions
{
    public static void AddIgnoreNull<T>(this List<T> list, T item)
    {
        if (item == null) return;
        list.Add(item);
    }

    public static T GetOrElse<T>(this Nullable<T> itself, T defaultValue) where T : struct
    {
        return itself.HasValue ? itself.Value : defaultValue;
    }

    public static int IndexOf<T>(this IReadOnlyList<T> self, T elementToFind)
    {
        int i = 0;
        foreach (T element in self)
        {
            if (Equals(element, elementToFind)) return i;
            i++;
        }

        return -1;
    }

    public static bool HasIndex<T>(this IList<T> self, int index)
    {
        return index < self.Count;
    }

    public static T GetOrAdd<T>(this IList<T> self, int index, Func<T> make)
    {
        if (self.HasIndex(index))
        {
            return self[index];
        }
        else
        {
            T newItem = make();
            self.Add(newItem);
            return newItem;
        }
    }

    public static void Set<T>(this List<T> self, IEnumerable<T> values)
    {
        self.Clear();
        self.AddRange(values);
    }

    public static void Set<T>(this List<T> self, params T[] values)
    {
        self.Clear();
        self.AddRange(values);
    }

    public static T GetOrNull<T>(this IList<T> self, int index) where T : class
    {
        if (self.HasIndex(index))
        {
            return self[index];
        }
        else
        {
            return null;
        }
    }

    public static TV GetOrSet<TK, TV>(this IDictionary<TK, TV> self, TK key, Func<TV> makeSetDefault)
    {
        if (self.TryGetValue(key, out TV value))
        {
            return value;
        }
        else
        {
            TV newValue = makeSetDefault.Invoke();
            self[key] = newValue;
            return newValue;
        }
    }

    public static TV GetOrDefault<TK, TV>(this IDictionary<TK, TV> self, TK key, TV defaultValue)
    {
        if (self.TryGetValue(key, out TV value))
        {
            return value;
        }
        else
        {
            return defaultValue;
        }
    }

    public static TV GetOrNull<TK, TV>(this IDictionary<TK, TV> self, TK key) where TV : class
    {
        if (self.TryGetValue(key, out TV value))
        {
            return value;
        }
        else
        {
            return null;
        }
    }
}
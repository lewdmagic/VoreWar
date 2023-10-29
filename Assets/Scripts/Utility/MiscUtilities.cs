using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class MiscUtilities
{
    /// <summary>
    /// Invokes the specified action after the specified period of time in seconds
    /// </summary>
    public static void DelayedInvoke(Action theDelegate, float time)
    {
        State.GameManager.StartCoroutine(ExecuteAfterTime(theDelegate, time));
    }

    internal static void DelayedInvoke(Action<bool> regenerate, float v)
    {
        State.GameManager.StartCoroutine(ExecuteAfterTime(regenerate, v));
    }

    private static IEnumerator ExecuteAfterTime(Action<bool> action, float v)
    {
        yield return new WaitForSeconds(v);
        try
        {
            action(false);
        }
        catch
        {

        }
    }

    private static IEnumerator ExecuteAfterTime(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        try
        {
            action();
        }
        catch
        {

        }

    }
    
    // https://github.com/dotnet/runtime/issues/24227
    // Extension to reimplement IndexOf for IReadOnlyList 
    // public static int IndexOf<T>(this IReadOnlyList<T> readOnlyList, T element)
    // {
    //     if (readOnlyList is IList<T> list)
    //     {
    //         return list.IndexOf(element);
    //     }
    //
    //     for (int i = 0; i < readOnlyList.Count; i++)
    //     {
    //         if (EqualityComparer<T>.Default.Equals(element, readOnlyList[i]))
    //         {
    //             return i;
    //         }
    //     }
    //
    //     return -1;
    // }
    
    
    // https://stackoverflow.com/questions/12838122/ilistt-and-ireadonlylistt
    // Read only wrapper. Prevents sneaky casting from IReadOnlyList to List. 
    public static IReadOnlyList<T> AsReadOnlyList<T>(this IList<T> list)
    {
        if (list == null) throw new ArgumentNullException(nameof(list));

        return new ReadOnlyWrapper<T>(list);
    }

    private sealed class ReadOnlyWrapper<T> : IReadOnlyList<T>
    {
        private readonly IList<T> _list;
        
        public ReadOnlyWrapper(IList<T> list) => _list = list;

        public int Count => _list.Count;

        public T this[int index] => _list[index];

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    
    
}


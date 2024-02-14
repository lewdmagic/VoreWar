using System;
using System.Collections.Generic;
using System.Linq;

public class EnumUtil
{
    // Get Typed values of an Enum
    // Useful for iteration
    public static IEnumerable<T> GetValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }
}
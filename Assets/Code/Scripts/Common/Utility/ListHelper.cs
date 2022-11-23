using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public static class ListHelper
{
    public static void Resize<T>(this List<T> list, int size, T c)
    {
        int cur = list.Count;
        if (size < cur)
            list.RemoveRange(size, cur - size);
        else if(size > cur)
        {
            if (size > list.Capacity)    //this bit is purely an optimisation, to avoid multiple automatic capacity changes.
                list.Capacity = size;
            list.AddRange(Enumerable.Repeat(c, size - cur));
        }
    }
    public static void Resize<T>(this List<T> list, int size) where T : new()
    {
        Resize(list, size, new T());
    }
}

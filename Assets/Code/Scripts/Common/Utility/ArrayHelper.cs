using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class ArrayHelper
{
    // Removes an element from an array at the given index and returns a copy
    public static T[] RemoveAt<T>(this T[] source, int index)
    {
        #region Original
        /*
        // the destination array, with reduced length to match the element it's removing
        T[] dest = new T[source.Length - 1];

        if (index > 0)
            Array.Copy(source, 0, dest, 0, index);

        if (index < source.Length - 1)
            Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

        return dest;
        */
        #endregion

        // the destination array, with reduced length to match the element it's removing
        T[] dest = new T[source.Length - 1];

        // copy the source array at index+1, to the destination array at index-1 to skip the element
        Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

        return dest;
    }

    // Remove any null elements from _characters and return _characters
    public static T[] RemoveNulls<T>(this T[] source)
    {
        // Loop through the array
        for (int i = 0; i < source.Length; i++)
        {
            // If the element at index 'i' is null, call RemoveAt(i)
            if (source[i] == null)
            {
                source = source.RemoveAt(i);
                // adjust the index, ensuring it does not go below zero
                //i = (i - 1 <= 0) ? 0 : (i--);
                i--;
            }
        }

        // Return the array, now free of null elements
        return source;
    }






}

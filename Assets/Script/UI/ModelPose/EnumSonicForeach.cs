using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class EnumSonicForeach<T> where T : struct, IConvertible
{
    static List<T> Values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
    static int Count = Values.Count;

    public static void Exec(Action<T> action)
    {
        for (int i = 0; i < Count; ++i)
        {
            action((T)Values[i]);
        }
    }
}
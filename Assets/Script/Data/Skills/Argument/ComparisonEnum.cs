using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComparisonEnum
{
    eq, notEq, lessThan, greaterThan, less, greater
}

public static class ComparisonEnumExtention
{
    public static bool Check<T>(this ComparisonEnum comparison, T a, T b) where T : IComparable
    {
        if (comparison == ComparisonEnum.eq) return a.CompareTo(b) == 0;
        if (comparison == ComparisonEnum.notEq) return a.CompareTo(b) != 0;
        if (comparison == ComparisonEnum.lessThan) return a.CompareTo(b) < 0;
        if (comparison == ComparisonEnum.greaterThan) return a.CompareTo(b) > 0;
        if (comparison == ComparisonEnum.less) return a.CompareTo(b) <= 0;
        if (comparison == ComparisonEnum.greater) return a.CompareTo(b) >= 0;
        return false;
    }

    public static string CardText(this ComparisonEnum comparison, string a, string b)
    {
        if (comparison == ComparisonEnum.eq) return a + "と" + b + "が等しい";
        if (comparison == ComparisonEnum.notEq) return a + "と" + b + "が等しくない";
        if (comparison == ComparisonEnum.lessThan) return a + "が" + b + "より小さい";
        if (comparison == ComparisonEnum.greaterThan) return a + "が" + b + "より大きい";
        if (comparison == ComparisonEnum.less) return a + "が" + b + "以下";
        if (comparison == ComparisonEnum.greater) return a + "が" + b + "以上";
        return "(評価しません)";
    }
}

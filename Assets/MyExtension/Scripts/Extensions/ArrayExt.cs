using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ArrayExt
{
    public static T RandomAt<T>(this T[] array) => array[UnityEngine.Random.Range(0, array.Length)];
    public static T RandomAt<T>(this List<T> list) => list[UnityEngine.Random.Range(0, list.Count)];    
    public static T Find<T>(this T[] array, Predicate<T> match) => Array.Find(array, match);
    public static T GetLast<T>(this T[] array, Predicate<T> match) => array[array.Length - 1];
    public static T GetLast<T>(this List<T> array) => array[array.Count - 1];

    public static void Sort<TSource, TResult>(this List<TSource> self, Func<TSource, TResult> selector) where TResult : IComparable
        => self.Sort((x, y) => selector(x).CompareTo(selector(y)));

    public static void SortDescending<TSource, TResult>(this List<TSource> self, Func<TSource, TResult> selector) where TResult : IComparable
        => self.Sort((x, y) => selector(y).CompareTo(selector(x)));

    public static List<GameObject> SortByDistance(this List<GameObject> objects, Vector3 mesureFrom)
    {
        return objects.OrderBy(x => Vector3.Distance(x.transform.position, mesureFrom)).ToList();
    }

}

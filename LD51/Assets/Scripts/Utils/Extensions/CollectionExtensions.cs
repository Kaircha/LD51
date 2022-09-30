using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollectionExtensions {
  public static T GetRandom<T>(this List<T> collection) => collection[Random.Range(0, collection.Count())];
  public static T GetRandom<T>(this T[] collection) => collection[Random.Range(0, collection.Count())];
}
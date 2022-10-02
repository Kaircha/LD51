using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions {
  public static List<Vector3> Cone(this Vector3 vec, int amount, float spread = 60f) {
    if (amount <= 0 || spread <= 0) return null;
    List<Vector3> vecs = new();
    if (amount == 1) {
      vecs.Add(vec);
      return vecs;
    }
    float half = spread / 2;
    for (float i = 0; i < amount; i++) { 
      vecs.Add(vec.Rotated(Mathf.Lerp(-half, half, i / (amount - 1))));
    }
    return vecs;
  }
  public static List<Vector2> Cone(this Vector2 vec, int amount, float spread = 60f) {
    if (amount <= 0 || spread <= 0) return null;
    List<Vector2> vecs = new();
    if (amount == 1) {
      vecs.Add(vec);
      return vecs;
    }
    float half = spread / 2;
    for (float i = 0; i < amount; i++) {
      vecs.Add(vec.Rotated(Mathf.Lerp(-half, half, i / (amount - 1))));
    }
    return vecs;
  }

  public static List<Vector3> Nova(this Vector3 vec, int amount) {
    if (amount <= 0) return null;
    List<Vector3> vecs = new();
    for (int i = 0; i < amount; i++) vecs.Add(vec.Rotated(i * 360f / amount));
    return vecs;
  }
  public static List<Vector2> Nova(this Vector2 vec, int amount) {
    if (amount <= 0) return null;
    List<Vector2> vecs = new();
    for (int i = 0; i < amount; i++) vecs.Add(vec.Rotated(i * 360f / amount));
    return vecs;
  }

  public static Vector3 Snapped(this Vector3 vec) => 
    new(Mathf.Round(vec.x / 0.1f) * 0.1f,
        Mathf.Round(vec.y / 0.1f) * 0.1f,
        Mathf.Round(vec.z / 0.1f) * 0.1f);
  public static Vector2 Snapped(this Vector2 vec) =>
    new(Mathf.Round(vec.x / 0.1f) * 0.1f,
        Mathf.Round(vec.y / 0.1f) * 0.1f);

  public static Vector3 Rotated(this Vector3 vec, float degrees)
    => Quaternion.AngleAxis(degrees, Vector3.back) * vec;
  public static Vector2 Rotated(this Vector2 vec, float degrees)
    => Quaternion.AngleAxis(degrees, Vector3.back) * vec;

  public static Vector3Int ToInt(this Vector3 vec) =>
    new(Mathf.RoundToInt(vec.x), Mathf.RoundToInt(vec.y), Mathf.RoundToInt(vec.z));
  public static Vector2Int ToInt(this Vector2 vec) =>
  new(Mathf.RoundToInt(vec.x), Mathf.RoundToInt(vec.y));
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelAligner : MonoBehaviour {
  public bool SnapToGrid = false;

  public void OnValidate() {
    if (SnapToGrid) {
      Debug.Log($"Snapping the children of {name} to the grid.");
      foreach (Transform child in transform) child.position = child.position.Snapped();
      SnapToGrid = false;
    }
  }
}
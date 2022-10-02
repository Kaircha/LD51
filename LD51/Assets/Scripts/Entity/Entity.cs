using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
  [HideInInspector] public Tile Tile;

  public void EnterTile(Tile tile) {
    if (tile == Tile) return;
    if (Tile != null) Tile.Entity = null;
    Tile = tile;
    Tile.Entity = this;
  }
}
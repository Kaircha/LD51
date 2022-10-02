using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public abstract class Movement : BeatBehaviour {
  public TileGrid TileGrid;
  private Vector3Int Direction;
  [HideInInspector] public Entity Entity;

  private void Awake() => Entity = GetComponent<Entity>();
  private void Start() => StartCoroutine(MovementRoutine());
  public abstract bool TryGetDirection(out Vector3Int dir);
  private IEnumerator MovementRoutine() {
    Move(Vector3Int.zero);
    while (true) {
      while (!IsBeat) {
        yield return null;
      }

      Direction = Vector3Int.zero;
      while (IsBeat) {
        if (TryGetDirection(out Vector3Int dir)) Direction = dir;
        yield return null;
      }
      Move(Direction);
    }
  }

  public bool Move(Vector3Int direction) {
    // Not allowed to move diagonally!
    if (Mathf.Abs(direction.x) + Mathf.Abs(direction.y) > 1) return false;

    Vector3Int cell = TileGrid.Grid.WorldToCell((Vector2)transform.position) + direction;
    if (TileGrid.TryGetTile(new(cell.x, cell.y), out Tile tile) && !tile.IsOccupied) {
      Entity.EnterTile(tile);
      transform.position = tile.transform.position;
      return true;
    } else {
      return false;
    }
  }
}
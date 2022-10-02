using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public abstract class Movement : BeatBehaviour {
  public TileGrid TileGrid;
  private Vector3Int Direction;
  private Vector3Int Position;
  [HideInInspector] public Entity Entity;
  [HideInInspector] public Tile TileTarget;

  private void Awake() => Entity = GetComponent<Entity>();
  public virtual void Start() => StartCoroutine(MovementRoutine());
  public abstract bool TryGetDirection(out Vector3Int dir);
  private IEnumerator MovementRoutine() {
    Position = TileGrid.Grid.WorldToCell((Vector2)transform.position);
    if (TileGrid.TryGetTile(new(Position.x, Position.y), out Tile start)) {
      Move(start);
    }

    while (true) {
      while (!IsBeat) {
        yield return null;
      }

      Direction = Vector3Int.zero;
      while (IsBeat) {
        SpawnManager.Instance.PlayerMoved = false;
        yield return null;
        if (TryGetDirection(out Vector3Int dir)) Direction = dir;
        if (Direction.x != 0 && Direction.y != 0) continue;

        Vector3Int cell = Position + Direction;
        if (TileGrid.TryGetTile(new(cell.x, cell.y), out Tile tile) && !SpawnManager.Instance.Choices.Contains(tile)) {
          TileTarget = tile;
        }
      }
      if (Entity is EnemyEntity) yield return new WaitUntil(() => SpawnManager.Instance.PlayerMoved == true);


      if (Entity is PlayerEntity){
        if (!Move(TileTarget))
          GameManager.Instance.Score -= 5;
        else
          GameManager.Instance.Score += 5;
      }
      GameManager.Instance.UpdateScore();

      if (Entity is PlayerEntity) SpawnManager.Instance.PlayerMoved = true;
    }
  }

  public bool Move(Tile tile) {
    if (tile.IsOccupied) {
      if (tile.Entity == Entity) return false;
      Entity.Attack(tile.Entity);
      return true;
    } else {
      Entity.EnterTile(tile);
      transform.position = tile.transform.position;
      Position = tile.Position;
      return true;
    }
  }
}
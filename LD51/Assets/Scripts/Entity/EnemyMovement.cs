using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement {
  public Entity Target;

  public override bool TryGetDirection(out Vector3Int dir) {
    Vector3Int EntityPos = Entity.Tile.Position;
    Vector3Int TargetPos = Target.Tile.Position;

    int x = 0;
    if (EntityPos.x > TargetPos.x) x = -1;
    if (EntityPos.x < TargetPos.x) x = 1;

    int y = 0;
    if (EntityPos.y > TargetPos.y) y = -1;
    if (EntityPos.y < TargetPos.y) y = 1;

    dir = new(x, y);
    return true;
  }
}
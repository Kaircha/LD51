using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement {
  public override bool TryGetDirection(out Vector3Int dir) {
    dir = new(Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")), Mathf.RoundToInt(Input.GetAxisRaw("Vertical")));
    return dir != Vector3Int.zero;
  }
}
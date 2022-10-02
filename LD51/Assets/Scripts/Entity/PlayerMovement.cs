using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement {
  public override bool TryGetDirection(out Vector3Int dir) {
    float horizontal = -Input.GetAxisRaw("Horizontal");
    float vertical = Input.GetAxisRaw("Vertical");
    dir = new(Mathf.RoundToInt(vertical), Mathf.RoundToInt(horizontal));
    return dir != Vector3Int.zero;
  }
}
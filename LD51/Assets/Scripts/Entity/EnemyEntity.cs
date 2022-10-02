using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity {
  public override void Heal() {
    base.Heal();
  }

  public override void Hurt() {
    base.Hurt();
    GameManager.Instance.Score += 5;
  }
}
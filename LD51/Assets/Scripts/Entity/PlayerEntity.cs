using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity {
  [ContextMenu("Heal")]
  public override void Heal() {
    Health = Mathf.Min(Health + 1, MaxHealth);
    base.Heal();
  }

  [ContextMenu("Hurt")]
  public override void Hurt() {
    Health = Mathf.Max(Health - 1, 0);
    base.Hurt();
    if (Health == 0) GameOver();
  }

  public void GameOver() {
    
  }
}
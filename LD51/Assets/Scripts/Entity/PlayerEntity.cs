using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity {
  public bool IsAttacking => Movement.TileTarget != null && Movement.TileTarget.Entity != null && Movement.TileTarget.Entity != this;

  public override void Attack(Entity entity) {
    base.Attack(entity);
    // Play a random fun sound?
  }

  [ContextMenu("Heal")]
  public override void Heal() {
    Health = Mathf.Min(Health + 1, MaxHealth);
    base.Heal();
  }

  [ContextMenu("Hurt")]
  public override void Hurt() {
    if (IsAttacking) return;
    Health = Mathf.Max(Health - 1, 0);
    base.Hurt();
    if (Health == 0) GameOver();
  }

  public void GameOver() {
    
  }
}
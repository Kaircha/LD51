using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {
  [HideInInspector] public Tile Tile;
  [HideInInspector] public Movement Movement;
  public int Health, MaxHealth;

  public event Action<int> OnHeal;
  public event Action<int> OnHurt;

  public void Init(TileGrid tileGrid) {
    Health = MaxHealth;
    Movement = GetComponent<Movement>();
    Movement.TileGrid = tileGrid;
  }

  public void EnterTile(Tile tile) {
    if (tile == Tile) return;
    if (Tile != null) Tile.Entity = null;
    Tile = tile;
    Tile.Entity = this;
  }

  public virtual void Attack(Entity entity) => entity.Hurt();
  public virtual void Heal() => OnHeal?.Invoke(Health);
  public virtual void Hurt() => OnHurt?.Invoke(Health);
}
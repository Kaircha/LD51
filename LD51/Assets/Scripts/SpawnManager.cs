using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager> {
  [Min(0)] public int SpawnCount;
  public Entity Zombie;
  public TileGrid TileGrid;
  public List<Tile> Choices = new();
  public Entity Player;

  public bool PlayerMoved = false;  

  private void OnEnable() {
    GameManager.Instance.OnInterval += Spawn;
    GameManager.Instance.OnInterval += Choose;
    Player.Init(TileGrid);
  }

  private void OnDisable() {
    GameManager.Instance.OnInterval -= Spawn;
    GameManager.Instance.OnInterval -= Choose;
  }

  public void Choose() => StartCoroutine(ChooseRoutine());
  public IEnumerator ChooseRoutine() {
    // Wait before indicators
    yield return new WaitForSeconds(1f);

    List<Tile> options = TileGrid.Tiles.Where(tile => tile.IsSpawn).Where(tile => !tile.IsOccupied).ToList();
    Choices = new();
    for (int i = 0; i < SpawnCount; i++) {
      if (options.Count == 0) yield break;
      Tile tile = options.GetRandom();
      options.Remove(tile);
      Choices.Add(tile);
    }

    foreach (Tile choice in Choices) {
      choice.Light.enabled = true;
      choice.Light.color = Color.green;
    }
  }

  public void Spawn() {
    foreach (Tile choice in Choices) {
      Entity zombie = Instantiate(Zombie, transform);
      zombie.transform.position = choice.transform.position;
      zombie.Init(TileGrid);
      choice.Light.enabled = false;
      choice.Light.color = Color.white;
    }
  }
}
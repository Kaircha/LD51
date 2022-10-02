using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager> {
  public Entity Zombie;
  public TileGrid TileGrid;
  private List<Tile> Choices = new();

  private void OnEnable() {
    GameManager.Instance.OnInterval += Spawn;
    GameManager.Instance.OnInterval += Choose;
  }

  private void OnDisable() {
    GameManager.Instance.OnInterval -= Spawn;
    GameManager.Instance.OnInterval -= Choose;
  }

  public void Choose() => StartCoroutine(ChooseRoutine());
  public IEnumerator ChooseRoutine() {
    // Wait before indicators
    yield return new WaitForSeconds(1f);

    int toSpawn = 1;
    List<Tile> options = TileGrid.Tiles.Where(tile => tile.IsSpawn).Where(tile => !tile.IsOccupied).ToList();
    Choices = new();
    for (int i = 0; i < toSpawn; i++) {
      if (options.Count == 0) yield break;
      Tile tile = options.GetRandom();
      options.Remove(tile);
      Choices.Add(tile);
    }

    // Display that zombies will spawn here next
  }

  public void Spawn() {
    foreach (Tile choice in Choices) {
      Entity zombie = Instantiate(Zombie, transform);
      zombie.transform.position = choice.transform.position;
      zombie.Init(TileGrid);
    }
  }
}
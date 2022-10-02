using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileGrid : MonoBehaviour {
  public Grid Grid;
  public List<Tile> Tiles = new();
  public Vector2Int Size;
  public Tile Prefab;

  public bool TryGetTile(Vector3Int position, out Tile tile) {
    tile = null;
    foreach (Tile t in Tiles) {
      if (t.Position == position) {
        tile = t;
        return true;
      }
    }
    return false;
  }

  [ContextMenu("Generate")]
  private void GenerateTiles() {
    foreach (Tile tile in Tiles.ToList()) {
      DestroyImmediate(tile.gameObject);
    }
    Tiles = new();
    for (int x = 0; x < Size.x; x++) {
      for (int y = 0; y < Size.y; y++) {
        Tile tile = Instantiate(Prefab, Grid.transform);
        tile.transform.position = Grid.CellToWorld(new(x, y));
        tile.Position = new(x, y);
        tile.name = $"Tile [{x}, {y}]";
        if (x == 0 || x == Size.x - 1 || y == 0 || y == Size.y - 1) {
          tile.Renderer.color = Color.HSVToRGB(0f, 0f, 0.4f);
          tile.Light.enabled = false;
          tile.IsSpawn = true;
        } else { 
          Color color = Color.HSVToRGB(Random.value, 0.5f, 1f);
          tile.Renderer.color = color;
          tile.Light.color = color;
          tile.Light.intensity = Random.Range(0.5f, 2f);
          tile.IsSpawn = false;
        }
        Tiles.Add(tile);
      }
    }
  }
}

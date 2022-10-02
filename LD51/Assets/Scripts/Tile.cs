using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Tile : MonoBehaviour {
  public bool IsOccupied => Entity != null;
  public Entity Entity;
  public Vector3Int Position;
  public SpriteRenderer Renderer;
  public Light2D Light;
}
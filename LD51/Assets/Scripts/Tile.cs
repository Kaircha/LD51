using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Tile : BeatBehaviour {
  public bool IsOccupied => Entity != null;
  [HideInInspector] public bool IsSpawn = false;
  public Entity Entity;
  public Vector3Int Position;
  public SpriteRenderer Renderer;
  public Light2D Light;

  public void OnEnable() => StartCoroutine(TrailRoutine());

  public IEnumerator TrailRoutine() {
    yield return null;
  }
}
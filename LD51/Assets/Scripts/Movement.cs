using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
  public Grid Grid;
  public SpriteRenderer Renderer;
  public BeatMusic Test;
  public bool IsBeat = false;
  private Vector2Int Direction;

  private void OnEnable() {
    AudioManager.Instance.OnBeatStart += BeatStart;
    AudioManager.Instance.OnBeatStop += BeatStop;
  }
  private void OnDisable() {
    AudioManager.Instance.OnBeatStart -= BeatStart;
    AudioManager.Instance.OnBeatStop -= BeatStop;
  }

  public void Start() => StartCoroutine(MovementRoutine());
  public IEnumerator MovementRoutine() {
    yield return new WaitForSeconds(2f);
    AudioManager.Instance.PlayBeatMusic(Test);
    while (true) {
      while (!IsBeat) {
        yield return null;
      }
      Direction = Vector2Int.zero;
      while (IsBeat) {
        Vector2Int direction = new(Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")), Mathf.RoundToInt(Input.GetAxisRaw("Vertical")));
        if (direction != Vector2Int.zero) Direction = direction;
        yield return null;
      }
    }
  }

  public void MoveInDirection(Vector2Int direction){
    Vector3Int cell = Grid.WorldToCell((Vector2)transform.position) + (Vector3Int)direction;
    transform.position = Grid.CellToWorld(cell) + new Vector3(0, .17f, 0);
  }

  public void BeatStart(){
    Renderer.color = Color.red;
    IsBeat = true;
  }

  public void BeatStop() {

    if(Direction == Vector2Int.zero){
      Renderer.color = Color.white;
    }
    else{
      MoveInDirection(Direction);
      Renderer.color = Color.green;
    }
    IsBeat = false;
  }
}
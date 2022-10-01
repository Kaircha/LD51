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
        Vector2Int direction = new(Mathf.RoundToInt(Input.GetAxis("Horizontal")), Mathf.RoundToInt(Input.GetAxis("Vertical")));
        if (direction != Vector2Int.zero) Direction = direction;
        yield return null;
      }
    }
  }

  public void BeatStart() {
    Renderer.color = Color.red;
    IsBeat = true;
  }

  public void BeatStop() {
    Renderer.color = Direction == Vector2Int.zero ? Color.white : Color.green;
    IsBeat = false;
  }
}
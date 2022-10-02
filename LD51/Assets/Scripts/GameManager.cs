using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager> {
  [Min(0)] public int Duration;
  public int Score;
  public TextMeshProUGUI ScoreText;
  public void Interval() => OnInterval?.Invoke();
  public event Action OnInterval;

  public BeatMusic Test;

  public void UpdateScore() => ScoreText.text = Score.ToString();

  public void Start() {
    Score = 0;
    StartCoroutine(IntervalRoutine());
  }

  public IEnumerator IntervalRoutine() {
    // Game start lag desyncs music and beat
    yield return new WaitForSeconds(1f);

    while (true) {
      // Temporary!
      AudioManager.Instance.PlayBeatMusic(Test);
      Score++;


      Interval();
      yield return new WaitForSeconds(Duration);
    }
  }
}
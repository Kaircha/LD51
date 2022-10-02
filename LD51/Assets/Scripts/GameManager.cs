using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : Singleton<GameManager> {
  [Min(0)] public int Duration;
  private int Score;
  public TextMeshProUGUI ScoreText;
  public void Interval() => OnInterval?.Invoke();
  public event Action OnInterval;

  public void UpdateScore(int val){
    Score = Mathf.Max(0, Score + val);
    ScoreText.text = Score.ToString();
    if (val < 0)
      ScoreText.color = Color.red;
    else
      ScoreText.color = Color.white;
  }
  public void Start() {
    Score = 0;
    StartCoroutine(IntervalRoutine());
  }

  public IEnumerator IntervalRoutine() {
    // Game start lag desyncs music and beat
    yield return new WaitForSeconds(1f);

    while (true) {
      Interval();
      yield return new WaitForSeconds(Duration);
    }
  }
}
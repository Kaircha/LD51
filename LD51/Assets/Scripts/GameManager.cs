using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
  [Min(0)] public int Duration;
  public void Interval() => OnInterval?.Invoke();
  public event Action OnInterval;

  public BeatMusic Test;

  public void Start() {
    StartCoroutine(IntervalRoutine());
  }

  public IEnumerator IntervalRoutine() {
    while (true) {
      // Temporary!
      AudioManager.Instance.PlayBeatMusic(Test);


      Interval();
      yield return new WaitForSeconds(Duration);
    }
  }
}
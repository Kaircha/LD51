using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatBehaviour : MonoBehaviour {
  [HideInInspector] public bool IsBeat = false;

  private void OnEnable() {
    AudioManager.Instance.OnBeatStart += OnBeatStart;
    AudioManager.Instance.OnBeatStop += OnBeatStop;
  }
  private void OnDisable() {
    AudioManager.Instance.OnBeatStart -= OnBeatStart;
    AudioManager.Instance.OnBeatStop -= OnBeatStop;
  }

  private void OnBeatStart() {
    IsBeat = true;
    BeatStart();
  }
  private void OnBeatStop() {
    BeatStop();
    IsBeat = false;
  }

  public virtual void BeatStart() { }
  public virtual void BeatStop() { }
}

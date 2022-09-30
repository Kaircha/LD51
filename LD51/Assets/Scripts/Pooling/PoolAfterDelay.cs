using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolAfterDelay : MonoBehaviour, IPoolable {
  public Pool Pool { get; set; }
  public float Delay;

  private void OnEnable() => DelayedPool();
  private void OnDisable() => StopAllCoroutines();

  public void DelayedPool(float delay = 0f) {
    if (delay == 0f) delay = Delay;
    StopAllCoroutines();
    StartCoroutine(PoolRoutine(delay));
  }

  IEnumerator PoolRoutine(float delay) {
    yield return new WaitForSeconds(delay);
    (this as IPoolable).Release(gameObject);
  }
}
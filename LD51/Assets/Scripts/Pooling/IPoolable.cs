using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable {
  public Pool Pool { get; set; }

  public void Release(GameObject gameObject) {
    if (Pool) Pool.Release(gameObject);
    else gameObject.SetActive(false);
  }
}
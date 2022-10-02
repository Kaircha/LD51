using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour {
  public List<GameObject> Hearts = new();
  public Entity Entity;

  private void OnEnable() {
    Entity.OnHeal += GainHealth;
    Entity.OnHurt += LoseHealth;
  }

  private void OnDisable() {
    Entity.OnHeal -= GainHealth;
    Entity.OnHurt -= LoseHealth;
  }

  public void GainHealth(int health) {
    for (int i = 0; i < Hearts.Count; i++) {
      Hearts[i].SetActive(i < health);
    }
  }

  public void LoseHealth(int health) {
    for (int i = 0; i < Hearts.Count; i++) {
      Hearts[i].SetActive(i < health);
    }
  }
}
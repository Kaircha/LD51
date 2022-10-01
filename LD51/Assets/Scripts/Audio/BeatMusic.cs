using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Beat Music", menuName = "Music/Beat")]
public class BeatMusic : ScriptableObject {
  public AudioClip Track;
  public List<float> Beats;
}
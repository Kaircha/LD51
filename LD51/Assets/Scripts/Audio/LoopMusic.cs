using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loop Music", menuName = "Music/Loop")]
public class LoopMusic : ScriptableObject {
  public AudioClip Start;
  public AudioClip Loop;
}
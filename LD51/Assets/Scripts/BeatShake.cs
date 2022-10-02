using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BeatShake : BeatBehaviour {
  public CinemachineImpulseSource Impulse;

  public override void BeatStart() => Impulse.GenerateImpulse();
}
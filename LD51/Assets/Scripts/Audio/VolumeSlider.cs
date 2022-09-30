using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour {
  public string SliderTarget = "MasterVolume";
  
  private void Start() {
    GetComponent<Slider>().value = DataManager.Instance.GetFloat(SliderTarget, 0.5f);
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJManager : Singleton<DJManager> {
  [Header("Audio Clips")]
  public AudioClip Intro;
  public AudioClip Loop;
  public List<AudioClip> Rhythms;
  public List<AudioClip> Fills;

  [Header("Audio Sources")]
  public AudioSource MusicSource;
  public AudioSource BeatSource;

  public void StartMusic() {
    StopAllCoroutines();
    StartCoroutine(MusicRoutine());
  }

  public IEnumerator MusicRoutine() {
    MusicSource.PlayOneShot(Intro);
    yield return new WaitForSeconds(Intro.length);
    MusicSource.clip = Loop;
    MusicSource.Play();

    //yield return new WaitForSeconds(3.5f);

    while (true) {
      AudioClip rhythm = Rhythms.GetRandom();
      BeatSource.clip = rhythm;
      //BeatSource.PlayOneShot(rhythm);
      BeatSource.Play();
      //yield return new WaitForSecondsRealtime(10f);
      yield return new WaitForSecondsRealtime(rhythm.length);
      AudioClip fill = Fills.GetRandom();
      BeatSource.clip = fill;
      //BeatSource.PlayOneShot(fill);
      BeatSource.Play();
      //yield return new WaitForSecondsRealtime(1f);
      yield return new WaitForSecondsRealtime(fill.length);
    }
  }
}
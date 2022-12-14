using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// The AudioManager requires both Singleton and DataManager scripts to function. <br></br> <br></br>
/// 
/// Additionally, the project must be set up with an AudioMixer, containing a Master, Music and SFX Controller. <br></br>
/// Each of which with their volume parameter exposed as MasterVolume, MusicVolume and SFXVolume respectively. <br></br> <br></br>
/// 
/// The volume parameter is in Decibel values, calculated as 20 * Log10(Percent), with Percent being a range of 0.0001f to 1f. <br></br>
/// This range doesn't start at 0 as to not break the Log10 calculation. Optionally, adjust the Sliders' minimum values to 0.0001f.
/// </summary>
public class AudioManager : Singleton<AudioManager> {
  public AudioMixerGroup AudioMixerGroup;
  public AudioSource Active;
  public AudioSource Inactive;
  public const float CrossfadeDuration = 0.5f;
  public float Timeframe = 0.6f;

  public void BeatStart() => OnBeatStart?.Invoke();
  public event Action OnBeatStart;
  public void BeatStop() => OnBeatStop?.Invoke();
  public event Action OnBeatStop;

  // Contains all audio channels in the AudioMixerGroup
  public static readonly string[] Channels = { "MasterVolume", "MusicVolume", "SFXVolume" };

  private void Start() {
    foreach (string channel in Channels) {
      SetVolume(channel, DataManager.Instance.GetFloat(channel, 0.5f));
    }
  }

  public void MasterVolume(float value) => SetVolume("MasterVolume", value);
  public void MusicVolume(float value) => SetVolume("MusicVolume", value);
  public void SFXVolume(float value) => SetVolume("SFXVolume", value);
  public void SetVolume(string channel = "MasterVolume", float value = 1f) {
    value = Mathf.Clamp(value, 0.0001f, 1f);
    DataManager.Instance.SetFloat(channel, value);
    AudioMixerGroup.audioMixer.SetFloat(channel, Mathf.Log10(value) * 20);
  }

  public void PlayLoopMusic(LoopMusic music, bool doCrossfade = true) => PlayLoopMusic(music.Start, music.Loop, doCrossfade);
  public void PlayLoopMusic(AudioClip start, AudioClip loop, bool doCrossfade = true) {
    StopAllCoroutines();
    
    Inactive.clip = loop;
    if (start == null) Inactive.Play();
    else {
      Inactive.PlayOneShot(start);
      Inactive.PlayScheduled(AudioSettings.dspTime + start.length);
    }

    if (doCrossfade) Crossfade();
    else Hardswap();
  }

  public void PlayBeatMusic(BeatMusic music) {
    Inactive.clip = music.Track;
    Inactive.Play();
    Hardswap();
    if (BeatCoroutine != null) StopCoroutine(BeatCoroutine);
    BeatCoroutine = StartCoroutine(BeatRoutine(music.Beats));
  }

  private Coroutine BeatCoroutine;
  public IEnumerator BeatRoutine(List<float> beats) {
    float time = 0f;
    for (int i = 0; i < beats.Count; i++) {
      yield return new WaitForSeconds(beats[i] - time - 0.5f * Timeframe);
      BeatStart();
      yield return new WaitForSeconds(Timeframe);
      BeatStop();
      time = beats[i] + 0.5f * Timeframe;
    }
  }
  
  public void PlayOneShot(AudioClip clip, bool doCrossfade = true) {
    Inactive.clip = null;
    Inactive.PlayOneShot(clip);

    if (doCrossfade) Crossfade();
    else Hardswap();
  }

  private void Crossfade(float duration = CrossfadeDuration) {
    Fadeout(Active, duration);
    Fadein(Inactive, duration);
    (Active, Inactive) = (Inactive, Active);
  }
  private void Hardswap() {
    Active.Stop();
    Active.clip = null;
    Active.volume = 0f;
    Inactive.volume = 1f;
    (Active, Inactive) = (Inactive, Active);
  }


  private Coroutine FadeinCoroutine;
  public void Fadein(AudioSource audio, float duration = CrossfadeDuration) {
    if (FadeinCoroutine != null) StopCoroutine(FadeinCoroutine);
    FadeinCoroutine = StartCoroutine(FadeinRoutine(audio, duration));
  }
  private IEnumerator FadeinRoutine(AudioSource audio, float duration = CrossfadeDuration) {
    if (duration <= 0f) duration = CrossfadeDuration;
    float timer = 0f;
    float volume = audio.volume;
    while (timer < 1f) {
      audio.volume = Mathf.Lerp(volume, 1f, timer);
      timer += Time.deltaTime / duration;
      yield return null;
    }
    audio.volume = 1f;
  }
  
  private Coroutine FadeoutCoroutine;
  public void Fadeout(AudioSource audio, float duration = CrossfadeDuration) {
    if (FadeoutCoroutine != null) StopCoroutine(FadeoutCoroutine);
    FadeoutCoroutine = StartCoroutine(FadeoutRoutine(audio, duration));
  }
  private IEnumerator FadeoutRoutine(AudioSource audio, float duration = CrossfadeDuration) {
    if (duration <= 0f) duration = CrossfadeDuration;
    float timer = 0f;
    float volume = audio.volume;
    while (timer < 1f) {
      audio.volume = Mathf.Lerp(volume, 0f, timer);
      timer += Time.deltaTime / duration;
      yield return null;
    }
    audio.volume = 0f;
    audio.Stop();
    audio.clip = null;
  }
}
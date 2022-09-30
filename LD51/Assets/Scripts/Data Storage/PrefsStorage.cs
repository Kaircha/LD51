using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsStorage : Storage {
  public override void Save() => PlayerPrefs.Save();
  public override void Load() { }
  public override void Delete() => PlayerPrefs.DeleteAll();
  public override bool Has(string key) => PlayerPrefs.HasKey(key);
  /* 
  public override T Get<T>(string key) {
    if (typeof(T) == typeof(int)) return (T)(object)PlayerPrefs.GetInt(key);
    if (typeof(T) == typeof(bool)) return (T)(object)(PlayerPrefs.GetInt(key) == 1);
    if (typeof(T) == typeof(float)) return (T)(object)PlayerPrefs.GetFloat(key);
    if (typeof(T) == typeof(string)) return (T)(object)PlayerPrefs.GetString(key);
    return default;
  }
  public override bool TryGet<T>(string key, out T value) {
    if (Has(key)) {
      value = Get<T>(key);
      return true;
    } else {
      value = default;
      return false;
    }
  }
  public override void Set<T>(string id, T value) {
    if (typeof(T) == typeof(int)) PlayerPrefs.SetInt(id, (int)(object)value);
    if (typeof(T) == typeof(bool)) PlayerPrefs.SetInt(id, (bool)(object)value ? 1 : 0);
    if (typeof(T) == typeof(float)) PlayerPrefs.SetFloat(id, (float)(object)value);
    if (typeof(T) == typeof(string)) PlayerPrefs.SetString(id, (string)(object)value);
  } 
  */

  public override int GetInt(string key, int fallback) => PlayerPrefs.GetInt(key, fallback);
  public override bool GetBool(string key, bool fallback) => PlayerPrefs.GetInt(key, fallback ? 1 : 0) == 1;
  public override float GetFloat(string key, float fallback) => PlayerPrefs.GetFloat(key, fallback);
  public override string GetString(string key, string fallback) => PlayerPrefs.GetString(key, fallback);
  public override Vector2 GetVector2(string key, Vector2 fallback) => 
    new(PlayerPrefs.GetFloat($"{key}_x", fallback.x), 
        PlayerPrefs.GetFloat($"{key}_y", fallback.y));
  public override Vector3 GetVector3(string key, Vector3 fallback) => 
    new(PlayerPrefs.GetFloat($"{key}_x", fallback.x), 
        PlayerPrefs.GetFloat($"{key}_y", fallback.y), 
        PlayerPrefs.GetFloat($"{key}_z", fallback.z));

  public override void SetInt(string key, int value) => PlayerPrefs.SetInt(key, value);
  public override void SetBool(string key, bool value) => PlayerPrefs.SetInt(key, value ? 1 : 0);
  public override void SetFloat(string key, float value) => PlayerPrefs.SetFloat(key, value);
  public override void SetString(string key, string value) => PlayerPrefs.SetString(key, value);
  public override void SetVector2(string key, Vector2 value) {
    PlayerPrefs.SetFloat($"{key}_x", value.x);
    PlayerPrefs.SetFloat($"{key}_y", value.y);
  }
  public override void SetVector3(string key, Vector3 value) {
    PlayerPrefs.SetFloat($"{key}_x", value.x);
    PlayerPrefs.SetFloat($"{key}_y", value.y);
    PlayerPrefs.SetFloat($"{key}_z", value.z);
  }
}
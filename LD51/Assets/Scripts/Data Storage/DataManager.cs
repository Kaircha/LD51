using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : Singleton<DataManager> {
  public GameObject Icon;
  private Storage Storage;

  public override void Awake() {
    base.Awake();
    Storage = new PrefsStorage();
    Load();
  }

  void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
  void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

  void OnSceneLoaded(Scene scene, LoadSceneMode mode) => Save();
  void OnApplicationQuit() => Save();

  [ContextMenu("Save")] public void Save() => Storage.Save();
  [ContextMenu("Load")] public void Load() => Storage.Load();
  [ContextMenu("Delete")] public void Delete() => Storage.Delete();

  // public bool Has(string key) => Storage.Has(key);
  // public T Get<T>(string key) => Storage.Get<T>(key);
  // public bool TryGet<T>(string key, out T value) => Storage.TryGet<T>(key, out value);
  // public void Set<T>(string id, T value) => Storage.Set(id, value);

  public int GetInt(string key, int fallback = 0) => Storage.GetInt(key, fallback);
  public bool GetBool(string key, bool fallback = false) => Storage.GetBool(key, fallback);
  public float GetFloat(string key, float fallback = 0f) => Storage.GetFloat(key, fallback);
  public string GetString(string key, string fallback = "") => Storage.GetString(key, fallback);
  public Vector2 GetVector2(string key, Vector2 fallback = new()) => Storage.GetVector2(key, fallback);
  public Vector3 GetVector3(string key, Vector3 fallback = new()) => Storage.GetVector3(key, fallback);

  public void SetInt(string key, int value) => Storage.SetInt(key, value);
  public void SetBool(string key, bool value) => Storage.SetBool(key, value);
  public void SetFloat(string key, float value) => Storage.SetFloat(key, value);
  public void SetString(string key, string value) => Storage.SetString(key, value);
  public void SetVector2(string key, Vector2 value) => Storage.SetVector2(key, value);
  public void SetVector3(string key, Vector3 value) => Storage.SetVector3(key, value);
}
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;

public class XMLStorage : Storage {
  public Dictionary<string, dynamic> Data;
  private string DataPath => Path.Combine(Application.persistentDataPath, "Save.xml");

  public override void Save() => Save(DataPath);
  private void Save(string path) {
    List<KeyValue<string, dynamic>> data = Data.Select(x => new KeyValue<string, dynamic> { Key = x.Key, Value = x.Value }).ToList();
    XmlSerializer serializer = new(typeof(List<KeyValue<string, dynamic>>));
    StreamWriter writer = new(path);
    serializer.Serialize(writer.BaseStream, data);
    writer.Close();
  }
  public override void Load() => Load(DataPath);
  private void Load(string path) {
    if (!File.Exists(path)) return;
    XmlSerializer serializer = new(typeof(List<KeyValue<string, dynamic>>));
    StreamReader reader = new(path);
    Data = ((List<KeyValue<string, dynamic>>)serializer.Deserialize(reader.BaseStream)).ToDictionary(x => x.Key, x => x.Value);
    reader.Close();
  }
  public override void Delete() => Data = new Dictionary<string, dynamic>();
  public override bool Has(string key) => Data.ContainsKey(key);

  public override int GetInt(string key, int fallback) => SafeGet(key, fallback);
  public override bool GetBool(string key, bool fallback) => SafeGet(key, fallback);
  public override float GetFloat(string key, float fallback) => SafeGet(key, fallback);
  public override string GetString(string key, string fallback) => SafeGet(key, fallback);
  public override Vector2 GetVector2(string key, Vector2 fallback) => SafeGet(key, fallback);
  public override Vector3 GetVector3(string key, Vector3 fallback) => SafeGet(key, fallback);

  public override void SetInt(string key, int value) => Data[key] = value;
  public override void SetBool(string key, bool value) => Data[key] = value;
  public override void SetFloat(string key, float value) => Data[key] = value;
  public override void SetString(string key, string value) => Data[key] = value;
  public override void SetVector2(string key, Vector2 value) => Data[key] = value;
  public override void SetVector3(string key, Vector3 value) => Data[key] = value;

  /*
public override T Get<T>(string key) => Data.TryGetValue(key, out dynamic value) ? value : default(T);
public override bool TryGet<T>(string key, out T value) {
 if (Data.TryGetValue(key, out dynamic v)) {
   value = v;
   return true;
 } else {
   value = default;
   return false;
 }
}
public override void Set<T>(string id, T value) => Data[id] = value;
*/

  private T SafeGet<T>(string key, T fallback) {
    if (Data.TryGetValue(key, out dynamic data) && data == typeof(T)) return data;
    return fallback;
  }
}

[XmlType(TypeName = "Entry")]
public class KeyValue<K, V> {
  public K Key { get; set; }
  public V Value { get; set; }
}
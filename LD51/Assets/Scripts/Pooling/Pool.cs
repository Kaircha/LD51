using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour {
  public int Minimum = 10;
  public int Maximum = 10000;
  public GameObject Prefab;
  public ObjectPool<GameObject> Objects;

  public GameObject Get() => Objects.Get();
  public bool TryGet<T>(out T type) {
    if (Get().TryGetComponent(out T component)) {
      type = component;
      return true;
    } else {
      type = default;
      return false;
    }
  }

  public void Release(GameObject gameObject) {
    if (gameObject.activeSelf) Objects.Release(gameObject);
  }

  public void Awake() => Objects = new(ObjectCreated, ObjectGotten, ObjectReleased, ObjectDestroyed, true, Minimum, Maximum);

  GameObject ObjectCreated() { 
    GameObject clone = Instantiate(Prefab, transform);

    var poolables = clone.GetComponents<IPoolable>();
    if (poolables.Length > 0) {
      foreach (IPoolable poolable in clone.GetComponents<IPoolable>()) {
        poolable.Pool = this;
      }
    } else {
      Debug.LogWarning($"{clone.name} is not an IPoolable!");
    }

    return clone;
  }
  void ObjectGotten(GameObject gameObject) => gameObject.SetActive(true);
  void ObjectReleased(GameObject gameObject) => gameObject.SetActive(false);
  void ObjectDestroyed(GameObject gameObject) => Destroy(gameObject);
}
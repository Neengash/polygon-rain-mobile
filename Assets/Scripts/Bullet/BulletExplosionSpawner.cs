using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosionSpawner : MonoBehaviour
{
    public static BulletExplosionSpawner singleton;

    ObjectPool bulletExplosionPool;

    private void Awake() {
        if (singleton == null) {
            singleton = this;
        } else {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        // No DontDestroyOnLoad because we don't want
        // the gameObject to persist through scenes
    }

    void Start() {
        bulletExplosionPool = GetComponent<ObjectPool>();
    }

    public BulletExplosion getExplosion() {
        return (BulletExplosion)bulletExplosionPool.getNext();
    }

}

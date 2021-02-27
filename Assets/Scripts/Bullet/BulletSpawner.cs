using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public static BulletSpawner singleton;

    ObjectPool bulletPool;

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
        bulletPool = GetComponent<ObjectPool>();
    }

    public BulletController getBullet() {
        return (BulletController)bulletPool.getNext();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    public static DropSpawner singleton;

    ObjectPool dropPool;

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
        dropPool = GetComponent<ObjectPool>();
    }

    public Drop getRandomDrop() {
        Drop drop = (Drop)dropPool.getNext();
        drop.SetType(Random.Range(0, 3));
        return drop;
    }
}

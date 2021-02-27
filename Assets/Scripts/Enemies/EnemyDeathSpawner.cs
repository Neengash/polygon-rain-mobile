using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathSpawner : MonoBehaviour
{
    public static EnemyDeathSpawner singleton;

    [SerializeField] ObjectPool hexDeathPool;
    [SerializeField] ObjectPool pentaDeathPool;
    [SerializeField] ObjectPool tetraDeathPool;
    [SerializeField] ObjectPool triDeathPool;

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

    public EnemyDeath getHexDeath() {
        return (EnemyDeath)hexDeathPool.getNext();
    }

    public EnemyDeath getPentaDeath() {
        return (EnemyDeath)pentaDeathPool.getNext();
    }

    public EnemyDeath getTetraDeath() {
        return (EnemyDeath)tetraDeathPool.getNext();
    }

    public EnemyDeath getTriDeath() {
        return (EnemyDeath)triDeathPool.getNext();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner singleton;

    [SerializeField] ObjectPool hexEnemyPool;
    [SerializeField] ObjectPool pentaEnemyPool;
    [SerializeField] ObjectPool tetraEnemyPool;
    [SerializeField] ObjectPool triEnemyPool;

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


    public HexEnemy getHexEnemy() {
        return (HexEnemy)hexEnemyPool.getNext();
    }

    public PentaEnemy getPentaEnemy() {
        return (PentaEnemy)pentaEnemyPool.getNext();
    }

    public TetraEnemy getTetraEnemy() {
        return (TetraEnemy)tetraEnemyPool.getNext();
    }

    public TriEnemy getTriEnemy() {
        return (TriEnemy)triEnemyPool.getNext();
    }
}

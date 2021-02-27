using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSpawner : MonoBehaviour
{
    public static ScoreSpawner singleton;

    ObjectPool scorePool;

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
        scorePool = GetComponent<ObjectPool>();
    }

    public ScoreText getScoreText() {
        return (ScoreText)scorePool.getNext();
    }
}

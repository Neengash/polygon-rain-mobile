using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager singleton;

    float nextSpawnTimer;

    const float
        FIRST_TIMER = 3f,
        BASE_TIMER = 10f,
        BASE_MARGIN = 0.5f,
        MIN_TIME = 5f,
        MIN_MARGIN = 0.5f;

    const int
        HEX_RATIO = 60,
        PENTA_RATIO = 25,
        TETRA_RATIO = 10,
        TRI_RATIO = 5;

    private const float
        MIN_X = -7.5f,
        MAX_X = 7.5f,
        MIN_Y = 0.3f,
        MAX_Y = 2.3f;

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
        beginGame();
    }

    void Update()
    {
        if (GameManager.singleton.isPlaying()) {
            nextSpawnTimer -= Time.deltaTime;
            if (nextSpawnTimer < 0) {
                spawnEnemy();
                resetSpawnTimer();
            }
        }
    }

    public void beginGame() {
        nextSpawnTimer = FIRST_TIMER;
    }

    void spawnEnemy() {
        EnemyBase enemy = getEnemy();
        enemy.transform.position = new Vector3(
            Random.Range(MIN_X, MAX_X),
            Random.Range(MIN_Y, MAX_Y),
            enemy.transform.position.z);
        enemy.transform.rotation = Quaternion.identity;
        enemy.gameObject.SetActive(true);
        enemy.spawn();
    }

    EnemyBase getEnemy()
    {
        int random = Random.Range(0, 100) + 1;

        if (random <= HEX_RATIO) {
            return EnemySpawner.singleton.getHexEnemy();
        } else if (random <= HEX_RATIO + PENTA_RATIO) {
            return EnemySpawner.singleton.getPentaEnemy();
        } else if (random <= HEX_RATIO + PENTA_RATIO + TETRA_RATIO) {
            return EnemySpawner.singleton.getTetraEnemy();
        } else {
            return EnemySpawner.singleton.getTriEnemy();
        }
    }

    void resetSpawnTimer() {
        float time = BASE_TIMER;
        float margin = BASE_MARGIN;
        float score = ScoreManager.singleton.currentScore;

        time -= (score / 20 * 0.5f);
        if (time < MIN_TIME) { time = MIN_TIME; }

        margin -= (score / 20 * 0.05f);
        if (margin < MIN_MARGIN) { margin = MIN_MARGIN; }

        nextSpawnTimer = Random.Range(time - margin, time + margin);
    }
}

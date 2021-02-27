using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager singleton;

    public int currentScore { get; protected set; }

    const string SCORE_KEY = "score";

    private void Awake() {
        if (singleton == null) {
            singleton = this;
        } else {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (SceneManager.GetActiveScene().buildIndex == Scenes.MAIN_GAME) {
            if (isNewRecord()) {
                saveHighScore();
            }
            currentScore = 0;
            CurrentScore.singleton.UpdateScore();
        }
    }

    public void incrementScore(int value) {
        currentScore += value;
        CurrentScore.singleton.UpdateScore();
    }

    public int getHighScore() {
        return PlayerPrefs.GetInt(SCORE_KEY, 0);
    }

    public void saveHighScore() {
        PlayerPrefs.SetInt(SCORE_KEY, currentScore);
    }

    public bool isNewRecord() {
        return currentScore > getHighScore();
    }
}

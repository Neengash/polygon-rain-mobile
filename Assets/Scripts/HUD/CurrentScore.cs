using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentScore : MonoBehaviour
{
    public static CurrentScore singleton;

    [SerializeField] TextMeshProUGUI text;
    const int MAX_LENGTH = 5;

    private void Awake() {
        if (singleton == null) {
            singleton = this;
        } else {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        // No DontDestroyOnLoad since we dont want
        // the object to persist through scenes
    }

    void Start()
    {
        renderScore(ScoreManager.singleton.currentScore);
    }

    public void UpdateScore() {
        renderScore(ScoreManager.singleton.currentScore);
    }

    private void renderScore(int highScore) {
        string strHighscore = highScore.ToString();
        int increment = MAX_LENGTH - strHighscore.Length;

        while(increment-- > 0) {
            strHighscore = "0" + strHighscore;
        }

        text.text = strHighscore;
    }
}

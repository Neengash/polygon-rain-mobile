using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MaxScore : MonoBehaviour
{
    TextMeshProUGUI text;
    const int MAX_LENGTH = 5;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        renderScore(ScoreManager.singleton.getHighScore());
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

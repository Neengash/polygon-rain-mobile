using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    int GameState;

    public const int
        STATE_PLAY = 0,
        STATE_PAUSE = 1,
        STATE_GAME_OVER = 2;

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
        GameState = STATE_PLAY;
    }

    public void pauseGame() { GameState = STATE_PAUSE; }

    public void gameOver() { GameState = STATE_GAME_OVER; }

    public void resumeGame() { GameState = STATE_PLAY; }

    public bool isPlaying() { return GameState == STATE_PLAY; }

    public bool isPaused() { return GameState == STATE_PAUSE; }

    public bool isGameOver() { return GameState == STATE_GAME_OVER; }


}

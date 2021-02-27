using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager singleton;

    [SerializeField] GameObject PauseCanvas;

    float time;
    const float STOP = 0f;

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
        time = Time.timeScale;
    }

    void Update() {
        if (GameManager.singleton.isPlaying() && Input.GetKeyDown(KeyCode.Escape)) {
            pauseGame();
        } else if(GameManager.singleton.isPaused() && Input.GetKeyDown(KeyCode.Escape)) {
            resumeGame();
        }
    }

    void pauseGame()
    {
        PauseCanvas.SetActive(true);
        GameManager.singleton.pauseGame();
        time = Time.timeScale;
        Time.timeScale = STOP;
    }

    public void resumeGame() {
        PauseCanvas.SetActive(false);
        GameManager.singleton.resumeGame();
        Time.timeScale = time;
    }

    public void stopPause() { //used when exit game to menu from pause
        GameManager.singleton.resumeGame();
        Time.timeScale = time;
    }

    private void OnDestroy() {
        Time.timeScale = time;
    }
}

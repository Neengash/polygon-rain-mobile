using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    const int
        START_GAME = 0,
        GAME_INSTRUCTION = 1,
        EXIT_GAME = 2;

    [SerializeField] RectTransform[] options;
    [SerializeField] RectTransform selector;
    [SerializeField] AudioClip okSound;
    [SerializeField] AudioClip changeSelectionSound;

    AudioSource audioSource;

    int idx;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        idx = START_GAME;
        updateSelector();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.J)) {
            audioSource.PlayOneShot(okSound);
            selectOption();
        } else if (Input.GetKeyDown(KeyCode.W)) {
            audioSource.PlayOneShot(changeSelectionSound);
            idx--;
            updateSelector();
        } else if (Input.GetKeyDown(KeyCode.S)) {
            audioSource.PlayOneShot(changeSelectionSound);
            idx++;
            updateSelector();
        }
    }

    protected void updateSelector() {
        if (idx > options.Length-1) { idx = 0; }
        if (idx < 0) { idx = options.Length - 1; }

        selector.anchoredPosition = new Vector2(
            selector.anchoredPosition.x,
            options[idx].anchoredPosition.y
            );
    }

    protected void selectOption() {
        switch (idx) {
            case START_GAME:
                SceneTransitioner.singleton.gotoScene(Scenes.MAIN_GAME);
                break;
            case GAME_INSTRUCTION:
                SceneTransitioner.singleton.gotoScene(Scenes.INSTRUCTIONS);
                break;
            case EXIT_GAME:
                SceneTransitioner.singleton.exitGame();
                break;
        }
    }
}

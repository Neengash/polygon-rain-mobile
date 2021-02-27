using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    const int
        PLAY_AGAIN = 0,
        MAIN_MENU = 1;

    [SerializeField] RectTransform[] options;
    [SerializeField] RectTransform selector;
    [SerializeField] AudioClip okSound;
    [SerializeField] AudioClip changeSelectionSound;

    AudioSource audioSource;

    int idx;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        idx = PLAY_AGAIN;
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
            case PLAY_AGAIN:
                SceneTransitioner.singleton.gotoScene(Scenes.MAIN_GAME);
                break;
            case MAIN_MENU:
                SceneTransitioner.singleton.gotoScene(Scenes.MAIN_MENU);
                break;
        }
    }
}

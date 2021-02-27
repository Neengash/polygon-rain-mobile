using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelController : MonoBehaviour
{
    [SerializeField] RectTransform[] options;
    [SerializeField] RectTransform selector;

    [SerializeField] AudioClip okSound;
    [SerializeField] AudioClip changeSound;
    AudioSource audioSource;

    int idx = 0;

    const int
        IDX_RESUME = 0,
        IDX_BACK_MENU = 1;

    void Start() {
        audioSource = FindObjectOfType<AudioSource>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.W)) { 
            idx++;
            audioSource.PlayOneShot(changeSound);
        }
        if (Input.GetKeyDown(KeyCode.S)) { 
            idx--; 
            audioSource.PlayOneShot(changeSound);
        }

        if (idx > options.Length -1) { idx = 0; }
        if (idx < 0) { idx = options.Length - 1; }

        selector.anchoredPosition = new Vector2(
            selector.anchoredPosition.x,
            options[idx].anchoredPosition.y);

        if (Input.GetKeyDown(KeyCode.J)) {
            audioSource.PlayOneShot(okSound);
            if (idx == IDX_RESUME) {
                PauseManager.singleton.resumeGame();
            } else {
                PauseManager.singleton.stopPause();
                SceneTransitioner.singleton.gotoScene(Scenes.MAIN_MENU);
            }
        }
    }
}

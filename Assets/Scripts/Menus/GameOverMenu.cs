using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] AudioClip okSound;
    AudioSource audioSource;
    bool locked = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAgainOption() {
        if (!locked) {
            locked = true;
            audioSource.PlayOneShot(okSound);
            SceneTransitioner.singleton.gotoScene(Scenes.MAIN_GAME);
        }
    }

    public void MainMenuOption() {
        if (!locked) {
            locked = true;
            audioSource.PlayOneShot(okSound);
            SceneTransitioner.singleton.gotoScene(Scenes.MAIN_MENU);
        }
    }
}

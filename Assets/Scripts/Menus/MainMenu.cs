using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioClip okSound;
    AudioSource audioSource;
    bool locked = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    public void StartGameOption() {
        if (!locked) {
            locked = true;
            audioSource.PlayOneShot(okSound);
            SceneTransitioner.singleton.gotoScene(Scenes.MAIN_GAME);
        }
    }

    public void InstructionsOption() {
        if (!locked) {
            locked = true;
            audioSource.PlayOneShot(okSound);
            SceneTransitioner.singleton.gotoScene(Scenes.INSTRUCTIONS);
        }
    }

    public void ExitGameOption() {
        if (!locked) {
            locked = true;
            audioSource.PlayOneShot(okSound);
            SceneTransitioner.singleton.exitGame();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioClip okSound;
    AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    public void StartGameOption() {
        audioSource.PlayOneShot(okSound);
        SceneTransitioner.singleton.gotoScene(Scenes.MAIN_GAME);
    }

    public void InstructionsOption() {
        audioSource.PlayOneShot(okSound);
        SceneTransitioner.singleton.gotoScene(Scenes.INSTRUCTIONS);
    }

    public void ExitGameOption() {
        audioSource.PlayOneShot(okSound);
        SceneTransitioner.singleton.exitGame();
    }
}

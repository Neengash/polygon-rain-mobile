using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelController : MonoBehaviour
{
    [SerializeField] AudioClip okSound;
    AudioSource audioSource;

    void Start() {
        audioSource = FindObjectOfType<AudioSource>();
    }

    public void ResumeOption() {
        audioSource.PlayOneShot(okSound);
        PauseManager.singleton.resumeGame();
    }

    public void MainMenuOption() {
        audioSource.PlayOneShot(okSound);
        PauseManager.singleton.stopPause();
        SceneTransitioner.singleton.gotoScene(Scenes.MAIN_MENU);
    }
}

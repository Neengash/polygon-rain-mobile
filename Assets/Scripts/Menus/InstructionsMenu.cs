using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsMenu : MonoBehaviour
{
    [SerializeField] AudioClip okSound;
    AudioSource audioSource;
    bool locked = false;
    bool instructionsLocked = false;
    int state = 0;

    const string STATE = "State";

    [SerializeField] GameObject leftArrow, rightArrow;
    [SerializeField] Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        UpdateMenu();
    }

    private void UpdateMenu()
    {
        checkArrows();
        updateAnimator();
    }

    private void checkArrows() {
        leftArrow.SetActive(state != 0);
        rightArrow.SetActive(state != 3);
    }

    private void updateAnimator() {
        animator.SetInteger(STATE, state);
    }

    public void MainMenuOption() {
        if (!locked) {
            locked = true;
            SceneTransitioner.singleton.gotoScene(Scenes.MAIN_MENU);
            audioSource.PlayOneShot(okSound);
        }
    }

    public void RightOption() {
        if (!instructionsLocked) {
            instructionsLocked = true;
            state++;
            UpdateMenu();
            StartCoroutine(unlockInstructions());
        }
    }

    public void LeftOption() {
        if (!instructionsLocked) {
            instructionsLocked = true;
            state--;
            UpdateMenu();
            StartCoroutine(unlockInstructions());
        }
    }

    IEnumerator unlockInstructions()
    {
        yield return new WaitForSeconds(1f);
        instructionsLocked = false;
    }
}

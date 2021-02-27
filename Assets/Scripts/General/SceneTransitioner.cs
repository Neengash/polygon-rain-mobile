using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    public static SceneTransitioner singleton;

    Animator anim;
    const string SCENE_END = "end";
    const float FADE_OUT_TIME = 1f;

    private void Awake() {
        if (singleton == null) {
            singleton = this;
        } else {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        // No DontDestroyOnLoad because we don't want
        // the gameObject to persist through scenes
        // Logic is easier if new object is created
    }

    void Start() {
        anim = GetComponent<Animator>();
    }

    public void gotoScene(int scene) {
        anim.SetTrigger(SCENE_END);
        StartCoroutine(doGotoScene(scene));
    }

    IEnumerator doGotoScene(int scene) {
        yield return new WaitForSeconds(FADE_OUT_TIME);
        SceneManager.LoadScene(scene);
    }

    public void exitGame() {
        anim.SetTrigger(SCENE_END);
        StartCoroutine(doExitGame());
    }
    IEnumerator doExitGame() {
        yield return new WaitForSeconds(FADE_OUT_TIME);
        Application.Quit();
    }
}

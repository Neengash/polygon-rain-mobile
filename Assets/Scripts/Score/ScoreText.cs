using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : PoolableObject
{
    TextMeshPro text;

    float ySpeed;
    const float BASE_Y_SPEED = 1f;

    const float LIFE_TIME = 1f;

    void Start() {
        loadReferences();
    }

    public override void loadReferences() {
        if (text == null) { text = GetComponent<TextMeshPro>(); }
        ySpeed = BASE_Y_SPEED;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y + ySpeed * Time.deltaTime,
            transform.position.z
            );
    }

    public void show() {
        StartCoroutine(deactivate());
    }

    IEnumerator deactivate()
    {
        yield return new WaitForSeconds(LIFE_TIME);
        gameObject.SetActive(false);
    }
}

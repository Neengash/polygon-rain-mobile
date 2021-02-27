using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject[] healthPoints;
    int healthValue;

    void Start() {
        healthValue = -1;
        renderHealthPoints();
    }

    void Update() {
        renderHealthPoints();
    }

    void renderHealthPoints() {
        int health = PlayerController.singleton.getHealth();
        if (health != healthValue) {
            healthValue = health;
            updateRender();
        }
    }

    void updateRender() {
        int idx = 0;
        while (idx <= healthValue -1) {
            healthPoints[idx++].SetActive(true);
        }
        while (idx <= healthPoints.Length -1) {
            healthPoints[idx++].SetActive(false);
        }
    }
}

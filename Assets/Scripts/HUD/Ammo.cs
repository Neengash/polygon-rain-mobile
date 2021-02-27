using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    [SerializeField] Image[] ammoSlots;
    int baseBullets, extraBullets;

    void Start() {
        baseBullets = -1;
        extraBullets = -1;
        renderAmmoSlots();
    }

    void Update() {
        renderAmmoSlots();
    }

    void renderAmmoSlots() {
        int baseBulletsValue = PlayerShooter.singleton.getBaseBullets();
        int extraBulletsValue = PlayerShooter.singleton.getExtraBullets();

        if (baseBullets != baseBulletsValue || extraBullets != extraBulletsValue)
        {
            baseBullets = baseBulletsValue;
            extraBullets = extraBulletsValue;
            updateRender();
        }
    }

    void updateRender() {
        int idx = 0;
        while (idx <= baseBullets -1) {
            ammoSlots[idx].gameObject.SetActive(true);
            ammoSlots[idx].color = Color.black;
            idx++;
        }
        while (idx <= baseBullets + extraBullets - 1) {
            ammoSlots[idx].gameObject.SetActive(true);
            ammoSlots[idx].color = Color.grey;
            idx++;
        }
        while (idx <= ammoSlots.Length -1) {
            ammoSlots[idx].gameObject.SetActive(false);
            idx++;
        }


    }
}

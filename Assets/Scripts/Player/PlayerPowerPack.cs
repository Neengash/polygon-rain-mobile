using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerPack : MonoBehaviour
{
    public static PlayerPowerPack singleton;

    public delegate void usePowerPackDelegate();
    public event usePowerPackDelegate usePowerPackRelease;

    private void Awake() {
        if (singleton == null) {
            singleton = this;
        } else {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        // No DontDestroyOnLoad because we don't want
        // the gameObject to persist through scenes
    }

    public void usePowerPack()
    {
        if (usePowerPackRelease != null) {
            usePowerPackRelease();
        }
    }
}

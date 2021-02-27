using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRecord : MonoBehaviour
{
    void Start() {
        if (!ScoreManager.singleton.isNewRecord()) {
            gameObject.SetActive(false);
        }
    }
}

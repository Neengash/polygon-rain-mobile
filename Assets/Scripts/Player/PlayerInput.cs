using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] float minTilt, tiltMultiplication;

    public float GetTilt() {
        float tilt = Input.acceleration.x;

        if (Mathf.Abs(tilt) < minTilt) {
            tilt = 0f;
        }

        tilt *= tiltMultiplication;
        
        if (Mathf.Abs(tilt) > 1) {
            tilt /= Mathf.Abs(tilt);
        }

        return tilt;
    }
}

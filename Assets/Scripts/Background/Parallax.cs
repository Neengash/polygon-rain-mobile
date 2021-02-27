using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer mesh;
    public float speedX;
    private float xPos;

    void Start() {
        mesh = GetComponent<MeshRenderer>();
        xPos = mesh.material.mainTextureOffset.x;
    }

    void Update() {
        xPos += speedX * Time.deltaTime;
        mesh.material.mainTextureOffset = new Vector2(
            xPos, mesh.material.mainTextureOffset.y);
    }
}

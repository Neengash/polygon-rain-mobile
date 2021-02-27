using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : PoolableObject
{
    ParticleSystem particles;

    void Start() {
        loadReferences();
    }

    public override void loadReferences() {
        if (particles == null) { particles = GetComponent<ParticleSystem>(); }
    }

    public void play() {
        loadReferences();
        particles.Play();
        StartCoroutine(stop());
    }

    IEnumerator stop()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

}

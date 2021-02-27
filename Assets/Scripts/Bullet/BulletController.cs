using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : PoolableObject
{
    [SerializeField] float ySpeed;
    AudioSource audioSource;
    [SerializeField] AudioClip hitWallSound;
    private Rigidbody2D rb2D;
    bool isBaseBullet;
    bool hasCollided;
    [SerializeField] GameObject[] children;
    Collider2D bulletCollider;

    void Start() {
        loadReferences();
    }

    public override void loadReferences() {
        if (rb2D == null) { rb2D = GetComponent<Rigidbody2D>(); }
        if (audioSource == null) { audioSource = GetComponent<AudioSource>(); }
        if (bulletCollider == null) { bulletCollider = GetComponent<Collider2D>(); }
    }

    public void resetValues() {
        hasCollided = false;
        activateChildren();
        bulletCollider.enabled = true;
    }

    public void isBase(bool isBase) {
        isBaseBullet = isBase;
    }

    void FixedUpdate() {
        rb2D.velocity = Vector2.up * ySpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == Layers.TOP) {
            audioSource.PlayOneShot(hitWallSound);
            removeBullet();
            addExplosion();
        }

        if (collision.gameObject.layer == Layers.ENEMY) {
            removeBullet();
        }
    }

    void addExplosion() {
        BulletExplosion explosion = BulletExplosionSpawner.singleton.getExplosion();
        explosion.transform.position = transform.position;
        explosion.gameObject.SetActive(true);
        explosion.play();
    }

    private void removeBullet()
    {
        if (!hasCollided) {
            hasCollided = true;
            bulletCollider.enabled = false;
            if (isBaseBullet) {
                PlayerShooter.singleton.recoverBaseBullet();
            }
            deactivateChildren();
            StartCoroutine(dissableBullet());
        }
    }
    
    IEnumerator dissableBullet() {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    void deactivateChildren() {
        updateChildrenStatus(false);
    }

    void activateChildren() {
        updateChildrenStatus(true);
    }

    void updateChildrenStatus(bool status) {
        foreach (GameObject child in children) {
            child.SetActive(status);
        }
    }
}

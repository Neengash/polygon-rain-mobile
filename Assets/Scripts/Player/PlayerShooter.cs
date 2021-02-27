using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public static PlayerShooter singleton;

    AudioSource audioSource;
    [SerializeField] AudioClip shootSound;
    int baseBullets;
    int extraBullets;
    float shootTimer;

    public const int MAX_EXTRA_BULLETS = 6;

    const float BASE_SHOOT_TIMER = .3f;

    const int
        STARTING_BASE_BULLETS = 2,
        STARTING_EXTRA_BULLETS = 0;

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

    private void Start() {
        baseBullets = STARTING_BASE_BULLETS;
        extraBullets = STARTING_EXTRA_BULLETS;
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (GameManager.singleton.isPlaying()) {
            doUpdate();
        }
    }

    void doUpdate() {
        if (shootTimer > 0) {
            shootTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.J) && shootTimer <= 0 && !PlayerController.singleton.isHurting()) {
            PlayerController.singleton.shootAnim();
            shootTimer = BASE_SHOOT_TIMER;
            if (baseBullets > 0) {
                shootBaseBullet();
            } else if (extraBullets > 0) {
                shootExtraBullet();
            }
        }
    }

    public void recoverBaseBullet() { if (baseBullets < STARTING_BASE_BULLETS) baseBullets++; }
    public void newExtraBullet() { if (extraBullets < MAX_EXTRA_BULLETS) extraBullets++; }
    public int getBaseBullets() { return baseBullets; }
    public int getExtraBullets() { return extraBullets; }

    private void shootBaseBullet() {
        BulletController bullet = shootBullet();
        bullet.resetValues();
        bullet.isBase(true);
        baseBullets--;
    }

    private void shootExtraBullet() {
        BulletController bullet = shootBullet();
        bullet.resetValues();
        bullet.isBase(false);
        extraBullets--;
    }

    private BulletController shootBullet() {
        audioSource.PlayOneShot(shootSound);
        BulletController bullet = BulletSpawner.singleton.getBullet();
        bullet.transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            bullet.transform.position.z);
        bullet.gameObject.SetActive(true);
        return bullet;
    }
}

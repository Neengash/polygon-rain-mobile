using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController singleton;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sRenderer;
    [SerializeField] ParticleSystem death;
    [SerializeField] AudioClip hurtSound;
    [SerializeField] AudioClip deadSound;
    AudioSource audioSource;

    [SerializeField] Collider2D hitbox;
    bool isHurt;
    const float REACTIVATE_HITBOX_TIME = 0.20f;

    int health;
    float speed;
    const float BASE_SPEED = 250f;
    const float STILL_SPEED = 0.01f;
    public const int BASE_HEALTH = 4;

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


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        speed = BASE_SPEED;
        health = BASE_HEALTH;
        isHurt = false;
    }

    void Update()
    {
        if (health > 0 && GameManager.singleton.isPlaying()) {
            float axis = Input.GetAxisRaw("Horizontal");
            bool isStill = Mathf.Abs(axis) < STILL_SPEED;
            anim.SetBool("Moving", !isStill);
            if (!isStill) {
                sRenderer.flipX = (axis < 0);
            }
        }
    }

    void FixedUpdate()
    {
        if (health > 0) {
            float xSpeed = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            rb.velocity = new Vector2(xSpeed, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Layers.ENEMY && !isHurt) {
            isHurt = true;
            health--;
            if (health <= 0) {
                playerDead();
            } else {
                playerHurt();
            }
        }
    }

    void playerHurt() {
        audioSource.PlayOneShot(hurtSound);
        hitbox.enabled = false;
        anim.SetTrigger("Hurt");
        StartCoroutine(reactivateCollider());
    }

    IEnumerator reactivateCollider() {
        yield return new WaitForSeconds(REACTIVATE_HITBOX_TIME);
        isHurt = false;
        yield return new WaitForSeconds(REACTIVATE_HITBOX_TIME);
        hitbox.enabled = true;
    }

    void playerDead() {
        audioSource.PlayOneShot(deadSound);
        rb.velocity = new Vector3();
        sRenderer.enabled = false;
        death.gameObject.SetActive(true);
        death.Play();
        GameManager.singleton.gameOver();
        StartCoroutine(goToGameOver());
    }
    
    IEnumerator goToGameOver() {
        yield return new WaitForSeconds(1f);
        SceneTransitioner.singleton.gotoScene(Scenes.GAME_OVER);
    }

    public bool isHurting() {
        return isHurt;
    }

    public void shootAnim() {
        anim.SetTrigger("Shoot");
    }

    public int getHealth() {
        return health;
    }

    public void heal() {
        if (health > 0 && health < BASE_HEALTH) {
            health++;
        }
    }
}

using System.Collections;
using UnityEngine;

public abstract class EnemyBase : PoolableObject {
    protected Rigidbody2D rb2d;
    protected Collider2D collider2d;
    protected Animator anim;
    protected float speed;
    bool rotationActive;
    float rotationSpeed;
    bool needsSpeed;

    [SerializeField] GameObject childRender;
    [SerializeField] AudioClip deathSound;
    AudioSource audioSource;

    // Those are the enemy speeds
    protected const float
        HEX_SPEED = 1.5f,
        PENTA_SPEED = 2f,
        TETRA_SPEED = 2.5f,
        TRI_SPEED = 3f;

    protected const float
        SPAWN_FORCE = 200f;

    protected const int 
        LEFT = -1,
        RIGHT = 1;

    protected const int
        ENEMY_SCORE_VALUE = 1;

    protected const float
        SPAWN_GRAVITY = 0.5f,
        BASE_GRAVITY = 1f;

    protected void Start() {
        loadReferences();
    }

    public override void loadReferences()
    {
        if (rb2d == null) { rb2d = GetComponent<Rigidbody2D>(); }
        if (collider2d == null) { collider2d = GetComponent<Collider2D>(); }
        if (anim == null) { anim = GetComponentInChildren<Animator>(); }
        if (audioSource == null) { audioSource = GetComponent<AudioSource>(); }
        rotationSpeed = 1.5f;
    }

    private void Update() {
        if (rotationActive && !GameManager.singleton.isPaused()) {
            int sign = rb2d.velocity.x < 0 ? 1 : -1;
            transform.Rotate(Vector3.forward * sign * rotationSpeed);
        }
    }

    public void setSpeed(float speed) {
        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        rotationActive = true;
    }

    public void applyUpForce() {
        rb2d.AddForce(new Vector2(0, SPAWN_FORCE));
    }

    public void spawn() {
        resetValues();
        needsSpeed = true;
        rb2d.gravityScale = SPAWN_GRAVITY;
        anim.SetTrigger("Spawn");
        StartCoroutine(startRotation());
        rotationActive = false;
    }

    IEnumerator startRotation() {
        yield return new WaitForSeconds(1f);
        rb2d.gravityScale = BASE_GRAVITY;
        rotationActive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == Layers.BULLET) {
            enemyDeath();
        }
    }

    public void resetValues() {
        childRender.SetActive(true);
        collider2d.enabled = true;
    }

    private void enemyDeath() {
        rotationActive = false;
        needsSpeed = false;
        ScoreManager.singleton.incrementScore(ENEMY_SCORE_VALUE);
        SpawnScoreText();
        deathActions();
        childRender.SetActive(false);
        collider2d.enabled = false;
        StartCoroutine(deactivateEnemy());
        audioSource.PlayOneShot(deathSound);

    }

    IEnumerator deactivateEnemy() {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (needsSpeed &&  collision.gameObject.layer == Layers.WALLS) {
            needsSpeed = false;
            startSpeed();
        }
    }

    private void SpawnScoreText() {
        ScoreText text = ScoreSpawner.singleton.getScoreText();
        text.transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            text.transform.position.z);
        text.gameObject.SetActive(true);
        text.show();
    }

    protected void OnEnable() {
        PlayerPowerPack.singleton.usePowerPackRelease += enemyDeath;
    }

    protected new void OnDisable() {
        PlayerPowerPack.singleton.usePowerPackRelease -= enemyDeath;
        base.OnDisable();
    }

    protected abstract void startSpeed();

    protected abstract void deathActions();
}

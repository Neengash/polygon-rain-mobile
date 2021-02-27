using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : PoolableObject
{
    [SerializeField] Sprite bullet, health, powerPack;

    int type;

    public const int
        TYPE_BULLET = 0,
        TYPE_HEALTH = 1,
        TYPE_POWER = 2;

    SpriteRenderer sRenderer;

    public override void loadReferences() {
        if (sRenderer == null) { sRenderer = GetComponent<SpriteRenderer>(); }
    }

    void Start() {
        loadReferences();
    }

    public void SetType(int type) {
        this.type = type;
        sRenderer.sprite = GetSprite();
    }

    protected Sprite GetSprite() {
        switch (this.type) {
            case Drop.TYPE_BULLET:
                return bullet;
            case Drop.TYPE_HEALTH:
                return health;
            case Drop.TYPE_POWER:
                return powerPack;
            default:
                return null; // Should never happen
        }
    }
    
    public int GetDropType() {
        return type;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == Layers.PLAYER) {
            this.gameObject.SetActive(false);
        }
    }
}

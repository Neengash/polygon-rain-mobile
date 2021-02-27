using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriEnemy : EnemyBase
{
    const float DROP_CHANCE = 0.2f;
    protected override void deathActions() {
        spawnTriDeath();
        if (Random.Range(0f, 1f) < DROP_CHANCE) {
            Drop drop = DropSpawner.singleton.getRandomDrop();
            if (drop != null) {
                drop.transform.position = new Vector2(
                    this.transform.position.x,
                    this.transform.position.y );
                drop.gameObject.SetActive(true);
            }
        }
    }

    protected override void startSpeed() {
        int sign = (Random.Range(0, 2) == 0) ? 1 : -1;
        setSpeed(sign * TRI_SPEED);
    }

    void spawnTriDeath() {
        EnemyDeath death = EnemyDeathSpawner.singleton.getTriDeath();
        death.transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            death.transform.position.z);
        death.gameObject.SetActive(true);
        death.play();
    }
}

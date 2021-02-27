using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetraEnemy : EnemyBase
{
    protected override void deathActions() {
        spawnTriEnemy(LEFT);
        spawnTriEnemy(RIGHT);
        spawnTetraDeath();
    }

    // Sign expects either -1 or 1;
    void spawnTriEnemy(int sign) {
        TriEnemy enemy = EnemySpawner.singleton.getTriEnemy();
        enemy.transform.position = new Vector3(
            transform.position.x + 0.5f * sign,
            transform.position.y,
            enemy.transform.position.z);
        enemy.gameObject.SetActive(true);
        enemy.resetValues();
        enemy.setSpeed(sign * TRI_SPEED );
        enemy.applyUpForce();
    }

    void spawnTetraDeath() {
        EnemyDeath death = EnemyDeathSpawner.singleton.getTetraDeath();
        death.transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            death.transform.position.z);
        death.gameObject.SetActive(true);
        death.play();
    }

    protected override void startSpeed() {
        int sign = (Random.Range(0, 2) == 0) ? 1 : -1;
        setSpeed(sign * TETRA_SPEED);
    }
}

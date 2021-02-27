using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentaEnemy : EnemyBase
{
    protected override void deathActions() {
        spawnTetraEnemy(LEFT);
        spawnTetraEnemy(RIGHT);
        spawnPentaDeath();
    }

    // Sign expects either -1 or 1;
    void spawnTetraEnemy(int sign) {
        TetraEnemy enemy = EnemySpawner.singleton.getTetraEnemy();
        enemy.transform.position = new Vector3(
            transform.position.x + 0.5f * sign,
            transform.position.y,
            enemy.transform.position.z);
        enemy.gameObject.SetActive(true);
        enemy.resetValues();
        enemy.setSpeed(sign * TETRA_SPEED);
        enemy.applyUpForce();
    }

    void spawnPentaDeath() {
        EnemyDeath death = EnemyDeathSpawner.singleton.getPentaDeath();
        death.transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            death.transform.position.z);
        death.gameObject.SetActive(true);
        death.play();
    }

    protected override void startSpeed() {
        int sign = (Random.Range(0, 2) == 0) ? 1 : -1;
        setSpeed(sign * PENTA_SPEED);
    }
}

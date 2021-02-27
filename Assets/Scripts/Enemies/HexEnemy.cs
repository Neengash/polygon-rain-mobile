using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexEnemy : EnemyBase
{
    protected override void deathActions() {
        spawnPentaEnemy(LEFT);
        spawnPentaEnemy(RIGHT);
        spawnHexDeath();
    }

    // Sign expects either -1 or 1;
    void spawnPentaEnemy(int sign) {
        PentaEnemy enemy = EnemySpawner.singleton.getPentaEnemy();
        enemy.transform.position = new Vector3(
            transform.position.x + 0.5f * sign,
            transform.position.y,
            enemy.transform.position.z);
        enemy.gameObject.SetActive(true);
        enemy.resetValues();
        enemy.setSpeed(sign * PENTA_SPEED);
        enemy.applyUpForce();
    }

    void spawnHexDeath() {
        EnemyDeath death = EnemyDeathSpawner.singleton.getHexDeath();
        death.transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            death.transform.position.z);
        death.gameObject.SetActive(true);
        death.play();
    }

    protected override void startSpeed() {
        int sign = (Random.Range(0, 2) == 0) ? 1 : -1;
        setSpeed(sign * HEX_SPEED);
    }
}

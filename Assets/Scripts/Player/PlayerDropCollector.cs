using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDropCollector : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip collectDropSound;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Layers.DROP) {
            audioSource.PlayOneShot(collectDropSound);
            Drop drop = collision.gameObject.GetComponent<Drop>();
            DoDropAction(drop);
        }
    }

    private void DoDropAction(Drop drop)
    {
        switch (drop.GetDropType()) {
            case Drop.TYPE_BULLET:
                PlayerShooter.singleton.newExtraBullet();
                break;
            case Drop.TYPE_HEALTH:
                PlayerController.singleton.heal();
                break;
            case Drop.TYPE_POWER:
                PlayerPowerPack.singleton.usePowerPack();
                break;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCollisionHandler : MonoBehaviour
{
    private const int Damage = 10;
    private GameObject explosionPrefab;

    private void Start()
    {
        explosionPrefab = Resources.Load<GameObject>("Explosion");
    }

    private void OnCollisionEnter(Collision collision){
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(Damage);
                Destroy(gameObject);
                break;
            case "Obstacle":
            case "Skill":
                GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
                Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.duration);
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}

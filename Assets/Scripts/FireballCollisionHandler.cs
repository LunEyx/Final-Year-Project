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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(Damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Skill"))
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.duration);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}

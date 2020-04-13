using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private int skillDamage = 10;
    public GameObject explosionPrefab;

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(skillDamage);
        }

        if (collision.gameObject.CompareTag("Skill"))
        {
            explosionPrefab = Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(explosionPrefab, explosionPrefab.GetComponent<ParticleSystem>().main.duration);
            Destroy(collision.gameObject);
        }
        
        Destroy(gameObject);
    }
}

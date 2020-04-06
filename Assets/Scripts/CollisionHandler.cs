using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private int skillDamage = 10;
    public GameObject explosionPrefab;

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "Enemy"){
            if (collision.gameObject.GetComponent<Enemy>().hpSystem.get_hp() > 0)
                collision.gameObject.GetComponent<Enemy>().hpSystem.damage(skillDamage);
            else if (collision.gameObject.GetComponent<Enemy>().hpSystem.get_hp() <= 0)
                Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Skill"){
            explosionPrefab = Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(explosionPrefab, explosionPrefab.GetComponent<ParticleSystem>().duration);
            Destroy(collision.gameObject);
        }
        
        Destroy(gameObject);
    }
}

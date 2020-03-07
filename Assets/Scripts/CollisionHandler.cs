using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private int skillDamage = 10;

    private void OnCollisionEnter(Collision collision){
        
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Obstacle"){

            if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<Enemy>().hpSystem.get_hp() > 0){
                collision.gameObject.GetComponent<Enemy>().hpSystem.damage(skillDamage);
            }
            if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<Enemy>().hpSystem.get_hp() <= 0){
                Destroy(collision.gameObject);
            }

            Destroy(gameObject);
        }
    }
}

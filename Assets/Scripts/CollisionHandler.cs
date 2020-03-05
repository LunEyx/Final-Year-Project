using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private int skillDamage = 10;

    private void OnCollisionEnter(Collision collision){
        
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Obstacle"){

            if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<Enemy>().health > 0){
                collision.gameObject.GetComponent<Enemy>().health -= skillDamage;
            } else if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<Enemy>().health <= 0){
                Destroy(collision.gameObject);
            }

            Destroy(gameObject);
        }
    }
}

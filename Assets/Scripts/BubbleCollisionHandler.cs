using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollisionHandler : MonoBehaviour
{
    private int skillDamage = 5;
    private float bubbleDuration = 5f;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Obstacle")
        {

            if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<Enemy>().hpSystem.get_hp() > 0)
            {
                collision.gameObject.GetComponent<Enemy>().hpSystem.damage(skillDamage);
                collision.gameObject.GetComponent<Enemy>().bubbled = true;
                collision.gameObject.GetComponent<Enemy>().bubbleDuration = bubbleDuration;
            }
            if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<Enemy>().hpSystem.get_hp() <= 0)
            {
                Destroy(collision.gameObject);
            }

            Destroy(gameObject);
        }
    }

}

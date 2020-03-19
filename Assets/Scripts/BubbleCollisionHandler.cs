using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollisionHandler : MonoBehaviour
{
    private int skillDamage = 5;
    private float bubbledDuration = 5f;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Obstacle")
        {

            if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<Enemy>().hpSystem.get_hp() > 0)
            {
                collision.gameObject.GetComponent<Enemy>().hpSystem.damage(skillDamage);
                collision.gameObject.GetComponent<Enemy>().ApplyDebuff("Bubbled", bubbledDuration);
            }
            if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<Enemy>().hpSystem.get_hp() <= 0)
            {
                Destroy(collision.gameObject);
            }

            Destroy(gameObject);
        }
    }

}

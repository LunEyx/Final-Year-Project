using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Obstacle")
        {

            if (collision.gameObject.tag == "Player")
            {
                // Logic of Hitting Player
            }

            Destroy(gameObject);
        }
    }
}

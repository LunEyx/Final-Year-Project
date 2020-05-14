using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoCollisionHandler : MonoBehaviour
{
    private const int FireDamage = 20;
    private int damage = 0;

    private void OnTriggerEnter(Collider other)
    {
        damage = Tornado.Damage;
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
        if (other.gameObject.CompareTag("Skill"))
        {
            if (other.gameObject.GetComponent<FireballCollisionHandler>() != null || other.gameObject.GetComponent<MeteorCollisionHandler>() != null)
            {
                Material mat = Resources.Load<Material>("Materials/glow_volumetric_alpha_red");
                gameObject.GetComponent<ParticleSystemRenderer>().material = mat;
                damage += FireDamage;
            }
        }
    }
}

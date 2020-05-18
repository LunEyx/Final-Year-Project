using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoCollisionHandler : MonoBehaviour
{
    private int damage = Tornado.Damage;

    private void OnTriggerEnter(Collider other)
    {
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
                damage += Fireball.Damage;
            }
        }
    }
}

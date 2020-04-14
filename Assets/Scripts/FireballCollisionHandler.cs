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
        switch (collision.gameObject.tag)
        {
            case "Enemy":
            case "Player":
                Actor actor = collision.gameObject.GetComponent<Actor>();
                actor.TakeDamage(Damage);
                Destroy(gameObject);
                break;
            case "Obstacle":
            case "Skill":
                if (collision.gameObject.GetComponent<TornadoCollisionHandler>() != null)
                {
                    transform.Rotate(new Vector3(0, Random.Range(0f, 360f), 0));
                }
                else
                {
                    GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
                    Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.duration);
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
    }
}

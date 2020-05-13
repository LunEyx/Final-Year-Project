using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollisionHandler : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(Bubble.Damage);
            enemy.ApplyEffect(Actor.Effect.Bubbled, Bubble.BubbledDuration);
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}

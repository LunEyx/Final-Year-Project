using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollisionHandler : MonoBehaviour
{
    private const int Damage = 5;
    private const float BubbledDuration = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(Damage);
            enemy.ApplyEffect(Actor.Effect.Bubbled, BubbledDuration);
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

}

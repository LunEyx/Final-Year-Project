using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollisionHandler : MonoBehaviour
{
    private const int SkillDamage = 5;
    private const float BubbledDuration = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(SkillDamage);
            enemy.ApplyEffect(Actor.Effect.Bubbled, BubbledDuration);
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

}

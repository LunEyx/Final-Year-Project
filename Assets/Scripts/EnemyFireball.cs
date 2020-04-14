using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireball : ProjectileSpell
{
    private void Awake()
    {
        prefab = Resources.Load<GameObject>("EnemyFireball");
        duration = 5f;
        minCooldown = 1f;
        maxCooldown = 2f;
        projectileSpeed = 40f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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

    [Command]
    protected override void CmdInstantiate()
    {
        Collider collider = GetComponent<Collider>();
        GameObject projectile = Instantiate(prefab, transform.position + transform.forward * (1 + collider.bounds.extents.z), transform.rotation);
        projectile.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
        NetworkServer.Spawn(projectile);
        Destroy(projectile, duration);
    }
}

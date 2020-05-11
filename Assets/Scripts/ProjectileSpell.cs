using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class ProjectileSpell : Spell
{
    protected GameObject prefab;
    protected float duration;
    protected float projectileSpeed;

    public override void Cast()
    {
        base.Cast();
        CmdInstantiate();
    }

    [Command]
    protected virtual void CmdInstantiate()
    {
        GameObject projectile = Instantiate(prefab, transform.position + transform.forward * 2, transform.rotation);
        projectile.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
        NetworkServer.Spawn(projectile);
        Destroy(projectile, duration);
    }
}

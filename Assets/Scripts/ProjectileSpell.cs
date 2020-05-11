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
        Debug.Log("Spawn");
        NetworkServer.Spawn(projectile);
        Debug.Log("Spawn Done");
        Destroy(projectile, duration);
    }
}

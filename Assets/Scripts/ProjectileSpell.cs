using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class ProjectileSpell : Spell
{
    public GameObject prefab;
    protected float duration;
    protected float projectileSpeed;
    private GameObject projectile;

    public override void Cast()
    {
        base.Cast();
        CmdInstantiate(prefab.name);
    }

    [Command]
    protected void CmdInstantiate(string prefabName)
    {
        Debug.Log(prefabName);
        GameObject prefab = Resources.Load<GameObject>(prefabName);
        projectile = Instantiate(prefab, transform.position + transform.forward * 2, transform.rotation);
        projectile.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;

        Destroy(projectile, duration);
        NetworkServer.Spawn(projectile);
    }
}

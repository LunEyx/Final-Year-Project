using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileSpell : Spell
{
    protected GameObject prefab;
    protected float duration;
    protected float projectileSpeed;

    public override void Cast()
    {
        base.Cast();
        Instantiate();
    }

    protected virtual void Instantiate()
    {
        GameObject projectile = Instantiate(prefab, transform.position + transform.forward * 2, transform.rotation);
        projectile.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
        Destroy(projectile, duration);
    }
}

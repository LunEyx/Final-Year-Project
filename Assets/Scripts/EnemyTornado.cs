using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTornado : Spell
{
    protected GameObject prefab;
    protected float duration;
    
    private void Awake() {
        prefab = Resources.Load<GameObject>("EnemyTornado");
        duration = 5f;
        minCooldown = 1f;
        maxCooldown = 10f;
    }

    public override void Cast(){
        base.Cast();
        GameObject centerTornado = Instantiate(prefab, transform.position + transform.forward * 5, transform.rotation);
        GameObject rightTornado = Instantiate(prefab, transform.position + transform.forward * 5 + transform.right * 20, transform.rotation);
        GameObject leftTornado = Instantiate(prefab, transform.position + transform.forward * 5 + transform.right * -20, transform.rotation);
        Destroy(centerTornado, duration);
        Destroy(rightTornado, duration);
        Destroy(leftTornado, duration);
    }
}

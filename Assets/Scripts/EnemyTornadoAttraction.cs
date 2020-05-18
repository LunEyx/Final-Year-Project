using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTornadoAttraction : TornadoAttraction
{
    public static int Damage = 1;

    protected override void OnTriggerEnter(Collider obj){
        if (obj.GetComponent<Rigidbody>() != null && obj.gameObject.tag != "Enemy") {
            StartCoroutine("PullObject", obj);
        }
    }

    protected override void HandlePulling(Collider obj){
        base.HandlePulling(obj);
        if (obj.gameObject.tag == "Player") {
            obj.GetComponent<Player>().TakeDamage(Damage);
        }
    }

}

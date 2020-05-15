using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireNovaCollisionHandler : MonoBehaviour
{

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public void Start()
    {
        FindTarget();
    }

    private void FindTarget()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, 7, targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            
            target.GetComponent<Actor>().TakeDamage(FireNova.Damage);
        }
    }
}

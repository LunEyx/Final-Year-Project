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
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < 360 / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    target.GetComponent<Actor>().TakeDamage(FireNova.Damage);
                }
            }
        }
    }
}

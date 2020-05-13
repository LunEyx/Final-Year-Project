using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoAttraction : MonoBehaviour
{
    public Transform particleCenter;
    public float pullForce;
    public float refreshRate;

    protected virtual void OnTriggerEnter(Collider obj){
        if (obj.GetComponent<Rigidbody>() != null)
        {
            StartCoroutine("PullObject", obj);
        }
    }

    protected virtual void HandlePulling(Collider obj){
        Vector3 pullDirection = particleCenter.position - obj.transform.position;
        obj.GetComponent<Rigidbody>().AddForce(pullDirection.normalized * pullForce * Time.deltaTime);
    }

    IEnumerator PullObject(Collider obj){
        if (obj != null)
        {
            HandlePulling(obj);
            yield return refreshRate;
            StartCoroutine("PullObject", obj);
        }
    }
}

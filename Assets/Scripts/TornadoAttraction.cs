using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoAttraction : MonoBehaviour
{
    public Transform particleCenter;
    public float pullForce;
    public float refreshRate;

    private void OnTriggerEnter(Collider obj){
        if (obj.GetComponent<Rigidbody>() != null)
        {
            StartCoroutine("PullObject", obj);
        }
    }

    IEnumerator PullObject(Collider obj){
        if (obj != null)
        {
            Vector3 pullDirection = particleCenter.position - obj.transform.position;
            obj.GetComponent<Rigidbody>().AddForce(pullDirection.normalized * pullForce * Time.deltaTime);
            yield return refreshRate;
            StartCoroutine("PullObject", obj);
        }
    }
}

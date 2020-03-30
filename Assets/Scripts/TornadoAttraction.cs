using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoAttraction : MonoBehaviour
{
    public Transform particleCenter;
    public float pullForce;
    public float refreshRate;

    private void OnTriggerEnter(Collider obj){
        StartCoroutine(pullObject(obj, true));
    }

    private void OnTriggerExit(Collider obj){
        StartCoroutine(pullObject(obj, false));
    }

    IEnumerator pullObject(Collider obj, bool pullable){
        if (pullable){
            Vector3 pullDirection = particleCenter.position - obj.transform.position;
            obj.GetComponent<Rigidbody>().AddForce(pullDirection.normalized * pullForce * Time.deltaTime);
            yield return refreshRate;
            StartCoroutine(pullObject(obj, pullable));
        }
    }
}

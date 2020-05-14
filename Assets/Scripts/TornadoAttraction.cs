using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoAttraction : MonoBehaviour
{
    public Transform particleCenter;
    public float pullForce;
    public float refreshRate;
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();

    protected virtual void OnTriggerEnter(Collider obj)
    {
        if (obj.GetComponent<Rigidbody>() != null)
        {
            StartCoroutine("PullObject", obj);
        }
    }

    protected virtual void HandlePulling(Collider obj)
    {
        Vector3 pullDirection = particleCenter.position - obj.transform.position;
        float distance = Vector3.Distance(obj.transform.position, transform.position);
        float distanceFactor = distance > 20 ? 0 : 1 / distance;
        if (obj.GetComponent<Enemy>() != null)
        {
            Enemy enemy = obj.GetComponent<Enemy>();
            enemy.ApplyEffect(Actor.Effect.Blow, refreshRate);
            obj.GetComponent<Rigidbody>().AddForce(pullDirection.normalized * pullForce * Time.deltaTime * 10 * distanceFactor);
        }
        else
        {
            obj.GetComponent<Rigidbody>().AddForce(pullDirection.normalized * pullForce * Time.deltaTime * distanceFactor);
        }
    }

    IEnumerator PullObject(Collider obj)
    {
        if (obj != null)
        {
            HandlePulling(obj);
            yield return refreshRate;
            StartCoroutine("PullObject", obj);
        }
    }
}

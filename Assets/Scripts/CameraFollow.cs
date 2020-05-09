using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private bool isFollow = true;
    private Vector3 distVect = new Vector3(0, 2, -5);

    private void Update()
    {
        if (isFollow && target != null)
        {
            transform.rotation = target.rotation;
            transform.position = target.position;
            transform.position += transform.forward * -5;
            transform.position += transform.up * 2;
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetIsFollow(bool value)
    {
        isFollow = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private const float MinAngleY = -80f;
    private const float MaxAngleY = 80f;

    public Transform lookat;

    private Camera cam;

    private float distance = 5f;
    private float currentX = 0f;
    private float currentY = 0f;
    private float sensitivityX = 4f;
    private float sensitivityY = 4f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        currentX += Input.GetAxis("Mouse X") * sensitivityX;
        currentY += Input.GetAxis("Mouse Y") * sensitivityY;
        currentY = Mathf.Clamp(currentY, MinAngleY, MaxAngleY);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = lookat.position + rotation * dir;
        transform.LookAt(lookat.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCaster : MonoBehaviour
{
    public GameObject prefab;
    public float skillLife = 10f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameObject skill = Instantiate(prefab, transform.position + transform.forward * 2, transform.rotation);
            skill.GetComponent<Rigidbody>().velocity = transform.forward * 15;
            Destroy(skill, skillLife);
        }
    }
}

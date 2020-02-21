using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public GameObject prefab;
    public float skillLife = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            GameObject skill = Instantiate(prefab, transform.position + transform.forward * 2, transform.rotation);
            skill.GetComponent<Rigidbody>().velocity = transform.forward * 40;
            Destroy(skill, skillLife);
        }
    }
}

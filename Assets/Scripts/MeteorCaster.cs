using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCaster : MonoBehaviour
{
    public GameObject prefab;
    public float skillLife = 1f;
    private int meteorCounter = 0;
    public int meteorNum = 20;
    public float range = 8;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            InvokeRepeating("skill_Meteor", 0, 0.1f);
            
        }
    }


    private void skill_Meteor()
    {
  
        float randPosX = Random.Range(0 - range, range);
        float randPosZ = Random.Range(0 - range, range);  

        GameObject meteor = Instantiate(prefab, new Vector3(transform.position.x + randPosX, transform.position.y + 5f, transform.position.z + randPosZ), transform.rotation);
        meteor.GetComponent<Rigidbody>().velocity = transform.up * -20;
        meteorCounter++;

        if (meteorCounter >= meteorNum)
        {
            CancelInvoke();
            meteorCounter = 0;
        }
        
            
    }

    
}

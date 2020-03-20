using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCaster : MonoBehaviour
{
    public GameObject prefab;
    public float skillLife = 5f;
    private int meteorCounter = 0;
    public int meteorNum = 20;
    public float radius = 8;

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
        float randPosX = Random.Range(0 - radius, radius);
        float randPosZ = Random.Range(0 - radius, radius);
        GameObject meteor = Instantiate(
            prefab,
            new Vector3(transform.position.x + randPosX, transform.position.y + 10f, transform.position.z + randPosZ),
            Quaternion.Euler(90, 0, 0));
        meteor.GetComponent<Rigidbody>().velocity = transform.up * -20;
        meteorCounter++;

        if (meteorCounter >= meteorNum)
        {
            CancelInvoke();
            meteorCounter = 0;
        }

        Destroy(meteor, skillLife);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private GameObject enemy;
    private float spawnTime = 1f;
    public static bool EnableSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        enemy = Resources.Load("Slime") as GameObject;
        StartCoroutine("spawn",enemy);
    }

    private IEnumerator spawn(GameObject enemey)
    {
        while (EnableSpawn)
        {
            yield return new WaitForSeconds(spawnTime);
            Instantiate(enemy,transform.position, transform.rotation);
        }
    }
}

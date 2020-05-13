using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnPoint : NetworkBehaviour
{
    private GameObject prefab;
    private float spawnTime = 3f;
    private bool coroutineRunning = false;
    public static bool EnableSpawn = false;

    private void Start()
    {
        prefab = Resources.Load("Slime") as GameObject;
    }

    private void Update()
    {   
        if (EnableSpawn && !coroutineRunning)
        {
            StartCoroutine("Spawn");
        }
    }

    [Command]
    private void CmdSpawn()
    {
        GameObject enemy = Instantiate(prefab, transform.position, transform.rotation);
        Renderer renderer = enemy.GetComponentInChildren<Renderer>();
        renderer.material.SetColor("_Color", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
        NetworkServer.Spawn(enemy);
    }

    private IEnumerator Spawn()
    {
        coroutineRunning = true;
        yield return new WaitForSeconds(spawnTime);
        CmdSpawn();
        coroutineRunning = false;
    }
}

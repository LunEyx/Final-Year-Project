using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public GameObject player;
    public SightOfView sightOfView;
    private NavMeshAgent navmesh;
    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        sightOfView = GetComponent<SightOfView>();
        navmesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sightOfView.visibleTargets.Count == 0)
        {
            navmesh.destination = player.transform.position;
        }
        else
        {
            navmesh.destination = navmesh.transform.position;
        }
    }
}
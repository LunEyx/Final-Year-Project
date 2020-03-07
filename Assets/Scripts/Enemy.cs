using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public GameObject player;
    private NavMeshAgent navmesh;
    public HpSystem hpSystem = new HpSystem(100);
    public Image hpBar;
   

    // Start is called before the first frame update
    void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navmesh.destination = player.transform.position;
        hpBar.fillAmount = hpSystem.currentLifePercentage();
    }
}
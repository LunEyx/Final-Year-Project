using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject skillPrefab;
    private SightOfView sightOfView;
    private NavMeshAgent navmesh;
    public int health = 100;
    public float skillLife = 1f;
    private readonly float minAttackCooldown = 1f;
    private readonly float maxAttackCooldown = 2f;
    private float attackCooldownTimer;

    // Start is called before the first frame update
    void Start()
    {
        sightOfView = GetComponent<SightOfView>();
        navmesh = GetComponent<NavMeshAgent>();
        attackCooldownTimer = Random.Range(minAttackCooldown, maxAttackCooldown);
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
        else
        {
            attackCooldownTimer = 0;
        }

        if (sightOfView.visibleTargets.Count == 0)
        {
            navmesh.destination = player.transform.position;
        }
        else
        {
            navmesh.destination = navmesh.transform.position;
            Vector3 targetPos = sightOfView.visibleTargets[0].position;
            Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
            navmesh.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
            if (attackCooldownTimer == 0)
            {
                Attack();
                attackCooldownTimer = Random.Range(minAttackCooldown, maxAttackCooldown);
            }
        }
    }

    void Attack()
    {
        GameObject skill = Instantiate(skillPrefab, transform.position + transform.forward, transform.rotation);
        skill.GetComponent<Rigidbody>().velocity = transform.forward * 40;
        Destroy(skill, skillLife);
    }
}
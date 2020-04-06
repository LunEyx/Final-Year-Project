using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject player;
    public GameObject bubblePrefab;
    public GameObject skillPrefab;
    private SightOfView sightOfView;
    private NavMeshAgent navmesh;
    public float skillLife = 1f;
    private readonly float minAttackCooldown = 1f;
    private readonly float maxAttackCooldown = 2f;
    private float attackCooldownTimer;
    public HpSystem hpSystem = new HpSystem(100);
    public Image hpBar;
    private bool bubbled = false;
    private GameObject bubble;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sightOfView = GetComponent<SightOfView>();
        navmesh = GetComponent<NavMeshAgent>();
        attackCooldownTimer = Random.Range(minAttackCooldown, maxAttackCooldown);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rb.velocity.magnitude);
        if (rb.velocity.magnitude > 0.05)
        {
            rb.velocity *= 0.95f;
        } else
        {
            rb.velocity = Vector3.zero;
        }
        if (!bubbled)
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
        hpBar.fillAmount = hpSystem.currentLifePercentage();
    }

    IEnumerator Bubbled(float duration)
    {
        bubbled = true;
        navmesh.destination = transform.position;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Destroy(bubble);
        bubble = Instantiate(bubblePrefab, transform);
        yield return new WaitForSeconds(duration);
        bubbled = false;
        Destroy(bubble);
    }

    void Attack()
    {
        GameObject skill = Instantiate(skillPrefab, transform.position + transform.forward, transform.rotation);
        skill.GetComponent<Rigidbody>().velocity = transform.forward * 40;
        Destroy(skill, skillLife);
        navmesh.destination = player.transform.position;
        hpBar.fillAmount = hpSystem.currentLifePercentage();
    }

    public void ApplyDebuff(string debuff, float duration)
    {
        StopCoroutine(debuff);
        StartCoroutine(debuff, duration);
    }
}

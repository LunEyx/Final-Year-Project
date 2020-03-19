using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
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
    public bool bubbled = false;
    public float bubbleDuration = 5f;
    private bool bubbleGenerated = false;
    private GameObject tempBubble;


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
        else
        {
            if (!bubbleGenerated)
            {
                tempBubble = Instantiate(bubblePrefab, transform);
                bubbleGenerated = true;
            }
                
            navmesh.destination = this.transform.position;
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            StartCoroutine(StopMoves());    
            
        }

        hpBar.fillAmount = hpSystem.currentLifePercentage();

        
    }

    IEnumerator StopMoves()
    {
        yield return new WaitForSeconds(bubbleDuration);
        bubbled = false;
        bubbleGenerated = false;
        Destroy(tempBubble);
    }


    void Attack()
    {
        GameObject skill = Instantiate(skillPrefab, transform.position + transform.forward, transform.rotation);
        skill.GetComponent<Rigidbody>().velocity = transform.forward * 40;
        Destroy(skill, skillLife);
        navmesh.destination = player.transform.position;
        hpBar.fillAmount = hpSystem.currentLifePercentage();
    }
}

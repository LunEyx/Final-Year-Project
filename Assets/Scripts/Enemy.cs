using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Enemy : Actor
{
    protected Animator animator;
    private GameObject expPopUp;
    protected Rigidbody rb;
    public Image enemyHpBar;
    protected SightOfView sightOfView;
    protected NavMeshAgent navmesh;
    private int exp;
    private int coin;
    public GameObject model;
    protected Vector3 bubbleOffset = Vector3.zero;
    protected float bubbleScale = 1;
    private Vector3 finalDest = Vector3.zero;

    protected override void Start()
    {
        base.Start();
        expPopUp = Resources.Load<GameObject>("ExpPopUp");
        hpBar = enemyHpBar;
        rb = GetComponent<Rigidbody>();
        sightOfView = GetComponent<SightOfView>();
        navmesh = GetComponent<NavMeshAgent>();
        coin = Random.Range(5, 10);
        exp = Random.Range(5, 10);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        VelocityFix();
        if (!bubbled)
        {
            PreAction();
            
            if (sightOfView.visibleTargets.Count == 0)
            {
                NoTargetAction();
            }
            else
            {
                TargetFoundAction();
            }

            PostAction();
        }
    }

    protected void VelocityFix()
    {
        if (rb.velocity.magnitude > 0.05)
        {
            rb.velocity *= 0.95f;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    protected virtual void PreAction()
    {

    }
    protected virtual void PostAction()
    {

    }

    IEnumerator Bubbled(float duration)
    {
        bubbled = true;
        navmesh.isStopped = true;
        float animationSpeed = 0;
        if (animator)
        {
            animationSpeed = animator.speed;
            animator.speed = 0;
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Destroy(bubble);
        if (model)
        {
            bubble = Instantiate(bubblePrefab, model.transform);
        }
        else
        {
            bubble = Instantiate(bubblePrefab, transform);
        }
        bubble.transform.localPosition += bubbleOffset;
        bubble.transform.localScale = new Vector3(bubbleScale, bubbleScale, bubbleScale);
        yield return new WaitForSeconds(duration);
        if (animator)
        {
            animator.speed = animationSpeed;
        }
        navmesh.isStopped = false;
        bubbled = false;
        Destroy(bubble);
    }

    IEnumerator Blow(float duration)
    {
        blew = true;
        navmesh.isStopped = true;
        rb.isKinematic = false;
        yield return new WaitForSeconds(duration);
        rb.isKinematic = true;
        navmesh.isStopped = false;
        blew = false;
    }

    protected virtual void NoTargetAction()
    {
        List<Player> players = GameManager.GetPlayers();
        if (players.Count == 0)
        {
            NoPlayerAction();
            return;
        }
        Player nearestPlayer = players[0];
        float minDistance = Mathf.Infinity;
        foreach (Player player in players)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < minDistance)
            {
                nearestPlayer = player;
                minDistance = distance;
            }
        }
        navmesh.destination = nearestPlayer.transform.position;
    }

    protected virtual void TargetFoundAction()
    {
        if (GameManager.GetPlayers().Count == 0)
        {
            NoPlayerAction();
            return;
        }
        Vector3 targetPos = sightOfView.visibleTargets[0].position;
        Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
        navmesh.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
    }

    protected virtual void NoPlayerAction()
    {
        if (Vector3.Distance(transform.position, navmesh.destination) < 2)
        {
            Vector3 newDest = Random.insideUnitSphere * 100 + transform.position;
            NavMeshHit hit;

            navmesh.destination = newDest;
        }
    }

    public override void TakeDamage(int value)
    {
        base.TakeDamage(value);
        if (GetHp() <= 0)
        {
            HandleDying();
        }
    }

    protected virtual void HandleDying() {
        GameManager.GetLocalPlayer().GetComponent<Player>().GainExp(exp);
        GameManager.GetLocalPlayer().GetComponent<Player>().GainCoin(coin);
        if (expPopUp) {
            ShowExpPopUp();
        }
        Destroy(gameObject);
    }

    private void ShowExpPopUp()
    {   
        GameObject obj = Instantiate(expPopUp, transform.position, Camera.main.transform.rotation);
        obj.GetComponent<TextMesh>().text = $"+{exp} exp";
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Enemy : Actor
{
    protected Animator animator;
    private GameObject expPopUp;
    protected Rigidbody rb;
    private GameObject[] playerObjs;
    public Image enemyHpBar;
    protected SightOfView sightOfView;
    protected NavMeshAgent navmesh;
    private const int exp = 10;
    private const int Coin = 10;
    public GameObject model;
    protected Vector3 bubbleOffset = Vector3.zero;
    protected float bubbleScale = 1;

    protected override void Start()
    {
        base.Start();
        playerObjs = GameObject.FindGameObjectsWithTag("Player");
        expPopUp = Resources.Load<GameObject>("ExpPopUp");
        hpBar = enemyHpBar;
        rb = GetComponent<Rigidbody>();
        sightOfView = GetComponent<SightOfView>();
        navmesh = GetComponent<NavMeshAgent>();
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

    protected virtual void NoTargetAction()
    {
        GameObject nearestPlayerObj = playerObjs[0];
        float minDistance = Mathf.Infinity;
        foreach (GameObject playerObj in playerObjs)
        {
            float distance = Vector3.Distance(playerObj.transform.position, transform.position);
            if (distance < minDistance)
            {
                nearestPlayerObj = playerObj;
                minDistance = distance;
            }
        }
        navmesh.destination = nearestPlayerObj.transform.position;
    }

    protected virtual void TargetFoundAction()
    {
        Vector3 targetPos = sightOfView.visibleTargets[0].position;
        Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
        navmesh.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
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
        playerObjs[0].GetComponent<Player>().GainExp(exp);
        playerObjs[0].GetComponent<Player>().gold += 10;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Slime : Enemy
{
    private const int MaxHp = 20;

    private int AttackDamage;
    private const int AttackCooldown = 5;
    private bool isAttackReady = true;

    protected override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
        hpSystem = new HpSystem(MaxHp * GameObject.Find("GameManager").GetComponent<GameManager>().level);
        bubbleOffset = new Vector3(0, 1, 0);
        bubbleScale = 4;
        CmdUpdateColor();
        AttackDamage = Random.Range(2, 6);
    }

    [Command]
    private void CmdUpdateColor()
    {
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        RpcUpdateColor(color);
    }

    [ClientRpc]
    private void RpcUpdateColor(Color color)
    {
        Renderer renderer = GetComponentInChildren<Renderer>();
        renderer.material.SetColor("_Color", color);
    }

    protected override void TargetFoundAction()
    {
        base.TargetFoundAction();
        if (isAttackReady)
        {
            isAttackReady = false;
            animator.SetTrigger("IsAttack");
            StartCoroutine("AfterAnimation");
        }
    }

    IEnumerator AfterAnimation() {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("SlimeAttacking"))
        {
            yield return null;
        }
        foreach (Transform target in sightOfView.visibleTargets)
        {
            Player player;
            if ((player = target.gameObject.GetComponent<Player>()) != null)
            {
                player.TakeDamage(AttackDamage);
            }
        }
        yield return new WaitForSeconds(AttackCooldown);
        isAttackReady = true;
    }
}

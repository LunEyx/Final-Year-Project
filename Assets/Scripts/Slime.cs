using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Slime : Enemy
{
    private const int MaxHp = 100;
    private const int AttackDamage = 5;
    private const int AttackCooldown = 5;
    private bool isAttackReady = true;

    protected override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
        hpSystem = new HpSystem(MaxHp);
        bubbleOffset = new Vector3(0, 1, 0);
        bubbleScale = 4;
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

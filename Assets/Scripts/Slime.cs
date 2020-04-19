using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Slime : Enemy
{
    private Spell spell;
    private Animator animator;
    private const int MaxHp = 100;

    protected override void Start()
    {
        base.Start();
        spell = GetComponent<Spell>();
        animator = GetComponentInChildren<Animator>();
        hpSystem = new HpSystem(MaxHp);
    }

    protected override void TargetFoundAction()
    {
        base.TargetFoundAction();
        if (!spell.IsCooldown())
        {
            animator.SetTrigger("IsAttack");
            spell.Cast();
        }
    }
}

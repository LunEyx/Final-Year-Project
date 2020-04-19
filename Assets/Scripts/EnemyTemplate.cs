using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyTemplate : Enemy
{
    private Spell spell;
    private const int MaxHp = 100;
    protected override void Start()
    {
        base.Start();
        spell = GetComponent<Spell>();
        hpSystem = new HpSystem(MaxHp);
    }

    protected override void TargetFoundAction()
    {
        base.TargetFoundAction();
        if (!spell.IsCooldown())
        {
            spell.Cast();
        }
    }
}

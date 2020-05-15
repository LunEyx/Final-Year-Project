using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : Enemy
{
    private const int MaxHp = 500;
    private int AttackDamage = 10;
    private const int AttackCooldown = 5;
    private const int SpellCooldown = 5;
    private bool isAttackReady = true;
    private bool isSpellReady = true;
    private Spell[] spells = new Spell[2];
    private int mode = 1;
    private bool currentSpell = false;
    public Image ScreenHpBar;
    public static int Difficulty = 1;

    protected override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
        hpSystem = new HpSystem(MaxHp * (1 + Difficulty / 10));
        bubbleOffset = new Vector3(0, 0.6f, 0);
        bubbleScale = 2;
        spells[0] = gameObject.AddComponent<BossFireball>() as Spell;
        spells[1] = gameObject.AddComponent<EnemyTornado>() as Spell;
    }

    protected override void TargetFoundAction()
    {
        base.TargetFoundAction();
        animator.SetBool("WalkForward", false);
        if (isAttackReady)
        {
            isAttackReady = false;
            if (UnityEngine.Random.Range(0, 2) == 1){
                animator.SetTrigger("AttackOne");
            } else {
                // set attack two damage
                AttackDamage = 20;
                animator.SetTrigger("AttackTwo");
            }

            StartCoroutine("AfterNormalAttack");
        }
    }

    IEnumerator AfterNormalAttack() {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 01") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 02"))
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

    protected override void PreAction() {
        animator.SetBool("WalkForward", true);
    }

    public override void TakeDamage(int value)
    {
        base.TakeDamage(value);
        ScreenHpBar.fillAmount = hpSystem.CurrentLifePercentage();
    }

    protected override void HandleDying() {
        if (mode == 1) {
            hpSystem = new HpSystem(MaxHp * (1 + Difficulty / 10));
            hpBar.fillAmount = hpSystem.CurrentLifePercentage();
            ScreenHpBar.fillAmount = hpSystem.CurrentLifePercentage();
            mode = 2;
        } else {
            base.HandleDying();
            animator.SetBool("WalkForward", false);
            animator.SetTrigger("Die");
            SceneManager.LoadScene("VictoryScene");
        }
    }

    protected override void PostAction() {
        if (mode != 1 && isSpellReady && !spells[Convert.ToInt32(currentSpell)].IsCooldown()){
            animator.SetBool("WalkForward", false);
            isSpellReady = false;
            StartCoroutine("AfterCastingSpell");
        }
    }

    IEnumerator AfterCastingSpell() {
        spells[Convert.ToInt32(currentSpell)].Cast();
        currentSpell = !currentSpell;
        yield return new WaitForSeconds(SpellCooldown);
        isSpellReady = true;
    }


}

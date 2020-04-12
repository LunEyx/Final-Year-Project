using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCaster : Spell
{
    public GameObject prefab;
    public float skillLife;
    public Animator animator;

    private void Start()
    {
        cooldown = 3f;
        animator = GetComponentInChildren<Animator>();
    }

    public override void Cast()
    {
        base.Cast();
        animator.SetTrigger("IsAttack");
        Invoke("SkillInstantiate", 0.5f);
    }

    void SkillInstantiate()
    {
        GameObject skill = Instantiate(prefab, transform.position + transform.forward * 2, transform.rotation);
        skill.GetComponent<Rigidbody>().velocity = transform.forward * 40;
        Destroy(skill, skillLife);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCaster : Spell
{
    public GameObject prefab;
    public float skillLife;
    public Animator animator;

    private void Awake()
    {
        icon = Resources.Load<Sprite>("Icons/SpellBookPreface_18");
        prefab = prefab ? prefab : Resources.Load<GameObject>("Fireball");
        skillLife = skillLife != 0 ? skillLife : 5f;
        cooldown = cooldown != 0 ? cooldown : 3f;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    public override void Cast()
    {
        base.Cast();
        if (animator != null)
        {
            animator.SetTrigger("IsAttack");
        }
        Invoke("SkillInstantiate", 0.5f);
    }

    void SkillInstantiate()
    {
        GameObject skill = Instantiate(prefab, transform.position + transform.forward * 2, transform.rotation);
        skill.GetComponent<Rigidbody>().velocity = transform.forward * 40;
        Destroy(skill, skillLife);
    }
}

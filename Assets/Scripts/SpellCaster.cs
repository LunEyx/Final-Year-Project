using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public GameObject prefab;
    public float skillLife = 1f;
    public float cooldown = 3f;
    public bool isCooldown = false;
    public Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public float GetCooldown()
    {
        return cooldown;
    }

    public bool IsCooldown()
    {
        return isCooldown;
    }

    public void Ready()
    {
        isCooldown = false;
    }

    public void Cast()
    {
        isCooldown = true;
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

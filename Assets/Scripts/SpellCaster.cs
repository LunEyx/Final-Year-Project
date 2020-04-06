using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public GameObject prefab;
    public float skillLife;
    public Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            animator.SetTrigger("IsAttack");
            Invoke("SkillInstantiate", 0.5f);
        }
    }

    void SkillInstantiate()
    {
        GameObject skill = Instantiate(prefab, transform.position + transform.forward * 2, transform.rotation);
        skill.GetComponent<Rigidbody>().velocity = transform.forward * 40;
        Destroy(skill, skillLife);
    }
}

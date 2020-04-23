using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Actor
{
    private const int MaxSkill = 4;

    private Rigidbody rb;
    private Animator animator;
    private Text hpText;

    private Spell[] spells = new Spell[MaxSkill];
    private Image[] spellIcons = new Image[MaxSkill];

    private int jumpCounter = 0;
    private float distanceToGround;
    
    public float magnitude = 1;
    private GameObject hud;
    public int gold = 50;

    public ExpSystem playerExpSystem = new ExpSystem();

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
        animator = GetComponentInChildren<Animator>();
        hud = GameObject.FindGameObjectWithTag("HUD");
        Transform hpObj = hud.transform.Find("Player Status").Find("HP");
        hpText = hpObj.GetComponentInChildren<Text>();
        hpBar = hpObj.Find("HP Bar").Find("hp_background").GetChild(0).GetComponentInChildren<Image>();
        for (int i = 0; i < MaxSkill; i++)
        {
            spellIcons[i] = hpObj.Find("Skill").GetComponentsInChildren<Image>()[i];
        }

        hpSystem = new HpSystem(100);
    }
    
    
    private void MovementControl()
    {
        if (Input.GetKey(KeyCode.W))
            rb.velocity = rb.transform.rotation * new Vector3(0, rb.velocity.y, 10);
        if (Input.GetKey(KeyCode.S))
            rb.velocity = rb.transform.rotation * new Vector3(0, rb.velocity.y, -10);
        if (Input.GetKey(KeyCode.A))
            rb.velocity = rb.transform.rotation * new Vector3(-10, rb.velocity.y, 0);
        if (Input.GetKey(KeyCode.D))
            rb.velocity = rb.transform.rotation * new Vector3(10, rb.velocity.y, 0);
        if (Input.GetKey(KeyCode.Space) && jumpCounter > 0)
        {
            jumpCounter--;
            rb.AddForce(new Vector3(0, 10f, 0), ForceMode.Impulse);
        }
    }

    private void Animation()
    {
        if (rb.velocity.magnitude > magnitude)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    private void CastSpell()
    {
        KeyCode[] spellKeys = { KeyCode.Q, KeyCode.E, KeyCode.R, KeyCode.F };
        for (int i = 0; i < 4; i++) {
            if (spells[i] == null)
            {
                continue;
            }
            if (!spells[i].IsCooldown() && Input.GetKey(spellKeys[i]))
            {
                spells[i].Cast();
                animator.SetTrigger("IsAttack");
                spellIcons[i].fillAmount = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
     
        MovementControl();
        Animation();
        CastSpell();

        if (transform.position.y < -5)
        {
            transform.position = new Vector3(30, 5, 0);
        }
    }

    public override void TakeDamage(int value)
    {
        base.TakeDamage(value);
        hpText.text = $"{hpSystem.GetHp()} / {hpSystem.GetMaxHp()}";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f))
        {
            jumpCounter = 1;
        }
    }

    public void LearnSpell(System.Type spellType, int index)
    {
        if (spells[index] != null)
        {
            Destroy(spells[index]);
        }
        Spell spell = gameObject.AddComponent(spellType) as Spell;
        spells[index] = spell;
        spell.SetIconContainer(spellIcons[index]);
    }
}

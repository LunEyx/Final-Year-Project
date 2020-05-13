using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player : Actor
{
    private const int MaxSkill = 4;

    private Rigidbody rb;
    private Animator animator;
    private Text hpText;
    private Text coinText;

    private Spell[] spells = new Spell[MaxSkill];
    private Image[] spellIcons = new Image[MaxSkill];
    
    private int jumpCounter = 0;
    private float distanceToGround;
    
    public float magnitude = 1;
    public GameObject clientHpBar;

    private GameObject hud;
    public int gold = 50;
    public int skillLearntCounter = 0;

    private int coin = 50;
    private GameObject popUpText;

    public ExpSystem expSystem;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Debug.Log("StartLocalPlayer");
        GameManager.SetLocalPlayer(this);
        GameManager.AddPlayer(this);
        Camera.main.GetComponent<CameraFollow>().SetTarget(transform);

        Transform hud = GameObject.FindGameObjectWithTag("HUD").transform;
        Transform playerStatus = hud.Find("Player Status");
        Transform hpObj = playerStatus.Find("HP");
        hpText = hpObj.GetComponentInChildren<Text>();
        hpBar = hpObj.Find("hp_background").GetChild(0).GetComponentInChildren<Image>();
        Transform skillObj = playerStatus.Find("Skill");
        for (int i = 0; i < MaxSkill; i++)
        {
            spellIcons[i] = skillObj.GetComponentsInChildren<Image>()[i];
        }
        Transform expObj = playerStatus.Find("Exp");
        Image expBar = expObj.Find("Background").GetComponentInChildren<Image>();
        Text expText = expObj.GetComponentInChildren<Text>();
        coinText = hud.Find("Coin").GetComponentInChildren<Text>();
        expSystem = new ExpSystem(expBar, expText);

        RefreshCoinHUD();
    }

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        popUpText = Resources.Load<GameObject>("CoinPopUp");
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
        animator = GetComponentInChildren<Animator>();
        
        hpSystem = new HpSystem(100);

        if (!isLocalPlayer)
        {
            clientHpBar.SetActive(true);
            hpBar = clientHpBar.GetComponentInChildren<Image>().GetComponentInChildren<Image>();
            GameManager.AddPlayer(this);
        }
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
        if (!isLocalPlayer) return;
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
        if (isLocalPlayer)
        {
            hpText.text = $"{hpSystem.GetHp()} / {hpSystem.GetMaxHp()}";
        }
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
        Debug.Log(spellType.Name);
        Debug.Log(index);
        if (spells[index] != null)
        {
            Destroy(spells[index]);
        }
        Spell spell = GetComponent(spellType) as Spell;
        Debug.Log(spell);
        spells[index] = spell;
        spell.SetIconContainer(spellIcons[index]);
        GameManager.UnlearntSpellList.Remove(spell.GetType().Name);
        GameManager.LearntSpellList.Add(spell.GetType().Name);
        skillLearntCounter++;
    } 

    public bool CanAfford(int value)
    {
        return coin >= value;
    }

    private void ShowCoinPopUp(int value)
    {
        GameObject obj = Instantiate(popUpText, coinText.transform);
        obj.GetComponent<Text>().text = value.ToString("+0;-#");
    }

    public void GainCoin(int value)
    {
        coin += value;
        ShowCoinPopUp(value);
        RefreshCoinHUD();
    }

    public void GainExp(int value)
    {
        expSystem.GainExp(value);
    }

    private void RefreshCoinHUD()
    {
        coinText.text = $"{coin}";
    }
}

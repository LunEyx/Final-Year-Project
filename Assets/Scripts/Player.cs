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
    private GameObject exitMenu;
    private Spell[] spells = new Spell[MaxSkill];
    private Image[] spellIcons = new Image[MaxSkill];
    private bool gotArmor = false;
    private int jumpCounter = 1;
    private float distanceToGround;
    private float movementSpeed = 0f;
    
    public float magnitude = 1;
    public GameObject clientHpBar;

    private GameObject hud;
    public int skillLearntCounter = 0;

    private int coin = 50;
    private GameObject popUpText;

    public ExpSystem expSystem;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
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
            rb.velocity = rb.transform.rotation * new Vector3(0, rb.velocity.y, 10 + movementSpeed);
        if (Input.GetKey(KeyCode.S))
            rb.velocity = rb.transform.rotation * new Vector3(0, rb.velocity.y, -10 - movementSpeed);
        if (Input.GetKey(KeyCode.A))
            rb.velocity = rb.transform.rotation * new Vector3(-10 - movementSpeed, rb.velocity.y, 0);
        if (Input.GetKey(KeyCode.D))
            rb.velocity = rb.transform.rotation * new Vector3(10 + movementSpeed, rb.velocity.y, 0);
        if (Input.GetKey(KeyCode.Space) && jumpCounter > 0)
        {
            jumpCounter--;
            StartCoroutine("Jump");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (exitMenu == null)
            {
                exitMenu = Instantiate(Resources.Load<GameObject>("Exit_Menu"));
            }
            else
            {
                Destroy(exitMenu);
            }
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
    }

    public override void TakeDamage(int value)
    {
        if (gotArmor) {
            if (value - 5 >= 0) {
                base.TakeDamage(value - 5);
            }
            else
            {
                base.TakeDamage(0);
            }
        }
        else
        {
            base.TakeDamage(value);
        }
        
        if (isLocalPlayer) {
            hpText.text = $"{hpSystem.GetHp()} / {hpSystem.GetMaxHp()}";
        }
    }

    private IEnumerator Jump()
    {
        rb.AddForce(new Vector3(0, 10f, 0), ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => { return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f); });
        jumpCounter = 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.5f))
        {
            jumpCounter = 1;
            StopCoroutine("Jump");
        }
    }
    
    public void LearnSpell(System.Type spellType, int index)
    {
        if (spells[index] != null)
        {
            Destroy(spells[index]);
        }
        Spell spell = GetComponent(spellType) as Spell;
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

    public void GetItem(int itemID)
    {
        switch (itemID)
        {
            case 1:
                gotArmor = true;
                break;
            case 2:
                hpSystem.IncreaseMaxHp(50);
                hpSystem.HealHp(50);
                hpText.text = $"{hpSystem.GetHp()} / {hpSystem.GetMaxHp()}";
                TakeDamage(0);
                break;
            case 3:
                Fireball.Damage+=10;
                FireNova.Damage += 10;
                Tornado.Damage += 10;
                Bubble.Damage += 10;
                Meteor.Damage += 10;
                break;
            case 4:
                movementSpeed += 10f;
                break;
            default:
                break;
        }
    }
}

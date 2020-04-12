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

    private Spell[] spells = new Spell[4];
    private Image[] spellIcons = new Image[4];

    private int maxHp = 100;
    private int jumpCounter = 0;
    private float turningSpeed = 400;
    private SpellCaster skill1;
    public float magnitude = 1;
    private GameObject hud;

    private float rotX;
    private float rotY;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        hud = GameObject.FindGameObjectWithTag("HUD");
        Transform hpObj = hud.transform.Find("Player Status").Find("HP");
        hpText = hpObj.GetComponentInChildren<Text>();
        hpBar = hpObj.Find("HP Bar").Find("hp_background").GetChild(0).GetComponentInChildren<Image>();
        skill1 = GetComponent<SpellCaster>();
        for (int i = 0; i < MaxSkill; i++)
        {
            spellIcons[i] = hpObj.Find("Skill").GetComponentsInChildren<Image>()[i];
        }

        hpSystem = new HpSystem(maxHp);
    }
    
    private void ViewControl()
    {
        Camera cam = Camera.main;
        transform.Rotate(0, cam.transform.rotation.y, 0);
        /*
        float horizontal = Input.GetAxis("Mouse X") * turningSpeed * Time.deltaTime;
        // transform.Rotate(0, horizontal, 0);
        Camera camera = GetComponentInChildren<Camera>();
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * turningSpeed * Time.deltaTime;
        rotX += mouseY * turningSpeed * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -80, 80);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        camera.transform.rotation = localRotation;
        //float vertical = Input.GetAxis("Mouse Y") * turningSpeed * Time.deltaTime;
        //camera.transform.Rotate(vertical, 0, 0);
        */
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
            if (spells[i].IsCooldown())
            {
                spellIcons[i].fillAmount = spells[i].GetCooldownTimerPercentage();
            }
            else if (Input.GetKey(spellKeys[i]))
            {
                spells[i].Cast();
                spellIcons[i].fillAmount = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ViewControl();
        MovementControl();
        Animation();
        CastSpell();
    }

    public override void TakeDamage(int value)
    {
        base.TakeDamage(value);
        hpText.text = $"{hpSystem.GetHp()} / {maxHp}";
    }

    void LateUpdate()
    {
        transform.LookAt(rb.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        jumpCounter += 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private Rigidbody rb;
    private Animator animator;
    private Text hpText;
    private Image hpBar;

    private int maxHp = 100;
    private int jumpCounter = 0;
    private float turningSpeed = 400;
    private SpellCaster skill1;
    private float skill1Cooldown;
    public float magnitude = 1;
    private GameObject hud;
    public Image skill1Icon;
    public HpSystem hpSystem;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        hud = GameObject.FindGameObjectWithTag("HUD");
        Transform hpObj = hud.transform.Find("Player Status").Find("HP");
        hpText = hpObj.GetComponentInChildren<Text>();
        hpBar = hpObj.Find("HP Bar").Find("hp_background").GetChild(0).GetComponentInChildren<Image>();
        skill1 = GetComponent<SpellCaster>();
        skill1Cooldown = skill1.GetCooldown();
        skill1Icon = hpObj.Find("Skill").GetComponentsInChildren<Image>()[0];

        hpSystem = new HpSystem(maxHp);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Mouse X") * turningSpeed * Time.deltaTime;
        transform.Rotate(0, horizontal, 0);
        
        if (Input.GetKey(KeyCode.W))
            rb.velocity = rb.transform.rotation * new Vector3(0, rb.velocity.y, 10);
        if (Input.GetKey(KeyCode.S))
            rb.velocity = rb.transform.rotation * new Vector3(0, rb.velocity.y, -10);
        if (Input.GetKey(KeyCode.A))
            rb.velocity = rb.transform.rotation * new Vector3(-10, rb.velocity.y, 0);
        if (Input.GetKey(KeyCode.D))
            rb.velocity = rb.transform.rotation * new Vector3(10, rb.velocity.y, 0);

        if (rb.velocity.magnitude > magnitude)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        if (Input.GetKey(KeyCode.Space) && jumpCounter>0)
        {
            jumpCounter--;
            rb.AddForce(new Vector3(0, 10f, 0), ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.Q) && !skill1.IsCooldown())
        {
            skill1.Cast();
            skill1Icon.fillAmount = 0;
        }
        else if (skill1.IsCooldown())
        {
            skill1Icon.fillAmount += 1 / skill1Cooldown * Time.deltaTime;
            if (skill1Icon.fillAmount >= 1)
            {
                skill1.Ready();
            }
        }

        hpText.text = $"{hpSystem.get_hp()} / {maxHp}";
        hpBar.fillAmount = hpSystem.currentLifePercentage();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public abstract class Spell : NetworkBehaviour
{
    protected Sprite icon;
    protected float cooldown;
    protected float minCooldown;
    protected float maxCooldown;
    private bool isCooldown = false;
    private float cooldownTimer;
    private Image iconContainer;
    

    protected void Update()
    {
        if (IsCooldown())
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldown)
            {
                isCooldown = false;
            }
            if (iconContainer != null)
            {
                iconContainer.fillAmount = GetCooldownTimerPercentage();
            }
        }
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    public float GetCooldownTimerPercentage()
    {
        return cooldownTimer / cooldown;
    }

    public float GetCooldownTimer()
    {
        return cooldownTimer;
    }

    public float GetCooldown()
    {
        return cooldown;
    }

    public void SetIconContainer(Image iconContainer)
    {
        iconContainer.sprite = icon;
        this.iconContainer = iconContainer;
    }

    public bool IsCooldown()
    {
        return isCooldown;
    }


    public virtual void Cast()
    {
        Debug.Log("cast");
        cooldownTimer = 0;
        if (maxCooldown != 0)
        {
            cooldown = Random.Range(minCooldown, maxCooldown);
        }
        isCooldown = true;
        if (iconContainer != null)
        {
            iconContainer.fillAmount = 0;
        }
        Debug.Log("cast Done");
    }

    public static Sprite getUnlearntSpellIcon(string spellName)
    {
        return Resources.Load<Sprite>("Icons/" + spellName);
    }

    public virtual void Upgrade()
    {

    }
}

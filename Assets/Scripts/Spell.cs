using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Spell : MonoBehaviour
{
    protected Image icon;
    private bool isCooldown = false;
    protected float cooldown;
    private float cooldownTimer;

    protected void Update()
    {
        if (IsCooldown())
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldown)
            {
                isCooldown = false;
            }
        }
    }

    public Image GetIcon()
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

    public bool IsCooldown()
    {
        return isCooldown;
    }

    public virtual void Cast()
    {
        cooldownTimer = 0;
    }
}

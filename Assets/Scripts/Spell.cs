using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    protected Sprite icon;
    private bool isCooldown = false;
    protected float cooldown;
    private float cooldownTimer;

    protected virtual void Update()
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

    public bool IsCooldown()
    {
        return isCooldown;
    }

    public virtual void Cast()
    {
        cooldownTimer = 0;
        isCooldown = true;
    }
}

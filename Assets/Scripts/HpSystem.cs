using UnityEngine;

public class HpSystem
{
    private int maxHp;
    private int currentHp;

    public HpSystem(int hp)
    {
        maxHp = hp;
        currentHp = hp;
    }

    public int GetHp()
    {
        return currentHp;
    }

    public int GetMaxHp()
    {
        return maxHp;
    }

    public void TakeDamage(int value)
    {
        currentHp -= value;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
    }

    public float CurrentLifePercentage()
    {
        return (float)currentHp / maxHp;
    }

    public void IncreaseMaxHp(int num)
    {
        maxHp += num;
    }

    public void HealHp(int num)
    {
        currentHp += num;
    }
}

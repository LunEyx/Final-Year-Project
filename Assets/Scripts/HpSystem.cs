using System;

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

    public void TakeDamage(int value)
    {
        currentHp -= value;
        currentHp = Math.Max(0, Math.Min(currentHp, maxHp));
    }

    public float CurrentLifePercentage()
    {
        return (float)currentHp / maxHp;
    }
}

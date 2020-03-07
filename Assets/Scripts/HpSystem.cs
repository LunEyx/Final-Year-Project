using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpSystem 
{
    private int max_hp;
    private int current_hp;

    public HpSystem(int hp)
    {
        this.max_hp = hp;
        this.current_hp = hp;

    }

    public int get_hp()
    {
        return current_hp;
    }

    public void damage(int hp_damage)
    {
        current_hp -= hp_damage;
        if (current_hp < 0)
            current_hp = 0;
    }

    public float currentLifePercentage()
    {
        return (float)current_hp / max_hp;
    }


}

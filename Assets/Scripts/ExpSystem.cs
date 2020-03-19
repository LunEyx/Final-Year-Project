using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpSystem 
{
    private int levelExpLimit = 100;

    private int exp = 0;

    private int level = 1;

    private float levelExpScale = 1.1f;

    public void levelUp()
    {
        this.level++;
    }

    public void gainExp(int expAmt)
    {
        Debug.Log("player gain: " + expAmt + " & current exp = " + exp + " & current level = " + level);
        exp += expAmt;
        if (exp > levelExpLimit)
        {
            exp -= levelExpLimit;
            levelUp();
            levelExpLimit = (int)(levelExpLimit * levelExpScale);
        }
    }
   
}

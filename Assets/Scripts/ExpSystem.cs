using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpSystem 
{
    private int levelExpLimit = 100;
    private int exp = 0;
    private int level = 1;
    private const float levelExpScale = 1.1f;

    public void LevelUp()
    {
        level++;
    }

    public void GainExp(int expAmt)
    {
        exp += expAmt;
        if (exp > levelExpLimit)
        {
            exp -= levelExpLimit;
            LevelUp();
            levelExpLimit = (int)(levelExpLimit * levelExpScale);
        }
    }
   
}

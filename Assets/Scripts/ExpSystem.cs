using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpSystem : MonoBehaviour
{
    private int levelExpLimit = 10;
    private int exp = 0;
    private int level = 1;
    private const float levelExpScale = 1.1f;
    public GameObject levelUpUI;

    public void LevelUp()
    {
        level++;
        levelUpUI.SetActive(true);
    }

    public void GainExp(int expAmt)
    {
        exp += expAmt;
        if (exp >= levelExpLimit)
        {
            exp -= levelExpLimit;
            LevelUp();
            levelExpLimit = (int)(levelExpLimit * levelExpScale);
        }
    }

    public int GetLevel()
    {
        return level;
    }
   
}

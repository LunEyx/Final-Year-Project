using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpSystem 
{
    private int levelExpLimit = 100;
    private int exp = 0;
    private int level = 1;
    private const float levelExpScale = 1.1f;
    private Image expBar;
    private Text expText;

    public ExpSystem(Image expBar, Text expText)
    {
        this.expBar = expBar;
        this.expText = expText;
        expBar.fillAmount = (float)exp / levelExpLimit;
        expText.text = $"{exp} / {levelExpLimit}";
    }

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
        expBar.fillAmount = (float)exp / levelExpLimit;
        expText.text = $"{exp} / {levelExpLimit}";
    }
}

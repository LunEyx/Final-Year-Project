using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpSystem : MonoBehaviour
{
    private int levelExpLimit = 10;
    private int exp = 0;
    private int level = 1;
    private const float levelExpScale = 1.1f;

    public GameObject levelUpUI;

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
        levelUpUI.SetActive(true);
        exp -= levelExpLimit;
        levelExpLimit = (int)(levelExpLimit * levelExpScale);
    }

    public void GainExp(int expAmt)
    {
        exp += expAmt;
        if (exp >= levelExpLimit)
        {  
            LevelUp();
        }
        expBar.fillAmount = (float)exp / levelExpLimit;
        expText.text = $"{exp} / {levelExpLimit}";
    }

    public int GetLevel()
    {
        return level;
    }
   

}

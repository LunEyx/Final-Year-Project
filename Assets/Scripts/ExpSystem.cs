﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpSystem
{
    private int levelExpLimit = 60;
    private int exp = 0;
    private int level = 1;
    private const float levelExpScale = 1.1f;

    private GameObject levelUpUI;

    private Image expBar;
    private Text expText;

    public ExpSystem(Image expBar, Text expText)
    {
        this.expBar = expBar;
        this.expText = expText;
        expBar.fillAmount = (float)exp / levelExpLimit;
        expText.text = $"{exp} / {levelExpLimit}";
        levelUpUI = Resources.Load<GameObject>("LevelUpUI");
    }


    public void LevelUp()
    {
        level++;
        GameObject.Instantiate(levelUpUI);
        exp -= levelExpLimit;
        levelExpLimit = (int)(levelExpLimit * levelExpScale);
        GameManager.GetLocalPlayer().TakeDamage(-GameManager.GetLocalPlayer().getMaxHp()/10);
    }

    public void GainExp(int expAmt)
    {
        exp += expAmt;
        if (exp >= levelExpLimit)
        {  
            LevelUp();
        }
        expBar.fillAmount = ((float)exp) / levelExpLimit;
        expText.text = $"{exp} / {levelExpLimit}";
    }

    public int GetLevel()
    {
        return level;
    }
   
    public void SetExpHud(Image expBar, Text expText)
    {
        this.expBar = expBar;
        this.expText = expText;
        expBar.fillAmount = (float)exp / levelExpLimit;
        expText.text = $"{exp} / {levelExpLimit}";
    }
}

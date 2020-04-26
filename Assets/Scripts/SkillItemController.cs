using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillItemController : MonoBehaviour
{
    public Player player;
    public string spellName;
    public GameObject levelUpUI;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentsInChildren<Text>()[0].text = spellName;
        if ((player.GetComponent(spellName) as Spell)!= null)
        {
            gameObject.GetComponentsInChildren<Image>()[1].sprite = (player.GetComponent(spellName) as Spell).GetIcon();
            gameObject.GetComponentsInChildren<Text>()[1].text = "Upgrade!";
            gameObject.GetComponentsInChildren<Text>()[1].color = Color.green;
            var method = Type.GetType(spellName).GetMethod("GetUpgradeDescription");
            if (method != null)
                gameObject.GetComponentInChildren<ToolTip>().tooltipText = method.Invoke(null, null).ToString();
        }
        else
        {
            gameObject.GetComponentsInChildren<Image>()[1].sprite = Spell.getUnlearntSpellIcon(spellName);
            gameObject.GetComponentsInChildren<Text>()[1].text = "New!";
            gameObject.GetComponentsInChildren<Text>()[1].color = Color.red;
            var method = Type.GetType(spellName).GetMethod("GetNewDescription");
            if (method != null)
                gameObject.GetComponentInChildren<ToolTip>().tooltipText = method.Invoke(null,null).ToString();
        }
            
    }

    public void Chosen()
    {
        if (gameObject.GetComponentsInChildren<Text>()[1].text == "New!")
        {
            player.LearnSpell(Type.GetType(spellName), player.skillLearntCounter);
        }
        else
        {
            (player.GetComponent(spellName) as Spell).Upgrade();
        }

        levelUpUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

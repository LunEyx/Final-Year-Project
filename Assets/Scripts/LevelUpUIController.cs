using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpUIController : MonoBehaviour
{
    private Rect screen;
    private GameObject skillItemTemplate;
    private GameObject levelUpUI;
    private int offsetCounter = 0;
    private Player player;
    public GameObject tooltip;
    public int widthOffset = 100;
    public int heightOffset = -25;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.GetCurrentPlayer();
        skillItemTemplate = Resources.Load<GameObject>("Skill_Container");
        levelUpUI = gameObject;
        for (int i = 0;i < (5-player.skillLearntCounter); i++)
        {
            createSkillItemTemplate(GameManager.UnlearntSpellList[i], offsetCounter++);
            
        }
        for(int i = 0;i < player.skillLearntCounter; i++)
        {
            createSkillItemTemplate(GameManager.LearntSpellList[i], offsetCounter++);
            
        }
        tooltip.SetActive(false);
        screen = new Rect(0, 0, Screen.width, Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        //if (screen.Contains(new Vector2(tooltip.transform))
        tooltip.transform.position = Input.mousePosition + new Vector3(widthOffset, heightOffset, 0);
    }

    private void createSkillItemTemplate(string spellName, int offset)
    {
        GameObject container = Instantiate(skillItemTemplate, transform);
        container.GetComponent<Transform>().localPosition = new Vector3(-580 + offset * 300, -41, 0);
        container.GetComponent<SkillItemController>().player = player;
        container.GetComponent<SkillItemController>().spellName = spellName;
        container.GetComponent<SkillItemController>().levelUpUI = levelUpUI;
        container.GetComponentInChildren<ToolTip>().tooltip = tooltip;
    }
}

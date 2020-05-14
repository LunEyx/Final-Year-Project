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
    public int widthOffset = 200;
    public int heightOffset = -25;

    // Start is called before the first frame update
    void Start()
    {
        int temp = 0;
        player = GameManager.GetLocalPlayer();
        skillItemTemplate = Resources.Load<GameObject>("Skill_Container");
        levelUpUI = gameObject;
        Debug.Log(GameManager.LearntSpellList.Count);
        List<int> randList = RandomList.getRandomIntList(0, GameManager.UnlearntSpellList.Count-1, Mathf.Max(3 - player.skillLearntCounter, 0));
        for (int i = 0;i < Mathf.Max(3-player.skillLearntCounter,0); i++)
        {
            createSkillItemTemplate(GameManager.UnlearntSpellList[randList[i]], offsetCounter++);
            temp++;
        }
        randList = RandomList.getRandomIntList(0, GameManager.LearntSpellList.Count - 1, Mathf.Max(3 - temp, 0));
        for (int i = 0;i < Mathf.Max(3-temp, 0) ; i++)
        {
            createSkillItemTemplate(GameManager.LearntSpellList[randList[i]], offsetCounter++);
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
        container.GetComponent<Transform>().localPosition = new Vector3(-450 + offset * 450, -41, 0);
        container.GetComponent<SkillItemController>().player = player;
        container.GetComponent<SkillItemController>().spellName = spellName;
        container.GetComponent<SkillItemController>().levelUpUI = levelUpUI;
        container.GetComponentInChildren<ToolTip>().tooltip = tooltip;
    }
}

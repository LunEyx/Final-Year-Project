using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryScene : MonoBehaviour
{
    private int nextCount = 1;
    public GameObject story_2;
    public GameObject story_3;
    public GameObject story_4;
    public GameObject story_5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (nextCount)
        {
            case 2:
                story_2.SetActive(true);
                break;
            case 3:
                story_3.SetActive(true);
                break;
            case 4:
                story_4.SetActive(true);
                break;
            case 5:
                story_5.SetActive(true);
                break;
            case 6:
                SceneManager.LoadScene("SampleScene");
                break;
        }
    }

    public void showStory()
    {
        nextCount++;
    }
}

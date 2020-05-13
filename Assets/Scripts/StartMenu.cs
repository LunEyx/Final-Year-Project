using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("StoryScene");
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}

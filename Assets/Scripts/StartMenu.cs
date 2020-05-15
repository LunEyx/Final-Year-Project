using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class StartMenu : MonoBehaviour
{
    public void GameStart()
    {
        GameManager.Clear();
        NetworkManager.singleton.networkPort = 7778;
        NetworkManager.singleton.StopHost();
        SceneManager.LoadScene("StoryScene");
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void HostNormal()
    {
        GameManager.Clear();
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.StopHost();
        NetworkManager.singleton.StartHost();
    }

    public void HostAdvanced()
    {
        GameManager.Clear();
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.StopHost();
        NetworkManager.singleton.StartHost();;
    }

    public void Join()
    {
        GameManager.Clear();
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.StopHost();
        NetworkManager.singleton.StartClient();
    }
}

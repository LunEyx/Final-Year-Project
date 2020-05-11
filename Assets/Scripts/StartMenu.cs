using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class StartMenu : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("SampleScene");
        Destroy(GameObject.Find("NetworkManager"));
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void HostNormal()
    {
        GameManager.Clear();
        NetworkManager.singleton.StartHost();
    }

    public void HostAdvanced()
    {
        GameManager.Clear();
        NetworkManager.singleton.StartHost();
    }

    public void Join()
    {
        GameManager.Clear();
        NetworkManager.singleton.StartClient();
    }
}

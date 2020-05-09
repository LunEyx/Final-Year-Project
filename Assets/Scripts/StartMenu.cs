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
        NetworkManager.singleton.StartHost();
    }

    public void HostAdvanced()
    {
        NetworkManager.singleton.StartHost();
    }

    public void Join()
    {
        NetworkManager.singleton.StartClient();
    }
}

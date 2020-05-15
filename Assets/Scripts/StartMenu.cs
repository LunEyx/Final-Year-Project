using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class StartMenu : MonoBehaviour
{
    public void GameStartNormal()
    {
        Slime.Difficulty = 1;
        Boss.Difficulty = 1;
        GameManager.Clear();
        NetworkManager.singleton.networkPort = 7778;
        NetworkManager.singleton.StopHost();
        SceneManager.LoadScene("StoryScene");
    }

    public void GameStartAdvanced()
    {
        Slime.Difficulty = 2;
        Boss.Difficulty = 2;
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
        Slime.Difficulty = 1;
        Boss.Difficulty = 1;
        GameManager.Clear();
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.StopHost();
        NetworkManager.singleton.StartHost();
    }

    public void HostAdvanced()
    {
        Slime.Difficulty = 2;
        Slime.Difficulty = 2;
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

    public void CreateOnlineMatch()
    {
        NetworkManager.singleton.StartMatchMaker();
        NetworkManager.singleton.matchMaker.CreateMatch("match name", 2, true, "password", "", "", 0, 0, NetworkManager.singleton.OnMatchCreate);
    }

    public void JoinOnlineMatch()
    {
        NetworkManager.singleton.StartMatchMaker();
        var match = NetworkManager.singleton.matches[0];
        NetworkManager.singleton.matchMaker.JoinMatch(match.networkId, "password", "", "", 0, 0, NetworkManager.singleton.OnMatchJoined);
    }
}

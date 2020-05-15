using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class StartMenu : MonoBehaviour
{
    public void GameStartNormal()
    {
        GameManager.Clear();
        Slime.Difficulty = 1;
        Boss.Difficulty = 1;
        NetworkManager.singleton.networkPort = 7778;
        NetworkManager.singleton.StopHost();
        SceneManager.LoadScene("StoryScene");
    }

    public void GameStartAdvanced()
    {
        GameManager.Clear();
        Slime.Difficulty = 2;
        Boss.Difficulty = 2;
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
        Slime.Difficulty = 1;
        Boss.Difficulty = 1;
        GameManager.isMultiplayer = true;
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.StopHost();
        NetworkManager.singleton.StartHost();
    }

    public void HostAdvanced()
    {
        GameManager.Clear();
        Slime.Difficulty = 2;
        Boss.Difficulty = 2;
        GameManager.isMultiplayer = true;
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.StopHost();
        NetworkManager.singleton.StartHost();
    }

    public void Join()
    {
        GameManager.Clear();
        GameManager.isMultiplayer = true;
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.StopHost();
        NetworkManager.singleton.StartClient();
    }

    public void CreateOnlineMatch()
    {
        GameManager.Clear();
        Slime.Difficulty = 2;
        Boss.Difficulty = 2;
        GameManager.isMultiplayer = true;
        NetworkManager.singleton.StartMatchMaker();
        NetworkManager.singleton.matchMaker.CreateMatch("", 2, true, "password", "", "", 0, 0, NetworkManager.singleton.OnMatchCreate);
    }

    public void JoinOnlineMatch()
    {
        GameManager.Clear();
        Slime.Difficulty = 2;
        Boss.Difficulty = 2;
        GameManager.isMultiplayer = true;
        NetworkManager.singleton.StartMatchMaker();
        NetworkManager.singleton.matchMaker.ListMatches(0, 20, "", false, 0, 0, NetworkManager.singleton.OnMatchList);
        if (NetworkManager.singleton.matches != null)
        {
            int numberOfRoom = NetworkManager.singleton.matches.Count;
            if (numberOfRoom == 0) return;
            var match = NetworkManager.singleton.matches[Random.Range(0, numberOfRoom)];
            NetworkManager.singleton.matchMaker.JoinMatch(match.networkId, "password", "", "", 0, 0, NetworkManager.singleton.OnMatchJoined);
        }
    }
}

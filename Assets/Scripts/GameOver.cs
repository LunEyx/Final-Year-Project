using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameOver : MonoBehaviour
{
    public void GameOverClick()
    {
        Text text = GetComponentsInChildren<Text>()[1];
        if (GameManager.GetPlayers().Count > 0 && text.text == "Watch other player")
        {
            Camera.main.GetComponent<CameraFollow>().SetTarget(GameManager.GetPlayers()[0].transform);
        }
        else
        {
            NetworkManager.singleton.StopClient();
        }
    }
}

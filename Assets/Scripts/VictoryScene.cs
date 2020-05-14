using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class VictoryScene : MonoBehaviour
{
    public void BackToTitle()
    {
        NetworkManager.singleton.StopHost();
        NetworkManager.singleton.StopClient();
    }
}

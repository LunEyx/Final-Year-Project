﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class ExitMenu : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToTitle()
    {
        NetworkManager.singleton.StopClient();
    }

    
}

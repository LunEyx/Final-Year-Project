using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets instance;

    public static GameAssets Instance
    {
        get
        {
            if (instance == null)
                instance = (Instantiate(Resources.Load("GameAssetsForItems")) as GameObject).GetComponent<GameAssets>();
            return instance;
        }
    }

    public Sprite item_Heart;
}

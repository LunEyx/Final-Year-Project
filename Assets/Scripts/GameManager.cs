﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float turningSpeed = 400;
    private Player[] players;
    private bool isGifted = false;
    private static List<Item> itemList = new List<Item>();
    public static bool CameraMove = true;
    public static List<Item> ItemList {
        get { return itemList; }
    }

    private void Start()
    {
        GameObject[] playerObjs = GameObject.FindGameObjectsWithTag("Player");
        players = new Player[playerObjs.Length];
        for (int i = 0; i < playerObjs.Length; i++)
        {
            players[i] = playerObjs[i].GetComponent<Player>();
        }

        TextAsset itemData = Resources.Load<TextAsset>("item list");
        string[] data = itemData.text.Split( '\n' );
        for (int i = 1; i < data.Length; i++)
        {
            string[] tempitem = data[i].Split(',');
            Sprite itemIcon = Resources.Load<Sprite>(tempitem[0]);
            itemList.Add(new Item(tempitem[0], tempitem[1], itemIcon, tempitem[2], false));
        }
    }

    private void Update()
    {
        if (CameraMove)
            ViewControl();
        if (!isGifted) {
            foreach (Player player in players)
            {
                player.LearnSpell(typeof(Fireball), 0);
                player.LearnSpell(typeof(Bubble), 1);
                player.LearnSpell(typeof(Tornado), 2);
                player.LearnSpell(typeof(Meteor), 3);
            }
            isGifted = true;
        }
    }

    private void ViewControl()
    {
        float horizontal = Input.GetAxis("Mouse X") * turningSpeed * Time.deltaTime;
        players[0].transform.Rotate(0, horizontal, 0);
    }
}

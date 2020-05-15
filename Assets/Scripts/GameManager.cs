using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    private bool levelStarted = false;
    private bool allEnemySpawned = false;
    private bool isLocationSet = false;
    private static bool isGifted = false;
    private float turningSpeed = 400;
    public GameObject shopUI;
    private static bool dataLoaded = false;
    private static List<Player> players = new List<Player>();
    private static Player localPlayer;
    private static List<Item> itemList = new List<Item>();
    private static List<string> unlearntSpellList = new List<string>();
    private static List<string> learntSpellList = new List<string>();
    public int level = 0;
    public static bool isMultiplayer = false;
    public static bool CameraMove = true;
    
    public static List<Item> ItemList {
        get { return itemList; }
    }
    public static List<string> UnlearntSpellList
    {
        get { return unlearntSpellList; }
    }
    public static List<string> LearntSpellList
    {
        get { return learntSpellList; }
    }

    private void Start()
    {
        if (localPlayer == null)
        {
            ClientScene.AddPlayer(0);
        }
        else
        {
            localPlayer.Reload();
        }

        if (!dataLoaded)
        {
            readItemData();
            readSkillData();
            dataLoaded = true;
        }
    }

    private void Update()
    {
        if (!localPlayer) return;

        if (!isLocationSet)
        {
            InitializePosition();
            isLocationSet = true;
        }

        if (!isGifted)
        {
            localPlayer.LearnSpell(typeof(Fireball), 0);
            localPlayer.LearnSpell(typeof(Tornado), 1);
            isGifted = true;
        }

        if (level < 0) return;
        if (isServer)
        {
            if (!levelStarted)
            {
                levelStarted = true;
                LevelStart();
            }

            if (allEnemySpawned && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                NextStage();
            }
        }

        if (CameraMove) {
            ViewControl();
        }
    }

    public static void Clear()
    {
        players = new List<Player>();
        localPlayer = null;
        CameraMove = true;
        isGifted = false;
        itemList = new List<Item>();
        unlearntSpellList = new List<string>();
        learntSpellList = new List<string>();
        dataLoaded = false;
        isMultiplayer = false;
        Fireball.Damage = 5;
        Bubble.Damage = 0;
        Tornado.Damage = 0;
        FireNova.Damage = 10;
        Meteor.Damage = 20;
    }

    public static List<Player> GetPlayers()
    {
        return players;
    }

    public static void SetLocalPlayer(Player player)
    {
        localPlayer = player;
    }

    public static Player GetLocalPlayer()
    {
        return localPlayer;
    }

    private void ViewControl()
    {
        float horizontal = Input.GetAxis("Mouse X") * turningSpeed * Time.deltaTime;
        GetLocalPlayer().transform.Rotate(0, horizontal, 0);
    }

    private void readItemData()
    {
        TextAsset itemData = Resources.Load<TextAsset>("item list");
        string[] data = itemData.text.Split('\n');
        for (int i = 1; i < data.Length; i++)
        {
            string[] tempitem = data[i].Split(',');
            Sprite itemIcon = Resources.Load<Sprite>("Item Icons/"+tempitem[0]);
            itemList.Add(new Item(tempitem[0], tempitem[1], itemIcon, tempitem[2], int.Parse(tempitem[3]), false));
        }
    }

    private void readSkillData()
    {
        TextAsset spellData = Resources.Load<TextAsset>("SpellList");
        string[] data = spellData.text.Split(',');
        
        for (int i = 1; i < data.Length; i++)
        {
            unlearntSpellList.Add(data[i]);
        }
    }

    public static void AddPlayer(Player player)
    {
        players.Add(player);
    }

    public static void RemovePlayer(Player player)
    {
        players.Remove(player);
    }

    private void NextStage()
    {
        switch (level)
        {
            case 1:
                NetworkManager.singleton.ServerChangeScene("RockScene");
                break;
            case 2:
                NetworkManager.singleton.ServerChangeScene("BossSence");
                break;
            default:
                Debug.Log("Next Stage Not Set");
                break;
        }
    }

    private void InitializePosition()
    {
        switch (level)
        {
            case 0:
                localPlayer.transform.position = new Vector3(30, 10, 0);
                break;
            case 1:
                localPlayer.transform.position = new Vector3(0, 0, 0);
                break;
            case 2:
                localPlayer.transform.position = new Vector3(70, 3, 70);
                break;
            case 3:
                localPlayer.transform.position = new Vector3(0, 5, 0);
                break;
            default:
                break;
        }
    }

    private void LevelStart()
    {
        switch (level)
        {
            case 1:
                StartCoroutine("Level1");
                break;
            case 2:
                StartCoroutine("Level2");
                break;
            default:
                Debug.Log("Level Not Set");
                break;
        }
    }

    private IEnumerator Level1()
    {
        yield return new WaitForSeconds(10);
        for (int i = 0; i < 1; i++) // TODO: 3
        {
            SpawnPoint.EnableSpawn = true;
            yield return new WaitForSeconds(20);
            SpawnPoint.EnableSpawn = false;
            yield return new WaitUntil(() => { return (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) ? true : false; });
            yield return new WaitForSeconds(5);
        }
        allEnemySpawned = true;
    }

    private IEnumerator Level2()
    {
        yield return new WaitForSeconds(10);
        for (int i=0; i< 1; i++) // TODO: 5
        {  
            SpawnPoint.EnableSpawn = true;
            SpawnPoint.spawnTime = 2f;
            yield return new WaitForSeconds(20);
            SpawnPoint.EnableSpawn = false;
            yield return new WaitForSeconds(20);
        }
        allEnemySpawned = true;
    }
}

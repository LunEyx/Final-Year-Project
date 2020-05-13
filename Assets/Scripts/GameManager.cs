using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    public int level = 0;
    private bool levelStarted = false;
    private bool allEnemySpawned = false;
    private bool isGifted = false;
    private float turningSpeed = 400;
    private static bool dataLoaded = false;
    private static List<Player> players = new List<Player>();
    private static Player localPlayer;
    private static List<Item> itemList = new List<Item>();
    private static List<string> unlearntSpellList = new List<string>();
    private static List<string> learntSpellList = new List<string>();
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
        ClientScene.AddPlayer(0);
        InitializePosition();

        if (!dataLoaded)
        {
            readItemData();
            readSkillData();

            TextAsset itemData = Resources.Load<TextAsset>("item list");
            string[] data = itemData.text.Split('\n');
            for (int i = 1; i < data.Length; i++)
            {
                string[] tempitem = data[i].Split(',');
                Sprite itemIcon = Resources.Load<Sprite>(tempitem[0]);
                itemList.Add(new Item(tempitem[0], tempitem[1], itemIcon, tempitem[2], int.Parse(tempitem[3]), false));
            }

            dataLoaded = true;
        }
    }

    private void Update()
    {
        if (!localPlayer) return;

        if (!isGifted)
        {
            localPlayer.LearnSpell(typeof(Fireball), 0);
            localPlayer.LearnSpell(typeof(Bubble), 1);
            localPlayer.LearnSpell(typeof(Tornado), 2);
            localPlayer.LearnSpell(typeof(Meteor), 3);
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
                Debug.Log("Level Complete");
                NextStage();
            }
        }

        if (CameraMove) {
            ViewControl();
        }

        if (!isGifted) {
            foreach (Player player in players)
            {
                player.LearnSpell(typeof(Fireball), 0);
                player.LearnSpell(typeof(Bubble), 1);
                player.LearnSpell(typeof(Tornado), 2);
                player.LearnSpell(typeof(FireNova), 3);
            }
            isGifted = true;
        }
    }

    public static void Clear()
    {
        players = new List<Player>();
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
            Sprite itemIcon = Resources.Load<Sprite>(tempitem[0]);
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

    private void NextStage()
    {
        switch (level)
        {
            case 1:
                SceneManager.LoadScene("NewScene");
                break;
            case 2:
                SceneManager.LoadScene("RockScene");
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
            case 2:
                localPlayer.transform.position = new Vector3(85, 3, 85);
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
        for (int i = 0; i < 3; i++)
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
        for (int i=0; i< 5; i++)
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

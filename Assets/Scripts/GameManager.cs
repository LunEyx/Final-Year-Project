using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public int level = 0;
    private float turningSpeed = 400;
    private static List<Player> players = new List<Player>();
    private bool isGifted = false;
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
        readItemData();
        readSkillData();

        TextAsset itemData = Resources.Load<TextAsset>("item list");
        string[] data = itemData.text.Split( '\n' );
        for (int i = 1; i < data.Length; i++)
        {
            string[] tempitem = data[i].Split(',');
            Sprite itemIcon = Resources.Load<Sprite>(tempitem[0]);
            itemList.Add(new Item(tempitem[0], tempitem[1], itemIcon, tempitem[2], false));
        }

        InitializeLevel();
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
                //player.LearnSpell(typeof(Meteor), 3);
            }
            isGifted = true;
        }
    }

    public static List<Player> GetPlayers()
    {
        return players;
    }

    public static Player GetCurrentPlayer()
    {
        // TODO: modify this for multiplayer get player
        return players[0];
    }

    private void ViewControl()
    {
        float horizontal = Input.GetAxis("Mouse X") * turningSpeed * Time.deltaTime;
        players[0].transform.Rotate(0, horizontal, 0);
    }

    private void readItemData()
    {
        TextAsset itemData = Resources.Load<TextAsset>("item list");
        string[] data = itemData.text.Split('\n');
        for (int i = 1; i < data.Length; i++)
        {
            string[] tempitem = data[i].Split(',');
            Sprite itemIcon = Resources.Load<Sprite>(tempitem[0]);
            itemList.Add(new Item(tempitem[0], tempitem[1], itemIcon, tempitem[2], false));
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

    public void AddPlayer(Player player)
    {
        players.Add(player);
        Camera.main.GetComponent<CameraFollow>().SetTarget(player.transform);
    }

    private Player SpawnPlayer(Vector3 position, Quaternion rotation)
    {
        GameObject prefab = Resources.Load<GameObject>("Player");
        GameObject obj = Instantiate(prefab, position, rotation);
        return obj.GetComponent<Player>();
    }

    private void InitializeLevel()
    {
        switch (level)
        {
            case 1:
                InitializeLevel1();
                break;
            default:
                Debug.Log("Level Not Set");
                break;
        }
    }

    private void InitializeLevel1()
    {
        Player player = SpawnPlayer(new Vector3(35, 10, 0), Quaternion.identity);
        AddPlayer(player);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Meteor : Spell
{
    protected GameObject prefab;
    protected float duration;
    protected float projectileSpeed;
    private int meteorCounter = 0;
    public int MeteorNum = 20;
    public const float Radius = 8;
    public static int Damage = 10;
    public static string UpgradeDes = "Increase the number of meteors by 10!";
    public static string NewDes = "Summon meteors to attack enemies!";

    private void Awake()
    {
        icon = Resources.Load<Sprite>("Icons/Meteor");
        prefab = Resources.Load<GameObject>("Meteor");
        duration = 5f;
        cooldown = 3f;
    }

    public override void Cast()
    {
        base.Cast();
        InvokeRepeating("Instantiate", 0, 0.1f);
    }

    private void Instantiate()
    {
        CmdInstantiate(prefab.name);
    }

    [Command]
    protected virtual void CmdInstantiate(string prefabName)
    {
        float randPosX = Random.Range(0 - Radius, Radius);
        float randPosZ = Random.Range(0 - Radius, Radius);
        GameObject meteor = Instantiate(
            prefab,
            new Vector3(transform.position.x + randPosX, transform.position.y + 10f, transform.position.z + randPosZ),
            Quaternion.Euler(90, 0, 0));
        meteor.GetComponent<Rigidbody>().velocity = transform.up * -20;
        meteorCounter++;

        if (meteorCounter >= MeteorNum)
        {
            CancelInvoke();
            meteorCounter = 0;
        }

        NetworkServer.Spawn(meteor);

        Destroy(meteor, duration);
    }

    public override void Upgrade()
    {
        MeteorNum += 10;
    }

    public static string GetNewDescription()
    {
        return NewDes;
    }

    public static string GetUpgradeDescription()
    {
        return UpgradeDes;
    }
}

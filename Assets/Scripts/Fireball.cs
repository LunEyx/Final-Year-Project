using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Fireball : ProjectileSpell
{
    public static int Damage = 5;
    public static string UpgradeDes = "Increase the damage by 5!";
    public static string NewDes = "Shoot a fireball that will explode when hit an enemy!";
    private void Awake()
    {
        prefab = Resources.Load<GameObject>("Fireball");
        icon = Resources.Load<Sprite>("Icons/Fireball");
        duration = 5f;
        cooldown = 2f;
        projectileSpeed = 40f;
    }

    public override void Upgrade()
    {
        Damage += 5;
    }

    public static string GetNewDescription()
    {
        return NewDes;
    }

    public static string GetUpgradeDescription()
    {
        return UpgradeDes;
    }

    public void Torched()
    {
        cooldown = 0.5f;
    }

}

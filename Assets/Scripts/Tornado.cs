using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : ProjectileSpell  
{
    public static int Damage = 0;
    public static string UpgradeDes = "Increase the damage by 3!";
    public static string NewDes = "Cast a tornado that will attract enemies!";

    private void Awake()
    {
        prefab = Resources.Load<GameObject>("Tornado");
        icon = Resources.Load<Sprite>("Icons/Tornado");
        duration = 5f;
        cooldown = 3f;
        projectileSpeed = 20f;
    }

    public override void Upgrade()
    {
        Damage += 3;
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

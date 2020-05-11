﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Fireball : ProjectileSpell
{
    public static int Damage = 10;
    public static string UpgradeDes = "Increase the damage by 10!";
    public static string NewDes = "Shoot a fireball that will explode when hit an enemy!";
    private void Awake()
    {
        icon = Resources.Load<Sprite>("Icons/Fireball");
        prefab = Resources.Load<GameObject>("Fireball");
        duration = 5f;
        cooldown = 3f;
        projectileSpeed = 40f;
    }

    public override void Upgrade()
    {
        Damage += 10;
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

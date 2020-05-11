using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : ProjectileSpell
{
    public static float BubbledDuration = 5f;
    public static string UpgradeDes = "Increase the stun duration by 1 second!";
    public static string NewDes = "Shoot a bubble that stuns an enemy!";

    private void Awake()
    {
        icon = Resources.Load<Sprite>("Icons/SpellBookPreface_05");
        prefab = Resources.Load<GameObject>("Bubble");
        duration = 10f;
        cooldown = 3f;
        projectileSpeed = 40f;
    }

    public override void Cast()
    {
        base.Cast();
        Debug.Log("Bubble");
    }

    public override void Upgrade()
    {
        BubbledDuration += 1f;
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

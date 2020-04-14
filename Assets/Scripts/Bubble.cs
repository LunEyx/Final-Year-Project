using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : ProjectileSpell
{
    private void Awake()
    {
        icon = Resources.Load<Sprite>("Icons/SpellBookPreface_05");
        prefab = Resources.Load<GameObject>("Bubble");
        duration = 10f;
        cooldown = 3f;
        projectileSpeed = 40f;
    }
}

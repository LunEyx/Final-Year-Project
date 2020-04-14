using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : ProjectileSpell  
{
    private void Awake()
    {
        icon = Resources.Load<Sprite>("Icons/SpellBookPreface_09");
        prefab = Resources.Load<GameObject>("Tornado");
        duration = 5f;
        cooldown = 3f;
        projectileSpeed = 20f;
    }
}

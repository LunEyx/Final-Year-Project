using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCaster : ProjectileSpell
{
    private void Awake()
    {
        icon = Resources.Load<Sprite>("Icons/SpellBookPreface_18");
        prefab = Resources.Load<GameObject>("Fireball");
        duration = 5f;
        cooldown = 3f;
        projectileSpeed = 40f;
    }
}

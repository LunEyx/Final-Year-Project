﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : ProjectileSpell
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

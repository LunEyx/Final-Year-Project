﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    private string itemName;
    private int itemCost;
    private Sprite itemIcon;
    private string itemDescription;
    private bool ownedByPlayer;

    public Item(string itemName, int itemCost, Sprite itemIcon, string itemDescription, bool ownedByPlayer)
    {
        this.itemName = itemName;
        this.itemCost = itemCost;
        this.itemIcon = itemIcon;
        this.itemDescription = itemDescription;
        this.ownedByPlayer = ownedByPlayer;
    }

    public string GetItemName()
    {
        return this.itemName;
    }

    public int GetItemCost()
    {
        return this.itemCost;
    }

    public Sprite GetItemIcon()
    {
        return this.itemIcon;
    }

    public string GetItemDescription()
    {
        return this.itemDescription;
    }

    public bool GetOwnership()
    {
        return this.ownedByPlayer;
    }
}

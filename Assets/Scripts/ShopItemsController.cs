using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemsController: MonoBehaviour
{
    public Player player;
    public Item item;
    private int itemCost;

    private void Start()
    {
        gameObject.GetComponentsInChildren<Text>()[0].text = item.GetItemName();
        gameObject.GetComponentsInChildren<Text>()[1].text = item.GetItemCost();
        gameObject.GetComponentsInChildren<Image>()[1].sprite = item.GetItemIcon();
        gameObject.GetComponentInChildren<ToolTip>().tooltipText = item.GetItemDescription();
        itemCost = int.Parse(item.GetItemCost());
    }

    
    
    public void Purchase()
    {
        if (player.gold >= itemCost)
        {
            
            player.gold -= itemCost;
            Destroy(gameObject);
        }
    }
}
    
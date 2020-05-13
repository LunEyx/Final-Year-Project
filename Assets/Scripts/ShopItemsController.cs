using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemsController: MonoBehaviour
{
    public Player player;
    public Item item;
    public GameObject soldIcon;
    public GameObject noGoldPrompt;
    public Button itemIcon; 
    private int itemCost;

    private void Start()
    {
        gameObject.GetComponentsInChildren<Text>()[0].text = item.GetItemName();
        gameObject.GetComponentsInChildren<Text>()[1].text = item.GetItemCost();
        gameObject.GetComponentsInChildren<Image>()[1].sprite = item.GetItemIcon();
        gameObject.GetComponentInChildren<ToolTip>().tooltipText = item.GetItemDescription();
        itemCost = int.Parse(item.GetItemCost());
        soldIcon.SetActive(false);
        noGoldPrompt.SetActive(false);
    }
    
    public void Purchase()
    {
        if (player.CanAfford(itemCost))
        {
            player.GainCoin(-itemCost);
            soldIcon.SetActive(true);
            itemIcon.enabled = false;
            player.GetItem(item.GetItemID());
        }
        else
        {
            noGoldPrompt.SetActive(true);
        }
    }
}
    
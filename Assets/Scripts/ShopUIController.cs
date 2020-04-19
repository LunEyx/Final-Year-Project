using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIController : MonoBehaviour
{
    
    private GameObject shopItemTemplate;
    private Player player;

    

    private void Start()
    {
        shopItemTemplate = Resources.Load<GameObject>("Shop_Item_Container");

        List<Item> itemList = GameManager.ItemList;
        

        CreateItemContainer(itemList[0],0);
        CreateItemContainer(itemList[1], 1);
    }

    private void CreateItemContainer(Item item, int offset)
    {
        GameObject container = Instantiate(shopItemTemplate,transform);
        container.GetComponent<Transform>().localPosition = new Vector3(-241 + offset * 175, 0, 0);

        container.GetComponentsInChildren<Text>()[0].text = item.GetItemName();
        container.GetComponentsInChildren<Text>()[1].text = item.GetItemCost();
        

        container.GetComponentsInChildren<Image>()[1].sprite = item.GetItemIcon();
    }

    public void Show(Player player)
    {
        this.player = player;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

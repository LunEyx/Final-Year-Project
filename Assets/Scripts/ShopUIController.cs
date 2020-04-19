using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIController : MonoBehaviour
{
    private Transform shopUI;
    private Transform shopItemTemplate;
    private Player player;

    private void Awake()
    {
        shopUI = transform.Find("Shop_UI");
        shopItemTemplate = shopUI.Find("Shop_Item_Container");
        shopItemTemplate.gameObject.SetActive(false);


    }

    private void CreateItemContainer(Item item)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, shopUI);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        shopItemRectTransform.Find("Item_Name").GetComponent<TextMesh>().text = item.GetItemName();
        shopItemRectTransform.Find("Item_Cost").GetComponent<TextMesh>().text = item.GetItemCost();
        //shopItemRectTransform.Find("Item_").GetComponent<TextMesh>().text = item.GetItemName();
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

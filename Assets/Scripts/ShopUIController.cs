using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIController : MonoBehaviour
{
    private GameObject shopItemTemplate;
    private Player player;
    public GameObject tooltip;
    public int widthOffset = 200;
    public int heightOffset = -25;

    private void Start()
    {
        shopItemTemplate = Resources.Load<GameObject>("Shop_Item_Container");
        player = GameManager.GetLocalPlayer();
        List<Item> itemList = GameManager.ItemList;

        CreateItemContainer(itemList[0], 0);
        CreateItemContainer(itemList[1], 1);
        CreateItemContainer(itemList[2], 2);

        tooltip.SetActive(false);
    }

    private void Update()
    {
        tooltip.transform.position = Input.mousePosition + new Vector3(widthOffset,heightOffset,0);
    }

    private void CreateItemContainer(Item item, int offset)
    {
        GameObject container = Instantiate(shopItemTemplate,transform);
        container.GetComponent<Transform>().localPosition = new Vector3( -500 + offset * 500, 0, 0);
        container.GetComponent<ShopItemsController>().item = item;
        container.GetComponent<ShopItemsController>().player = player;
        container.GetComponentInChildren<ToolTip>().tooltip = tooltip;
        
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

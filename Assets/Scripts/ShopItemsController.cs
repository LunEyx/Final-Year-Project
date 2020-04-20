using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemsController: MonoBehaviour
{
    
    private int itemCost;

    private void Start()
    {
        //itemCost = int.Parse(gameObject.GetComponentsInChildren<Text>()[1].ToString());
        
    }

    
    
    public void Purchase()
    {
        //if (player.gold >= itemCost)
        {
            
         //   player.gold -= itemCost;
            Destroy(gameObject);
        }
    }
}

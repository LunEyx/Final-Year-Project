using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTriggerCollider : MonoBehaviour
{
    [SerializeField] private ShopUIController shop_UI;
    private void OnTriggerEnter(Collider collider)
    {
        Player player = collider.GetComponent<Player>();
        if (player != null)
        {
            shop_UI.Show(player);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        Player player = collider.GetComponent<Player>();
        if (player != null)
        {
            shop_UI.Hide();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTriggerCollider : MonoBehaviour
{
    [SerializeField] private ShopUIController shop_UI;
    private void OnTriggerEnter(Collider collider)
    {
        Player player = collider.GetComponent<Player>();
        if (player != null && player.isLocalPlayer)
        {
            shop_UI.Show(player);
            GameManager.CameraMove = false;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        Player player = collider.GetComponent<Player>();
        if (player != null && player.isLocalPlayer)
        {
            shop_UI.Hide();
            GameManager.CameraMove = true;
        }
    }
}

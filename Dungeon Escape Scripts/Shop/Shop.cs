using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public int currentSelectedItem = 0;
    public int currentItemCost;

    private Player _player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _player = other.GetComponent<Player>();
            if (_player != null)
            {
                UIManager.Instance.OpenShop(_player.diamonds);
            }
            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        switch (item)
        {
            case 0: /*flame sword */
                UIManager.Instance.UpdateShopSelection(109);
                currentSelectedItem = 0;
                currentItemCost = 200;
                break;
            case 1: /*boots */
                UIManager.Instance.UpdateShopSelection(-3);
                currentSelectedItem = 1;
                currentItemCost = 400;
                break;
            case 2: /*keys to the castle */
                UIManager.Instance.UpdateShopSelection(-107);
                currentSelectedItem = 2;
                currentItemCost = 100;
                break;

        }
    }

    public void BuyItem()
    {
        /* check if the player has enough money */
        if(_player.diamonds >= currentItemCost)
        {
            
            /* purchase sword */
            if(currentSelectedItem == 0)
            {
                if (_player.HasSword()) return;
                _player.PurchaseSword();
            }
            /* purchase boots */
            else if (currentSelectedItem == 1)
            {
                if (_player.HasBoots()) return;
                _player.PurchaseBoots();
            }
            /* purchase key */
            else if(currentSelectedItem == 2)
            {
                if (_player.HasKey()) return;
                GameManager.Instance.HasKeyToCastle = true;
            }
            _player.diamonds -= currentItemCost;
            UIManager.Instance.UpdateGemCount(_player.diamonds);
            shopPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Not enough gems");
            shopPanel.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChestNode : MonoBehaviour
{
    public GameObject openChestButton;
    public GameObject player;
    public GameObject itemAcquiredPanel;

    public Inventory inventoryScript;
    public Shop shopScript;

    private bool chestOpened = false;

    void Update()
    {
        if (Collision() && !chestOpened)
        {
            openChestButton.SetActive(true);
        }
        else
        {
            openChestButton.SetActive(false);
        }
    }

    public void OpenChest()
    {
        itemAcquiredPanel.SetActive(true);
        int itemGenerated = UnityEngine.Random.Range(1, 6);
        switch(itemGenerated)
        {
            case 1:
                inventoryScript.pick_item(
                    inventoryScript.init_w("Wooden Sword", 5, shopScript.images[1])
                );
                itemAcquiredPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You got a Wooden Sword!";
                itemAcquiredPanel.transform.GetChild(1).GetComponent<Image>().sprite = shopScript.images[1];
                break;
            case 2:
                inventoryScript.pick_item(
                    inventoryScript.init_w("Metal Sword", 8, shopScript.images[2])
                );
                itemAcquiredPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You got a Metal Sword!";
                itemAcquiredPanel.transform.GetChild(1).GetComponent<Image>().sprite = shopScript.images[2];
                break;
            case 3:
                inventoryScript.pick_item(
                    inventoryScript.init_s("Wooden Shield", 3, shopScript.images[3])
                );
                itemAcquiredPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You got a Wooden Shield!";
                itemAcquiredPanel.transform.GetChild(1).GetComponent<Image>().sprite = shopScript.images[3];
                break;
            case 4:
                inventoryScript.pick_item(
                    inventoryScript.init_s("Metal Shield", 5, shopScript.images[4])
                );
                itemAcquiredPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You got a Metal Shield!";
                itemAcquiredPanel.transform.GetChild(1).GetComponent<Image>().sprite = shopScript.images[4];
                break;
            case 5:
                inventoryScript.pick_item(100);
                itemAcquiredPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "You got 100 coins!";
                itemAcquiredPanel.transform.GetChild(1).GetComponent<Image>().sprite = shopScript.images[5];
                break;       
        }
        chestOpened = true;
        openChestButton.SetActive(false);
    }

    public void CloseItemPanel()
    {
        itemAcquiredPanel.SetActive(false);
    }

    private bool Collision()
    {
        
        //gets the centers of both
        Vector3 pCenter = player.GetComponentInChildren<SpriteRenderer>().bounds.center;
        Vector3 sCenter = this.gameObject.transform.position;

        //determines the distance between the centers of the player and the given shop
        float distance = (float)Math.Sqrt(Math.Pow(sCenter.x - pCenter.x, 2) + Math.Pow(sCenter.y - pCenter.y, 2));

        //checks if there is a collision, returns true if true
        if (distance < this.transform.localScale.x + player.transform.localScale.x)
        {
            //print("Collide");
            return true;
        }
        else
        {
            return false;
        }
    }
}

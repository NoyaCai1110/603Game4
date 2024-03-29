using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public GameObject shop1;
    public GameObject shop2;
    public GameObject coinItems;
    public GameObject moneyItems;
    public Shop shopScript;
    public GameObject itemPrefab;

    private List<GameObject> items;

    void start()
    {
        shop1.SetActive(true);
        shop2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hideShop1()
    {
        shop2.SetActive(true);
        shop1.SetActive(false);
        displayItems();
    }

    public void hideShop2()
    {
        shop1.SetActive(true);
        shop2.SetActive(false);
    }

    private void displayItems()
    {
        //displays all coin items
        if (shopScript.m_coin.Count > 0)
        {
            for (int i = 0; i < shopScript.m_coin.Count; i++)
            {
                items.Add(Instantiate(itemPrefab, new Vector3(0f, 292f * i, 0), Quaternion.identity));//this gets an error
                items[i].transform.parent = coinItems.transform;

                TextMeshProUGUI[] textBoxes = items[i].GetComponentsInChildren<TextMeshProUGUI>();
                textBoxes[0].text = shopScript.m_coin[i].name;
                textBoxes[1].text = shopScript.m_coin[i].stat.ToString();
                textBoxes[2].text = shopScript.m_coin[i].price.ToString();
                items[i].GetComponentInChildren<Button>().onClick.AddListener(delegate { shopScript.purchase_item(shopScript.m_coin, i, shopScript.backpack); });
            }
        }


        //displays all money items
        if (shopScript.m_money.Count > 0)
        {
            for (int i = 0; i < shopScript.m_money.Count; i++)
            {
                items.Add(Instantiate(itemPrefab, new Vector3(0f, 292f * i, 0), Quaternion.identity));
                items[i].transform.parent = moneyItems.transform;

                TextMeshProUGUI[] textBoxes = items[i].GetComponentsInChildren<TextMeshProUGUI>();
                textBoxes[0].text = shopScript.m_money[i].name;
                textBoxes[1].text = shopScript.m_money[i].stat.ToString();
                textBoxes[2].text = shopScript.m_money[i].price.ToString();
                items[i].GetComponentInChildren<Button>().onClick.AddListener(delegate { shopScript.purchase_item(shopScript.m_money, i, shopScript.backpack); });
            }
        }
    }
}

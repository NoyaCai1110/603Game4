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

    void Awake()
    {
        //displays all coin items
        /*for(int i = 0; i < shopScript.m_coin.Count; i++)
        {
            items.Add(Instantiate(itemPrefab, new Vector3(0f, 292f * i, 0), Quaternion.identity));
            TextMeshProUGUI[] textBoxes = items[i].GetComponentsInChildren<TextMeshProUGUI>();
            textBoxes[0].text = shopScript.m_coin[i].name;
            textBoxes[1].text = shopScript.m_coin[i].stat.ToString();
            textBoxes[2].text = shopScript.m_coin[i].price.ToString();
            //items[i].GetComponentInChildren<Button>().onClick = shopScript.purchase_item();
        }

        //displays all coin items
        for (int i = 0; i < shopScript.m_money.Count; i++)
        {
            items.Add(Instantiate(itemPrefab, new Vector3(0f, 292f * i, 0), Quaternion.identity));
            TextMeshProUGUI[] textBoxes = items[i].GetComponentsInChildren<TextMeshProUGUI>();
            textBoxes[0].text = shopScript.m_money[i].name;
            textBoxes[1].text = shopScript.m_money[i].stat.ToString();
            textBoxes[2].text = shopScript.m_money[i].price.ToString();
            //items[i].GetComponentInChildren<Button>().onClick = shopScript.purchase_item();
        }*/

        hideShop2();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hideShop1()
    {
        shop1.SetActive(false);
        shop2.SetActive(true);
    }

    public void hideShop2()
    {
        shop2.SetActive(false);
        shop1.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
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
    public Inventory backpack;
    [SerializeField] private List<GameObject> items = new List<GameObject>();
    private List<GameObject> itemsM = new List<GameObject>();

    void Start()
    {
        shopScript = GameObject.Find("Shop").GetComponent<Shop>();
        backpack = GameObject.Find("Player").GetComponent<Inventory>();
        //shop1.SetActive(true);
        //shop2.SetActive(false);
        hideShop1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hideShop1()
    {
        shop2.SetActive(true);
        shop1.SetActive(false);
        
        if(items.Count == 0)
        {
            displayItems();
        }
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
            //Debug.Log("count "+shopScript.m_coin.Count);
            //Debug.Log(items.Count);
            for (int i = 0; i < shopScript.m_coin.Count; i++)
            {
                items.Add(Instantiate(itemPrefab, new Vector3(0f,430f-200f * i, 0), Quaternion.identity));
                items[i].transform.SetParent(coinItems.transform, false);

                updateItemUICoin(i);

                /*TextMeshProUGUI[] textBoxes = items[i].GetComponentsInChildren<TextMeshProUGUI>();
                textBoxes[0].text = shopScript.m_coin[i].stock.ToString();
                textBoxes[1].text = shopScript.m_coin[i].name;
                textBoxes[2].text = shopScript.m_coin[i].stat.ToString();
                textBoxes[3].text = shopScript.m_coin[i].price.ToString();*/

                //Image itemImage = items[i].GetComponent<Image>();
                //itemImage = shopScript.m_coin[i].image;//doesn't work
                

                if (shopScript.m_coin[i].stock > 0)
                {
                    //print(i);
                    //items[i].GetComponentInChildren<Button>().onClick.AddListener(() => onButtonClickCoin(i));
                }
                
            }
            int j = 0;
            while(j < shopScript.m_coin.Count)
            {
                if (shopScript.m_coin[j].stock > 0)
                    items[j].GetComponentInChildren<Button>().onClick.AddListener(() => onButtonClickCoin(j));
                j++;
            }
            j = shopScript.m_coin.Count - 1;
            //items[0].GetComponentInChildren<Button>().onClick.AddListener(() => onButtonClickCoin(0));
            //items[1].GetComponentInChildren<Button>().onClick.AddListener(() => onButtonClickCoin(1));
            //items[2].GetComponentInChildren<Button>().onClick.AddListener(() => onButtonClickCoin(2));
            //items[3].GetComponentInChildren<Button>().onClick.AddListener(() => onButtonClickCoin(3));
            //items[4].GetComponentInChildren<Button>().onClick.AddListener(() => onButtonClickCoin(4));
        }
        

        //displays all money items
        if (shopScript.m_money.Count > 0)
        {
            for (int i = 0; i < shopScript.m_money.Count; i++)
            {
                itemsM.Add(Instantiate(itemPrefab, new Vector3(0f, 430f-200f * i, 0), Quaternion.identity));
                itemsM[i].transform.SetParent(moneyItems.transform, false);

                updateItemUIMoney(i);

                /*TextMeshProUGUI[] textBoxesM = itemsM[i].GetComponentsInChildren<TextMeshProUGUI>();
                textBoxesM[0].text = shopScript.m_money[i].stock.ToString();
                textBoxesM[1].text = shopScript.m_money[i].name;
                textBoxesM[2].text = shopScript.m_money[i].stat.ToString();
                textBoxesM[3].text = shopScript.m_money[i].price.ToString();*/

                //Image itemImage = itemsM[i].GetComponent<Image>();
                //itemImage = shopScript.m_money[i].image;//doesn't work
                
                if (shopScript.m_money[i].stock > 0)
                {
                    //itemsM[i].GetComponent<IDkeeper>().id = i;
                    itemsM[i].GetComponentInChildren<Button>().onClick.AddListener(()=>onButtonClickMoney(i));
                }
            }
        }
    }

    private void updateItemUICoin(int i)
    {
        //Debug.Log(i);
        TextMeshProUGUI[] textBoxes = items[i].GetComponentsInChildren<TextMeshProUGUI>();
        textBoxes[0].text = shopScript.m_coin[i].stock.ToString();
        textBoxes[1].text = shopScript.m_coin[i].name;
        textBoxes[2].text = shopScript.m_coin[i].stat.ToString();
        textBoxes[3].text = shopScript.m_coin[i].price.ToString();

        if(shopScript.m_coin[i].stock == 0)
        {
            Button button = items[i].GetComponentInChildren<Button>();
            if(button != null)  items[i].GetComponentInChildren<Button>().gameObject.SetActive(false);
        }
    }

    private void updateItemUIMoney(int i)
    {
        TextMeshProUGUI[] textBoxes = itemsM[i].GetComponentsInChildren<TextMeshProUGUI>();
        textBoxes[0].text = shopScript.m_money[i].stock.ToString();
        textBoxes[1].text = shopScript.m_money[i].name;
        textBoxes[2].text = shopScript.m_money[i].stat.ToString();
        textBoxes[3].text = shopScript.m_money[i].price.ToString();

        if (shopScript.m_money[i].stock == 0)
        {
            itemsM[i].GetComponentInChildren<Button>().gameObject.SetActive(false);
        }
    }

    public void onButtonClickCoin(int i)
    {
        Debug.Log("i = "+i);
        shopScript.purchase_item(shopScript.m_coin, i, backpack);
        
        updateItemUICoin(i);
    }

    public void onButtonClickMoney(int i)
    {
        shopScript.purchase_item(shopScript.m_money, i, backpack);

        updateItemUIMoney(i);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDkeeper : MonoBehaviour
{
    // Start is called before the first frame update
    public int id;
    public Shop shop;
    public Inventory backpack;
    public ShopUI shopUI;
    void Start()
    {
        backpack = GameObject.Find("Player").GetComponent<Inventory>();
    }

    public void SetShops(Shop shop, ShopUI shopUI)
    {
        //print(shop.gameObject.name + shopUI.gameObject.name);
        this.shop = shop; 
        this.shopUI = shopUI;
    }
    public void onButtonClickCoin()
    {
        shop.purchase_item(shop.m_coin, id, backpack);

        shopUI.updateItemUICoin(id);
    }

    public void onButtonClickMoney()
    {
        shop.purchase_item(shop.m_money, id, backpack);

        shopUI.updateItemUIMoney(id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

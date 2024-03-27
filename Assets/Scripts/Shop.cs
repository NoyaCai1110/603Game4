using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct merchandise
{
    public string name;
    public int price;
    public int stock;
    public int stat;    //Coin, Attack, Defense, Recover
    public item_type type;    
};
public enum item_type
{
    Coin,   
    Weapon,    
    Shield,   
    Potions
}
public class Shop : MonoBehaviour
{
    // Start is called before the first frame update
    
    merchandise init_item(string name, int price, int stock, int stat, item_type type)
    {
        merchandise item = new merchandise();
        item.name = name;
        item.price = price;
        item.stock = stock;
        item.stat = stat;
        item.type = type;
        return item;
    }
    void purchase_c_item(merchandise c_item, Player player)
    {
        if(c_item.stock <= 0)
        {
            Debug.Log("Out of stock");
            return;
        }
        c_item.stock --;
        // Charge player && Inventory
        switch (c_item.type)
        {
            case item_type.Coin:
                player.coins += c_item.stat;
                break;
            case item_type.Weapon:
                player.coins -= c_item.price;
                player.w_list.Add(c_item);
                break;
            case item_type.Shield:
                player.coins -= c_item.price;
                player.s_list.Add(c_item);
                break;
            case item_type.Potions: 
                player.coins -= c_item.price;
                player.p_list.Add(c_item);
                break;
        }
    }
    public List<merchandise> m_coin = new List<merchandise>();
    private merchandise c_item;
    public List<merchandise> m_money = new List<merchandise>();
    private merchandise m_item;
    void Start()
    {
        //merchandise using coins
        c_item = init_item("Life Potion", 20, 5, 20, item_type.Potions);
        m_coin.Add(c_item);
        c_item = init_item("Wooden Sword", 50, 1, 5, item_type.Weapon);
        m_coin.Add(c_item);
        c_item = init_item("Wooden Shield", 50, 1, 3, item_type.Shield);
        m_coin.Add(c_item);
        //merchandise using money
        m_item = init_item("500 Coins", 5, 1000, 500, item_type.Coin);
        m_money.Add(m_item);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

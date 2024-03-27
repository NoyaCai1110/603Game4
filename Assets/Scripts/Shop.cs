using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Merchandise
{
    public string name;
    public int price;
    public int stock;
    public int stat;    //Coin, Attack, Defense, Recover
    public Sprite image;
    public item_type type;    
};
[Serializable]
public struct Weapon
{
    public string name;
    public int stat;    //Attack
    public Sprite image;
};
[Serializable]
public struct Shield
{
    public string name;
    public int stat;    //Defense
    public Sprite image;
};
[Serializable]
public struct Potion
{
    public string name;
    public int stat;    //Recover
    public int quantity;
    public Sprite image;
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
    
    Merchandise init_item(string name, int price, int stock, int stat, Sprite image, item_type type)
    {
        Merchandise item = new Merchandise();
        item.name = name;
        item.price = price;
        item.stock = stock;
        item.stat = stat;
        item.image = image;
        item.type = type;
        return item;
    }
    Weapon w_converter(Merchandise m)
    {
        Weapon item = new Weapon();
        item.name = m.name;
        item.stat = m.stat;
        item.image = m.image;
        return item;
    }
    Shield s_converter(Merchandise m)
    {
        Shield item = new Shield();
        item.name = m.name;
        item.stat = m.stat;
        item.image = m.image;
        return item;
    }
    Potion p_converter(Merchandise m)
    {
        Potion item = new Potion();
        item.name = m.name;
        item.stat = m.stat;
        item.image = m.image;
        return item;
    }
    void purchase_c_item(Merchandise c_item, Player player)
    {
        if(c_item.stock <= 0)
        {
            Debug.Log("Out of stock");
            return;
        }
        if(player.coins < c_item.price && c_item.type != item_type.Coin)
        {
            Debug.Log("Not enough Coins");
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
                Weapon w = w_converter(c_item);
                player.w_list.Add(w);
                break;
            case item_type.Shield:
                player.coins -= c_item.price;
                Shield s = s_converter(c_item);
                player.s_list.Add(s);
                break;
            case item_type.Potions: 
                player.coins -= c_item.price;
                Potion p = p_converter(c_item);
                bool found = false;
                for(int i = 0; i < player.p_list.Count; i++)
                {
                    if (player.p_list[i].name == p.name)
                    {
                        Potion tmp = player.p_list[i];
                        tmp.quantity ++;
                        player.p_list[i] = tmp;
                        found = true;
                    }
                }
                if(!found)
                    player.p_list.Add(p);
                break;
            default:
                break;
        }
    }
    public List<Merchandise> m_coin = new List<Merchandise>();
    private Merchandise c_item;
    public List<Merchandise> m_money = new List<Merchandise>();
    private Merchandise m_item;
    [HideInInspector]public Sprite image;    //placeholder
    void Awake()
    {
        //merchandise using coins
        c_item = init_item("Life Potion", 20, 5, 20, image, item_type.Potions);
        m_coin.Add(c_item);
        c_item = init_item("Wooden Sword", 50, 1, 5, image, item_type.Weapon);
        m_coin.Add(c_item);
        c_item = init_item("Wooden Shield", 50, 1, 3, image, item_type.Shield);
        m_coin.Add(c_item);
        //merchandise using money
        m_item = init_item("500 Coins", 5, 1000, 500, image, item_type.Coin);
        m_money.Add(m_item);
    }
    void Start()
    {

        
    }


    // Update is called once per frame
    void Update()
    {
        //m_coin.Clear();
    }
}

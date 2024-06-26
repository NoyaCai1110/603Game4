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
        item.equipped = false;
        return item;
    }
    Shield s_converter(Merchandise m)
    {
        Shield item = new Shield();
        item.name = m.name;
        item.stat = m.stat;
        item.image = m.image;
        item.equipped = false;
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
    public void purchase_item(List<Merchandise> list, int index, Inventory bp)
    {
        if(index > list.Count - 1) 
        {
            //Debug.Log(index);
            Debug.Log("Item Not Valid");
            return;
        }
        Merchandise item = list[index];
        if(item.stock <= 0)
        {
            Debug.Log("Out of stock");
            return;
        }
        if(bp.Coins < item.price && item.type != item_type.Coin)
        {
            Debug.Log("Not enough Coins");
            return;
        }
        item.stock --;
        list[index] = item;
        // Charge player && Inventory
        switch (item.type)
        {
            case item_type.Coin:
                bp.Coins += item.stat;
                break;
            case item_type.Weapon:
                bp.Coins -= item.price;
                Weapon w = w_converter(item);
                bp.w_list.Add(w);
                bp.w_list.Sort((x, y) => x.stat.CompareTo(y.stat));
                break;
            case item_type.Shield:
                bp.Coins -= item.price;
                Shield s = s_converter(item);
                bp.s_list.Add(s);
                bp.s_list.Sort((x, y) => x.stat.CompareTo(y.stat));
                break;
            case item_type.Potions: 
                bp.Coins -= item.price;
                Potion p = p_converter(item);
                bool found = false;
                for(int i = 0; i < bp.p_list.Count; i++)
                {
                    if (bp.p_list[i].name == p.name)
                    {
                        Potion tmp = bp.p_list[i];
                        tmp.num ++;
                        bp.p_list[i] = tmp;
                        found = true;
                    }
                }
                if (!found)
                {
                    p.num = 1;
                    bp.p_list.Add(p);
                }
                bp.p_list.Sort((x, y) => -x.stat.CompareTo(y.stat));
                break;
            default:
                break;
        }
        return;
    }
    public List<Merchandise> m_coin = new List<Merchandise>();
    private Merchandise c_item;
    public List<Merchandise> m_money = new List<Merchandise>();
    private Merchandise m_item;
    public List<Sprite> images;
    public Inventory backpack;
    void Awake()
    {
        //merchandise using coins
        c_item = init_item("Wooden Sword", 150, 4, 5, images[1], item_type.Weapon);
        m_coin.Add(c_item);
        c_item = init_item("Metal Sword", 300, 4, 8, images[2], item_type.Weapon);
        m_coin.Add(c_item);
        c_item = init_item("Wooden Shield", 150, 4, 3, images[3], item_type.Shield);
        m_coin.Add(c_item);
        c_item = init_item("Metal Shield", 300, 4, 5, images[4], item_type.Shield);
        m_coin.Add(c_item);
        //merchandise using money
        m_item = init_item("50 Coins", 1, 1000, 50, images[5], item_type.Coin);
        m_money.Add(m_item);
        m_item = init_item("175 Coins", 3, 1000, 175, images[5], item_type.Coin);
        m_money.Add(m_item);
        m_item = init_item("400 Coins", 5, 1000, 400, images[5], item_type.Coin);
        m_money.Add(m_item);
    }
    void Start()
    {
        //backpack = GameObject.Find("Player").GetComponent<Inventory>();
        //purchase_item(m_coin, 0, backpack);   //purchase m_coin[0]
        //purchase_item(m_coin, 0, backpack);
        //purchase_item(m_coin, 0, backpack);
        //purchase_item(m_coin, 0, backpack);
        //purchase_item(m_coin, 0, backpack);
        //purchase_item(m_coin, 0, backpack);
        //purchase_item(m_coin, 1, backpack);
        //purchase_item(m_coin, 2, backpack);
        //purchase_item(m_coin, 3, backpack);
        //purchase_item(m_coin, 4, backpack);
    }


    // Update is called once per frame
    void Update()
    {
        //m_coin.Clear();
    }
}

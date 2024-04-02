using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct Weapon
{
    public string name;
    public int stat;    //Attack
    public bool equipped;
    public Sprite image;
};
[Serializable]
public struct Shield
{
    public string name;
    public int stat;    //Defense
    public bool equipped;
    public Sprite image;
};
[Serializable]
public struct Potion
{
    public string name;
    public int stat;    //Recover
    public int num;
    public Sprite image;
};

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    public int Coins;
    public List<Weapon> w_list = new List<Weapon>();  //Weapon
    public List<Shield> s_list = new List<Shield>();  //Shield
    public List<Potion> p_list = new List<Potion>();  //Potion

    public void equip_weapon(int w_index, PartyMember pm)
    {
        if (w_index > w_list.Count - 1)
        {
            Debug.Log("No Weapon here");
            return;
        }
        if (!w_list[w_index].equipped)
        {
            Weapon tmp;
            if (pm.cur_w != -1)
            {
                tmp = w_list[pm.cur_w];
                tmp.equipped = false;
                w_list[pm.cur_w] = tmp;
            }
            tmp = w_list[w_index];
            tmp.equipped = true;
            w_list[w_index] = tmp;
            pm.cur_w = w_index;
            pm.Attack = w_list[pm.cur_w].stat;
        }
    }
    public void unequip_weapon(PartyMember pm)
    {
        if (pm.cur_w != -1)
        {
            Weapon tmp;
            tmp = w_list[pm.cur_w];
            tmp.equipped = false;
            w_list[pm.cur_w] = tmp;
            pm.cur_w = -1;
            pm.Attack = 0;
        }
    }

    public void equip_shield(int s_index, PartyMember pm)
    {
        if (s_index > s_list.Count - 1)
        {
            Debug.Log("No Shield here");
            return;
        }
        if (!s_list[s_index].equipped)
        {
            Shield tmp;
            if (pm.cur_s != -1)
            {
                tmp = s_list[pm.cur_s];
                tmp.equipped = false;
                s_list[pm.cur_s] = tmp;
            }
            tmp = s_list[s_index];
            tmp.equipped = true;
            s_list[s_index] = tmp;
            pm.cur_s = s_index;
            pm.Defense = s_list[pm.cur_s].stat;
        }
    }

    public void unequip_shield(PartyMember pm)
    {
        if (pm.cur_s != -1)
        {
            Shield tmp;
            tmp = s_list[pm.cur_s];
            tmp.equipped = false;
            s_list[pm.cur_s] = tmp;
            pm.cur_s = -1;
            pm.Defense = 0;
        }
    }

    void drink_potion(int p_index, PartyMember pm)
    {
        if (p_index > p_list.Count - 1)
        {
            Debug.Log("No Potion here");
            return;
        }
        Potion tmp;
        tmp = p_list[p_index];
        if (tmp.num == 1)
        {
            p_list.Remove(tmp);
        }
        else
        {
            tmp.num--;
            p_list[p_index] = tmp;
        }
        pm.HP += tmp.stat;
        if (pm.HP > pm.MaxHP)
        {
            pm.HP = pm.MaxHP;
        }
    }

    void Start()
    {
        Coins = 300;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    public int Coins;
    public List<Weapon> w_list = new List<Weapon>();  //Weapon
    public List<Shield> s_list = new List<Shield>();  //Shield
    public List<Potion> p_list = new List<Potion>();  //Potion

    void equip_weapon(int w_index, PartyMember pm)
    {
        if (w_index > w_list.Count - 1)
        {
            Debug.Log("No Weapon here");
            return;
        }
        if (!w_list[w_index].equipped)
        {
            Weapon tmp;
            tmp = w_list[pm.cur_w];
            tmp.equipped = false;
            w_list[pm.cur_w] = tmp;
            tmp = w_list[w_index];
            tmp.equipped = true;
            w_list[w_index] = tmp;
            pm.cur_w = w_index;
            pm.Attack = w_list[pm.cur_w].stat;
        }
    }

    void equip_shield(int s_index, PartyMember pm)
    {
        if (s_index > s_list.Count - 1)
        {
            Debug.Log("No Shield here");
            return;
        }
        if (!s_list[s_index].equipped)
        {
            Shield tmp;
            tmp = s_list[pm.cur_s];
            tmp.equipped = false;
            s_list[pm.cur_s] = tmp;
            tmp = s_list[s_index];
            tmp.equipped = true;
            s_list[s_index] = tmp;
            pm.cur_s = s_index;
            pm.Defense = s_list[pm.cur_s].stat;
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
        if (tmp.quantity == 1)
        {
            p_list.Remove(tmp);
        }
        else
        {
            tmp.quantity--;
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
        Coins = 500;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
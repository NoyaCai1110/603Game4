using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class Enemy : ScriptableObject
{
    // Start is called before the first frame update
    public string name;
    public int HP;
    public int MaxHP;
    public int Attack;
    public int Defense;
    public int dropped_gold;
    public int exp;
    public Sprite sprite;


    //deep copy 
    public Enemy(Enemy enemytoCopy)
    {
        this.name = enemytoCopy.name;
        this.HP = enemytoCopy.HP;
        this.MaxHP = enemytoCopy.MaxHP;
        this.Attack = enemytoCopy.Attack;
        this.Defense = enemytoCopy.Defense;
        this.dropped_gold = enemytoCopy.dropped_gold;
        this.exp = enemytoCopy.exp;
        this.sprite = enemytoCopy.sprite;
    }

    public void BasicAttack(List<PartyMember> playerParty)
    {

    }

    public void TakeDamage(int damage)
    {
        this.HP = Mathf.Max(0, this.HP - damage);

        if (HP <= 0)
        {
           
        }
    }

    //Healing health
    public void HealDamage(int amount)
    {
        this.HP = Mathf.Max(this.MaxHP, this.HP + amount);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

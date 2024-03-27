using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu]
public class PartyMember : ScriptableObject
{
    [SerializeField]
    public int HP, MaxHP,MP,MaxMP,Attack, Defense,Magic,Speed,level,exp;
    public string name; 
    public bool isDead; 

    public void Copy(PartyMember member)
    {
        this.name = member.name;
        this.HP = member.HP;
        this.MaxHP = member.MaxMP;
        this.MP = member.MP;
        this.MaxMP = member.MaxMP;
        this.Attack = member.Attack;
        this.Defense = member.Defense;
        this.Magic = member.Magic;
        this.Speed = member.Speed;
        this.level = member.level;
        this.exp = member.exp; 

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

    public void LevelUp()
    {
        this.level += 1;
        //level up stuff
    }

    public void UseSkill()
    {

    }

    public void BasicAttack(Enemy enemy)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

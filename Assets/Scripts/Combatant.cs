using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : ScriptableObject
{
    //every combatant should have these attributes
    public int HP, MaxHP, MP, MaxMP, Attack, Defense, Magic, Speed;
    public string name;
    public bool isDead, isDefending;



    protected void Copy(Combatant combatant)
    {
        this.name = combatant.name;
        this.HP = combatant.HP;
        this.MaxHP = combatant.MaxMP;
        this.MP = combatant.MP;
        this.MaxMP = combatant.MaxMP;
        this.Attack = combatant.Attack;
        this.Defense = combatant.Defense;
        this.Magic = combatant.Magic;
        this.Speed = combatant.Speed;

    }



    //Behavior for what happens when this character acts
    public virtual void Act()
    {

    }

    public void TakeDamage(int damage)
    {
        this.HP = Mathf.Max(0, this.HP - damage);

        if (HP <= 0)
        {
            isDead = true;
        }
    }

    //Healing health
    public void HealDamage(int amount)
    {
        this.HP = Mathf.Max(this.MaxHP, this.HP + amount);
    }

    public void BasicAttack(Combatant target)
    {
        
    }

    public void Defend()
    {

    }

    public void UseItem(Potion potion)
    {
        
    }

    

}

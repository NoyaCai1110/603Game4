using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : ScriptableObject
{
    //every combatant should have these attributes
    public int HP, MaxHP, MP, MaxMP, Attack, Defense, Magic, Speed;
    public string name;
    public bool isDead = false;
    public bool isDefending = false;



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
    public virtual List<string> Act(Ability ability)
    {
        return null;
    }

    public List<string> TakeDamage(int damage)
    {
        List<string> log_events = new List<string>();
        this.HP = Mathf.Max(0, this.HP - damage);

        log_events.Add($"{this.name} takes {damage} damage.");

        if (HP <= 0)
        {
            log_events.AddRange(Death());
        }

        return log_events;
    }

    public List<string> Death()
    {
        List<string> log_events = new List<string>();

        log_events.Add($"{this.name} is slain.");
        isDead = true;

        return log_events;
    }
    //Healing health
    public List<string> HealDamage(int amount)
    {
        this.HP = Mathf.Max(this.MaxHP, this.HP + amount);

        List<string> log_events = new List<string>();

        log_events.Add($"{this.name} recovers health!");
        isDead = true;


        return log_events;

    }

    public List<string> BasicAttack(Combatant target)
    {
        return null;
    }

    public List<string> Defend()
    {
        return null;
    }

    public List<string> UseItem(Potion potion)
    {
        return null;    
    }

    

}

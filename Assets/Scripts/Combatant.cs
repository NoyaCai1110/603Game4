using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : ScriptableObject
{
    //every combatant should have these attributes
    public int HP, MaxHP, MP, MaxMP, Attack, Defense, MagicDefense, Magic, Speed;
    public string name;
    public bool isDead = false;
    public bool isDefending = false;



    protected void Copy(Combatant combatant)
    {
        this.name = combatant.name;
        this.HP = combatant.HP;
        this.MaxHP = combatant.MaxHP;
        this.MP = combatant.MP;
        this.MaxMP = combatant.MaxMP;
        this.Attack = combatant.Attack;
        this.Defense = combatant.Defense;
        this.MagicDefense = combatant.MagicDefense;
        this.Magic = combatant.Magic;
        this.Speed = combatant.Speed;
    }


    //Behavior for what happens when this character acts
    public virtual List<string> Act(Ability ability)
    {
        return null;
    }

    public List<string> ParseAbility(Ability ability)
    {
        List<string> log_events = new List<string>();



        log_events.Add(ability.GetLogText());

        switch (ability.abilityType)
        {
            case AbilityType.MagicDamage:
                {
                    foreach (Combatant c in ability.targets)
                    {
                        bool success = true;

                        //check conditions
                        foreach (Condition cond in ability.conditions)
                        {
                            switch (cond)
                            {
                                case Condition.TargetIsAlive:
                                    {
                                        if (c.isDead)
                                        {
                                            success = false;
                                        }
                                        break;
                                    }
                            }
                        }

                        if (!success)
                        {
                            if (ability.targetType == Targetable.AoEFriendly || ability.targetType == Targetable.AoEEnemy)
                            {
                                break;
                            }

                            log_events.Add($"{name} hits nothing but air...");
                            break;
                        }
                        else
                        {
                            //take damage + check for death
                            int damage = Mathf.Max((int)(this.Magic* ability.power_modifier) - c.MagicDefense, 0);
                            c.TakeDamage(damage, log_events);

                        }

                    }
                    break;
                }
            case AbilityType.PhysicalDamage:
                {

                    foreach (Combatant c in ability.targets)
                    {
                        bool success = true;

                        //check conditions
                        foreach (Condition cond in ability.conditions)
                        {
                            switch (cond)
                            {
                                case Condition.TargetIsAlive:
                                    {
                                        if (c.isDead)
                                        {
                                            success = false;
                                        }
                                        break;
                                    }
                            }
                        }

                        if (!success)
                        {
                            if(ability.targetType == Targetable.AoEFriendly || ability.targetType == Targetable.AoEEnemy)
                            {
                                break;
                            }

                            log_events.Add($"{name} hits nothing but air...");
                            break;
                        }
                        else
                        {
                            //take damage + check for death
                            int damage = Mathf.Max((int)(this.Attack * ability.power_modifier) - c.Defense, 0);
                            c.TakeDamage(damage, log_events);

                        }

                    }
                    break;
                }
            case AbilityType.Healing:
                {
                    foreach (Combatant c in ability.targets)
                    {
                        bool success = true;

                        //check conditions
                        foreach (Condition cond in ability.conditions)
                        {
                            switch (cond)
                            {
                                case Condition.TargetIsAlive:
                                    {
                                        if (c.isDead)
                                        {
                                            success = false;
                                        }
                                        break;
                                    }
                            }
                        }

                        if (!success)
                        {
                            if (ability.targetType == Targetable.AoEFriendly || ability.targetType == Targetable.AoEEnemy)
                            {
                                break;
                            }

                            log_events.Add($"Had no effect on {c.name}...");
                            break;
                        }
                        else
                        {
                            //take damage + check for death
                            int healAmount = Mathf.Max((int)(this.Magic * ability.power_modifier), 0);
                            c.HealDamage(healAmount, log_events);

                        }

                    }
                    break;
                }
            case AbilityType.Special:
                {
                    break;
                }
            case AbilityType.None:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }



        return log_events;
    }
    public void TakeDamage(int damage, List<string> log_events)
    {

        this.HP = Mathf.Max(0, this.HP - damage);

        log_events.Add($"{this.name} takes {damage} damage.");

        if (HP <= 0)
        {
            Death(log_events);
        }


    }

    public void Death(List<string> log_events)
    {

        log_events.Add($"{this.name} is slain.");
        this.isDead = true;

    }
    //Healing health
    public void HealDamage(int amount, List<string> log_events)
    {
        this.HP = Mathf.Min(this.MaxHP, this.HP + amount);
        log_events.Add($"{this.name} recovers {amount} health!");

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

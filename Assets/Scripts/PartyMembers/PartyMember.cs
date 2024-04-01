using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu]
public class PartyMember : Combatant
{
    [SerializeField]
    public int level,exp;

    public List<Ability> abilities; 

    Weapon equippedWeapon;
    Shield equippedShield;


    public int cur_w = 0;
    public int cur_s = 0;
    public void Copy(PartyMember member)
    {
        base.Copy(member);
        this.level = member.level;
        this.exp = member.exp; 

    }

    public override List<string> Act(Ability ability)
    {
        List<string> complete_log = new List<string>();

        complete_log.AddRange(ParseAbility(ability));

        foreach(string s in complete_log)
        {
            Debug.Log(s);
        }
        return complete_log;
    }

    public List<string> ParseAbility(Ability ability)
    {
        List<string> log_events = new List<string>();

        log_events.Add(ability.GetLogText());

        switch (ability.abilityType)
        {
            case AbilityType.MagicDamage:
                {
                   
                        break;
                }
            case AbilityType.PhysicalDamage:
                {
                    foreach (Combatant c in ability.targets)
                    {
                        bool success = true; 
                        
                        //check conditions
                        foreach(Condition cond in ability.conditions)
                        {
                            switch (cond)
                            {
                                case Condition.IsDead:
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
                            log_events.Add($"{name} hits nothing but air...");
                            break;
                        }
                        else
                        {
                            //take damage
                            int damage = Mathf.Max(this.Attack - c.Defense, 0);
                            log_events.AddRange(c.TakeDamage(damage));
                            //check for death
                        }

                    }
                    break;
                }
            case AbilityType.Healing:
                {
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
    public void LevelUp()
    {
        this.level += 1;
        //level up stuff
    }


    public void BasicAttack(Enemy enemy)
    {
        int damage = Mathf.Max(this.Attack - enemy.Defense, 0);
        enemy.TakeDamage(damage);
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

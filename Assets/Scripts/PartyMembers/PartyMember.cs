using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu]
public class PartyMember : Combatant
{
    [SerializeField]
    public int level,exp;

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

    public override void Act(Turn currentTurn)
    {
        ActionType action = currentTurn.action;
        switch (action)
        {
            case ActionType.Attack:
                {
                    this.BasicAttack(currentTurn.targets[0]);
                    break;
                }
            case ActionType.Revive:
                {
                    break;
                }
            case ActionType.WideSweep:
                {
                    break;
                }
            case ActionType.Escape:
                {
                    break;
                }
            case ActionType.Fireball:
                {
                    break;
                }
            case ActionType.Burn:
                {
                    break;
                }
            case ActionType.Heal:
                {
                    break;
                }
            case ActionType.Item:
                {
                    break;
                }
        }
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

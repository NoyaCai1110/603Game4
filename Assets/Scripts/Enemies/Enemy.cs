using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class Enemy : Combatant
{
    //loot and exp from defeating it
    public int dropped_gold, exp;
    public Sprite sprite;

    public List<Ability> abilities;
    //deep copy 

    public void Copy(Enemy enemyToCopy)
    {
        base.Copy(enemyToCopy as Combatant);

        this.dropped_gold = enemyToCopy.dropped_gold;
        this.exp = enemyToCopy.exp;
        this.sprite = enemyToCopy.sprite;
    }

    public override List<string> Act(Ability ability)
    {
        List<string> complete_log = new List<string>();

        //complete_log.AddRange(ParseAbility(ability));

        foreach (string s in complete_log)
        {
            //Debug.Log(s);
        }
        return complete_log;
  
        
  
    }

    public List<string> Act(Ability ability, List<PartyMember> partyMembers)
    {
        List<string> complete_log = new List<string>();

        //decision-making
        float randomNum = Random.Range(0f, 1f);

        if(abilities.Count > 0)
        {
            //50% chance to use a skill
            if (randomNum < .5)
            {
               

            }
            //otherwise, attack
            else
            {
                PartyMember target = partyMembers[0];
                //get a valid player target
                do
                {
                    int rand_num = Random.Range(0, partyMembers.Count - 1);
                    target = partyMembers[rand_num];
                }
                while(target.isDead);

                

                
            }
        }
        else
        {

        }
       
       
        complete_log.AddRange(Act(ability));



        return complete_log;
    }
}

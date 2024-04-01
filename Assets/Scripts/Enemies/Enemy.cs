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
        //implement enemy behavior here
        return new List<string>();
    }
}

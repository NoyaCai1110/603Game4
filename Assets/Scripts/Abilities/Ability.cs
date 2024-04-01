using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    MagicDamage,
    PhysicalDamage,
    None,
    Healing, 
    Special

}

public enum Targetable
{
    EnemyOnly,
    FriendlyOnly,
    Any,
    Special,
    AoEFriendly,
    AoEEnemy
}

//actions will ignore targets that don't fulfill these conditions
public enum Condition
{
    IsDead,
    IsAlive,
    CantAct
}

[CreateAssetMenu]
public class Ability : ScriptableObject
{
    public Targetable targetType;
    public string owner;
    public string ability_name;
    public AbilityType abilityType; //does this ability use Attack or Magic 
    public float power_modifier; //how much damage 
    public int MP_Cost; //mana cost of the ability
    public List<Combatant> targets;
    public List<Condition> conditions;

    [SerializeField]
    private string log_text; //the text that will appear battle log when the skill is performed
    public string description;

    // Start is called before the first frame update
    public int unlocked_lvl; //level by which the ability can be used

    public string GetLogText()
    {
        string parsedString = log_text.Replace("{owner}", $"{owner}");

        return parsedString;
    }

}

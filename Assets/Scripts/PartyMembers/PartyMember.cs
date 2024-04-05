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


    public int cur_w = -1;
    public int cur_s = -1;
    public void Copy(PartyMember member)
    {
        this.isDead = member.isDead;
        this.HP = member.HP;
        this.MP = member.MP;

    }

    public void Setup()
    {
        this.HP = this.MaxHP;
        this.MP = this.MaxMP;
        this.isDead = false; 
    }

    public void UpdateHP_MP(int hp, int mp)
    {
        this.HP = mp;
        this.MP = mp;
    }

    public override List<string> Act(Ability ability)
    {
        List<string> complete_log = new List<string>();

        complete_log.AddRange(ParseAbility(ability));


        //factor in mana costs
        this.MP -= ability.MP_Cost;

        foreach (string s in complete_log)
        {
            Debug.Log(s);
        }
        return complete_log;
    }


    public void LevelUp()
    {
        this.level += 1;
        //level up stuff
    }

    // Start is called before the first frame update
    void Start()
    {
        cur_w = -1;
        cur_s = -1;
}

    // Update is called once per frame
    void Update()
    {
        
    }
}

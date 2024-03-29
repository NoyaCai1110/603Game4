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
               
        this.level = member.level;
        this.exp = member.exp; 

    }

    public override void Act()
    {
        
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

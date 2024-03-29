using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu]
public class PartyMember : Combatant
{
    [SerializeField]
    public int level,exp;
    public int cur_w = 0;
    public int cur_s = 0;
    public void Copy(PartyMember member)
    {
               
        this.level = member.level;
        this.exp = member.exp; 

    }
    public void TakeDamage(int damage)
    {
        this.HP = Mathf.Max(0, this.HP - damage);

        if (HP <= 0)
        {
            
        }
    }

    //Healing health
    public void HealDamage(int amount)
    {
        this.HP = Mathf.Max(this.MaxHP, this.HP + amount);
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

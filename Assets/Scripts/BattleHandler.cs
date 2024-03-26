using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHandler : MonoBehaviour
{
    public Player player;
    public Enemy enemy;
    public GameObject monster_container; //anchor for the monster sprite
    public TextMeshProUGUI logger; 
    public void Setup(Player player, Enemy enemy)
    {
        //load player
        this.player = player;

        //load enemy
        this.enemy = enemy;

        

    }


    //after setting up references, start the fight!
    private void Kickoff()
    {
        //initial encounter text
        logger.text = $"{enemy.name} draws near!";
    }

    private void EnemuActs()
    {
        //enemy attacks

        //player dies

        //player still alive; pass to player
    }

    private void PlayerActs()
    {
        //player attcks

        //enemy slain

        //enemy still alive; pass to enemy
    }

    private void PlayerVictory()
    {
        
    }

    private void EnemyVictory()
    {

    }
}

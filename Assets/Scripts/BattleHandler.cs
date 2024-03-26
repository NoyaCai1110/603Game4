using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public enum BattleEvent
{
    BattleStart,
    BattleEnd, 
    PlayerAction,
    EnemyAction,
    EnemyVictory, 
    PlayerVictory, 
    PlayerDies, 
    EnemyDies, 
    LevelUp, 
    GainXP, 
    GainLoot
  
}

public class BattleHandler : MonoBehaviour
{
    public Queue<BattleEvent> actionQueue = new Queue<BattleEvent>();

    public BattleEvent currentEvent; 

    public Player player;
    public Enemy enemy;
    public GameObject monster_container; //anchor for the monster sprite
    public GameObject monsterPrefab;
    public Logger battle_log; //battle text box

    public TextMeshProUGUI hp_text;
    public TextMeshProUGUI atk_text;
    public TextMeshProUGUI def_text;
    public TextMeshProUGUI lvl_text; 
    public void Setup(Player player, List<Enemy> enemyParty)
    {
        
        //load player
        this.player = player;

        hp_text.text = $"HP {player.HP}";
        atk_text.text = $"Attack: {player.Attack}";
        def_text.text = $"Defense: {player.Defense}";
        lvl_text.text = $"Lv. {player.level}";

        //set up player stats


        //load enemy
        //since parties are only 1v1s, just grab the first enemy

        this.enemy = new Enemy(enemyParty[0]);


        //add monster to the screen
        GameObject monsterSprite = GameObject.Instantiate(monsterPrefab, monster_container.transform);

        monsterSprite.GetComponent<Image>().sprite = enemy.sprite;

        Kickoff();

    }


    //after setting up references, start the fight!
    private void Kickoff()
    {
        actionQueue.Enqueue(BattleEvent.BattleStart);
        battle_log.ShowDialogue($"{enemy.name} draws near!");
        //initial encounter text

        //the player acts first (for now)
        actionQueue.Enqueue(BattleEvent.PlayerAction);
    }

    private void EnemyActs()
    {
        //enemy attacks
        int damage = Mathf.Max(0, (enemy.Attack - player.Defense));
        player.TakeDamage(damage);
        hp_text.text = $"HP {player.HP}";


        battle_log.ShowDialogue($"{enemy.name} attacks! You take {damage} damage!");

        //player dies
        if(player.HP <= 0)
        {
            actionQueue.Enqueue(BattleEvent.EnemyVictory);
        }
        //player still alive; pass to player
        else
        {
            actionQueue.Enqueue(BattleEvent.PlayerAction);
        }
    }

    private void PlayerActs()
    {
        //player attqcks
        int damage = Mathf.Max(0, (player.Attack - enemy.Defense));
        enemy.TakeDamage(damage);

        battle_log.ShowDialogue($"You strike the {enemy.name}. It takes {damage} damage!");

        //enemy slain
        if(enemy.HP <= 0)
        {
            actionQueue.Enqueue(BattleEvent.EnemyDies);
        }

        //enemy still alive; pass to enemy
        else
        {
            actionQueue.Enqueue(BattleEvent.EnemyAction);
        }
        

    }

    private void PlayerDeath(Player player)
    {
        battle_log.ShowDialogue("Hero is knocked out.");

        //if(player party alive count == 0)
        //actionQueue.Enqueue(BattleEvent.EnemyVictory)

        actionQueue.Enqueue(BattleEvent.EnemyVictory);
    }

    private void EnemyDeath(Enemy enemy)
    {
        //destroy sprites
        GameObject.Destroy(monster_container);


        battle_log.ShowDialogue($"{enemy.name} is slain!");
        actionQueue.Enqueue(BattleEvent.PlayerVictory);
    }

    private void PlayerVictory()
    {
        battle_log.ShowDialogue("Victory!");
        /// add exp
        player.exp += enemy.exp;
        actionQueue.Enqueue(BattleEvent.GainXP);

        //player reaches level up threshold
        //if(player.exp > threshold){
        //actionQueue.Enqueue(BattleEvent.Levelup)
        //}

        actionQueue.Enqueue(BattleEvent.GainLoot);
    }
    
    private void GainLoot()
    {
        battle_log.ShowDialogue($"Loot found: {enemy.dropped_gold} Gold");

        actionQueue.Enqueue(BattleEvent.BattleEnd);
    }

    private void GainXP() {
        battle_log.ShowDialogue($"You earned {enemy.exp} experience points.");
    }

    private void LevelUp()
    {
        player.LevelUp();
        lvl_text.text = $"Lv. {player.level}";
        battle_log.ShowDialogue($"You leveled up!");
    }

    private void EnemyVictory()
    {
        battle_log.ShowDialogue("The party was wiped out... (Press R to restart the game)");

        //if(enemy party alive count == 0)
        //actionQueue.Enqueue(BattleEvent.PlayerVictory)

        actionQueue.Enqueue(BattleEvent.PlayerVictory);
    }

    //press A to move to the next event 
    public void OnConfirm()
    {
        actionQueue.Dequeue();

        currentEvent = actionQueue.Peek();

        switch (currentEvent)
        {
            case BattleEvent.PlayerAction:
                {
                    PlayerActs();
                    break; 
                }
            case BattleEvent.EnemyAction:
                {
                    EnemyActs();
                    break;
                }
            case BattleEvent.PlayerDies:
                {
                    PlayerDeath(player);
                    break;
                }
            case BattleEvent.EnemyDies:
                {
                    EnemyDeath(enemy);
                    break; 
                }
            case BattleEvent.PlayerVictory:
                {
                    PlayerVictory();
                    break;
                }
            case BattleEvent.EnemyVictory:
                {
                    EnemyVictory();
                    break; 
                }
            case BattleEvent.GainLoot:
                {
                    GainLoot();
                    break;
                }
            case BattleEvent.GainXP:
                {
                    GainXP();
                    break; 
                }
            case BattleEvent.LevelUp:
                {
                    LevelUp();
                    break;
                }
            case BattleEvent.BattleEnd:
                {
                    player.EndBattle();
                    //remove the screen 
                    Destroy(this);
                    break;
                }

        }
    }
}
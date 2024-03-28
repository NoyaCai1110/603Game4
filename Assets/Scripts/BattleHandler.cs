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

public struct Action
{
   
}

public class BattleHandler : MonoBehaviour
{
    //handles the dialogue for the battle log 
    private Queue<BattleEvent> eventQueue = new Queue<BattleEvent>();
    private Queue<BattleEvent> actionQueue = new Queue<BattleEvent>();

    //each party member and their respective ui panel on the top screen
    private Dictionary<GameObject, PartyMember> partyPanels = new Dictionary<GameObject, PartyMember>();  

    public BattleEvent currentEvent; 

    public Player player;
    public Enemy enemy;
    public List<PartyMember> playerParty;
    public List<Enemy> enemyParty;

    public GameObject monster_container; //anchor for the monster sprite
    public GameObject monsterPrefab;
    public GameObject charpanelPrefab; 
    public Logger battle_log; //battle text box
    public void Setup(List<PartyMember> playerParty, List<Enemy> enemyParty)
    {

        //load player party
        this.playerParty = playerParty;

        int index = 1; 
        //set up player stats
        foreach (PartyMember member in playerParty)
        {
               
            //create a prefab and anchor it to one of the slots on the top part of the HUD
            GameObject newPanel = GameObject.Instantiate(charpanelPrefab, GameObject.Find($"Slot{index}").transform.position, Quaternion.identity);

            newPanel.transform.SetParent(this.transform);

            //Create a copy so that the original isn't permanently modified outside of runtime 
            PartyMember memberCopy = Instantiate(member);

            //set up the panel's initial values
            newPanel.GetComponent<CharPanel>().Setup(memberCopy);

            //store the panel 
            partyPanels.Add(newPanel, memberCopy);

            //move to the next slot
            index++;
        }

        //x-coord of the next sprite's position
        float xPos = 0;
        RectTransform rect_transform = monster_container.GetComponent<RectTransform>();

        float startPositionX = rect_transform.rect.xMin;
        startPositionX += Mathf.Abs(startPositionX) * .3f; 
        float endPositionX = rect_transform.rect.xMax;
        endPositionX -= endPositionX * .3f;

        //load enemies & their sprites
        for (int i = 0; i < enemyParty.Count; i++)
        {
            float ratio = (float)i / (float)(enemyParty.Count - 1);

            Vector3 spritePosition = new Vector3(Mathf.Lerp(startPositionX, endPositionX, ratio),
                                                 0,
                                                 0);

            //add monster to the screen
            GameObject monsterSprite = GameObject.Instantiate(monsterPrefab, monster_container.transform);

            monsterSprite.GetComponent<RectTransform>().localPosition = spritePosition;

            //assign parent 
            monsterSprite.transform.SetParent(monster_container.transform);

            //set sprite
            monsterSprite.GetComponent<Image>().sprite = enemyParty[i].sprite;
        }
  
        this.enemy = new Enemy(enemyParty[0]);

        Kickoff();

    }


    //after setting up references, start the fight!
    private void Kickoff()
    {
        eventQueue.Enqueue(BattleEvent.BattleStart);
        battle_log.ShowDialogue($"A group of monsters draw near!");
        //initial encounter text

        //the player acts first (for now)
        eventQueue.Enqueue(BattleEvent.PlayerAction);
    }

    private void EnemyActs()
    {
        //enemy attacks
        int damage = Mathf.Max(0, (enemy.Attack - player.Defense));
        player.TakeDamage(damage);
     


        battle_log.ShowDialogue($"{enemy.name} attacks! You take {damage} damage!");

        //player dies
        if(player.HP <= 0)
        {
            eventQueue.Enqueue(BattleEvent.EnemyVictory);
        }
        //player still alive; pass to player
        else
        {
            eventQueue.Enqueue(BattleEvent.PlayerAction);
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
            eventQueue.Enqueue(BattleEvent.EnemyDies);
        }

        //enemy still alive; pass to enemy
        else
        {
            eventQueue.Enqueue(BattleEvent.EnemyAction);
        }
        

    }

    private void PlayerDeath(Player player)
    {
        battle_log.ShowDialogue("Hero is knocked out.");

        //if(player party alive count == 0)
        //eventQueue.Enqueue(BattleEvent.EnemyVictory)

        eventQueue.Enqueue(BattleEvent.EnemyVictory);
    }

    private void EnemyDeath(Enemy enemy)
    {
        //destroy sprites
        GameObject.Destroy(monster_container);


        battle_log.ShowDialogue($"{enemy.name} is slain!");
        eventQueue.Enqueue(BattleEvent.PlayerVictory);
    }

    private void PlayerVictory()
    {
        battle_log.ShowDialogue("Victory!");
        /// add exp
        player.exp += enemy.exp;
        eventQueue.Enqueue(BattleEvent.GainXP);

        //player reaches level up threshold
        //if(player.exp > threshold){
        //eventQueue.Enqueue(BattleEvent.Levelup)
        //}

        eventQueue.Enqueue(BattleEvent.GainLoot);
    }
    
    private void GainLoot()
    {
        battle_log.ShowDialogue($"Loot found: {enemy.dropped_gold} coins");
        player.AddCoins(enemy.dropped_gold);

        eventQueue.Enqueue(BattleEvent.BattleEnd);
    }

    private void GainXP() {
        battle_log.ShowDialogue($"You earned {enemy.exp} experience points.");
    }

    private void LevelUp()
    {
        player.LevelUp();
        battle_log.ShowDialogue($"You leveled up!");
    }

    private void EnemyVictory()
    {
        battle_log.ShowDialogue("The party was wiped out...");

        //if(enemy party alive count == 0)
        //eventQueue.Enqueue(BattleEvent.PlayerVictory)

        eventQueue.Enqueue(BattleEvent.PlayerVictory);
    }

    //press A to move to the next event 
    //public void OnConfirm()
    //{
    //    eventQueue.Dequeue();

    //    currentEvent = eventQueue.Peek();

    //    switch (currentEvent)
    //    {
    //        case BattleEvent.PlayerAction:
    //            {
    //                PlayerActs();
    //                break; 
    //            }
    //        case BattleEvent.EnemyAction:
    //            {
    //                EnemyActs();
    //                break;
    //            }
    //        case BattleEvent.PlayerDies:
    //            {
    //                PlayerDeath(player);
    //                break;
    //            }
    //        case BattleEvent.EnemyDies:
    //            {
    //                EnemyDeath(enemy);
    //                break; 
    //            }
    //        case BattleEvent.PlayerVictory:
    //            {
    //                PlayerVictory();
    //                break;
    //            }
    //        case BattleEvent.EnemyVictory:
    //            {
    //                EnemyVictory();
    //                break; 
    //            }
    //        case BattleEvent.GainLoot:
    //            {
    //                GainLoot();
    //                break;
    //            }
    //        case BattleEvent.GainXP:
    //            {
    //                GainXP();
    //                break; 
    //            }
    //        case BattleEvent.LevelUp:
    //            {
    //                LevelUp();
    //                break;
    //            }
    //        case BattleEvent.BattleEnd:
    //            {
    //                player.EndBattle();
    //                //remove the screen 
    //                Destroy(this);
    //                break;
    //            }

    //    }
    //}
}
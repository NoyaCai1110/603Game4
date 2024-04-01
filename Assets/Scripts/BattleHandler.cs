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
    IssuingCommands,
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


public struct Turn
{
    public Turn(Combatant combatant)
    {
        owner = combatant;
        ability = null; //make sure that the ability is assigned!
    }

    public Combatant owner;
    public Ability ability; 
}


public class BattleHandler : MonoBehaviour
{
    //handles the dialogue for the battle log 
    private Queue<string> eventQueue = new Queue<string>();
    private Queue<Turn> actionQueue = new Queue<Turn>();

    //each party member and their respective ui panel on the top screen
    public Dictionary<GameObject, PartyMember> partyPanels = new Dictionary<GameObject, PartyMember>();
    public Dictionary<GameObject, Enemy> monster_sprites = new Dictionary<GameObject, Enemy>();

    private Turn currentTurn;
    private BattleEvent currentEvent;
    private bool issuing_commands = false;

    public List<PartyMember> playerParty;
    public List<Enemy> enemyParty;

    public GameObject monster_container; //anchor for the monster sprite
    public GameObject monsterPrefab;
    public GameObject charpanelPrefab;
    public CommandManager commandPanel;
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

            //attach each enemy to their respective sprite
            monster_sprites.Add(monsterSprite, enemyParty[i]);
            //and then store them in backend (for targeting)
            //instantiate so that the base object isn't permanently modified
            this.enemyParty.Add(Instantiate(enemyParty[i]));


        }

        Kickoff();



    }

    //after setting up references, start the fight!
    private void Kickoff()
    {
        battle_log.ShowDialogue($"Monsters draw near!");
        //initial encounter text

        LoadRound();

    }

    //https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.sort?view=net-8.0#system-collections-generic-list-1-sort(system-comparison((-0)))
    //load the initiative order in order of each Combatant's Speed
    private void LoadRound()
    {
        int sortBySpeed(Combatant a, Combatant b)
        {
            if (a == null)
            {
                if (b == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (b == null)
                {
                    return 1;
                }
                else
                {
                    //if a and b are valid, check speed;
                    int returnValue = a.Speed.CompareTo(b.Speed);

                    if (returnValue != 0)
                    {
                        return returnValue;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        List<Combatant> combatants = new List<Combatant>();

        foreach (Enemy e in enemyParty)
        {
            if (!e.isDead)
            {
                combatants.Add(e);
            }
        }

        foreach (PartyMember p in playerParty)
        {
            if (!p.isDead)
            {
                combatants.Add(p);
            }
        }

        //sort by Speed
        combatants.Sort(sortBySpeed);

        for (int i = combatants.Count - 1; i == 0; i--)
        {
            Turn newTurn = new Turn(combatants[i]);

            actionQueue.Enqueue(newTurn);
        }


    }

    public void PerformTurn(Turn currentTurn)
    {
        List<string> text_events = new List<string>(); 
        if (currentTurn.owner is Enemy)
        {
            text_events.AddRange((currentTurn.owner as Enemy).Act(currentTurn.ability));
        }

        if (currentTurn.owner is PartyMember)
        {
            text_events.AddRange((currentTurn.owner as PartyMember).Act(currentTurn.ability));
        }

        foreach(string item in text_events)
        {
            eventQueue.Enqueue(item);
        }
    }

    private void PlayerVictory()
    {
        battle_log.ShowDialogue("Victory!");
        /// add exp
        //player reaches level up threshold
        //if(player.exp > threshold){
        //eventQueue.Enqueue(BattleEvent.Levelup)
        //}

    }

    private void GainLoot()
    {

    }

    private void GainXP()
    {

    }

    private void LevelUp()
    {

    }

    private void EnemyVictory()
    {
        battle_log.ShowDialogue("The party was wiped out...");

        //if(enemy party alive count == 0)
        //eventQueue.Enqueue(BattleEvent.PlayerVictory)

    }


    public void LoadNextEvent()
    {
        battle_log.ShowDialogue(eventQueue.Dequeue());
    }

    public void RecieveCommand(PartyMember member, Ability ability)
    {
        //Find the turn and modify the action
        Turn action_owner; 
        foreach(Turn t in actionQueue)
        {
            if (t.owner == member)
            {
                action_owner = t;
                break;
            }
        }

        action_owner.ability = ability;
    }

    //after commands are inputted, set up initiative and then close command panel
    public void EndCommands()
    {
    
        commandPanel.Enable(false);
        issuing_commands = false;
        OnConfirm(); //load in the first action in the action queue
    }


    private void UpdateUI()
    {
        foreach (GameObject panel in partyPanels.Keys)
        {
            panel.GetComponent<CharPanel>().Setup(partyPanels[panel]);
        }

        foreach(GameObject e in monster_sprites.Keys)
        {
            if (monster_sprites[e].isDead)
            {
                monster_sprites.Remove(e);
                GameObject.Destroy(e);
            }
        }

    }
    //public void LoadCommands;
    //press A to move to the next event 
    public void OnConfirm()
    {
        //this input is disabled while the command panel is enabled 
        if (!issuing_commands)
        {
            //check the turn 
            bool hasTurn = actionQueue.TryPeek(out currentTurn);

            
            //tell the turn's owner to Act()
            //perform the action, which should load the battleEvent queue
            if (hasTurn)
            {
                //skip turns that have nothing
                if(currentTurn.ability.abilityType == AbilityType.None)
                {
                    actionQueue.Dequeue();
                }

                PerformTurn(currentTurn);

                //cleanup 
                UpdateUI();

                //pass through all loaded battle log text before passing to the next action
                if (eventQueue.Count != 0)
                {
                    LoadNextEvent();
                }
                else
                {
                    actionQueue.Dequeue();


                }
            }

            //This should always run first at the very first round of battle
            //when the queue is emptied, load in command panel
            if (actionQueue.Count == 0)
            {
                LoadRound();
                issuing_commands = true;
                commandPanel.Enable(true);
                commandPanel.Setup();
  
            }
        }
    }
}
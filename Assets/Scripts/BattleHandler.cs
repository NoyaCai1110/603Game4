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
    public Turn(Combatant combatant, Ability ability)
    {
        owner = combatant;
        this.ability = ability;
    }

    public Combatant owner;
    public Ability ability;
}


public class BattleHandler : MonoBehaviour
{
    public Player player;
    //handles the dialogue for the battle log 
    private Queue<string> eventQueue = new Queue<string>();

    //handles actions
    private List<Turn> actionQueue = new List<Turn>();
    private int queue_index = -1;

    //each party member and their respective ui panel on the top screen
    public Dictionary<GameObject, PartyMember> partyPanels = new Dictionary<GameObject, PartyMember>();
    public Dictionary<GameObject, Enemy> monster_sprites = new Dictionary<GameObject, Enemy>();

    private Turn currentTurn;
    private bool issuing_commands = false;
    private bool firstRound = true;
    private bool battleEnded = false;
    private bool enemyWon = false;
    private bool playerWon = false;

    public Ability nothing;

    public List<PartyMember> playerParty;
    public List<Enemy> enemyParty;

    public GameObject monster_container; //anchor for the monster sprite
    public GameObject monsterPrefab;
    public GameObject charpanelPrefab;
    public CommandManager commandPanel;
    public Logger battle_log; //battle text box
    public void Setup(List<PartyMember> playerParty, List<Enemy> enemyParty)
    {
        player = GameObject.Find("Player").GetComponent<Player>();

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
            monster_sprites.Add(monsterSprite, Instantiate(enemyParty[i]));
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

    }

    //https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.sort?view=net-8.0#system-collections-generic-list-1-sort(system-comparison((-0)))
    //load the initiative order in order of each Combatant's Speed
    private void LoadRound()
    {
        //clear previous initiative
        actionQueue.Clear();

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

        for (int i = 0; i < combatants.Count; i++)
        {

            Turn newTurn = new Turn(combatants[combatants.Count - 1 - i], nothing);

            actionQueue.Add(newTurn);
        }


    }

    public void PerformTurn(Turn currentTurn)
    {
        List<string> text_events = new List<string>();
        if (currentTurn.owner is Enemy)
        {
            List<PartyMember> temp = new List<PartyMember>();
            foreach (PartyMember p in partyPanels.Values)
            {
                temp.Add(p);
            }
            text_events.AddRange((currentTurn.owner as Enemy).Act(temp));
        }

        if (currentTurn.owner is PartyMember)
        {
            text_events.AddRange((currentTurn.owner as PartyMember).Act(currentTurn.ability));
        }

        foreach (string item in text_events)
        {
            eventQueue.Enqueue(item);
        }
    }

    public void LoadNextEvent()
    {
        string text = eventQueue.Peek();
        battle_log.ShowDialogue(text);
        eventQueue.Dequeue();
    }

    public void RecieveCommand(PartyMember member, Ability ability)
    {
        Turn newTurn = new Turn(member, ability);

        //Find the turn and modify the action
        for (int i = 0; i < actionQueue.Count; i++)
        {
            if (actionQueue[i].owner == newTurn.owner)
            {
                actionQueue[i] = newTurn;
            }
        }
    }

    //after commands are inputted, set up initiative and then close command panel
    public void EndCommands()
    {
        queue_index = 0; //set to index 0 
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

        foreach (GameObject e in monster_sprites.Keys)
        {
            //set health bars
            e.GetComponent<MonsterBar>().SetFill((float)monster_sprites[e].HP / (float)monster_sprites[e].MaxHP);

        }

    }

    private void PlayerVictory()
    {
        int totalExp = 0;
        int totalGold = 0;
        foreach (Enemy e in enemyParty)
        {
            totalExp += e.exp;
            totalGold += e.dropped_gold;
        }

        player.AddCoins(totalGold);
        eventQueue.Enqueue("You emerged victorious!");
        eventQueue.Enqueue($"The monsters dropped {totalGold} coins.");
    }

    private void EnemyVictory()
    {
        eventQueue.Enqueue("The party was wiped out...!");
    }
    //check for any notable changes in the game
    private void UpdateState()
    {
        bool an_enemy_is_alive = false;
        foreach (GameObject e in monster_sprites.Keys)
        {
            if (!monster_sprites[e].isDead)
            {
                an_enemy_is_alive = true;
            }
            if (monster_sprites[e].isDead)
            {
                e.SetActive(false);
            }
        }

        //check for enemy wipe
        if (!an_enemy_is_alive)
        {
            if (!battleEnded)
            {
                eventQueue.Clear();
                PlayerVictory();

                //reward loot, exp,
                //then close the handler
                battleEnded = true;
                playerWon = true;
                PlayerMovement PM = player.GetComponent<PlayerMovement>();
                PM.busy = false;
            }

        }
        int playerDeadCount = 0;

        //check for player wipe
        foreach (PartyMember p in partyPanels.Values)
        {
            if (p.isDead)
            {
                playerDeadCount++;
            }
        }
        if (playerDeadCount == playerParty.Count)
        {
            if (!battleEnded)
            {
                eventQueue.Clear();

                EnemyVictory();

                //then close the handler
                battleEnded = true;
                enemyWon = true;
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
            //first round edge case
            if (firstRound)
            {
                firstRound = false;
                queue_index = -1;
                LoadRound();
                issuing_commands = true;
                commandPanel.Enable(true);
                commandPanel.Setup();

                queue_index = actionQueue.Count;

                return;
            }

            //handle pre-round actions
            if (queue_index == -1)
            {

            }
            //per turn actions
            else if (queue_index < actionQueue.Count && actionQueue.Count != 0)
            {
                bool noEventsRemaining = (eventQueue.Count == 0);

                //don't perform the next turn until all battle log events have been handled
                if (noEventsRemaining)
                {
                    //if the battle has ended due to a wipe on either sides, or other circumstances, close the ui
                    if (battleEnded)
                    {
                        //save changes to HP and MP
                        int i = 0;
                        foreach (PartyMember p in partyPanels.Values)
                        {
                            playerParty[i].Copy(p);
                            i++;
                        }

                        if (playerWon)
                        {

                            player.WinBattle();
                        }

                        if (enemyWon)
                        {
                            player.LoseBattle();
                        }

                        //preserve any changes to each party member back onto the play
                        GameObject.Destroy(gameObject);

                        return;
                    }

                    //tell the turn's owner to Act()
                    //perform the action, which should load the battleEvent queue

                    currentTurn = actionQueue[queue_index];

                    //skip turns that have nothing

                    while (currentTurn.ability.abilityType == AbilityType.None || currentTurn.owner.isDead)
                    {
                        if (currentTurn.owner is Enemy)
                        {
                            if (currentTurn.owner.isDead)
                            {
                                queue_index++;
                            }

                            if (queue_index == actionQueue.Count)
                            {
                                break;
                            }

                            currentTurn = actionQueue[queue_index];
                            break;
                        }

                        if (currentTurn.owner is PartyMember)
                        {
                            queue_index++;
                        }

                        if (queue_index == actionQueue.Count)
                        {
                            break;
                        }

                        currentTurn = actionQueue[queue_index];
                    }

                    PerformTurn(currentTurn);

                }

                if (eventQueue.Count != 0)
                {
                    LoadNextEvent();
                }

                if (eventQueue.Count == 0)
                {
                    queue_index++;
                    //cleanup 
                    UpdateUI();
                    UpdateState();
                }

                //end of round actions
                if (queue_index >= actionQueue.Count)
                {
                    queue_index = -1;


                    //if battle has ended, do not open the command panel
                    if (battleEnded)
                    {
                        queue_index = 0;
                    }
                    else
                    {
                        LoadRound();
                        issuing_commands = true;
                        commandPanel.Enable(true);
                        commandPanel.Setup();
                    }


                }

            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CommandManager : MonoBehaviour
{
    public enum UIState
    {
        Init,
        ManualCommand,
        Targeting,
        SkillMenu,
        ItemMenu,

    }
    public Dictionary<GameObject, Enemy> enemyParty;
    public Dictionary<GameObject, PartyMember> playerParty;
    public BattleHandler handler;

    private List<PartyMember> command_queue = new List<PartyMember>();
    private List<Ability> queued_abilties = new List<Ability>();
    private int queue_index = 0;
    private PartyMember member_to_command;

    public GameObject ui_wrapper;
    public GameObject manual_panel;
    public GameObject init_panel;
    public GameObject skill_panel;
    public Button bk_button;
    public GameObject targeting_panel;
    public TextMeshProUGUI member_name;
    public TextMeshProUGUI command_desc;

    //every combatant should have a basic attack
    public Ability basicAttack;
    public Ability nullAction;

    //current selected ability whern targetting 
    private Ability loadedAbility;

    private UIState uiState;


    private List<GameObject> panels = new List<GameObject>();


    public void Setup()
    {

        this.enemyParty = handler.monster_sprites;
        this.playerParty = handler.partyPanels;

        //don't add panels if this has already been setup
        if (panels.Count == 0)
        {
            panels.Add(manual_panel);
            panels.Add(init_panel);
            panels.Add(targeting_panel);
            panels.Add(skill_panel);
        }

        //load in the queue
        foreach (PartyMember member in handler.playerParty)
        {
            command_queue.Add(member);
        }

        member_name.text = "";
        command_desc.text = "";


        uiState = UIState.Init;
        //first, show the initial screen to allow manual / auto combat
        ShowPanel(init_panel);


    }


    public void Enable(bool showing)
    {
        ui_wrapper.SetActive(showing);
    }


    //Clicking 'Issue Orders'
    public void OnManual()
    {
        //if all commands have been set, end the command dialogue
        if (queue_index == playerParty.Count)
        {
            SendCommands();
            return;
        }

        do
        {
            //begin issuing commands for the first party member
            member_to_command = command_queue[queue_index];

            //ignore dead members
            if (member_to_command.isDead)
            {
                Ability nothing = Instantiate(nullAction);
                nothing.owner = member_to_command.name;
                queued_abilties.Add(nothing);
                queue_index++;

                if (queue_index >= playerParty.Count)
                {
                    SendCommands();
                    return;
                }
            }

        }
        while (member_to_command.isDead);

        LoadCommands(member_to_command);
    }

    public void SendCommands()
    {
        for (int i = 0; i < command_queue.Count; i++)
        {
            //DEBUG PRINT
            Debug.Log($"{queued_abilties[i].owner}, {queued_abilties[i].ability_name}");
            handler.RecieveCommand(command_queue[i], queued_abilties[i]);
        }
        queue_index = 0;
        queued_abilties.Clear();
        command_queue.Clear();

        handler.EndCommands();
    }

    private void LoadCommands(PartyMember member)
    {

        ShowPanel(manual_panel);

        //update UI
        uiState = UIState.ManualCommand;
        member_name.text = member.name;
        command_desc.text = "";
    }

    //Clicking 'Auto'
    public void OnAuto()
    {
        //issue commands automatically
        //destroy the panel
    }

    //Clicking 'Attack' 
    public void OnClickAttack(bool auto)
    {
       
        Ability abilityCopy = Instantiate(basicAttack);

        LoadTargets(basicAttack.targetType, abilityCopy);
        uiState = UIState.Targeting;
        ShowPanel(targeting_panel);
    }


    private void LoadTargets(Targetable targetType, Ability ability)
    {

        command_desc.text = ability.description;

        if (targetType == Targetable.EnemyOnly)
        {
            foreach (GameObject panel in enemyParty.Keys)
            {

                Button button = panel.GetComponent<Button>();
                button.interactable = true;
                button.onClick.AddListener(() => { OnClickTarget(enemyParty[panel], ability); });
                
            
            }
        }

        if (targetType == Targetable.FriendlyOnly)
        {
            foreach (GameObject target in playerParty.Keys)
            {
                Button button = target.GetComponent<Button>();
                button.interactable = true;
                button.onClick.AddListener(() => { OnClickTarget(playerParty[target], ability); });

              
            }
        }

        if (targetType == Targetable.Any)
        {
            foreach (GameObject panel in enemyParty.Keys)
            {

                Button button = panel.GetComponent<Button>();
                button.interactable = true;
                button.onClick.AddListener(() => { OnClickTarget(enemyParty[panel], ability); });

          
            }

            foreach (GameObject target in playerParty.Keys)
            {
                Button button = target.GetComponent<Button>();
                button.interactable = true;
                button.onClick.AddListener(() => { OnClickTarget(playerParty[target], ability); });

            

            }
        }
    }

    private void DisableTargets()
    {
        foreach (GameObject target in enemyParty.Keys)
        {
            Button button = target.GetComponent<Button>();
            button.interactable = false;
            button.onClick.RemoveAllListeners();
        }

        foreach (GameObject target in enemyParty.Keys)
        {
            Button button = target.GetComponent<Button>();
            button.interactable = false;
            button.onClick.RemoveAllListeners();
        }
    }


    //When the user clicks a target on the screen
    public void OnClickTarget(Combatant combatant, Ability ability)
    {

        //load the action to the queue; its set to be performed
        ability.owner = member_to_command.name;

        ability.targets = new List<Combatant>();
        ability.targets.Add(combatant);

        queued_abilties.Add(ability);

        //go to next party member
        queue_index++;
        //clear targetting panel
        DisableTargets();
        OnManual();

    }

    //Clicking 'Defend'
    public void OnDefend()
    {

    }

    //Clicking 'Item'
    public void OnItem()
    {

    }

    //Clicking 'Skill'
    public void OnSkill()
    {
        uiState = UIState.SkillMenu;

        ShowPanel(skill_panel);
        LoadSkills(member_to_command);

    }

    private void LoadSkills(PartyMember member)
    {
        Button[] skillButtons = skill_panel.GetComponentsInChildren<Button>();
        for (int i = 0; i < 3; i++)
        {
            Button skillButton = skillButtons[i];
            TextMeshProUGUI buttonName = skillButton.GetComponentInChildren<TextMeshProUGUI>();

            //clear old listeners first
            skillButton.onClick.RemoveAllListeners();

            //if the member doesn't have a first, second, or third ability
            if (i > member.abilities.Count - 1 || i == 3) //index #3 is the back button; we don't want to change that
            {
                buttonName.text = "?";
                skillButton.interactable = false;
            }
            //locked abilities are also not shown
            else if (member.abilities[i].unlocked_lvl > member.level)
            {
                buttonName.text = "?";
                skillButton.interactable = false;
            }
            else
            {
                buttonName.text = $"{member.abilities[i].ability_name}";
                skillButton.interactable = true;

                Ability abilityCopy = Instantiate(member.abilities[i]);

                skillButton.onClick.AddListener(() => { LoadSkillTargets(abilityCopy); });

                if (member_to_command.MP < abilityCopy.MP_Cost)
                {
                    skillButton.interactable = false;
                }

            }
        }

        void LoadSkillTargets(Ability ability)
        {
            //check the tags to see if the ability affects enemies or foes
            switch (ability.targetType)
            {
                case Targetable.AoEFriendly:
                    {
                        //load the action to the queue; its set to be performed
                        ability.owner = member_to_command.name;
                        ability.targets = new List<Combatant>();

                        foreach (PartyMember member in playerParty.Values)
                        {
                            ability.targets.Add(member);
                        }


                        queued_abilties.Add(ability);

                        //go to next party member
                        queue_index++;
                        //clear targetting panel
                        DisableTargets();
                        OnManual();
                        break;
                    }
                case Targetable.AoEEnemy:
                    {
                        //load the action to the queue; its set to be performed
                        ability.owner = member_to_command.name;
                        ability.targets = new List<Combatant>();

                        foreach (Enemy e in enemyParty.Values)
                        {
                            ability.targets.Add(e);
                        }


                        queued_abilties.Add(ability);

                        //go to next party member
                        queue_index++;
                        //clear targetting panel
                        DisableTargets();
                        OnManual();
                        break;
                    }
                case Targetable.Special:
                    {
                        break;
                    }
                default:
                    {
                        //open targeting dialogue
                        LoadTargets(ability.targetType, ability);
                        ShowPanel(targeting_panel);

                        break;
                    }
            }
        }
    }

    //toggles off all panels, then shows the targetted panels
    public void ShowPanel(GameObject panel)
    {
        foreach (GameObject p in panels)
        {
            p.SetActive(false);
        }

        panel.SetActive(true);
    }

    public void OnBack()
    {
        member_name.text = "";
        command_desc.text = "";

        if (uiState == UIState.Init)
        {
            return;
        }

        if (uiState != UIState.ManualCommand)
        {

            if (queue_index == 0)
            {

            }
            ShowPanel(manual_panel);
            uiState = UIState.ManualCommand;
            DisableTargets();


            return;
        }

        if (queue_index == 0)
        {
            ShowPanel(init_panel);
            uiState = UIState.Init;

        }
        else
        {
            member_name.text = "";
            queue_index--;
            queued_abilties.RemoveAt(queue_index);

            if (queue_index == 0)
            {
                ShowPanel(init_panel);
                uiState = UIState.Init;
            }
            else
            {
                //ignore dead members
                if (member_to_command.isDead)
                {
                    queued_abilties.RemoveAt(queue_index);
                    queue_index--;

                }

            }


            OnManual();
        }

    }
}

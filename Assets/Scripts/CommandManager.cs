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
    private int queue_index = 0;
    private PartyMember member_to_command;

    public GameObject ui_wrapper; 
    public GameObject manual_panel;
    public GameObject init_panel;
    public Button bk_button;
    public GameObject targeting_panel;
    public TextMeshProUGUI member_name;
    public TextMeshProUGUI command_desc;

    private UIState uiState;


    private List<GameObject> panels = new List<GameObject>();


    public void Setup()
    {

        this.enemyParty = handler.monster_sprites;
        this.playerParty = handler.partyPanels;

        panels.Add(manual_panel);
        panels.Add(init_panel);
        panels.Add(targeting_panel);

        foreach (PartyMember member in handler.playerParty)
        {
            command_queue.Add(member);
        }

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
            handler.EndCommands();
            queue_index = 0;
            return;
        }

        do {
            //begin issuing commands for the first party member
            member_to_command = command_queue[queue_index];

            //ignore dead members
            if (member_to_command.isDead)
            {
                queue_index++;
            }

        }
        while (member_to_command.isDead);

        LoadCommands(member_to_command);
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
        command_desc.text = "Strike an enemy with your weapon.";
        LoadTargets("enemy");
        ShowPanel(targeting_panel);
    }

    private void LoadTargets(string targetType)
    {
        if(targetType == "enemy")
        {
            foreach(GameObject target in enemyParty.Keys)
            {
                Button button = target.GetComponent<Button>();
                button.interactable = true;
                button.onClick.AddListener(()=> { OnClickTarget(enemyParty[target]);});
            }
        }

        if (targetType == "player")
        {
            foreach(GameObject target in playerParty.Keys)
            {
                Button button = target.GetComponent<Button>();
                button.interactable = true;
                button.onClick.AddListener(() => { OnClickTarget(playerParty[target]);});
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
    public void OnClickTarget(Combatant combatant)
    {
        //tell BattleHandler about the action
        

        //go to next party member
        queue_index++;
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
        if(uiState == UIState.Init)
        {
            return;
        }

        if(uiState != UIState.ManualCommand)
        {

            if(queue_index == 0){

            }
            ShowPanel(manual_panel);
            uiState = UIState.ManualCommand;
            DisableTargets();
 

            return; 
        }

        if(queue_index == 0)
        {
            ShowPanel(init_panel);
            uiState = UIState.Init;
            
        }
        else
        {
            member_name.text = "";
            queue_index--;
            OnManual();
        }

    }
}

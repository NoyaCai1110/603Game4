using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public List<Enemy> enemyParty;
    public BattleHandler handler;

    private Queue<PartyMember> command_queue = new Queue<PartyMember>();
    private PartyMember member_to_command;

    public GameObject ui_panel;
    public GameObject manual_panel;
    public GameObject init_panel;
    public GameObject targeting_panel;

    public CommandManager(BattleHandler handler)
    {
        this.handler = handler;
        this.enemyParty = handler.enemyParty;

        foreach(PartyMember member in handler.playerParty)
        {
            command_queue.Enqueue(member);   
        }

        //first, show the initial screen to allow manual / auto combat
    }

   
    private void PromptCommand(PartyMember member)
    {

    }

    public void Show(bool showing)
    {
        ui_panel.SetActive(showing);
    }
    public void OnManual()
    {
        
    }

    public void OnAuto()
    {

    }

    public void OnClickAttack()
    {

    }

    public void OnClickDefend()
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

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
  
    // Start is called before the first frame update

    public List<PartyMember> party;
    public int coins;

    public Enemy testEnemy;

    Rigidbody2D rb;
    public bool isFreeze;
    public GameObject battleHUDPrefab;
    private FightEncounter currentEncounter; 
    

    
    void Start()
    {
        //create initial party
        CreateInitialParty();
        coins = GetComponentInParent<Inventory>().Coins;
        if (GetComponent<Rigidbody2D>() != null) 
            rb = GetComponent<Rigidbody2D>();
        else
            rb = transform.AddComponent<Rigidbody2D>();
        rb.isKinematic = false;
        rb.gravityScale = 0.0f;
        isFreeze = false;

    }
    private void CreateInitialParty()
    {
        AddPartyMember("bard", "Hero");
        AddPartyMember("fighter", "Jack");
    }

    public void AddPartyMember(string char_class, string char_name)
    {
        PartyMember newMember = GameObject.Find("GameManager").GetComponent<CharCreator>().CreateCharacter(char_class, char_name);

        party.Add(newMember);
    }

    void UpdateMovement()
    {
        if (rb == null) return;
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector2(0, 10);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(0, -10);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(10, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-10, 0);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    //handler for adding gold

    public void AddCoins(int amount)
    {
        coins += amount;
    }
    

    public void BeginBattle(FightEncounter encounter)
    {

        /*conjure the battle HUD */
        GameObject battleHUD = GameObject.Instantiate(battleHUDPrefab);
        List<Enemy> e_list = encounter.enemies;
        currentEncounter = encounter;
        battleHUD.GetComponent<BattleHandler>().Setup(party, e_list);

        
    }

    public void WinBattle()
    {
        currentEncounter.defeated = true;
        //tell the move script that they can move now
        this.gameObject.GetComponent<PlayerMovement>().busy = false;
    }

    public void LoseBattle()
    {

    }

}

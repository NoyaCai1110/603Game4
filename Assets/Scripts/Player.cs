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
    public int level;
    public int exp;

    public Enemy testEnemy;

    Rigidbody2D rb;
    public bool isFreeze;
    public GameObject battleHUDPrefab;
    private GameObject currentEncounter;
    

    
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

        //FOR DEBUGGING
        /*List<Enemy> testEncounter = new List<Enemy>();
        testEncounter.Add(testEnemy);
        testEncounter.Add(testEnemy);
        testEncounter.Add(testEnemy);

        BeginBattle(testEncounter);*/
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
    
    void Freeze()
    {
        rb.velocity = new Vector2 (0, 0);
    }

    public void BeginBattle(List<Enemy> enemyParty)
    {

        /*conjure the battle HUD */
        GameObject battleHUD = GameObject.Instantiate(battleHUDPrefab);
        battleHUD.GetComponent<BattleHandler>().Setup(party, enemyParty);

        currentEncounter = battleHUD;
    }

    public void EndBattle()
    {
        GameObject.Destroy(currentEncounter);
    }
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //begin battle when encountering an enemy
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyList enemyParty = collision.gameObject.GetComponent<EnemyList>();
            BeginBattle(enemyParty.enemies);
            GameObject.Destroy(collision.gameObject);

        }
    }
}

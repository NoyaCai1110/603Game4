using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public int HP;
    public int MaxHP;
    public int Attack;
    public int Defense;
    public int loots;
    public int level;
    public int exp; 

    Rigidbody2D rb;
    public bool isFreeze;
    public GameObject battleHUDPrefab;

    private GameObject currentEncounter; 

    void Start()
    {
        HP = 10;
        MaxHP = 10;
        Attack = 3;
        Defense = 1;
        loots = 0;
        if (GetComponent<Rigidbody2D>() != null) 
            rb = GetComponent<Rigidbody2D>();
        else
            rb = transform.AddComponent<Rigidbody2D>();
        rb.isKinematic = false;
        rb.gravityScale = 0.0f;
        isFreeze = false;
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

    //handler for taking damage 
    public void TakeDamage(int damage)
    {
        this.HP = Mathf.Max(0, this.HP - damage);

        if(HP <= 0)
        {
            Lose();
        }
    }

    //Healing health
    public void HealDamage(int amount)
    {
        this.HP = Mathf.Max(this.MaxHP, this.HP + amount);
    }

    public void LevelUp()
    {
        this.level += 1;
        //level up stuff
    }

    void Lose()
    {
        
    }
    void Freeze()
    {
        rb.velocity = new Vector2 (0, 0);
    }

    void BeginBattle(List<Enemy> enemyParty)
    {
        Freeze();
        isFreeze = true;


        /*conjure the battle HUD */
        GameObject battleHUD = GameObject.Instantiate(battleHUDPrefab);
        battleHUD.GetComponent<BattleHandler>().Setup(this, enemyParty);

        currentEncounter = battleHUD;


        /*while(HP > 0)
        {
            Fight(enemy);
            Debug.Log(HP + " " + enemy.HP);
            if (enemy.HP <= 0)
            {
                Win(enemy);
                Destroy(collision.gameObject);
                break;
            }
            if(HP <= 0)
            {
                Lose();
                //YOU DIE!!
            }
        }*/
    }

    public void EndBattle()
    {
        GameObject.Destroy(currentEncounter);
        isFreeze = false;
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
    void Update()
    {
        //commented out for testing purposes
        if(!isFreeze) 
        {
            UpdateMovement();
        }
        else 
        {
            Freeze();
        }
    }
}

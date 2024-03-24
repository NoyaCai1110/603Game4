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
    Rigidbody2D rb;
    public bool isFreeze;

    void Start()
    {
        HP = 50;
        MaxHP = 50;
        Attack = 30;
        Defense = 10;
        loots = 10;
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
    void Fight(Enemy enemy)
    {
        //Deal Damage
        if((Attack - enemy.Defense) > 0)
        {
            enemy.HP -= (Attack - enemy.Defense);
        }
        //Take Damage
        if((enemy.Attack - Defense) > 0)
        {
            HP -= (enemy.Attack - Defense);
        }
        
    }
    void Win (Enemy enemy)
    {
        loots += enemy.loots;
    }
    void Lose(Enemy enemy)
    {

    }
    void Freeze()
    {
        rb.velocity = new Vector2 (0, 0);
    }
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Freeze();
            isFreeze = true;
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            while(HP > 0)
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
                    Lose(enemy);
                    //YOU DIE!!
                }
            }
            isFreeze = false;
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

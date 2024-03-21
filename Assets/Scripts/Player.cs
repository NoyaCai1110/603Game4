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
    Rigidbody2D rb;

    void Start()
    {
        HP = 50;
        MaxHP = 50;
        Attack = 0;
        Defense = 0;
        if (GetComponent<Rigidbody2D>() != null) 
            rb = GetComponent<Rigidbody2D>();
        else
            rb = transform.AddComponent<Rigidbody2D>();
        rb.isKinematic = false;
        rb.gravityScale = 0.0f;
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
    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class Enemy : ScriptableObject
{
    // Start is called before the first frame update
    public string name;
    public int HP;
    public int MaxHP;
    public int Attack;
    public int Defense;
    public int dropped_gold;
    public int exp;
    public Sprite sprite; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

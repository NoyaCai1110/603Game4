using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharPanel : MonoBehaviour
{
    public TextMeshProUGUI health_num;
    public TextMeshProUGUI mana_num;
    public TextMeshProUGUI lvl_num;
    public TextMeshProUGUI name;

    //initial setup for character state 
    public void Setup(PartyMember character)
    {
        health_num.text = $"HP {character.HP}";
        mana_num.text = $"MP {character.MP}";
        lvl_num.text = $"Lv. {character.level}";
        name.text = $"{character.name}";
    }

    //updates text to reflect character's state
    public void UpdateValues(PartyMember character)
    {
        Setup(character);
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

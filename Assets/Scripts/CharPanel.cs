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

        health_num.color = Color.white;

        if (character.HP == 0)
        {
            health_num.color = Color.red;
        }

        if (character.HP == character.MaxHP)
        {
            health_num.color = Color.green;
        }



        if (character.MP == character.MaxMP)
        {
            mana_num.color = Color.green;
        }
        else
        {
            mana_num.color = Color.white;
        }
    }

    //updates text to reflect character's state
    public void UpdateValues(PartyMember character)
    {
        Setup(character);
    }

}

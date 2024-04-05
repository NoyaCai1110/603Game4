using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Onboarding : MonoBehaviour
{
    public TMP_Text textField;
    //public MapNode currentNode;

    void Start()
    {
        textField.text = "Use WASD Keys to Navigate on the map";
    }

    // Method to update current node type in the Onboarding script
    public void UpdateCurrentNodeType(Map_type type)
    {
        if(type.ToString() == "Fight")
        {
            textField.text = "Use Space Key to advance in the Fight";
        }
        else if(type.ToString() == "Shop")
        {
            textField.text = "You can visit the store to buy weapons";
        }
        else if(type.ToString() == "Chest")
        {
            textField.text = "Open the Chest to get a suprice reward";
        }
    }
}

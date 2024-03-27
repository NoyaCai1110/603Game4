using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Logger : MonoBehaviour
{
    public TextMeshProUGUI loggerText; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDialogue(string text)
    {
        loggerText.text = text; 
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Onboarding : MonoBehaviour
{
    public TMP_Text textField;
    private string currentNode;
    public GameObject exitPopUp;
    public GameObject onboardingPanel;
    private bool isOnBoarding = false;

    void Start()
    {
        textField.text = "Use WASD Keys to Navigate on the map";
        isOnBoarding = true;
    }

    void Update()
    {
        if(currentNode == "Exit")
        {
            exitPopUp.SetActive(true);
            onboardingPanel.SetActive(false);
        }
    }

    // Method to update current node type in the Onboarding script
    public void UpdateCurrentNodeType(Map_type type)
    {
        currentNode = type.ToString();
        if (isOnBoarding == true)
        {
            if (currentNode == "Fight")
            {
                textField.text = "Use Space Key to advance in the Fight";
            }
            else if (currentNode == "Shop")
            {
                textField.text = "You can visit the store to buy weapons";
            }
            else if (currentNode == "Chest")
            {
                textField.text = "Open the Chest to get a suprice reward";
            }
            else
            {
                textField.text = "Use WASD Keys to Navigate on the map";
            }
        }
        else
        {
            return;
        }

    }

    public void closePopup()
    {
        SceneManager.LoadScene(0);
        isOnBoarding = false;
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(3);
        isOnBoarding = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnboardingUI : MonoBehaviour
{
    public Player playerScript;
    public TextMeshProUGUI onboardingText;

    private int onboardingState = 1;

    // Start is called before the first frame update
    void Start()
    {  
        //Show onboarding panel and prevent player from moving
        this.gameObject.SetActive(true);
        playerScript.isFreeze = true;
    }

    //Change onboarding state when button is clicked
    public void NextOnboardingState()
    {
        onboardingState++;

        switch(onboardingState)
        {
            //Explain enemies
            case 2:
                onboardingText.text = "The orange dots represent enemies within the dungeon. Approaching one will commence a battle.";
                break;

            //Explain battle mechanics
            case 3:
                onboardingText.text = "When in a battle, Press 'A' to progress the text window and click commands to issue them to your party members.";
                break;

            //Explain shop
            case 4:
                onboardingText.text = "The shop will have equipment and items for purchase with the coins obtained from defeating enemies.";
                break;

            //Final message
            case 5:
                onboardingText.text = "Good luck on your travels through this perilous dungeon!";
                break;

            //Close onboarding and allow player to move
            case 6:
                this.gameObject.SetActive(false);
                playerScript.isFreeze = false;
                break;
        }
    }
}

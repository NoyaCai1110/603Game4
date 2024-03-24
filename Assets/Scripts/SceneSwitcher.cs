using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    //references to the ints of the scenes (found in build settings)
    public int menuScene = 0;
    public int gameScene = 1;
    public int optionsScene = 2;
    public int controlsScene = 3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onSwitchMenu()
    {
        SceneManager.LoadScene(menuScene, LoadSceneMode.Single);// switches to the menu scene
    }

    public void onSwitchGame()
    {
        SceneManager.LoadScene(gameScene, LoadSceneMode.Single);// switches to the game scene
    }

    public void onSwitchOptions()
    {
        SceneManager.LoadScene(optionsScene, LoadSceneMode.Single);// switches to the options scene
    }

    public void onSwitchControls()
    {
        SceneManager.LoadScene(controlsScene, LoadSceneMode.Single);// switches to the controls scene
    }

    public void onQuitGame()
    {
        Application.Quit();
    }
}

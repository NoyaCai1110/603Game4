using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyManager : MonoBehaviour
{
    //references
    public GameObject player;
    public GameObject enemy;//right now this is going to reference the prefab, but depending on how we spawn them this will change
    public GameObject maze;
    public GameObject popup;


    private bool ispaused = false;
    private MazeGame mazeScript;//reference to the script that moves the player
    private Player playerScript;//reference to the script that holds the player's stats


    // Start is called before the first frame update
    void Start()
    {
        popup.SetActive(ispaused);//hides popup

        mazeScript = maze.GetComponent<MazeGame>();//gets script from maze GameObject

        //playerScript=player.GetComponent<Player>();//gets the script
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ispaused)
            {
                OnUnpause();
            }
            else
            {
                OnPause();
            }
        }

    }

    public void OnPause()
    {
        ispaused = true;
        popup.SetActive(ispaused);

        mazeScript.isFreeze = true;
        //freeze the enemies too
    }

    public void OnUnpause()
    {
        ispaused = false;
        popup.SetActive(ispaused);

        mazeScript.isFreeze = false;
        //freeze the enemies too
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGame : MonoBehaviour
{
    protected Maze m;
    protected Vector2Int playerPos;

    public bool isFreeze = false; //from Player script

    private MazeGameState gameState;
    // Start is called before the first frame update
    public virtual void Start()
    {
        m = new Maze();
        playerPos = m.startPos;
        gameState = MazeGameState.AWAITING_INPUT;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(gameState == MazeGameState.AWAITING_INPUT && !isFreeze)//partly from player script
        {
            HandleInput();
        }
    }


    private void Move(Vector2Int delta)
    {
        if (m.GetPosition(playerPos.x, playerPos.y).IsValidMove(delta))
        {
            playerPos += delta;
        }
    }


    private void HandleInput()
    {
        if(Input.GetKey(KeyCode.W))
        {
            Move(Vector2Int.up);
        }
        if(Input.GetKey(KeyCode.S))
        {
            Move(Vector2Int.down);
        }
        if(Input.GetKey(KeyCode.D))
        {
            Move(Vector2Int.right);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Move(Vector2Int.left);
        }

    }
}


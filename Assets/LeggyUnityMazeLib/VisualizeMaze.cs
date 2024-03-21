using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeMaze : MazeGame
{
    public GameObject mazePosPrefab;
    public Transform player;
    private int endPointX;
    private int endPointY;


    private void Visualize()
    {
        for (int x = 0; x < Maze.MAZE_WIDTH; x++)
        {
            for (int y = 0; y < Maze.MAZE_HEIGHT; y++)
            {
                MazePosition pos = m.GetPosition(x, y);
                endPointX = m.endPos.x;
                endPointY = m.endPos.y;
                if (pos == null)
                {
                    continue;
                }
                var newPosObj = Instantiate(mazePosPrefab, transform);
                newPosObj.transform.localPosition = new Vector2(x, y);
                var newObjScript = newPosObj.GetComponent<VisualizePosition>();
                if (m.startPos.x == x && m.startPos.y == y || m.endPos.x == x && m.endPos.y == y)
                {
                    pos = new MazePosition(pos);
                    if (x == 0)
                    {
                        pos.canMoveWest = true;
                    }
                    if (x == Maze.MAZE_WIDTH - 1)
                    {
                        pos.canMoveEast = true;
                    }
                    if (y == 0)
                    {
                        pos.canMoveSouth = true;
                    }
                    if (y == Maze.MAZE_HEIGHT - 1)
                    {
                        pos.canMoveNorth = true;
                    }
                }
                newObjScript.Visualize(pos);
            }
        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Visualize();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        player.localPosition = new Vector3(playerPos.x, playerPos.y, 0);
        if(playerPos.x == endPointX && playerPos.y == endPointY)
        {
            Debug.Log("You Win");
        }
        
    }
}

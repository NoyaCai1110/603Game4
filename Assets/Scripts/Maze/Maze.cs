
using System.Collections.Generic;
using UnityEngine;

public class Maze
{
    private MazePosition[,] maze;
    public const int MAZE_WIDTH = 20, MAZE_HEIGHT = 20;
    public Vector2Int startPos, endPos;

    public MazePosition GetPosition(int x, int y)
    {
        return maze[x, y];
    }

    private void MakeMazeNull()
    {
        maze = new MazePosition[MAZE_WIDTH, MAZE_HEIGHT];
        for(int x = 0; x < MAZE_WIDTH; x++)
        {
            for(int y = 0; y < MAZE_HEIGHT; y++)
            {
                maze[x, y] = null;
            }
        }
    }

    private void GenerateStartPos()
    {
        int swap = Random.Range(0, 2);
        if (swap == 0)
        {
            startPos = new Vector2Int(Random.Range(0, MAZE_WIDTH), Random.Range(0, 2)*(MAZE_HEIGHT-1));
        }
        else
        {
            startPos = new Vector2Int(Random.Range(0, 2) * (MAZE_WIDTH-1), Random.Range(0, MAZE_HEIGHT));
        }
    }


    private int FlipOrRandomize(int value, int exclusiveMax)
    {
        int inclusiveMax = exclusiveMax-1;
        if(value == 0) return inclusiveMax;
        if (value == inclusiveMax) return 0;
        return Random.Range(0, exclusiveMax);
    }

    private void GenerateEndPos()
    {
        endPos = Vector2Int.zero;
        endPos.x = FlipOrRandomize(startPos.x, MAZE_WIDTH);
        endPos.y = FlipOrRandomize(startPos.y, MAZE_HEIGHT);
        Debug.Log("End Position x ="+ endPos.x);
        Debug.Log("End Position y ="+ endPos.y);

    }

    private static bool IsInMaze(Vector2Int pos)
    {
        bool goodX = pos.x >= 0 && pos.x < MAZE_WIDTH;
        bool goodY = pos.y >= 0 && pos.y < MAZE_HEIGHT;
        return goodX && goodY;
    }
    static readonly Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };

    private Vector2Int[] GetPossibleMoves(Vector2Int currentPosition)
    {
        List<Vector2Int> output = new List<Vector2Int>();
        foreach (var dir in directions)
        {
            Vector2Int nextPos = currentPosition + dir; ;
            if (IsInMaze(nextPos) && maze[nextPos.x, nextPos.y] == null)
            {
                output.Add(dir);
            }
        }
        return output.ToArray();
    }


    private void RemoveWall(Vector2Int selectedMove, ref MazePosition mazePosition)
    {
        if (selectedMove.Equals(Vector2Int.up))
        {
            // remove north wall
            mazePosition.canMoveNorth = true;
        }
        else if (selectedMove.Equals(Vector2Int.down))
        {
            // remove south wall
            mazePosition.canMoveSouth = true;
        }
        else if (selectedMove.Equals(Vector2Int.left))
        {
            // remove west wall
            mazePosition.canMoveWest = true;
        }
        else if (selectedMove.Equals(Vector2Int.right))
        {
            // remove east wall
            mazePosition.canMoveEast = true;
        }
        else
        {
            // throw error
            Debug.LogError("Bad direction");
        }
    }

    private void GenerateMaze()
    {
        // how do we do this? -  "recursive backtracker" algorithm
        MakeMazeNull();
        GenerateStartPos();
        GenerateEndPos();

        Stack<Vector2Int> visitedPositions = new Stack<Vector2Int>();
        Vector2Int currentPos = new Vector2Int(startPos.x, startPos.y);
        maze[currentPos.x, currentPos.y] = new MazePosition(false);
        while (true)
        {
            Vector2Int[] possibleMoves = GetPossibleMoves(currentPos);
            if (possibleMoves.Length == 0)
            {
                // if we're back to the start square and have been in every direction, stop
                if (visitedPositions.Count == 0) break;

                // backtrack
                currentPos = visitedPositions.Pop();
                continue;
            }
            visitedPositions.Push(currentPos);
            var selectedMove = possibleMoves[Random.Range(0, possibleMoves.Length)];
            RemoveWall(selectedMove, ref maze[currentPos.x, currentPos.y]);
            currentPos += selectedMove;
            maze[currentPos.x, currentPos.y] = new MazePosition(false);
            RemoveWall(selectedMove * -1, ref maze[currentPos.x, currentPos.y]);
        }
    }

    public Maze()
    {
        GenerateMaze();
    }
}

//*****************************************************************//

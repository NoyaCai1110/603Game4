using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizePosition : MonoBehaviour
{
    public GameObject top, bottom, left, right;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Visualize(MazePosition pos)
    {
        top.SetActive(!pos.canMoveNorth);
        bottom.SetActive(!pos.canMoveSouth);
        left.SetActive(!pos.canMoveWest);
        right.SetActive(!pos.canMoveEast);
    }

}

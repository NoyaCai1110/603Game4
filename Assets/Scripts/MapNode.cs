using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Map_type
{
    Shop,
    Fight,
    Start,
    Exit,
    Chest,
    Empty  
}

public class MapNode : MonoBehaviour
{
    // Start is called before the first frame update
    public Map_type type;
    public MapNode left;
    public MapNode right;
    public MapNode up;
    public MapNode down;
    void Start()
    {
        //type = Map_type.Fight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

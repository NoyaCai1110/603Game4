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
    public LineRenderer lr;
    void Start()
    {
        lr = gameObject.AddComponent<LineRenderer>();
        lr.startWidth = 0.1f;        lr.endWidth = 0.1f;
        lr.startColor = Color.white; lr.endColor = Color.white;
        lr.positionCount = 3;
        lr.material = Resources.Load<Material>("Path");
        int i = 0;
        if (right != null)
        {
            lr.SetPosition(0, transform.position + new Vector3(0.5f, 0, -0.0f));
            lr.SetPosition(1, right.transform.position + new Vector3(-0.5f, 0, -0.0f));
            lr.SetPosition(2, transform.position + new Vector3(0.5f, 0, -0.0f));
            i = 3;
        }
        if(up != null)
        {
            lr.positionCount = i + 3;
            lr.SetPosition(i, transform.position + new Vector3(0, 0.5f, -0.0f));
            lr.SetPosition(i+1, up.transform.position + new Vector3(0, -0.5f, -0.0f));
            lr.SetPosition(i+2, transform.position + new Vector3(0, 0.5f, -0.0f));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

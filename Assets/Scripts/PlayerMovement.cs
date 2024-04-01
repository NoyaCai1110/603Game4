using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MapNode currentNode;

    public void MoveToNode(MapNode targetNode)
    {
        if (targetNode != null && IsAdjacentNode(targetNode))
        {
            transform.position = targetNode.transform.position;
            currentNode = targetNode;
            Debug.Log("Moved to node of type: " + targetNode.type);
        }
        else
        {
            Debug.Log("Cannot move to the selected node.");
        }
    }
    private bool IsAdjacentNode(MapNode node)
    {
        return node == currentNode.left || node == currentNode.right || 
               node == currentNode.up || node == currentNode.down;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) && currentNode.up != null)
        {
            MoveToNode(currentNode.up);
        }
        else if (Input.GetKey(KeyCode.S) && currentNode.down != null)
        {
            MoveToNode(currentNode.down);
        }
        else if (Input.GetKey(KeyCode.A) && currentNode.left != null)
        {
            MoveToNode(currentNode.left);
        }
        else if (Input.GetKey(KeyCode.D) && currentNode.right != null)
        {
            MoveToNode(currentNode.right);
        }
        
    }
}

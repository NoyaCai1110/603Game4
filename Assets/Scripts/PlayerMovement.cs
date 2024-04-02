using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MapNode currentNode;
    public GameObject inventoryPanel;

    public void MoveToNode(MapNode targetNode)
    {
        if (targetNode != null && IsAdjacentNode(targetNode))
        {
            transform.position = targetNode.transform.position + new Vector3(0.0f, 0.5f, 0.0f);
            currentNode = targetNode;
            Debug.Log("Moved to node of type: " + targetNode.type);
        }
        else
        {
            Debug.Log("Cannot move to the selected node.");
        }

        switch (currentNode.type)
        {
            case Map_type.Fight:
                {
                    FightEncounter encounter = currentNode.gameObject.GetComponent<FightEncounter>();
                    List<Enemy> e_list = encounter.enemies;
                    this.gameObject.GetComponent<Player>().BeginBattle(e_list);
                    break;
                }
            case Map_type.Shop:
                {
                    break;
                }
            case Map_type.Start:
                {
                    break;
                }
            case Map_type.Exit:
                {
                    break;
                }
            case Map_type.Chest:
                {
                    break;
                }
            //nothing happens
            case Map_type.Empty:
                {

                    break;
                }
        }

    }
    private bool IsAdjacentNode(MapNode node)
    {
        return node == currentNode.left || node == currentNode.right || 
               node == currentNode.up || node == currentNode.down;
    }
    void Start()
    {
        transform.position = currentNode.transform.position + new Vector3(0.0f, 0.5f, 0.0f);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && currentNode.up != null && !inventoryPanel.activeInHierarchy)
        {
            MoveToNode(currentNode.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) && currentNode.down != null && !inventoryPanel.activeInHierarchy)
        {
            MoveToNode(currentNode.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) && currentNode.left != null && !inventoryPanel.activeInHierarchy)
        {
            MoveToNode(currentNode.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) && currentNode.right != null && !inventoryPanel.activeInHierarchy)
        {
            MoveToNode(currentNode.right);
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public LevelSelectNode startNode;

    private static LevelSelectNode lastNode;
    private Direction lastDirection;
    private LevelSelectNode currentNode;

    private enum Direction { NONE, NORTH, SOUTH, EAST, WEST }

    private void Activate()
    {
        currentNode.Activate();
    }

    private void Awake()
    {
        Move(lastNode ? lastNode : startNode);
    }

    private void Move(LevelSelectNode node)
    {
        currentNode = lastNode = node;
        transform.position = node.transform.position;
    }

    private void Update()
    {
        LevelSelectNode moveNode = null;

        if (Input.GetButtonDown("Submit"))
        {
            Activate();
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            if (lastDirection != Direction.EAST)
            {
                moveNode = currentNode.east;
                lastDirection = Direction.EAST;
            }
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            if (lastDirection != Direction.WEST)
            {
                moveNode = currentNode.west;
                lastDirection = Direction.WEST;
            }
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            if (lastDirection != Direction.NORTH)
            {
                moveNode = currentNode.north;
                lastDirection = Direction.NORTH;
            }
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            if (lastDirection != Direction.SOUTH)
            {
                moveNode = currentNode.south;
                lastDirection = Direction.SOUTH;
            }
        }
        else
        {
            lastDirection = Direction.NONE;
        }

        if (moveNode)
            Move(moveNode);
    }
}

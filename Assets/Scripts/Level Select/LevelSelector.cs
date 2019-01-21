using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public LevelSelectNode startNode;

    private static LevelSelectNode lastNode;

    //private bool canMove = true;
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

    /* Old Update Function */
    /*
    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"),
                                    Input.GetAxis("Vertical"));

        if (Input.GetButtonDown("Submit"))
        {
            Activate();
        }
        else if (canMove)
        {
            if (!input.Equals(Vector2.zero))
            {
                canMove = false;

                if (Mathf.Abs(input.y) > Mathf.Abs(input.x))
                {
                    if (input.y > 0)
                    {
                        if (currentNode.north)
                            Move(currentNode.north);
                    }
                    else
                    {
                        if (currentNode.south)
                            Move(currentNode.south);
                    }
                }
                else
                {
                    if (input.x > 0)
                    {
                        if (currentNode.east)
                            Move(currentNode.east);
                    }
                    else
                    {
                        if (currentNode.west)
                            Move(currentNode.west);
                    }
                }
            }
        }
        else
        {
            canMove = input.Equals(Vector2.zero);
        }
    }
    */

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

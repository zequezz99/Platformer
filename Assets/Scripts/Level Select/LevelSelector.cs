using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public LevelSelectNode startNode;

    private static LevelSelectNode lastNode;

    private bool canMove = true;
    private LevelSelectNode currentNode;

    private void Activate()
    {
        currentNode.Activate();
    }

    private void Awake()
    {
        Move(lastNode ? lastNode : startNode);
    }

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

    private void Move(LevelSelectNode node)
    {
        currentNode = lastNode = node;
        transform.position = node.transform.position;
    }
}

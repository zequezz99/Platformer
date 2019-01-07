using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public LevelSelectNode startNode;

    private static LevelSelectNode lastNode;

    private const float moveTime = 0.25f;

    private float lastMoveTime;
    private LevelSelectNode currentNode;

    private void Activate()
    {
        currentNode.Activate();
    }

    private void Awake()
    {
        lastMoveTime = Time.fixedTime - moveTime;

        Move(lastNode ? lastNode : startNode);
    }

    private void Update()
    {

        if (Input.GetButtonDown("Submit"))
        {
            Activate();
        }

        else
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal > 0)
            {
                if (currentNode.east)
                    Move(currentNode.east);
            }
            else if (horizontal < 0)
            {
                if (currentNode.west)
                    Move(currentNode.west);
            }
            if (vertical > 0)
            {
                if (currentNode.north)
                    Move(currentNode.north);
            }
            else if (vertical < 0)
            {
                if (currentNode.south)
                    Move(currentNode.south);
            }
        }
    }

    private void Move(LevelSelectNode node)
    {
        if (Time.fixedTime - lastMoveTime >= moveTime)
        {
            currentNode = lastNode = node;
            transform.position = node.transform.position;

            lastMoveTime = Time.fixedTime;
        }
    }
}

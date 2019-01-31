using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : NPCPlatformerController
{
    public float minFollowDistance = 1f;
    public float maxFollowDistance = 2f;

    private float targetX;

    private const float xThreshold = 0.1f;

    protected override void FindTargetDirection()
    {
        float x = transform.position.x;
        float playerX = FindObjectOfType<PlayerPlatformerController>().transform.position.x;

        if ((Mathf.Abs(targetX - playerX) > maxFollowDistance) && (Mathf.Abs(targetX - x) <= xThreshold))
        {
            if (playerX > x)
                targetX = playerX - Random.Range(minFollowDistance, maxFollowDistance);
            else
                targetX = playerX + Random.Range(minFollowDistance, maxFollowDistance);
        }

        if (Mathf.Abs(targetX - x) > xThreshold)
        {
            targetDirection = new Vector2(targetX - x, 0);
        }
        else
        {
            targetDirection = Vector2.zero;
        }
    }

    private void Awake()
    {
        targetX = transform.position.x;
    }
}

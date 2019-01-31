using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPlatformerController : PhysicsObject
{
    public float maxSpeed = 5f;

    protected Vector2 targetDirection;

    protected override void ComputeVelocity()
    {
        FindTargetDirection();
        targetVelocity = targetDirection.normalized * maxSpeed;
    }

    protected virtual void FindTargetDirection() { }
}

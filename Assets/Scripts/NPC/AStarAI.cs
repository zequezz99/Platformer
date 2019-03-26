using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

public abstract class AStarAI : PhysicsObject
{
    private struct Node
    {
        public Vector2 pos;
        public float heuristic;

        public Node(Vector2 pos, float heuristic)
        {
            this.pos = pos;
            this.heuristic = heuristic;
        }
    }

    public float maxSpeed = 7f;
    public float jumpTakeOffSpeed = 10f;
    public float refreshRate = 1f;

    protected abstract bool CanFly { get; }
    protected abstract bool CanJump { get; }

    protected Vector2 targetPos;

    private Grid grid;
    private float lastRefresh;

    protected abstract void ComputeTargetPos();

    private void FindPath()
    {
        lastRefresh = Time.fixedTime;
        ComputeTargetPos();

        SimplePriorityQueue<Node> openNodes = new SimplePriorityQueue<Node>();
        SimplePriorityQueue<Node> closedNodes = new SimplePriorityQueue<Node>();

        Vector2 targetGridPos = (Vector2Int)grid.WorldToCell(targetPos);
        Vector2 startGridPos = (Vector2Int)grid.WorldToCell(transform.position);

        Node startNode = new Node(startGridPos, Vector2.Distance(startGridPos, targetGridPos));
        openNodes.Enqueue(startNode, startNode.heuristic);

        while(openNodes.Count > 0)
        {
            Node current = openNodes.Dequeue();

            if (current.pos.Equals(targetGridPos))
            {
                break;
            }

            
        }
    }

    private void Start()
    {
        grid = FindObjectOfType<Grid>();
        
        FindPath();
    }

    protected override void Update()
    {
        base.Update();

        if (Time.fixedTime - lastRefresh >= refreshRate)
        {
            FindPath();
        }
    }
}

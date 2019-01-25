using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PhysicsObject : MonoBehaviour
{

    public float minGroundNormalY = 1f;
    public float gravityModifier = 1.5f;
    public float knockbackScalar = 1f;

    protected Vector2 targetVelocity;
    protected List<Vector2> addedVelocities = new List<Vector2>();
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;

    private RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    private const float minMoveDistance = 0.001f;
    private const float shellRadius = 0.01f;
    private const float addedVelocityDecay = 4f;
    private const float groundedKnockbackThreshold = 3f;

    public void AddVelocity(Vector2 velocity)
    {
        this.velocity.y += velocity.y;
        addedVelocities.Add(new Vector2(velocity.x, 0));
    }

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;

        groundNormal = Vector2.up;
    }

    protected virtual void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
        ComputeAddedVelocity();
    }

    protected virtual void ComputeVelocity()
    {

    }

    private void ComputeAddedVelocity()
    {
        List<Vector2> newAddedVelocities = new List<Vector2>();
        for (int i = 0; i < addedVelocities.Count; i++)
        {
            targetVelocity += addedVelocities[i];

            bool positive = addedVelocities[i].x > 0;
            addedVelocities[i] = new Vector2(addedVelocities[i].x - addedVelocityDecay * Time.deltaTime, 0);
            if ((positive ? addedVelocities[i].x > 0 : addedVelocities[i].x < 0)
                && (!grounded || addedVelocities[i].magnitude >= groundedKnockbackThreshold))
            {
                newAddedVelocities.Add(addedVelocities[i]);
            }
        }
        addedVelocities = newAddedVelocities;
    }

    void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            int count = GetComponent<Collider2D>().Cast(move,
                                                        contactFilter,
                                                        hitBuffer,
                                                        distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }


        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }

}
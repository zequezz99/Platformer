using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PhysicsObject : MonoBehaviour
{

    public float minGroundNormalY = 1f;
    public float gravityModifier = 1.5f;
    //public float knockbackScalar = 1f;

    protected Vector2 targetVelocity;
    protected Vector2 addedVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;

    private RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    private const float minMoveDistance = 0.001f;
    private const float shellRadius = 0.01f;
    private const float hitVelocity = 8f;
    private const float minAddedVelocity = 2f;
    private const float addedVelocityDecay = 5f;

    /*
    public void AddVelocity(Vector2 velocity)
    {
        this.velocity.y += velocity.y;
        addedVelocities.Add(new Vector2(velocity.x, 0));
    }
    */

    public void KnockBack(Vector2 direction)
    {
        direction = new Vector2(direction.x, 0).normalized;

        velocity.y = hitVelocity;

        addedVelocity.x = direction.x * hitVelocity;
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
        velocity.x = 0;
        ComputeVelocity();
        ComputeAddedVelocity();
    }

    protected virtual void ComputeVelocity()
    {

    }

    private void ComputeAddedVelocity()
    {
        if (Mathf.Abs(addedVelocity.x) < minAddedVelocity)
            addedVelocity.x = 0;

        if (Mathf.Abs(addedVelocity.y) < minAddedVelocity)
            addedVelocity.y = 0;

        float decay = addedVelocityDecay * Time.deltaTime;

        if (Mathf.Abs(addedVelocity.x) > 0)
        {
            if (addedVelocity.x > 0)
                decay = Mathf.Min(-decay, decay);
            else
                decay = Mathf.Max(-decay, decay);
            addedVelocity.x += decay;
        }

        if (Mathf.Abs(addedVelocity.y) > 0)
        {
            if (addedVelocity.y > 0)
                decay = Mathf.Min(-decay, decay);
            else
                decay = Mathf.Max(-decay, decay);
            addedVelocity.y += decay;
        }

        velocity += addedVelocity;
    }


    void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x += targetVelocity.x;

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
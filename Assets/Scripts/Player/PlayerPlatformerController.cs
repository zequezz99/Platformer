using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 10;
    public float attackTime = 0.75f;

    private bool attackBoxActive;
    private float lastAttackTime;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private const float attackBoxActiveTime = 0.5f;

    public void Die()
    {

    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        if (transform.localScale.x > 0 ? (move.x < 0.0f) : (move.x > 0.0f))
        {
            transform.localScale = new Vector3(transform.localScale.x * -1,
                                               transform.localScale.y,
                                               transform.localScale.z);
        }
        /*
        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
        */
        targetVelocity = move * maxSpeed;
    }

    private void Attack()
    {
        GetComponentInChildren(typeof(AttackBox), true).gameObject.SetActive(true);
        attackBoxActive = true;
    }

    private void Awake()
    {
        lastAttackTime = Time.fixedTime - attackTime;

        //spriteRenderer = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (attackBoxActive && Time.fixedTime - lastAttackTime > attackBoxActiveTime)
        {
            GetComponentInChildren(typeof(AttackBox), true).gameObject.SetActive(false);
            attackBoxActive = false;
        }

        if (Input.GetButtonDown("Fire1") && Time.fixedTime - lastAttackTime >= attackTime)
        {
            lastAttackTime = Time.fixedTime;
            Attack();
        }
    }
}
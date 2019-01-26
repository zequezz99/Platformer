using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class MeleeEnemy : MonoBehaviour
{
    public int damage;

    public void OnCollisionEnter2D(Collision2D collision) { }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health)
            health.Damage(damage, transform.position, gameObject);
    }
}

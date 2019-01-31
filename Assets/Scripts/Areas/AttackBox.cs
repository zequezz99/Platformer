using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class AttackBox : MonoBehaviour {

    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health targetHealth = collision.gameObject.GetComponent<Health>();
        if (targetHealth)
        {
            targetHealth.Damage(damage,
                                collision.GetContact(0).point,
                                transform.parent.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class AttackBox : MonoBehaviour {

    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health targetHealth = other.GetComponent<Health>();
        if (targetHealth)
        {
            targetHealth.Damage(damage,
                                transform.parent.position,
                                transform.parent.gameObject);
        }
    }
}

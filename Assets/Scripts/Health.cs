using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;

    private int health;

    public virtual void Damage(int damage, Vector2 hitPos, GameObject damageSource = null)
    {
        health -= Mathf.Min(damage, health);

        if (health == 0)
        {
            Die();
            return;
        }

        PhysicsObject physObj = GetComponent<PhysicsObject>();
        if (physObj)
        {
            Vector2 knockback = (hitPos - (Vector2)transform.position).normalized;
            knockback *= damage * physObj.knockbackScalar;
            physObj.AddVelocity(knockback);
        }
    }

    public void Heal(int heal)
    {
        health += Math.Min(heal, maxHealth - health);
    }

    public int GetHealth()
    {
        return health;
    }

    private void Awake()
    {
        health = maxHealth;
    }

    private void Die()
    {

    }
}

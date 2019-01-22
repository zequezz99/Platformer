using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;

    private int health;

    public void Damage(int damage)
    {
        health -= Mathf.Min(damage, health);

        if (health == 0)
            Die();
    }

    public void Heal(int heal)
    {
        health += Math.Min(heal, maxHealth - health);
    }

    public int GetHealth()
    {
        return health;
    }

    protected virtual void Die() { }

    private void Awake()
    {
        health = maxHealth;
    }
}

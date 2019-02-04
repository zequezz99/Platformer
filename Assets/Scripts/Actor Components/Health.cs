using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 1;

    private int health;

    public virtual void Damage(int damage, GameObject damageSource = null)
    {
        if (damageSource && damageSource.Equals(gameObject))
            return;

        health -= Mathf.Min(damage, health);

        if (health == 0)
        {
            Die();
            return;
        }

        PhysicsObject physObj = GetComponent<PhysicsObject>();
        if (physObj && damageSource)
        { 
            physObj.KnockBack(transform.position - damageSource.transform.position);
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
        PlayerPlatformerController player = GetComponent<PlayerPlatformerController>();
        if (player)
        {
            player.Die();
        }
        else
        {
            DropTable dropTable = GetComponent<DropTable>();
            if (dropTable)
                dropTable.Drop();

            Destroy(gameObject);
        }
    }
}

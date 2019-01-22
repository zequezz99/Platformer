using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AttackBox : MonoBehaviour {

    public int damage = 1;

    private void OnTriggerEnter(Collider target)
    {
        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth)
        {
            targetHealth.Damage(damage);
        }
    }
}

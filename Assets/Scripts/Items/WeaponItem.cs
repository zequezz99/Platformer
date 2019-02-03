using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Abilities))]
public class WeaponItem : InventoryItem {

    public int damage = 1;
    public float attackRate = 0.75f;
    public float attackDuration = 0.25f;
}

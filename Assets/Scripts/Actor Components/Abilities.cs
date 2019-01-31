using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Desc: Holds an int value for each Ability and calculates Ability bonuses,
 *       accounting for equipment and weapon.
 * 
 * Add to: Any Actor with Ability scores or any equipable item to denote Ability
 *         bonuses when equipped.
 */
public class Abilities : MonoBehaviour
{
    public enum Ability
    {
        Strength = 0,
        Charm = 1,
        Viatlity = 2,
        Luck = 3
    };

    public int[] scores = new int[Enum.GetNames(typeof(Ability)).Length];

    public int GetBonus(Ability ability)
    {
        return Mathf.FloorToInt((float)(GetScore(ability) - 10) / 2);
    }

    public int GetScore(Ability ability)
    {
        int eBonus = 0;
        Equipment e = GetComponent<Equipment>();
        if (e)
        {
            for (int i = 0; i < e.slots.Length; i++)
            {
                EquipmentItem item = e.GetItem(i);
                if (item)
                {
                    eBonus += item.GetComponent<Abilities>().GetScore(ability);
                }
            }
        }

        if (enabled.GetType() == typeof(WeaponEquipment))
        {
            WeaponItem weapon = ((WeaponEquipment)e).GetWeapon();
            if (weapon)
                eBonus += weapon.GetComponent<Abilities>().GetScore(ability);
        }

        return eBonus + scores[(int)ability];
    }
}

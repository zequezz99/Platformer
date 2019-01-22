using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public enum Ability
    {
        Strength = 0,
        Dexterity = 1,
        Luck = 2
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

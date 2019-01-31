using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquipment : Equipment {

    private WeaponItem weapon;

    public WeaponItem GetWeapon()
    {
        return weapon;
    }

    public void EquipWeapon(WeaponItem weapon)
    {
        UnequipWeapon();
        this.weapon = weapon;
    }

    public bool HasRoomFor(WeaponItem weapon)
    {
        return !weapon;
    }

    public bool UnequipWeapon()
    {
        if (weapon)
        {
            Inventory inv = GetComponent<Inventory>();
            if (!inv || inv.Add(weapon) == 0)
                InventoryPickup.DropItem(weapon, transform.position);

            return true;
        }
        return false;
    }
}

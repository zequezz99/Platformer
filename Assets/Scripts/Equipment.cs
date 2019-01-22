using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public enum Slot { Necklace, Ring, Feet }

    public Slot[] slots;

    private EquipmentItem[] equipment;

    public bool Equip(EquipmentItem item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (item.slot == slots[i] && !equipment[i])
            {
                return Equip(i, item);
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (item.slot == slots[i])
            {
                return Equip(i, item);
            }
        }

        return false;
    }

    public bool Equip(int index, EquipmentItem item)
    {
        if (slots[index] != item.slot)
            return false;

        Unequip(index);
        equipment[index] = item;

        return true;
    }

    public EquipmentItem GetItem(int index)
    {
        return equipment[index];
    }

    public bool HasRoomFor(EquipmentItem item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == item.slot && !equipment[i])
                return true;
        }
        return false;
    }

    public bool Unequip(int index)
    {
        if (equipment[index])
        {
            Inventory inv = GetComponent<Inventory>();

            if (!inv || inv.Add(equipment[index]) == 0)
                InventoryPickup.DropItem(equipment[index], transform.position);

            return true;
        }
        return false;
    }

    private void Awake()
    {
        equipment = new EquipmentItem[slots.Length];
    }
}

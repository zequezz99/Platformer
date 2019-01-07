using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public enum Slot { Head, Torso, Legs, Hands, Feet }

    private Dictionary<Slot, EquipmentItem> items;

    public void Equip(EquipmentItem item)
    {
        Unequip(item.slot);
        items.Add(item.slot, item);
    }

    public EquipmentItem GetItem(Slot slot)
    {
        return items[slot];
    }

    public void Unequip(Slot slot)
    {
        if (items.TryGetValue(slot, out EquipmentItem item))
        {
            Inventory inv = GetComponent<Inventory>();
            if (inv)
            {
                item.Count -= inv.Add(item);
            }
            if (item.Count > 0)
            {
                InventoryPickup.DropItem(item, transform.position);
            }
            items.Add(slot, null);
        }
    }

    private void Awake()
    {
        items = new Dictionary<Slot, EquipmentItem>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int capacity = 16;
    public bool autoEquip;

    public int Count
    {
        get { return items.Count; }
    }

    private List<InventoryItem> items = new List<InventoryItem>();

    public int Add(InventoryItem item)
    {
        if (autoEquip)
        {
            if (item.GetComponent<EquipmentItem>())
            {
                Equipment equipment = GetComponent<Equipment>();
                if (equipment && equipment.HasRoomFor((EquipmentItem)item))
                {
                    equipment.Equip((EquipmentItem)item);
                    return item.Count;
                }
            }
            else if (item.GetComponent<WeaponItem>())
            {
                WeaponEquipment equipment = GetComponent<WeaponEquipment>();
                if (equipment && equipment.HasRoomFor((WeaponItem)item))
                {
                    equipment.EquipWeapon((WeaponItem)item);
                    return item.Count;
                }
            }
        }

        int count = item.Count;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Equals(item))
            {
                if (items[i].Count + item.Count > item.maxCount)
                {
                    int diff = items[i].maxCount - items[i].Count;
                    items[i].Count += diff;
                    item.Count -= diff;
                }
                else
                {
                    items[i].Count += item.Count;
                    return count;
                }
            }
        }

        if (items.Count < capacity)
        {
            items.Add(item);
            return count;
        }

        return count - item.Count;
    }

    public int CountItem(InventoryItem item)
    {
        int count = 0;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Equals(item))
                count += items[i].Count;
        }

        return count;
    }

    public int Drop(InventoryItem item)
    {
        item.Count = Remove(item);

        if (item.Count > 0)
            InventoryPickup.DropItem(item, transform.position);

        return item.Count;
    }

    public InventoryItem Get(int index)
    {
        return items[index];
    }

    public int Remove(InventoryItem item)
    {
        int count = item.Count;

        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i].Equals(item))
            {
                if (items[i].Count - item.Count < 0)
                {
                    item.Count -= items[i].Count;
                    items[i].Count = 0;
                }
                else
                {
                    items[i].Count -= item.Count;
                    item.Count = 0;
                }
            }
        }

        CleanOut();
        return count - item.Count;
    }

    private void Awake()
    {
        items = new List<InventoryItem>();
    }

    private void CleanOut()
    {
        for (int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i].Count == 0)
            {
                items.RemoveAt(i);
            }
        }
    }
}

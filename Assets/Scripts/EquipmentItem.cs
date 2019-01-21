using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Abilities))]
public class EquipmentItem : InventoryItem
{
    public Equipment.Slot slot;
}

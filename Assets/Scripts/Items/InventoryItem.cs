using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public string itemName;
    public int maxCount = 1;
    public Sprite sprite;

    public int Count { get; set; }

    public bool Equals(InventoryItem other)
    {
        return itemName.Equals(other.itemName);
    }
}

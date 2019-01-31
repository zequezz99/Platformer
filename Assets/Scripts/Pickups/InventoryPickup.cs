using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPickup : Pickup
{
    public InventoryItem inventoryItem;

    private static float itemRadius = 1f;

    public static void DropItem(InventoryItem item, Vector2 pos)
    {
        GameObject obj = new GameObject();

        obj.AddComponent<CircleCollider2D>();
        obj.GetComponent<CircleCollider2D>().radius = itemRadius;

        obj.AddComponent<InventoryPickup>();
        obj.GetComponent<InventoryPickup>().inventoryItem = item;

        Instantiate(obj, pos, Quaternion.identity);
    }

    protected override void PickUp(Collider2D actor)
    {
        Inventory inv = actor.GetComponent<Inventory>();

        if (inv)
        {
            inventoryItem.Count -= inv.Add(inventoryItem);
            if (inventoryItem.Count == 0)
                Destroy(gameObject);
        }
    }
}

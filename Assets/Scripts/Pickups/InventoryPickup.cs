using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class InventoryPickup : Pickup
{
    public InventoryItem inventoryItem;

    public static InventoryItem item;

    private static float itemRadius = 0.5f;

    public static void DropItem(InventoryItem item, Vector2 pos)
    {
        GameObject pickup = new GameObject(item.name + " Pickup");

        pickup.transform.position = pos;

        pickup.AddComponent<InventoryPickup>().inventoryItem = item;

        pickup.GetComponent<CircleCollider2D>().radius = itemRadius;
        pickup.GetComponent<CircleCollider2D>().isTrigger = true;

        pickup.GetComponent<SpriteRenderer>().sprite = item.sprite;
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

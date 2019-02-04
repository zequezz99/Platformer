using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class InventoryPickup : Pickup
{
    public InventoryItem inventoryItem;

    private static float itemRadius = 0.5f;

    public static void DropItem(InventoryItem item, Vector2 pos)
    {
        InventoryPickup pickup = Instantiate(FindObjectOfType<DefaultPrefabs>().inventoryPickup,
                                             pos, Quaternion.identity);

        pickup.GetComponent<CircleCollider2D>().radius = itemRadius;

        pickup.GetComponent<InventoryPickup>().inventoryItem = item;

        pickup.GetComponent<SpriteRenderer>().sprite = item.sprite;

        pickup.name = item.name + " Pickup";
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

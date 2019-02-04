using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable : MonoBehaviour
{
    [System.Serializable]
    public class DropItem { public float dropRate; public InventoryItem item; }

    public DropItem[] dropTable;

    public void Drop()
    {
        foreach (DropItem dropItem in dropTable)
        {
            float random = Random.Range(0f, 1f);
            if (random <= dropItem.dropRate)
            {
                InventoryPickup.DropItem(dropItem.item, transform.position);
            }
        }
    }
}

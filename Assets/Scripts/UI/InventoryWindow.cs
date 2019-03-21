using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindow : MonoBehaviour
{
    public Inventory inventory;

    public Image itemFrame;
    public InventoryIcon itemIcon;

    private void OnEnable()
    {
        PauseController.Pause();

        for (int i = 0; i < inventory.capacity; i++)
        {
            Image frame = Instantiate(itemFrame, transform);

            if (i < inventory.Count)
            {
                InventoryIcon icon = Instantiate(itemIcon, frame.transform);
                icon.GetComponent<Image>().sprite = inventory.Get(i).sprite;
                icon.inventoryIndex = i;
            }
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        PauseController.Unpause();
    }
}

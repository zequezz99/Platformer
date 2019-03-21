using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class Chest : PlayerInteractable
{
    public GameObject inventoryWindow;

    private Canvas canvas;

    protected override void Interact()
    {
        if (GetComponent<Lock>() && GetComponent<Lock>().IsLocked())
            return;

        GameObject window = Instantiate(inventoryWindow, canvas.transform);
    }

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    private void Start()
    {
        inventoryWindow.GetComponentInChildren<InventoryWindow>()
                .inventory = GetComponent<Inventory>();
    }
}

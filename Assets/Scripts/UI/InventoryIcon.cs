using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryIcon : MonoBehaviour
{
    public GameObject optionsMenu;

    public int inventoryIndex;

    private void OnMouseEnter()
    {
        Instantiate(optionsMenu, transform);
    }

    private void OnMouseExit()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}

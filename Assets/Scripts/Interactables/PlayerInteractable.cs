using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerInteractable : MonoBehaviour
{
    protected virtual void Interact() { }

    protected bool touchingPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPlatformerController player = collision.GetComponent<PlayerPlatformerController>();
        if (player)
            touchingPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerPlatformerController player = collision.GetComponent<PlayerPlatformerController>();
        if (player)
            touchingPlayer = false;
    }

    private void Update()
    {
        if (!PauseController.IsPaused()
            && touchingPlayer 
            && Input.GetButtonDown("Use"))
        {
            Interact();
        }
    }
}

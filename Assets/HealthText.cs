using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HealthText : MonoBehaviour
{
    private void Update()
    {
        Text text = GetComponent<Text>();
        text.text = "Health: " + FindObjectOfType<PlayerPlatformerController>().GetComponent<Health>().GetHealth();
    }
}

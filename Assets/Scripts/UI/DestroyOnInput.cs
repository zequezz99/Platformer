using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnInput : MonoBehaviour
{
    public string[] inputs;

    private void Update()
    {
        foreach (string input in inputs)
        {
            if (Input.GetButtonDown(input))
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}

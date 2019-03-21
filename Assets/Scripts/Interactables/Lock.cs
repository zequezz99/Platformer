using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    private bool locked = true;

    public bool IsLocked()
    {
        return locked;
    }

    protected void SetLock(bool locked)
    {
        this.locked = locked;
    }
}

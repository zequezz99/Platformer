using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PauseController
{
    private static bool paused;
    private static float timeScale;

    public static bool IsPaused()
    {
        return paused;
    }

    public static void Pause()
    {
        paused = true;
        timeScale = Time.timeScale;
        Time.timeScale = 0f;
    }

    public static void Unpause()
    {
        paused = false;
        Time.timeScale = timeScale;
    }
}

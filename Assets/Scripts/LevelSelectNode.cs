using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectNode : MonoBehaviour
{
    public int sceneIndex = -1;

    public LevelSelectNode north;
    public LevelSelectNode south;
    public LevelSelectNode east;
    public LevelSelectNode west;

    public void Activate()
    {
        if (sceneIndex >= 0)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}

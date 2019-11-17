using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLevelManager : GameManagerBase
{
    public LevelLoader levelLoader;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            levelLoader.LoadNextScene();
        }
    }
}

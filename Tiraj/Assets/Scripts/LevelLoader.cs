using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void LoadInvasionLevel()
    {
        SceneManager.LoadScene("Level Invasion");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

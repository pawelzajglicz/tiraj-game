using System;
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
        GameManager.GetInstance().isInvasionLevel = true;
        SceneManager.LoadScene("Level Invasion");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.GetInstance().Reset();
    }

    internal void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void LoadEndLevel()
    {
        GameManager.GetInstance().isInvasionLevel = false;
        SceneManager.LoadScene("End Menu");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public void LoadFirstLevel()
    {
        GameSessionInfo.GetInstance().Reset();
        SceneManager.LoadScene("Level 1");
    }

    public void LoadMainMenu()
    {
        GameSessionInfo.GetInstance().Reset();
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

    public void ReloadLevel()
    {
        GameSessionInfo.GetInstance().Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    internal void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void LoadEndLevel()
    {
        SceneManager.LoadScene("End Menu");
    }

    public void LoadIntro()
    {
        SceneManager.LoadScene("Intro");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public void LoadFirstLevel()
    {
        GameManager.GetInstance().isInvasionLevel = false;
        GameManager.GetInstance().isEndLevel = false;
        GameManager.GetInstance().bringAliens = true;
        GameManager.GetInstance().isGameStarted = false;
        SceneManager.LoadScene("Level 1");
    }

    public void LoadMainMenu()
    {
        GameManager.GetInstance().isInvasionLevel = false;
        GameManager.GetInstance().isEndLevel = false;
        GameManager.GetInstance().bringAliens = false;
        GameManager.GetInstance().isGameStarted = false;
        GameManager.GetInstance().isGameOver = false;
        GameManager.GetInstance().Reset();
        SceneManager.LoadScene("Start Menu");
    }

    public void LoadInvasionLevel()
    {
        GameManager.GetInstance().isInvasionLevel = true;
        GameManager.GetInstance().isEndLevel = false;
        GameManager.GetInstance().bringAliens = true;
        GameManager.GetInstance().isGameStarted = false;
        SceneManager.LoadScene("Level Invasion");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReloadLevel()
    {
        GameManager.GetInstance().isGameStarted = false;
        GameManager.GetInstance().Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    internal void LoadNextScene()
    {
        GameManager.GetInstance().isGameStarted = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void LoadEndLevel()
    {
        GameManager.GetInstance().isInvasionLevel = false;
        GameManager.GetInstance().isEndLevel = true;
        GameManager.GetInstance().bringAliens = true;
        GameManager.GetInstance().isGameStarted = false;
        SceneManager.LoadScene("End Menu");
    }

    public void LoadIntro()
    {
        GameManager.GetInstance().isInvasionLevel = false;
        GameManager.GetInstance().isEndLevel = false;
        GameManager.GetInstance().bringAliens = false;
        GameManager.GetInstance().isGameStarted = false;
        SceneManager.LoadScene("Intro");
    }
}

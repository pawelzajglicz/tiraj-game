using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player playerPrefab;
    public int playersAmount;
    public Vector3 startPosition;
    public bool isLivesLimited = true;
    public int lifesLimit = 3;
    public bool isMenuActive = false;
    public GameObject menu;
    public LevelLoader levelLoader;
    public bool isInvasionLevel;
    public int remainedLifes;
    public Lifes lifes;
    public CinemachineVirtualCamera camera;
    public bool isGameOver;

    private static GameManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject.GetComponent<GameManager>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        lifes = FindObjectOfType<Lifes>();
        remainedLifes = lifesLimit;
        lifes.SetLifes(remainedLifes);
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public void Reset()
    {
        remainedLifes = lifesLimit;
        if (lifes != null)
        {
            lifes.SetLifes(remainedLifes);
        }
    }

    void Update()
    {
        playersAmount = Player.playersAmount;

        if (playersAmount <= 0)
        {
            if (!isInvasionLevel && !isGameOver)
            {
                remainedLifes--;
                if (lifes != null)
                {
                    lifes.SetLifes(remainedLifes);
                }
            }

            if (remainedLifes <= 0)
            {
                isGameOver = true;
                ShowGameOver();
            }
            else
            {
                Player newAlien = BringNewAlien();

                if (!isInvasionLevel)
                {
                    camera.Follow = newAlien.transform;
                }
            }
        }

        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) && !isGameOver))
        {
            if (isMenuActive)
            {
                HideMenu();
            }
            else
            {
                ShowMenu();
            }
        }

        if (isMenuActive || isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                levelLoader.ReloadLevel();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                levelLoader.LoadMainMenu();
            }
        }
    }

    private void ShowGameOver()
    {
        menu.SetActive(true);
        GameObject gameStatusText = menu.transform.Find("GameStatus").gameObject;
        TextMeshProUGUI textMesh = gameStatusText.GetComponent<TextMeshProUGUI>();
        textMesh.text = "Game Over";


        PlayerMove[] players = FindObjectsOfType<PlayerMove>();
        foreach (PlayerMove player in players)
        {
            player.Pause();
        }
    }

    private void HideMenu()
    {
        isMenuActive = false;
        menu.SetActive(false);
        PlayerMove[] players = FindObjectsOfType<PlayerMove>();
        foreach (PlayerMove player in players)
        {
            player.Resume();
        }
    }

    private void ShowMenu()
    {
        isMenuActive = true;
        menu.SetActive(true);
        PlayerMove[] players = FindObjectsOfType<PlayerMove>();
        foreach (PlayerMove player in players)
        {
            player.Pause();
        }
    }

    public Player BringNewAlien()
    {
        return Instantiate(playerPrefab, startPosition, Quaternion.identity);
    }

    internal void PlayerEnteredPortal()
    {
        if (isInvasionLevel)
        {

        }
        else
        {
            levelLoader.LoadNextScene();
        }
    }
}

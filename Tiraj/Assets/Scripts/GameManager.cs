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
    public int switchesStartNumber = 3;
    public int switchesLeft;
    public GravitySwitches gravitySwitches;

    internal void DecreaseSwitchNumber(int v)
    {
        switchesLeft -= v;
        gravitySwitches.SetSwitches(switchesLeft);
    }

    internal void IncreaseSwitchNumber(int v)
    {
        switchesLeft += v;
        gravitySwitches.SetSwitches(switchesLeft);
    }

    internal void IncreaseLifesNumber(int v)
    {
        remainedLifes += v;
        lifes.SetLifes(remainedLifes);
    }

    public CinemachineVirtualCamera camera;
    public bool isGameOver;
    public Score score;
    public int points = 0;
    public bool isEndLevel;
    public float timeForQuit = 2.5f;
    public bool bringAliens = true;
    public bool isGameStarted;



    private static GameManager instance;
    public float xBirthMovement = 7f;

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

    internal void ManageTimeEnd()
    {
        PlayerMove.horizontalDirection = 1;
        remainedLifes = 1;
        levelLoader.LoadEndLevel();
    }

    private void Start()
    {
        switchesLeft = switchesStartNumber;
        lifes = FindObjectOfType<Lifes>();
        remainedLifes = lifesLimit;
        if (!isInvasionLevel && lifes != null)
        {
            lifes.SetLifes(remainedLifes);
        }
        DontDestroyOnLoad(this);
        FindCamera();
    }

    public void FindCamera()
    {
        CinemachineVirtualCamera camera = FindObjectOfType<CinemachineVirtualCamera>();
        if (camera != null)
        {
            this.camera = camera;
        }
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public void Reset()
    {
        remainedLifes = lifesLimit;
        isGameOver = false;
        if (lifes != null)
        {
            lifes.SetLifes(remainedLifes);
        }
    }

    void Update()
    {
        playersAmount = Player.playersAmount;

        if (!isGameStarted)
        {
            if (playersAmount > 0)
            {
                isGameStarted = true;
            }
            else
            {
                return;
            }
        }

        
        if (!isInvasionLevel && !isGameOver && !isEndLevel)
        {
            FindCamera();

            if (menu == null)
            {
                Menu findedMenu = Menu.getInstance();
                if (findedMenu != null)
                {
                    menu = findedMenu.gameObject;
                    HideMenu();
                }
            }

            if (lifes == null)
            {
                Lifes findedLifes = Lifes.getInstance();
                if (findedLifes != null)
                {
                    lifes = findedLifes;
                }
            }

            if (gravitySwitches == null)
            {
                GravitySwitches findedGravitySwitches = GravitySwitches.getInstance();
                if (findedGravitySwitches != null)
                {
                    gravitySwitches = findedGravitySwitches;
                    gravitySwitches.SetSwitches(switchesLeft);
                }
            }
        }

        if (playersAmount <= 0)
        {
            if (!isInvasionLevel && !isGameOver && !isEndLevel)
            {
                remainedLifes--;
                if (lifes != null)
                {
                    lifes.SetLifes(remainedLifes);
                }


                if (remainedLifes <= 0)
                {
                    if (isEndLevel)
                    {
                        StartCoroutine(ProcessEnding());
                    }
                    else if (menu != null)
                    {
                        isGameOver = true;
                        ShowGameOver();
                    }
                }
                else if (bringAliens)
                {
                    //Player newAlien = BringNewAlien();

                    if (!isInvasionLevel && !isEndLevel && camera != null)
                    {
                        Vector3 cameraPos = camera.transform.position;
                        float newX = cameraPos.x - xBirthMovement;
                        if (newX < startPosition.x)
                        {
                            newX = startPosition.x;
                        }

                        Player newAlien = BringNewAlien(new Vector2(newX, startPosition.y));
                        camera.Follow = newAlien.transform;
                    }
                }
            }
            else if (isInvasionLevel)
            {
                BringNewAlien();
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
        if (menu != null)
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

    public Player BringNewAlien(Vector2 position)
    {
        return Instantiate(playerPrefab, position, Quaternion.identity);
    }

    internal void PlayerEnteredPortal()
    {
        if (isInvasionLevel)
        {
            
        }
        else if (isEndLevel)
        {
            levelLoader.LoadMainMenu();
        }
        else
        {
            levelLoader.LoadInvasionLevel();
        }
    }

    public void ManageScoreComponent()
    {
        if (score == null)
        {
            score = FindObjectOfType<Score>();
        }
    }

    public void AddPoints(int pointsToAdd)
    {
        if (isInvasionLevel)
        {
            ManageScoreComponent();

            points += pointsToAdd;
            score.SetPoints(points);
        }
    }

    public void RemovePoints(int pointsToRemove)
    {
        if (isInvasionLevel)
        {
            ManageScoreComponent();

            points -= pointsToRemove;
            score.SetPoints(points);
        } else if (isEndLevel)
        {
            points -= pointsToRemove;
        }
    }

    IEnumerator ProcessEnding()
    {
        yield return new WaitForSeconds(timeForQuit);
        
    }
}

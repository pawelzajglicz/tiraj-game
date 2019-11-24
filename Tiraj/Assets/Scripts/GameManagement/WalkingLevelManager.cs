using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalkingLevelManager : GameManagerBase
{
    private static WalkingLevelManager instance;
    [SerializeField] Player playerPrefab;
    public Vector3 startPosition;

    public Lifes lifes;
    public GravitySwitches gravitySwitches;
    public bool isMenuActive = false;
    public GameObject menu;
    public CinemachineVirtualCamera camera;
    public bool isGameOver;
    public LevelLoader levelLoader;
    private int playersAmount;
    public float xBirthMovement = 7f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject.GetComponent<WalkingLevelManager>();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public static WalkingLevelManager GetInstance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

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

    public override Player BringNewAlien()
    {
        return Instantiate(playerPrefab, startPosition, Quaternion.identity);
    }

    public Player BringNewAlien(Vector2 position)
    {
        return Instantiate(playerPrefab, position, Quaternion.identity);
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

        FlyingEnemy[] enemies = FindObjectsOfType<FlyingEnemy>();
        foreach (FlyingEnemy enemy in enemies)
        {
            enemy.Resume();
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

        FlyingEnemy[] enemies = FindObjectsOfType<FlyingEnemy>();
        foreach (FlyingEnemy enemy in enemies)
        {
            enemy.Pause();
        }
    }

    internal void DecreaseSwitchNumber(int v)
    {
        GameSessionInfo.GetInstance().RemoveGravitySwitch();
        int remainedGravitySwitches = GameSessionInfo.GetInstance().GetGravitySwitchesLeft();

        gravitySwitches.SetSwitches(remainedGravitySwitches);
    }

    internal void IncreaseSwitchNumber(int v)
    {
        GameSessionInfo.GetInstance().AddGravitySwitch();
        int remainedGravitySwitches = GameSessionInfo.GetInstance().GetGravitySwitchesLeft();

        gravitySwitches.SetSwitches(remainedGravitySwitches);
    }

    internal void IncreaseLifesNumber(int v)
    {
        GameSessionInfo.GetInstance().AddLife();
        int remainedLifes = GameSessionInfo.GetInstance().GetLifesLeft();

        lifes.SetLifes(remainedLifes);
    }

    public override void ManagePlayerDeath(int deathPoints)
    {
        GameSessionInfo.GetInstance().RemoveLife();
        int remainedLifes = GameSessionInfo.GetInstance().GetLifesLeft();

        lifes.SetLifes(remainedLifes);

        if (remainedLifes <= 0)
        {
            isGameOver = true;
            ShowGameOver();
        }
        else
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

    public override void RemoveGravitySwitch()
    {
        DecreaseSwitchNumber(1);
    }

    public override void ManagePlayerSuccess(int pointsForSuccess)
    {
        DecreaseSwitchNumber(1);
    }
    
    public override void PlayerEnteredPortal()
    {
        levelLoader.LoadNextScene();
    }

    public override bool IsGravitySwitchAllowed()
    {
        return true;
    }
}
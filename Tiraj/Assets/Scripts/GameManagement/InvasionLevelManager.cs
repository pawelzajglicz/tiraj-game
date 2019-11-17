using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvasionLevelManager : GameManagerBase
{
    private static InvasionLevelManager instance;
    [SerializeField] Player playerPrefab;
    public Vector3 startPosition;
    
    public LevelLoader levelLoader;
    public GameObject menu;
    public TimeLimit timeLimit;

    public Score score;
    public int points = 0;
    private int playersAmount;
    private bool isMenuActive;

    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject.GetComponent<InvasionLevelManager>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static InvasionLevelManager GetInstance()
    {
        return instance;
    }

    public override void ManageTimeEnd()
    {
        PlayerMove.horizontalDirection = 1;
        levelLoader.LoadEndLevel();
    }

    private void Update()
    {
        playersAmount = Player.playersAmount;

        if (playersAmount <= 0)
        {
            BringNewAlien();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
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

        if (isMenuActive)
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


    private void HideMenu()
    {
        isMenuActive = false;
        menu.SetActive(false);
        PlayerMove[] players = FindObjectsOfType<PlayerMove>();
        timeLimit.Activate();

        foreach (PlayerMove player in players)
        {
            player.Resume();
        }
    }

    private void ShowMenu()
    {
        isMenuActive = true;
        menu.SetActive(true);
        timeLimit.Deactivate();

        PlayerMove[] players = FindObjectsOfType<PlayerMove>();
        foreach (PlayerMove player in players)
        {
            player.Pause();
        }
    }


    public override Player BringNewAlien()
    {
        return Instantiate(playerPrefab, startPosition, Quaternion.identity);
    }

    public override void ManagePlayerDeath(int deathPoints)
    {
        DecreaseScoreAmount(deathPoints);
    }

    internal void DecreaseScoreAmount(int v)
    {
        GameSessionInfo.GetInstance().RemovePoints(v);
        int currentPoints = GameSessionInfo.GetInstance().GetCurrentPoints();

        score.SetPoints(currentPoints);
    }

    internal void IncreaseScoreAmount(int v)
    {
        GameSessionInfo.GetInstance().AddPoints(v);
        int currentPoints = GameSessionInfo.GetInstance().GetCurrentPoints();

        score.SetPoints(currentPoints);
    }

    public override void ManagePlayerSuccess(int successPoints)
    {
        IncreaseScoreAmount(successPoints);
    }

    public override bool IsGravitySwitchAllowed()
    {
        return false;
    }
}

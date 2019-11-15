using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player playerPrefab;
    public int playersAmount;
    public Vector3 startPosition;
    public bool isLivesLimited = true;
    public int livesLimit = 3;
    public bool isMenuActive = false;

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

    public static GameManager GetInstance()
    {
        return instance;
    }

    void Update()
    {
        playersAmount = Player.playersAmount;

        if (playersAmount <= 0)
        {
            BringNewAlien();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.M))
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
    }

    private void HideMenu()
    {
        isMenuActive = false;
    }

    private void ShowMenu()
    {
        isMenuActive = true;
        print("menu");
    }

    public void BringNewAlien()
    {
        Instantiate(playerPrefab, startPosition, Quaternion.identity);
    }
}

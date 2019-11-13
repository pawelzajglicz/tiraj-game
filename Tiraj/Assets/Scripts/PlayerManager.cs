using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Player playerPrefab;
    public int playersAmount;
    public Vector3 startPosition;

    private static PlayerManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject.GetComponent<PlayerManager>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static PlayerManager GetInstance()
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
    }

    public void BringNewAlien()
    {
        Instantiate(playerPrefab, startPosition, Quaternion.identity);
    }
}

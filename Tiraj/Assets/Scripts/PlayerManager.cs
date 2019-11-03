using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Player playerPrefab;
    public int playerAmount;

    // Update is called once per frame
    void Update()
    {
        int playersAmount = FindObjectsOfType<Player>().Length;
        playerAmount = playersAmount;

        if (playersAmount <= 1)
            playerPrefab.BringNewAlien();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Player playerPrefab;

    // Update is called once per frame
    void Update()
    {
        int playersAmount = FindObjectsOfType<Player>().Length;

        if (playersAmount <= 1)
            playerPrefab.BringNewAlien();
    }
}

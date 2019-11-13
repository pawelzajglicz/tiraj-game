using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpPlatform : MonoBehaviour
{
    public bool isActive = true;
    private List<Player> players;

    private void Start()
    {
        players = new List<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (!isActive) return;

        GameObject colliderGameObject = collider.gameObject;
        if (colliderGameObject.CompareTag("Player"))
        {
            Player player = colliderGameObject.GetComponent<Player>();
            player.MakeGrowing();

            players.Add(player);
        }
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        if (!isActive) return;

        GameObject colliderGameObject = collider.gameObject;
        if (colliderGameObject.CompareTag("Player"))
        {
            Player player = colliderGameObject.GetComponent<Player>();
            player.StopGrowing();
        }
    }

    internal void Activate()
    {
        isActive = true;
    }

    internal void Deactivate()
    {
        isActive = false;
        if (players == null || players.Count == 0) return;

        foreach (Player player in players)
        {
            player.StopGrowing();
        }
    }
}

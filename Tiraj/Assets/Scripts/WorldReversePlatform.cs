﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldReversePlatform : MonoBehaviour
{
    private Camera camera;

    private void Start()
    {
        camera = FindObjectOfType<Camera>();
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Player player = collider.gameObject.GetComponent<Player>();
            if (player.isAllowedToReverseWorld())
            {

                player.worldReversingCounter++;
                camera.transform.Rotate(0, 0, 180, Space.Self);

                Player[] players = FindObjectsOfType<Player>();
                foreach (Player pl in players)
                {
                    pl.ReverseHorizontal();
                }
            }
        }

    }
}

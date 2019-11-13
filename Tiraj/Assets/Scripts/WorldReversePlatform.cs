using System.Collections;
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
        GameObject colliderGameObject = collider.gameObject;
        if (colliderGameObject.CompareTag("Player"))
        {
            ManageWorldReversing(colliderGameObject);
        }

    }

    private void ManageWorldReversing(GameObject colliderGameObject)
    {
        Player player = colliderGameObject.GetComponent<Player>();
        if (player.IsAllowedToReverseWorld())
        {
            ReverseWorld(player);
        }
    }

    private void ReverseWorld(Player player)
    {
        player.worldReversingCounter++;
        camera.transform.Rotate(0, 0, 180, Space.Self);

        PlayerMove.ReverseHorizontal();
    }
}

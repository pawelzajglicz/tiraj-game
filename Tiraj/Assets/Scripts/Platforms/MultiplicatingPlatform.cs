using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplicatingPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collider)
    {
        GameObject colliderGameObject = collider.gameObject;
        if (colliderGameObject.CompareTag("Player"))
        {
            Player player = colliderGameObject.GetComponent<Player>();
            player.BringNewAlien();
        }
    }
}

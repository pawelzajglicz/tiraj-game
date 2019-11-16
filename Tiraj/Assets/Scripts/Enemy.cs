using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collider)
    {
        GameObject colliderGameObject = collider.gameObject;
        if (colliderGameObject.CompareTag("Player"))
        {
            colliderGameObject.GetComponent<Player>().Plop();
        }

    }
}

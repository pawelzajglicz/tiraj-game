using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyDestroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collider)
    {
        GameObject colliderGameObject = collider.gameObject;
        if (colliderGameObject.CompareTag("Player"))
        {
            FlyingEnemy[] enemies = FindObjectsOfType<FlyingEnemy>();

            if (enemies != null && enemies.Length > 0)
            {
                for(int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].Die();
                }
            }

            Destroy(gameObject);
        }
    }
}

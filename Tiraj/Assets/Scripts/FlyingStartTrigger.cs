using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingStartTrigger : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collider)
    {
        GameObject colliderGameObject = collider.gameObject;
        if (colliderGameObject.CompareTag("Player"))
        {
            FlyingEnemy[] enemies = FindObjectsOfType<FlyingEnemy>();

            if (enemies != null && enemies.Length > 0)
            {
                foreach (FlyingEnemy enemy in enemies)
                {
                    enemy.StartFly();
                }
            }

            Destroy(gameObject);
        }
    }
}

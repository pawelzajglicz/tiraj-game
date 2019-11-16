using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public int gravitySwitchBonus = 1;
    public int lifeBonus = 1;


    private void OnCollisionEnter2D(Collision2D collider)
    {
        GameObject colliderGameObject = collider.gameObject;
        if (colliderGameObject.CompareTag("Player"))
        {
            GiveBonus();
        }
    }

    private void GiveBonus()
    {
        GameManager.GetInstance().IncreaseSwitchNumber(gravitySwitchBonus);
        GameManager.GetInstance().IncreaseLifesNumber(lifeBonus);


        Destroy(gameObject);
    }
}

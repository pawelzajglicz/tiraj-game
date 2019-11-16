using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour
{
    public float liveTimeLimit = 2.5f;
    public float liveTime = 0f;

    private void Update()
    {
        liveTime += Time.deltaTime;

        if (liveTime > liveTimeLimit)
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collider)
    {

        GameObject colliderGameObject = collider.gameObject;
        if (colliderGameObject.CompareTag("Player"))
        {
            colliderGameObject.GetComponent<Player>().Plop();
        }
    }
}

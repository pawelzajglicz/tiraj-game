using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector2(10f, 0f);
    [SerializeField] float period = 10f;

    float movementFactor;

    Vector2 startingPos;
    
    void Start()
    {
        startingPos = transform.position;
    }
    
    void Update()
    {

        if (period <= Mathf.Epsilon) return;

        float cycles = Time.time / period; // grows continually from 0

        const float tau = Mathf.PI * 2; // about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSinWave / 2f + 0.5f;
        Vector2 offset = movementFactor * movementVector;
        transform.position = startingPos + offset;
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        GameObject colliderGameObject = collider.gameObject;
        if (colliderGameObject.CompareTag("Player"))
        {
            colliderGameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        GameObject colliderGameObject = collider.gameObject;
        if (colliderGameObject.CompareTag("Player"))
        {
            colliderGameObject.transform.parent = null;
        }
    }
}

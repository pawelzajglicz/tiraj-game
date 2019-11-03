using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsPlatform : MonoBehaviour
{
    
    public Vector2 stepOffset = new Vector2(2f, 0.5f);
    public int numberOfSteps = 5;
    public float timeInPosition = 2f;


    private Vector2 startPosition;
    public bool isInteractable = true;
    private int currentStep = 0;

    void Start()
    {
        startPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (!isInteractable) return;

        if (collider.gameObject.tag == "Player")
        {
            ProcessSteppingSequence();
        }
    }

    private void ProcessSteppingSequence()
    {
        isInteractable = false;
        StartCoroutine(ProcessStepping());
    }

    IEnumerator ProcessStepping()
    {
        yield return new WaitForSeconds(timeInPosition);
        currentStep++;

        if (currentStep <= numberOfSteps)
        { 
        transform.position = new Vector2(transform.position.x + stepOffset.x, transform.position.y + stepOffset.y);
        StartCoroutine(ProcessStepping());
        }
        else
        {
            transform.position = startPosition;
            isInteractable = true;
            currentStep = 0;
        }
    }
}

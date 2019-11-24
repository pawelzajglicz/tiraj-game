using System;
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
    public int currentStep = 0;
    /*public int currentStep
    {
        get
        {
            return currentStep;
        }
        set
        {
            currentStep = value;
            print(currentStep);
            BroadcastMessage("ReactToCurrentStep", currentStep);
        }
    }*/



    void Start()
    {
        startPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (!isInteractable) return;

        GameObject colliderGameObject = collider.gameObject;
        if (colliderGameObject.CompareTag("Player"))
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
        AddStep();

        if (currentStep <= numberOfSteps)
        {
            ProcessNextStep();
        }
        else
        {
            EndStepping();
        }
    }

    private void AddStep()
    {
        currentStep++;
        BroadcastMessage("ReactToCurrentStep", currentStep);
    }

    private void ProcessNextStep()
    {
        transform.position = new Vector2(transform.position.x + stepOffset.x, transform.position.y + stepOffset.y);
        StartCoroutine(ProcessStepping());
    }

    private void EndStepping()
    {
        transform.position = startPosition;
        isInteractable = true;
        currentStep = 0;
    }
}

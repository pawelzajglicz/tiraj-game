﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StairsPumpMixPlatform : MonoBehaviour
{
    public Vector2 stepOffset = new Vector2(2f, 0.5f);
    public int numberOfSteps = 5;
    public float timeInPosition = 2f;


    private Vector2 startPosition;
    private bool isInteractable = true;
    private int currentStep = 0;

    private PumpPlatform pumpPlatform;
    public int[] pumpSteps = {2};

    private SpriteRenderer sprtiteRenderer;
    private Color orange = new Color(1.0f, 0.5f, 0f);

    void Start()
    {
        startPosition = transform.position;
        pumpPlatform = GetComponent<PumpPlatform>();
        pumpPlatform.Deactivate();
        sprtiteRenderer = GetComponent<SpriteRenderer>();
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
        if (pumpSteps.Contains(currentStep))
        {
            pumpPlatform.Activate();
            sprtiteRenderer.color = orange;
        }
        else
        {
            pumpPlatform.Deactivate();
            sprtiteRenderer.color = Color.white;
        }

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

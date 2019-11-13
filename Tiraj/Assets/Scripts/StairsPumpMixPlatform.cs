using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[RequireComponent(typeof(PumpPlatform))]
[RequireComponent(typeof(StairsPlatform))]
public class StairsPumpMixPlatform : MonoBehaviour
{

    private PumpPlatform pumpPlatform;
    private StairsPlatform stairsPlatform;
    public int[] pumpSteps = {2};

    private SpriteRenderer sprtiteRenderer;
    private Color orange = new Color(1.0f, 0.5f, 0f);

    void Start()
    {
        pumpPlatform = GetComponent<PumpPlatform>();
        stairsPlatform = GetComponent<StairsPlatform>();
        pumpPlatform.Deactivate();
        sprtiteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void ReactToCurrentStep(int stepNumber)
    {
        if (pumpSteps.Contains(stepNumber))
        {
            BecomePumping();
        }
        else
        {
            BecomeNormal();
        }
    }

    private void BecomePumping()
    {
        pumpPlatform.Activate();
        sprtiteRenderer.color = orange;
    }

    private void BecomeNormal()
    {
        pumpPlatform.Deactivate();
        sprtiteRenderer.color = Color.white;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingSummary : MonoBehaviour
{
    private int points;
    TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    private void Update()
    {
        if (GameManager.GetInstance().points != points)
        {
            points = GameManager.GetInstance().points;
            UpdateText();
        }
    }

    private void UpdateText()
    {
        textMesh.text = "Your actions result in " + points + " aliens invading earth" + "\n" + "Do you prefer start again or quit?";
    }
}

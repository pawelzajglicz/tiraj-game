﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lifes : MonoBehaviour
{
    Text lifesText;
    int lifes;

    void Start()
    {
        lifesText = GetComponent<Text>();
        lifesText.text = GameManager.GetInstance().remainedLifes.ToString();
    }


    public void AddLives(int lifesToAdd)
    {
        lifes += lifesToAdd;
        lifesText.text = lifes.ToString();
    }

    public void RemovePoints(int lifesToRemove)
    {
        lifes -= lifesToRemove;
        lifesText.text = lifes.ToString();
    }

    public void SetLifes(int newValue)
    {
        lifes = newValue;
        if (lifesText != null)
        {
            lifesText.text = lifes.ToString();
        }
    }
}

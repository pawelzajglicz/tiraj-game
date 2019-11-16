using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text scoreText;
    int points;
    
    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = "0";
    }


    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        scoreText.text = points.ToString();
    }

    public void RemovePoints(int pointsToRemove)
    {
        points -= pointsToRemove;
        scoreText.text = points.ToString();
    }

    public void SetPoints(int newValue)
    {
        points = newValue;
        scoreText.text = points.ToString();
    }

    
}

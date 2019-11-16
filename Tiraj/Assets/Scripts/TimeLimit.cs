using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimit : MonoBehaviour
{
    Text timeText;
    public float timeLimit = 33f;
    public float currentTime;

    void Start()
    {
        currentTime = timeLimit;
        timeText = GetComponent<Text>();
        timeText.text = ((int)currentTime).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        timeText.text = ((int)currentTime).ToString();

        if (currentTime < 0)
        {
            ManageTimeEnded();
        }
    }

    private void ManageTimeEnded()
    {
        GameManager.GetInstance().ManageTimeEnd();
    }
}
